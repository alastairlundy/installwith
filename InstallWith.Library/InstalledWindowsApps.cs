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

public class InstalledWindowsApps
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeWindowsSystemPrograms"></param>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("windows")]
    public static IEnumerable<AppModel> GetInstalled(bool includeWindowsSystemPrograms)
    {
        if (OperatingSystem.IsWindows())
        {
            List<AppModel> apps = new List<AppModel>();

            string programFiles = CommandRunner.RunCmdCommand(
                $"dir {Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}");

            string programFilesX86 = CommandRunner.RunCmdCommand(
                $"dir {Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}");

            string appData = CommandRunner
                .RunCmdCommand($"dir {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}");

            string winPrograms = CommandRunner
                .RunCmdCommand(Environment.GetFolderPath(Environment.SpecialFolder.System));

            foreach (AppModel program in ExpandWinSpecialFolderPath(programFiles))
            {
                apps.Add(program);
            }
            foreach (AppModel program in ExpandWinSpecialFolderPath(programFilesX86))
            {
                apps.Add(program);
            }
            foreach (AppModel program in ExpandWinSpecialFolderPath(appData))
            {
                apps.Add(program);
            }

            if (includeWindowsSystemPrograms)
            {
                foreach (AppModel program in ExpandWinSpecialFolderPath(winPrograms))
                {
                    apps.Add(program);
                }
            }

            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("windows")]
    protected static IEnumerable<AppModel> ExpandWinSpecialFolderPath(string directory)
    {
        List<AppModel> apps = new List<AppModel>();

        if (OperatingSystem.IsWindows())
        {
            string[] files = directory.Split(Environment.NewLine);

            for (int programIndex = 0; programIndex < files.Length; programIndex++)
            {
                string item = files[programIndex];

                if (item.Contains("<DIR>"))
                {
                    IEnumerable<AppModel> programs = GetExecutablesInDirectory(item);

                    foreach (AppModel program in programs)
                    {
                        apps.Add(program);
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
    /// <param name="folderPath"></param>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    [SupportedOSPlatform("windows")]
    public static IEnumerable<AppModel> GetExecutablesInDirectory(string folderPath)
    {
        if (OperatingSystem.IsWindows())
        {
            List<AppModel> apps = new List<AppModel>();
            string[] directories = Directory.GetDirectories(folderPath);

            for (int directoryIndex = 0; directoryIndex < directories.Length; directoryIndex++)
            {
                string[] programs = Directory.GetFiles(directories[directoryIndex]);

                foreach (var program in programs)
                {
                    if (program.EndsWith(".exe") || program.EndsWith(".appx") || program.EndsWith(".msi"))
                    {
                        apps.Add(new AppModel(program, directories[directoryIndex]));
                    }
                }
            }

            return apps.ToArray();
        }

        throw new PlatformNotSupportedException();
    }
}
