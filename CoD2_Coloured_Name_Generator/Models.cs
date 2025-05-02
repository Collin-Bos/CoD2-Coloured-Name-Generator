namespace CoD2_Coloured_Name_Generator;

internal struct NameSection(string name)
{
    internal string Name { get; set; } = name;
    internal ColorCode ColorCode { get; set; }
}

internal readonly struct ColorCode(RGB consoleColor, string codColorIndex, string name)
{
    internal RGB ConsoleColor { get; init; } = consoleColor;
    internal string CodColorIndex { get; init; } = codColorIndex;
    internal string Name { get; init; } = name;
}

internal struct RGB(int red = 0, int green = 0, int blue = 0)
{
    internal int Red { get; set; } = red;
    internal int Green { get; set; } = green;
    internal int Blue { get; set; } = blue;
}