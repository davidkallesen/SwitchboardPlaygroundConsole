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

    public Cell[] findPathBFS(Point start, Point target)
    {
        var queue = new Queue<Cell>();
        var visited = new HashSet<Cell>();
        var path = new Dictionary<Cell, List<Cell>>();
        var startCell = GetCell(start);
        var targetCell = GetCell(target);
        queue.Enqueue(startCell);
        visited.Add(startCell);
        path[startCell] = new List<Cell>(); // Initialize the path for the start cell
        Cell? finalCell = null;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Console.WriteLine($"current = {current}");
            if (IsConnected(current, targetCell))
            {
                Console.WriteLine($"found target = {targetCell}");
                finalCell = current;
                break;
            }

            var nextCells = GetCellContinuations(current);
            foreach (var neighbor in nextCells)
            {
                Console.WriteLine($"neighbor = {neighbor}");
                if (visited.Contains(neighbor))
                {
                    Console.WriteLine($"already visited = {neighbor} - skipping");
                    continue;
                }

                queue.Enqueue(neighbor);
                visited.Add(neighbor);
                // Update the path for the neighbor cell by appending the current cell to the existing path
                path[neighbor] = new List<Cell>(path[current]) { current };
            }
            Console.WriteLine($"queue.Count = {queue.Count}");
            //print out path
            // foreach (var cell in path)
            // {
            //     Console.WriteLine($"path = {cell}");
            // }
        }
        Console.WriteLine("done?");
        Console.WriteLine($"finalCell = {finalCell}");
        var result = new List<Cell>();
        var currentCell = GetCell(target);
        Console.WriteLine($"currentCell = {currentCell}");
        var winnerpath = path[finalCell];
        Console.WriteLine($"winnerpath = {winnerpath}");
        foreach (var cell in winnerpath)
        {
            Console.WriteLine($"cell = {cell}");
            result.Add(cell);
        }
        // while (currentCell != startCell)
        // {
        //     result.Add(currentCell);
        //     currentCell = path[currentCell];
        // }

        // result.Reverse();
        // Console.WriteLine(result);
        return result.ToArray();
    }



    public IEnumerable<Cell> GetCellContinuations(Cell cell1)
    {
        if (cell1.Out == CellDirection.Unknown)
        {
            yield break;
        }

        var target = getCellNeighbor(cell1.Location, cell1.Out);
        if (target is null)
        {
            yield break;
        }
        // Since neighbor is now a legal location, we need to create the 5 legal track pieces for that location.
        // We then yield return each of those 5 legal pieces.
        // We priortise the straight piece first, then the 135 degree turn, then the 90 degree turn.
        var offsetOptions = new[] { 4, 3, 5, 2, 6 };
        foreach (var offset in offsetOptions)
        {
            var inDirection = cell1.Out.Opposite();
            var outDirection = (CellDirection)(((int)inDirection + offset) % 8);
            var cell2 = new Cell(target.Location);
            cell2.SetInOut(inDirection, outDirection);

            yield return cell2;
        }
        yield break;
    }
}