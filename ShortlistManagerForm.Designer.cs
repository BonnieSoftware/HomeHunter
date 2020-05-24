namespace CefSharp.MinimalExample.WinForms
{
    partial class ShortlistManagerForm
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
            this.cbShortlistName = new System.Windows.Forms.ComboBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbShortlistName
            // 
            this.cbShortlistName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShortlistName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbShortlistName.FormattingEnabled = true;
            this.cbShortlistName.Location = new System.Drawing.Point(13, 13);
            this.cbShortlistName.Name = "cbShortlistName";
            this.cbShortlistName.Size = new System.Drawing.Size(429, 28);
            this.cbShortlistName.TabIndex = 0;
            this.cbShortlistName.SelectedIndexChanged += new System.EventHandler(this.cbShortlistName_SelectedIndexChanged);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(466, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(548, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ShortlistManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 55);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.cbShortlistName);
            this.Name = "ShortlistManagerForm";
            this.Text = "Shortlist Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbShortlistName;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}