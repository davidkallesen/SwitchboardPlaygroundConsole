namespace SwitchboardCalculator.Tests.TestCasesResources;

public static class TestCasesResourcesParser
{
    private static readonly string ResourceRootPath = "SwitchboardCalculator.Tests.TestCasesResources.";

    public static string[] GetCaseNames()
    {
        var resourceNames = Assembly
            .GetAssembly(typeof(TestCasesResourcesParser))!
            .GetManifestResourceNames();

        return resourceNames
            .Where(x => x.EndsWith(".txt", StringComparison.Ordinal) &&
                        x.StartsWith(ResourceRootPath, StringComparison.Ordinal))
            .Select(x => x
                .Replace(ResourceRootPath, string.Empty, StringComparison.Ordinal)
                .Replace(".txt", string.Empty, StringComparison.Ordinal))
            .ToArray();
    }

    public static (Switchboard Switchboard, Point Start, Point Target) GetSwitchboardDataByCaseName(
        string caseName)
    {
        var resourceStream = Assembly
            .GetAssembly(typeof(TestCasesResourcesParser))!
            .GetManifestResourceStream($"{ResourceRootPath}{caseName}.txt");

        if (resourceStream is null)
        {
            throw new Exception($"Resource '{caseName}' don't exist");
        }

        using var reader = new StreamReader(resourceStream);
        var lines = reader.ReadToEnd().ToLines();

        var switchboard = CreateEmptySwitchboardFromLines(lines);
        return PopulateSwitchboardFromLines(lines, switchboard);
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

    private static (Switchboard Switchboard, Point Start, Point Target) PopulateSwitchboardFromLines(
        IEnumerable<string> lines, Switchboard sw)
    {
        var startPoint = Point.Empty;
        var targetPoint = Point.Empty;
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
                            startPoint = new Point(x, y);
                            sw.SetStartCell(startPoint, cellDirectionIn, cellDirectionOut);
                            break;
                        }
                    case >= 'A' and <= 'H':
                        {
                            var cellDirectionIn = CellDirectionHelper.GetCellDirectionByLetter(c);
                            var cellDirectionOut = cellDirectionIn.Opposite();
                            targetPoint = new Point(x, y);
                            sw.SetTargetCell(targetPoint, cellDirectionIn, cellDirectionOut);
                            break;
                        }
                }
            }

            y++;
        }

        return (sw, startPoint, targetPoint);
    }
}