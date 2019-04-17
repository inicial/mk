namespace terms_prepaid
{
    partial class frmMessages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessages));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpComments = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSpendMTM = new System.Windows.Forms.Button();
            this.tbMTMMessage = new System.Windows.Forms.TextBox();
            this.rtbMTM = new System.Windows.Forms.RichTextBox();
            this.tp = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMTCSpend = new System.Windows.Forms.Button();
            this.tbMTCMessage = new System.Windows.Forms.TextBox();
            this.rtbMTCMesages = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tpComments.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tp.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpComments);
            this.tabControl1.Controls.Add(this.tp);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1096, 447);
            this.tabControl1.TabIndex = 0;
            // 
            // tpComments
            // 
            this.tpComments.Controls.Add(this.tableLayoutPanel1);
            this.tpComments.Location = new System.Drawing.Point(4, 22);
            this.tpComments.Name = "tpComments";
            this.tpComments.Padding = new System.Windows.Forms.Padding(3);
            this.tpComments.Size = new System.Drawing.Size(1088, 421);
            this.tpComments.TabIndex = 0;
            this.tpComments.Text = "Примечания";
            this.tpComments.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbMTM, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.192771F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.80723F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1082, 415);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1076, 27);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnSpendMTM
            // 
            this.btnSpendMTM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSpendMTM.Location = new System.Drawing.Point(974, 3);
            this.btnSpendMTM.Name = "btnSpendMTM";
            this.btnSpendMTM.Size = new System.Drawing.Size(99, 21);
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
            this.rtbMTM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMTM.Location = new System.Drawing.Point(3, 36);
            this.rtbMTM.Name = "rtbMTM";
            this.rtbMTM.ReadOnly = true;
            this.rtbMTM.Size = new System.Drawing.Size(1076, 376);
            this.rtbMTM.TabIndex = 1;
            this.rtbMTM.Text = "";
            // 
            // tp
            // 
            this.tp.Controls.Add(this.tableLayoutPanel3);
            this.tp.Location = new System.Drawing.Point(4, 22);
            this.tp.Name = "tp";
            this.tp.Padding = new System.Windows.Forms.Padding(3);
            this.tp.Size = new System.Drawing.Size(1088, 421);
            this.tp.TabIndex = 1;
            this.tp.Text = "Сообщение покупателю";
            this.tp.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.rtbMTCMesages, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.192771F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.80723F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1082, 415);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.24164F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.758365F));
            this.tableLayoutPanel4.Controls.Add(this.btnMTCSpend, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tbMTCMessage, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1076, 27);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnMTCSpend
            // 
            this.btnMTCSpend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMTCSpend.Location = new System.Drawing.Point(974, 3);
            this.btnMTCSpend.Name = "btnMTCSpend";
            this.btnMTCSpend.Size = new System.Drawing.Size(99, 21);
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
            this.tbMTCMessage.Size = new System.Drawing.Size(965, 20);
            this.tbMTCMessage.TabIndex = 1;
            // 
            // rtbMTCMesages
            // 
            this.rtbMTCMesages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMTCMesages.Location = new System.Drawing.Point(3, 36);
            this.rtbMTCMesages.Name = "rtbMTCMesages";
            this.rtbMTCMesages.ReadOnly = true;
            this.rtbMTCMesages.Size = new System.Drawing.Size(1076, 376);
            this.rtbMTCMesages.TabIndex = 1;
            this.rtbMTCMesages.Text = "";
            // 
            // frmMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 447);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMessages";
            this.Text = "Сообщения";
            this.tabControl1.ResumeLayout(false);
            this.tpComments.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tp.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpComments;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TabPage tp;
        private System.Windows.Forms.RichTextBox rtbMTM;
        private System.Windows.Forms.Button btnSpendMTM;
        private System.Windows.Forms.TextBox tbMTMMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnMTCSpend;
        private System.Windows.Forms.TextBox tbMTCMessage;
        private System.Windows.Forms.RichTextBox rtbMTCMesages;
    }
}