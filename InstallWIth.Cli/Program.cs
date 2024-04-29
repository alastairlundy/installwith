using System.Reflection;

using AlastairLundy.Extensions.System.AssemblyExtensions;
using AlastairLundy.Extensions.System.VersionExtensions;

using McMaster.Extensions.CommandLineUtils;

namespace InstallWIth.Cli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            CommandLineApplication application = new CommandLineApplication();

            var help = application.HelpOption("-h|--help");
            var version = application.Option("-v|--version", localizations.Resource.Version_Description, CommandOptionType.NoValue);


            application.Command("install", installCommand =>
            {


                installCommand.OnExecute(() =>
                {



                });
            });

            application.Command("remove", removeCommand =>
            {


                removeCommand.OnExecute(() =>
                {




                });
            });

            application.Command("update", updateCommand =>
            {


                updateCommand.OnExecute(() =>
                {



                });
            });

            application.Command("search", searchCommand =>
            {





            });

            application.Command("list", listCommand =>
            {





            });

            application.OnExecute(() =>
            {
                if (help.HasValue())
                {
                    application.ShowHelp();
                }
                if(version.HasValue())
                {
                    string appName = AssemblyGetProgramName.GetProjectName(Assembly.GetExecutingAssembly());
                    string version = AssemblyGetProgramVersion.GetProjectVersion(Assembly.GetExecutingAssembly()).GetFriendlyVersionToString();

                    Console.WriteLine($"{appName} {version}");
                }

            });

           return application.Execute(args);
        }
    }
}
