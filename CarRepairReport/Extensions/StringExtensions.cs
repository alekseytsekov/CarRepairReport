namespace CarRepairReport.Extensions
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string ToCapital(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            var tokens = Regex.Split(str, "\\s");

            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = Char.ToUpper(tokens[i][0]) + tokens[i].Substring(1); 
            }

            var result = string.Join(" ", tokens);

            return result;
        }
    }
}