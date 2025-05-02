namespace CoD2_Coloured_Name_Generator.Commands;

public interface ICommand
{
    public string Name { get; }
    public string Description { get; }
    public void Execute(string[] args);
}
