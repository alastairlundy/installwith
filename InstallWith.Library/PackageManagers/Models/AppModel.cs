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

#nullable enable
namespace InstallWith.Library.PackageManagers.Models;

public class AppModel
{
    public AppModel(string executableName, string installLocation)
    {
        this.ExecutableName = executableName;
        this.InstallLocation = installLocation;
    }

    public string? Author { get; set; }

    public string? FriendlyName { get; set; }
    public string ExecutableName { get; set; }
    public string InstallLocation { get; set; }
    public Version? InstalledVersion { get; set; }
}