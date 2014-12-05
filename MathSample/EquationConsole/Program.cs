using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PolynomialTest();
        }

        static void PolynomialTest()
        {
            var x = Polynomial.X;

            var p1 = x + 2;
            var p2 = x - 2;

            var p3 = p1 + p2;
            var p4 = p1 - p2;
            var p5 = p1 * p2;
            var p6 = p1 / 2;

            Console.WriteLine(((Polynomial)0)[5]);
            Console.WriteLine(p1[5]);
            Console.WriteLine(p5[5]);
        }
    }
}
