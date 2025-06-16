using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CookieDestructive
{
    class cmd
    {
        public static bool ExecuteCommand(string command, bool IsVisible = false)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c" + command,
                    RedirectStandardOutput = !IsVisible,
                    RedirectStandardInput = !IsVisible,
                    UseShellExecute = false,
                    CreateNoWindow = !IsVisible
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
