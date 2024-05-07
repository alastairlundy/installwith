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

using InstallWith.Library.Enums;
using InstallWith.Library.PackageManagers;

using PlatformKit.Linux;
using PlatformKit.Linux.Enums;
using PlatformKit.Windows;
// ReSharper disable RedundantIfElseBlock

namespace InstallWith.Library;

public class PackageManagerDetector
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static PackageManager GetDefaultForPlatform()
    {
       if(OperatingSystem.IsLinux())
       {
            LinuxOsRelease osRelease = LinuxAnalyzer.GetLinuxDistributionInformation();
            LinuxDistroBase distroBase = LinuxAnalyzer.GetDistroBase(osRelease);

            switch (distroBase)
            {
                case LinuxDistroBase.Arch or LinuxDistroBase.Manjaro:
                    return PackageManager.Pacman;
                case LinuxDistroBase.Debian:
                    return PackageManager.APT;
                case LinuxDistroBase.Ubuntu:
                    string osName = osRelease.PrettyName.ToLower();

                    if (osName.Contains("buntu"))
                    {
                        return PackageManager.Snap;
                    }
                    else
                    {
                        return PackageManager.APT;
                    }
                case LinuxDistroBase.Fedora or LinuxDistroBase.RHEL:
                    return PackageManager.DNF;
                default:
                    if(Flatpaks.IsFlatpakInstalled())
                    {
                        return PackageManager.Flatpak;
                    }

                    if(Snaps.IsSnapInstalled())
                    {
                        return PackageManager.Snap;
                    }
                    if(HomeBrew.IsHomeBrewInstalled())
                    {
                        return PackageManager.Homebrew;
                    }

                    return PackageManager.NotDetected;
            }

       }
       if(OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst()) 
       {
            if(HomeBrew.IsHomeBrewInstalled()) 
            {
                return PackageManager.Homebrew;
            }

            // TODO: Add Mac Ports support here


            return PackageManager.NotSupported;
       }
       if (OperatingSystem.IsWindows())
       {
            if (WindowsAnalyzer.IsAtLeastVersion(WindowsVersion.Win10_v1809) && WindowsAnalyzer.GetWindowsEdition() != WindowsEdition.Server)
            {
                return PackageManager.Winget;
            }

            if (Chocolatey.IsChocolateySupported() && Chocolatey.IsChocolateyInstalled())
            {
                return PackageManager.Chocolatey;
            }

            return PackageManager.NotSupported;
       }

       throw new PlatformNotSupportedException();
    }
}
