using System;
using System.Text;

namespace ParserAPI.Core.Extensions
{
    internal static class StringTransformationExtension
    {       
        public static string RemoveSpecialCharacters(this string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == ' '))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static bool CaseInsensitiveContains(this string text, string value, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
    }
}
