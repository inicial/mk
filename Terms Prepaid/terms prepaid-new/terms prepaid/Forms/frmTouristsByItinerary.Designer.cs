namespace terms_prepaid.Forms
{
    partial class frmTouristsByItinerary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTouristsByItinerary));
            this.dgvTurists = new System.Windows.Forms.DataGridView();
            this.tbTurist = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbNeedCruise = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDgCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnParner = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTurists = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurists)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTurists
            // 
            this.dgvTurists.AllowUserToAddRows = false;
            this.dgvTurists.AllowUserToDeleteRows = false;
            this.dgvTurists.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTurists.BackgroundColor = System.Drawing.Color.White;
            this.dgvTurists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurists.Location = new System.Drawing.Point(15, 132);
            this.dgvTurists.MultiSelect = false;
            this.dgvTurists.Name = "dgvTurists";
            this.dgvTurists.ReadOnly = true;
            this.dgvTurists.RowHeadersVisible = false;
            this.dgvTurists.Size = new System.Drawing.Size(1400, 689);
            this.dgvTurists.TabIndex = 0;
            this.dgvTurists.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTurists_CellContentClick);
            this.dgvTurists.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTurists_CellFormatting);
            this.dgvTurists.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvTurists_CellPainting);
            this.dgvTurists.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvTurists_RowPostPaint);
            this.dgvTurists.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvTurists_RowPrePaint);
            // 
            // tbTurist
            // 
            this.tbTurist.Location = new System.Drawing.Point(12, 29);
            this.tbTurist.Name = "tbTurist";
            this.tbTurist.Size = new System.Drawing.Size(127, 20);
            this.tbTurist.TabIndex = 1;
            this.tbTurist.TextChanged += new System.EventHandler(this.tbTurist_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Поиск по туристу";
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "dd.MM.yy";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(145, 37);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(85, 20);
            this.dtpBegin.TabIndex = 3;
            this.dtpBegin.ValueChanged += new System.EventHandler(this.dtpBegin_ValueChanged);
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "dd.MM.yy";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(236, 37);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(85, 20);
            this.dtpEnd.TabIndex = 4;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpBegin_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Дата пребывания на маршруте";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "по";
            // 
            // cbNeedCruise
            // 
            this.cbNeedCruise.AutoSize = true;
            this.cbNeedCruise.Location = new System.Drawing.Point(148, 71);
            this.cbNeedCruise.Name = "cbNeedCruise";
            this.cbNeedCruise.Size = new System.Drawing.Size(103, 17);
            this.cbNeedCruise.TabIndex = 7;
            this.cbNeedCruise.Text = "Убрать круизы";
            this.cbNeedCruise.UseVisualStyleBackColor = true;
            this.cbNeedCruise.CheckedChanged += new System.EventHandler(this.cbNeedCruise_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Поиск по № путевки";
            // 
            // tbDgCode
            // 
            this.tbDgCode.Location = new System.Drawing.Point(12, 68);
            this.tbDgCode.Name = "tbDgCode";
            this.tbDgCode.Size = new System.Drawing.Size(127, 20);
            this.tbDgCode.TabIndex = 9;
            this.tbDgCode.TextChanged += new System.EventHandler(this.tbTurist_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "с";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.GreenYellow;
            this.btnSearch.Location = new System.Drawing.Point(145, 94);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(173, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Обновить";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnParner
            // 
            this.btnParner.BackColor = System.Drawing.Color.SandyBrown;
            this.btnParner.Location = new System.Drawing.Point(337, 9);
            this.btnParner.Name = "btnParner";
            this.btnParner.Size = new System.Drawing.Size(119, 37);
            this.btnParner.TabIndex = 12;
            this.btnParner.Text = "Срочный поиск партнера";
            this.btnParner.UseVisualStyleBackColor = false;
            this.btnParner.Click += new System.EventHandler(this.btnParner_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1337, 827);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Вернуться";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTurists
            // 
            this.btnTurists.Location = new System.Drawing.Point(337, 52);
            this.btnTurists.Name = "btnTurists";
            this.btnTurists.Size = new System.Drawing.Size(119, 36);
            this.btnTurists.TabIndex = 14;
            this.btnTurists.Text = "Срочный поиск туриста";
            this.btnTurists.UseVisualStyleBackColor = true;
            this.btnTurists.Click += new System.EventHandler(this.btnTurists_Click);
            // 
            // frmTouristsByItinerary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 862);
            this.Controls.Add(this.btnTurists);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnParner);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDgCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbNeedCruise);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpBegin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTurist);
            this.Controls.Add(this.dgvTurists);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTouristsByItinerary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Туристы на маршруте";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmTouristsByItinerary_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurists)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTurists;
        private System.Windows.Forms.TextBox tbTurist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbNeedCruise;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDgCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnParner;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTurists;
    }
}