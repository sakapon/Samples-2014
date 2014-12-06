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
            LinearEquationTest();
            QuadraticEquationTest();
        }

        static void LinearEquationTest()
        {
            var x = Polynomial.X;

            Console.WriteLine(x.SolveLinearEquation());
            Console.WriteLine((x - 2).SolveLinearEquation());
            Console.WriteLine((2 * x + 1).SolveLinearEquation());
        }

        static void QuadraticEquationTest()
        {
            var x = Polynomial.X;
            var x2 = x ^ 2;

            WriteLine((x2 + 1).SolveQuadraticEquation());
            WriteLine(x2.SolveQuadraticEquation());
            WriteLine((x2 - 6 * x + 9).SolveQuadraticEquation());
            WriteLine(((x + 2) * (2 * x - 1)).SolveQuadraticEquation());
            WriteLine((x2 + x - 1).SolveQuadraticEquation());
        }

        static void WriteLine<T>(IEnumerable<T> source)
        {
            Console.WriteLine(string.Join(", ", source));
        }
    }
}
