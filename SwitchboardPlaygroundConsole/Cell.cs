namespace SwitchboardPlaygroundConsole;

public class Cell
{
    public Point Location { get; set; }

    public bool Occupied { get; set; } = false;

    public int In { get; set; } = -1;

    public int Out { get; set; } = -1;

    public int Weight => CellWeight();

    public string DisplayCellRow(int z)
    {
        if (Occupied)
        {
            return "...";
        }

        if (In == -1 && Out == -1)
        {
            return "   ";
        }

        var array = new char[3, 3];
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                array[i, j] = ' ';
            }
        }

        var weight = CellWeight();
        array[1, 1] = (weight >= 0) ? (char)(weight + '0') : '*';

        char[,] template = new char[3, 3] {
            { '\\', '|', '/' },
            { '-', '*', '-' },
            { '/', '|', '\\' }
        };

        int[,] connectionName = new int[3,3] {
            { 0, 1, 2 },
            { 7, -1, 3 },
            { 6, 5, 4 }        };
        //connection points are numbered from 0 to 7, starting at the top left corner and going clockwise.
        //if a connection is in use, we need to draw the appropriate line by taking the character from the template
        //and putting it in the right place in the array
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (connectionName[r, c] == In || connectionName[r, c] == Out)
                {
                    array[r, c] = template[r, c];
                }
            }
        }

        return $"{array[z, 0]}{array[z, 1]}{array[z, 2]}";
    }

    public int CellWeight() {
        // The connection points will be numbered from 0 to 7, starting at the top left corner and going clockwise.
        //Distance between In and Out should be calculated modulo 8
        //Weight should be calculated as follows:
        //If in and out have a distance of 4, then it's a straight line and weight is 2
        //If In and out have a distance of 3, then it's a 135 degree turn and weight is 3
        //If In and out have a distance of 2, then it's a 90 degree turn and weight is 4
        //If In and out have a distance of 1, then return -1
        //If cell is occupied, then return -1
        if (In == -1 || Out == -1) return -1;
        if (In == Out) return -1;
        var distance = Math.Min(Math.Abs(In - Out), 8-Math.Abs(In - Out));
        if (distance == 4) return 2;
        if (distance == 3) return 3;
        if (distance == 2) return 4;
        if (distance == 1) return -1;
        return -1;
    }

    public override string ToString() 
        => $"{nameof(Location)}: {Location}, {nameof(Occupied)}: {Occupied}, {nameof(In)}: {In}, {nameof(Out)}: {Out}, {nameof(Weight)}: {Weight}";
}