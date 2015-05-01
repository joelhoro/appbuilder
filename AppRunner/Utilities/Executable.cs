using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AppRunner.Utilities
{
    public delegate void ExecutionCompletedHandler(object sender, EventArgs args);
    public interface IExecutable
    {
        event DataReceivedEventHandler OutputDataReceived;
        event ExecutionCompletedHandler ExecutionCompleted;
        void AddOutputHandler(DataReceivedEventHandler handler);
        void RemoveOutputHandlers();
        string Output { get; }
        void Destroy();
    }

    public class Executable : Process, IExecutable
    {
        #region Constructor
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

        public StringBuilder OutputBuilder = new StringBuilder();

        public string Output { get { return OutputBuilder.ToString(); } }

        public void InitializeOutputBuilder()
        {
            OutputBuilder = new StringBuilder();
            AddOutputHandler((sender, args) => OutputBuilder.AppendLine(args.Data));
        }

        #endregion

        #region Events and Handling of eventhandlers

        public event ExecutionCompletedHandler ExecutionCompleted = delegate { };

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
        #endregion


        public string FileName { get { return StartInfo.FileName; } }
        public int PID { get { return Id; } }
        private bool IsRunning()
        {
            try { Process.GetProcessById(Id); }
            catch (InvalidOperationException) { return false; }
            catch (ArgumentException) { return false; }
            return true;
        }


        private void Run(string commandLineArgs = "", bool async = true, DataReceivedEventHandler eventHandler=null)
        {
            if (IsRunning())
                throw new Exception("Can not run more than one process at a time");
            StartInfo.Arguments = commandLineArgs;
            AddOutputHandler(eventHandler);
            Start();
            // Create a task that waits for the end and sends an ExecutionCompleted event when done
            // (couldn't make the Exited event to work for some reason)
            Task.Run(() =>
            {
                WaitForExit();
                ExecutionCompleted(this,new EventArgs());
            });

            BeginOutputReadLine();
            if (!async)
                WaitForExit();
        }

        /// <summary>
        /// Asynchronuous version - takes eventHandler that gets invoked at every line
        /// </summary>
        /// <param name="commandLineArgs"></param>
        /// <param name="eventHandler"></param>
        public void RunAsync(string commandLineArgs = "", DataReceivedEventHandler eventHandler = null)
        {
            InitializeOutputBuilder();
            Run(commandLineArgs, async:true, eventHandler:eventHandler);
        }

        /// <summary>
        /// Synchronuous version - returns process' output
        /// </summary>
        /// <param name="commandLineArgs"></param>
        /// <returns></returns>
        public string Run(string commandLineArgs = "")
        {
            var output = new StringBuilder();
            DataReceivedEventHandler eventHandler = (s, e) => output.AppendLine(e.Data);
            Run(commandLineArgs, async: false, eventHandler: eventHandler);
            return output.ToString();
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
        }
    }
}
