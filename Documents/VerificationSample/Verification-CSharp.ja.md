## もし C# で形式的検証ができたら

C# では、コンパイル時に静的にコードをチェックし、警告やエラーを通知してくれます。
例えば、次のようなものです。

![Warning-Unreachable](Images/Warning-Unreachable.png)

![Error-TypeConstrait](Images/Error-TypeConstrait.png)

現時点のコンパイラもそれなりに強力な判断能力を持っているとは思いますが、
もし C# のコンパイラがさらに空気を読んで賢くなったら、
どのようなプログラミング エクスペリエンスを実現できるのかを考えてみます。

実装のお題として「与えられた 2 つの整数を昇順に並べ替えるメソッド」という単純なものを設定し、
どうすればバグのないプログラミングができるかを考えます。

まず、並べ替えのメソッドが満たすべき条件は次の通りです。

* 実行結果が意図した順番に並んでいる
* 実行前の値のセットと実行後の値のセットが同じである

そこで、メソッドの実装を始める前に、メソッドのシグネチャでこれらの条件を表現してみます。

```c#:Program.cs
/* 
 * このプログラムには、架空の機能が含まれます。
 */
using System;

namespace SortConsole
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        // Point: 戻り値に対する高度な制約。
        static OrderedTwoValues Sort(TwoValues v) where Sort(v).SetEquals(v)
        {
            // TODO: Implementation.
            throw new NotImplementedException();
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
            : base(x, y) where x <= y
        {
        }
    }
}
```

配列やリストの代わりに、TwoValues クラスを定義して 2 つの整数を表現しています。
TwoValues クラスを継承して、順序を保持する OrderedTwoValues クラスを定義します。
コンストラクターにある「where x <= y」は、
静的チェックのレベルで x ≦ y を満たした状態でコンストラクターを呼び出さなければ、
コンパイルがエラーとなることを意味します。

T.B.D.

#### 作成したサンプル
* [SortConsole](https://github.com/sakapon/Samples-2014/blob/master/VerificationSample/SortConsole/Program.cs) (GitHub)
* [MathConsole](https://github.com/sakapon/Samples-2014/blob/master/VerificationSample/MathConsole/Program.cs) (GitHub)

#### 参照
* [形式的検証 - Wikipedia](http://j.mp/e1FGFM)
* [プログラミング Coq](http://www.iij-ii.co.jp/lab/techdoc/coqt/)
* [証明駆動開発入門](http://www.iij-ii.co.jp/lab/techdoc/coqt/coqt8.html)
