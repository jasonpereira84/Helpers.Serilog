using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using global::Serilog.Events;

    namespace Serilog
    {
        public class LogLevels
        {
            public LogEventLevel Default { get; set; }

            public Dictionary<String, LogEventLevel> Overrides { get; set; }

            public static LogLevels From(LogEventLevel @default, Dictionary<String, LogEventLevel> overrides)
                => new LogLevels
                {
                    Default = @default,
                    Overrides = overrides
                };
        }
    }

    namespace Extensions
    {
        public static partial class Serilog
        {
            public static LogEventLevel Parse(this String @string, LogEventLevel defaultValue)
                => Enum.TryParse(@string, true, out LogEventLevel logEventLevel) ? logEventLevel : defaultValue;

            public static Helpers.Serilog.LogLevels ToLogLevels(this Dictionary<String, String> dictionary, LogEventLevel defaultValue)
            {
                var levels = Helpers.Serilog.LogLevels.From(defaultValue, new Dictionary<String, LogEventLevel>());
                foreach (var pair in dictionary)
                {
                    if (String.Equals(pair.Key, "Default", StringComparison.OrdinalIgnoreCase))
                        levels.Default = Parse(pair.Value, defaultValue);
                    else
                        levels.Overrides.Add(pair.Key, Parse(pair.Value, defaultValue));
                }
                return levels;
            }
        }
    }
}