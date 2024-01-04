namespace SwitchboardPlaygroundConsoleTests.XUnitTestData;

internal static class TestMemberDataForCases
{
    public static TheoryData<string> TestCaseData
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