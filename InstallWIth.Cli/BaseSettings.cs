using System.ComponentModel;
using Spectre.Console.Cli;

namespace InstallWith.Cli;

public class BaseSettings : CommandSettings
{
    [CommandArgument(0, "<Packages>")]
    public string[]? Packages { get; init; }
    
    [CommandOption("-admin|--admin")]
    [DefaultValue(false)]
    public bool RunAsAdministrator { get; init; }
    
    [CommandOption("-with|--with:<PackageManager>")]
    public string? PackageManagerToUse { get; init; }
}