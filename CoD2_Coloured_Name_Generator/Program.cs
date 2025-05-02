using CoD2_Coloured_Name_Generator;
using CoD2_Coloured_Name_Generator.Commands;

ConsoleColorManager.EnableTrueColor();
var settings = new Settings(ConsoleColorManager.ColorCodes);
var renderer = new ConsoleRenderer(settings);

var commandRegistry = new CommandRegistry();
RegisterCommands(commandRegistry, settings);

var colorizer = new UsernameColorizer(renderer, commandRegistry, settings);

while (true)
{
    ConsoleRenderer.PrepareConsole();
    var userInput = Console.ReadLine();
    userInput = userInput?.Trim();
    colorizer.ProcessInput(userInput);
}

static void RegisterCommands(CommandRegistry commandRegistry, Settings settings)
{
    commandRegistry.RegisterCommand(new HelpCommand());
    commandRegistry.RegisterCommand(new IncludeCommand(settings));
    commandRegistry.RegisterCommand(new ExcludeCommand(settings));
    commandRegistry.RegisterCommand(new ShowSettingsCommand(settings));
    commandRegistry.RegisterCommand(new ColumnAmountCommand(settings));
    commandRegistry.RegisterCommand(new WhitespaceCommand(settings));
    commandRegistry.RegisterCommand(new ColorsCommand());
    commandRegistry.RegisterCommand(new PrinterCommand(settings));
    commandRegistry.RegisterCommand(new ClearCommand());
    commandRegistry.RegisterCommand(new ExitCommand());

    commandRegistry.RegisterCommand(new ListCommandsCommand(commandRegistry)); // This has to be the last command added to the registry
}