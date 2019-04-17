namespace terms_prepaid.Forms
{
    partial class frmNewOptionsConfirmTaskerWPF
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
            this.ConfirmControl = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // ConfirmControl
            // 
            this.ConfirmControl.BackColor = System.Drawing.Color.White;
            this.ConfirmControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfirmControl.Location = new System.Drawing.Point(0, 0);
            this.ConfirmControl.Name = "ConfirmControl";
            this.ConfirmControl.Size = new System.Drawing.Size(550, 250);
            this.ConfirmControl.TabIndex = 0;
            this.ConfirmControl.Text = "ConfirmControl";
            this.ConfirmControl.Child = null;
            // 
            // frmNewOptionsConfirmTaskerWPF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 250);
            this.Controls.Add(this.ConfirmControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewOptionsConfirmTaskerWPF";
            this.ShowIcon = false;
            this.Text = "Подтверждение";
            this.Deactivate += new System.EventHandler(this.frmNewOptionsTask_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewOptionsConfirmTaskerWPF_FormClosing);
            this.Load += new System.EventHandler(this.frmNewOptionsTasker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost ConfirmControl;
    }
}