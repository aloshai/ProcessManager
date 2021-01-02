using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.Models
{
    public class ProcessModel
    {
        public ProcessModel(Process process, string command, string path)
        {
            Process = process;
            Command = command;
            Path = path;
        }

        public static string Formalize(string command, string path)
        {
            var pathNames = path.Split('\\');
            var processName = pathNames.Length == 0 ? "Any Process" : pathNames[pathNames.Length - 1];
            return $"{processName} | {command}";
        }

        public override string ToString()
        {
            return Formalize(this.Command, this.Path);
        }

        public Process Process;
        public string Command, Path;
    }
}
