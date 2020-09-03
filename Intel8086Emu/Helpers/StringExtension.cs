using System.Text.RegularExpressions;

namespace Intel8086Emu.Helpers
{
    public static class StringExtension
    {
        public static bool IsReg(this string text) 
            => new Regex(@"^[a-zA-Z]{2}$").IsMatch(text);

        public static bool IsMem(this string text) 
            => new Regex(
                @"^([dDcCsSeE][sS]\:)?(\[\s?[a-zA-z]{2}(\s?\++\s?[a-zA-Z]{2}|\s?\++\s?[0-9]+[bB]?|\s?\++\s?[0-9a-fA-F]+[hH]{1})*\s?\])")
                .IsMatch(text);
        
        public static bool IsHexValue(this string text) 
            => new Regex(@"^0?[0-9A-Fa-f]*[hH]{1}$").IsMatch(text);

        public static bool IsBinaryValue(this string text)
            => new Regex(@"([0-1]{4}|[0-1]{8})[bB]{1}").IsMatch(text);

        public static bool IsNumber(this string text) 
            => new Regex(@"^[0-9]*$").IsMatch(text);

        public static bool IsImmediate(this string text) 
            => text.IsHexValue() || text.IsBinaryValue() || text.IsNumber();
    }
}
