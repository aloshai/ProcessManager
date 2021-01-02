using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.Managers
{
    public static class ProcessManager
    {
        public static List<Process> Processes = new List<Process>();

        public static Process StartProcess(string path, string command)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                WorkingDirectory = path,
                // CreateNoWindow = true,
                // WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"cmd /K {command}",
                FileName = "cmd.exe",
                UseShellExecute = false
            };
            process.ErrorDataReceived += ProcessOnErrorDataReceived;
            process.OutputDataReceived += ProcessOnOutputDataReceived;
            Processes.Add(process);
            process.Start();
            return process;
        }

        public static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
                ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();

            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection) KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"]));
            }

            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }


        private static void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("OUTPUT: " + e.Data);
        }

        private static void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("ERROR: " + e.Data);
        }
    }
}
