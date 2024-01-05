namespace SwitchboardCalculator;

public class Switchboard
{
    private readonly ILogger logger;
    private readonly Cell[,] data;

    public Switchboard(
        ILogger logger, 
        int horizontal, 
        int vertical)
    {
        this.logger = logger;
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

    public void SetStartCell(Point point, CellDirection @in, CellDirection @out) 
    {
        var cell = GetCell(point);
        cell.SetInOut(@in, @out);
        cell.Decorator = 'S';
    } 

    public void SetTargetCell(Point point, CellDirection @in, CellDirection @out) 
    {
        var cell = GetCell(point);
        cell.SetInOut(@in, @out);
        cell.Decorator = 'T';
    } 

    public Cell? GetCellNW(Point point)
    {
        var target = new Point(point.X - 1, point.Y - 1);
        return ExistsCellAt(target)
            ? data[target.X, target.Y]
            : null;
    }

    public Cell? GetCellN(Point point)
    {
        var target = point with { Y = point.Y - 1 };
        return ExistsCellAt(target)
            ? data[target.X, target.Y]
            : null;
    }

    public Cell? GetCellNE(Point point)
    {
        var target = new Point(point.X + 1, point.Y - 1);
        return ExistsCellAt(target)
            ? data[target.X, target.Y]
            : null;
    }

    public Cell? GetCellW(Point point)
    {
        var target = point with { X = point.X - 1 };
        return ExistsCellAt(target)
            ? data[target.X, target.Y]
            : null;
    }

    public Cell? GetCellE(Point point)
    {
        var target = point with { X = point.X + 1 };
        return ExistsCellAt(target)
            ? data[target.X, target.Y]
            : null;
    }

    public Cell? GetCellSW(Point point)
    {
        var target = new Point(point.X - 1, point.Y + 1);
        return ExistsCellAt(target)
            ? data[target.X, target.Y]
            : null;
    }

    public Cell? GetCellS(Point point)
    {
        var target = point with { Y = point.Y + 1 };
        return ExistsCellAt(target) 
            ? data[target.X, target.Y]
            : null;
    }

    public Cell? GetCellSE(Point point)
    {
        var target = new Point(point.X + 1, point.Y + 1);
        return ExistsCellAt(target) 
            ? data[target.X, target.Y]
            : null;
    }

    public Cell? GetCellNeighbor(Point point, CellDirection direction)
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
        => point.X >= 0 && 
           point.X < MaxHorizontal && 
           point.Y >= 0 && 
           point.Y < MaxVertical;

    public bool IsConnected(Cell cell1, Cell cell2)
    {
        if (cell1.Occupied || cell1.IsEmpty || cell2.Occupied || cell2.IsEmpty)
        {
            return false;
        }

        var target = GetCellNeighbor(cell1.Location, cell1.Out);
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

    public Cell[] FindPathBreadthFirstSearch(Point start, Point target)
    {
        var queue = new Queue<Cell>();
        var visited = new HashSet<Cell>();
        var path = new Dictionary<Cell, List<Cell>>();
        var startCell = GetCell(start);
        var targetCell = GetCell(target);
        queue.Enqueue(startCell);
        path[startCell] = new List<Cell>(); // Initialize the path for the start cell
        Cell? finalCell = null;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            visited.Add(current);

            logger.LogTrace($"current = {current}");

            if (IsConnected(current, targetCell))
            {
                logger.LogTrace($"found target = {targetCell}");
                finalCell = current;
                break;
            }

            var nextCells = GetCellContinuations(current);
            foreach (var neighbor in nextCells)
            {
                logger.LogTrace($"neighbor = {neighbor}");
                if (visited.Contains(neighbor))
                {
                    logger.LogTrace($"already visited = {neighbor} - skipping");
                    continue;
                }

                queue.Enqueue(neighbor);
                visited.Add(neighbor);

                // Update the path for the neighbor cell by appending the current cell to the existing path
                path[neighbor] = new List<Cell>(path[current]) { current };
            }
        }

        var currentCell = GetCell(target);
        logger.LogTrace($"finalCell = {finalCell}");
        logger.LogTrace($"currentCell = {currentCell}");

        var result = new List<Cell>();
        if (finalCell is null)
        {
            logger.LogWarning("Hmm - no valid path");
        }
        else
        {
            logger.LogTrace("WinnerPath:");
            foreach (var cell in path[finalCell])
            {
                logger.LogTrace($"\tcell = {cell}");
                result.Add(cell);
            }

            result.Add(finalCell);
        }

        return result.ToArray();
    }

    public Cell[] FindPathDijkstra(Point start, Point target)
    {
        // Initialize data structures
        var queue = new PriorityQueue<Cell,int>(); // Create a priority queue to store cells to visit based on their "price" (distance from the start cell)
        var visited = new HashSet<Cell>(); // Keep track of visited cells
        //var path = new Dictionary<Cell, List<Cell>>();
        var pathCost = new Dictionary<Cell, int>(); // Store the shortest distances from the start cell to each cell
        var previous = new Dictionary<Cell, Cell>(); // Store the previous cell in the shortest path

        var startCell = GetCell(start); // Get the start cell object
        var targetCell = GetCell(target); // Get the target cell object

        pathCost[startCell] = 0; // Set the distance from the start cell to itself as 0
        queue.Enqueue(startCell, 0); // Enqueue the start cell with priority 0
        Cell? finalCell = null;
        while (queue.Count > 0) // Continue until the queue is empty
        {
            var current = queue.Dequeue(); // Dequeue the cell with the highest priority (lowest distance)
            logger.LogTrace($"current = {current}");

            visited.Add(current); // Mark the current cell as visited

            if (IsConnected(current, targetCell))
            {
                logger.LogTrace($"found target = {targetCell}");
                finalCell = current;
                previous[targetCell] = current; // Update the previous cell in the shortest path
                break;
            }

            var nextCells = GetCellContinuations(current); // Get the neighboring cells of the current cell
            foreach (var neighbor in nextCells) // Iterate through each neighbor
            {
                var weight = neighbor.Weight; // Get the weight of the neighbor cell
                // Add estimated distance to target (A* improvement of Dijkstra)
                // var remainingDistance = RemainingDistanceEstimate(neighbor.Location, targetCell.Location);
                // var cost = pathCost[current] + weight + remainingDistance; // Calculate the accumulated weight from the start cell to the neighbor cell + estimated distance to target
                var cost = pathCost[current] + weight; // Calculate the accumulated weight from the start cell to the neighbor cell
                if (!pathCost.ContainsKey(neighbor) || cost < pathCost[neighbor])
                {
                    pathCost[neighbor] = cost; // Update the shortest distance to the neighbor cell
                    previous[neighbor] = current; // Update the previous cell in the shortest path

                    if (!visited.Contains(neighbor)) // If the neighbor cell has not been visited yet
                    {
                        queue.Enqueue(neighbor, cost); // Enqueue the neighbor cell with its updated distance as priority
                    }
                }
            }
        }
        //print all cells in previous
        foreach (var (cell, previousCell) in previous)
        {
            logger.LogTrace($"cell = {cell} - previousCell = {previousCell}");
        }

        var result = new List<Cell>();
        if (!previous.ContainsKey(targetCell)) // If there is no path from the start cell to the target cell
        {
            logger.LogWarning("Hmm - no valid path");
            return result.ToArray(); // Return an empty array
        }

        var path = new List<Cell>(); // Create a list to store the cells in the shortest path
        var currentCell = targetCell; // Start from the target cell
        while (currentCell != startCell) // Traverse back from the target cell to the start cell
        {
            logger.LogTrace($"currentCell = {currentCell}");
            path.Insert(0, currentCell); // Insert the current cell at the beginning of the path list
            currentCell = previous[currentCell]; // Move to the previous cell in the shortest path
        }
        //path.Insert(0, startCell); // Insert the start cell at the beginning of the path list

        return path.ToArray(); // Convert the path list to an array and return it
    }
    
    public int RemainingDistanceEstimate(Point start, Point target)
    {

        //calcululate diagonal distance rounded to integer
        return 2*(int)Math.Sqrt(Math.Pow(Math.Abs(start.X - target.X), 2) + Math.Pow(Math.Abs(start.Y - target.Y), 2));
        //TODO: try other distance estimations
        //Maybe use distance from start.neighbor to target.predecessor

        //Manhattan distance
        //return Math.Abs(start.X - target.X) + Math.Abs(start.Y - target.Y);
    }

    public IEnumerable<Cell> GetCellContinuations(Cell cell1)
    {
        if (cell1.Out == CellDirection.Unknown)
        {
            yield break;
        }

        var target = GetCellNeighbor(cell1.Location, cell1.Out);
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

            if (IsUsableContinuation(cell2))
            {
                yield return cell2;
            }
        }
    }

    private bool IsUsableContinuation(Cell cell2)
    {
        var cellNeighbor = GetCellNeighbor(cell2.Location, cell2.Out);
        return cellNeighbor is not null &&
               !cellNeighbor.Occupied;
    }
}