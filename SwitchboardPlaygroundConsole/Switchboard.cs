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

    public int MaxHorizontal { get; }

    public int MaxVertical { get; }

    public Cell GetCell(int x, int y) => data[x, y];

    public Cell GetCell(Point point) => GetCell(point.X, point.Y);
    
    public void SetOccupied(Point point) => GetCell(point).SetOccupied();

    public void SetInOut(Point point, int i, int o) => GetCell(point).SetInOut(i, o);

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

    private bool ExistsCellAt(Point point)
    {
        return point.X >= 0 && point.X < MaxHorizontal && point.Y >= 0 && point.Y < MaxVertical;
    }
    
    public bool IsConnected(Cell cell1, Cell cell2)
    {
        if (cell1.Occupied || cell1.IsEmpty || cell2.Occupied || cell2.IsEmpty)
        {
            return false;
        }

        if (cell1.Out == 0)
        {
            var target = GetCellNW(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == 4)
            {
                return true;
            }
        }

        if (cell1.Out == 1)
        {
            var target = GetCellN(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == 5)
            {
                return true;
            }
        }

        if (cell1.Out == 2)
        {
            var target = GetCellNE(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == 6)
            {
                return true;
            }
        }

        if (cell1.Out == 3)
        {
            var target = GetCellW(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == 7)
            {
                return true;
            }
        }

        if (cell1.Out == 4)
        {
            var target = GetCellE(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == 0)
            {
                return true;
            }
        }

        if (cell1.Out == 5)
        {
            var target = GetCellSW(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == 1)
            {
                return true;
            }
        }

        if (cell1.Out == 6)
        {
            var target = GetCellS(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == 2)
            {
                return true;
            }
        }

        if (cell1.Out == 7)
        {
            var target = GetCellSE(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == 3)
            {
                return true;
            }
        }

        return false;
    }
}