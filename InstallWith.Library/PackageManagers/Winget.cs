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
using System.Text;

using AlastairLundy.Extensions.System.VersionExtensions;

using InstallWith.Library.PackageManagers.Models;

using PlatformKit;
using PlatformKit.Windows;

namespace InstallWith.Library.PackageManagers;

public class Winget
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("windows")]
    public static IEnumerable<AppModel> GetUpdatable()
    {
        if (IsWingetSupported() && IsWingetInstalled())
        {
            List<AppModel> apps = new List<AppModel>();

            string[] results = CommandRunner.RunCmdCommand("winget upgrade --source=winget")
                .Replace("-", string.Empty).Split(Environment.NewLine);
            
            string wingetLocation = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            
            int idPosition = results[0].IndexOf("Id", StringComparison.Ordinal);

            for (int index = 1; index < results.Length; index++)
            {
                string line = results[index];

                if (line.ToLower().Contains("upgrades available"))
                {
                    break;
                }
                
                StringBuilder stringBuilder = new StringBuilder();

                for (int charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    if (charIndex < (idPosition - 1))
                    {
                        stringBuilder.Append(line[charIndex]);
                    }
                    else
                    {
                        string appName = stringBuilder.ToString();
                        
                        if (appName.Contains("  "))
                        {
                            appName = appName.Replace("  ", string.Empty);
                        }
                        apps.Add(new AppModel(appName, wingetLocation));
                        break; 
                    }
                }
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
        if (IsWingetSupported() && IsWingetInstalled())
        {
            List<AppModel> apps = new List<AppModel>();

            string[] results = CommandRunner.RunCmdCommand("winget list --source=winget")
                .Replace("-", string.Empty).Split(Environment.NewLine);
            
            string wingetLocation = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            int idPosition = results[0].IndexOf("Id", StringComparison.Ordinal);
            
            for (int index = 1; index < results.Length; index++)
            {
                string line = results[index];
                
                StringBuilder stringBuilder = new StringBuilder();

                for (int charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    if (charIndex < (idPosition - 1))
                    {
                        stringBuilder.Append(line[charIndex]);
                    }
                    else
                    {
                        string appName = stringBuilder.ToString();
                        
                        if (appName.Contains("  "))
                        {
                           appName = appName.Replace("  ", string.Empty);
                        }
                        apps.Add(new AppModel(appName, wingetLocation));
                        break;
                    }
                }
            }

            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [SupportedOSPlatform("windows")]
    public static bool IsWingetInstalled()
    {
        if (OperatingSystem.IsWindows())
        {
            if (!IsWingetSupported())
            {
                return false;
            }
            
            try
            {
                string[] wingetTest = CommandRunner.RunCmdCommand("winget").Split(' ');
                    
                if (wingetTest[0].Contains("Windows") && wingetTest[1].Contains("Package") && wingetTest[2].Contains("Manager"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        return false;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static bool IsWingetSupported()
    {
        if (OperatingSystem.IsWindows())
        {
            WindowsEdition edition = WindowsAnalyzer.GetWindowsEdition();
            
            if (WindowsAnalyzer.IsAtLeastVersion(WindowsVersion.Win10_v1809) &&
                edition != WindowsEdition.Server && edition != WindowsEdition.Team)
            {
                return true;
            }
            else
            {
                if (WindowsAnalyzer.GetWindowsVersion().IsOlderThan(WindowsAnalyzer.GetWindowsVersionFromEnum(WindowsVersion.Win10_v1809)))
                {
                    return false;
                }
                if (WindowsAnalyzer.GetWindowsVersionToEnum() == WindowsVersion.Win10_v1809 &&
                    WindowsAnalyzer.GetWindowsEdition() == WindowsEdition.Server)
                {
                    return false;
                }

                return false;
            }
        }

        return false;
    }
}