using System.Text;

namespace CoD2_Coloured_Name_Generator;

internal interface ICombinationPrinter
{
    string Name { get; }
    void Print(NameSection[] combination);
}

internal class DefaultPrinter : ICombinationPrinter
{
    public string Name { get; } = "Default";

    public void Print(NameSection[] combination)
    {
        for (int i = 0; i < combination.Length; i++)
        {
            ConsoleColorManager.SetForegroundColor(combination[i].ColorCode.ConsoleColor);
            Console.Write(combination[i].Name);

            if (i + 1 < combination.Length)
            {
                PrintCaretCharacterIfNextColorBlack(combination[i + 1]);
            }
        }
    }

    private static void PrintCaretCharacterIfNextColorBlack(NameSection section)
    {
        var nextColorIndex = section.ColorCode.CodColorIndex;
        if (nextColorIndex.Equals(ConsoleColorManager.BlackColorIndex))
        {
            Console.Write('^');
        }
    }
}

internal class Cod2Printer : ICombinationPrinter
{
    public string Name { get; } = "Cod2";

    public void Print(NameSection[] combination)
    {
        StringBuilder sb = new();
        for (int i = 0; i < combination.Length; i++)
        {
            sb.Clear();
            sb.Append('^');
            AddCaretCharacterIfColorBlack(sb, combination[i]);
            sb.Append(combination[i].ColorCode.CodColorIndex);
            sb.Append(combination[i].Name);

            ConsoleColorManager.SetForegroundColor(combination[i].ColorCode.ConsoleColor);
            Console.Write(sb);
        }
    }

    private static void AddCaretCharacterIfColorBlack(StringBuilder builder, NameSection section)
    {
        if (section.ColorCode.CodColorIndex.Equals(ConsoleColorManager.BlackColorIndex))
        {
            builder.Append('^');
        }
    }
}

internal class CombinedPrinter : ICombinationPrinter
{
    public string Name { get; } = "Combined";

    private readonly DefaultPrinter _defaultPrinter = new();
    private readonly Cod2Printer _cod2Printer = new();

    public void Print(NameSection[] combination)
    {
        _defaultPrinter.Print(combination);

        var blackCount = combination.Count(c => c.ColorCode.CodColorIndex.Equals(ConsoleColorManager.BlackColorIndex));
        Console.Write(new string(' ', combination.Length + 3 - blackCount));

        _cod2Printer.Print(combination);
    }
}