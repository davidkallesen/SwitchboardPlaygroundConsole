using Xunit;
using SwitchboardPlaygroundConsole;

public class CellTests
{
    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenInOrOutIsMinusOne()
    {
        var cell = new Cell { In = -1, Out = 0 };
        Assert.Equal(-1, cell.CellWeight());

        cell = new Cell { In = 0, Out = -1 };
        Assert.Equal(-1, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenInEqualsOut()
    {
        var cell = new Cell { In = 0, Out = 0 };
        Assert.Equal(-1, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsTwo_WhenDistanceIsFour()
    {
        var cell = new Cell { In = 0, Out = 4 };
        Assert.Equal(2, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsThree_WhenDistanceIsThree()
    {
        var cell = new Cell { In = 0, Out = 3 };
        Assert.Equal(3, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsThree_When0to5()
    {
        var cell = new Cell { In = 0, Out = 5 };
        Assert.Equal(3, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsFour_WhenDistanceIsTwo()
    {
        var cell = new Cell { In = 0, Out = 2 };
        Assert.Equal(4, cell.CellWeight());
    }

    [Fact]
    public void CellWeight_ReturnsMinusOne_WhenDistanceIsOne()
    {
        var cell = new Cell { In = 0, Out = 1 };
        Assert.Equal(-1, cell.CellWeight());
    }
}