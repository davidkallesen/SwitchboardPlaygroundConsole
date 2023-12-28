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

        var w = CellWeight();
        if (w >= 0)
        {
            array[1, 1] = (char)w;
        }
        else
        {
            array[1, 1] = '*';
        }

        if (In == 0 || Out == 0)
        {
            array[0, 0] = '\\';
        }
        
        if (In == 1 || Out == 1)
        {
            array[0, 1] = '|';
        }
        
        if (In == 2 || Out == 2)
        {
            array[0, 2] = '/';
        }
        
        if (In == 3 || Out == 3)
        {
            array[1, 2] = '-';
        }
        
        if (In == 4 || Out == 4)
        {
            array[2, 2] = '\\';
        }
        
        if (In == 5 || Out == 5)
        {
            array[2, 1] = '|';
        }
        
        if (In == 6 || Out == 6)
        {
            array[2, 0] = '/';
        }
        
        if (In == 7 || Out == 7)
        {
            array[1, 0] = '-';
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

}