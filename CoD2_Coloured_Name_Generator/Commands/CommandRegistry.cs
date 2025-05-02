namespace CoD2_Coloured_Name_Generator.Commands;

internal class CommandRegistry
{
    public static char CommandPrefixChar { get; } = '/';

    private readonly Dictionary<string, ICommand> _commands = new(StringComparer.OrdinalIgnoreCase);

    internal void RegisterCommand(ICommand command)
    {
        _commands[command.Name] = command;
    }

    internal bool TryExecute(string input)
    {
        if (input.StartsWith(CommandPrefixChar))
        {
            input = input[1..];           
        }

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return false;

        var commandName = parts[0];
        var args = parts.Skip(1).ToArray();

        if (_commands.TryGetValue(commandName, out var command))
        {
            command.Execute(args);
            return true;
        }

        return false;
    }

    internal IEnumerable<ICommand> GetAllCommands() => _commands.Values;
}
