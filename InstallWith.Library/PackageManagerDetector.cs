using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstallWith.Library.Enums;

using PlatformKit.Linux;
using PlatformKit.Linux.Enums;
using PlatformKit.Software;
using PlatformKit.Windows;

namespace InstallWith.Library
{
    public class PackageManagerDetector
    {

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
                        if(InstalledFlatpaks.IsFlatpakInstalled())
                        {
                            return PackageManager.Flatpak;
                        }

                        if(InstalledSnaps.IsSnapInstalled())
                        {
                            return PackageManager.Snap;
                        }
                        if(InstalledBrewCasks.IsHomeBrewInstalled())
                        {
                            return PackageManager.Homebrew;
                        }

                        return PackageManager.NotDetected;
                }

            }
           if(OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst()) 
            {
                if(InstalledBrewCasks.IsHomeBrewInstalled()) 
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

                

                return PackageManager.NotSupported;
            }
            throw new PlatformNotSupportedException();
        }
    }
}
