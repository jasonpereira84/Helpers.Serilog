using System;
using System.Collections.Generic;
using System.Reflection;
using Serilog.Core;
using Serilog.Events;
using System.Linq;

#nullable enable

namespace JasonPereira84.Helpers
{
    namespace Serilog
    {
        public class SensitiveDataDestructuringPolicy : IDestructuringPolicy
        {
            public Boolean TryDestructure(Object value, ILogEventPropertyValueFactory factory, out LogEventPropertyValue? result)
            {
                // Avoid infinite recursion by checking if we are already destructuring
                var type = value.GetType();
                var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Create a list of log properties, masking those with the attribute
                var logProperties = props.Select(p => {
                    var val = p.GetValue(value);
                    if (p.GetCustomAttribute<SensitiveDataAttribute>() != null)
                        return new LogEventProperty(p.Name, new ScalarValue("***MASKED***"));

                    return new LogEventProperty(p.Name, factory.CreatePropertyValue(val, true));
                });

                result = new StructureValue(logProperties, type.Name);
                return true;
            }
        }
    }
}
