using System;

namespace JasonPereira84.Helpers.Serilog
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensitiveDataAttribute : Attribute
    {
    }
}
