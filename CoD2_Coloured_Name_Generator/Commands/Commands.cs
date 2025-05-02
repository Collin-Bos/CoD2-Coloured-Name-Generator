namespace CoD2_Coloured_Name_Generator.Commands;
internal class ListCommandsCommand(CommandRegistry registry) : ICommand
{
    private readonly CommandRegistry _registry = registry;

    public string Name => "commands";
    public string Description => "Displays a list of all available commands and their descriptions.";

    public void Execute(string[] args)
    {
        Console.WriteLine("Available Commands:\n");

        var commands = _registry.GetAllCommands().ToArray();
        for (int i = 0; i < commands.Length; i++)
        {
            if (i % 2 == 0)
                ConsoleColorManager.SetForegroundBaseColor();
            else
                ConsoleColorManager.SetForegroundHighlightColor();
            
            Console.WriteLine($"  {commands[i].Name,-15} {commands[i].Description}");
        }

        Console.WriteLine();
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.WriteLine($"All comamnds have to be prefix with '{CommandRegistry.CommandPrefixChar}' in order to be recognised: e.g /help\n");
    }
}

internal class HelpCommand : ICommand
{
    public string Name => "help";
    public string Description => "Displays general information about how the program works.";

    public void Execute(string[] args)
    {
        Console.WriteLine("=== CoD2 Colored Name Generator Help ===\n");
        Console.WriteLine("This tool generates all possible color combinations for a given username.");
        Console.WriteLine();
        Console.WriteLine("Input Rules:");
        Console.WriteLine(" - Use spaces to define separate parts of the username that should receive different colors.");
        Console.WriteLine("   Example: `Player One` generates color variations for 'Player' and 'One'.");
        Console.WriteLine();
        Console.WriteLine(" - Use underscores `_` to create color variation points without keeping the underscore.");
        Console.WriteLine("   Example: `Player_One` treated like `Player One`, but outputs as `PlayerOne`.");
        Console.WriteLine();
        Console.WriteLine("This allows you to customize how the name is split and styled, while keeping the final output clean.");
        Console.WriteLine();
        Console.WriteLine("Output Format:");
        Console.WriteLine(" - Use the printer command to switch between different output formats.");
        Console.WriteLine($"   - `{CommandRegistry.CommandPrefixChar}printer default`: Shows what your name will look like in-game.");
        Console.WriteLine($"   - `{CommandRegistry.CommandPrefixChar}printer cod2`: Outputs a version you can copy and paste into the CoD2 console and set your name using `/name`.");
        Console.WriteLine($"   - `{CommandRegistry.CommandPrefixChar}printer combined`: Outputs both the default and cod2 result next to eachother.");
        Console.WriteLine();
        Console.WriteLine($"Type `{CommandRegistry.CommandPrefixChar}commands` to view all available commands.\n");
    }
}


internal class ClearCommand : ICommand
{
    public string Name => "clear";

    public string Description => "Clears the console of the currently displayed text";

    public void Execute(string[] args)
    {
        Console.Clear();
    }
}

internal class ExitCommand : ICommand
{
    public string Name => "exit";

    public string Description => "Exits the program";

    public void Execute(string[] args)
    {
        Environment.Exit(0);
    }
}

internal class IncludeCommand(Settings settings) : ICommand
{
    private readonly Settings _settings = settings;

