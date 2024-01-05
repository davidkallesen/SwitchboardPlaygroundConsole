namespace SwitchboardCalculator.Tests.XUnitTestData;

internal static class TestMemberDataForCases
{
    public static TheoryData<string> TestCaseParser
    {
        get
        {
            var data = new TheoryData<string>();
            var caseNames = TestCasesResourcesParser.GetCaseNames();
            foreach (var caseName in caseNames.Where(x => x.StartsWith(nameof(TestCaseParser), StringComparison.Ordinal)))
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
            foreach (var caseName in caseNames.Where(x => x.StartsWith(nameof(FindPathBreadthFirstSearch), StringComparison.Ordinal)))
            {
                data.Add(caseName);
            }

            return data;
        }
    }

    public static TheoryData<string> FindPathDijkstra
    {
        get
        {
            var data = new TheoryData<string>();
            var caseNames = TestCasesResourcesParser.GetCaseNames();
            foreach (var caseName in caseNames.Where(x => x.StartsWith(nameof(FindPathDijkstra), StringComparison.Ordinal)))
            {
                data.Add(caseName);
            }

            return data;
        }
    }
}