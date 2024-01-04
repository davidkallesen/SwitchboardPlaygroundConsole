namespace SwitchboardPlaygroundConsoleTests;

public class TestOutputHelperAdapter : ITextOutput
{
    private readonly ITestOutputHelper _outputHelper;

    private string lineBuffer = "";

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

    public string GetOutput()
    {
        throw new NotImplementedException();
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
