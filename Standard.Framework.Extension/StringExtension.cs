using System;

namespace Standard.Framework.Extension
{
    public static class StringExtension
    {
        public static string RemoveCpfMask(this string input)
        {
            return input.Replace(".", string.Empty).Replace("-", string.Empty);
        }
    }
}
