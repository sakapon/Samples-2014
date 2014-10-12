using System;
using System.Diagnostics;

namespace MonadConsole
{
    /// <summary>
    /// The simplest monad class.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    [DebuggerDisplay(@"\{{Value}\}")]
    public class Monad<T>
    {
        public T Value { get; private set; }

        // Requirement for monad 1: Unit.
        public Monad(T value)
        {
            Value = value;
        }

        // Requirement for monad 2: Bind.
        public Monad<TResult> Bind<TResult>(Func<T, Monad<TResult>> func)
        {
            return func(Value);
        }

        public Monad<TResult> Map<TResult>(Func<T, TResult> mapping)
        {
            return mapping(Value);
        }

        public static explicit operator T(Monad<T> value)
        {
            return value.Value;
        }

        public static implicit operator Monad<T>(T value)
        {
            return new Monad<T>(value);
        }
    }

    public static class Monad
    {
        public static Monad<T> ToMonad<T>(this T value)
        {
            return value;
        }

        public static Monad<TResult> Select<T, TResult>(this Monad<T> monad, Func<T, TResult> selector)
        {
            return monad.Map(selector);
        }

        public static Monad<TResult> SelectMany<T, TResult>(this Monad<T> monad, Func<T, Monad<TResult>> selector)
        {
            return monad.Bind(selector);
        }

        public static Monad<TResult> SelectMany<T, U, TResult>(this Monad<T> monad, Func<T, Monad<U>> selector, Func<T, U, TResult> resultSelector)
        {
            return resultSelector((T)monad, (U)monad.Bind(selector));
        }
    }
}
