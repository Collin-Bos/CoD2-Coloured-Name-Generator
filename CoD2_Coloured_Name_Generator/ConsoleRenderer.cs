using CoD2_Coloured_Name_Generator.Commands;
using System.Text;

namespace CoD2_Coloured_Name_Generator;

internal class ConsoleRenderer(Settings settings)
{
    private readonly Settings _settings = settings;
    private int _currentLine = 0;
    private int _totalLines = 0;

    internal static void PrepareConsole()
    {
        ConsoleColorManager.SetBackgroundColor(new(40, 40, 40));
        ConsoleColorManager.SetForegroundBaseColor();
        Console.Write("Write ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write($"{CommandRegistry.CommandPrefixChar}help");
        ConsoleColorManager.SetForegroundBaseColor();
        Console.WriteLine(" to learn how to use this tool\n");
        Console.Write("Write ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write($"{CommandRegistry.CommandPrefixChar}commands");
        ConsoleColorManager.SetForegroundBaseColor();
        Console.WriteLine(" to see all available commands\n");
        Console.WriteLine("Write your username to generate all possible color combinations\n");
        ConsoleColorManager.SetForegroundHighlightColor();
    }

    internal void PrintColorCombinations(List<NameSection[]> combinations, int amountOfColors)
    {
        int totalCombinations = combinations.Count;
        int rowsPerColumn = amountOfColors;
        int totalColumns = (int)Math.Ceiling((double)totalCombinations / rowsPerColumn);
        int totalGroups = (int)Math.Ceiling((double)totalColumns / _settings.AmountOfColumns);
        _currentLine = 0;
        _totalLines = rowsPerColumn * totalColumns;

        for (int groupIndex = 0; groupIndex < totalGroups; groupIndex++)
        {
            PrintGroup(combinations, groupIndex, rowsPerColumn, totalCombinations);
            Console.WriteLine();
        }
    }

    private void PrintGroup(List<NameSection[]> combinations, int groupIndex, int rowsPerColumn, int total)
    {
        for (int row = 0; row < rowsPerColumn; row++)
        {
            PrintLineCount();

            for (int colOffset = 0; colOffset < _settings.AmountOfColumns; colOffset++)
            {
                int columnIndex = groupIndex * _settings.AmountOfColumns + colOffset;
                int index = row + columnIndex * rowsPerColumn;

                if (index >= total)
                    continue;

                PrintSingleCombination(combinations[index]);
                PrintSpaceBetweenColumn(_settings.WhitespaceBetweenColumns, combinations[index]);
            }
            Console.WriteLine();
        }
    }

    private void PrintLineCount()
    {
        _currentLine++;
        ConsoleColorManager.SetForegroundColor(new(125, 125, 125));
        string whitespaceAtFront = string.Empty;

        if (_currentLine < 10 && _totalLines >= 10)
        {
            whitespaceAtFront = whitespaceAtFront.Insert(0, " ");
        }

        if (_currentLine < 100 && _totalLines >= 100)
        {
            whitespaceAtFront = whitespaceAtFront.Insert(0, " ");
        }

        if (_currentLine < 1000 && _totalLines >= 1000)
        {
            whitespaceAtFront = whitespaceAtFront.Insert(0, " ");
        }

        if (_currentLine < 10000 && _totalLines >= 10000)
        {
            whitespaceAtFront = whitespaceAtFront.Insert(0, " ");
        }

        Console.Write(whitespaceAtFront + _currentLine + new string(' ', 5));
    }

    private void PrintSingleCombination(NameSection[] combination) 
        => _settings.Printer.Print(combination);

    private static void PrintSpaceBetweenColumn(int totalSpaces, NameSection[] combination)
    {
        for (int i = 0; i < totalSpaces; i++)
        {
            var last = combination.Last().ColorCode.CodColorIndex;
            if (i == 0 && last.Equals(ConsoleColorManager.BlackColorIndex))
            {
                continue;
            }

            Console.Write(" ");
        }
    }

    internal static void PrintTotalCombinations(int total)
    {
        ConsoleColorManager.SetForegroundColor(new(120, 120, 120));
        Console.Write("Generated a total of ");
        ConsoleColorManager.SetForegroundColor(new(255, 0, 0));
        Console.Write(total);
        ConsoleColorManager.SetForegroundColor(new(120, 120, 120));
        Console.WriteLine(" possible color combinations\n");
    }
}