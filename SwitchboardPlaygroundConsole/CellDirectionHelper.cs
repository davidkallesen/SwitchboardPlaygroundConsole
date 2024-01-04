namespace SwitchboardPlaygroundConsole;

public static class CellDirectionHelper
{
    public static CellDirection GetCellDirection(char input) 
        => (CellDirection)input - 'A';
}