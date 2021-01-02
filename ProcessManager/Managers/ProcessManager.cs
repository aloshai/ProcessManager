using ProcessManager.Models;
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
        public static List<ProcessModel> Processes = new List<ProcessModel>();

        public static ProcessModel StartProcess(string path, string command)
        {
            var processModel = Processes.FirstOrDefault(pm => pm.Path == path);
            if(processModel != null) KillProcess(processModel);

            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                WorkingDirectory = path,
                //CreateNoWindow = true,
                //WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"cmd /C {command}",
                FileName = "cmd.exe",
                UseShellExecute = false
            };
            if (processModel == null)
            {
                processModel = new ProcessModel(process, command, path);
                Processes.Add(processModel);
            }
            else
            {
                processModel.Path = path;
                processModel.Command = command;
                processModel.Process = process;
            }

            process.Start();
            return processModel;
        }

        public static void KillProcess(ProcessModel processModel)
        {
            KillProcessAndChildrens(processModel.Process.Id);
            processModel.Process = null;
        }
        public static void KillProcess(Process process) => KillProcessAndChildrens(process.Id);
        public static void KillProcess(int pid) => KillProcessAndChildrens(pid);
        private static void KillProcessAndChildrens(int pid)
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
                
            }
        }
    }
}
