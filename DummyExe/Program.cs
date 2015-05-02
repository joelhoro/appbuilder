using System;
using System.Collections.Generic;
using System.Linq;
using Process = System.Diagnostics.Process;
using Thread = System.Threading.Thread;
using AppRunner.Utilities;

namespace DummyExe
{
    class Program
    {
        private static void Main(string[] args)
        {
            var duration = int.Parse(args[0]);
            var name = args[1];
            var PID = Process.GetCurrentProcess().Id;

            var startTime = DateTime.Now;
            var i = 0;
            var random = new Random();
            Func<double> runTime = () => (DateTime.Now - startTime).TotalSeconds;
            while (runTime() < duration)
            {
                var progress = GetProgress(runTime()/duration);
                Console.WriteLine("{2} [{0}-{3}]  line {1:d5} ", name, i, progress, PID);
                double interval;
                if ((i++%15) == 0)
                {
                    Console.WriteLine("Cmdline: {0}", Environment.GetCommandLineArgs().Skip(1).ToList().Join(" "));
                    interval = 2000;
                }
                else
                    interval = Math.Round(Math.Exp(-10*random.NextDouble())*1000);
                interval = 10;
                Thread.Sleep((int) interval);
            }
        }

        private static string GetProgress(double p)
        {
            p = Math.Min(1, Math.Max(0, p));
            var fraction = (int)Math.Floor(p * 10);
            return "|" + String.Join("",
              Enumerable
                  .Range(0, 10)
                  .Select(i => i == fraction ? "o" : "-")) + "| " + String.Format("{0:d2}%",(int)(p*100));
        }
    }
}
