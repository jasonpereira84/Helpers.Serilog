using System;

namespace JasonPereira84.Helpers
{
    internal static partial class _internalHelpers
    {
        public static String SafeInvoke<T>(this Func<T, String> stringifier, T t)
        {
            String _def() => t != null ? t.ToString() : t.GetType().FullName;

            try
            {
                if (stringifier == null)
                    return _def();

                return stringifier.Invoke(t);
            }
            catch (Exception)
            {
                return _def();
            }
        }

        public static Func<B, A, O> FlipInvoke<A, B, O>(this Func<A, B, O> func)
            => new Func<B, A, O>((b, a) => func.Invoke(a, b));
        public static Action<B, A> FlipInvoke<A, B>(this Action<A, B> action)
            => new Action<B, A>((b, a) => action.Invoke(a, b));

        public static Func<T1, T2, O> SingleInvoke<T1, T2, O>(this Func<T1, O> func)
            => new Func<T1, T2, O>((t1, t2) => func.Invoke(t1));
        public static Action<T1, T2> SingleInvoke<T1, T2>(this Action<T1> action)
            => new Action<T1, T2>((t1, t2) => action.Invoke(t1));


        public static Func<T1, T2, O> NoneInvoke<T1, T2, O>(this Func<O> func)
            => new Func<T1, T2, O>((t1, t2) => func.Invoke());
        public static Action<T1, T2> NoneInvoke<T1, T2>(this Action action)
            => new Action<T1, T2>((t1, t2) => action.Invoke());

        public static Func<T, O> NoneInvoke<T, O>(this Func<O> func)
            => new Func<T, O>((t) => func.Invoke());
        public static Action<T> NoneInvoke<T>(this Action action)
            => new Action<T>((t) => action.Invoke());
    }
}
