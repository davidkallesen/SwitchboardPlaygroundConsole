namespace SwitchboardCalculator.Tests;

public class StringHelperAdapter : ITextOutput
{
    private readonly ITestOutputHelper output;
    private readonly StringBuilder sb;

    public StringHelperAdapter(ITestOutputHelper output)
    {
        this.output = output;
        sb = new StringBuilder();
    }

    public void WriteLine(string message)
    {
        output.WriteLine(message);
        sb.AppendLine(message);
    }

    public void WriteLine()
    {
        sb.AppendLine();
    }

    public void Write(string message)
    {
        sb.Append(message);
    }

    public void Write(char c)
    {
        sb.Append(c);
    }

    public string GetOutput()
    {
        return sb.ToString();
    }
}