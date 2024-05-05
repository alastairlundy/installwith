/*
   Copyright 2024 Alastair Lundy

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License. 
 */

using System.Reflection;

using AlastairLundy.Extensions.System.AssemblyExtensions;
using AlastairLundy.Extensions.System.VersionExtensions;

using InstallWith.Cli.localizations;

using McMaster.Extensions.CommandLineUtils;

namespace InstallWith.Cli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            CommandLineApplication application = new CommandLineApplication();

            application.Name = Resource.App_Name;
            application.Description = Resource.App_Description;
            
            var help = application.HelpOption("-h|--help");
            var version = application.Option("-v|--version", localizations.Resource.Command_Version_Description, CommandOptionType.NoValue);
            var license = application.Option("-l|--license", Resource.Command_License_Description, CommandOptionType.NoValue);

            application.Command("install", installCommand =>
            {
                installCommand.Description = Resource.Command_Install_Description;

                installCommand.OnExecute(() =>
                {



                });
            });

            application.Command("remove", removeCommand =>
            {
                removeCommand.Description = Resource.Command_Remove_Description;

                removeCommand.OnExecute(() =>
                {




                });
            });

            application.Command("update", updateCommand =>
            {
                updateCommand.Description = Resource.Command_Update_Description;

                updateCommand.OnExecute(() =>
                {



                });
            });

            application.Command("search", searchCommand =>
            {
                searchCommand.Description = Resource.Command_Search_Description;




            });

            application.Command("list", listCommand =>
            {
                listCommand.Description = Resource.Command_List_Description;




            });

            application.OnExecute(() =>
            {
                if (help.HasValue())
                {
                    application.ShowHelp();
                }
                if(version.HasValue())
                {
                    Console.WriteLine($@"{Resource.App_Name} {Assembly.GetExecutingAssembly().GetProjectVersion().GetFriendlyVersionToString()}");
                }

                if (license.HasValue())
                {
                    
                }
            });

           return application.Execute(args);
        }
    }
}
