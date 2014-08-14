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
            NaturalNumber n = 1; // OK
            NaturalNumber m = -1; // Error

            Console.WriteLine(n + m);
        }
    }

    public class NaturalNumber
    {
        int i;

        public NaturalNumber(int i) // where i > 0
        {
            this.i = i;
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
