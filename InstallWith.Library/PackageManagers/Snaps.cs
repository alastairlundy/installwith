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

using InstallWith.Library.PackageManagers.Models;

using PlatformKit;

namespace InstallWith.Library.PackageManagers;

public static class Snaps
{
    /// <summary>
    /// Detect what Snap packages (if any) are installed on a linux distribution or on macOS.
    /// </summary>
    /// <returns>Returns a list of installed snaps. Returns an empty array if no Snaps are installed.</returns>
    /// <exception cref="PlatformNotSupportedException">Throws an exception if run on a Platform other than Linux, macOS, and FreeBsd.</exception>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public static IEnumerable<AppModel> GetInstalled()
    {
        List<AppModel> apps = new List<AppModel>();

        if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            if (IsSnapInstalled())
            {
                string[] snapResults = CommandRunner.RunCommandOnLinux(
                    $"ls {Path.DirectorySeparatorChar}snap{Path.DirectorySeparatorChar}bin").Split(' ');

                foreach (string snap in snapResults)
                {
                    apps.Add(new AppModel(snap,
                        $"{Path.DirectorySeparatorChar}snap{Path.DirectorySeparatorChar}bin"));
                }

                return apps.ToArray();
            }

            apps.Clear();
            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// Detect if the Snap package manager is installed.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public static bool IsSnapInstalled()
    {
        if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            return  Directory.Exists($"{Path.DirectorySeparatorChar}snap{Path.DirectorySeparatorChar}bin");
        }

        throw new PlatformNotSupportedException();
    }
        
    /// <summary>
    /// Determines whether a snap package is installed.
    /// </summary>
    /// <param name="packageName"></param>
    /// <returns></returns>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public static bool IsPackageInstalled(string packageName)
    {
        if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
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

        throw new PlatformNotSupportedException();
    }
}