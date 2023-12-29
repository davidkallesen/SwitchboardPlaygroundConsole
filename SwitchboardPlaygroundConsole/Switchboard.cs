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
                data[c, r] = new Cell(new Point(c, r));
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

    public Cell? GetCellNW(Point point)
    {
        var target = new Point(point.X - 1, point.Y - 1);
        if (!ExistsCellAt(target))
        {
            return null;
        }
        return data[target.X, target.Y];
    }

    public Cell? GetCellN(Point point)
    {
        var target = new Point(point.X, point.Y - 1);
        if (!ExistsCellAt(target))
        {
            return null;
        }
        return data[target.X, target.Y];
    }

    public Cell? GetCellNE(Point point)
    {
        var target = new Point(point.X + 1, point.Y - 1);
        if (!ExistsCellAt(target))
        {
            return null;
        }
        return data[target.X, target.Y];
    }

    public Cell? GetCellW(Point point)
    {
        var target = new Point(point.X - 1, point.Y);
        if (!ExistsCellAt(target))
        {
            return null;
        }
        return data[target.X, target.Y];
    }

    public Cell? GetCellE(Point point)
    {
        var target = new Point(point.X + 1, point.Y);
        if (!ExistsCellAt(target))
        {
            return null;
        }
        return data[target.X, target.Y];
    }

    public Cell? GetCellSW(Point point)
    {
        var target = new Point(point.X - 1, point.Y + 1);
        if (!ExistsCellAt(target))
        {
            return null;
        }
        return data[target.X, target.Y];
    }

    public Cell? GetCellS(Point point)
    {
        var target = new Point(point.X, point.Y + 1);
        if (!ExistsCellAt(target))
        {
            return null;
        }
        return data[target.X, target.Y];
    }

    public Cell? GetCellSE(Point point)
    {
        var target = new Point(point.X + 1, point.Y + 1);
        if (!ExistsCellAt(target))
        {
            return null;
        }
        return data[target.X, target.Y];
    }
}