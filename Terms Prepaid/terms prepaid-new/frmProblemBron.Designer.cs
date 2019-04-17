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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOption3 = new System.Windows.Forms.Button();
            this.tbOption3 = new System.Windows.Forms.TextBox();
            this.btnNoPrintDoc = new System.Windows.Forms.Button();
            this.btnNoInsertDoc = new System.Windows.Forms.Button();
            this.btnNoDogovorAccept = new System.Windows.Forms.Button();
            this.btnNoInshur = new System.Windows.Forms.Button();
            this.btnNoAcceptUsluga = new System.Windows.Forms.Button();
            this.btnDogovorNoAccept = new System.Windows.Forms.Button();
            this.tbNoPrintDoc = new System.Windows.Forms.TextBox();
            this.tbNoInsertDoc = new System.Windows.Forms.TextBox();
            this.tbNoDogovorAccept = new System.Windows.Forms.TextBox();
            this.tbNoInshur = new System.Windows.Forms.TextBox();
            this.tbNoAcceptUsluga = new System.Windows.Forms.TextBox();
            this.tbDogovorNoAccept = new System.Windows.Forms.TextBox();
            this.timePulse = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnOption3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbOption3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnNoPrintDoc, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnNoInsertDoc, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnNoDogovorAccept, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnNoInshur, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnNoAcceptUsluga, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDogovorNoAccept, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbNoPrintDoc, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbNoInsertDoc, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbNoDogovorAccept, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbNoInshur, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbNoAcceptUsluga, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbDogovorNoAccept, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 108);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnOption3
            // 
            this.btnOption3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOption3.Location = new System.Drawing.Point(3, 3);
            this.btnOption3.Name = "btnOption3";
            this.btnOption3.Size = new System.Drawing.Size(257, 21);
            this.btnOption3.TabIndex = 0;
            this.btnOption3.Text = "Oпции , которые заканчиваются  за 3 часа";
            this.btnOption3.UseVisualStyleBackColor = true;
            this.btnOption3.Click += new System.EventHandler(this.btnOption3_Click);
            // 
            // tbOption3
            // 
            this.tbOption3.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tbOption3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOption3.Location = new System.Drawing.Point(266, 3);
            this.tbOption3.Name = "tbOption3";
            this.tbOption3.ReadOnly = true;
            this.tbOption3.Size = new System.Drawing.Size(44, 20);
            this.tbOption3.TabIndex = 7;
            // 
            // btnNoPrintDoc
            // 
            this.btnNoPrintDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoPrintDoc.Location = new System.Drawing.Point(316, 30);
            this.btnNoPrintDoc.Name = "btnNoPrintDoc";
            this.btnNoPrintDoc.Size = new System.Drawing.Size(257, 21);
            this.btnNoPrintDoc.TabIndex = 1;
            this.btnNoPrintDoc.Text = "Путевки с нераспечатаными документами";
            this.btnNoPrintDoc.UseVisualStyleBackColor = true;
            this.btnNoPrintDoc.Click += new System.EventHandler(this.btnNoPrintDoc_Click);
            // 
            // btnNoInsertDoc
            // 
            this.btnNoInsertDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoInsertDoc.Location = new System.Drawing.Point(3, 57);
            this.btnNoInsertDoc.Name = "btnNoInsertDoc";
            this.btnNoInsertDoc.Size = new System.Drawing.Size(257, 21);
            this.btnNoInsertDoc.TabIndex = 2;
            this.btnNoInsertDoc.Text = "Путевки, по которым не выложены документы";
            this.btnNoInsertDoc.UseVisualStyleBackColor = true;
            this.btnNoInsertDoc.Click += new System.EventHandler(this.btnNoInsertDoc_Click);
            // 
            // btnNoDogovorAccept
            // 
            this.btnNoDogovorAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoDogovorAccept.Location = new System.Drawing.Point(316, 3);
            this.btnNoDogovorAccept.Name = "btnNoDogovorAccept";
            this.btnNoDogovorAccept.Size = new System.Drawing.Size(257, 21);
            this.btnNoDogovorAccept.TabIndex = 3;
            this.btnNoDogovorAccept.Text = "Путевки, по которым договор не заключен";
            this.btnNoDogovorAccept.UseVisualStyleBackColor = true;
            this.btnNoDogovorAccept.Click += new System.EventHandler(this.btnNoDogovorAccept_Click);
            // 
            // btnNoInshur
            // 
            this.btnNoInshur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoInshur.Location = new System.Drawing.Point(3, 84);
            this.btnNoInshur.Name = "btnNoInshur";
            this.btnNoInshur.Size = new System.Drawing.Size(257, 21);
            this.btnNoInshur.TabIndex = 4;
            this.btnNoInshur.Text = "Путевки  с невыписанной страховкой";
            this.btnNoInshur.UseVisualStyleBackColor = true;
            this.btnNoInshur.Click += new System.EventHandler(this.btnNoInshur_Click);
            // 
            // btnNoAcceptUsluga
            // 
            this.btnNoAcceptUsluga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoAcceptUsluga.Location = new System.Drawing.Point(3, 30);
            this.btnNoAcceptUsluga.Name = "btnNoAcceptUsluga";
            this.btnNoAcceptUsluga.Size = new System.Drawing.Size(257, 21);
            this.btnNoAcceptUsluga.TabIndex = 5;
            this.btnNoAcceptUsluga.Text = "Путевки с неподтвержденной услугой";
            this.btnNoAcceptUsluga.UseVisualStyleBackColor = true;
            this.btnNoAcceptUsluga.Click += new System.EventHandler(this.btnNoAcceptUsluga_Click);
            // 
            // btnDogovorNoAccept
            // 
            this.btnDogovorNoAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDogovorNoAccept.Location = new System.Drawing.Point(316, 57);
            this.btnDogovorNoAccept.Name = "btnDogovorNoAccept";
            this.btnDogovorNoAccept.Size = new System.Drawing.Size(257, 21);
            this.btnDogovorNoAccept.TabIndex = 6;
            this.btnDogovorNoAccept.Text = "Бронь, требующая потверждения";
            this.btnDogovorNoAccept.UseVisualStyleBackColor = true;
            this.btnDogovorNoAccept.Visible = false;
            this.btnDogovorNoAccept.Click += new System.EventHandler(this.btnDogovorNoAccept_Click);
            // 
            // tbNoPrintDoc
            // 
            this.tbNoPrintDoc.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tbNoPrintDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNoPrintDoc.Location = new System.Drawing.Point(579, 30);
            this.tbNoPrintDoc.Name = "tbNoPrintDoc";
            this.tbNoPrintDoc.ReadOnly = true;
            this.tbNoPrintDoc.Size = new System.Drawing.Size(44, 20);
            this.tbNoPrintDoc.TabIndex = 8;
            // 
            // tbNoInsertDoc
            // 
            this.tbNoInsertDoc.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tbNoInsertDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNoInsertDoc.Location = new System.Drawing.Point(266, 57);
            this.tbNoInsertDoc.Name = "tbNoInsertDoc";
            this.tbNoInsertDoc.ReadOnly = true;
            this.tbNoInsertDoc.Size = new System.Drawing.Size(44, 20);
            this.tbNoInsertDoc.TabIndex = 9;
            // 
            // tbNoDogovorAccept
            // 
            this.tbNoDogovorAccept.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tbNoDogovorAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNoDogovorAccept.Location = new System.Drawing.Point(579, 3);
            this.tbNoDogovorAccept.Name = "tbNoDogovorAccept";
            this.tbNoDogovorAccept.ReadOnly = true;
            this.tbNoDogovorAccept.Size = new System.Drawing.Size(44, 20);
            this.tbNoDogovorAccept.TabIndex = 10;
            // 
            // tbNoInshur
            // 
            this.tbNoInshur.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tbNoInshur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNoInshur.Location = new System.Drawing.Point(266, 84);
            this.tbNoInshur.Name = "tbNoInshur";
            this.tbNoInshur.ReadOnly = true;
            this.tbNoInshur.Size = new System.Drawing.Size(44, 20);
            this.tbNoInshur.TabIndex = 11;
            // 
            // tbNoAcceptUsluga
            // 
            this.tbNoAcceptUsluga.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tbNoAcceptUsluga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNoAcceptUsluga.Location = new System.Drawing.Point(266, 30);
            this.tbNoAcceptUsluga.Name = "tbNoAcceptUsluga";
            this.tbNoAcceptUsluga.ReadOnly = true;
            this.tbNoAcceptUsluga.Size = new System.Drawing.Size(44, 20);
            this.tbNoAcceptUsluga.TabIndex = 12;
            // 
            // tbDogovorNoAccept
            // 
            this.tbDogovorNoAccept.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tbDogovorNoAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDogovorNoAccept.Location = new System.Drawing.Point(579, 57);
            this.tbDogovorNoAccept.Name = "tbDogovorNoAccept";
            this.tbDogovorNoAccept.ReadOnly = true;
            this.tbDogovorNoAccept.Size = new System.Drawing.Size(44, 20);
            this.tbDogovorNoAccept.TabIndex = 13;
            this.tbDogovorNoAccept.Visible = false;
            // 
            // timePulse
            // 
            this.timePulse.Interval = 200;
            this.timePulse.Tick += new System.EventHandler(this.timePulse_Tick);
            // 
            // frmProblemBron
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 108);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProblemBron";
            this.Text = "Проблемные брони";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnOption3;
        private System.Windows.Forms.Button btnNoPrintDoc;
        private System.Windows.Forms.Button btnNoInsertDoc;
        private System.Windows.Forms.Button btnNoInshur;
        private System.Windows.Forms.Button btnNoAcceptUsluga;
        private System.Windows.Forms.Button btnDogovorNoAccept;
        private System.Windows.Forms.TextBox tbOption3;
        private System.Windows.Forms.TextBox tbNoPrintDoc;
        private System.Windows.Forms.TextBox tbNoInsertDoc;
        private System.Windows.Forms.TextBox tbNoInshur;
        private System.Windows.Forms.TextBox tbNoAcceptUsluga;
        private System.Windows.Forms.TextBox tbDogovorNoAccept;
        private System.Windows.Forms.Timer timePulse;
        private System.Windows.Forms.Button btnNoDogovorAccept;
        private System.Windows.Forms.TextBox tbNoDogovorAccept;


    }
}