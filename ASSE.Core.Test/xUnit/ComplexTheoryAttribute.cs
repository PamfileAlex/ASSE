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

using Xunit.Sdk;

namespace ASSE.Core.Test.xUnit
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[XunitTestCaseDiscoverer("ASSE.Core.Test.xUnit.ComplexTheoryDiscoverer", "ASSE.Core.Test")]
	public class ComplexTheoryAttribute : FactAttribute
	{
	}
}
