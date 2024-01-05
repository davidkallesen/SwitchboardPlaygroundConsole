namespace SwitchboardCalculator.Tests;

[UsesVerify]
public class SwitchboardCasesTests
{
    private readonly ITextOutput output;
    private readonly VerifySettings verifySettings;

    public SwitchboardCasesTests(ITestOutputHelper output)
    {
        this.output = new StringHelperAdapter(output);
        
        verifySettings = new VerifySettings();
        verifySettings.UseDirectory("TestCasesVerify");
        verifySettings.DisableDiff();
    }

    [Theory]
    [MemberData(nameof(TestMemberDataForCases.TestCaseParser), MemberType = typeof(TestMemberDataForCases))]
    public Task TestCaseParser(string caseName)
    {
        // Arrange
        verifySettings.UseFileName(caseName);
        var data = TestCasesResourcesParser.GetSwitchboardDataByCaseName(caseName);

        ConsoleHelper.Render(data.Switchboard, output);

        // Assert
        return Verify(output.GetOutput(), verifySettings);
    }

    // [Theory]
    // [MemberData(nameof(TestMemberDataForCases.FindPathBreadthFirstSearch), MemberType = typeof(TestMemberDataForCases))]
    public Task FindPathBreadthFirstSearch(string caseName)
    {
        // Arrange
        verifySettings.UseFileName(caseName);
        var data = TestCasesResourcesParser.GetSwitchboardDataByCaseName(caseName);

        var pathCells = data.Switchboard.FindPathBreadthFirstSearch(data.Start, data.Target);
        if (pathCells.Length == 0)
        {
            ConsoleHelper.Render(data.Switchboard, output);
            var s = output.GetOutput() + Environment.NewLine + "No path found";
            return Verify(s, verifySettings);
        }

        foreach (var cell in pathCells)
        {
            data.Switchboard.GetCell(cell.Location).SetInOut(cell.In, cell.Out);
        }

        ConsoleHelper.Render(data.Switchboard, output);
        return Verify(output.GetOutput(), verifySettings);
    }

    [Theory]
    [MemberData(nameof(TestMemberDataForCases.FindPathBreadthFirstSearch), MemberType = typeof(TestMemberDataForCases))]
    public Task FindPathDijkstra(string caseName)
    {
        // Arrange
        verifySettings.UseFileName(caseName);
        var data = TestCasesResourcesParser.GetSwitchboardDataByCaseName(caseName);

        var pathCells = data.Switchboard.FindPathDijkstra(data.Start, data.Target);
        if (pathCells.Length == 0)
        {
            ConsoleHelper.Render(data.Switchboard, output);
            var s = output.GetOutput() + Environment.NewLine + "No path found";
            return Verify(s, verifySettings);
        }

        foreach (var cell in pathCells)
        {
            data.Switchboard.GetCell(cell.Location).SetInOut(cell.In, cell.Out);
        }

        ConsoleHelper.Render(data.Switchboard, output);
        return Verify(output.GetOutput(), verifySettings);
    }
}