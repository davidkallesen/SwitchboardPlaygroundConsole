namespace SwitchboardCalculator.Benchmark;

[MemoryDiagnoser]
public class BenchmarkTests
{
    private (Switchboard Switchboard, Point Start, Point Target) DataForTestCaseParserCase1;
    private (Switchboard Switchboard, Point Start, Point Target) DataForTestCaseParserCase2;

    private (Switchboard Switchboard, Point Start, Point Target) DataForFindPathBreadthCaseAlreadyConnected;
    private (Switchboard Switchboard, Point Start, Point Target) DataForFindPathBreadthCaseCircularPath;
    private (Switchboard Switchboard, Point Start, Point Target) DataForFindPathBreadthCaseNoPossiblePath;
    private (Switchboard Switchboard, Point Start, Point Target) DataForFindPathBreadthCaseMaze;

    private (Switchboard Switchboard, Point Start, Point Target) DataForFindPathDijkstraCaseAlreadyConnected;
    private (Switchboard Switchboard, Point Start, Point Target) DataForFindPathDijkstraCaseCircularPath;
    private (Switchboard Switchboard, Point Start, Point Target) DataForFindPathDijkstraCaseNoPossiblePath;
    private (Switchboard Switchboard, Point Start, Point Target) DataForFindPathDijkstraCaseMaze;

    [GlobalSetup]
    public void Setup()
    {
        DataForTestCaseParserCase1 = TestCasesResourcesParser.GetSwitchboardDataByCaseName("TestCaseParser.Case1");
        DataForTestCaseParserCase2 = TestCasesResourcesParser.GetSwitchboardDataByCaseName("TestCaseParser.Case2");

        DataForFindPathBreadthCaseAlreadyConnected = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathBreadthFirstSearch.Case_already_connected");
        DataForFindPathBreadthCaseCircularPath = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathBreadthFirstSearch.Case_circular_path");
        DataForFindPathBreadthCaseNoPossiblePath = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathBreadthFirstSearch.Case_no_possible_path");
        DataForFindPathBreadthCaseMaze = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathBreadthFirstSearch.Case_maze");

        DataForFindPathDijkstraCaseAlreadyConnected = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathDijkstra.Case_already_connected");
        DataForFindPathDijkstraCaseCircularPath = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathDijkstra.Case_circular_path");
        DataForFindPathDijkstraCaseNoPossiblePath = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathDijkstra.Case_no_possible_path");
        DataForFindPathDijkstraCaseMaze = TestCasesResourcesParser.GetSwitchboardDataByCaseName("FindPathDijkstra.Case_maze");
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
        DataForFindPathBreadthCaseAlreadyConnected.Switchboard.FindPathBreadthFirstSearch(
            DataForFindPathBreadthCaseAlreadyConnected.Start,
            DataForFindPathBreadthCaseAlreadyConnected.Target);
    }

    [Benchmark]
    public void FindPathBreadthFirstSearch_CaseCircularPath()
    {
        DataForFindPathBreadthCaseCircularPath.Switchboard.FindPathBreadthFirstSearch(
            DataForFindPathBreadthCaseCircularPath.Start,
            DataForFindPathBreadthCaseCircularPath.Target);
    }

    [Benchmark]
    public void FindPathBreadthFirstSearch_CaseMaze()
    {
        DataForFindPathBreadthCaseMaze.Switchboard.FindPathBreadthFirstSearch(
            DataForFindPathBreadthCaseMaze.Start,
            DataForFindPathBreadthCaseMaze.Target);
    }

    [Benchmark]
    public void FindPathBreadthFirstSearch_CaseNoPossiblePath()
    {
        DataForFindPathBreadthCaseNoPossiblePath.Switchboard.FindPathBreadthFirstSearch(
            DataForFindPathBreadthCaseNoPossiblePath.Start,
            DataForFindPathBreadthCaseNoPossiblePath.Target);
    }

    [Benchmark]
    public void FindPathDijkstra_CaseAlreadyConnected()
    {
        DataForFindPathDijkstraCaseAlreadyConnected.Switchboard.FindPathDijkstra(
            DataForFindPathDijkstraCaseAlreadyConnected.Start,
            DataForFindPathDijkstraCaseAlreadyConnected.Target);
    }

    [Benchmark]
    public void FindPathDijkstra_CaseCircularPath()
    {
        DataForFindPathDijkstraCaseCircularPath.Switchboard.FindPathDijkstra(
            DataForFindPathDijkstraCaseCircularPath.Start,
            DataForFindPathDijkstraCaseCircularPath.Target);
    }

    [Benchmark]
    public void FindPathDijkstra_CaseNoPossiblePath()
    {
        DataForFindPathDijkstraCaseNoPossiblePath.Switchboard.FindPathDijkstra(
            DataForFindPathDijkstraCaseNoPossiblePath.Start,
            DataForFindPathDijkstraCaseNoPossiblePath.Target);
    }

    [Benchmark]
    public void FindPathDijkstra_CaseMaze()
    {
        DataForFindPathDijkstraCaseMaze.Switchboard.FindPathDijkstra(
            DataForFindPathDijkstraCaseMaze.Start,
            DataForFindPathDijkstraCaseMaze.Target);
    }
}