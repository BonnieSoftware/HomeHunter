namespace CefSharp.MinimalExample.WinForms
{
    partial class BrowserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.forwardButton = new System.Windows.Forms.ToolStripButton();
            this.urlTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.goButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDevToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopCrawlingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAShortlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightmoveHomepageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zooplaHomepageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crawlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crawlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runUnitTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutHomeHunterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.statusLabel);
            this.toolStripContainer.ContentPanel.Controls.Add(this.outputLabel);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(730, 435);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(730, 466);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusLabel.Location = new System.Drawing.Point(0, 409);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 1;
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.outputLabel.Location = new System.Drawing.Point(0, 422);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(0, 13);
            this.outputLabel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.urlTextBox,
            this.goButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(730, 31);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Layout += new System.Windows.Forms.LayoutEventHandler(this.HandleToolStripLayout);
            // 
            // backButton
            // 
            this.backButton.Enabled = false;
            this.backButton.Image = global::CefSharp.MinimalExample.WinForms.Properties.Resources.nav_left_green;
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(60, 28);
            this.backButton.Text = "Back";
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // forwardButton
            // 
            this.forwardButton.Enabled = false;
            this.forwardButton.Image = global::CefSharp.MinimalExample.WinForms.Properties.Resources.nav_right_green;
            this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(78, 28);
            this.forwardButton.Text = "Forward";
            this.forwardButton.Click += new System.EventHandler(this.ForwardButtonClick);
            // 
            // urlTextBox
            // 
            this.urlTextBox.AutoSize = false;
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(500, 25);
            this.urlTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UrlTextBoxKeyUp);
            // 
            // goButton
            // 
            this.goButton.Image = global::CefSharp.MinimalExample.WinForms.Properties.Resources.nav_plain_green;
            this.goButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(50, 28);
            this.goButton.Text = "Go";
            this.goButton.Click += new System.EventHandler(this.GoButtonClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.shortlistToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.shortcutsToolStripMenuItem,
            this.crawlerToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(730, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDevToolsToolStripMenuItem,
            this.stopCrawlingToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // showDevToolsToolStripMenuItem
            // 
            this.showDevToolsToolStripMenuItem.Name = "showDevToolsToolStripMenuItem";
            this.showDevToolsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.showDevToolsToolStripMenuItem.Text = "Show Debugging Tools...";
            this.showDevToolsToolStripMenuItem.Click += new System.EventHandler(this.ShowDevToolsMenuItemClick);
            // 
            // stopCrawlingToolStripMenuItem
            // 
            this.stopCrawlingToolStripMenuItem.Name = "stopCrawlingToolStripMenuItem";
            this.stopCrawlingToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.stopCrawlingToolStripMenuItem.Text = "Stop Crawling";
            this.stopCrawlingToolStripMenuItem.Click += new System.EventHandler(this.stopCrawlingToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuItemClick);
            // 
            // shortlistToolStripMenuItem
            // 
            this.shortlistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createAShortlistToolStripMenuItem});
            this.shortlistToolStripMenuItem.Name = "shortlistToolStripMenuItem";
            this.shortlistToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.shortlistToolStripMenuItem.Text = "Shortlist";
            this.shortlistToolStripMenuItem.DropDownOpening += new System.EventHandler(this.shortlistToolStripMenuItem_DropDownOpening);
            // 
            // createAShortlistToolStripMenuItem
            // 
            this.createAShortlistToolStripMenuItem.Name = "createAShortlistToolStripMenuItem";
            this.createAShortlistToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.createAShortlistToolStripMenuItem.Text = "&Create a Shortlist...";
            this.createAShortlistToolStripMenuItem.Click += new System.EventHandler(this.createAShortlistToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noReportsToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            this.reportsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.reportsToolStripMenuItem_DropDownOpening);
            // 
            // noReportsToolStripMenuItem
            // 
            this.noReportsToolStripMenuItem.Enabled = false;
            this.noReportsToolStripMenuItem.Name = "noReportsToolStripMenuItem";
            this.noReportsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.noReportsToolStripMenuItem.Text = "No Reports Available";
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rightmoveHomepageToolStripMenuItem,
            this.zooplaHomepageToolStripMenuItem});
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            this.shortcutsToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.shortcutsToolStripMenuItem.Text = "&Bookmarks";
            // 
            // rightmoveHomepageToolStripMenuItem
            // 
            this.rightmoveHomepageToolStripMenuItem.Name = "rightmoveHomepageToolStripMenuItem";
            this.rightmoveHomepageToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.rightmoveHomepageToolStripMenuItem.Text = "Rightmove Homepage";
            this.rightmoveHomepageToolStripMenuItem.Click += new System.EventHandler(this.rightmoveHomepageToolStripMenuItem_Click);
            // 
            // zooplaHomepageToolStripMenuItem
            // 
            this.zooplaHomepageToolStripMenuItem.Name = "zooplaHomepageToolStripMenuItem";
            this.zooplaHomepageToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.zooplaHomepageToolStripMenuItem.Text = "Zoopla Homepage";
            this.zooplaHomepageToolStripMenuItem.Click += new System.EventHandler(this.zooplaHomepageToolStripMenuItem_Click);
            // 
            // crawlerToolStripMenuItem
            // 
            this.crawlerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crawlToolStripMenuItem,
            this.logsToolStripMenuItem,
            this.runUnitTestsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.crawlerToolStripMenuItem.Name = "crawlerToolStripMenuItem";
            this.crawlerToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.crawlerToolStripMenuItem.Text = "&Tools";
            this.crawlerToolStripMenuItem.DropDownOpening += new System.EventHandler(this.crawlerToolStripMenuItem_DropDownOpening);
            // 
            // crawlToolStripMenuItem
            // 
            this.crawlToolStripMenuItem.Name = "crawlToolStripMenuItem";
            this.crawlToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.crawlToolStripMenuItem.Text = "&Shortlist Manager...";
            this.crawlToolStripMenuItem.Click += new System.EventHandler(this.crawlToolStripMenuItem_Click);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.logsToolStripMenuItem.Text = "Open &Logs Folder...";
            this.logsToolStripMenuItem.Click += new System.EventHandler(this.logsToolStripMenuItem_Click);
            // 
            // runUnitTestsToolStripMenuItem
            // 
            this.runUnitTestsToolStripMenuItem.Name = "runUnitTestsToolStripMenuItem";
            this.runUnitTestsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.runUnitTestsToolStripMenuItem.Text = "Run Unit Tests";
            this.runUnitTestsToolStripMenuItem.Click += new System.EventHandler(this.runUnitTestsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideToolStripMenuItem,
            this.aboutHomeHunterToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.userGuideToolStripMenuItem.Text = "&User Guide";
            // 
            // aboutHomeHunterToolStripMenuItem
            // 
            this.aboutHomeHunterToolStripMenuItem.Name = "aboutHomeHunterToolStripMenuItem";
            this.aboutHomeHunterToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.aboutHomeHunterToolStripMenuItem.Text = "&About Home Hunter";
            this.aboutHomeHunterToolStripMenuItem.Click += new System.EventHandler(this.aboutHomeHunterToolStripMenuItem_Click);
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 490);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BrowserForm";
            this.Text = "Home Hunter";
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.PerformLayout();
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton backButton;
        private System.Windows.Forms.ToolStripButton forwardButton;
        private System.Windows.Forms.ToolStripTextBox urlTextBox;
        private System.Windows.Forms.ToolStripButton goButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ToolStripMenuItem showDevToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crawlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crawlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutHomeHunterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createAShortlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightmoveHomepageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runUnitTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopCrawlingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zooplaHomepageToolStripMenuItem;
    }
}