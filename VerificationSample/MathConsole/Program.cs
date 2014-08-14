/* 
 * このプログラムには、架空の機能が含まれます。
 */
using System;

namespace MathConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            NaturalNumber n = 3; // OK
            NaturalNumber m = -1; // Compilation Error

            Console.WriteLine(Divide(1, n)); // OK
            Console.WriteLine(Divide(1, 0)); // Compilation Error
        }

        static double Divide(double d, double divisor) // where divisor != 0
        {
            return d / divisor;
        }
    }

    public class NaturalNumber
    {
        int i;

        public NaturalNumber(int i) // where i > 0
        {
            this.i = i;
        }

        public override string ToString()
        {
            return i.ToString();
        }

        public static implicit operator int(NaturalNumber n)
        {
            return n.i;
        }

        public static implicit operator NaturalNumber(int i)
        {
            return new NaturalNumber(i);
        }
    }
}
