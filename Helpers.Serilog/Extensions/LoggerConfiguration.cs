using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using global::Serilog.Events;
    using LoggerConfiguration = global::Serilog.LoggerConfiguration;
    using LoggerMinimumLevelConfiguration = global::Serilog.Configuration.LoggerMinimumLevelConfiguration;

    namespace Extensions
    {
        public static partial class Serilog
        {
            public static LoggerConfiguration OverrideMinimumLevelFor(this LoggerConfiguration loggerConfiguration, Dictionary<String, LogEventLevel> minimumLevels)
            {
                if (minimumLevels == null)
                    return loggerConfiguration;

                if (!minimumLevels.Any())
                    return loggerConfiguration;

                var retVal = loggerConfiguration;
                foreach (var pair in minimumLevels)
                    retVal = loggerConfiguration.MinimumLevel.Override(pair.Key, pair.Value);
                return retVal;
            }
        }
    }
}