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
using InstallWith.Library.Models;

using PlatformKit;

namespace InstallWith.Library.PackageManagers;


// ReSharper disable once ClassNeverInstantiated.Global
public static class Flatpaks
{
    
    /// <summary>
    /// Platforms Supported On: Linux and FreeBsd.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public static IEnumerable<AppModel> GetInstalled()
    {
        if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            List<AppModel> apps = new List<AppModel>();

            if (IsFlatpakInstalled())
            {
                string[] flatpakResults = CommandRunner.RunCommandOnLinux("flatpak list --columns=name")
                .Split(Environment.NewLine);

                string installLocation = CommandRunner.RunCommandOnLinux("flatpak --installations");

                foreach (string flatpak in flatpakResults)
                {
                    apps.Add(new AppModel(flatpak, installLocation));
                }

                return apps.ToArray();
            }

            apps.Clear();
            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// Determines whether the Flatpak package manager is installed or not.
    /// </summary>
    /// <returns></returns>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public static bool IsFlatpakInstalled()
    {
        if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            try
            {
                string[] flatpakTest = CommandRunner.RunCommandOnLinux("flatpak --version").Split(' ');
                
                if (flatpakTest[0].Contains("Flatpak"))
                {
                    Version.Parse(flatpakTest[1]);

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// Determines whether a flatpak package is installed.
    /// </summary>
    /// <param name="packageName"></param>
    /// <returns></returns>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public static bool IsPackageInstalled(string packageName)
    {
        foreach (AppModel app in GetInstalled())
        {
            if (app.ExecutableName.Equals(packageName))
            {
                return true;
            }       
        }

        return false;
    }
}