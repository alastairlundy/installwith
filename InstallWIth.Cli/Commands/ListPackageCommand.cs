using Spectre.Console.Cli;

namespace InstallWith.Cli.Commands;

public class ListPackageCommand : Command<ListPackageCommand.Settings>
{

    public class Settings : BaseSettings
    {

    }

    public override int Execute(CommandContext context, Settings settings)
    {
        throw new NotImplementedException();
    }
}