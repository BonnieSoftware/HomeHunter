using System;
using System.Collections.Generic;
using System.Linq;

namespace CefSharp.MinimalExample.WinForms
{
    public static class WebInput
    {
        public static List<string> ToList(object value)
        {
            if (value == null) return new List<string>();
            return value.ToString().Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static string ToWebString(List<string> list)
        {
            return string.Join(@"\n", list);
        }

        public static string ToRegistryString(List<string> list)
        {
            return string.Join("\n", list);
        }

        public static int ToInt32(object value)
        {
            return string.IsNullOrEmpty(value.ToString()) ? -1 : Convert.ToInt32(value.ToString());
        }
    }
}
