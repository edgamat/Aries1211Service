using System;

namespace UnitTests.Aries1211.Domain
{
    public static class StringExtensions
    {
        public static bool IsMissing(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsNotMissing(this string input)
        {
            return !input.IsMissing();
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (maxLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength), "Must be a non-negative integer");

            if (value == null) return null;

            return value.Length <= maxLength
                ? value
                : value[..maxLength];
        }

        public static string ToYesNo(this bool value)
        {
            return value ? "Y" : "N";
        }

        public static string ToYesNo(this bool? value)
        {
            return value?.ToYesNo();
        }
    }
}