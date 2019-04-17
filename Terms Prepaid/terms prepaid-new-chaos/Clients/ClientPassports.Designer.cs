namespace lanta.Clients
{
    partial class ClientPassports
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView_psp = new System.Windows.Forms.DataGridView();
            this.CP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLКеу = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLPASPORTSER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLPASPORTNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLNAMELAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLFNAMELAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLSNAMELAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLPASPORTDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLPASPORTDATEEND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_CLPASPORTBYWHOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CP_COMMENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button_Select = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.button_Edit = new System.Windows.Forms.Button();
            this.button_Del = new System.Windows.Forms.Button();
            this.label_Name = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_psp)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_psp, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_Name, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(971, 391);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView_psp
            // 
            this.dataGridView_psp.AllowUserToAddRows = false;
            this.dataGridView_psp.AllowUserToDeleteRows = false;
            this.dataGridView_psp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_psp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CP_ID,
            this.CP_CLКеу,
            this.CP_CLPASPORTSER,
            this.CP_CLPASPORTNUM,
            this.CP_CLNAMELAT,
            this.CP_CLFNAMELAT,
            this.CP_CLSNAMELAT,
            this.CP_CLPASPORTDATE,
            this.CP_CLPASPORTDATEEND,
            this.CP_CLPASPORTBYWHOM,
            this.CP_COMMENT});
            this.dataGridView_psp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_psp.Location = new System.Drawing.Point(3, 34);
            this.dataGridView_psp.Name = "dataGridView_psp";
            this.dataGridView_psp.ReadOnly = true;
            this.dataGridView_psp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_psp.Size = new System.Drawing.Size(965, 329);
            this.dataGridView_psp.TabIndex = 1;
            this.dataGridView_psp.DoubleClick += new System.EventHandler(this.dataGridView_psp_DoubleClick);
            // 
            // CP_ID
            // 
            this.CP_ID.DataPropertyName = "CP_ID";
            this.CP_ID.HeaderText = "Ключ записи";
            this.CP_ID.Name = "CP_ID";
            this.CP_ID.ReadOnly = true;
            this.CP_ID.Visible = false;
            // 
            // CP_CLКеу
            // 
            this.CP_CLКеу.DataPropertyName = "CP_CLКеу";
            this.CP_CLКеу.HeaderText = "Ключ клиента";
            this.CP_CLКеу.Name = "CP_CLКеу";
            this.CP_CLКеу.ReadOnly = true;
            this.CP_CLКеу.Visible = false;
            // 
            // CP_CLPASPORTSER
            // 
            this.CP_CLPASPORTSER.DataPropertyName = "CP_CLPASPORTSER";
            this.CP_CLPASPORTSER.HeaderText = "Серия паспорта";
            this.CP_CLPASPORTSER.Name = "CP_CLPASPORTSER";
            this.CP_CLPASPORTSER.ReadOnly = true;
            // 
            // CP_CLPASPORTNUM
            // 
            this.CP_CLPASPORTNUM.DataPropertyName = "CP_CLPASPORTNUM";
            this.CP_CLPASPORTNUM.HeaderText = "Номер паспорта";
            this.CP_CLPASPORTNUM.Name = "CP_CLPASPORTNUM";
            this.CP_CLPASPORTNUM.ReadOnly = true;
            // 
            // CP_CLNAMELAT
            // 
            this.CP_CLNAMELAT.DataPropertyName = "CP_CLNAMELAT";
            this.CP_CLNAMELAT.HeaderText = "Фамилия";
            this.CP_CLNAMELAT.Name = "CP_CLNAMELAT";
            this.CP_CLNAMELAT.ReadOnly = true;
            // 
            // CP_CLFNAMELAT
            // 
            this.CP_CLFNAMELAT.DataPropertyName = "CP_CLFNAMELAT";
            this.CP_CLFNAMELAT.HeaderText = "Имя";
            this.CP_CLFNAMELAT.Name = "CP_CLFNAMELAT";
            this.CP_CLFNAMELAT.ReadOnly = true;
            // 
            // CP_CLSNAMELAT
            // 
            this.CP_CLSNAMELAT.DataPropertyName = "CP_CLSNAMELAT";
            this.CP_CLSNAMELAT.HeaderText = "Отчество";
            this.CP_CLSNAMELAT.Name = "CP_CLSNAMELAT";
            this.CP_CLSNAMELAT.ReadOnly = true;
            // 
            // CP_CLPASPORTDATE
            // 
            this.CP_CLPASPORTDATE.DataPropertyName = "CP_CLPASPORTDATE";
            this.CP_CLPASPORTDATE.HeaderText = "Дата выдачи паспорта";
            this.CP_CLPASPORTDATE.Name = "CP_CLPASPORTDATE";
            this.CP_CLPASPORTDATE.ReadOnly = true;
            // 
            // CP_CLPASPORTDATEEND
            // 
            this.CP_CLPASPORTDATEEND.DataPropertyName = "CP_CLPASPORTDATEEND";
            this.CP_CLPASPORTDATEEND.HeaderText = "Окончание действия паспорта";
            this.CP_CLPASPORTDATEEND.Name = "CP_CLPASPORTDATEEND";
            this.CP_CLPASPORTDATEEND.ReadOnly = true;
            // 
            // CP_CLPASPORTBYWHOM
            // 
            this.CP_CLPASPORTBYWHOM.DataPropertyName = "CP_CLPASPORTBYWHOM";
            this.CP_CLPASPORTBYWHOM.HeaderText = "Кем выдан";
            this.CP_CLPASPORTBYWHOM.Name = "CP_CLPASPORTBYWHOM";
            this.CP_CLPASPORTBYWHOM.ReadOnly = true;
            // 
            // CP_COMMENT
            // 
            this.CP_COMMENT.DataPropertyName = "CP_COMMENT";
            this.CP_COMMENT.HeaderText = "Комментарий";
            this.CP_COMMENT.Name = "CP_COMMENT";
            this.CP_COMMENT.ReadOnly = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 503F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel2.Controls.Add(this.button_Select, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_Add, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_Edit, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_Del, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(971, 31);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // button_Select
            // 
            this.button_Select.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Select.Location = new System.Drawing.Point(3, 3);
            this.button_Select.Name = "button_Select";
            this.button_Select.Size = new System.Drawing.Size(98, 25);
            this.button_Select.TabIndex = 0;
            this.button_Select.Text = "Выбрать";
            this.button_Select.UseVisualStyleBackColor = true;
            this.button_Select.Click += new System.EventHandler(this.button_Select_Click);
            // 
            // button_Add
            // 
            this.button_Add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Add.Location = new System.Drawing.Point(107, 3);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(132, 25);
            this.button_Add.TabIndex = 1;
            this.button_Add.Text = "Добавить новый";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Edit
            // 
            this.button_Edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Edit.Location = new System.Drawing.Point(245, 3);
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size(110, 25);
            this.button_Edit.TabIndex = 1;
            this.button_Edit.Text = "Редактировать";
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Click += new System.EventHandler(this.button_Edit_Click);
            // 
            // button_Del
            // 
            this.button_Del.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Del.Location = new System.Drawing.Point(864, 3);
            this.button_Del.Name = "button_Del";
            this.button_Del.Size = new System.Drawing.Size(104, 25);
            this.button_Del.TabIndex = 1;
            this.button_Del.Text = "Удалить";
            this.button_Del.UseVisualStyleBackColor = true;
            this.button_Del.Click += new System.EventHandler(this.button_Del_Click);
            // 
            // label_Name
            // 
            this.label_Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(3, 372);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(73, 13);
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "Имя клиента";
            // 
            // ClientPassports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 391);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ClientPassports";
            this.Text = "Паспорта клиента";
            this.Load += new System.EventHandler(this.ClientPassports_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_psp)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.DataGridView dataGridView_psp;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLКеу;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLPASPORTSER;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLPASPORTNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLNAMELAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLFNAMELAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLSNAMELAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLPASPORTDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLPASPORTDATEEND;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_CLPASPORTBYWHOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CP_COMMENT;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_Edit;
        private System.Windows.Forms.Button button_Del;
    }
}