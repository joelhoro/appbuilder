using AppRunner.Models;
using AppRunner.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void TestBuild()
        {
            var solutionName = @"c:\Users\Joel\Documents\Visual Studio 2013\Projects\DummyExe\DummyExe.sln";
            var solution = new Solution(solutionName);
            solution.Build(@"C:\temp\dummy", "DummyExe",verbose: false);
        }

        static void TestSettings()
        {
            var fileName = @"C:\temp\settings.json";
            var settings = new UserSettings();
            //settings.Applications = new List<ApplicationViewModel>() {
            //    new ApplicationViewModel { WorkArea = @"C:\dev\p4v", Executable = "vNextApp", CommandLineArgs = "MigrateStrategy strategy=28787"  }
            //};
            //settings.Workspaces = new List<string>() { @"C:\dev\p4v", @"E:\dev\p4v" };

            //AppEnvironment.Settings = settings;
            //AppEnvironment.LoadSettings(fileName);
            //AppEnvironment.SaveSettings(fileName);

        }

        static void Main(string[] args)
        {
            TestSettings();
            Console.WriteLine("Done");
            Console.ReadKey();

            //var fileName = @"c:\users\joel\documents\visual studio 2013\Projects\DummyExe\DummyExe\bin\Debug\DummyExe.exe";
            //var executable = new Executable(fileName);
            //executable.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
            //executable.Run();
            //executable.Run();

            //while (true)
            //{
            //    Console.WriteLine("Waiting for input");
            //    var k = Console.ReadKey();
            //    if (k.KeyChar == 'x')
            //        executable.Abort();
            //}

        }



    }
}
