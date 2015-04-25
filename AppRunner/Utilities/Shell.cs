using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Utilities
{
    public static class Shell
    {
        public static Process RunCommand(string fileName, string args, DataReceivedEventHandler eventhandler = null, bool async = true)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = args,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };

            if(eventhandler != null)
                process.OutputDataReceived += eventhandler;

            process.Start();
            process.BeginOutputReadLine();
            if (!async)
                process.WaitForExit();
            return process;
        }

        public static string RunMSBuild(string args, Action<string> callback = null)
        {
            throw new NotImplementedException();
            //return RunCommand(UserSettings.MSBuildPath, args, callback);
        }

        public static void CompileAndRun(string root, string solutionName, string executableName, string commandLineArgs)
        {
            var tempPath = Path.GetTempPath() + @"\test-build";
            var solution = new Solution(root, solutionName);
            var buildResults = solution.Build(tempPath,executableName);
            if (buildResults.Success)
            {
                var executable = new Executable { FileName = tempPath + @"\" + executableName };
                executable.Run(commandLineArgs);
            }
        }
    }
}
