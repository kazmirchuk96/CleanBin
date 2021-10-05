using System;
using System.Runtime.InteropServices;
using IWshRuntimeLibrary;

namespace CleanBin
{
    class Program
    {
        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        static void Main(string[] args)
        {
            SHEmptyRecycleBin(IntPtr.Zero, null,
                RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOPROGRESSUI | RecycleFlags.SHERB_NOSOUND);
            CreateShortcut("CleanBin", Environment.GetFolderPath(Environment.SpecialFolder.Startup), @"C:\Program Files (x86)\CleanBin\CleanBin.exe");//добавление программы в автозагрузку
        }

        static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            var shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "My shortcut description";   // The description of the shortcut
            shortcut.IconLocation = @"c:\myicon.ico";           // The icon of the shortcut
            shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                    // Save the shortcut
        }
    }
}
