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

        public BuildResults Build(string outputPath, string executableName, bool verbose = false)
        {
            var args = String.Format(@"/property:OutputPath=""{0}"" ""{1}""", outputPath, FullPathName);

            string output;
            if (verbose)
                output = Shell.RunMSBuild(args, l => Console.WriteLine(l));
            else
                output = Shell.RunMSBuild(args);
            return BuildResults.FromOutput(output);
        }

    }

}
