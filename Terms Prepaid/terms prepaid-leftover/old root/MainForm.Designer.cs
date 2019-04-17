namespace terms_prepaid
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvPaymants = new System.Windows.Forms.DataGridView();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.cbComissionType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOptionFiles = new System.Windows.Forms.Button();
            this.btnEditOption = new System.Windows.Forms.Button();
            this.btnAddOption = new System.Windows.Forms.Button();
            this.dgvOptions = new System.Windows.Forms.DataGridView();
            this.btnDelpp = new System.Windows.Forms.Button();
            this.btnDelP = new System.Windows.Forms.Button();
            this.dtPPaymanDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tbKomossion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtPaymand = new System.Windows.Forms.DateTimePicker();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymants)).BeginInit();
            this.gbInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPaymants
            // 
            this.dgvPaymants.AllowUserToAddRows = false;
            this.dgvPaymants.AllowUserToDeleteRows = false;
            this.dgvPaymants.AllowUserToOrderColumns = true;
            this.dgvPaymants.AllowUserToResizeRows = false;
            this.dgvPaymants.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvPaymants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaymants.Location = new System.Drawing.Point(12, 6);
            this.dgvPaymants.MultiSelect = false;
            this.dgvPaymants.Name = "dgvPaymants";
            this.dgvPaymants.ReadOnly = true;
            this.dgvPaymants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPaymants.Size = new System.Drawing.Size(387, 370);
            this.dgvPaymants.TabIndex = 0;
            this.dgvPaymants.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaymants_CellDoubleClick);
            // 
            // gbInfo
            // 
            this.gbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInfo.Controls.Add(this.cbComissionType);
            this.gbInfo.Controls.Add(this.label5);
            this.gbInfo.Controls.Add(this.btnOptionFiles);
            this.gbInfo.Controls.Add(this.btnEditOption);
            this.gbInfo.Controls.Add(this.btnAddOption);
            this.gbInfo.Controls.Add(this.dgvOptions);
            this.gbInfo.Controls.Add(this.btnDelpp);
            this.gbInfo.Controls.Add(this.btnDelP);
            this.gbInfo.Controls.Add(this.dtPPaymanDate);
            this.gbInfo.Controls.Add(this.label4);
            this.gbInfo.Controls.Add(this.tbKomossion);
            this.gbInfo.Controls.Add(this.label3);
            this.gbInfo.Controls.Add(this.dtPaymand);
            this.gbInfo.Controls.Add(this.btnOk);
            this.gbInfo.Controls.Add(this.btnCancel);
            this.gbInfo.Controls.Add(this.tbValue);
            this.gbInfo.Controls.Add(this.label2);
            this.gbInfo.Controls.Add(this.label1);
            this.gbInfo.Enabled = false;
            this.gbInfo.Location = new System.Drawing.Point(405, 12);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(395, 370);
            this.gbInfo.TabIndex = 1;
            this.gbInfo.TabStop = false;
            // 
            // cbComissionType
            // 
            this.cbComissionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComissionType.FormattingEnabled = true;
            this.cbComissionType.Items.AddRange(new object[] {
            "(EU\\USD)",
            "Процент"});
            this.cbComissionType.Location = new System.Drawing.Point(145, 106);
            this.cbComissionType.Name = "cbComissionType";
            this.cbComissionType.Size = new System.Drawing.Size(244, 21);
            this.cbComissionType.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Тип комиссии";
            // 
            // btnOptionFiles
            // 
            this.btnOptionFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOptionFiles.Location = new System.Drawing.Point(238, 309);
            this.btnOptionFiles.Name = "btnOptionFiles";
            this.btnOptionFiles.Size = new System.Drawing.Size(108, 23);
            this.btnOptionFiles.TabIndex = 16;
            this.btnOptionFiles.Text = "Файлы опции";
            this.btnOptionFiles.UseVisualStyleBackColor = true;
            this.btnOptionFiles.Click += new System.EventHandler(this.btnOptionFiles_Click);
            // 
            // btnEditOption
            // 
            this.btnEditOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditOption.Location = new System.Drawing.Point(122, 309);
            this.btnEditOption.Name = "btnEditOption";
            this.btnEditOption.Size = new System.Drawing.Size(108, 23);
            this.btnEditOption.TabIndex = 15;
            this.btnEditOption.Text = "Изменить опцию";
            this.btnEditOption.UseVisualStyleBackColor = true;
            this.btnEditOption.Click += new System.EventHandler(this.btnEditOption_Click);
            // 
            // btnAddOption
            // 
            this.btnAddOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddOption.Location = new System.Drawing.Point(6, 309);
            this.btnAddOption.Name = "btnAddOption";
            this.btnAddOption.Size = new System.Drawing.Size(108, 23);
            this.btnAddOption.TabIndex = 14;
            this.btnAddOption.Text = "Добавить  опцию";
            this.btnAddOption.UseVisualStyleBackColor = true;
            this.btnAddOption.Click += new System.EventHandler(this.btnAddOption_Click);
            // 
            // dgvOptions
            // 
            this.dgvOptions.AllowUserToAddRows = false;
            this.dgvOptions.AllowUserToDeleteRows = false;
            this.dgvOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOptions.Location = new System.Drawing.Point(6, 141);
            this.dgvOptions.Name = "dgvOptions";
            this.dgvOptions.ReadOnly = true;
            this.dgvOptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOptions.Size = new System.Drawing.Size(378, 162);
            this.dgvOptions.TabIndex = 13;
            // 
            // btnDelpp
            // 
            this.btnDelpp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelpp.Location = new System.Drawing.Point(296, 32);
            this.btnDelpp.Name = "btnDelpp";
            this.btnDelpp.Size = new System.Drawing.Size(93, 23);
            this.btnDelpp.TabIndex = 12;
            this.btnDelpp.Text = "Удалить";
            this.btnDelpp.UseVisualStyleBackColor = false;
            this.btnDelpp.Click += new System.EventHandler(this.btnDelpp_Click);
            // 
            // btnDelP
            // 
            this.btnDelP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelP.Location = new System.Drawing.Point(296, 7);
            this.btnDelP.Name = "btnDelP";
            this.btnDelP.Size = new System.Drawing.Size(93, 23);
            this.btnDelP.TabIndex = 11;
            this.btnDelP.Text = "Удалить";
            this.btnDelP.UseVisualStyleBackColor = true;
            this.btnDelP.Click += new System.EventHandler(this.btnDelP_Click);
            // 
            // dtPPaymanDate
            // 
            this.dtPPaymanDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPPaymanDate.Location = new System.Drawing.Point(145, 33);
            this.dtPPaymanDate.Name = "dtPPaymanDate";
            this.dtPPaymanDate.Size = new System.Drawing.Size(149, 20);
            this.dtPPaymanDate.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Срок оплаты";
            // 
            // tbKomossion
            // 
            this.tbKomossion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKomossion.Location = new System.Drawing.Point(145, 83);
            this.tbKomossion.Name = "tbKomossion";
            this.tbKomossion.Size = new System.Drawing.Size(244, 20);
            this.tbKomossion.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Комиссия";
            // 
            // dtPaymand
            // 
            this.dtPaymand.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPaymand.Location = new System.Drawing.Point(145, 10);
            this.dtPaymand.Name = "dtPaymand";
            this.dtPaymand.Size = new System.Drawing.Size(149, 20);
            this.dtPaymand.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(209, 341);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(81, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(294, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbValue
            // 
            this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbValue.Location = new System.Drawing.Point(145, 61);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(244, 20);
            this.tbValue.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Размер предоплаты";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Срок предоплаты";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 394);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.dgvPaymants);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymants)).EndInit();
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPaymants;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtPaymand;
        private System.Windows.Forms.TextBox tbKomossion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtPPaymanDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelpp;
        private System.Windows.Forms.Button btnDelP;
        private System.Windows.Forms.Button btnOptionFiles;
        private System.Windows.Forms.Button btnEditOption;
        private System.Windows.Forms.Button btnAddOption;
        private System.Windows.Forms.DataGridView dgvOptions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbComissionType;

    }
}

