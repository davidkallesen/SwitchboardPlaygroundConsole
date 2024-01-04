namespace SwitchboardPlaygroundConsoleTests;

[UsesVerify]
public class SwitchboardCasesTests
{
    private readonly ITextOutput output;
    private readonly VerifySettings verifySettings = new();

    public SwitchboardCasesTests(ITestOutputHelper output)
    {
        this.output = new StringHelperAdapter(output);
    }

    [Theory]
    [MemberData(nameof(TestMemberDataForCases.TestCaseData), MemberType = typeof(TestMemberDataForCases))]
    public Task TestCaseParser(string caseName)
    {
        var switchboard = TestCasesResourcesParser.GetSwitchboardByCaseName(caseName);

        ConsoleHelper.Render(switchboard, output);

        verifySettings.UseFileName($"TestCaseParser_{caseName}");
        return Verify(output.GetOutput(), verifySettings);
    }
}