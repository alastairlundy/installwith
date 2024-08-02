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
using System.IO;
using System.Text.Json;

using PlatformKit.Software.Enums;
using PlatformKit.Software.Exceptions;

namespace InstallWith.Library;

public class PackageManagerSyntaxHandler
{
    public static bool DoesPackageManagerSupportCommand(PackageManager packageManager, string key)
    {
        try
        {
           string? result = GetPackageManagerSyntax(packageManager, key);

           if (result == null)
           {
               return false;
           }
           else
           {
               return true;
           }
        }
        catch
        {
            return false;
        }
    }
    
    internal static string? GetPackageManagerSyntax(PackageManager packageManager, string key)
    {
        string path = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}";

        string fileName = packageManager switch
        {
            PackageManager.APT => "apt.json",
            PackageManager.AUR => "aur.json",
            PackageManager.Chocolatey => "chocolatey.json",
            PackageManager.DNF => "dnf.json",
            PackageManager.Flatpak => "flatpak.json",
            PackageManager.Homebrew => "homebrew.json",
            PackageManager.Pacman => "pacman.json",
            PackageManager.Snap => "snap.json",
            PackageManager.Winget => "winget.json",
            _ => ""
        };

        if (fileName.Equals(""))
        {
            throw new PackageManagerNotSupportedException(packageManager.ToString());
        }

        fileName = fileName.Insert(0, $"{path} ");

        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(fileName));

        return document.RootElement.GetProperty(key).GetString();
    }
}