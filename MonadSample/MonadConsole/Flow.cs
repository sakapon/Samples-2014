using System;

namespace MonadConsole
{
    public class Flow<T>
    {
        T _value;

        public T Value
        {
            get
            {
                if (HasError) throw new InvalidOperationException();
                return _value;
            }
        }

        Exception _error;

        public Exception Error
        {
            get
            {
                if (!HasError) throw new InvalidOperationException();
                return _error;
            }
        }

        public bool HasError { get; private set; }

        public Flow(T value)
        {
            _value = value;
        }

        public Flow(Exception error)
        {
            _error = error;
            HasError = true;
        }

        public static explicit operator T(Flow<T> value)
        {
            return value.Value;
        }

        public static implicit operator Flow<T>(T value)
        {
            return new Flow<T>(value);
        }

        public Flow<TResult> Bind<TResult>(Func<T, Flow<TResult>> func)
        {
            if (HasError) return new Flow<TResult>(_error);

            try
            {
                return func(_value);
            }
            catch (Exception ex)
            {
                return new Flow<TResult>(ex);
            }
        }
    }

    public static class Flow
    {
        public static Flow<T> ToFlow<T>(this T value)
        {
            return value;
        }

        public static Flow<TResult> Select<T, TResult>(this Flow<T> flow, Func<T, TResult> selector)
        {
            return flow.Bind(v => selector(v).ToFlow());
        }

        public static Flow<TResult> SelectMany<T, U, TResult>(this Flow<T> flow, Func<T, Flow<U>> selector, Func<T, U, TResult> resultSelector)
        {
            var selected = flow.Bind(selector);
            if (selected.HasError) return new Flow<TResult>(selected.Error);

            try
            {
                return resultSelector((T)flow, (U)selected);
            }
            catch (Exception ex)
            {
                return new Flow<TResult>(ex);
            }
        }
    }
}
