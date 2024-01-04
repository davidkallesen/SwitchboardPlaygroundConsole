namespace SwitchboardCalculator.Benchmark;

[MemoryDiagnoser]
public class BenchmarkTests
{
    private (Switchboard Switchboard, Point Start, Point Target) DataForTestCaseParserCase1;
    private (Switchboard Switchboard, Point Start, Point Target) DataForTestCaseParserCase2;
    private (Switchboard Switchboard, Point Start, Point Target) DataForCaseAlreadyConnected;
    private (Switchboard Switchboard, Point Start, Point Target) DataForCaseCircularPath;
    private (Switchboard Switchboard, Point Start, Point Target) DataForCaseNoPossiblePath;
    
    [GlobalSetup]
    public void Setup()
    {
        DataForTestCaseParserCase1 = TestCasesResourcesParser.GetSwitchboardDataByCaseName("TestCaseParser.Case1");
        DataForTestCaseParserCase2 = TestCasesResourcesParser.GetSwitchboardDataByCaseName("TestCaseParser.Case2");
        DataForCaseAlreadyConnected = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathBreadthFirstSearch.Case_already_connected");
        DataForCaseCircularPath = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathBreadthFirstSearch.Case_circular_path");
        DataForCaseNoPossiblePath = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathBreadthFirstSearch.Case_no_possible_path");
    }

    [Benchmark]
    public void FindPathBreadthFirstSearch_TestCaseParserCase1()
    {
        DataForTestCaseParserCase1.Switchboard.FindPathBreadthFirstSearch(
            DataForTestCaseParserCase1.Start, 
            DataForTestCaseParserCase1.Target);
    }

    [Benchmark]
    public void FindPathBreadthFirstSearch_TestCaseParserCase2()
    {
        DataForTestCaseParserCase2.Switchboard.FindPathBreadthFirstSearch(
            DataForTestCaseParserCase2.Start, 
            DataForTestCaseParserCase2.Target);
    }

    [Benchmark]
    public void FindPathBreadthFirstSearch_CaseAlreadyConnected()
    {
        DataForCaseAlreadyConnected.Switchboard.FindPathBreadthFirstSearch(
            DataForCaseAlreadyConnected.Start,
            DataForCaseAlreadyConnected.Target);
    }

    [Benchmark]
    public void FindPathBreadthFirstSearch_CaseCircularPath()
    {
        DataForCaseCircularPath.Switchboard.FindPathBreadthFirstSearch(
            DataForCaseCircularPath.Start,
            DataForCaseCircularPath.Target);
    }

    [Benchmark]
    public void FindPathBreadthFirstSearch_CaseNoPossiblePath()
    {
        DataForCaseNoPossiblePath.Switchboard.FindPathBreadthFirstSearch(
            DataForCaseNoPossiblePath.Start,
            DataForCaseNoPossiblePath.Target);
    }
}