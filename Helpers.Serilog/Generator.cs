using System;

namespace JasonPereira84.Helpers
{
    using global::Serilog.Events;

    namespace Serilog
    {
        public static class Generator
        {
            public delegate String ConnectionString<TDatabase>(TDatabase database) where TDatabase : AppSettings.Database._Base;

            public delegate String CommandText(DateTimeOffset timestamp, LogEventLevel level, String messageTemplate, Exception exception, (String Name, String Value)[] parameters);
        }
    }
}