namespace DocumentServices
{
    partial class frmFileToFtp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileToFtp));
            this.clbTurists = new System.Windows.Forms.CheckedListBox();
            this.lbType = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.clbUslugi = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // clbTurists
            // 
            this.clbTurists.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.clbTurists.FormattingEnabled = true;
            this.clbTurists.Location = new System.Drawing.Point(12, 152);
            this.clbTurists.Name = "clbTurists";
            this.clbTurists.Size = new System.Drawing.Size(1036, 139);
            this.clbTurists.TabIndex = 1;
            // 
            // lbType
            // 
            this.lbType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbType.FormattingEnabled = true;
            this.lbType.Location = new System.Drawing.Point(12, 297);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(1036, 147);
            this.lbType.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.Location = new System.Drawing.Point(156, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Выход";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.Location = new System.Drawing.Point(12, 450);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(138, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Добавить файл";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // clbUslugi
            // 
            this.clbUslugi.FormattingEnabled = true;
            this.clbUslugi.Location = new System.Drawing.Point(12, 22);
            this.clbUslugi.Name = "clbUslugi";
            this.clbUslugi.Size = new System.Drawing.Size(1036, 124);
            this.clbUslugi.TabIndex = 5;
            this.clbUslugi.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbUslugi_ItemCheck);
            // 
            // frmFileToFtp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 480);
            this.Controls.Add(this.clbUslugi);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbType);
            this.Controls.Add(this.clbTurists);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFileToFtp";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbTurists;
        private System.Windows.Forms.ListBox lbType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckedListBox clbUslugi;
    }
}