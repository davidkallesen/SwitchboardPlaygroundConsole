using SwitchboardPlaygroundConsole;

public class SwitchBoardTests
{
    [Fact]
    public void IsConnected_ReturnsFalse_WhenEitherCellIsOccupiedOrEmpty()
    {
        var switchboard = new Switchboard(5, 5);
        switchboard.SetOccupied(2, 2);
        var cell1 = switchboard.GetCell(new Point(2,2));
        var cell2 = switchboard.GetCell(new Point(1,1));
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void IsConnected_ReturnsTrue_WhenCellsAreConnected()
    {
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(2, 2, 4, 0);
        switchboard.SetInOut(1, 1, 4, 0);
        var cell1 = switchboard.GetCell(new Point(2,2));
        var cell2 = switchboard.GetCell(new Point(1,1));
        Assert.True(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void IsConnected_ReturnsTrue_WhenCellsAreConnectedB()
    {
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(2, 2, 4, 1);
        switchboard.SetInOut(2, 1, 5, 2);
        var cell1 = switchboard.GetCell(new Point(2,2));
        var cell2 = switchboard.GetCell(new Point(2,1));
        Assert.True(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void IsConnected_ReturnsFalse_WhenCellsAreNotConnected()
    {
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(2, 2, 4, 0);
        switchboard.SetInOut(1, 1, 0, 5);
        var cell1 = switchboard.GetCell(new Point(2,2));
        var cell2 = switchboard.GetCell(new Point(1,1));
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }

    [Fact]
    public void IsConnected_ReturnsFalse_WhenCellsAreNotAdjacent()
    {
        var switchboard = new Switchboard(5, 5);
        switchboard.SetInOut(2, 2, 4, 0);
        switchboard.SetInOut(0, 0, 0, 4);
        var cell1 = switchboard.GetCell(new Point(2,2));
        var cell2 = switchboard.GetCell(new Point(0,0));
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }
    // Add more tests for each condition in the IsConnected method
}