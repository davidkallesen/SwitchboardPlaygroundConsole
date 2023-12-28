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
        data[x, y].Occupied = true;
        data[x, y].In = -1;
        data[x, y].Out = -1;
    }

    public void SetInOut(int x, int y, int i, int o)
    {
        data[x, y].Occupied = false;
        data[x, y].In = i;
        data[x, y].Out = o;
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