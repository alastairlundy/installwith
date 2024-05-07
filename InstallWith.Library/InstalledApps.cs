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

using PlatformKit;

namespace InstallWith.Library;

public class InstalledApps
{

    /// <summary>
    /// Determine whether an app is installed or not.
    /// </summary>
    /// <param name="appName"></param>
    /// <returns></returns>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("macos")]
    public static bool IsInstalled(string appName)
    {
        foreach (AppModel app in Get())
        {
            if (app.ExecutableName.Equals(appName))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets a collection of apps and programs installed on this device.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("macos")]
    public static IEnumerable<AppModel> Get()
    {
        if (OperatingSystem.IsWindows())
        {
            return InstalledWindowsApps.GetInstalled(true);
        }
        else if (OperatingSystem.IsMacOS())
        {
            return InstalledMacApps.GetInstalled();
        }
        else if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            return InstalledLinuxApps.GetInstalled(true);
        }

        throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// Opens the specified app or program.
    /// </summary>
    /// <param name="appModel"></param>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("macos")]
    public static void Open(AppModel appModel)
    {
        if (OperatingSystem.IsWindows())
        {
            ProcessRunner.RunProcessOnWindows(appModel.InstallLocation, appModel.ExecutableName);
        }
        else if (OperatingSystem.IsMacOS())
        {
            ProcessRunner.RunProcessOnMac(appModel.InstallLocation, appModel.ExecutableName);
        }
        else if (OperatingSystem.IsLinux())
        {
            ProcessRunner.RunProcessOnLinux(appModel.InstallLocation, appModel.ExecutableName);
        }
        else if (OperatingSystem.IsFreeBSD())
        {
            ProcessRunner.RunProcessOnFreeBsd(appModel.InstallLocation, appModel.ExecutableName);
        }
        else
        {
            throw new PlatformNotSupportedException();
        }
    }
}