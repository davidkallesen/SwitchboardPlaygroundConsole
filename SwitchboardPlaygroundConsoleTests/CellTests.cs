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
        Assert.Equal(2, cell.CellWeight());
    }

    [Theory]
    [InlineData(2, CellDirection.W, CellDirection.E)]
    [InlineData(2, CellDirection.E, CellDirection.W)]
    [InlineData(2, CellDirection.NW, CellDirection.SE)]
    [InlineData(2, CellDirection.SE, CellDirection.NW)]
    [InlineData(3, CellDirection.NW, CellDirection.E)]
    [InlineData(3, CellDirection.E, CellDirection.NW)]
    [InlineData(4, CellDirection.E, CellDirection.S)]
    [InlineData(4, CellDirection.S, CellDirection.E)]
    [InlineData(4, CellDirection.W, CellDirection.N)]
    [InlineData(4, CellDirection.N, CellDirection.W)]
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