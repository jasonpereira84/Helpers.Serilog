using System;

namespace JasonPereira84.Helpers
{
    internal static partial class _internalHelpers
    {
        public static String Replace(this String format, String valueName, String valueValue)
            => format.Replace(valueName, valueValue);

        public static String Replace(this String format, String valueName, (Boolean IsSane, String Value) valueValue_EVAL, Func<String, String> ifSane_valueValue, Func<String, String> ifNotSane_valueValue)
            => Replace(format, valueName, valueValue_EVAL.IsSane ? ifSane_valueValue(valueValue_EVAL.Value) : ifNotSane_valueValue(valueValue_EVAL.Value));

        public static String Replace<TValue>(this String format, String valueName, TValue value, Func<TValue, (Boolean IsSane, String Value)> sanitizingStringifier, Func<String, String> ifSane_valueValue, Func<String, String> ifNotSane_valueValue)
            => Replace(format, valueName, sanitizingStringifier.Invoke(value), ifSane_valueValue, ifNotSane_valueValue);
    }
}
