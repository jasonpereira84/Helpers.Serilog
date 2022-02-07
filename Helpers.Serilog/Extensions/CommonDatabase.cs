using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Serilog
        {
            public static String AsConnectionString_MySql(this Helpers.Serilog.CommonDatabase commonDatabase)
                => $"Database=common;Server={commonDatabase.Host};Port={commonDatabase.Port ?? 3306};User Id=app.common;Password={commonDatabase.Secret};";
        }
    }
}