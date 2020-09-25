using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using global::Serilog.Events;

    internal static partial class _internalHelpers
    {
        public static String SingleQuoted(this String value)
            => $"'{value}'";

        public static String EscapeSingleQuotes(this String value)
            => value.Replace("'", "''");

        public static (Boolean IsSane, String Value) EvaluateSanity<TSource>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, String> ifSane_valueValue, Func<IEnumerable<TSource>, String> ifNotSane_valueValue)
            => (source?.Any() ?? false)
                ? (IsSane: true, Value: ifSane_valueValue.Invoke(source))
                : (IsSane: false, Value: ifNotSane_valueValue.Invoke(source));

        public static String Interpolate(this String format, String table, DateTimeOffset timestamp, LogEventLevel level, String messageTemplate, Exception exception, (String Name, String Value)[] parameters)
            => format
                .Replace("{Table}", table)
                .Replace("{Timestamp}", timestamp.ToString("yyyy-MM-dd HH:mm:ss.fffzzz").SingleQuoted())
                .Replace("{Level}", level.ToString().SingleQuoted())
                .Replace("{MessageTemplate}", messageTemplate.SanitizeTo("message template").SingleQuoted())
                .Replace("{Exception}", exception,
                        x => EvaluateSanity(x?.ToString()),
                        ifNotSane_valueValue: xs => "NULL",
                        ifSane_valueValue: xs => xs.SingleQuoted())
                .Replace("{Properties}", parameters,
                        @params => EvaluateSanity(@params, src => String.Join(", ", src.Select(pair => $"\"{pair.Name}\":{pair.Value}")), src => "NULL"),
                        ifNotSane_valueValue: @params => "{}".SingleQuoted(),
                        ifSane_valueValue: @params => ($"{{{@params}}}").SingleQuoted());
    }
}
