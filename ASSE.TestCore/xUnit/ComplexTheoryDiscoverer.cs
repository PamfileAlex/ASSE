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

using Xunit.Abstractions;
using Xunit.Sdk;

namespace ASSE.TestCore.xUnit
{
	public class ComplexTheoryDiscoverer : IXunitTestCaseDiscoverer
	{
		protected readonly IMessageSink _MessageSink;

		public ComplexTheoryDiscoverer(IMessageSink messageSink)
		{
			_MessageSink = messageSink;
		}

		public IMessageSink MessageSink { get { return _MessageSink; } }

		public IEnumerable<IXunitTestCase> Discover(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod,
			IAttributeInfo factAttribute)
		{
			var skipReason = factAttribute.GetNamedArgument<string>("Skip");
			if (skipReason != null)
				return CreateTestCasesForSkip(discoveryOptions, testMethod, factAttribute, skipReason);

			if (discoveryOptions.PreEnumerateTheoriesOrDefault())
			{
				try
				{
					var dataAttributes = testMethod.Method.GetCustomAttributes(typeof(DataAttribute)).ToList();
					var results = new List<IXunitTestCase>();

					for (int index = 0; index < dataAttributes.Count; index++)
					{
						var discovererAttribute =
							dataAttributes[index]
							.GetCustomAttributes(typeof(DataDiscovererAttribute))
							.First();

						var discoverer = ExtensibilityPointFactory.GetDataDiscoverer(MessageSink, discovererAttribute);
						skipReason = dataAttributes[index].GetNamedArgument<string>("Skip");

						if (!discoverer.SupportsDiscoveryEnumeration(dataAttributes[index], testMethod.Method))
							return new[] { CreateTestCaseForTheory(discoveryOptions, testMethod, factAttribute) };

						IEnumerable<object[]> data =
							discoverer.GetData(dataAttributes[index], testMethod.Method).ToList();

						if (data is IDictionary<string, object[]>)
						{
							foreach (KeyValuePair<string, object[]> dataRow in (IDictionary<string, object[]>)data)
							{
								var testCase =
									skipReason != null
										? CreateTestCaseForSkippedDataRow(discoveryOptions, testMethod, factAttribute, dataRow.Value, skipReason)
										: CreateTestCaseForNamedDataRow(discoveryOptions, testMethod, factAttribute, index, dataRow.Key);

								results.Add(testCase);
							}
						}
						else if (data.GroupBy(x => (x[0]?.ToString()) ?? string.Empty).All(x => x.Count() == 1))
						{
							foreach (object[] dataRow in data)
							{
								var testCase =
									skipReason != null
										? CreateTestCaseForSkippedDataRow(discoveryOptions, testMethod, factAttribute, dataRow, skipReason)
										: CreateTestCaseForNamedDataRow(discoveryOptions, testMethod, factAttribute, index, dataRow[0].ToString() ?? string.Empty);

								results.Add(testCase);
							}
						}
						else
						{
							results.AddRange(
								data.Select((x, i) =>
									skipReason != null ?
										CreateTestCaseForSkippedDataRow(discoveryOptions, testMethod, factAttribute, x, skipReason) :
										CreateTestCaseForDataRow(discoveryOptions, testMethod, factAttribute, index, i)));
						}
					}

					if (results.Count == 0)
						results.Add(
							new ExecutionErrorTestCase(
								MessageSink,
								discoveryOptions.MethodDisplayOrDefault(),
								TestMethodDisplayOptions.None,
								testMethod,
								$"No data found for {testMethod.TestClass.Class.Name}.{testMethod.Method.Name}"));

					return results;
				}
				catch (Exception ex)    // If something goes wrong, fall through to return just the XunitTestCase
				{
					MessageSink.OnMessage(
						new DiagnosticMessage($"Exception thrown during theory discovery on '{testMethod.TestClass.Class.Name}.{testMethod.Method.Name}'; falling back to single test case.{Environment.NewLine}{ex}"));
				}
			}

			return CreateTestCasesForTheory(discoveryOptions, testMethod, factAttribute);
		}

		protected virtual IEnumerable<IXunitTestCase> CreateTestCasesForTheory(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod, IAttributeInfo theoryAttribute)
		{
			return new[] {
				CreateTestCaseForTheory(
					discoveryOptions,
					testMethod,
					theoryAttribute) };
		}

		protected virtual IEnumerable<IXunitTestCase> CreateTestCasesForSkip(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod, IAttributeInfo theoryAttribute, string skipReason)
		{
			return new[] {
				CreateTestCaseForSkip(
					discoveryOptions,
					testMethod,
					theoryAttribute,
					skipReason) };
		}

		protected virtual IXunitTestCase CreateTestCaseForSkip(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod, IAttributeInfo factAttribute, string skipReason)
		{
			return new XunitTestCase(
				_MessageSink,
				discoveryOptions.MethodDisplayOrDefault(),
				TestMethodDisplayOptions.None,
				testMethod);
		}

		protected virtual IXunitTestCase CreateTestCaseForTheory(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod, IAttributeInfo factAttribute, string skipReason = null)
		{
			return new XunitTheoryTestCase(
				MessageSink,
				discoveryOptions.MethodDisplayOrDefault(),
				TestMethodDisplayOptions.None,
				testMethod);
		}

		protected virtual IEnumerable<IXunitTestCase> CreateTestCasesForSkippedDataRow(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod, IAttributeInfo theoryAttribute,
			object[] dataRow, string skipReason)
		{
			return new[] {
				CreateTestCaseForSkippedDataRow(
					discoveryOptions,
					testMethod,
					theoryAttribute,
					dataRow,
					skipReason) };
		}

		protected virtual IXunitTestCase CreateTestCaseForSkippedDataRow(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod, IAttributeInfo factAttribute,
			object[] dataRow, string skipReason)
		{
			return new XunitSkippedDataRowTestCase(
				MessageSink,
				discoveryOptions.MethodDisplayOrDefault(),
				TestMethodDisplayOptions.None,
				testMethod,
				skipReason,
				dataRow);
		}

		protected virtual IXunitTestCase CreateTestCaseForDataRow(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod, IAttributeInfo theoryAttribute,
			int dataAttributeNumber, int dataRowNumber)
		{
			return new ComplexDataRowTestCase(
				MessageSink,
				discoveryOptions.MethodDisplayOrDefault(),
				TestMethodDisplayOptions.None,
				testMethod,
				dataAttributeNumber,
				dataRowNumber);
		}

		protected virtual IXunitTestCase CreateTestCaseForNamedDataRow(
			ITestFrameworkDiscoveryOptions discoveryOptions,
			ITestMethod testMethod,
			IAttributeInfo theoryAttribute,
			int dataAttributeNumber,
			string dataRowName)
		{
			return new ComplexNamedDataRowTestCase(
				MessageSink,
				discoveryOptions.MethodDisplayOrDefault(),
				TestMethodDisplayOptions.None,
				testMethod,
				dataAttributeNumber,
				dataRowName);
		}

	}
}
