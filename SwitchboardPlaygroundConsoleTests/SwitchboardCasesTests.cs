namespace SwitchboardPlaygroundConsoleTests;

public class SwitchboardCasesTests
{
    private readonly ITextOutput output;

    public SwitchboardCasesTests(ITestOutputHelper output)
    {
        this.output = new TestOutputHelperAdapter(output);
    }

    [Theory]
    [MemberData(nameof(TestMemberDataForCases.TestCaseData), MemberType = typeof(TestMemberDataForCases))]
    public void TestCases(string caseName)
    {
        var switchboard = TestCasesResourcesParser.GetSwitchboardByCaseName(caseName);
        ConsoleHelper.Render(switchboard, output);
        Assert.True(false);
    }
}