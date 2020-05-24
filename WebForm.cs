using CefSharp.WinForms;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public abstract partial class WebForm : Form
    {
        protected readonly ChromiumWebBrowser browser;
        protected readonly string blankPageAddress = "about:blank";

        protected WebForm(string url)
        {
            InitializeComponent();

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Substring(6);

            browser = new ChromiumWebBrowser(path + "\\" + url);
            Controls.Add(browser);

            browser.FrameLoadEnd += OnBrowserFrameLoadEnd;
        }

        protected virtual void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            // Do nothing.
        }

        // Text fields
        protected void SetInputValue(string formInputId, string value)
        {
            if (value == null) return;
            browser.ExecuteScriptAsync("$('#" + formInputId + "').val('" + value.Replace("'", @"\'") + "');");
        }

        // Select fields
        protected void SetInputValue(string formInputId, int value)
        {
            browser.ExecuteScriptAsync("$('#" + formInputId + "').val('" + value.ToString() + "');");
        }

        // Checkboxes.
        protected void SetInputValue(string formInputId, bool value)
        {
            var jsValue = value ? "true" : "false";
            browser.ExecuteScriptAsync("$('#" + formInputId + "').prop('checked', " + jsValue + ")");
        }
    }
}
