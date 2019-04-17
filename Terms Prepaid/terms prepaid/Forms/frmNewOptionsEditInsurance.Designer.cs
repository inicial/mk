namespace terms_prepaid
{
    partial class frmNewOptionsEditInsurance
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
            this.ServiceListHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // ServiceListHost
            // 
            this.ServiceListHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServiceListHost.Location = new System.Drawing.Point(0, 0);
            this.ServiceListHost.Name = "ServiceListHost";
            this.ServiceListHost.Size = new System.Drawing.Size(1384, 662);
            this.ServiceListHost.TabIndex = 0;
            this.ServiceListHost.Text = "ServiceListHost";
            this.ServiceListHost.Child = null;
            // 
            // frmNewOptionsEditInsurance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 662);
            this.Controls.Add(this.ServiceListHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewOptionsEditInsurance";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Модификация  услуг  страхования";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewOptionsEditInsurance_FormClosing);
            this.Load += new System.EventHandler(this.frmNewOptionsEditInsurance_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost ServiceListHost;
    }
}