namespace terms_prepaid.Forms
{
    partial class frmChildWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChildWindow));
            this.wcChild = new Awesomium.Windows.Forms.WebControl(this.components);
            this.wspChild = new Awesomium.Windows.Forms.WebSessionProvider(this.components);
            this.SuspendLayout();
            // 
            // wcChild
            // 
            this.wcChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wcChild.Location = new System.Drawing.Point(0, 0);
            this.wcChild.Size = new System.Drawing.Size(686, 469);
            this.wcChild.TabIndex = 0;
            this.wcChild.TargetURLChanged += new Awesomium.Core.UrlEventHandler(this.Awesomium_Windows_Forms_WebControl_TargetURLChanged);
            // 
            // wspChild
            // 
            this.wspChild.Views.Add(this.wcChild);
            // 
            // frmChildWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 469);
            this.Controls.Add(this.wcChild);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChildWindow";
            this.Load += new System.EventHandler(this.frmChildWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Awesomium.Windows.Forms.WebControl wcChild;
        public Awesomium.Windows.Forms.WebSessionProvider wspChild;

    }
}