using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Utilities
{
    public delegate void OutputChangedHandler(Executable sender, string line);
    public class Executable
    {
        public string FileName;
        private Process process;

        public event DataReceivedEventHandler OutputChanged = (s, l) => {};

        public void Run(string commandLineArgs = "", bool async = true)
        {
            if (process != null)
                throw new Exception("Can not run more than one process at a time");
            process = Shell.RunCommand(FileName, commandLineArgs, OutputChanged, async: async);
        }

        public override string ToString()
        {
            var output = "Process {0}".With(FileName);
            if (process != null)
                output = "[Running {0}] {1}".With(process.Id,output);
            return output;

        }
        public void Abort() {
            Console.WriteLine("Closing");
            process.Kill();
            process = null;
        }
    }
}
