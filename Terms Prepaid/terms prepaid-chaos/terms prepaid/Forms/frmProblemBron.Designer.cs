namespace terms_prepaid
{
    partial class frmProblemBron
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProblemBron));
            this.timePulse = new System.Windows.Forms.Timer(this.components);
            this.tcAllMy = new System.Windows.Forms.TabControl();
            this.tcpAll = new System.Windows.Forms.TabPage();
            this.tcpMy = new System.Windows.Forms.TabPage();
            this.tcAllMy.SuspendLayout();
            this.SuspendLayout();
            // 
            // timePulse
            // 
            this.timePulse.Interval = 200;
            this.timePulse.Tick += new System.EventHandler(this.timePulse_Tick);
            // 
            // tcAllMy
            // 
            this.tcAllMy.Controls.Add(this.tcpAll);
            this.tcAllMy.Controls.Add(this.tcpMy);
            this.tcAllMy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAllMy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tcAllMy.Location = new System.Drawing.Point(0, 0);
            this.tcAllMy.Name = "tcAllMy";
            this.tcAllMy.SelectedIndex = 0;
            this.tcAllMy.Size = new System.Drawing.Size(641, 289);
            this.tcAllMy.TabIndex = 0;
            // 
            // tcpAll
            // 
            this.tcpAll.AutoScroll = true;
            this.tcpAll.Location = new System.Drawing.Point(4, 25);
            this.tcpAll.Name = "tcpAll";
            this.tcpAll.Padding = new System.Windows.Forms.Padding(3);
            this.tcpAll.Size = new System.Drawing.Size(633, 260);
            this.tcpAll.TabIndex = 0;
            this.tcpAll.Text = "Все";
            this.tcpAll.UseVisualStyleBackColor = true;
            // 
            // tcpMy
            // 
            this.tcpMy.AutoScroll = true;
            this.tcpMy.Location = new System.Drawing.Point(4, 22);
            this.tcpMy.Name = "tcpMy";
            this.tcpMy.Padding = new System.Windows.Forms.Padding(3);
            this.tcpMy.Size = new System.Drawing.Size(633, 263);
            this.tcpMy.TabIndex = 1;
            this.tcpMy.Text = "Мои";
            this.tcpMy.UseVisualStyleBackColor = true;
            // 
            // frmProblemBron
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 289);
            this.Controls.Add(this.tcAllMy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProblemBron";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Проблемные брони";
            this.Load += new System.EventHandler(this.frmProblemBron_Load);
            this.tcAllMy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timePulse;
        private System.Windows.Forms.TabControl tcAllMy;
        private System.Windows.Forms.TabPage tcpAll;
        private System.Windows.Forms.TabPage tcpMy;


    }
}