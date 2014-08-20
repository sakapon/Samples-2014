## C# で形式的検証

C# では、コンパイル時に静的にコードをチェックし、警告やエラーを通知してくれます。
例えば、次のようなものです。

![Warning-Unreachable](Images/Warning-Unreachable.png)

![Error-TypeConstrait](Images/Error-TypeConstrait.png)

現時点のコンパイラもそれなりに強力な判断能力を持っているとは思いますが、
もし C# のコンパイラがさらに空気を読んで賢くなったら、
どのようなプログラミング エクスペリエンスを実現できるのかを考えてみます。

実装のお題として「与えられた 2 つの整数を昇順に並べ替えるメソッド」という単純なものを設定し、
どうすればバグのないプログラミングができるかを考えます。

まず、並べ替えを満たす条件は次の通りです。

* 結果の値が意図した順番に並んでいる
* 変換前の値のセットと変換後の値のセットが同じである

T.B.D.

#### 作成したサンプル
[SortConsole](https://github.com/sakapon/Samples-2014/blob/master/VerificationSample/SortConsole/Program.cs) (GitHub)  
[MathConsole](https://github.com/sakapon/Samples-2014/blob/master/VerificationSample/MathConsole/Program.cs) (GitHub)

#### 参照
[プログラミング Coq](http://www.iij-ii.co.jp/lab/techdoc/coqt/)  
[証明駆動開発入門](http://www.iij-ii.co.jp/lab/techdoc/coqt/coqt8.html)
