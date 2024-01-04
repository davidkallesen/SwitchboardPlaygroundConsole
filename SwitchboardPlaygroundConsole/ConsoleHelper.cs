namespace SwitchboardPlaygroundConsole;

public static class ConsoleHelper
{
    public static void Render(Switchboard switchboard, ITextOutput output)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        output.Write('+');
        for (var i = 0; i < switchboard.MaxHorizontal; i++)
        {
            output.Write("---+");
        }

        output.WriteLine();

        for (var r = 0; r < switchboard.MaxVertical; r++)
        {
            for (var z = 0; z < 3; z++)
            {
                output.Write('|');
                for (var c = 0; c < switchboard.MaxHorizontal; c++)
                {
                    var cell = switchboard.GetCell(c, r);
                    Console.ForegroundColor = cell.Occupied
                        ? ConsoleColor.Yellow
                        : ConsoleColor.Red;
                    output.Write(cell.DisplayCellRow(z));
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    output.Write('|');
                }

                output.WriteLine();
            }

            output.Write('+');
            for (var i = 0; i < switchboard.MaxHorizontal; i++)
            {
                output.Write("---+");
            }

            output.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
    }
    public static void Render(Switchboard switchboard)
    {
        var consoleOutput = new ConsoleTextOutput();
        Render(switchboard, consoleOutput);
    }
}