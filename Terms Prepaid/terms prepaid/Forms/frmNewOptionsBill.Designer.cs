namespace terms_prepaid.Forms
{
    partial class frmNewOptionsBill
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txt_BillNumber = new System.Windows.Forms.TextBox();
            this.txt_Summa = new System.Windows.Forms.TextBox();
            this.txt_SummaDeposit = new System.Windows.Forms.TextBox();
            this.txt_SummaRest = new System.Windows.Forms.TextBox();
            this.txt_RegNumber = new System.Windows.Forms.TextBox();
            this.dt_DatePayDeposit = new System.Windows.Forms.DateTimePicker();
            this.dt_DatePayRest = new System.Windows.Forms.DateTimePicker();
            this.btn_Search = new System.Windows.Forms.Button();
            this.txt_Status = new System.Windows.Forms.TextBox();
            this.chk_New = new System.Windows.Forms.CheckBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.txt_Dogovor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_FileName = new System.Windows.Forms.TextBox();
            this.txt_FilePath = new System.Windows.Forms.TextBox();
            this.btn_ShowFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(35, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер  счета  партнера";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(35, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Сумма  к  оплате";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(35, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Сумма  предоплаты";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(35, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Оплатить  к  дате";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(35, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Сумма  конечной  оплаты";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(35, 306);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Оплатить  к  дате";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(35, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Регистрационный  №  письма";
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(251)))), ((int)(((byte)(156)))));
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Save.Location = new System.Drawing.Point(426, 181);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(170, 26);
            this.btn_Save.TabIndex = 32;
            this.btn_Save.Text = "Подтвердить";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Cancel.Location = new System.Drawing.Point(426, 241);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(170, 26);
            this.btn_Cancel.TabIndex = 33;
            this.btn_Cancel.Text = "Отменить";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // txt_BillNumber
            // 
            this.txt_BillNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_BillNumber.Location = new System.Drawing.Point(244, 153);
            this.txt_BillNumber.Name = "txt_BillNumber";
            this.txt_BillNumber.Size = new System.Drawing.Size(150, 23);
            this.txt_BillNumber.TabIndex = 22;
            this.txt_BillNumber.TextChanged += new System.EventHandler(this.DataEditingHandler);
            // 
            // txt_Summa
            // 
            this.txt_Summa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_Summa.Location = new System.Drawing.Point(244, 183);
            this.txt_Summa.Name = "txt_Summa";
            this.txt_Summa.Size = new System.Drawing.Size(150, 23);
            this.txt_Summa.TabIndex = 23;
            this.txt_Summa.TextChanged += new System.EventHandler(this.DataEditingHandler);
            // 
            // txt_SummaDeposit
            // 
            this.txt_SummaDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_SummaDeposit.Location = new System.Drawing.Point(244, 213);
            this.txt_SummaDeposit.Name = "txt_SummaDeposit";
            this.txt_SummaDeposit.Size = new System.Drawing.Size(150, 23);
            this.txt_SummaDeposit.TabIndex = 24;
            this.txt_SummaDeposit.TextChanged += new System.EventHandler(this.DataEditingHandler);
            // 
            // txt_SummaRest
            // 
            this.txt_SummaRest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_SummaRest.Location = new System.Drawing.Point(244, 273);
            this.txt_SummaRest.Name = "txt_SummaRest";
            this.txt_SummaRest.Size = new System.Drawing.Size(150, 23);
            this.txt_SummaRest.TabIndex = 26;
            this.txt_SummaRest.TextChanged += new System.EventHandler(this.DataEditingHandler);
            // 
            // txt_RegNumber
            // 
            this.txt_RegNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_RegNumber.Location = new System.Drawing.Point(244, 31);
            this.txt_RegNumber.Name = "txt_RegNumber";
            this.txt_RegNumber.Size = new System.Drawing.Size(150, 23);
            this.txt_RegNumber.TabIndex = 21;
            this.txt_RegNumber.TextChanged += new System.EventHandler(this.txt_RegNumber_TextChanged);
            // 
            // dt_DatePayDeposit
            // 
            this.dt_DatePayDeposit.CustomFormat = "dd.MM.yyyy";
            this.dt_DatePayDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dt_DatePayDeposit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_DatePayDeposit.Location = new System.Drawing.Point(244, 243);
            this.dt_DatePayDeposit.MaxDate = new System.DateTime(2199, 12, 31, 0, 0, 0, 0);
            this.dt_DatePayDeposit.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dt_DatePayDeposit.Name = "dt_DatePayDeposit";
            this.dt_DatePayDeposit.Size = new System.Drawing.Size(150, 23);
            this.dt_DatePayDeposit.TabIndex = 25;
            this.dt_DatePayDeposit.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dt_DatePayDeposit.ValueChanged += new System.EventHandler(this.DataEditingHandler);
            // 
            // dt_DatePayRest
            // 
            this.dt_DatePayRest.CustomFormat = "dd.MM.yyyy";
            this.dt_DatePayRest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dt_DatePayRest.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_DatePayRest.Location = new System.Drawing.Point(244, 303);
            this.dt_DatePayRest.MaxDate = new System.DateTime(2199, 12, 31, 0, 0, 0, 0);
            this.dt_DatePayRest.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dt_DatePayRest.Name = "dt_DatePayRest";
            this.dt_DatePayRest.Size = new System.Drawing.Size(150, 23);
            this.dt_DatePayRest.TabIndex = 27;
            this.dt_DatePayRest.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dt_DatePayRest.ValueChanged += new System.EventHandler(this.DataEditingHandler);
            // 
            // btn_Search
            // 
            this.btn_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Search.Location = new System.Drawing.Point(426, 29);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(170, 26);
            this.btn_Search.TabIndex = 31;
            this.btn_Search.Text = "Найти  счет";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // txt_Status
            // 
            this.txt_Status.BackColor = System.Drawing.Color.Azure;
            this.txt_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_Status.Location = new System.Drawing.Point(30, 65);
            this.txt_Status.Multiline = true;
            this.txt_Status.Name = "txt_Status";
            this.txt_Status.ReadOnly = true;
            this.txt_Status.Size = new System.Drawing.Size(566, 46);
            this.txt_Status.TabIndex = 29;
            this.txt_Status.TabStop = false;
            // 
            // chk_New
            // 
            this.chk_New.AutoSize = true;
            this.chk_New.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chk_New.Location = new System.Drawing.Point(426, 125);
            this.chk_New.Name = "chk_New";
            this.chk_New.Size = new System.Drawing.Size(173, 21);
            this.chk_New.TabIndex = 30;
            this.chk_New.Text = "внести  новую  запись";
            this.chk_New.UseVisualStyleBackColor = true;
            this.chk_New.CheckedChanged += new System.EventHandler(this.chk_New_CheckedChanged);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Clear.Location = new System.Drawing.Point(426, 211);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(170, 26);
            this.btn_Clear.TabIndex = 34;
            this.btn_Clear.Text = "Очистить";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // txt_Dogovor
            // 
            this.txt_Dogovor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_Dogovor.Location = new System.Drawing.Point(244, 123);
            this.txt_Dogovor.Name = "txt_Dogovor";
            this.txt_Dogovor.ReadOnly = true;
            this.txt_Dogovor.Size = new System.Drawing.Size(150, 23);
            this.txt_Dogovor.TabIndex = 36;
            this.txt_Dogovor.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(35, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 17);
            this.label8.TabIndex = 35;
            this.label8.Text = "Номер брони  ( договор )";
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_SelectFile.Location = new System.Drawing.Point(426, 361);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(170, 26);
            this.btn_SelectFile.TabIndex = 37;
            this.btn_SelectFile.Text = "Выбрать  файл ";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(35, 336);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 17);
            this.label9.TabIndex = 38;
            this.label9.Text = "Файл  счета ";
            // 
            // txt_FileName
            // 
            this.txt_FileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_FileName.Location = new System.Drawing.Point(244, 333);
            this.txt_FileName.Name = "txt_FileName";
            this.txt_FileName.ReadOnly = true;
            this.txt_FileName.Size = new System.Drawing.Size(150, 23);
            this.txt_FileName.TabIndex = 39;
            this.txt_FileName.TabStop = false;
            // 
            // txt_FilePath
            // 
            this.txt_FilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_FilePath.Location = new System.Drawing.Point(30, 363);
            this.txt_FilePath.Name = "txt_FilePath";
            this.txt_FilePath.ReadOnly = true;
            this.txt_FilePath.Size = new System.Drawing.Size(364, 23);
            this.txt_FilePath.TabIndex = 40;
            this.txt_FilePath.TabStop = false;
            // 
            // btn_ShowFile
            // 
            this.btn_ShowFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_ShowFile.Location = new System.Drawing.Point(426, 331);
            this.btn_ShowFile.Name = "btn_ShowFile";
            this.btn_ShowFile.Size = new System.Drawing.Size(170, 26);
            this.btn_ShowFile.TabIndex = 41;
            this.btn_ShowFile.Text = "Открыть  файл ";
            this.btn_ShowFile.UseVisualStyleBackColor = true;
            this.btn_ShowFile.Click += new System.EventHandler(this.btn_ShowFile_Click);
            // 
            // frmNewOptionsBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(628, 414);
            this.Controls.Add(this.btn_ShowFile);
            this.Controls.Add(this.txt_FilePath);
            this.Controls.Add(this.txt_FileName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btn_SelectFile);
            this.Controls.Add(this.txt_Dogovor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.chk_New);
            this.Controls.Add(this.txt_Status);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.dt_DatePayRest);
            this.Controls.Add(this.dt_DatePayDeposit);
            this.Controls.Add(this.txt_RegNumber);
            this.Controls.Add(this.txt_SummaRest);
            this.Controls.Add(this.txt_SummaDeposit);
            this.Controls.Add(this.txt_Summa);
            this.Controls.Add(this.txt_BillNumber);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewOptionsBill";
            this.Text = "Счет";
            this.Deactivate += new System.EventHandler(this.frmNewOptionsBill_Deactivate);
            this.Load += new System.EventHandler(this.frmNewOptionsBill_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txt_BillNumber;
        private System.Windows.Forms.TextBox txt_Summa;
        private System.Windows.Forms.TextBox txt_SummaDeposit;
        private System.Windows.Forms.TextBox txt_SummaRest;
        private System.Windows.Forms.TextBox txt_RegNumber;
        private System.Windows.Forms.DateTimePicker dt_DatePayDeposit;
        private System.Windows.Forms.DateTimePicker dt_DatePayRest;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox txt_Status;
        private System.Windows.Forms.CheckBox chk_New;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.TextBox txt_Dogovor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_FileName;
        private System.Windows.Forms.TextBox txt_FilePath;
        private System.Windows.Forms.Button btn_ShowFile;
    }
}