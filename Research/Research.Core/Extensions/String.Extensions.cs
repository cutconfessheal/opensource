using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Research.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Concat(this string[] input, string addBefore, string addAfter)
        {
            string output = string.Empty;
            if (input != null && input.Count() > 0)
                output =
                    input.Aggregate<string, StringBuilder>(new StringBuilder(),
                        (x, y) => x.AppendFormat("{0}{1}{2}", addBefore ?? string.Empty, y, addAfter ?? string.Empty))
                            .ToString();
            if (!string.IsNullOrEmpty(addAfter) && output.EndsWith(addAfter))
                output = output.Substring(0, (output.Length - addAfter.Length));
            return output;
        }

        public static string Concat(this int length, string prefix)
        {
            string output = string.Empty;
            string[] values = new string[length];
            for (int i = 0; i < length; ++i)
                values[i] = (i + 1).ToString();
            output = values.Concat(prefix, ",");
            return output;
        }
    }
}
