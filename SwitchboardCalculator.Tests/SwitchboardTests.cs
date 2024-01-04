namespace SwitchboardCalculator.Tests;

public class SwitchboardTests
{
    private readonly ITextOutput output;

    public SwitchboardTests(ITestOutputHelper output)
    {
        this.output = new TestOutputHelperAdapter(output);
    }

    [Fact]
    public void IsConnected_ReturnsFalse_WhenEitherCellIsOccupiedOrEmpty()
    {
        var switchboard = new Switchboard(5, 5);
        switchboard.SetOccupied(new Point(2, 2));
        var cell1 = switchboard.GetCell(new Point(2, 2));
        var cell2 = switchboard.GetCell(new Point(1, 1));
        ConsoleHelper.Render(switchboard, output);
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void IsConnected_ReturnsTrue_WhenCellsAreConnected()
    {
        var p1 = new Point(2, 2);
        var p2 = new Point(1, 1);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, CellDirection.SE, CellDirection.NW);
        switchboard.SetInOut(p2, CellDirection.SE, CellDirection.NW);
        var cell1 = switchboard.GetCell(p1);
        var cell2 = switchboard.GetCell(p2);
        ConsoleHelper.Render(switchboard, output);
        Assert.True(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void IsConnected_ReturnsTrue_WhenCellsAreConnectedB()
    {
        var p1 = new Point(2, 2);
        var p2 = new Point(2, 1);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, CellDirection.SE, CellDirection.N);
        switchboard.SetInOut(p2, CellDirection.S, CellDirection.NE);
        var cell1 = switchboard.GetCell(p1);
        var cell2 = switchboard.GetCell(p2);
        ConsoleHelper.Render(switchboard, output);
        Assert.True(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void IsConnected_ReturnsFalse_WhenCellsAreNotConnected()
    {
        var p1 = new Point(2, 2);
        var p2 = new Point(1, 1);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, CellDirection.SE, CellDirection.NW);
        switchboard.SetInOut(p2, CellDirection.NW, CellDirection.S);
        var cell1 = switchboard.GetCell(p1);
        var cell2 = switchboard.GetCell(p2);
        ConsoleHelper.Render(switchboard, output);
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void IsConnected_ReturnsFalse_WhenCellsAreNotAdjacent()
    {
        var p1 = new Point(2, 2);
        var p2 = new Point(0, 0);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, CellDirection.SE, CellDirection.NW);
        switchboard.SetInOut(p2, CellDirection.NW, CellDirection.SE);
        var cell1 = switchboard.GetCell(p1);
        var cell2 = switchboard.GetCell(p2);
        ConsoleHelper.Render(switchboard, output);
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }

    [Theory]
    [InlineData(1, 1, CellDirection.SE, CellDirection.NW, 0, 0, CellDirection.SE, CellDirection.NW)]
    [InlineData(1, 1, CellDirection.SE, CellDirection.N, 1, 0, CellDirection.S, CellDirection.NE)]
    [InlineData(1, 1, CellDirection.SE, CellDirection.NE, 2, 0, CellDirection.SW, CellDirection.N)]
    [InlineData(1, 1, CellDirection.SW, CellDirection.E, 2, 1, CellDirection.W, CellDirection.N)]
    [InlineData(1, 1, CellDirection.SW, CellDirection.SE, 2, 2, CellDirection.NW, CellDirection.S)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.S, 1, 2, CellDirection.N, CellDirection.SE)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.SW, 0, 2, CellDirection.NE, CellDirection.SW)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.W, 0, 1, CellDirection.E, CellDirection.SW)]
    [InlineData(1, 1, CellDirection.W, CellDirection.E, 2, 1, CellDirection.W, CellDirection.W)]
    public void IsConnected_ShouldBeConnected (
        int p1X, int p1Y, CellDirection p1In, CellDirection p1Out, 
        int p2X, int p2Y, CellDirection p2In, CellDirection p2Out)
    {
        var p1 = new Point(p1X, p1Y);
        var p2 = new Point(p2X, p2Y);
        var switchboard = new Switchboard(3, 3);
        switchboard.SetInOut(p1, p1In, p1Out);
        switchboard.SetInOut(p2, p2In, p2Out);
        var cell1 = switchboard.GetCell(p1);
        var cell2 = switchboard.GetCell(p2);
        ConsoleHelper.Render(switchboard, output);
        Assert.True(switchboard.IsConnected(cell1, cell2));
    }

    [Theory]
    [InlineData(1, 1, CellDirection.SE, CellDirection.NW, 0, 0, CellDirection.NW, CellDirection.SE)]
    [InlineData(1, 1, CellDirection.SE, CellDirection.N, 1, 0, CellDirection.NE, CellDirection.S)]
    [InlineData(1, 1, CellDirection.SE, CellDirection.NE, 2, 0, CellDirection.N, CellDirection.SW)]
    [InlineData(1, 1, CellDirection.SW, CellDirection.E, 2, 1, CellDirection.N, CellDirection.W)]
    [InlineData(1, 1, CellDirection.SW, CellDirection.SE, 2, 2, CellDirection.S, CellDirection.NW)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.S, 1, 2, CellDirection.SE, CellDirection.N)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.SW, 0, 2, CellDirection.SW, CellDirection.NE)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.W, 0, 1, CellDirection.SW, CellDirection.E)]
    public void IsConnected_ShouldNotBeConnected_WrongTargetIn(
        int p1X, int p1Y, CellDirection p1In, CellDirection p1Out, 
        int p2X, int p2Y, CellDirection p2In, CellDirection p2Out)
    {
        var p1 = new Point(p1X, p1Y);
        var p2 = new Point(p2X, p2Y);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, p1In, p1Out);
        switchboard.SetInOut(p2, p2In, p2Out);
        var cell1 = switchboard.GetCell(p1);
        var cell2 = switchboard.GetCell(p2);
        ConsoleHelper.Render(switchboard, output);
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }

    [Theory]
    [InlineData(1, 1, CellDirection.SE, CellDirection.NW, 1, 0, CellDirection.SE, CellDirection.NW)]
    [InlineData(1, 1, CellDirection.SE, CellDirection.N, 1, 1, CellDirection.S, CellDirection.NE)]
    [InlineData(1, 1, CellDirection.SE, CellDirection.NE, 2, 1, CellDirection.SW, CellDirection.N)]
    [InlineData(1, 1, CellDirection.SW, CellDirection.E, 2, 2, CellDirection.W, CellDirection.N)]
    [InlineData(1, 1, CellDirection.SW, CellDirection.SE, 1, 2, CellDirection.NW, CellDirection.S)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.S, 2, 2, CellDirection.N, CellDirection.SE)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.SW, 1, 2, CellDirection.NE, CellDirection.SW)]
    [InlineData(1, 1, CellDirection.NE, CellDirection.W, 0, 2, CellDirection.E, CellDirection.SW)]

    public void IsConnected_ShouldNotBeConnected_WrongCell(
        int p1X, int p1Y, CellDirection p1In, CellDirection p1Out, 
        int p2X, int p2Y, CellDirection p2In, CellDirection p2Out)
    {
        var p1 = new Point(p1X, p1Y);
        var p2 = new Point(p2X, p2Y);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, p1In, p1Out);
        switchboard.SetInOut(p2, p2In, p2Out);
        var cell1 = switchboard.GetCell(p1);
        var cell2 = switchboard.GetCell(p2);
        ConsoleHelper.Render(switchboard, output);
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void GetCellContinuations_ShouldReturnEmptyList_WhenCellIsOccupied()
    {
        var p1 = new Point(2, 2);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetOccupied(p1);
        var cell = switchboard.GetCell(p1);
        ConsoleHelper.Render(switchboard, output);
        Assert.Empty(switchboard.GetCellContinuations(cell));
    }

    [Fact]
    public void GetCellContinuations_ShouldReturnEmptyList_WhenCellIsEmpty()
    {
        var p1 = new Point(2, 2);
        var switchboard = new Switchboard(5, 5);
        var cell = switchboard.GetCell(p1);
        ConsoleHelper.Render(switchboard, output);
        Assert.Empty(switchboard.GetCellContinuations(cell));
    }

    [Fact]
    public void GetCellContinuations_ShouldReturnEmptyList_WhenCellHasNoLegalNeighbors()
    {
        var p1 = new Point(0, 0);
        var switchboard = new Switchboard(2, 2);
        switchboard.SetInOut(p1, CellDirection.SE, CellDirection.N);
        var cell = switchboard.GetCell(p1);
        ConsoleHelper.Render(switchboard, output);
        Assert.Empty(switchboard.GetCellContinuations(cell));
    }

    [Fact]
    public void GetCellContinuations_ShouldReturnFivePossibleContinuations_WhenCellIsConnectedN()
    {
        var p1 = new Point(2, 2);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, CellDirection.SE, CellDirection.N);
        var cell = switchboard.GetCell(p1);
        ConsoleHelper.Render(switchboard, output);
        Assert.NotNull(cell);
        
        var myCell = (Cell)cell; // Casting cell? to Cell
        var result = switchboard.GetCellContinuations(myCell);
        Console.WriteLine(result);
        Assert.Equal(5, switchboard.GetCellContinuations(myCell).Count());
        foreach (var item in result)
        {
            Console.WriteLine(item);
            Assert.Equal(new Point(2,1), item.Location);
            Assert.Equal(CellDirection.S, item.In);
        }
    }

    [Fact]
    public void GetCellContinuations_ShouldReturnFivePossibleContinuations_WhenCellIsConnectedSE()
    {
        var p1 = new Point(2, 2);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, CellDirection.N, CellDirection.SE);
        var cell = switchboard.GetCell(p1);
        ConsoleHelper.Render(switchboard, output);
        Assert.NotNull(cell);
        
        var myCell = (Cell)cell; // Casting cell? to Cell
        var result = switchboard.GetCellContinuations(myCell);
        Console.WriteLine(result);
        Assert.Equal(5, switchboard.GetCellContinuations(myCell).Count());
        foreach (var item in result)
        {
            Console.WriteLine(item);
            Assert.Equal(new Point(3,3), item.Location);
            Assert.Equal(CellDirection.NW, item.In);
        }
    }

    [Theory]
    [InlineData(2, 2, CellDirection.SE, CellDirection.N, 2, 1, CellDirection.S)]
    [InlineData(2, 2, CellDirection.NW, CellDirection.SW, 1, 3, CellDirection.NE)]
    public void GetCellContinuations_ShouldReturnFivePossibleContinuations_WhenCellIsConnected
        (int startX, int StartY, CellDirection startIn, CellDirection startOut, 
        int endX, int endY, CellDirection endIn)
    {
        var p1 = new Point(startX, StartY);
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(p1, startIn, startOut);
        var cell = switchboard.GetCell(p1);
        ConsoleHelper.Render(switchboard, output);
        Assert.NotNull(cell);

        var myCell = (Cell)cell; // Casting cell? to Cell
        var result = switchboard.GetCellContinuations(myCell);
        Console.WriteLine(result);
        Assert.Equal(5, switchboard.GetCellContinuations(myCell).Count());
        foreach (var item in result)
        {
            Console.WriteLine(item);
            Assert.Equal(new Point(endX, endY), item.Location);
            Assert.Equal(endIn, item.In);
        }
    }
}