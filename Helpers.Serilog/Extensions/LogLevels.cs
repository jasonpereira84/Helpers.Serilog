using System;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using global::Serilog.Events;

    namespace Extensions
    {
        public static partial class Serilog
        {
            public static Helpers.Serilog.LogLevels AsLogLevels(this IDictionary<String, String> dictionary, LogEventLevel defaultValue = LogEventLevel.Warning)
            {
                LogEventLevel _parse(String s) 
                    => Enum.TryParse(s, true, out LogEventLevel l) ? l : defaultValue;

                var levels = new Helpers.Serilog.LogLevels
                {
                    Default = defaultValue,
                    Overrides = new Dictionary<String, LogEventLevel>()
                };
                foreach (var pair in dictionary)
                {
                    if (String.Equals(pair.Key, "Default", StringComparison.OrdinalIgnoreCase))
                        levels.Default = _parse(pair.Value);
                    else
                        levels.Overrides.Add(pair.Key, _parse(pair.Value));
                }
                return levels;
            }

        }
    }
}