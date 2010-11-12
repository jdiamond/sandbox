using System.Text.RegularExpressions;

namespace NaturalSorting
{
    public static class PadNumbersExtension
    {
        private static readonly Regex NumbersRegex = new Regex(@"\d+");

        public static string PadNumbers(this string value, int totalWidth)
        {
            return NumbersRegex.Replace(value, m => m.Value.PadLeft(totalWidth, '0'));
        }
    }
}