namespace SwitchboardCalculator.Tests;

public class CellTests
{
    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenInOrOutIsMinusOne()
    {
        var cell = new Cell(new Point());
        cell.SetInOut(CellDirection.Unknown, 0);
        Assert.Equal(-1, cell.CellWeight());

        cell = new Cell(new Point());
        cell.SetInOut(0, CellDirection.Unknown);
        Assert.Equal(-1, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenInEqualsOut()
    {
        var cell = new Cell(new Point());
        cell.SetInOut(0, 0);
        Assert.Equal(-1, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsTwo_WhenDistanceIsFour()
    {
        var cell = new Cell(new Point());
        cell.SetInOut(CellDirection.NW, CellDirection.SE);
        Assert.Equal(Cell.WeightStraight, cell.CellWeight());
    }

    [Theory]
    [InlineData(Cell.WeightStraight, CellDirection.W, CellDirection.E)]
    [InlineData(Cell.WeightStraight, CellDirection.E, CellDirection.W)]
    [InlineData(Cell.WeightStraight, CellDirection.NW, CellDirection.SE)]
    [InlineData(Cell.WeightStraight, CellDirection.SE, CellDirection.NW)]
    [InlineData(Cell.WeightSoft, CellDirection.NW, CellDirection.E)]
    [InlineData(Cell.WeightSoft, CellDirection.E, CellDirection.NW)]
    [InlineData(Cell.WeightHard, CellDirection.E, CellDirection.S)]
    [InlineData(Cell.WeightHard, CellDirection.S, CellDirection.E)]
    [InlineData(Cell.WeightHard, CellDirection.W, CellDirection.N)]
    [InlineData(Cell.WeightHard, CellDirection.N, CellDirection.W)]
    public void CellWeight_ReturnsCorrectWeight_WhenDistanceIsLegal(int expected, CellDirection @in, CellDirection @out)
    {
        var cell = new Cell(new Point());
        cell.SetInOut(@in, @out);
        Assert.Equal(expected, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenDistanceIsOne()
    {
        var cell = new Cell(new Point());
        cell.SetInOut(CellDirection.NW, CellDirection.N);
        Assert.Equal(-1, cell.CellWeight());
    }
}