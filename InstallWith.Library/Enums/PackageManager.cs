using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallWith.Library.Enums
{
    public enum PackageManager
    {
        APT,
        AUR,
        DNF,
        Pacman,
        Nix,
        Homebrew,
        Flatpak,
        Snap,
        Chocolatey,
        Winget,
        MacPorts,
        NotSupported,
        NotDetected
    }
}
