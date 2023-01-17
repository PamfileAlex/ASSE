//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Required so that Test Explorer recognizes Theory tests with custom objects (user defined) as different
//	tests and not grouping them together as a single test. Using this method, tests are shown as separate.
//	Classes were renamed to reflect the solution solved
//
//	Solution was found on the following StackOverflow links:
//	https://stackoverflow.com/questions/46749152/xunit-display-test-names-for-theory-memberdata-testcase
//	https://stackoverflow.com/a/43968005
//
//	https://stackoverflow.com/questions/30574322/memberdata-tests-show-up-as-one-test-instead-of-many
//	https://stackoverflow.com/a/46755286
//
//	Link to files on the DjvuNet repo:
//	https://github.com/DjvuNet/DjvuNet/tree/master/DjvuNet.Shared.Tests/xunit
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel;
using Xunit.Abstractions;
using Xunit.Sdk;
using DiagnosticMessage = Xunit.Sdk.DiagnosticMessage;
using NullMessageSink = Xunit.Sdk.NullMessageSink;
using TestMethodDisplay = Xunit.Sdk.TestMethodDisplay;
using TestMethodDisplayOptions = Xunit.Sdk.TestMethodDisplayOptions;

namespace ASSE.Core.Test.xUnit
{
	public class ComplexNamedDataRowTestCase : TestMethodTestCase, IXunitTestCase
	{
		private readonly IMessageSink _messageSink;
		private int _attributeNumber;
		private string _rowName;

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use for deserialization only.")]
		public ComplexNamedDataRowTestCase()
		{
			_messageSink = new NullMessageSink();
			_rowName = string.Empty;
		}

		public ComplexNamedDataRowTestCase(
			IMessageSink messageSink,
			TestMethodDisplay defaultMethodDisplay,
			TestMethodDisplayOptions methodDisplayOptions,
			ITestMethod testMethod,
			int attributeNumber,
			string rowName)
			: base(
				  defaultMethodDisplay,
				  methodDisplayOptions,
				  testMethod,
				  GetTestMethodArguments(testMethod, attributeNumber, rowName, messageSink))
		{
			_attributeNumber = attributeNumber;
			_rowName = rowName;
			_messageSink = messageSink;
		}

		/// <summary>
		/// TODO implement interface memeber
		/// </summary>
		public virtual int Timeout { get; }

		protected virtual string GetDisplayName(IAttributeInfo factAttribute, string displayName)
		{
			return TestMethod.Method
				.GetDisplayNameWithArguments(displayName, TestMethodArguments, MethodGenericTypes);
		}

		protected virtual string GetSkipReason(IAttributeInfo factAttribute)
		{
			return factAttribute.GetNamedArgument<string>("Skip");
		}

		/// <inheritdoc/>
		protected override void Initialize()
		{
			base.Initialize();

			var factAttribute = TestMethod.Method.GetCustomAttributes(typeof(FactAttribute)).First();
			var baseDisplayName = factAttribute.GetNamedArgument<string>("DisplayName") ?? BaseDisplayName;

			DisplayName = GetDisplayName(factAttribute, baseDisplayName);
			SkipReason = GetSkipReason(factAttribute);

			foreach (var traitAttribute in GetTraitAttributesData(TestMethod))
			{
				var discovererAttribute =
					traitAttribute.GetCustomAttributes(typeof(TraitDiscovererAttribute)).FirstOrDefault();

				if (discovererAttribute != null)
				{
					var discoverer = ExtensibilityPointFactory.GetTraitDiscoverer(_messageSink, discovererAttribute);
					if (discoverer != null)
						foreach (var keyValuePair in discoverer.GetTraits(traitAttribute))
							Add(Traits, keyValuePair.Key, keyValuePair.Value);
				}
				else
					_messageSink.OnMessage(
						new DiagnosticMessage(
							$"Trait attribute on '{DisplayName}' did not have [TraitDiscoverer]"));
			}
		}

		static void Add<TKey, TValue>(IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
		{
			(dictionary[key] ?? (dictionary[key] = new List<TValue>())).Add(value);
		}

		static IEnumerable<IAttributeInfo> GetTraitAttributesData(ITestMethod testMethod)
		{
			return testMethod.TestClass.Class.Assembly.GetCustomAttributes(typeof(ITraitAttribute))
				.Concat(testMethod.Method.GetCustomAttributes(typeof(ITraitAttribute)))
				.Concat(testMethod.TestClass.Class.GetCustomAttributes(typeof(ITraitAttribute)));
		}


		static object[]? GetTestMethodArguments(
			ITestMethod testMethod, int attributeNumber, string rowName, IMessageSink diagnosticMessageSink)
		{
			try
			{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
				IAttributeInfo dataAttribute =
					testMethod.Method
					.GetCustomAttributes(typeof(DataAttribute))
					.Where((x, i) => i == attributeNumber)
					.FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

				if (dataAttribute == null)
					return null;

				IAttributeInfo discovererAttribute =
					dataAttribute.GetCustomAttributes(typeof(DataDiscovererAttribute)).First();

				IDataDiscoverer discoverer =
					ExtensibilityPointFactory.GetDataDiscoverer(diagnosticMessageSink, discovererAttribute);

				IEnumerable<object[]> data = discoverer.GetData(dataAttribute, testMethod.Method);

				if (data is IDictionary<string, object[]> dictionary)
					return dictionary[rowName];

				return data.FirstOrDefault(x => x[0].ToString() == rowName);
			}
			catch
			{
				return null;
			}
		}

		public override void Serialize(IXunitSerializationInfo data)
		{
			data.AddValue("TestMethod", TestMethod);
			data.AddValue("AttributeNumber", _attributeNumber);
			data.AddValue("RowName", _rowName);
		}

		public override void Deserialize(IXunitSerializationInfo data)
		{
			TestMethod = data.GetValue<ITestMethod>("TestMethod");
			_attributeNumber = data.GetValue<int>("AttributeNumber");
			_rowName = data.GetValue<string>("RowName");
			TestMethodArguments = GetTestMethodArguments(TestMethod, _attributeNumber, _rowName, _messageSink);
		}

		protected override string GetUniqueID()
		{
			return
				$"{TestMethod.TestClass.TestCollection.TestAssembly.Assembly.Name};" +
				$"{TestMethod.TestClass.Class.Name};" +
				$"{TestMethod.Method.Name};" +
				$"{_attributeNumber}/{_rowName}";
		}

		/// <inheritdoc/>
		public virtual Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
												 IMessageBus messageBus,
												 object[] constructorArguments,
												 ExceptionAggregator aggregator,
												 CancellationTokenSource cancellationTokenSource)
		{
			return
				new XunitTestCaseRunner(
					this,
					DisplayName,
					SkipReason,
					constructorArguments,
					TestMethodArguments,
					messageBus,
					aggregator,
					cancellationTokenSource).RunAsync();
		}
	}
}
