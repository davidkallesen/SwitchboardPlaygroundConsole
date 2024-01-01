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

    public void SetInOut(Point point, CellDirection @in, CellDirection @out) => GetCell(point).SetInOut(@in, @out);

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

        if (cell1.Out == CellDirection.NW)
        {
            var target = GetCellNW(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == CellDirection.SE)
            {
                return true;
            }
        }

        if (cell1.Out == CellDirection.N)
        {
            var target = GetCellN(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == CellDirection.S)
            {
                return true;
            }
        }

        if (cell1.Out == CellDirection.NE)
        {
            var target = GetCellNE(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == CellDirection.SW)
            {
                return true;
            }
        }

        if (cell1.Out == CellDirection.E)
        {
            var target = GetCellE(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == CellDirection.W)
            {
                return true;
            }
        }

        if (cell1.Out == CellDirection.SE)
        {
            var target = GetCellSE(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == CellDirection.NW)
            {
                return true;
            }
        }

        if (cell1.Out == CellDirection.S)
        {
            var target = GetCellS(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == CellDirection.N)
            {
                return true;
            }
        }

        if (cell1.Out == CellDirection.SW)
        {
            var target = GetCellSW(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == CellDirection.NE)
            {
                return true;
            }
        }

        if (cell1.Out == CellDirection.W)
        {
            var target = GetCellW(cell1.Location);
            if (target is null)
            {
                return false;
            }
            if (target == cell2 && target.In == CellDirection.E)
            {
                return true;
            }
        }

        return false;
    }
}