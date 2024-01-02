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

    public Cell? getCellNeighbor(Point point, CellDirection direction)
    {
        return direction switch
        {
            CellDirection.NW => GetCellNW(point),
            CellDirection.N => GetCellN(point),
            CellDirection.NE => GetCellNE(point),
            CellDirection.W => GetCellW(point),
            CellDirection.E => GetCellE(point),
            CellDirection.SW => GetCellSW(point),
            CellDirection.S => GetCellS(point),
            CellDirection.SE => GetCellSE(point),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
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

        var target = getCellNeighbor(cell1.Location, cell1.Out);
        if (target is null)
        {
            return false;
        }

        return cell1.Out switch
        {
            CellDirection.NW => target == cell2 && target.In == CellDirection.SE,
            CellDirection.N => target == cell2 && target.In == CellDirection.S,
            CellDirection.NE => target == cell2 && target.In == CellDirection.SW,
            CellDirection.E => target == cell2 && target.In == CellDirection.W,
            CellDirection.SE => target == cell2 && target.In == CellDirection.NW,
            CellDirection.S => target == cell2 && target.In == CellDirection.N,
            CellDirection.SW => target == cell2 && target.In == CellDirection.NE,
            CellDirection.W => target == cell2 && target.In == CellDirection.E,
            _ => false
        };
    }
}