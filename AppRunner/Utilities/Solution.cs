using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Build solution synchroniously
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="executableName"></param>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public BuildResults Build(string outputPath, string executableName, bool verbose = false)
        {
            var args = String.Format(@"/property:OutputPath=""{0}"" ""{1}""", outputPath, FullPathName);
            string output;
            if (verbose)
                OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
            output = Shell.RunMSBuild(args, OutputDataReceived, async: false);
            return BuildResults.FromOutput(output);
        }

    }

}
