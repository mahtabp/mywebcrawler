using System;
namespace Crawler.Services
{
    public static class StringExtension
    {
        public static string NormalizeString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return str.ToLower().Replace("<b>", "").Replace("</b>", "");
        }
    }
}
