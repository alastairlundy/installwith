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

    internal static string? GetPackageManagerSyntax(PackageManager packageManager, string key)
    {
        string path = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}";

        string fileName = "";

       switch(packageManager)
        {
            case PackageManager.APT:
                fileName = "apt.json";
                break;
            case PackageManager.AUR:
                fileName = "aur.json";
                break;
            case PackageManager.Chocolatey:
                fileName = "chocolatey.json";
                break;
            case PackageManager.DNF:
                fileName = "dnf.json";
                break;
            case PackageManager.Flatpak:
                fileName = "flatpak.json";
                break;
            case PackageManager.Homebrew:
                fileName = "homebrew.json";
                break;
            case PackageManager.Pacman:
                fileName = "pacman.json";
                break;
            case PackageManager.Snap:
                fileName = "snap.json";
                break;
            case PackageManager.Winget:
                fileName = "winget.json";
                break;
        }

        fileName = fileName.Insert(0, $"{path} ");

        using var doc = JsonDocument.Parse(File.ReadAllText(fileName));

        return doc.RootElement.GetProperty(key).GetString();
    }

    public static void InstallPackage(PackageManager packageManager, string packageName)
    {


    }
    
    public static void RemovePackage(PackageManager packageManager, string packageName) 
    {
        
    }


    internal static IEnumerable<string> ExtractPackageNames(IEnumerable<AppModel> apps)
    {
        List<string> updates = new List<string>();

        foreach (AppModel app in apps)
        {
            updates.Add(app.ExecutableName);
        }

        return updates.ToArray();
    }

    public IEnumerable<string> GetAvailableUpdates(PackageManager packageManager)
    {
        switch(packageManager)
        {
            case PackageManager.Winget:
                return ExtractPackageNames(Winget.GetUpdatable());
            case PackageManager.Chocolatey:
                return ExtractPackageNames(Chocolatey.GetUpdatable());
          //  case PackageManager.Snap:
            //    return ExtractPackageNames(Snaps.Get);
           
        }
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
