using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public class ToolStripItemComparer : IComparer
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern int StrCmpLogicalW(string x, string y);

        public int Compare(object x, object y)
        {
            var oItem1 = (ToolStripItem)x;
            var oItem2 = (ToolStripItem)y;

            return StrCmpLogicalW(oItem1.Text, oItem2.Text);
        }
    }
}
