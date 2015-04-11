using System;
using System.Globalization;
using JetBrains.Annotations;

namespace Selkie.Windsor.Extensions
{
    public static class StringExtensions
    {
        [NotNull]
        [StringFormatMethod("format")]
        public static string Inject([NotNull] this string format,
                                    [NotNull] params object[] arguments)
        {
            return String.Format(CultureInfo.CurrentCulture,
                                 format,
                                 arguments);
        }
    }
}