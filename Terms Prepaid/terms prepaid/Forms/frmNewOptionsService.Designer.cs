namespace terms_prepaid.Forms
{
    partial class frmNewOptionsService
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
            this.lst_Name = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lst_Name
            // 
            this.lst_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_Name.FormattingEnabled = true;
            this.lst_Name.Location = new System.Drawing.Point(0, 0);
            this.lst_Name.Name = "lst_Name";
            this.lst_Name.Size = new System.Drawing.Size(450, 296);
            this.lst_Name.TabIndex = 3;
            this.lst_Name.Click += new System.EventHandler(this.lst_Name_MouseClick);
            this.lst_Name.SelectedIndexChanged += new System.EventHandler(this.lst_Name_SelectedIndexChanged);
            this.lst_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmNewOptionsService_KeyDown);
            // 
            // frmNewOptionsService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 296);
            this.Controls.Add(this.lst_Name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNewOptionsService";
            this.Text = "Значения для услуги";
            this.Deactivate += new System.EventHandler(this.frmNewOptionsService_Deactivate);
            this.Load += new System.EventHandler(this.frmNewOptionsService_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmNewOptionsService_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lst_Name;
    }
}