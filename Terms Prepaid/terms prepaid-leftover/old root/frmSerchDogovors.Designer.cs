using System.Windows.Forms;

namespace terms_prepaid
{
    partial class frmSerchDogovors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSerchDogovors));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDogovor = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDateCreate = new System.Windows.Forms.CheckBox();
            this.dtpCreateDateS = new System.Windows.Forms.DateTimePicker();
            this.btnClients = new System.Windows.Forms.Button();
            this.dtpCreateDatePo = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.cbTurDate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpTurDatePo = new System.Windows.Forms.DateTimePicker();
            this.dtpTurDateS = new System.Windows.Forms.DateTimePicker();
            this.btnFind = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbdgcode = new System.Windows.Forms.TextBox();
            this.cbAnnul = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.clbClasses = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.lbBronir = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.lbRealizator = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.lbDogovor = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.lbVisa = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.lbDocument = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.btnProblemBrony = new System.Windows.Forms.Button();
            this.btnMyBrony = new System.Windows.Forms.Button();
            this.btnNewBrony = new System.Windows.Forms.Button();
            this.tbProblemBron = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnBronir = new System.Windows.Forms.Button();
            this.btnRealiz = new System.Windows.Forms.Button();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.lMain = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.timeRefreshProb = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDogovor)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dgvDogovor, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel13, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel15, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1417, 537);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // dgvDogovor
            // 
            this.dgvDogovor.AllowUserToAddRows = false;
            this.dgvDogovor.AllowUserToDeleteRows = false;
            this.dgvDogovor.AllowUserToResizeRows = false;
            this.dgvDogovor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDogovor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDogovor.Location = new System.Drawing.Point(3, 266);
            this.dgvDogovor.MultiSelect = false;
            this.dgvDogovor.Name = "dgvDogovor";
            this.dgvDogovor.ReadOnly = true;
            this.dgvDogovor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDogovor.Size = new System.Drawing.Size(1411, 268);
            this.dgvDogovor.TabIndex = 2;
            this.dgvDogovor.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDogovor_CellContentClick);
            this.dgvDogovor.DoubleClick += new System.EventHandler(this.dgvDogovor_DoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel8, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel9, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel10, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel11, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel12, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel14, 9, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 75);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1411, 154);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbDateCreate, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.dtpCreateDateS, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.btnClients, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.dtpCreateDatePo, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(161, 148);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(35, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "По дате создания";
            // 
            // cbDateCreate
            // 
            this.cbDateCreate.AutoSize = true;
            this.cbDateCreate.Location = new System.Drawing.Point(3, 3);
            this.cbDateCreate.Name = "cbDateCreate";
            this.cbDateCreate.Size = new System.Drawing.Size(15, 14);
            this.cbDateCreate.TabIndex = 12;
            this.cbDateCreate.UseVisualStyleBackColor = true;
            // 
            // dtpCreateDateS
            // 
            this.dtpCreateDateS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCreateDateS.Location = new System.Drawing.Point(35, 30);
            this.dtpCreateDateS.Name = "dtpCreateDateS";
            this.dtpCreateDateS.Size = new System.Drawing.Size(123, 20);
            this.dtpCreateDateS.TabIndex = 10;
            this.dtpCreateDateS.ValueChanged += new System.EventHandler(this.dtpCreateDateS_ValueChanged);
            // 
            // btnClients
            // 
            this.btnClients.BackColor = System.Drawing.Color.DarkGray;
            this.btnClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClients.Location = new System.Drawing.Point(35, 100);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(123, 45);
            this.btnClients.TabIndex = 17;
            this.btnClients.Text = "История клиента";
            this.btnClients.UseVisualStyleBackColor = false;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // dtpCreateDatePo
            // 
            this.dtpCreateDatePo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCreateDatePo.Location = new System.Drawing.Point(35, 57);
            this.dtpCreateDatePo.Name = "dtpCreateDatePo";
            this.dtpCreateDatePo.Size = new System.Drawing.Size(123, 20);
            this.dtpCreateDatePo.TabIndex = 0;
            this.dtpCreateDatePo.ValueChanged += new System.EventHandler(this.dtpCreateDatePo_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(3, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 27);
            this.label10.TabIndex = 15;
            this.label10.Text = "С";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 27);
            this.label1.TabIndex = 16;
            this.label1.Text = "По";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.cbTurDate, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.dtpTurDatePo, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.dtpTurDateS, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.btnFind, 1, 4);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(170, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 5;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(161, 148);
            this.tableLayoutPanel7.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(3, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 27);
            this.label11.TabIndex = 16;
            this.label11.Text = "С";
            // 
            // cbTurDate
            // 
            this.cbTurDate.AutoSize = true;
            this.cbTurDate.Location = new System.Drawing.Point(3, 3);
            this.cbTurDate.Name = "cbTurDate";
            this.cbTurDate.Size = new System.Drawing.Size(15, 14);
            this.cbTurDate.TabIndex = 13;
            this.cbTurDate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(35, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "По дате заезда";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(3, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 27);
            this.label4.TabIndex = 15;
            this.label4.Text = "По";
            // 
            // dtpTurDatePo
            // 
            this.dtpTurDatePo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTurDatePo.Location = new System.Drawing.Point(35, 57);
            this.dtpTurDatePo.Name = "dtpTurDatePo";
            this.dtpTurDatePo.Size = new System.Drawing.Size(123, 20);
            this.dtpTurDatePo.TabIndex = 3;
            this.dtpTurDatePo.ValueChanged += new System.EventHandler(this.dtpTurDatePo_ValueChanged);
            // 
            // dtpTurDateS
            // 
            this.dtpTurDateS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTurDateS.Location = new System.Drawing.Point(35, 30);
            this.dtpTurDateS.Name = "dtpTurDateS";
            this.dtpTurDateS.Size = new System.Drawing.Size(123, 20);
            this.dtpTurDateS.TabIndex = 7;
            this.dtpTurDateS.ValueChanged += new System.EventHandler(this.dtpTurDateS_ValueChanged);
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFind.Location = new System.Drawing.Point(35, 100);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(123, 45);
            this.btnFind.TabIndex = 24;
            this.btnFind.Text = "Обновить";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbdgcode, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.cbAnnul, 0, 3);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(337, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(127, 148);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "По номеру путевки";
            // 
            // tbdgcode
            // 
            this.tbdgcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbdgcode.Location = new System.Drawing.Point(3, 30);
            this.tbdgcode.Name = "tbdgcode";
            this.tbdgcode.Size = new System.Drawing.Size(121, 20);
            this.tbdgcode.TabIndex = 5;
            // 
            // cbAnnul
            // 
            this.cbAnnul.AutoSize = true;
            this.cbAnnul.Checked = true;
            this.cbAnnul.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAnnul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAnnul.Location = new System.Drawing.Point(3, 97);
            this.cbAnnul.Name = "cbAnnul";
            this.cbAnnul.Size = new System.Drawing.Size(121, 48);
            this.cbAnnul.TabIndex = 6;
            this.cbAnnul.Text = "Не показывать аннулированые";
            this.cbAnnul.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Controls.Add(this.clbClasses, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(470, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(109, 148);
            this.tableLayoutPanel8.TabIndex = 5;
            // 
            // clbClasses
            // 
            this.clbClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbClasses.FormattingEnabled = true;
            this.clbClasses.Location = new System.Drawing.Point(3, 30);
            this.clbClasses.Name = "clbClasses";
            this.clbClasses.Size = new System.Drawing.Size(103, 115);
            this.clbClasses.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 27);
            this.label9.TabIndex = 23;
            this.label9.Text = "По услугам";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.lbStatus, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(585, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(132, 148);
            this.tableLayoutPanel9.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "По статусу опции";
            // 
            // lbStatus
            // 
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStatus.FormattingEnabled = true;
            this.lbStatus.Location = new System.Drawing.Point(3, 30);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(126, 115);
            this.lbStatus.TabIndex = 23;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.lbBronir, 0, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(723, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(124, 148);
            this.tableLayoutPanel10.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 27);
            this.label6.TabIndex = 20;
            this.label6.Text = "По бронировщику";
            // 
            // lbBronir
            // 
            this.lbBronir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbBronir.FormattingEnabled = true;
            this.lbBronir.Location = new System.Drawing.Point(3, 30);
            this.lbBronir.Name = "lbBronir";
            this.lbBronir.Size = new System.Drawing.Size(118, 115);
            this.lbBronir.TabIndex = 21;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.lbRealizator, 0, 1);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(853, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(124, 148);
            this.tableLayoutPanel11.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 27);
            this.label7.TabIndex = 21;
            this.label7.Text = "По реализатору";
            // 
            // lbRealizator
            // 
            this.lbRealizator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRealizator.FormattingEnabled = true;
            this.lbRealizator.Location = new System.Drawing.Point(3, 30);
            this.lbRealizator.Name = "lbRealizator";
            this.lbRealizator.Size = new System.Drawing.Size(118, 115);
            this.lbRealizator.TabIndex = 22;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 1;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.lbDogovor, 0, 1);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(983, 3);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(124, 148);
            this.tableLayoutPanel12.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 27);
            this.label12.TabIndex = 0;
            this.label12.Text = "По статусу договора";
            // 
            // lbDogovor
            // 
            this.lbDogovor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDogovor.FormattingEnabled = true;
            this.lbDogovor.Location = new System.Drawing.Point(3, 30);
            this.lbDogovor.Name = "lbDogovor";
            this.lbDogovor.Size = new System.Drawing.Size(118, 115);
            this.lbDogovor.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbVisa, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1113, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(133, 148);
            this.tableLayoutPanel4.TabIndex = 10;
            this.tableLayoutPanel4.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 27);
            this.label13.TabIndex = 0;
            this.label13.Text = "По статусу визы";
            // 
            // lbVisa
            // 
            this.lbVisa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVisa.FormattingEnabled = true;
            this.lbVisa.Location = new System.Drawing.Point(3, 30);
            this.lbVisa.Name = "lbVisa";
            this.lbVisa.Size = new System.Drawing.Size(127, 115);
            this.lbVisa.TabIndex = 1;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.lbDocument, 0, 1);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(1252, 3);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 2;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(156, 148);
            this.tableLayoutPanel14.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(150, 27);
            this.label14.TabIndex = 0;
            this.label14.Text = "По статусу документов\r\n";
            // 
            // lbDocument
            // 
            this.lbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDocument.FormattingEnabled = true;
            this.lbDocument.Location = new System.Drawing.Point(3, 30);
            this.lbDocument.Name = "lbDocument";
            this.lbDocument.Size = new System.Drawing.Size(150, 115);
            this.lbDocument.TabIndex = 1;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 5;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.btnProblemBrony, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnMyBrony, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnNewBrony, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.tbProblemBron, 3, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 39);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(1411, 30);
            this.tableLayoutPanel13.TabIndex = 6;
            // 
            // btnProblemBrony
            // 
            this.btnProblemBrony.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProblemBrony.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnProblemBrony.Location = new System.Drawing.Point(263, 3);
            this.btnProblemBrony.Name = "btnProblemBrony";
            this.btnProblemBrony.Size = new System.Drawing.Size(172, 24);
            this.btnProblemBrony.TabIndex = 5;
            this.btnProblemBrony.Text = "Проблемные брони";
            this.btnProblemBrony.UseVisualStyleBackColor = true;
            this.btnProblemBrony.Click += new System.EventHandler(this.btnProblemBrony_Click);
            // 
            // btnMyBrony
            // 
            this.btnMyBrony.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMyBrony.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMyBrony.Location = new System.Drawing.Point(133, 3);
            this.btnMyBrony.Name = "btnMyBrony";
            this.btnMyBrony.Size = new System.Drawing.Size(124, 24);
            this.btnMyBrony.TabIndex = 4;
            this.btnMyBrony.Text = "Все мои брони";
            this.btnMyBrony.UseVisualStyleBackColor = true;
            this.btnMyBrony.Click += new System.EventHandler(this.btnMyBrony_Click);
            // 
            // btnNewBrony
            // 
            this.btnNewBrony.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewBrony.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewBrony.Location = new System.Drawing.Point(3, 3);
            this.btnNewBrony.Name = "btnNewBrony";
            this.btnNewBrony.Size = new System.Drawing.Size(124, 24);
            this.btnNewBrony.TabIndex = 3;
            this.btnNewBrony.Text = "Новые брони";
            this.btnNewBrony.UseVisualStyleBackColor = true;
            this.btnNewBrony.Click += new System.EventHandler(this.btnNewBrony_Click);
            // 
            // tbProblemBron
            // 
            this.tbProblemBron.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbProblemBron.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProblemBron.Location = new System.Drawing.Point(441, 3);
            this.tbProblemBron.Name = "tbProblemBron";
            this.tbProblemBron.ReadOnly = true;
            this.tbProblemBron.Size = new System.Drawing.Size(44, 20);
            this.tbProblemBron.TabIndex = 6;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 235F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btnSetting, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnBronir, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRealiz, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 235);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1411, 25);
            this.tableLayoutPanel3.TabIndex = 7;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetting.Location = new System.Drawing.Point(1179, 3);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(229, 19);
            this.btnSetting.TabIndex = 26;
            this.btnSetting.Text = "Назначить бронировщика \\реализатора";
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnBronir
            // 
            this.btnBronir.BackColor = System.Drawing.Color.Chocolate;
            this.btnBronir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBronir.Location = new System.Drawing.Point(979, 3);
            this.btnBronir.Name = "btnBronir";
            this.btnBronir.Size = new System.Drawing.Size(194, 19);
            this.btnBronir.TabIndex = 1;
            this.btnBronir.Text = "Назначить меня бронировщиком";
            this.btnBronir.UseVisualStyleBackColor = false;
            this.btnBronir.Click += new System.EventHandler(this.btnBronir_Click);
            // 
            // btnRealiz
            // 
            this.btnRealiz.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRealiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRealiz.Location = new System.Drawing.Point(779, 3);
            this.btnRealiz.Name = "btnRealiz";
            this.btnRealiz.Size = new System.Drawing.Size(194, 19);
            this.btnRealiz.TabIndex = 2;
            this.btnRealiz.Text = "Назначить меня реализатором";
            this.btnRealiz.UseVisualStyleBackColor = false;
            this.btnRealiz.Click += new System.EventHandler(this.btnRealiz_Click);
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Controls.Add(this.lMain, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.tbName, 1, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(1411, 30);
            this.tableLayoutPanel15.TabIndex = 8;
            // 
            // lMain
            // 
            this.lMain.AutoSize = true;
            this.lMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.lMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lMain.Location = new System.Drawing.Point(702, 0);
            this.lMain.Name = "lMain";
            this.lMain.Size = new System.Drawing.Size(0, 30);
            this.lMain.TabIndex = 5;
            this.lMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbName
            // 
            this.tbName.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbName.Location = new System.Drawing.Point(708, 3);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(165, 26);
            this.tbName.TabIndex = 6;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // timeRefreshProb
            // 
            this.timeRefreshProb.Enabled = true;
            this.timeRefreshProb.Interval = 300000;
            this.timeRefreshProb.Tick += new System.EventHandler(this.timeRefreshProb_Tick);
            // 
            // frmSerchDogovors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 537);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSerchDogovors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рабочее место ";
            this.Load += new System.EventHandler(this.frmSerchDogovors_Load);
            this.Move += new System.EventHandler(this.frmSerchDogovors_Move);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDogovor)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel2;
        private DateTimePicker dtpCreateDateS;
        private DateTimePicker dtpTurDateS;
        private TextBox tbdgcode;
        private Label label3;
        private DateTimePicker dtpTurDatePo;
        private Label label2;
        private DateTimePicker dtpCreateDatePo;
        private DataGridView dgvDogovor;
        private CheckedListBox clbClasses;
        private Label label4;
        private CheckBox cbTurDate;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label5;
        private CheckBox cbDateCreate;
        private Label label10;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label11;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private ListBox lbStatus;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel11;
        private ListBox lbBronir;
        private ListBox lbRealizator;
        private TableLayoutPanel tableLayoutPanel12;
        private Label label12;
        private ListBox lbDogovor;
        private CheckBox cbAnnul;
        private TableLayoutPanel tableLayoutPanel13;
        private Button btnProblemBrony;
        private Button btnMyBrony;
        private Button btnNewBrony;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnRealiz;
        private Button btnBronir;
        private Button btnFind;
        private Button btnSetting;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label13;
        private ListBox lbVisa;
        private TableLayoutPanel tableLayoutPanel14;
        private Label label14;
        private ListBox lbDocument;
        private TableLayoutPanel tableLayoutPanel15;
        private Label lMain;
        private TextBox tbName;
        private Button btnClients;
        private Timer timeRefreshProb;
        private TextBox tbProblemBron;


    }
}