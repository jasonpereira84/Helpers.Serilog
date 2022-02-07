using System;

namespace JasonPereira84.Helpers
{
    namespace Serilog
    {
        public class CommonDatabase
        {
            public String Host { get; set; }

            public String Secret { get; set; }

            public Nullable<UInt16> Port { get; set; }
        }
    }
}