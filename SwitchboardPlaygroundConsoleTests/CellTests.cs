using SwitchboardPlaygroundConsole;

public class CellTests
{
    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenInOrOutIsMinusOne()
    {
        var cell = new Cell(new Point()) { In = -1, Out = 0 };
        Assert.Equal(-1, cell.CellWeight());

        cell = new Cell(new Point()) { In = 0, Out = -1 };
        Assert.Equal(-1, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenInEqualsOut()
    {
        var cell = new Cell(new Point()) { In = 0, Out = 0 };
        Assert.Equal(-1, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsTwo_WhenDistanceIsFour()
    {
        var cell = new Cell(new Point()) { In = 0, Out = 4 };
        Assert.Equal(2, cell.CellWeight());
    }

    [Theory]
    [InlineData(2, 7, 3)]
    [InlineData(2, 3, 7)]
    [InlineData(2, 0, 4)]
    [InlineData(2, 4, 0)]
    [InlineData(3, 0, 3)]
    [InlineData(3, 3, 0)]
    [InlineData(4, 3, 5)]
    [InlineData(4, 5, 3)]
    [InlineData(4, 7, 1)]
    [InlineData(4, 1, 7)]
    public void CellWeight_ReturnsCorrectWeight_WhenDistanceIsLegal(int expected, int @in, int @out)
    {
        var cell = new Cell(new Point()) { In = @in, Out = @out };
        Assert.Equal(expected, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenDistanceIsOne()
    {
        var cell = new Cell(new Point()) { In = 0, Out = 1 };
        Assert.Equal(-1, cell.CellWeight());
    }
}