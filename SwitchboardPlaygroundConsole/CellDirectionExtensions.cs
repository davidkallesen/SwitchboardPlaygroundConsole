namespace SwitchboardPlaygroundConsole;

public static class CellDirectionExtensions
{
    public static CellDirection Opposite(this CellDirection direction)
    {
        return (CellDirection)(((int)direction + 4) % 8);
    }
}