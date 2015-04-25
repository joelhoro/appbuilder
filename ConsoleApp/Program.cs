using AppRunner.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            solution.Build(@"C:\temp\dummy", "DummyExe", true);
        }

        static void Main(string[] args)
        {

            var fileName = @"c:\users\joel\documents\visual studio 2013\Projects\DummyExe\DummyExe\bin\Debug\DummyExe.exe";
            var executable = new Executable { FileName = fileName };
            executable.OutputChanged += (s, e) => Console.WriteLine(e.Data);
            executable.Run();
            executable.Run();

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
