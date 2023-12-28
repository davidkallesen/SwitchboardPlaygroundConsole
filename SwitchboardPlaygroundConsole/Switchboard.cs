namespace SwitchboardPlaygroundConsole;

public class Switchboard
{
    private readonly Cell[,] data;

    public Switchboard(int horizontal, int vertical)
    {
        MaxHorizontal = horizontal;
        MaxVertical = vertical;
        data = new Cell[horizontal, vertical];
        for (var r = 0; r < vertical; r++)
        {
            for (var c = 0; c < horizontal; c++)
            {
                data[c, r] = new Cell
                {
                    Location = new Point(c, r)
                };
            }
        }
    }

    public int MaxHorizontal { get; private set; }

    public int MaxVertical { get; private set; }

    public Cell GetCell(int x, int y) => data[x, y];

    public void SetOccupied(int x, int y)
    {
        data[x, y] = new Cell
        {
            Occupied = true,
            In = -1,
            Out = -1,
        };
    }

    public void SetInOut(int x, int y, int i, int o)
    {
        data[x, y] = new Cell
        {
            Occupied = false,
            In = i,
            Out = o,
        };
    }

    public Cell GetCellNW(int x, int y)
    {
        return data[x - 1, y - 1];
    }

    public Cell GetCellN(int x, int y)
    {
        return data[x, y - 1];
    }

    public Cell GetCellNE(int x, int y)
    {
        return data[x + 1, y - 1];
    }

    public Cell GetCellW(int x, int y)
    {
        return data[x - 1, y];
    }

    public Cell GetCellE(int x, int y)
    {
        return data[x + 1, y];
    }

    public Cell GetCellSW(int x, int y)
    {
        return data[x - 1, y + 1];
    }

    public Cell GetCellS(int x, int y)
    {
        return data[x, y + 1];
    }

    public Cell GetCellSE(int x, int y)
    {
        return data[x + 1, y + 1];
    }
}