namespace SwitchboardPlaygroundConsole;

public static class SwitchboardHelper
{
    public static void SetOccupied(Cell[,] switchboard, int x, int y)
    {
        switchboard[x, y] = new Cell
        {
            Occupied = true
        };
    }

    public static void SetInOut(Cell[,] switchboard, int x, int y, int i, int o)
    {
        switchboard[x, y] = new Cell
        {
            In = i,
            Out = o,
        };
    }

    public static void Render(Cell[,] switchboard, int hmax, int vmax)
    {
        for (var r = 0; r < vmax; r++)
        {
            for (var z = 0; z < 3; z++)
            {
                for (var c = 0; c < hmax; c++)
                {
                    Console.Write(switchboard[c, r].DisplayCellRow(z));
                    Console.Write("|");
                }

                Console.WriteLine();
            }

            for (var i = 0; i < hmax; i++)
            {
                Console.Write("---+");
            }

            Console.WriteLine();
        }
    }
}