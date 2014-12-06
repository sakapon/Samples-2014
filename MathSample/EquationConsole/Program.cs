using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationConsole
{
    class Program
    {
        static readonly Polynomial x = Polynomial.X;
        static readonly Polynomial x2 = x ^ 2;

        static void Main(string[] args)
        {
            LinearEquationTest();
            QuadraticEquationTest();

            IntersectionTest();
        }

        static void LinearEquationTest()
        {
            Console.WriteLine(x.SolveLinearEquation());
            Console.WriteLine((x - 2).SolveLinearEquation());
            Console.WriteLine((2 * x + 1).SolveLinearEquation());
        }

        static void QuadraticEquationTest()
        {
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

        static void IntersectionTest()
        {
            // 直線 y = x - 1 と直線 y = -2x + 5 の交点。
            var l1 = x - 1;
            var l2 = -2 * x + 5;
            var p_x = (l1 - l2).SolveLinearEquation();
            var p_y = l1[p_x];
            Console.WriteLine("({0}, {1})", p_x, p_y);
        }
    }
}
