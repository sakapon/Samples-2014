using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationConsole
{
    static class Program
    {
        static readonly Polynomial x = Polynomial.X;
        static readonly Polynomial x2 = x ^ 2;

        static void Main(string[] args)
        {
            LinearEquationTest();
            QuadraticEquationTest();

            IntersectionTest();
            PointsOnLineTest();
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

        static void PointsOnLineTest()
        {
            var p1 = new Point2D(0, -300);
            var p2 = new Point2D(1800, 300);
            var y_to_x = GetFunc_y_to_x(p1, p2);
            Console.WriteLine(y_to_x(0));
            Console.WriteLine(y_to_x(-100));
        }

        // P1, P2 を通る直線上で、指定された y 座標に対応する x 座標を求めるための関数。
        static Func<double, double> GetFunc_y_to_x(Point2D p1, Point2D p2)
        {
            // P1 (x1, y1) および P2 (x2, y2) を通る直線の方程式:
            // (x - x1) (y2 - y1) - (x2 - x1) (y - y1) = 0
            return y => ((x - p1.X) * (p2.Y - p1.Y) - (p2.X - p1.X) * (y - p1.Y)).SolveLinearEquation();
        }
    }

    struct Point2D
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point2D(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }
    }
}
