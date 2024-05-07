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

public class Chocolatey
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("windows")]
    public static IEnumerable<AppModel> GetUpdatable()
    {
        if (IsChocolateySupported() && IsChocolateyInstalled())
        {
            List<AppModel> apps = new List<AppModel>();
            
            string[] chocoResults = CommandRunner.RunCmdCommand("choco outdated -l -r --id-only").Split(Environment.NewLine);

            string chocolateyLocation = CommandRunner.RunPowerShellCommand("$env:ChocolateyInstall");
            
            foreach (string package in chocoResults)
            {
                apps.Add(new AppModel(package, chocolateyLocation));
            }

            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("windows")]
    public static IEnumerable<AppModel> GetInstalled()
    {
        if (IsChocolateySupported() && IsChocolateyInstalled())
        {
            List<AppModel> apps = new List<AppModel>();
            
            string[] chocoResults = CommandRunner.RunCmdCommand("choco list -l -r --id-only").Split(Environment.NewLine);

            string chocolateyLocation = CommandRunner.RunPowerShellCommand("$env:ChocolateyInstall");
            
            foreach (string package in chocoResults)
            {
                apps.Add(new AppModel(package, chocolateyLocation));
            }

            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static bool IsChocolateyInstalled()
    {
        if (OperatingSystem.IsWindows())
        {
            try
            {
                string[] chocoTest = CommandRunner.RunCmdCommand("choco info").Split(' ');
                    
                if (chocoTest[0].Contains("Chocolatey"))
                {
                    Version.Parse(chocoTest[1]);

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
    /// 
    /// </summary>
    /// <returns></returns>
    public static bool IsChocolateySupported()
    {
        return OperatingSystem.IsWindows();
    }
}