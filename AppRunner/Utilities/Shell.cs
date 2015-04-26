using System.Diagnostics;
using System.IO;
using System.Text;

namespace AppRunner.Utilities
{
    public static class Shell
    {
        public static Executable RunCommand(string fileName, string args, DataReceivedEventHandler eventhandler = null, bool async = true)
        {
            var executable = new Executable(fileName);
            executable.Run(args, async: async, eventHandler : eventhandler);
            return executable;
        }

        public static string RunMsBuild(string args, DataReceivedEventHandler handler = null, bool async = true)
        {
            var output = new StringBuilder();
            DataReceivedEventHandler addToOutput = (s, e) => output.AppendLine(e.Data);
            RunCommand(AppEnvironment.Settings.MsBuildPath, args, eventhandler: handler + addToOutput, async : async);
            return output.ToString();
        }

        public static void CompileAndRun(string root, string solutionName, string executableName, string commandLineArgs)
        {
            var tempPath = Path.GetTempPath() + @"\test-build";
            var solution = new Solution(root, solutionName);
            var buildResults = solution.Build(tempPath,executableName);
            if (buildResults.Success)
            {
                var executable = new Executable(tempPath + @"\" + executableName);
                executable.Run(commandLineArgs);
            }
        }
    }
}
