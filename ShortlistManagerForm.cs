using Microsoft.Win32;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class ShortlistManagerForm : Form
    {
        public ShortlistManagerForm()
        {
            InitializeComponent();
            InitialiseShortlistNameComboBoxValues();
            EnsureButtonStateCorrect();
            cbShortlistName.Width -= 1;
            cbShortlistName.Height -= 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete? This action cannot be undone.", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var shortlistId = (cbShortlistName.SelectedItem as ComboBoxItem).Value.ToString();
                Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\HomeHunter\Shortlists\" + shortlistId);
                InitialiseShortlistNameComboBoxValues();
                EnsureButtonStateCorrect();
            }
        }

        private void EnsureButtonStateCorrect()
        {
            if (!string.IsNullOrEmpty(cbShortlistName.Text))
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ShortlistEditor.Open((cbShortlistName.SelectedItem as ComboBoxItem).Value.ToString(), () =>
            {
                var shortlistManagerForm = new ShortlistManagerForm();
                shortlistManagerForm.Show();
            });
            Close();
        }

        private void InitialiseShortlistNameComboBoxValues()
        {
            cbShortlistName.Items.Clear();

            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\HomeHunter\Shortlists");

            if (key == null)
                return;

            foreach (string subKeyName in key.GetSubKeyNames())
            {
                using (RegistryKey tempKey = key.OpenSubKey(subKeyName))
                {
                    string shortlistName = tempKey.GetValue("Name").ToString();

                    var item = new ComboBoxItem();
                    item.Text = shortlistName;
                    item.Value = subKeyName;

                    cbShortlistName.Items.Add(item);
                }
            }

            ResortComboBoxItemCollection(cbShortlistName.Items);

            if (cbShortlistName.Items.Count > 0)
            {
                cbShortlistName.SelectedIndex = 0;
            }
        }

        private void ResortComboBoxItemCollection(ComboBox.ObjectCollection coll)
        {
            var oAList = new ArrayList(coll);
            oAList.Sort(new ComboBoxItemComparer());
            coll.Clear();

            foreach (object oItem in oAList)
            {
                coll.Add(oItem);
            }
        }

        // Fix combox box style.
        protected override void OnPaint(PaintEventArgs e)
        {
            //TODO: Black is the wrong color it is too dark.
            SolidBrush myBrush = new SolidBrush(Color.Black);
            Graphics formGraphics;

            formGraphics = CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(cbShortlistName.Left - 1, cbShortlistName.Top - 1, cbShortlistName.Width + 2, cbShortlistName.Height + 2));
            formGraphics.Dispose();

            myBrush.Dispose();
        }

        private void cbShortlistName_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnsureButtonStateCorrect();
        }
    }
}
