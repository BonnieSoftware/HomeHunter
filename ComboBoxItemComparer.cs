using System.Collections;
using System.Runtime.InteropServices;

namespace CefSharp.MinimalExample.WinForms
{
    public class ComboBoxItemComparer : IComparer
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern int StrCmpLogicalW(string x, string y);

        public int Compare(object x, object y)
        {
            return StrCmpLogicalW(x.ToString(), y.ToString());
        }
    }
}
