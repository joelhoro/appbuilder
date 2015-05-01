using System.Diagnostics;
using System.IO;
using System.Text;

namespace AppRunner.Utilities
{
    public static class Shell
    {
        public static Executable RunCommandAsync(string fileName, string args, DataReceivedEventHandler eventhandler = null, ExecutionCompletedHandler completedHandler = null)
        {
            var executable = new Executable(fileName);
            executable.ExecutionCompleted += completedHandler;
            executable.RunAsync(args, eventHandler : eventhandler);
            return executable;
        }

        public static string RunCommand(string fileName, string args)
        {
            var executable = new Executable(fileName);
            return executable.Run(args);
        }

        //public static void CompileAndRun(string root, string solutionName, string executableName, string commandLineArgs)
        //{
        //    var tempPath = Path.GetTempPath() + @"\test-build";
        //    var solution = new Solution(root, solutionName);
        //    var buildResults = solution.Build(tempPath);
        //    if (buildResults.Success)
        //    {
        //        var executable = new Executable(tempPath + @"\" + executableName);
        //        executable.Run(commandLineArgs);
        //    }
        //}
    }
}
