using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

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