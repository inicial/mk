namespace terms_prepaid.Forms
{
    partial class frmFlight
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
            this.wbBook = new Awesomium.Windows.Forms.WebControl(this.components);
            this.SuspendLayout();
            // 
            // wbBook
            // 
            this.wbBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbBook.Location = new System.Drawing.Point(0, 0);
            this.wbBook.Size = new System.Drawing.Size(514, 418);
            this.wbBook.TabIndex = 0;
            this.wbBook.ShowCreatedWebView += new Awesomium.Core.ShowCreatedWebViewEventHandler(this.Awesomium_Windows_Forms_WebControl_ShowCreatedWebView);
            // 
            // frmFlight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 418);
            this.Controls.Add(this.wbBook);
            this.Name = "frmFlight";
            this.Text = "frmFlight";
            this.ResumeLayout(false);

            Awesomium.Core.WebPreferences webPreferences1 = new Awesomium.Core.WebPreferences(true);
            this.wspBook = new Awesomium.Windows.Forms.WebSessionProvider(this.components);

            // 
            // wspBook
            // 
            webPreferences1.AcceptLanguage = "ru";
            webPreferences1.Databases = true;
            webPreferences1.JavascriptViewChangeSource = false;
            webPreferences1.JavascriptViewEvents = false;
            webPreferences1.JavascriptViewExecute = false;
            webPreferences1.PdfJS = false;
            this.wspBook.Preferences = webPreferences1;
            this.wspBook.Views.Add(this.wbBook);
        }

        #endregion

        private Awesomium.Windows.Forms.WebControl wbBook;
        public Awesomium.Windows.Forms.WebSessionProvider wspBook;
    }
}