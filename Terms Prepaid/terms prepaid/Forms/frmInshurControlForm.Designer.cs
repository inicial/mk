namespace terms_prepaid
{
    partial class frmInshurControlForm
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
            this.InshurControlHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // InshurControlHost
            // 
            this.InshurControlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InshurControlHost.Location = new System.Drawing.Point(0, 0);
            this.InshurControlHost.Name = "InshurControlHost";
            this.InshurControlHost.Size = new System.Drawing.Size(1344, 662);
            this.InshurControlHost.TabIndex = 1;
            this.InshurControlHost.Text = "InshurControlHost";
            this.InshurControlHost.Child = null;
            // 
            // frmInshurControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 662);
            this.Controls.Add(this.InshurControlHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInshurControlForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "База данных цен (страхование)";
            this.Load += new System.EventHandler(this.frmInshurControlForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost InshurControlHost;
    }
}