using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MonadTest();
            MaybeTest();
            FlowTest();
        }

        static void MonadTest()
        {
            var r1 =
                from x in 0.ToMonad()
                select x;

            var r2 =
                from x in 1.ToMonad()
                from y in 2.ToMonad()
                select x + y;
        }

        static void MaybeTest()
        {
            var r1 =
                from x in 2.ToMaybe()
                where x % 3 == 1
                select x + 1;

            var r2 =
                from x in 1.ToMaybe()
                from y in 2.ToMaybe()
                where x % 3 == 1
                select x + y;
        }

        static void FlowTest()
        {
            var r1 =
                from x in 1.ToFlow()
                from y in 0.ToFlow()
                select x / y;
        }
    }
}
