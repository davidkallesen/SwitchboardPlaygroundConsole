namespace SwitchboardPlaygroundConsole;

public static class ConsoleHelper
{
    public static void Render(
        Switchboard switchboard)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write('+');
        for (var i = 0; i < switchboard.MaxHorizontal; i++)
        {
            Console.Write("---+");
        }

        Console.WriteLine();

        for (var r = 0; r < switchboard.MaxVertical; r++)
        {
            for (var z = 0; z < 3; z++)
            {
                Console.Write('|');
                for (var c = 0; c < switchboard.MaxHorizontal; c++)
                {
                    Console.ForegroundColor = switchboard.GetCell(c, r).Occupied 
                        ? ConsoleColor.Yellow 
                        : ConsoleColor.Red;
                    Console.Write(switchboard.GetCell(c, r).DisplayCellRow(z));
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write('|');
                }

                Console.WriteLine();
            }

            Console.Write('+');
            for (var i = 0; i < switchboard.MaxHorizontal; i++)
            {
                Console.Write("---+");
            }

            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
    }
}