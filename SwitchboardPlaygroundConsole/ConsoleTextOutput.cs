public class ConsoleTextOutput : ITextOutput
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteLine()
    {
        Console.WriteLine();
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public void Write(char c)
    {
        Console.Write(c);
    }
}
