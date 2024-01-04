namespace SwitchboardPlaygroundConsoleTests;

public static class TestCasesResourcesParser
{
    public static string[] GetCaseNames()
    {
        var resourceNames = Assembly
            .GetAssembly(typeof(TestCasesResourcesParser))!
            .GetManifestResourceNames();

        return resourceNames
            .Select(
                x => x
                    .Replace("SwitchboardPlaygroundConsoleTests.TestCasesResources.", string.Empty)
                    .Replace(".txt", string.Empty))
            .ToArray();
    }

    public static Switchboard GetSwitchboardByCaseName(
        string caseName)
    {
        var resourceStream = Assembly
            .GetAssembly(typeof(TestCasesResourcesParser))!
            .GetManifestResourceStream($"SwitchboardPlaygroundConsoleTests.TestCasesResources.{caseName}.txt");

        if (resourceStream is null)
        {
            throw new Exception($"Resource '{caseName}' don't exist");
        }

        using var reader = new StreamReader(resourceStream);
        var lines = reader.ReadToEnd().ToLines();
        var switchboard = CreateEmptySwitchboardFromLines(lines);
        PopulateSwitchboardFromLines(lines, switchboard);
        return switchboard;
    }

    private static Switchboard CreateEmptySwitchboardFromLines(
        IEnumerable<string> lines)
    {
        var horizontal = 0;
        var vertical = 0;

        foreach (var line in lines)
        {
            if (line.Length <= 1)
            {
                continue;
            }

            if (horizontal == 0)
            {
                horizontal = line.Length;
            }

            vertical++;
        }

        var sw = new Switchboard(horizontal, vertical);
        return sw;
    }

    private static void PopulateSwitchboardFromLines(
        IEnumerable<string> lines, Switchboard sw)
    {
        var y = 0;
        foreach (var line in lines)
        {
            if (line.Length <= 1)
            {
                continue;
            }

            for (var x = 0; x < line.Length; x++)
            {
                var c = line[x];
                switch (c)
                {
                    case '.':
                        continue;
                    case '#':
                        sw.SetOccupied(new Point(x, y));
                        break;
                    case >= '0' and <= '7':
                    {
                        var cellDirectionOut = CellDirectionHelper.GetCellDirectionByNumber(c);
                        var cellDirectionIn = cellDirectionOut.Opposite();
                        sw.SetInOut(new Point(x, y), cellDirectionIn, cellDirectionOut);
                        break;
                    }
                    case >= 'A' and <= 'H':
                    {
                        var cellDirectionOut = CellDirectionHelper.GetCellDirectionByLetter(c);
                        var cellDirectionIn = cellDirectionOut.Opposite();
                        sw.SetInOut(new Point(x, y), cellDirectionIn, cellDirectionOut);
                        break;
                    }
                }
            }

            y++;
        }
    }
}