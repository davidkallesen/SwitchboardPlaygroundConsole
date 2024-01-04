namespace SwitchboardCalculator.Tests.XUnitTestData;

internal static class TestMemberDataForCases
{
    public static TheoryData<string> TestCaseParser
    {
        get
        {
            var data = new TheoryData<string>();
            var caseNames = TestCasesResourcesParser.GetCaseNames();
            foreach (var caseName in caseNames)
            {
                data.Add(caseName);
            }

            return data;
        }
    }

    public static TheoryData<string> FindPathBreadthFirstSearch
    {
        get
        {
            var data = new TheoryData<string>();
            var caseNames = TestCasesResourcesParser.GetCaseNames();
            foreach (var caseName in caseNames)
            {
                data.Add(caseName);
            }

            return data;
        }
    }
}