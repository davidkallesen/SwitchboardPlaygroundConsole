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
                data[c, r] = new Cell
                {
                    Location = new Point(c, r)
                };
            }
        }
    }

    public int MaxHorizontal { get; private set; }

    public int MaxVertical { get; private set; }

    public Cell GetCell(int x, int y) => data[x, y];

    public void SetOccupied(int x, int y)
    {
        data[x, y] = new Cell
        {
            Occupied = true,
            In = -1,
            Out = -1,
        };
    }

    public void SetInOut(int x, int y, int i, int o)
    {
        data[x, y] = new Cell
        {
            Occupied = false,
            In = i,
            Out = o,
        };
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
        var distance = Math.Abs(In - Out);
        if (distance == 4) return 2;
        if (distance == 3) return 3;
        if (distance == 2) return 4;
        if (distance == 1) return -1;
        return -1;
    }

    public Cell GetCellNW(int x, int y)
    {
        return data[x - 1, y - 1];
    }

    public Cell GetCellN(int x, int y)
    {
        return data[x, y - 1];
    }

    public Cell GetCellNE(int x, int y)
    {
        return data[x + 1, y - 1];
    }

    public Cell GetCellW(int x, int y)
    {
        return data[x - 1, y];
    }

    public Cell GetCellE(int x, int y)
    {
        return data[x + 1, y];
    }

    public Cell GetCellSW(int x, int y)
    {
        return data[x - 1, y + 1];
    }

    public Cell GetCellS(int x, int y)
    {
        return data[x, y + 1];
    }

    public Cell GetCellSE(int x, int y)
    {
        return data[x + 1, y + 1];
    }
}