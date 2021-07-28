using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    internal static partial class _internalHelpers
    {
        public static Boolean None<TSource>(this IEnumerable<TSource> source)
            => !source.Any();

        public static Boolean IsNullOrNone<TSource>(this IEnumerable<TSource> source)
            => source?.None() ?? true;
    }
}
