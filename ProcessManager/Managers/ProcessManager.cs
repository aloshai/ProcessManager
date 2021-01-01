using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.Managers
{
    public static class ProcessManager
    {
        public static List<Process> ActiveProcess;
        public static List<string> FileNames;
        public static Process[] GetAllProcesses() => Process.GetProcesses();


        public static Process StartProcess(ProcessStartInfo info)
        {
            var process = Process.Start(info);
            ActiveProcess.Add(process);
            FileNames.Add(info.FileName);
            return process;
        }
    }
}
