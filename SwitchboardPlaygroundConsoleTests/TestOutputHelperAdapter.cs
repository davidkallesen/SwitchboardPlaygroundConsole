using Xunit.Abstractions;
using Xunit;

public class TestOutputHelperAdapter : ITextOutput
{

    private readonly ITestOutputHelper _outputHelper;

    private String lineBuffer = "";

    public TestOutputHelperAdapter(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    public void Write(string message)
    {
        lineBuffer += message;
    }

    public void Write(char c)
    {
        lineBuffer += c;
    }

    public void WriteLine(string message)
    {
        _outputHelper.WriteLine(lineBuffer + message);
        lineBuffer = "";
    }

    public void WriteLine()
    {
        _outputHelper.WriteLine(lineBuffer);
        lineBuffer = "";
    }
}
