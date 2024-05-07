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

using System.Runtime.Versioning;

using InstallWith.Library.PackageManagers;

using PlatformKit;


namespace InstallWith.Library;

public class InstalledMacApps
{
  
    [SupportedOSPlatform("macos")]
    public static IEnumerable<AppModel> GetInstalled()
    {
        if (OperatingSystem.IsMacOS())
        {
            List<AppModel> apps = new List<AppModel>();

            string binDirectory = $"{Path.DirectorySeparatorChar}usr{Path.DirectorySeparatorChar}bin";

            string listFilesStart = "ls -F";
            string listFilesEnd = " | grep -v /";

            string[] binResult = CommandRunner.RunCommandOnLinux($"{listFilesStart} {binDirectory} {listFilesEnd}").Split(Environment.NewLine);

            foreach (string app in binResult)
            {
                apps.Add(new AppModel(app, binDirectory));
            }

            string applicationsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Programs);

            string[] appResults = CommandRunner
                .RunCommandOnMac($"{listFilesStart} {applicationsFolder} {listFilesEnd}")
                .Split(Environment.NewLine);

            foreach (string app in appResults)
            {
                apps.Add(new AppModel(app, applicationsFolder));
            }

            if (HomeBrew.IsHomeBrewInstalled())
            {
                foreach (AppModel app in HomeBrew.GetInstalled())
                {
                    apps.Add(app);
                }
            }

            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }
}