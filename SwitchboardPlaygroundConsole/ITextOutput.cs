namespace SwitchboardPlaygroundConsole;

public interface ITextOutput
{
    void WriteLine(string message);

    void WriteLine();

    void Write(string message);

    void Write(char c);
}
