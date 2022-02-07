using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JasonPereira84.Helpers.Serilog.Tests
{
    using global::Serilog.Events;

    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        [TestClass]
        public class Test_LogLevels
        {
            [TestMethod]
            public void AsLogLevels()
            {
                var defaultValue = LogEventLevel.Debug;

                {
                    var @default = LogEventLevel.Information;
                    var pairs = Enumerable.Range(0, 6)
                        .Select(x => (x, (LogEventLevel)x))
                        .ToDictionary(x => x.Item1, x => x.Item2);
                    var dictionary = new Dictionary<String, String>
                    {
                        { "Default", $"{@default}" },
                    };
                    foreach (var pair in pairs)
                        dictionary.Add($"{pair.Key}", $"{pair.Value}");

                    var retVal = dictionary.AsLogLevels(defaultValue);
                    Assert.AreEqual(
                        expected: @default,
                        actual: retVal.Default);
                    foreach(var pair in pairs)
                    {
                        var key = $"{pair.Key}";
                        Assert.IsTrue(retVal.Overrides.ContainsKey(key));
                        Assert.AreEqual(
                            expected: pair.Value,
                            actual: retVal.Overrides[key]);
                    }
                    
                }

                {
                    {
                        var @default = LogEventLevel.Information;
                        var dictionary = new Dictionary<String, String>
                        {
                            { "Default", $"{@default}" },
                        };

                        var retVal = dictionary.AsLogLevels(defaultValue);
                        Assert.AreEqual(
                            expected: @default,
                            actual: retVal.Default);
                    }

                    {
                        var @default = LogEventLevel.Information;
                        var dictionary = new Dictionary<String, String>
                        {
                            { "default", $"{@default}" },
                        };

                        var retVal = dictionary.AsLogLevels(defaultValue);
                        Assert.AreEqual(
                            expected: @default,
                            actual: retVal.Default);
                    }

                    {
                        var key = "a";
                        var dictionary = new Dictionary<String, String>
                        {
                            { key, $"{LogEventLevel.Information}" },
                        };

                        var retVal = dictionary.AsLogLevels(defaultValue);
                        Assert.AreEqual(
                            expected: defaultValue,
                            actual: retVal.Default);
                        Assert.IsTrue(retVal.Overrides.ContainsKey(key));
                    }

                }

                {
                    var key = "a";
                    var dictionary = new Dictionary<String, String>
                        {
                            { key, "SomeValue" },
                        };

                    var retVal = dictionary.AsLogLevels(defaultValue);
                    Assert.IsTrue(retVal.Overrides.ContainsKey(key));
                    Assert.AreEqual(
                        expected: defaultValue,
                        actual: retVal.Overrides[key]);
                }

            }

        }
    }
}
