namespace terms_prepaid.UserControls
{
    partial class ucDogovorSetting
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpVisaDate = new System.Windows.Forms.DateTimePicker();
            this.dtpPrePaymentDate = new System.Windows.Forms.DateTimePicker();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lbPrePaydDateCaption = new System.Windows.Forms.Label();
            this.lbPaydDateCaption = new System.Windows.Forms.Label();
            this.lbPrePaydCaption = new System.Windows.Forms.Label();
            this.tbPrePaid = new System.Windows.Forms.TextBox();
            this.cbIsprocentPrePayd = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDiscount = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbStatuses = new System.Windows.Forms.ComboBox();
            this.cbIsProcentCommision = new System.Windows.Forms.CheckBox();
            this.gbPaydPreayd = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pPrePaydChange = new System.Windows.Forms.Panel();
            this.tbPrePaydSum = new System.Windows.Forms.TextBox();
            this.lbPrePaydEu = new System.Windows.Forms.Label();
            this.lbPrePaydProcent = new System.Windows.Forms.Label();
            this.mtbPaydTime = new System.Windows.Forms.MaskedTextBox();
            this.mtbPrePaydTime = new System.Windows.Forms.MaskedTextBox();
            this.pPrePaydView = new System.Windows.Forms.Panel();
            this.lbPriceM = new System.Windows.Forms.Label();
            this.lbPriceD = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.lbPrePayd = new System.Windows.Forms.Label();
            this.lbPaymentDate = new System.Windows.Forms.Label();
            this.lbPrePaydeDate = new System.Windows.Forms.Label();
            this.lbPaydCaption = new System.Windows.Forms.Label();
            this.lbPrePaydM = new System.Windows.Forms.Label();
            this.lbPaymentDateM = new System.Windows.Forms.Label();
            this.lbPPaymentDateM = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbPPaymentDateD = new System.Windows.Forms.Label();
            this.lbPaymentDateD = new System.Windows.Forms.Label();
            this.lbPrePaydD = new System.Windows.Forms.Label();
            this.lbDiscountM = new System.Windows.Forms.Label();
            this.lbDiscountD = new System.Windows.Forms.Label();
            this.gbStatuses = new System.Windows.Forms.GroupBox();
            this.pStatusChange = new System.Windows.Forms.Panel();
            this.btnSaveStatus = new System.Windows.Forms.Button();
            this.lbStatusesDateNew = new System.Windows.Forms.Label();
            this.pStatusView = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lbStatusM = new System.Windows.Forms.Label();
            this.lbStatusD = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.gbDiscount = new System.Windows.Forms.GroupBox();
            this.pDiscountChange = new System.Windows.Forms.Panel();
            this.tbDiscountSum = new System.Windows.Forms.TextBox();
            this.lbDiscountEu = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSaveDiscoount = new System.Windows.Forms.Button();
            this.pDiscountView = new System.Windows.Forms.Panel();
            this.lbDiscount = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbServices = new System.Windows.Forms.GroupBox();
            this.WpfServicesHost = new System.Windows.Forms.Integration.ElementHost();
            this.gbPaydPreayd.SuspendLayout();
            this.pPrePaydChange.SuspendLayout();
            this.pPrePaydView.SuspendLayout();
            this.gbStatuses.SuspendLayout();
            this.pStatusChange.SuspendLayout();
            this.pStatusView.SuspendLayout();
            this.gbDiscount.SuspendLayout();
            this.pDiscountChange.SuspendLayout();
            this.pDiscountView.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbServices.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpVisaDate
            // 
            this.dtpVisaDate.CustomFormat = "dd.MM.yy";
            this.dtpVisaDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVisaDate.Location = new System.Drawing.Point(228, 51);
            this.dtpVisaDate.Name = "dtpVisaDate";
            this.dtpVisaDate.ShowCheckBox = true;
            this.dtpVisaDate.Size = new System.Drawing.Size(95, 23);
            this.dtpVisaDate.TabIndex = 0;
            this.dtpVisaDate.Visible = false;
            // 
            // dtpPrePaymentDate
            // 
            this.dtpPrePaymentDate.CustomFormat = "dd.MM.yy";
            this.dtpPrePaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrePaymentDate.Location = new System.Drawing.Point(6, 5);
            this.dtpPrePaymentDate.Name = "dtpPrePaymentDate";
            this.dtpPrePaymentDate.ShowCheckBox = true;
            this.dtpPrePaymentDate.Size = new System.Drawing.Size(68, 20);
            this.dtpPrePaymentDate.TabIndex = 1;
            this.dtpPrePaymentDate.ValueChanged += new System.EventHandler(this.dtpPrePaymentDate_ValueChanged);
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.CustomFormat = "dd.MM.yy";
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPaymentDate.Location = new System.Drawing.Point(6, 25);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.ShowCheckBox = true;
            this.dtpPaymentDate.Size = new System.Drawing.Size(68, 20);
            this.dtpPaymentDate.TabIndex = 2;
            this.dtpPaymentDate.ValueChanged += new System.EventHandler(this.dtpPaymentDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Срок сдачи визовых докуметов";
            this.label1.Visible = false;
            // 
            // lbPrePaydDateCaption
            // 
            this.lbPrePaydDateCaption.AutoSize = true;
            this.lbPrePaydDateCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPrePaydDateCaption.ForeColor = System.Drawing.Color.Green;
            this.lbPrePaydDateCaption.Location = new System.Drawing.Point(3, 23);
            this.lbPrePaydDateCaption.Name = "lbPrePaydDateCaption";
            this.lbPrePaydDateCaption.Size = new System.Drawing.Size(83, 13);
            this.lbPrePaydDateCaption.TabIndex = 4;
            this.lbPrePaydDateCaption.Text = "Предоплата до";
            // 
            // lbPaydDateCaption
            // 
            this.lbPaydDateCaption.AutoSize = true;
            this.lbPaydDateCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPaydDateCaption.ForeColor = System.Drawing.Color.Green;
            this.lbPaydDateCaption.Location = new System.Drawing.Point(3, 43);
            this.lbPaydDateCaption.Name = "lbPaydDateCaption";
            this.lbPaydDateCaption.Size = new System.Drawing.Size(59, 13);
            this.lbPaydDateCaption.TabIndex = 5;
            this.lbPaydDateCaption.Text = "Оплата до";
            // 
            // lbPrePaydCaption
            // 
            this.lbPrePaydCaption.AutoSize = true;
            this.lbPrePaydCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPrePaydCaption.ForeColor = System.Drawing.Color.Green;
            this.lbPrePaydCaption.Location = new System.Drawing.Point(3, 61);
            this.lbPrePaydCaption.Name = "lbPrePaydCaption";
            this.lbPrePaydCaption.Size = new System.Drawing.Size(111, 13);
            this.lbPrePaydCaption.TabIndex = 6;
            this.lbPrePaydCaption.Text = "Сумма передоплаты";
            // 
            // tbPrePaid
            // 
            this.tbPrePaid.Location = new System.Drawing.Point(6, 46);
            this.tbPrePaid.Name = "tbPrePaid";
            this.tbPrePaid.Size = new System.Drawing.Size(41, 20);
            this.tbPrePaid.TabIndex = 7;
            this.tbPrePaid.Click += new System.EventHandler(this.tbPrePaid_Click);
            this.tbPrePaid.TextChanged += new System.EventHandler(this.tbPrePaid_TextChanged);
            this.tbPrePaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPrePaid_KeyPress);
            // 
            // cbIsprocentPrePayd
            // 
            this.cbIsprocentPrePayd.AutoSize = true;
            this.cbIsprocentPrePayd.Location = new System.Drawing.Point(135, 10);
            this.cbIsprocentPrePayd.Name = "cbIsprocentPrePayd";
            this.cbIsprocentPrePayd.Size = new System.Drawing.Size(34, 17);
            this.cbIsprocentPrePayd.TabIndex = 8;
            this.cbIsprocentPrePayd.Text = "%";
            this.cbIsprocentPrePayd.UseVisualStyleBackColor = true;
            this.cbIsprocentPrePayd.Visible = false;
            this.cbIsprocentPrePayd.CheckedChanged += new System.EventHandler(this.cbIsprocentPrePayd_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(3, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Скидка/коммисия";
            // 
            // tbDiscount
            // 
            this.tbDiscount.Location = new System.Drawing.Point(6, 8);
            this.tbDiscount.Name = "tbDiscount";
            this.tbDiscount.Size = new System.Drawing.Size(37, 20);
            this.tbDiscount.TabIndex = 11;
            this.tbDiscount.Click += new System.EventHandler(this.tbDiscount_Click);
            this.tbDiscount.TextChanged += new System.EventHandler(this.tbDiscount_TextChanged);
            this.tbDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPrePaid_KeyPress);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.Location = new System.Drawing.Point(157, 68);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // cbStatuses
            // 
            this.cbStatuses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatuses.FormattingEnabled = true;
            this.cbStatuses.Location = new System.Drawing.Point(6, 3);
            this.cbStatuses.Name = "cbStatuses";
            this.cbStatuses.Size = new System.Drawing.Size(164, 21);
            this.cbStatuses.TabIndex = 14;
            // 
            // cbIsProcentCommision
            // 
            this.cbIsProcentCommision.AutoSize = true;
            this.cbIsProcentCommision.Enabled = false;
            this.cbIsProcentCommision.Location = new System.Drawing.Point(249, 9);
            this.cbIsProcentCommision.Name = "cbIsProcentCommision";
            this.cbIsProcentCommision.Size = new System.Drawing.Size(34, 17);
            this.cbIsProcentCommision.TabIndex = 12;
            this.cbIsProcentCommision.Text = "%";
            this.cbIsProcentCommision.UseVisualStyleBackColor = true;
            this.cbIsProcentCommision.Visible = false;
            this.cbIsProcentCommision.CheckedChanged += new System.EventHandler(this.cbIsProcentCommision_CheckedChanged);
            // 
            // gbPaydPreayd
            // 
            this.gbPaydPreayd.Controls.Add(this.label12);
            this.gbPaydPreayd.Controls.Add(this.pPrePaydChange);
            this.gbPaydPreayd.Controls.Add(this.pPrePaydView);
            this.gbPaydPreayd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbPaydPreayd.Location = new System.Drawing.Point(3, 3);
            this.gbPaydPreayd.Name = "gbPaydPreayd";
            this.gbPaydPreayd.Size = new System.Drawing.Size(708, 134);
            this.gbPaydPreayd.TabIndex = 2;
            this.gbPaydPreayd.TabStop = false;
            this.gbPaydPreayd.Text = "Оплата\\предоплата";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(454, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Изменение статусов";
            // 
            // pPrePaydChange
            // 
            this.pPrePaydChange.BackColor = System.Drawing.Color.SandyBrown;
            this.pPrePaydChange.Controls.Add(this.tbPrePaydSum);
            this.pPrePaydChange.Controls.Add(this.lbPrePaydEu);
            this.pPrePaydChange.Controls.Add(this.lbPrePaydProcent);
            this.pPrePaydChange.Controls.Add(this.mtbPaydTime);
            this.pPrePaydChange.Controls.Add(this.mtbPrePaydTime);
            this.pPrePaydChange.Controls.Add(this.btnSave);
            this.pPrePaydChange.Controls.Add(this.dtpPrePaymentDate);
            this.pPrePaydChange.Controls.Add(this.dtpPaymentDate);
            this.pPrePaydChange.Controls.Add(this.tbPrePaid);
            this.pPrePaydChange.Controls.Add(this.cbIsprocentPrePayd);
            this.pPrePaydChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pPrePaydChange.Location = new System.Drawing.Point(370, 34);
            this.pPrePaydChange.Name = "pPrePaydChange";
            this.pPrePaydChange.Size = new System.Drawing.Size(326, 94);
            this.pPrePaydChange.TabIndex = 24;
            // 
            // tbPrePaydSum
            // 
            this.tbPrePaydSum.Location = new System.Drawing.Point(64, 46);
            this.tbPrePaydSum.Name = "tbPrePaydSum";
            this.tbPrePaydSum.Size = new System.Drawing.Size(48, 20);
            this.tbPrePaydSum.TabIndex = 23;
            this.tbPrePaydSum.Click += new System.EventHandler(this.tbPrePaydSum_Click);
            this.tbPrePaydSum.TextChanged += new System.EventHandler(this.tbPrePaydSum_TextChanged);
            this.tbPrePaydSum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPrePaid_KeyPress);
            // 
            // lbPrePaydEu
            // 
            this.lbPrePaydEu.AutoSize = true;
            this.lbPrePaydEu.Location = new System.Drawing.Point(114, 51);
            this.lbPrePaydEu.Name = "lbPrePaydEu";
            this.lbPrePaydEu.Size = new System.Drawing.Size(20, 13);
            this.lbPrePaydEu.TabIndex = 22;
            this.lbPrePaydEu.Text = "Eu";
            // 
            // lbPrePaydProcent
            // 
            this.lbPrePaydProcent.AutoSize = true;
            this.lbPrePaydProcent.Location = new System.Drawing.Point(47, 51);
            this.lbPrePaydProcent.Name = "lbPrePaydProcent";
            this.lbPrePaydProcent.Size = new System.Drawing.Size(15, 13);
            this.lbPrePaydProcent.TabIndex = 21;
            this.lbPrePaydProcent.Text = "%";
            // 
            // mtbPaydTime
            // 
            this.mtbPaydTime.Location = new System.Drawing.Point(80, 25);
            this.mtbPaydTime.Mask = "00:00";
            this.mtbPaydTime.Name = "mtbPaydTime";
            this.mtbPaydTime.Size = new System.Drawing.Size(32, 20);
            this.mtbPaydTime.TabIndex = 4;
            this.mtbPaydTime.ValidatingType = typeof(System.DateTime);
            // 
            // mtbPrePaydTime
            // 
            this.mtbPrePaydTime.Location = new System.Drawing.Point(80, 5);
            this.mtbPrePaydTime.Mask = "00:00";
            this.mtbPrePaydTime.Name = "mtbPrePaydTime";
            this.mtbPrePaydTime.Size = new System.Drawing.Size(32, 20);
            this.mtbPrePaydTime.TabIndex = 3;
            this.mtbPrePaydTime.ValidatingType = typeof(System.DateTime);
            // 
            // pPrePaydView
            // 
            this.pPrePaydView.Controls.Add(this.lbPriceM);
            this.pPrePaydView.Controls.Add(this.lbPriceD);
            this.pPrePaydView.Controls.Add(this.lbPrice);
            this.pPrePaydView.Controls.Add(this.lbPrePayd);
            this.pPrePaydView.Controls.Add(this.lbPaymentDate);
            this.pPrePaydView.Controls.Add(this.lbPrePaydeDate);
            this.pPrePaydView.Controls.Add(this.lbPaydCaption);
            this.pPrePaydView.Controls.Add(this.lbPrePaydDateCaption);
            this.pPrePaydView.Controls.Add(this.lbPrePaydM);
            this.pPrePaydView.Controls.Add(this.lbPrePaydCaption);
            this.pPrePaydView.Controls.Add(this.lbPaymentDateM);
            this.pPrePaydView.Controls.Add(this.lbPaydDateCaption);
            this.pPrePaydView.Controls.Add(this.lbPPaymentDateM);
            this.pPrePaydView.Controls.Add(this.label6);
            this.pPrePaydView.Controls.Add(this.label7);
            this.pPrePaydView.Controls.Add(this.lbPPaymentDateD);
            this.pPrePaydView.Controls.Add(this.lbPaymentDateD);
            this.pPrePaydView.Controls.Add(this.lbPrePaydD);
            this.pPrePaydView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pPrePaydView.Location = new System.Drawing.Point(6, 22);
            this.pPrePaydView.Name = "pPrePaydView";
            this.pPrePaydView.Size = new System.Drawing.Size(358, 106);
            this.pPrePaydView.TabIndex = 23;
            // 
            // lbPriceM
            // 
            this.lbPriceM.AutoSize = true;
            this.lbPriceM.ForeColor = System.Drawing.Color.Green;
            this.lbPriceM.Location = new System.Drawing.Point(292, 82);
            this.lbPriceM.Name = "lbPriceM";
            this.lbPriceM.Size = new System.Drawing.Size(13, 13);
            this.lbPriceM.TabIndex = 29;
            this.lbPriceM.Text = "2";
            // 
            // lbPriceD
            // 
            this.lbPriceD.AutoSize = true;
            this.lbPriceD.ForeColor = System.Drawing.Color.Green;
            this.lbPriceD.Location = new System.Drawing.Point(200, 84);
            this.lbPriceD.Name = "lbPriceD";
            this.lbPriceD.Size = new System.Drawing.Size(13, 13);
            this.lbPriceD.TabIndex = 28;
            this.lbPriceD.Text = "1";
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.ForeColor = System.Drawing.Color.Green;
            this.lbPrice.Location = new System.Drawing.Point(101, 82);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(13, 13);
            this.lbPrice.TabIndex = 27;
            this.lbPrice.Text = "0";
            // 
            // lbPrePayd
            // 
            this.lbPrePayd.AutoSize = true;
            this.lbPrePayd.ForeColor = System.Drawing.Color.Green;
            this.lbPrePayd.Location = new System.Drawing.Point(101, 60);
            this.lbPrePayd.Name = "lbPrePayd";
            this.lbPrePayd.Size = new System.Drawing.Size(13, 13);
            this.lbPrePayd.TabIndex = 26;
            this.lbPrePayd.Text = "0";
            // 
            // lbPaymentDate
            // 
            this.lbPaymentDate.AutoSize = true;
            this.lbPaymentDate.ForeColor = System.Drawing.Color.Green;
            this.lbPaymentDate.Location = new System.Drawing.Point(101, 42);
            this.lbPaymentDate.Name = "lbPaymentDate";
            this.lbPaymentDate.Size = new System.Drawing.Size(13, 13);
            this.lbPaymentDate.TabIndex = 25;
            this.lbPaymentDate.Text = "0";
            // 
            // lbPrePaydeDate
            // 
            this.lbPrePaydeDate.AutoSize = true;
            this.lbPrePaydeDate.ForeColor = System.Drawing.Color.Green;
            this.lbPrePaydeDate.Location = new System.Drawing.Point(101, 25);
            this.lbPrePaydeDate.Name = "lbPrePaydeDate";
            this.lbPrePaydeDate.Size = new System.Drawing.Size(13, 13);
            this.lbPrePaydeDate.TabIndex = 24;
            this.lbPrePaydeDate.Text = "0";
            // 
            // lbPaydCaption
            // 
            this.lbPaydCaption.AutoSize = true;
            this.lbPaydCaption.ForeColor = System.Drawing.Color.Green;
            this.lbPaydCaption.Location = new System.Drawing.Point(3, 83);
            this.lbPaydCaption.Name = "lbPaydCaption";
            this.lbPaydCaption.Size = new System.Drawing.Size(81, 13);
            this.lbPaydCaption.TabIndex = 23;
            this.lbPaydCaption.Text = "Сумма оплаты";
            // 
            // lbPrePaydM
            // 
            this.lbPrePaydM.AutoSize = true;
            this.lbPrePaydM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPrePaydM.ForeColor = System.Drawing.Color.Green;
            this.lbPrePaydM.Location = new System.Drawing.Point(292, 60);
            this.lbPrePaydM.Name = "lbPrePaydM";
            this.lbPrePaydM.Size = new System.Drawing.Size(13, 13);
            this.lbPrePaydM.TabIndex = 21;
            this.lbPrePaydM.Text = "2";
            // 
            // lbPaymentDateM
            // 
            this.lbPaymentDateM.AutoSize = true;
            this.lbPaymentDateM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPaymentDateM.ForeColor = System.Drawing.Color.Green;
            this.lbPaymentDateM.Location = new System.Drawing.Point(292, 42);
            this.lbPaymentDateM.Name = "lbPaymentDateM";
            this.lbPaymentDateM.Size = new System.Drawing.Size(13, 13);
            this.lbPaymentDateM.TabIndex = 20;
            this.lbPaymentDateM.Text = "2";
            // 
            // lbPPaymentDateM
            // 
            this.lbPPaymentDateM.AutoSize = true;
            this.lbPPaymentDateM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPPaymentDateM.ForeColor = System.Drawing.Color.Green;
            this.lbPPaymentDateM.Location = new System.Drawing.Point(292, 21);
            this.lbPPaymentDateM.Name = "lbPPaymentDateM";
            this.lbPPaymentDateM.Size = new System.Drawing.Size(13, 13);
            this.lbPPaymentDateM.TabIndex = 19;
            this.lbPPaymentDateM.Text = "2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(189, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Дата изменения";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(292, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Менеджер";
            // 
            // lbPPaymentDateD
            // 
            this.lbPPaymentDateD.AutoSize = true;
            this.lbPPaymentDateD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPPaymentDateD.ForeColor = System.Drawing.Color.Green;
            this.lbPPaymentDateD.Location = new System.Drawing.Point(200, 23);
            this.lbPPaymentDateD.Name = "lbPPaymentDateD";
            this.lbPPaymentDateD.Size = new System.Drawing.Size(13, 13);
            this.lbPPaymentDateD.TabIndex = 13;
            this.lbPPaymentDateD.Text = "1";
            // 
            // lbPaymentDateD
            // 
            this.lbPaymentDateD.AutoSize = true;
            this.lbPaymentDateD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPaymentDateD.ForeColor = System.Drawing.Color.Green;
            this.lbPaymentDateD.Location = new System.Drawing.Point(200, 41);
            this.lbPaymentDateD.Name = "lbPaymentDateD";
            this.lbPaymentDateD.Size = new System.Drawing.Size(13, 13);
            this.lbPaymentDateD.TabIndex = 14;
            this.lbPaymentDateD.Text = "1";
            // 
            // lbPrePaydD
            // 
            this.lbPrePaydD.AutoSize = true;
            this.lbPrePaydD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPrePaydD.ForeColor = System.Drawing.Color.Green;
            this.lbPrePaydD.Location = new System.Drawing.Point(200, 62);
            this.lbPrePaydD.Name = "lbPrePaydD";
            this.lbPrePaydD.Size = new System.Drawing.Size(13, 13);
            this.lbPrePaydD.TabIndex = 15;
            this.lbPrePaydD.Text = "1";
            // 
            // lbDiscountM
            // 
            this.lbDiscountM.AutoSize = true;
            this.lbDiscountM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbDiscountM.ForeColor = System.Drawing.Color.Green;
            this.lbDiscountM.Location = new System.Drawing.Point(292, 11);
            this.lbDiscountM.Name = "lbDiscountM";
            this.lbDiscountM.Size = new System.Drawing.Size(13, 13);
            this.lbDiscountM.TabIndex = 22;
            this.lbDiscountM.Text = "2";
            // 
            // lbDiscountD
            // 
            this.lbDiscountD.AutoSize = true;
            this.lbDiscountD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbDiscountD.ForeColor = System.Drawing.Color.Green;
            this.lbDiscountD.Location = new System.Drawing.Point(200, 13);
            this.lbDiscountD.Name = "lbDiscountD";
            this.lbDiscountD.Size = new System.Drawing.Size(13, 13);
            this.lbDiscountD.TabIndex = 16;
            this.lbDiscountD.Text = "1";
            // 
            // gbStatuses
            // 
            this.gbStatuses.Controls.Add(this.pStatusChange);
            this.gbStatuses.Controls.Add(this.pStatusView);
            this.gbStatuses.Controls.Add(this.label1);
            this.gbStatuses.Controls.Add(this.dtpVisaDate);
            this.gbStatuses.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbStatuses.Location = new System.Drawing.Point(3, 209);
            this.gbStatuses.Name = "gbStatuses";
            this.gbStatuses.Size = new System.Drawing.Size(705, 83);
            this.gbStatuses.TabIndex = 4;
            this.gbStatuses.TabStop = false;
            this.gbStatuses.Text = "Статусы";
            // 
            // pStatusChange
            // 
            this.pStatusChange.BackColor = System.Drawing.Color.SandyBrown;
            this.pStatusChange.Controls.Add(this.btnSaveStatus);
            this.pStatusChange.Controls.Add(this.lbStatusesDateNew);
            this.pStatusChange.Controls.Add(this.cbStatuses);
            this.pStatusChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pStatusChange.Location = new System.Drawing.Point(370, 16);
            this.pStatusChange.Name = "pStatusChange";
            this.pStatusChange.Size = new System.Drawing.Size(326, 61);
            this.pStatusChange.TabIndex = 1;
            // 
            // btnSaveStatus
            // 
            this.btnSaveStatus.BackColor = System.Drawing.Color.Green;
            this.btnSaveStatus.Location = new System.Drawing.Point(157, 33);
            this.btnSaveStatus.Name = "btnSaveStatus";
            this.btnSaveStatus.Size = new System.Drawing.Size(75, 23);
            this.btnSaveStatus.TabIndex = 17;
            this.btnSaveStatus.Text = "Сохранить";
            this.btnSaveStatus.UseVisualStyleBackColor = false;
            // 
            // lbStatusesDateNew
            // 
            this.lbStatusesDateNew.AutoSize = true;
            this.lbStatusesDateNew.Location = new System.Drawing.Point(176, 6);
            this.lbStatusesDateNew.Name = "lbStatusesDateNew";
            this.lbStatusesDateNew.Size = new System.Drawing.Size(13, 13);
            this.lbStatusesDateNew.TabIndex = 16;
            this.lbStatusesDateNew.Text = "3";
            this.lbStatusesDateNew.Visible = false;
            // 
            // pStatusView
            // 
            this.pStatusView.Controls.Add(this.label9);
            this.pStatusView.Controls.Add(this.lbStatusM);
            this.pStatusView.Controls.Add(this.lbStatusD);
            this.pStatusView.Controls.Add(this.lbStatus);
            this.pStatusView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pStatusView.Location = new System.Drawing.Point(6, 16);
            this.pStatusView.Name = "pStatusView";
            this.pStatusView.Size = new System.Drawing.Size(358, 29);
            this.pStatusView.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Green;
            this.label9.Location = new System.Drawing.Point(3, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Статус";
            // 
            // lbStatusM
            // 
            this.lbStatusM.AutoSize = true;
            this.lbStatusM.ForeColor = System.Drawing.Color.Green;
            this.lbStatusM.Location = new System.Drawing.Point(292, 6);
            this.lbStatusM.Name = "lbStatusM";
            this.lbStatusM.Size = new System.Drawing.Size(13, 13);
            this.lbStatusM.TabIndex = 17;
            this.lbStatusM.Text = "2";
            // 
            // lbStatusD
            // 
            this.lbStatusD.AutoSize = true;
            this.lbStatusD.ForeColor = System.Drawing.Color.Green;
            this.lbStatusD.Location = new System.Drawing.Point(200, 6);
            this.lbStatusD.Name = "lbStatusD";
            this.lbStatusD.Size = new System.Drawing.Size(13, 13);
            this.lbStatusD.TabIndex = 16;
            this.lbStatusD.Text = "1";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.ForeColor = System.Drawing.Color.Green;
            this.lbStatus.Location = new System.Drawing.Point(49, 6);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(13, 13);
            this.lbStatus.TabIndex = 15;
            this.lbStatus.Text = "0";
            // 
            // gbDiscount
            // 
            this.gbDiscount.Controls.Add(this.pDiscountChange);
            this.gbDiscount.Controls.Add(this.pDiscountView);
            this.gbDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbDiscount.Location = new System.Drawing.Point(3, 143);
            this.gbDiscount.Name = "gbDiscount";
            this.gbDiscount.Size = new System.Drawing.Size(708, 60);
            this.gbDiscount.TabIndex = 3;
            this.gbDiscount.TabStop = false;
            this.gbDiscount.Text = "Скидки";
            // 
            // pDiscountChange
            // 
            this.pDiscountChange.BackColor = System.Drawing.Color.SandyBrown;
            this.pDiscountChange.Controls.Add(this.tbDiscountSum);
            this.pDiscountChange.Controls.Add(this.lbDiscountEu);
            this.pDiscountChange.Controls.Add(this.label10);
            this.pDiscountChange.Controls.Add(this.btnSaveDiscoount);
            this.pDiscountChange.Controls.Add(this.tbDiscount);
            this.pDiscountChange.Controls.Add(this.cbIsProcentCommision);
            this.pDiscountChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pDiscountChange.Location = new System.Drawing.Point(370, 17);
            this.pDiscountChange.Name = "pDiscountChange";
            this.pDiscountChange.Size = new System.Drawing.Size(326, 37);
            this.pDiscountChange.TabIndex = 1;
            // 
            // tbDiscountSum
            // 
            this.tbDiscountSum.Location = new System.Drawing.Point(64, 8);
            this.tbDiscountSum.Name = "tbDiscountSum";
            this.tbDiscountSum.Size = new System.Drawing.Size(48, 20);
            this.tbDiscountSum.TabIndex = 23;
            this.tbDiscountSum.Click += new System.EventHandler(this.tbDiscountSum_Click);
            this.tbDiscountSum.TextChanged += new System.EventHandler(this.tbDiscountSum_TextChanged);
            this.tbDiscountSum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDiscountSum_KeyPress);
            // 
            // lbDiscountEu
            // 
            this.lbDiscountEu.AutoSize = true;
            this.lbDiscountEu.Location = new System.Drawing.Point(115, 13);
            this.lbDiscountEu.Name = "lbDiscountEu";
            this.lbDiscountEu.Size = new System.Drawing.Size(20, 13);
            this.lbDiscountEu.TabIndex = 22;
            this.lbDiscountEu.Text = "Eu";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "%";
            // 
            // btnSaveDiscoount
            // 
            this.btnSaveDiscoount.BackColor = System.Drawing.Color.Green;
            this.btnSaveDiscoount.Location = new System.Drawing.Point(157, 8);
            this.btnSaveDiscoount.Name = "btnSaveDiscoount";
            this.btnSaveDiscoount.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDiscoount.TabIndex = 13;
            this.btnSaveDiscoount.Text = "Сохранить";
            this.btnSaveDiscoount.UseVisualStyleBackColor = false;
            // 
            // pDiscountView
            // 
            this.pDiscountView.Controls.Add(this.lbDiscount);
            this.pDiscountView.Controls.Add(this.label5);
            this.pDiscountView.Controls.Add(this.lbDiscountD);
            this.pDiscountView.Controls.Add(this.lbDiscountM);
            this.pDiscountView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pDiscountView.Location = new System.Drawing.Point(6, 17);
            this.pDiscountView.Name = "pDiscountView";
            this.pDiscountView.Size = new System.Drawing.Size(358, 37);
            this.pDiscountView.TabIndex = 0;
            // 
            // lbDiscount
            // 
            this.lbDiscount.AutoSize = true;
            this.lbDiscount.ForeColor = System.Drawing.Color.Green;
            this.lbDiscount.Location = new System.Drawing.Point(101, 11);
            this.lbDiscount.Name = "lbDiscount";
            this.lbDiscount.Size = new System.Drawing.Size(13, 13);
            this.lbDiscount.TabIndex = 23;
            this.lbDiscount.Text = "0";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.gbPaydPreayd);
            this.flowLayoutPanel1.Controls.Add(this.gbDiscount);
            this.flowLayoutPanel1.Controls.Add(this.gbStatuses);
            this.flowLayoutPanel1.Controls.Add(this.gbServices);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(714, 526);
            this.flowLayoutPanel1.TabIndex = 5;
            this.flowLayoutPanel1.ClientSizeChanged += new System.EventHandler(this.flowLayoutPanel1_ClientSizeChanged);
            this.flowLayoutPanel1.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel1_ControlAdded);
            // 
            // gbServices
            // 
            this.gbServices.Controls.Add(this.WpfServicesHost);
            this.gbServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.gbServices.Location = new System.Drawing.Point(3, 298);
            this.gbServices.Name = "gbServices";
            this.gbServices.Size = new System.Drawing.Size(705, 224);
            this.gbServices.TabIndex = 5;
            this.gbServices.TabStop = false;
            this.gbServices.Text = "Услуги";
            // 
            // WpfServicesHost
            // 
            this.WpfServicesHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WpfServicesHost.Location = new System.Drawing.Point(3, 19);
            this.WpfServicesHost.Margin = new System.Windows.Forms.Padding(0);
            this.WpfServicesHost.Name = "WpfServicesHost";
            this.WpfServicesHost.Size = new System.Drawing.Size(699, 202);
            this.WpfServicesHost.TabIndex = 0;
            this.WpfServicesHost.Child = null;
            // 
            // ucDogovorSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ucDogovorSetting";
            this.Size = new System.Drawing.Size(717, 302);
            this.gbPaydPreayd.ResumeLayout(false);
            this.gbPaydPreayd.PerformLayout();
            this.pPrePaydChange.ResumeLayout(false);
            this.pPrePaydChange.PerformLayout();
            this.pPrePaydView.ResumeLayout(false);
            this.pPrePaydView.PerformLayout();
            this.gbStatuses.ResumeLayout(false);
            this.gbStatuses.PerformLayout();
            this.pStatusChange.ResumeLayout(false);
            this.pStatusChange.PerformLayout();
            this.pStatusView.ResumeLayout(false);
            this.pStatusView.PerformLayout();
            this.gbDiscount.ResumeLayout(false);
            this.pDiscountChange.ResumeLayout(false);
            this.pDiscountChange.PerformLayout();
            this.pDiscountView.ResumeLayout(false);
            this.pDiscountView.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbServices.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpVisaDate;
        private System.Windows.Forms.DateTimePicker dtpPrePaymentDate;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbPrePaydDateCaption;
        private System.Windows.Forms.Label lbPaydDateCaption;
        private System.Windows.Forms.Label lbPrePaydCaption;
        private System.Windows.Forms.TextBox tbPrePaid;
        private System.Windows.Forms.CheckBox cbIsprocentPrePayd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDiscount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbStatuses;
        private System.Windows.Forms.CheckBox cbIsProcentCommision;
        private System.Windows.Forms.GroupBox gbPaydPreayd;
        private System.Windows.Forms.GroupBox gbStatuses;
        private System.Windows.Forms.Label lbDiscountD;
        private System.Windows.Forms.Label lbPrePaydD;
        private System.Windows.Forms.Label lbPaymentDateD;
        private System.Windows.Forms.Label lbPPaymentDateD;
        private System.Windows.Forms.Label lbStatusesDateNew;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbDiscountM;
        private System.Windows.Forms.Label lbPrePaydM;
        private System.Windows.Forms.Label lbPaymentDateM;
        private System.Windows.Forms.Label lbPPaymentDateM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pPrePaydView;
        private System.Windows.Forms.GroupBox gbDiscount;
        private System.Windows.Forms.Panel pPrePaydChange;
        private System.Windows.Forms.Panel pDiscountChange;
        private System.Windows.Forms.Panel pDiscountView;
        private System.Windows.Forms.MaskedTextBox mtbPaydTime;
        private System.Windows.Forms.MaskedTextBox mtbPrePaydTime;
        private System.Windows.Forms.Label lbPaydCaption;
        private System.Windows.Forms.Panel pStatusChange;
        private System.Windows.Forms.Panel pStatusView;
        private System.Windows.Forms.Label lbPriceM;
        private System.Windows.Forms.Label lbPriceD;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Label lbPrePayd;
        private System.Windows.Forms.Label lbPaymentDate;
        private System.Windows.Forms.Label lbPrePaydeDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbStatusM;
        private System.Windows.Forms.Label lbStatusD;
        private System.Windows.Forms.Label lbDiscount;
        private System.Windows.Forms.Button btnSaveStatus;
        private System.Windows.Forms.Button btnSaveDiscoount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbPrePaydProcent;
        private System.Windows.Forms.Label lbPrePaydEu;
        private System.Windows.Forms.TextBox tbPrePaydSum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox tbDiscountSum;
        private System.Windows.Forms.Label lbDiscountEu;
        private System.Windows.Forms.GroupBox gbServices;
        private System.Windows.Forms.Integration.ElementHost WpfServicesHost;
    }
}
