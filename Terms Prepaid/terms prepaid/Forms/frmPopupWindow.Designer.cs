namespace terms_prepaid.Forms
{
    partial class frmPopupWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPopupWindow));
            this.webControlContextMenu = new Awesomium.Windows.Forms.WebControlContextMenu(this.components);
            this.openSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webControl1 = new Awesomium.Windows.Forms.WebControl(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.changeVisibleStateBtn = new System.Windows.Forms.Button();
            this.webControlContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webControlContextMenu
            // 
            this.webControlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSeparator,
            this.openMenuItem});
            this.webControlContextMenu.Name = "webControlContextMenu";
            this.webControlContextMenu.Size = new System.Drawing.Size(204, 154);
            this.webControlContextMenu.View = null;
            this.webControlContextMenu.Opening += new Awesomium.Windows.Forms.ContextMenuOpeningEventHandler(this.webControlContextMenu_Opening);
            this.webControlContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.webControlContextMenu_ItemClicked);
            // 
            // openSeparator
            // 
            this.openSeparator.Name = "openSeparator";
            this.openSeparator.Size = new System.Drawing.Size(200, 6);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(203, 22);
            this.openMenuItem.Tag = "open";
            this.openMenuItem.Text = "Open in new window...";
            // 
            // webControl1
            // 
            this.webControl1.Location = new System.Drawing.Point(52, 40);
            this.webControl1.Size = new System.Drawing.Size(75, 23);
            this.webControl1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.changeVisibleStateBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 454);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 20);
            this.panel1.TabIndex = 1;
            // 
            // changeVisibleStateBtn
            // 
            this.changeVisibleStateBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.changeVisibleStateBtn.Location = new System.Drawing.Point(933, 0);
            this.changeVisibleStateBtn.Name = "changeVisibleStateBtn";
            this.changeVisibleStateBtn.Size = new System.Drawing.Size(75, 20);
            this.changeVisibleStateBtn.TabIndex = 0;
            this.changeVisibleStateBtn.Text = "Скрыть";
            this.changeVisibleStateBtn.UseVisualStyleBackColor = true;
            this.changeVisibleStateBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmPopupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 474);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPopupWindow";
            this.Load += new System.EventHandler(this.frmPopupWindow_Load);
            this.webControlContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Awesomium.Windows.Forms.WebControlContextMenu webControlContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripSeparator openSeparator;
        private Awesomium.Windows.Forms.WebControl webControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button changeVisibleStateBtn;

    }
}