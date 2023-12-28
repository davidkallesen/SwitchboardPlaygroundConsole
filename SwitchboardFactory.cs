using System.Drawing;

namespace SwitchboardPlaygroundConsole;

public static class SwitchboardFactory
{
    public static Cell[,] Create(int hmax, int vmax)
    {
        var switchboard = new Cell[hmax, vmax];
        for (var r = 0; r < vmax; r++)
        {
            for (var c = 0; c < hmax; c++)
            {
                switchboard[c, r] = new Cell
                {
                    Location = new Point(c, r)
                };
            }
        }

        return switchboard;
    }
}