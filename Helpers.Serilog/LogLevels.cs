using System;
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
}