using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstallWith.Library.Enums;
using InstallWith.Library.PackageManagers;
using InstallWith.Library.PackageManagers.Models;

namespace InstallWith.Library
{
    public class Commands
    {
        internal static string GetPackageManagerSyntax(string key)
        {

        }

        public static void InstallPackage(PackageManager packageManager, string packageName)
        {


        }
        
        public static void RemovePackage(PackageManager packageManager, string packageName) 
        {
            
        }


        internal static string[] ExtractPackageNames(IEnumerable<AppModel> apps)
        {
            List<string> updates = new List<string>();

            foreach (AppModel app in apps)
            {
                updates.Add(app.ExecutableName);
            }

            return updates.ToArray();
        }

        public string[] GetAvailableUpdates(PackageManager packageManager)
        {
            switch(packageManager)
            {
                case PackageManager.Winget:
                    return ExtractPackageNames(Winget.GetUpdatable());
                case PackageManager.Chocolatey:
                    return ExtractPackageNames(Chocolatey.GetUpdatable());
                case PackageManager.Snap:
                    return ExtractPackageNames(Snaps.);
               
            }
        }

        public static void UpdatePackageSources(PackageManager packageManager)
        {
            if(packageManager == PackageManager.Snap)
            {

            }
        }

        public static void UpgradePackage(PackageManager packageManager, string packageName)
        {

        }

        public static void UpgradeAllPackages(PackageManager packageManager) 
        {

        }
    }
}
