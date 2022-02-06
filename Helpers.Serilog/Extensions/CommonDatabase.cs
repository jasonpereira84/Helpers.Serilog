using System;
using System.Text;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Serilog
        {
            public static String AsConnectionString_MySql(this Helpers.Serilog.CommonDatabase commonDatabase)
                => new StringBuilder($"Database=common;")
                    .Append($"Server={commonDatabase.Host};Port={commonDatabase.Port ?? 3306};")
                    .Append($"User Id=app.common;Password={commonDatabase.Secret};")
                    .ToString();
        }
    }
}