namespace terms_prepaid.Forms
{
    partial class frmUnhandledCorrespondence
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpAll = new System.Windows.Forms.TabPage();
            this.tpMy = new System.Windows.Forms.TabPage();
            this.tpManager = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpAll);
            this.tabControl1.Controls.Add(this.tpMy);
            this.tabControl1.Controls.Add(this.tpManager);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 112);
            this.tabControl1.TabIndex = 0;
            // 
            // tpAll
            // 
            this.tpAll.Location = new System.Drawing.Point(4, 22);
            this.tpAll.Name = "tpAll";
            this.tpAll.Padding = new System.Windows.Forms.Padding(3);
            this.tpAll.Size = new System.Drawing.Size(276, 236);
            this.tpAll.TabIndex = 0;
            this.tpAll.Text = "Все";
            this.tpAll.UseVisualStyleBackColor = true;
            // 
            // tpMy
            // 
            this.tpMy.Location = new System.Drawing.Point(4, 22);
            this.tpMy.Name = "tpMy";
            this.tpMy.Padding = new System.Windows.Forms.Padding(3);
            this.tpMy.Size = new System.Drawing.Size(476, 86);
            this.tpMy.TabIndex = 1;
            this.tpMy.Text = "Мои";
            this.tpMy.UseVisualStyleBackColor = true;
            // 
            // tpManager
            // 
            this.tpManager.Location = new System.Drawing.Point(4, 22);
            this.tpManager.Name = "tpManager";
            this.tpManager.Size = new System.Drawing.Size(276, 236);
            this.tpManager.TabIndex = 2;
            this.tpManager.Text = "По сотруднику";
            this.tpManager.UseVisualStyleBackColor = true;
            // 
            // frmUnhandledCorrespondence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 112);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmUnhandledCorrespondence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Необработанная переписка";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpAll;
        private System.Windows.Forms.TabPage tpMy;
        private System.Windows.Forms.TabPage tpManager;
    }
}