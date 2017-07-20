using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calc.PositionalSystem;
namespace Calc.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var num = "-30.0";
            var num2 = "-10.0";

            var test1 = BaseConverter.ConvertToBase(num, 10 ,10);
            var test2 = BaseConverter.ConvertToBase(num2, 10, 10);

            var test3 = test1 / test2;

            System.Console.WriteLine(test3.DecimalValue);
            System.Console.ReadLine();
         }
    }
}
