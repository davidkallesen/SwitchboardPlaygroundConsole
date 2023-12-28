namespace SwitchboardPlaygroundConsole;

public class Cell
{
    public Point Location { get; set; }
    public bool Occupied { get; set; } = false;

    public int In { get; set; } = -1;

    public int Out { get; set; } = -1;

    public int Weight { get; set; } = 0;

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

        array[1, 1] = '*';
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
}