using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
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

    public interface ISolution : IExecutable
    {
        void BuildAsync(string outputPath);
        string FullPathName { get; }

    }

    public class DummySolution : Executable, ISolution
    {
        private const string DummyPath = "DummyExe.exe";
        public string FullPathName { get; set; }

        public DummySolution(string root, string solutionName) : base(DummyPath)
        {
            FullPathName = String.Format(@"{0}\src\{1}\{1}.sln", root, solutionName);
        }

        public void BuildAsync(string outputPath) 
        {
            var args = "5 Testing";
            RunAsync(args);
        }
    }

    public class Solution : Executable, ISolution
    {
        public string FullPathName { get; set; }
        public Solution(string fullPathName) : base(AppEnvironment.Settings.MsBuildPath)
        {
            FullPathName = fullPathName;
        }

        public Solution(string root, string solutionName) : base(AppEnvironment.Settings.MsBuildPath)
        {
            FullPathName = String.Format(@"{0}\src\{1}\{1}.sln", root, solutionName);
        }

        /// <summary>
        /// Build solution synchroniously
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="executableName"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public void BuildAsync(string outputPath)
        {
            InitializeOutputBuilder();
            var args = String.Format(@"/property:OutputPath=""{0}"" ""{1}""", outputPath, FullPathName);
            RunAsync(args);
        }

    }

}
