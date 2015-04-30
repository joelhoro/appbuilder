using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace AppRunner.Utilities
{
    public class BuildResults
    {
        public int Warnings;
        public int Errors;
        public bool Success;

        public static BuildResults FromOutput(string output)
        {
            var warnings = 0;
            var errors = 0;

            return new BuildResults
            {
                Warnings = warnings,
                Errors = errors,
                Success = output.Contains("Build succeeded"),
            };
        }

        public override string ToString()
        {
            return String.Format("Build {0} [E:{1},W:{2}]", Success ? "Succeeded" : "Failed", Errors, Warnings);
        }
    }

    public class Solution
    {
        public string FullPathName;
        public Solution(string fullPathName)
        {
            FullPathName = fullPathName;
        }

        public Solution(string root, string solutionName)
        {
            FullPathName = String.Format(@"{0}\src\{1}\{1}.sln", root, solutionName);
        }

        public event DataReceivedEventHandler OutputDataReceived = (s, e) => { };

        public StringBuilder BuildOutput;

        /// <summary>
        /// Build solution synchroniously
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="executableName"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public BuildResults Build(string outputPath, bool verbose = false, DataReceivedEventHandler handler = null)
        {
            BuildOutput = new StringBuilder();
            var args = String.Format(@"/property:OutputPath=""{0}"" ""{1}""", outputPath, FullPathName);
            DataReceivedEventHandler addToOutput = (s, e) => BuildOutput.AppendLine(e.Data);
            if (verbose)
                addToOutput += (sender, eventArgs) => Console.WriteLine(eventArgs.Data);
            if (handler != null)
                addToOutput += handler;

            // Launch build asynchronuously but wait for completion
            var running = true;
            Executable.ExecutionCompletedHandler completionHandler =  (s, e) => { running = false; };
            var executable = Shell.RunCommandAsync(AppEnvironment.Settings.MsBuildPath, args, eventhandler: addToOutput, completedHandler: completionHandler);
            while (running)
                Thread.Sleep(250);

            return BuildResults.FromOutput(BuildOutput.ToString());
        }

    }

}
