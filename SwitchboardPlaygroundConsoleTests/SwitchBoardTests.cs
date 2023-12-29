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
        switchboard.SetInOut(p1, 4, 0);
        switchboard.SetInOut(p2, 4, 0);
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
        switchboard.SetInOut(p1, 4, 1);
        switchboard.SetInOut(p2, 5, 2);
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
        switchboard.SetInOut(p1, 4, 0);
        switchboard.SetInOut(p2, 0, 5);
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
        switchboard.SetInOut(p1, 4, 0);
        switchboard.SetInOut(p2, 0, 4);
        var cell1 = switchboard.GetCell(p1);
        var cell2 = switchboard.GetCell(p2);
        Assert.False(switchboard.IsConnected(cell1, cell2));
    }
    // Add more tests for each condition in the IsConnected method

    [Theory]
    [InlineData(true, 2, 2, 4, 0, 1, 1, 4, 0)]
    [InlineData(true, 2, 2, 4, 1, 2, 1, 5, 2)]
    [InlineData(false, 2, 2, 4, 0, 1, 1, 0, 5)]
    [InlineData(false, 2, 2, 4, 0, 0, 0, 0, 4)]
    public void IsConnected_LegalCases(bool expected, int p1X, int p1Y, int p1In, int p1Out, int p2X, int p2Y, int p2In, int p2Out)
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