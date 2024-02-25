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

    [Theory]
    [InlineData(0, CellDirection.NW, CellDirection.NE)]
    [InlineData(0, CellDirection.NE, CellDirection.NW)]
    [InlineData(1, CellDirection.N, CellDirection.E)]
    [InlineData(1, CellDirection.E, CellDirection.N)]
    [InlineData(2, CellDirection.NE, CellDirection.SE)]
    [InlineData(2, CellDirection.SE, CellDirection.NE)]
    [InlineData(3, CellDirection.E, CellDirection.S)]
    [InlineData(3, CellDirection.S, CellDirection.E)]
    [InlineData(4, CellDirection.SE, CellDirection.SW)]
    [InlineData(4, CellDirection.SW, CellDirection.SE)]
    [InlineData(5, CellDirection.S, CellDirection.W)]
    [InlineData(5, CellDirection.W, CellDirection.S)]
    [InlineData(6, CellDirection.SW, CellDirection.NW)]
    [InlineData(6, CellDirection.NW, CellDirection.SW)]
    [InlineData(7, CellDirection.W, CellDirection.N)]
    [InlineData(7, CellDirection.N, CellDirection.W)]
    public void NormaliseDirection_ReturnsCorrectDirectionForSharpCorners(int expected, CellDirection @in, CellDirection @out)
    {
        var cell = new Cell(new Point());
        cell.SetInOut(@in, @out);
        Assert.Equal(expected, cell.NormalizeDirection());
    }

    [Theory]
    [InlineData(0, CellDirection.NW, CellDirection.E)]
    [InlineData(0, CellDirection.E, CellDirection.NW)]
    [InlineData(1, CellDirection.N, CellDirection.SE)]
    [InlineData(1, CellDirection.SE, CellDirection.N)]
    [InlineData(2, CellDirection.NE, CellDirection.S)]
    [InlineData(2, CellDirection.S, CellDirection.NE)]
    [InlineData(3, CellDirection.E, CellDirection.SW)]
    [InlineData(3, CellDirection.SW, CellDirection.E)]
    [InlineData(4, CellDirection.SE, CellDirection.W)]
    [InlineData(4, CellDirection.W, CellDirection.SE)]
    [InlineData(5, CellDirection.S, CellDirection.NW)]
    [InlineData(5, CellDirection.NW, CellDirection.S)]
    [InlineData(6, CellDirection.SW, CellDirection.N)]
    [InlineData(6, CellDirection.N, CellDirection.SW)]
    [InlineData(7, CellDirection.W, CellDirection.NE)]
    [InlineData(7, CellDirection.NE, CellDirection.W)]
    public void NormaliseDirection_ReturnsCorrectDirectionForSoftCorners(int expected, CellDirection @in, CellDirection @out)
    {
        var cell = new Cell(new Point());
        cell.SetInOut(@in, @out);
        Assert.Equal(expected, cell.NormalizeDirection());
    }

    [Theory]
    [InlineData(0, CellDirection.NW, CellDirection.SE)]
    [InlineData(1, CellDirection.N, CellDirection.S)]
    [InlineData(2, CellDirection.NE, CellDirection.SW)]
    [InlineData(3, CellDirection.E, CellDirection.W)]
    [InlineData(4, CellDirection.SE, CellDirection.NW)]
    [InlineData(5, CellDirection.S, CellDirection.N)]
    [InlineData(6, CellDirection.SW, CellDirection.NE)]
    [InlineData(7, CellDirection.W, CellDirection.E)]
    public void NormaliseDirection_ReturnsCorrectDirectionForStraightPieces(int expected, CellDirection @in, CellDirection @out)
    {
        var cell = new Cell(new Point());
        cell.SetInOut(@in, @out);
        Assert.Equal(expected, cell.NormalizeDirection());
    }

}