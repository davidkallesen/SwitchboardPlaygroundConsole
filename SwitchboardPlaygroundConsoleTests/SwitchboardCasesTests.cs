namespace SwitchboardPlaygroundConsoleTests;

public class SwitchboardCasesTests
{
    private readonly ITextOutput output;

    public SwitchboardCasesTests(ITestOutputHelper output)
    {
        this.output = new TestOutputHelperAdapter(output);
    }

    [Fact]
    public void Hallo()
    {
        // TODO: var caseNames = TestCasesResourcesParser.GetCaseNames();

        var switchboard = TestCasesResourcesParser.GetSwitchboardByCaseName("Case1");
        ConsoleHelper.Render(switchboard, output);
        Assert.True(false);
    }
}