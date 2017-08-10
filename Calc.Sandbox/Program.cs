using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calc.PositionalSystem;
using System.Diagnostics;

namespace Calc.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var conv = new BaseConverter();

            var timer = new Stopwatch();

            int reps = 10000;

            timer.Start();
            for(int i = 0; i < reps; i++)
            {
                var num = conv.ToBase(i, 16);
            }
            timer.Stop();

            System.Console.WriteLine(reps.ToString() + " conversions took " + timer.ElapsedMilliseconds.ToString());
            System.Console.ReadLine();
         }
    }
}
