using CefSharp.MinimalExample.WinForms.Controls;
using CefSharp.WinForms;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class BrowserForm : Form
    {
        private readonly ChromiumWebBrowser _browser;

        private static readonly string _ReportFolderPath = Path.Combine(OptionsForm.DataDirectory, "Reports");

        public BrowserForm()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            menuStrip1.Renderer = new CustomToolStripMenuRenderer();

            _browser = new ChromiumWebBrowser(_blankPageAddress);
            toolStripContainer.ContentPanel.Controls.Add(_browser);

            _browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            _browser.LoadingStateChanged += OnLoadingStateChanged;
            _browser.ConsoleMessage += OnBrowserConsoleMessage;
            _browser.StatusMessage += OnBrowserStatusMessage;
            _browser.TitleChanged += OnBrowserTitleChanged;
            _browser.AddressChanged += OnBrowserAddressChanged;
            _browser.FrameLoadEnd += OnBrowserFrameLoadEnd;
        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);

            this.InvokeOnUiThreadIfRequired(() => b.Focus());
        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            if (OptionsForm.DebugingEnabled)
                DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            SetCanGoBack(args.CanGoBack);
            SetCanGoForward(args.CanGoForward);

            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            //this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            if (!_initialisedAddress)
            {
                _initialisedAddress = true;
                return;
            }

            this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }

        private void SetCanGoBack(bool canGoBack)
        {
            this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }

        private void SetIsLoading(bool isLoading)
        {
            goButton.Text = isLoading ?
                "Stop" :
                "Go";
            goButton.Image = isLoading ?
                Properties.Resources.nav_plain_red :
                Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }

        public void DisplayOutput(string output)
        {
            this.InvokeOnUiThreadIfRequired(() => outputLabel.Text = output);
        }

        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != urlTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            _browser.Dispose();
            Cef.Shutdown();
            Close();
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            LoadUrl(urlTextBox.Text);
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            _browser.Back();
        }

        private void ForwardButtonClick(object sender, EventArgs e)
        {
            _browser.Forward();
        }

        private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            LoadUrl(urlTextBox.Text);
        }

        private void ShowDevToolsMenuItemClick(object sender, EventArgs e)
        {
            _browser.ShowDevTools();
        }

        //
        // Bookmarks
        //

        private void rightmoveHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUrl("https://www.rightmove.co.uk/");
        }

        private void zooplaHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUrl("https://www.zoopla.co.uk/");
        }

        //
        // Tool strip menu item opening actions
        //

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            showDevToolsToolStripMenuItem.Visible = OptionsForm.DebugingEnabled;
            stopCrawlingToolStripMenuItem.Visible = OptionsForm.DebugingEnabled && _crawling == true;
        }

        private void crawlerToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            logsToolStripMenuItem.Visible = OptionsForm.DebugingEnabled;
            runUnitTestsToolStripMenuItem.Visible = OptionsForm.DebugingEnabled;
        }

        //
        // Tool strip menu item actions
        //

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(OptionsForm.DataDirectory);
        }

        private void crawlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var shortlistManager = new ShortlistManagerForm();
            shortlistManager.Show();
        }

        private void aboutHomeHunterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options = new OptionsForm();
            options.Show();
        }

        private void createAShortlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShortlistEditor.Open();
        }

        private void runUnitTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnitTests.Run();
        }

        //TODO: Need a new window that can add a crawl cancellation file or something
        // which the crawler checks for to see if it should continue to crawl or not
        // and then we can also allow the crawler to be paused to help debug.
        private void stopCrawlingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _crawling = false;
        }

        private void shortlistToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            shortlistToolStripMenuItem.DropDownItems.Clear();
            var createToolStripMenuItem = new ToolStripMenuItem("&Create a Shortlist...", null, createAShortlistToolStripMenuItem_Click, "create");
            shortlistToolStripMenuItem.DropDownItems.Add(createToolStripMenuItem);

            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\HomeHunter\Shortlists");

            if (key == null)
                return;

            foreach (string subKeyName in key.GetSubKeyNames())
            {
                using (RegistryKey tempKey = key.OpenSubKey(subKeyName))
                {
                    string shortlistName = tempKey.GetValue("Name").ToString();
                    var toolStripMenuItem = new ToolStripMenuItem(shortlistName, null, DropDownItem_Click, subKeyName);
                    shortlistToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
                }
            }

            ResortToolStripItemCollection(shortlistToolStripMenuItem.DropDownItems);
        }

        private void DropDownItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = (ToolStripMenuItem)sender;

            try
            {
                Crawl(Shortlist.Open(toolStripMenuItem.Name));
            }
            catch (UnexpectedPageFormatException)
            {
                MessageBox.Show("Unable to create a shortlist for this page.");
            }
        }

        private void reportsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            reportsToolStripMenuItem.DropDownItems.Clear();

            string[] files = Directory.GetFiles(_ReportFolderPath);

            if (files.Length < 1)
            {
                var noReportsToolStripMenuItem = new ToolStripMenuItem();
                noReportsToolStripMenuItem.Name = "noReportsToolStripMenuItem";
                noReportsToolStripMenuItem.Text = "No Reports Available";
                noReportsToolStripMenuItem.Enabled = false;
                reportsToolStripMenuItem.DropDownItems.Add(noReportsToolStripMenuItem);
            }
            else
            {
                foreach (string file in files)
                {
                    var reportToolStripMenuItem = new ToolStripMenuItem(Path.GetFileNameWithoutExtension(file), null, reportToolStripMenuItem_Click, file);
                    reportsToolStripMenuItem.DropDownItems.Add(reportToolStripMenuItem);
                }
            }
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            LoadUrl(menuItem.Name);
        }

        private void ResortToolStripItemCollection(ToolStripItemCollection coll)
        {
            var oAList = new ArrayList(coll);
            oAList.Sort(new ToolStripItemComparer());
            coll.Clear();

            foreach (ToolStripItem oItem in oAList)
            {
                coll.Add(oItem);
            }
        }
    }
}
