namespace SwitchboardPlaygroundConsole;

public static class CellDirectionHelper
{
    public static CellDirection GetCellDirectionByNumber(char input)
        => (CellDirection)Convert.ToInt32(input) - 48;

    public static CellDirection GetCellDirectionByLetter(char input) 
        => (CellDirection)input - 'A';
}