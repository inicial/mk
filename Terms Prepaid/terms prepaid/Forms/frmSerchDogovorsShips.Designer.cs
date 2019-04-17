namespace terms_prepaid.Forms
{
    partial class frmSerchDogovorsShips
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
            this.panelForm = new System.Windows.Forms.Panel();
            this.flg_River = new System.Windows.Forms.CheckBox();
            this.flg_See = new System.Windows.Forms.CheckBox();
            this.flg_All = new System.Windows.Forms.CheckBox();
            this.lst_Ship = new System.Windows.Forms.ListBox();
            this.lst_Cruise = new System.Windows.Forms.ListBox();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForm
            // 
            this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForm.Controls.Add(this.flg_River);
            this.panelForm.Controls.Add(this.flg_See);
            this.panelForm.Controls.Add(this.flg_All);
            this.panelForm.Controls.Add(this.lst_Ship);
            this.panelForm.Controls.Add(this.lst_Cruise);
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(450, 300);
            this.panelForm.TabIndex = 3;
            // 
            // flg_River
            // 
            this.flg_River.AutoSize = true;
            this.flg_River.Location = new System.Drawing.Point(146, 6);
            this.flg_River.Name = "flg_River";
            this.flg_River.Size = new System.Drawing.Size(63, 17);
            this.flg_River.TabIndex = 8;
            this.flg_River.Text = "речные";
            this.flg_River.UseVisualStyleBackColor = true;
            this.flg_River.Click += new System.EventHandler(this.flg_River_Click);
            // 
            // flg_See
            // 
            this.flg_See.AutoSize = true;
            this.flg_See.Location = new System.Drawing.Point(70, 6);
            this.flg_See.Name = "flg_See";
            this.flg_See.Size = new System.Drawing.Size(70, 17);
            this.flg_See.TabIndex = 7;
            this.flg_See.Text = "морские";
            this.flg_See.UseVisualStyleBackColor = true;
            this.flg_See.Click += new System.EventHandler(this.flg_See_Click);
            // 
            // flg_All
            // 
            this.flg_All.AutoSize = true;
            this.flg_All.Location = new System.Drawing.Point(11, 6);
            this.flg_All.Name = "flg_All";
            this.flg_All.Size = new System.Drawing.Size(44, 17);
            this.flg_All.TabIndex = 6;
            this.flg_All.Text = "все";
            this.flg_All.UseVisualStyleBackColor = true;
            this.flg_All.Click += new System.EventHandler(this.flg_All_Click);
            // 
            // lst_Ship
            // 
            this.lst_Ship.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_Ship.FormattingEnabled = true;
            this.lst_Ship.Location = new System.Drawing.Point(186, 29);
            this.lst_Ship.Name = "lst_Ship";
            this.lst_Ship.Size = new System.Drawing.Size(251, 262);
            this.lst_Ship.TabIndex = 4;
            this.lst_Ship.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lst_Ship_MouseClick);
            this.lst_Ship.SelectedIndexChanged += new System.EventHandler(this.lst_Ship_SelectedIndexChanged);
            this.lst_Ship.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_Ship_MouseDoubleClick);
            this.lst_Ship.MouseEnter += new System.EventHandler(this.lst_Ship_MouseEnter);
            // 
            // lst_Cruise
            // 
            this.lst_Cruise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_Cruise.FormattingEnabled = true;
            this.lst_Cruise.Location = new System.Drawing.Point(10, 28);
            this.lst_Cruise.Name = "lst_Cruise";
            this.lst_Cruise.Size = new System.Drawing.Size(170, 262);
            this.lst_Cruise.TabIndex = 3;
            this.lst_Cruise.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lst_Cruise_MouseClick);
            this.lst_Cruise.SelectedIndexChanged += new System.EventHandler(this.lst_Cruise_SelectedIndexChanged);
            this.lst_Cruise.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_Cruise_MouseDoubleClick);
            this.lst_Cruise.MouseEnter += new System.EventHandler(this.lst_Cruise_MouseEnter);
            // 
            // frmSerchDogovorsShips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.panelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSerchDogovorsShips";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Выбор лайнера";
            this.Deactivate += new System.EventHandler(this.frmSerchDogovorsShips_Deactivate);
            this.Load += new System.EventHandler(this.frmSerchDogovorsShips_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSerchDogovorsShips_KeyDown);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.ListBox lst_Cruise;
        private System.Windows.Forms.ListBox lst_Ship;
        private System.Windows.Forms.CheckBox flg_River;
        private System.Windows.Forms.CheckBox flg_See;
        private System.Windows.Forms.CheckBox flg_All;
    }
}