namespace SwitchboardCalculator;

public class Cell : IEquatable<Cell>
{
    public const int WeightStraight = 20;
    public const int WeightSoft = 21;
    public const int WeightHard = 80;

    private static readonly char[,] displayAsciiTemplate = new char[3, 3]
    {
        { '\\', '|', '/' },
        { '-', '*', '-' },
        { '/', '|', '\\' }
    };

    private static readonly CellDirection[,] connectionName = new CellDirection[3, 3]
    {
            { CellDirection.NW, CellDirection.N, CellDirection.NE },
            { CellDirection.W, CellDirection.Unknown, CellDirection.E },
            { CellDirection.SW, CellDirection.S, CellDirection.SE }
    };

    private readonly char[,] displayAsciiGrid = new char[3, 3];

    public Cell(Point location)
    {
        Location = location;
        InitAsciiGrid();
    }

    public Point Location { get; set; }

    public bool Occupied { get; set; }

    private char decorator = ' ';

    public char Decorator
    {
        get => decorator;
        set
        {
            if (decorator != value)
            {
                decorator = value;
                UpdateAsciiGrid();
            }
        }
    }

    public bool IsEmpty => !Occupied && In == CellDirection.Unknown && Out == CellDirection.Unknown;

    public CellDirection In { get; private set; } = CellDirection.Unknown;

    public CellDirection Out { get; private set; } = CellDirection.Unknown;

    public int Weight => CellWeight();

    private void InitAsciiGrid()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                displayAsciiGrid[i, j] = ' ';
            }
        }
    }

    private void UpdateAsciiGrid()
    {
        var weight = CellWeight();
        switch (weight)
        {
            case WeightHard:
                weight = 4;
                break;
            case WeightSoft:
                weight = 3;
                break;
            case WeightStraight:
                weight = 2;
                break;
        }

        var weightChar = (weight >= 0) ? (char)(weight + '0') : '*';
        displayAsciiGrid[1, 1] = Decorator == ' ' ? weightChar : Decorator;

        //connection points are numbered from 0 to 7, starting at the top left corner and going clockwise.
        //if a connection is in use, we need to draw the appropriate line by taking the character from the template
        //and putting it in the right place in the array
        for (var r = 0; r < 3; r++)
        {
            for (var c = 0; c < 3; c++)
            {
                if (connectionName[r, c] == In || connectionName[r, c] == Out)
                {
                    displayAsciiGrid[r, c] = displayAsciiTemplate[r, c];
                }
            }
        }
    }

    public string DisplayCellRow(int z)
    {
        if (Occupied)
        {
            return "...";
        }

        if (In == CellDirection.Unknown && Out == CellDirection.Unknown)
        {
            return "   ";
        }

        return $"{displayAsciiGrid[z, 0]}{displayAsciiGrid[z, 1]}{displayAsciiGrid[z, 2]}";
    }

    public int CellWeight()
    {
        // The connection points will be numbered from 0 to 7, starting at the top left corner and going clockwise.
        //Distance between In and Out should be calculated modulo 8
        //Weight should be calculated as follows:
        //If in and out have a distance of 4, then it's a straight line and weight is 2
        //If In and out have a distance of 3, then it's a 135 degree turn and weight is 3
        //If In and out have a distance of 2, then it's a 90 degree turn and weight is 4
        //If In and out have a distance of 1, then return -1
        //If cell is occupied, then return -1
        if (In == CellDirection.Unknown || Out == CellDirection.Unknown)
        {
            return -1;
        }

        if (In == Out)
        {
            return -1;
        }

        var distance = Math.Min(Math.Abs((int)In - (int)Out), 8 - Math.Abs((int)In - (int)Out));
        return distance switch
        {
            4 => WeightStraight,
            3 => WeightSoft,
            2 => WeightHard,
            1 => -1,
            _ => -1
        };
    }

    // NormalizeDirection should return the direction that is first in the clockwise direction from In to Out
    // In other words, if In is 0 and Out is 2, then the result should be 0, because 0 is the first direction
    // and if In is 2 and Out is 0, then the result should be 0 as well.
    // The tricky part is to get this right when we cross the 0/7 boundary, and
    // for all 3 tile types (straight, soft and hard).
    public int NormalizeDirection()
    {
        int inDir = (int)In;
        int outDir = (int)Out;

        int diff = (outDir - inDir + 8) % 8;

        if (diff == 2 || diff == 3 || diff == 4)
        {
            return inDir;
        }
        else // diff is 5, 6 or 7
        {
            return outDir;
        }
    }

    public override string ToString()
    {
        if (!Occupied && In == CellDirection.Unknown && Out == CellDirection.Unknown && Weight == -1)
        {
            return $"{nameof(Location)}: {Location}";
        }

        return $"{nameof(Location)}: {Location}, {nameof(Occupied)}: {Occupied}, {nameof(In)}: {In}, {nameof(Out)}: {Out}, {nameof(Weight)}: {Weight}";
    }

    public void SetOccupied()
    {
        Occupied = true;
        In = CellDirection.Unknown;
        Out = CellDirection.Unknown;
        UpdateAsciiGrid();
    }

    public void SetInOut(CellDirection @in, CellDirection @out)
    {
        Occupied = false;
        In = @in;
        Out = @out;
        UpdateAsciiGrid();
    }

    public bool Equals(Cell? other)
    {
        if (other == null)
            return false;

        return this.Location == other.Location && this.In == other.In && this.Out == other.Out;
    }

    public override int GetHashCode()
    {
        return Location.GetHashCode() ^ In.GetHashCode() ^ Out.GetHashCode();
    }
}