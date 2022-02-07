using System;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using global::Serilog.Events;
    using LoggerConfiguration = global::Serilog.LoggerConfiguration;

    namespace Extensions
    {
        public static partial class Serilog
        {
            public static LoggerConfiguration OverrideMinimumLevelFor(this LoggerConfiguration loggerConfiguration, IDictionary<String, LogEventLevel> minimumLevels)
            {
                if (minimumLevels != null)
                    foreach (var pair in minimumLevels)
                        loggerConfiguration.MinimumLevel.Override(pair.Key, pair.Value);

                return loggerConfiguration;
            }
        }
    }
}