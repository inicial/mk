namespace terms_prepaid
{
    partial class frmNewOptionsEditInsList
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
            this.lstServices = new System.Windows.Forms.ListBox();
            this.lblClose = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstServices
            // 
            this.lstServices.FormattingEnabled = true;
            this.lstServices.Location = new System.Drawing.Point(2, 2);
            this.lstServices.Name = "lstServices";
            this.lstServices.Size = new System.Drawing.Size(496, 329);
            this.lstServices.TabIndex = 0;
            this.lstServices.SelectedIndexChanged += new System.EventHandler(this.lstServices_SelectedIndexChanged);
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblClose.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblClose.Location = new System.Drawing.Point(417, 333);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(64, 17);
            this.lblClose.TabIndex = 1;
            this.lblClose.Text = "Закрыть";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // frmNewOptionsEditInsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 353);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.lstServices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNewOptionsEditInsList";
            this.Text = "frmNewOptionsEditInsList";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.Form_Deactivate);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstServices;
        private System.Windows.Forms.Label lblClose;
    }
}