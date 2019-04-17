namespace terms_prepaid.Forms
{
    partial class frmMessagesNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessagesNew));
            this.MessagesHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // MessagesHost
            // 
            this.MessagesHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessagesHost.Location = new System.Drawing.Point(0, 0);
            this.MessagesHost.Name = "MessagesHost";
            this.MessagesHost.Size = new System.Drawing.Size(746, 448);
            this.MessagesHost.TabIndex = 0;
            this.MessagesHost.Text = "elementHost1";
            this.MessagesHost.Child = null;
            // 
            // frmMessagesNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 448);
            this.Controls.Add(this.MessagesHost);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMessagesNew";
            this.Text = "Сообщения";
            this.Shown += new System.EventHandler(this.frmMessagesNew_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost MessagesHost;
    }
}