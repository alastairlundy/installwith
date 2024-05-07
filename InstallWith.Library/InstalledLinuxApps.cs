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

public class InstalledLinuxApps
{

    // ReSharper disable once IdentifierTypo
    [SupportedOSPlatform("linux")]
    public static IEnumerable<AppModel> GetInstalled(bool includeBrewCasks = true)
    {
        if (OperatingSystem.IsLinux())
        {
            List<AppModel> apps = new List<AppModel>();

            string[] binResult = CommandRunner.RunCommandOnLinux("ls -F /usr/bin | grep -v /").Split(Environment.NewLine);

            foreach (string app in binResult)
            {
                apps.Add(new AppModel(app, $"{Path.DirectorySeparatorChar}usr{Path.DirectorySeparatorChar}bin"));
            }

            if (includeBrewCasks)
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
