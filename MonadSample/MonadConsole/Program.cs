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

            var r2 = Add(1, 2);
            var r3 = Add(2, 1);
            var r4 = Add(1, Maybe<int>.None);
        }

        static Maybe<int> Add(Maybe<int> x, Maybe<int> y)
        {
            return
                from _x in x
                from _y in y
                where _x < _y
                select _x + _y;
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
