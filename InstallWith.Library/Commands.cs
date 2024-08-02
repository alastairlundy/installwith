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

using System;
using System.Collections.Generic;
using System.Text.Json;

using PlatformKit.Software;
using PlatformKit.Software.Enums;
using PlatformKit.Software.PackageManagers;


namespace InstallWith.Library;

public class Commands
{
    internal bool CommandIncludesInstallWith(string packageName)
    {
        if (packageName.ToLower().Equals("installwith") || packageName.ToLower().Contains("installwith") || packageName.ToLower().Equals("install_with"))
        {
            return true;
        }
        return false;
    }



    public static void InstallPackage(PackageManager packageManager, string packageName)
    {


    }
    
    public static void RemovePackage(PackageManager packageManager, string packageName) 
    {
        
    }


    public IEnumerable<string> GetAvailableUpdates(PackageManager packageManager)
    {
        if (OperatingSystem.IsWindows())
        {
            switch (packageManager)
            {
                case PackageManager.Winget:
                    Winget winget = new Winget();
                    return ExtractPackageNames(winget.GetUpdatable());
                case PackageManager.Chocolatey:
                    Chocolatey chocolatey = new Chocolatey();
                    return ExtractPackageNames(chocolatey.GetUpdatable());
            }
        }
        if (OperatingSystem.IsLinux())
        {
            switch (packageManager)
            {
                case PackageManager.Snap:
                    Snap snap = new Snap();
                    return ExtractPackageNames(snap.GetUpdatable());
                case PackageManager.Flatpak:
                    Flatpak flatpak = new Flatpak();
                    return ExtractPackageNames(flatpak.GetUpdatable());
                case PackageManager.Homebrew:
                    HomeBrew homeBrew = new HomeBrew();
                    return ExtractPackageNames(homeBrew.GetUpdatable());
            }
        }

        if (OperatingSystem.IsFreeBSD())
        {
            switch (packageManager)
            {
                case PackageManager.Snap:
                    Snap snap = new Snap();
                    return ExtractPackageNames(snap.GetUpdatable());
                case PackageManager.Homebrew:
                    HomeBrew homeBrew = new HomeBrew();
                    return ExtractPackageNames(homeBrew.GetUpdatable());
            }
        }

        if (OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst())
        {
            switch (packageManager)
            {
                case PackageManager.Homebrew:
                    HomeBrew homeBrew = new HomeBrew();
                    return ExtractPackageNames(homeBrew.GetUpdatable());
            }
        }

        throw new PlatformNotSupportedException();
    }

    public static void UpdatePackageSources(PackageManager packageManager)
    {
       
    }

    public static void UpgradePackage(PackageManager packageManager, string packageName)
    {

    }

    public static void UpgradeAllPackages(PackageManager packageManager) 
    {

    }
}
