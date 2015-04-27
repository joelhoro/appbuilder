using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // Don't like this, but not clear how to make sure we remove all the events
        private List<DataReceivedEventHandler> _outputHandlers = new List<DataReceivedEventHandler>();

        public void AddOutputHandler(DataReceivedEventHandler handler)
        {
            _outputHandlers.Add(handler);
            OutputDataReceived += handler;
        }

        public void RemoveOutputHandlers()
        {
            _outputHandlers.ForEach(handler => OutputDataReceived -= handler);
            _outputHandlers.Clear();
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

        public delegate void ExecutionCompletedHandler(object sender, EventArgs args);

        public event ExecutionCompletedHandler ExecutionCompleted = delegate {};

        public void Run(string commandLineArgs = "", bool async = true, DataReceivedEventHandler eventHandler=null)
        {
            if (IsRunning())
                throw new Exception("Can not run more than one process at a time");
            StartInfo.Arguments = commandLineArgs;
            AddOutputHandler(eventHandler);
            Start();
            Task.Run(() =>
            {
                WaitForExit();
                //OutputDataReceived -= eventHandler;
                ExecutionCompleted(this,new EventArgs());
            });
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

        public void Destroy()
        {
            RemoveOutputHandlers();
            try
            {
                Kill();
            }
            catch
            {
                // ignored
            }
            //ExecutionCompleted(this, new EventArgs());
        }
    }
}
