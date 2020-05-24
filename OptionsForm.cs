using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class OptionsForm : Form
    {
        private static bool? _DebuggingEnabled = null;
        public static bool DebugingEnabled
        {
            get
            {
                if (_DebuggingEnabled == null)
                {
                    var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\HomeHunter");
                    _DebuggingEnabled = Convert.ToBoolean(key.GetValue("DebugingEnabled"));
                }
                return (bool)_DebuggingEnabled;
            }
            private set
            {
                _DebuggingEnabled = value;
                var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\HomeHunter", true);
                key.SetValue("DebugingEnabled", _DebuggingEnabled);
            }
        }
        public static string DataDirectory { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HomeHunter");

        public OptionsForm()
        {
            InitializeComponent();
            checkBox1.Checked = DebugingEnabled;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DebugingEnabled = checkBox1.Checked;
        }
    }
}
