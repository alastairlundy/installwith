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
using PlatformKit.Mac;

namespace InstallWith.Library.PackageManagers;

public static class HomeBrew
{
    /// <summary>
    /// Returns all the detected installed brew casks.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    public static IEnumerable<AppModel> GetInstalled()
    {
        if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
        {
            List<AppModel> apps = new List<AppModel>();

            if (IsHomeBrewInstalled())
            {
                string[] casks = CommandRunner.RunCommandOnMac("ls -l bin").Split(Environment.NewLine);

                foreach (string cask in casks)
                {
                    string[] nameArr = cask.Replace("->", string.Empty).Replace(" ", string.Empty).Split(" ");

                    if (MacOsAnalyzer.IsAppleSiliconMac())
                    {
                        apps.Add(new AppModel(nameArr[0].Replace("bin/", string.Empty), nameArr[1].Replace("..", "/opt/homebrew")));
                    }
                    else
                    {
                        apps.Add(new AppModel(nameArr[0].Replace("bin/", string.Empty), nameArr[1]));
                    }
                }

                return apps;
            }
            
            apps.Clear();
            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// Determines whether the Homebrew package manager is installed or not.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static bool IsHomeBrewInstalled()
    {
        if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
        {
            try
            {
                string[] brewTest = CommandRunner.RunCommandOnLinux("brew -v").Split(' ');
                
                if (brewTest[0].Contains("Flatpak"))
                {
                    Version.Parse(brewTest[1]);

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
    /// Determines whether the specified brew cask is installed or not.
    /// </summary>
    /// <param name="caskName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static bool IsCaskInstalled(string caskName)
    {
        if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
        {
            if (IsHomeBrewInstalled())
            {
                foreach (AppModel app in GetInstalled())
                {
                    if (app.ExecutableName.Equals(caskName))
                    {
                        return true;
                    }
                }

                return false;
            }

            throw new ArgumentException();
        }

        throw new PlatformNotSupportedException();
    }
}