using System.IO;
using System.Linq;
using AppRunner.Models;
using AppRunner.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void TestBuild()
        {
            var solutionName = @"c:\Users\Joel\Documents\Visual Studio 2013\Projects\DummyExe\DummyExe.sln";
            var solution = new Solution(solutionName);
            //Console.WriteLine(solution.Build(@"C:\temp\dummy", verbose: true));
        }

        static void TestSettings()
        {
            var fileName = @"C:\temp\settings.json";
            var settings = new UserSettings();
            //settings.Applications = new List<ApplicationVM>() {
            //    new ApplicationVM { WorkArea = @"C:\dev\p4v", Executable = "vNextApp", CommandLineArgs = "MigrateStrategy strategy=28787"  }
            //};
            //settings.Workspaces = new List<string>() { @"C:\dev\p4v", @"E:\dev\p4v" };

            //AppEnvironment.Settings = settings;
            //AppEnvironment.LoadSettings(fileName);
            //AppEnvironment.SaveSettings(fileName);

        }

        static void Main(string[] args)
        {
            FileSystem.Initialize(AppEnvironment.Settings.Workspaces, true);
            var path = @"c:\Users\Joel\Documents\Visual Studio 2013\Projects";
            var x = FileSystem.FileList(path)
                .Select(f => f.Replace(path+@"\", ""));

            //TestMode();
            //TestBuild();
            Console.WriteLine("Done");
            Console.ReadKey();



        }



    }
}
