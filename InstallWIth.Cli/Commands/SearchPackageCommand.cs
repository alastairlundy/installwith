using Spectre.Console.Cli;

namespace InstallWith.Cli.Commands;

public class SearchPackageCommand : Command<SearchPackageCommand.Settings>
{
    public class Settings : BaseSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }
}