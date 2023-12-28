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
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write('+');
        for (var i = 0; i < hmax; i++)
        {
            Console.Write("---+");
        }

        Console.WriteLine();

        for (var r = 0; r < vmax; r++)
        {
            for (var z = 0; z < 3; z++)
            {
                Console.Write('|');
                for (var c = 0; c < hmax; c++)
                {
                    Console.ForegroundColor = switchboard[c, r].Occupied 
                        ? ConsoleColor.Yellow 
                        : ConsoleColor.Red;
                    Console.Write(switchboard[c, r].DisplayCellRow(z));
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write('|');
                }

                Console.WriteLine();
            }

            Console.Write('+');
            for (var i = 0; i < hmax; i++)
            {
                Console.Write("---+");
            }

            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
    }

    public static Cell GetCell(Cell[,] switchboard, int[] coords) {
        return switchboard[coords[0], coords[1]];
    }

    public static Cell N(Cell[,] switchboard, int[] coords) {
        return GetCell(switchboard, new int[] { coords[0], coords[1] - 1 });
    }
    
    public static Cell NE(Cell[,] switchboard, int[] coords) {
        return GetCell(switchboard, new int[] { coords[0] + 1, coords[1] - 1 });
    }
    
}