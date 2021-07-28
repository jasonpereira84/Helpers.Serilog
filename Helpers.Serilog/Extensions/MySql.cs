using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Serilog
        {
            public static class MySql
            {
                public static Helpers.Serilog.Generator.ConnectionString<AppSettings.Database.MySql> DefaultConnectionStringGenerator
                    => Helpers.Serilog.MySql.Context.DefaultConnectionStringGenerator;

                public static Helpers.Serilog.Generator.CommandText DefaultCommandTextGenerator(String format, String table)
                    => Helpers.Serilog.MySql.Context.DefaultCommandTextGenerator(format, table);
                public static Helpers.Serilog.Generator.CommandText DefaultCommandTextGenerator(String table)
                    => Helpers.Serilog.MySql.Context.DefaultCommandTextGenerator(table);
            }
        }
    }
}