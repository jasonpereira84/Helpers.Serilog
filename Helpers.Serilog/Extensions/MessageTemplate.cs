using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Serilog
        {
            public static String ReplaceParameters(this String messageTemplate, params (String Name, String Value)[] parameters)
            {
                var message = messageTemplate;
                foreach (var parameter in parameters)
                {
                    message = message.Replace(
                        oldValue: $"{{{parameter.Name}}}",
                        newValue: parameter.Value.StartsWith('"') && parameter.Value.EndsWith('"')
                            ? parameter.Value.Substring(1, parameter.Value.Length - 2)
                            : parameter.Value);
                }
                return message;
            }
        }
    }
}