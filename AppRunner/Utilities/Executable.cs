using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Utilities
{    
    public class Executable : Process
    {
        public Executable(string fileName) : base()
        {
            StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };
        }

        public string FileName { get { return StartInfo.FileName; } }
        public int PID { get { return Id; } }
        private bool IsRunning()
        {
            try { Process.GetProcessById(Id); }
            catch (InvalidOperationException) { return false; }
            catch (ArgumentException) { return false; }
            return true;
        }

        public void Run(string commandLineArgs = "", bool async = true, DataReceivedEventHandler eventHandler=null)
        {
            if (IsRunning())
                throw new Exception("Can not run more than one process at a time");
            StartInfo.Arguments = commandLineArgs;
            if (eventHandler != null)
                OutputDataReceived += eventHandler;
            Start();
            BeginOutputReadLine();
            if (!async)
                WaitForExit();
        }

        public override string ToString()
        {
            var output = "Process {0}".With(FileName);
            if (IsRunning())
                output = "[Running {0}] {1}".With(Id,output);
            return output;
        }
    }
}
