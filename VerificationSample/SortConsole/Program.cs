using System;

namespace SortConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Sort(new TwoValues(1, 1)));
            Console.WriteLine(Sort(new TwoValues(1, 2)));
            Console.WriteLine(Sort(new TwoValues(2, 1)));
        }

        // Point: 戻り値に対する高度な制約。
        static OrderedTwoValues Sort(TwoValues v) // where Sort(v).SetEquals(v)
        {
            // Point: 変数の大小関係などの高度なコンテキスト。
            // コンパイルが成功すれば、このメソッドの実装も成功です。
            return v.X <= v.Y
                ? new OrderedTwoValues(v.X, v.Y)
                : new OrderedTwoValues(v.Y, v.X);
        }
    }

    public class TwoValues
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public TwoValues(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool SetEquals(TwoValues v)
        {
            return X == v.X ? Y == v.Y
                : X == v.Y && Y == v.X;
        }

        public override string ToString()
        {
            return string.Format("{{{0}, {1}}}", X, Y);
        }
    }

    public class OrderedTwoValues : TwoValues
    {
        // Point: 引数に対する高度な制約。
        public OrderedTwoValues(int x, int y)
            : base(x, y) // where x <= y
        {
        }
    }
}