    public string Name => "include";
    public string Description => "Includes a color in the active color set";

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            PrintMessageInvalidUsage();
            return;
        }

        var result = _settings.IncludeColor(args[0]);
        if (result == null)
        {
            PrintMessageInvalidId(args[0]);
            return;
        }

        PrintSuccessMessage(result.Value, args[0]);
    }

    private static void PrintMessageInvalidUsage()
    {
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine($"Invalid input. Usage: {CommandRegistry.CommandPrefixChar}include <colorId>");
        Console.Write($"Type ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write($"{CommandRegistry.CommandPrefixChar}colors");
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine(" to see a list of all avaiable color IDs.");
    }

    private static void PrintMessageInvalidId(string colorId)
    {
        ConsoleColorManager.SetForegroundWarningColor();
        Console.Write($"Color with id '{colorId}' is either already included or is not a valid color Id.\nType ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write($"{CommandRegistry.CommandPrefixChar}colors");
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine(" to see a list of all avaiable color IDs.");

        Console.Write("Type ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write($"{CommandRegistry.CommandPrefixChar}settings");
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine(" to see a list of all currently active colors.");
    }

    private static void PrintSuccessMessage(ColorCode colorCode, string colorId)
    {
        Console.Write($"Color ");
        ConsoleColorManager.SetForegroundColor(colorCode.ConsoleColor);
        Console.Write(colorCode.Name);
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.WriteLine($" with Id '{colorId}' included.");
    }
}

internal class ExcludeCommand(Settings settings) : ICommand
{
    private readonly Settings _settings = settings;

    public string Name => "exclude";
    public string Description => "Excludes a color from the active color set";

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            ConsoleColorManager.SetForegroundWarningColor();
            Console.WriteLine($"Invalid input. Usage: {CommandRegistry.CommandPrefixChar}exclude <colorId>");
            return;
        }

        var removedColor = _settings.ExcludeColor(args[0]);
        if (removedColor == null)
        {
            PrintMessageInvalidId(args[0]);
            return;
        }

        PrintSuccessMessage((ColorCode)removedColor, args[0]);
    }

    private static void PrintMessageInvalidId(string colorId)
    {
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine($"Color with id '{colorId}' is not in the active colors list");
        Console.Write("Type ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write($"{CommandRegistry.CommandPrefixChar}settings");
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine(" to see a list of all currently active colors.");
    }

    private static void PrintSuccessMessage(ColorCode colorCode, string colorId)
    {
        Console.Write($"Color ");
        ConsoleColorManager.SetForegroundColor(colorCode.ConsoleColor);
        Console.Write(colorCode.Name);
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.WriteLine($" with Id '{colorId}' excluded.");
    }
}

internal class WhitespaceCommand(Settings settings) : ICommand
{
    private readonly Settings _settings = settings;

    public string Name => "whitespace";
    public string Description => "Sets the number of spaces between columns";

    public void Execute(string[] args)
    {
        if (args.Length == 0 || !int.TryParse(args[0], out int space))
        {
            ConsoleColorManager.SetForegroundWarningColor();
            Console.WriteLine($"Invalid input. Usage: {CommandRegistry.CommandPrefixChar}whitespace <number>");
            return;
        }

        if (space < 0 || space > 20)
        {
            ConsoleColorManager.SetForegroundWarningColor();
            Console.WriteLine("Input cannot be lower than 0 or higher than 20");
            return;
        }

        _settings.SetWhiteSpace(space);
        Console.WriteLine($"Whitespace set to {space}.");
    }
}

internal class ColumnAmountCommand(Settings settings) : ICommand
{
    private readonly Settings _settings = settings;

    public string Name => "columns";
    public string Description => "Sets the number of columns";

    public void Execute(string[] args)
    {
        if (args.Length == 0 || !int.TryParse(args[0], out int columnAmount))
        {
            ConsoleColorManager.SetForegroundWarningColor();
            Console.WriteLine($"Invalid input. Usage: {CommandRegistry.CommandPrefixChar}columns <number>");
            return;
        }

        if (columnAmount < 1 || columnAmount > 20)
        {
            ConsoleColorManager.SetForegroundWarningColor();
            Console.WriteLine("Input cannot be lower than 1 or higher than 20");
            return;
        }

        _settings.SetColumns(columnAmount);
        Console.WriteLine($"Amount of columns set to {columnAmount}.");
    }
}

internal class ShowSettingsCommand(Settings settings) : ICommand
{
    private readonly Settings _settings = settings;

    public string Name => "settings";
    public string Description => "Displays current values of all settings, including active color set";

    public void Execute(string[] args)
    {
        // Manually add console writes here, when adding new settings
        Console.WriteLine("Current Settings:\n");
        Console.WriteLine("Active Colors:");
        CommandHelper.PrintColorCodes([.. _settings.ActiveColors]);
        
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.WriteLine($"\nColumns: {_settings.AmountOfColumns}");
        Console.WriteLine($"\nWhitespace: {_settings.WhitespaceBetweenColumns}");
        Console.WriteLine($"\nCurrent printer: {_settings.Printer.Name}");
    }
}

internal class ColorsCommand : ICommand
{
    public string Name => "colors";

    public string Description => "Displays a list of all avaiable colors";

    public void Execute(string[] args)
    {
        Console.WriteLine("All avaiable colors");
        CommandHelper.PrintColorCodes(ConsoleColorManager.ColorCodes);
    }
}

internal class CommandHelper
{
    internal static void PrintColorCodes(ColorCode[] colors)
    {
        foreach (var color in colors)
        {
            ConsoleColorManager.SetForegroundColor(new(255, 255, 255));
            Console.Write($"Id: {color.CodColorIndex}  ");
            ConsoleColorManager.SetForegroundColor(color.ConsoleColor);
            Console.WriteLine(color.Name);
        }
    }
}

internal class PrinterCommand(Settings settings) : ICommand
{
    public string Name => "printer";
    public string Description => $"Sets which printer to use. Options: {AvailablePrinters}";
    private static string AvailablePrinters => "default, cod2, combined";

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            ConsoleColorManager.SetForegroundWarningColor();
            Console.WriteLine($"Usage: {CommandRegistry.CommandPrefixChar}printer <printer>");
            Console.WriteLine($"Available printers: {AvailablePrinters}");
            return;
        }

        switch (args[0].ToLower())
        {
            case "default":
                settings.SetPrinter(new DefaultPrinter());
                Console.WriteLine($"Switched to {settings.Printer.Name} printer.");
                break;
            case "cod2":
                settings.SetPrinter(new Cod2Printer());
                Console.WriteLine($"Switched to {settings.Printer.Name} printer.");
                break;
            case "combined":
                settings.SetPrinter(new CombinedPrinter());
                Console.WriteLine($"Switched to {settings.Printer.Name} printer (default + cod2 side by side).");
                break;
            default:
                ConsoleColorManager.SetForegroundWarningColor();
                Console.WriteLine("Invalid printer");
                break;
        }
    }
}