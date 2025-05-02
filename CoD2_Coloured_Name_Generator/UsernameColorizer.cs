using CoD2_Coloured_Name_Generator.Commands;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoD2_Coloured_Name_Generator;

internal class UsernameColorizer(ConsoleRenderer renderer, CommandRegistry commandRegistry, Settings settings)
{
    private readonly ConsoleRenderer _renderer = renderer;
    private readonly CommandRegistry _commandRegistry = commandRegistry;
    private readonly Settings _settings = settings;
    private const int WarnCombinationThreshold = 10000;

    internal void ProcessInput(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid username, please try again.\n");
            return;
        }

        Console.WriteLine();

        if (ExecuteIfCommand(input)) return;

        var sections = ParseInput(input, out int totalNameParts);
        var totalCombinationsToBeGenerated = (int)Math.Pow(_settings.ActiveColors.Count, totalNameParts);
        if (!WarnOfLargeCombinationsCount(totalCombinationsToBeGenerated))
        {
            return;
        }

        var combinations = GenerateCombinations(sections);
        _renderer.PrintColorCombinations(combinations, _settings.ActiveColors.Count);
        ConsoleRenderer.PrintTotalCombinations(totalCombinationsToBeGenerated);
    }

    private bool ExecuteIfCommand(string input)
    {
        if (input.StartsWith(CommandRegistry.CommandPrefixChar))
        {
            var isRecognisedCommand = _commandRegistry.TryExecute(input);

            if (!isRecognisedCommand)
            {
                PrinteUnrecognisedCommandMessage();
            }

            Console.WriteLine();
            return true;
        }

        return false;
    }

    private static void PrinteUnrecognisedCommandMessage()
    {
        ConsoleColorManager.SetForegroundWarningColor();
        Console.Write("Your input command was not recognised. \nType ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write($"{CommandRegistry.CommandPrefixChar}commands");
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine(" to see a list of all avaiable commands.");
    }

    private static NameSection[] ParseInput(string input, out int totalNameParts)
    {
        var tokens = SplitStringRetainingSpaces(input);
        var flattenedParts = FlattenUnderscoreParts(tokens);

        totalNameParts = flattenedParts.Count;

        return flattenedParts.Select(p => new NameSection(p)).ToArray();
    }

    private static string[] SplitStringRetainingSpaces(string input) =>
        Regex.Split(input, @"(?<=[ ])");

    private static List<string> FlattenUnderscoreParts(IEnumerable<string> parts)
    {
        var result = new List<string>();

        foreach (var part in parts)
        {
            if (part.Contains('_'))
                result.AddRange(part.Split('_', StringSplitOptions.RemoveEmptyEntries));
            else
                result.Add(part);
        }

        return result;
    }

    private static bool WarnOfLargeCombinationsCount(int totalCombinations)
    {
        if (totalCombinations < WarnCombinationThreshold)
        {
            return true;
        }

        PrintCombinationsWarning(totalCombinations);
        return PromptUserForConfirmation();
    }

    private static void PrintCombinationsWarning(int totalCombinations)
    {
        ConsoleColorManager.SetForegroundWarningColor();
        Console.Write("You are about to generate a total of ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write(totalCombinations);
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine(" combinations.");
        Console.WriteLine("This amount is large enough that it will take a bit of time to generate." +
            "\nThe result might not fit in the consoles max line count.");
        Console.Write("If the result does not fit in the consoles max line count you can increase the column count with the ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write($"{CommandRegistry.CommandPrefixChar}columns");
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine(" command and try again.");
    }

    private static bool PromptUserForConfirmation()
    {
        PrintPropmtWarning();
        return GetUserResponse();
    }

    private static void PrintPropmtWarning()
    {
        Console.WriteLine();
        ConsoleColorManager.SetForegroundWarningColor();
        Console.Write("Are you sure you want to proceed?\nType ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write("yes");
        ConsoleColorManager.SetForegroundWarningColor();
        Console.Write(" to continue, type ");
        ConsoleColorManager.SetForegroundHighlightColor();
        Console.Write("no");
        ConsoleColorManager.SetForegroundWarningColor();
        Console.WriteLine(" to stop.");
    }

    private static bool GetUserResponse()
    {
        while (true)
        {
            ConsoleColorManager.SetForegroundHighlightColor();
            var userInput = Console.ReadLine();
            ConsoleColorManager.SetForegroundWarningColor();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Invalid input. Only 'yes' 'y' 'no' or 'n' is accepted");
                continue;
            }

            string normalizedInput = userInput.Trim().ToLower();

            if (normalizedInput.Equals("yes") || normalizedInput.Equals("y"))
            {
                return true;
            }

            if (normalizedInput.Equals("no") || normalizedInput.Equals("n"))
            {
                return false;
            }

            Console.WriteLine("Invalid input. Only 'yes' 'y' 'no' or 'n' is accepted");
            continue;
        }
    }

    private List<NameSection[]> GenerateCombinations(NameSection[] baseSections)
    {
        List<NameSection[]> result = [];
        GenerateRecursive(baseSections, 0, result);
        return result;
    }

    private void GenerateRecursive(NameSection[] current, int position, List<NameSection[]> result)
    {
        if (position == current.Length)
        {
            var snapshot = current
                .Select(section => new NameSection(section.Name) { ColorCode = section.ColorCode })
                .ToArray();
            result.Add(snapshot);
            return;
        }

        foreach (var colorCode in _settings.ActiveColors)
        {
            current[position].ColorCode = colorCode;
            GenerateRecursive(current, position + 1, result);
        }
    }
}