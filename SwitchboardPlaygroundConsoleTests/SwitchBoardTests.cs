using SwitchboardPlaygroundConsole;

public class SwitchBoardTests
{
    [Fact]
    public void IsConnected_ReturnsFalse_WhenEitherCellIsOccupiedOrEmpty()
    {
        var switchboard = new Switchboard(5, 5);
        switchboard.SetOccupied(new Point(2, 2));
        var cell1 = switchboard.GetCell(new Point(2, 2));
        var cell2 = switchboard.GetCell(new Point(1, 1));
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
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }
    // Add more tests for each condition in the IsConnected method

    [Theory]
    [InlineData(true, 2, 2, CellDirection.SE, CellDirection.NW, 1, 1, CellDirection.SE, CellDirection.NW)]
    [InlineData(true, 2, 2, CellDirection.SE, CellDirection.N, 2, 1, CellDirection.S, CellDirection.NE)]
    [InlineData(false, 2, 2, CellDirection.SE, CellDirection.NW, 1, 1, CellDirection.NW, CellDirection.S)]
    [InlineData(false, 2, 2, CellDirection.SE, CellDirection.NW, 0, 0, CellDirection.NW, CellDirection.SE)]
    public void IsConnected_LegalCases(
        bool expected, 
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
        Assert.Equal(expected, switchboard.IsConnected(cell1, cell2));
    }
}