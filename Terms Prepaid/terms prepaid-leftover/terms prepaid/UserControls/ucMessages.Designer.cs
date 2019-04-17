namespace terms_prepaid.UserControls
{
    partial class ucMessages
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
            this.tpComments = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSpendMTM = new System.Windows.Forms.Button();
            this.tbMTMMessage = new System.Windows.Forms.TextBox();
            this.rtbMTM = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpMessages = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMTCSpend = new System.Windows.Forms.Button();
            this.tbMTCMessage = new System.Windows.Forms.TextBox();
            this.cbClientOk = new System.Windows.Forms.CheckBox();
            this.rtbMTCMesages = new System.Windows.Forms.RichTextBox();
            this.cmsRich = new System.Windows.Forms.ContextMenuStrip();
            this.cmstCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tpComments.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tpMessages.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.cmsRich.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpComments);
            this.tabControl1.Controls.Add(this.tpMessages);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1096, 447);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtbMTCMesages_MouseDown);
            // 
            // tpComments
            // 
            this.tpComments.BackColor = System.Drawing.Color.Tan;
            this.tpComments.Controls.Add(this.tableLayoutPanel1);
            this.tpComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tpComments.ImageIndex = 1;
            this.tpComments.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tpComments.Location = new System.Drawing.Point(4, 25);
            this.tpComments.Name = "tpComments";
            this.tpComments.Padding = new System.Windows.Forms.Padding(3);
            this.tpComments.Size = new System.Drawing.Size(1088, 418);
            this.tpComments.TabIndex = 0;
            this.tpComments.Text = "Сообщения бронировщику\\реализатору";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbMTM, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1082, 412);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.24164F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.758365F));
            this.tableLayoutPanel2.Controls.Add(this.btnSpendMTM, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbMTMMessage, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1076, 29);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnSpendMTM
            // 
            this.btnSpendMTM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSpendMTM.Location = new System.Drawing.Point(974, 3);
            this.btnSpendMTM.Name = "btnSpendMTM";
            this.btnSpendMTM.Size = new System.Drawing.Size(99, 23);
            this.btnSpendMTM.TabIndex = 0;
            this.btnSpendMTM.Text = "Отправить";
            this.btnSpendMTM.UseVisualStyleBackColor = true;
            this.btnSpendMTM.Click += new System.EventHandler(this.btnSpendMTM_Click);
            // 
            // tbMTMMessage
            // 
            this.tbMTMMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMTMMessage.Location = new System.Drawing.Point(3, 3);
            this.tbMTMMessage.Name = "tbMTMMessage";
            this.tbMTMMessage.Size = new System.Drawing.Size(965, 20);
            this.tbMTMMessage.TabIndex = 1;
            // 
            // rtbMTM
            // 
            this.rtbMTM.ContextMenuStrip = this.contextMenuStrip1;
            this.rtbMTM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMTM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbMTM.Location = new System.Drawing.Point(3, 38);
            this.rtbMTM.Name = "rtbMTM";
            this.rtbMTM.ReadOnly = true;
            this.rtbMTM.Size = new System.Drawing.Size(1076, 371);
            this.rtbMTM.TabIndex = 1;
            this.rtbMTM.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "cmsRich";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem1.Text = "Копировать";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tpMessages
            // 
            this.tpMessages.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tpMessages.Controls.Add(this.tableLayoutPanel3);
            this.tpMessages.Location = new System.Drawing.Point(4, 25);
            this.tpMessages.Name = "tpMessages";
            this.tpMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tpMessages.Size = new System.Drawing.Size(1088, 418);
            this.tpMessages.TabIndex = 1;
            this.tpMessages.Text = "Переписка с клиентом";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.rtbMTCMesages, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1082, 412);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.94424F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.05576F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tableLayoutPanel4.Controls.Add(this.btnMTCSpend, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbMTCMessage, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbClientOk, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1076, 29);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnMTCSpend
            // 
            this.btnMTCSpend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMTCSpend.Location = new System.Drawing.Point(742, 3);
            this.btnMTCSpend.Name = "btnMTCSpend";
            this.btnMTCSpend.Size = new System.Drawing.Size(125, 23);
            this.btnMTCSpend.TabIndex = 0;
            this.btnMTCSpend.Text = "Отправить";
            this.btnMTCSpend.UseVisualStyleBackColor = true;
            this.btnMTCSpend.Click += new System.EventHandler(this.btnMTCSpend_Click);
            // 
            // tbMTCMessage
            // 
            this.tbMTCMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMTCMessage.Location = new System.Drawing.Point(3, 3);
            this.tbMTCMessage.Name = "tbMTCMessage";
            this.tbMTCMessage.Size = new System.Drawing.Size(733, 23);
            this.tbMTCMessage.TabIndex = 1;
            // 
            // cbClientOk
            // 
            this.cbClientOk.AutoSize = true;
            this.cbClientOk.Location = new System.Drawing.Point(873, 3);
            this.cbClientOk.Name = "cbClientOk";
            this.cbClientOk.Size = new System.Drawing.Size(148, 21);
            this.cbClientOk.TabIndex = 2;
            this.cbClientOk.Text = "Закрыть преписку";
            this.cbClientOk.UseVisualStyleBackColor = true;
            this.cbClientOk.CheckedChanged += new System.EventHandler(this.cbClientOk_CheckedChanged);
            // 
            // rtbMTCMesages
            // 
            this.rtbMTCMesages.ContextMenuStrip = this.cmsRich;
            this.rtbMTCMesages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMTCMesages.Location = new System.Drawing.Point(3, 38);
            this.rtbMTCMesages.Name = "rtbMTCMesages";
            this.rtbMTCMesages.ReadOnly = true;
            this.rtbMTCMesages.Size = new System.Drawing.Size(1076, 371);
            this.rtbMTCMesages.TabIndex = 1;
            this.rtbMTCMesages.Text = "";
            this.rtbMTCMesages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtbMTCMesages_MouseDown);
            // 
            // cmsRich
            // 
            this.cmsRich.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmstCopy});
            this.cmsRich.Name = "cmsRich";
            this.cmsRich.Size = new System.Drawing.Size(140, 26);
            // 
            // cmstCopy
            // 
            this.cmstCopy.Name = "cmstCopy";
            this.cmstCopy.Size = new System.Drawing.Size(139, 22);
            this.cmstCopy.Text = "Копировать";
            this.cmstCopy.Click += new System.EventHandler(this.cmstCopy_Click);
            // 
            // ucMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucMessages";
            this.Size = new System.Drawing.Size(1096, 447);
            this.tabControl1.ResumeLayout(false);
            this.tpComments.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tpMessages.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.cmsRich.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpComments;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TabPage tpMessages;
        private System.Windows.Forms.RichTextBox rtbMTM;
        private System.Windows.Forms.Button btnSpendMTM;
        private System.Windows.Forms.TextBox tbMTMMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnMTCSpend;
        private System.Windows.Forms.TextBox tbMTCMessage;
        private System.Windows.Forms.RichTextBox rtbMTCMesages;
        private System.Windows.Forms.ContextMenuStrip cmsRich;
        private System.Windows.Forms.ToolStripMenuItem cmstCopy;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.CheckBox cbClientOk;
    }
}