using Spectre.Console.Cli;

namespace InstallWith.Cli.Commands;

public class ListPackageUpdatesCommand : Command<ListPackageUpdatesCommand.Settings>
{
    public class Settings : BaseSettings
    {
        
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }
}