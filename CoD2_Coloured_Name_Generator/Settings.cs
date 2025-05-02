namespace CoD2_Coloured_Name_Generator;

internal class Settings
{
    // When adding a new setting make sure to also add it to the 'ShowSettingsCommand'
    internal List<ColorCode> ActiveColors { get; } = [];

    internal int AmountOfColumns { get; private set; } = 3;

    internal int WhitespaceBetweenColumns { get; private set; } = 5;

    internal ICombinationPrinter Printer { get; private set; }

    internal Settings(IEnumerable<ColorCode> defaultColors)
    {
        ActiveColors.AddRange(defaultColors);
        Printer = new DefaultPrinter();
    }

    internal ColorCode? IncludeColor(string colorId)
    {
        if (!ActiveColors.Any(c => c.CodColorIndex == colorId))
        {
            var color = ConsoleColorManager.ColorCodes.FirstOrDefault(c => c.CodColorIndex == colorId);
            if (color.CodColorIndex == null)
            {
                return null;
            }

            ActiveColors.Add(color);
            ActiveColors.Sort((x, y) => string.Compare(x.CodColorIndex, y.CodColorIndex, StringComparison.Ordinal));

            return color;
        }

        return null;
    }

    internal ColorCode? ExcludeColor(string colorId)
    {
        var toBeRemoved = ActiveColors.Find(c => c.CodColorIndex.Equals(colorId));
        if (toBeRemoved.CodColorIndex == null)
        {
            return null;
        }

        ActiveColors.Remove(toBeRemoved);
        return toBeRemoved;
    }

    internal void SetColumns(int columnAmount)
    {
        AmountOfColumns = columnAmount;
    }

    internal void SetWhiteSpace(int whitespaceAmount)
    {
        WhitespaceBetweenColumns = whitespaceAmount;
    }

    internal void SetPrinter(ICombinationPrinter printer)
    {
        Printer = printer;
    }
}