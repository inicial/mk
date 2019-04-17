using System.Windows.Forms;

namespace terms_prepaid
{
    partial class frmNewOptions
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
            Awesomium.Core.WebPreferences webPreferences1 = new Awesomium.Core.WebPreferences(true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewOptions));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tcpCruise = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpDataOfBronCruise = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCabinNomber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpOption = new System.Windows.Forms.DateTimePicker();
            this.mtbTime = new System.Windows.Forms.MaskedTextBox();
            this.cbCabinDef = new System.Windows.Forms.ComboBox();
            this.tbNomberOptions = new System.Windows.Forms.TextBox();
            this.tbSpecCanc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbCabinCategory = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.cbIsBook = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbDocumentGet = new System.Windows.Forms.CheckBox();
            this.cbDocumentQuery = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbCruises = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tlpCruiseBl1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.tlpBonusCruise = new System.Windows.Forms.TableLayoutPanel();
            this.tcpHotel = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.cbHotels = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tlpHotels = new System.Windows.Forms.TableLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.cbHotelOk = new System.Windows.Forms.CheckBox();
            this.btnHotelOk = new System.Windows.Forms.Button();
            this.tcpInshur = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.cbInshur = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.cbInshurCreate = new System.Windows.Forms.CheckBox();
            this.btnOkInshur = new System.Windows.Forms.Button();
            this.btnInshur = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tlpInshur = new System.Windows.Forms.TableLayoutPanel();
            this.tcpVisa = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbVisa = new System.Windows.Forms.ComboBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.label24 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.tcpDopPaket = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.cbDopPaket = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tlpDopPaket = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tcpAvia = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.cbAvia = new System.Windows.Forms.ComboBox();
            this.rtbAvia = new System.Windows.Forms.RichTextBox();
            this.tcpOther = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.cbOther = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tlpOther = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.tcPartnerPutevka = new System.Windows.Forms.TabControl();
            this.tcpPartner = new System.Windows.Forms.TabPage();
            this.tlpWebBrowser = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEnter = new System.Windows.Forms.Button();
            this.tbAdress = new System.Windows.Forms.TextBox();
            this.wbBook = new Awesomium.Windows.Forms.WebControl(this.components);
            this.tcpPutevka = new System.Windows.Forms.TabPage();
            this.tlpPutevka = new System.Windows.Forms.TableLayoutPanel();
            this.scPutevka = new System.Windows.Forms.SplitContainer();
            this.dgvPutevka = new System.Windows.Forms.DataGridView();
            this.tcProblemChanges = new System.Windows.Forms.TabControl();
            this.tcpProblem = new System.Windows.Forms.TabPage();
            this.dgvProblems = new System.Windows.Forms.DataGridView();
            this.tcpChanges = new System.Windows.Forms.TabPage();
            this.dgvChanges = new System.Windows.Forms.DataGridView();
            this.tcpItinerary = new System.Windows.Forms.TabPage();
            this.dgvItinerary = new System.Windows.Forms.DataGridView();
            this.tcpSettings = new System.Windows.Forms.TabPage();
            this.tcpMessages = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tbDGcod = new System.Windows.Forms.TextBox();
            this.tbDateBegin = new System.Windows.Forms.TextBox();
            this.tbDateEnd = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tbUslug = new System.Windows.Forms.TextBox();
            this.tbAccept = new System.Windows.Forms.TextBox();
            this.tbNoAccept = new System.Windows.Forms.TextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.tlpTurists = new System.Windows.Forms.TableLayoutPanel();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.tbPax = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnDocumenSettings = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.btnDogovor = new System.Windows.Forms.Button();
            this.btnExtend = new System.Windows.Forms.Button();
            this.btnRegion = new System.Windows.Forms.Button();
            this.btnDownloadedFiles = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tlpCruisePrice = new System.Windows.Forms.TableLayoutPanel();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.wspBook = new Awesomium.Windows.Forms.WebSessionProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tcOptions.SuspendLayout();
            this.tcpCruise.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpDataOfBronCruise.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tcpHotel.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tcpInshur.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tcpVisa.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel11.SuspendLayout();
            this.tcpDopPaket.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tcpAvia.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.tcpOther.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel12.SuspendLayout();
            this.tcPartnerPutevka.SuspendLayout();
            this.tcpPartner.SuspendLayout();
            this.tlpWebBrowser.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tcpPutevka.SuspendLayout();
            this.tlpPutevka.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scPutevka)).BeginInit();
            this.scPutevka.Panel1.SuspendLayout();
            this.scPutevka.Panel2.SuspendLayout();
            this.scPutevka.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPutevka)).BeginInit();
            this.tcProblemChanges.SuspendLayout();
            this.tcpProblem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblems)).BeginInit();
            this.tcpChanges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChanges)).BeginInit();
            this.tcpItinerary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItinerary)).BeginInit();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tlpTurists.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 416F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tcOptions, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel12, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label20, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel15, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel16, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel10, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel17, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1424, 862);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tcpCruise);
            this.tcOptions.Controls.Add(this.tcpHotel);
            this.tcOptions.Controls.Add(this.tcpInshur);
            this.tcOptions.Controls.Add(this.tcpVisa);
            this.tcOptions.Controls.Add(this.tcpDopPaket);
            this.tcOptions.Controls.Add(this.tcpAvia);
            this.tcOptions.Controls.Add(this.tcpOther);
            this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOptions.Location = new System.Drawing.Point(4, 187);
            this.tcOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(408, 671);
            this.tcOptions.TabIndex = 0;
            this.tcOptions.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcOptions_Selected);
            // 
            // tcpCruise
            // 
            this.tcpCruise.Controls.Add(this.tableLayoutPanel2);
            this.tcpCruise.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tcpCruise.Location = new System.Drawing.Point(4, 25);
            this.tcpCruise.Margin = new System.Windows.Forms.Padding(4);
            this.tcpCruise.Name = "tcpCruise";
            this.tcpCruise.Padding = new System.Windows.Forms.Padding(4);
            this.tcpCruise.Size = new System.Drawing.Size(400, 642);
            this.tcpCruise.TabIndex = 0;
            this.tcpCruise.Text = "Круиз";
            this.tcpCruise.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.cbCruises, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel9, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.85715F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(392, 634);
            this.tableLayoutPanel2.TabIndex = 0;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tlpDataOfBronCruise);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 326);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 147);
            this.panel1.TabIndex = 1;
            // 
            // tlpDataOfBronCruise
            // 
            this.tlpDataOfBronCruise.ColumnCount = 2;
            this.tlpDataOfBronCruise.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.60773F));
            this.tlpDataOfBronCruise.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.39227F));
            this.tlpDataOfBronCruise.Controls.Add(this.label1, 0, 0);
            this.tlpDataOfBronCruise.Controls.Add(this.tbCabinNomber, 1, 0);
            this.tlpDataOfBronCruise.Controls.Add(this.label2, 0, 2);
            this.tlpDataOfBronCruise.Controls.Add(this.label3, 0, 3);
            this.tlpDataOfBronCruise.Controls.Add(this.label4, 0, 4);
            this.tlpDataOfBronCruise.Controls.Add(this.label5, 0, 5);
            this.tlpDataOfBronCruise.Controls.Add(this.tableLayoutPanel4, 1, 2);
            this.tlpDataOfBronCruise.Controls.Add(this.cbCabinDef, 1, 3);
            this.tlpDataOfBronCruise.Controls.Add(this.tbNomberOptions, 1, 4);
            this.tlpDataOfBronCruise.Controls.Add(this.tbSpecCanc, 1, 5);
            this.tlpDataOfBronCruise.Controls.Add(this.label8, 0, 1);
            this.tlpDataOfBronCruise.Controls.Add(this.tbCabinCategory, 1, 1);
            this.tlpDataOfBronCruise.Controls.Add(this.tableLayoutPanel7, 0, 6);
            this.tlpDataOfBronCruise.Controls.Add(this.btnOk, 1, 7);
            this.tlpDataOfBronCruise.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpDataOfBronCruise.Location = new System.Drawing.Point(0, 0);
            this.tlpDataOfBronCruise.Margin = new System.Windows.Forms.Padding(4);
            this.tlpDataOfBronCruise.Name = "tlpDataOfBronCruise";
            this.tlpDataOfBronCruise.RowCount = 8;
            this.tlpDataOfBronCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpDataOfBronCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpDataOfBronCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpDataOfBronCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpDataOfBronCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpDataOfBronCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpDataOfBronCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tlpDataOfBronCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDataOfBronCruise.Size = new System.Drawing.Size(363, 343);
            this.tlpDataOfBronCruise.TabIndex = 3;
            this.tlpDataOfBronCruise.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер каюты";
            // 
            // tbCabinNomber
            // 
            this.tbCabinNomber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCabinNomber.Location = new System.Drawing.Point(151, 4);
            this.tbCabinNomber.Margin = new System.Windows.Forms.Padding(4);
            this.tbCabinNomber.Name = "tbCabinNomber";
            this.tbCabinNomber.Size = new System.Drawing.Size(208, 23);
            this.tbCabinNomber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(4, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Бронь на опции до";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(4, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "Уровень дефицита кают";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(4, 132);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 26);
            this.label4.TabIndex = 4;
            this.label4.Text = "Номер брони в кр.компании";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(4, 165);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 26);
            this.label5.TabIndex = 5;
            this.label5.Text = "Отказ по спец сервису";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.35897F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.64103F));
            this.tableLayoutPanel4.Controls.Add(this.dtpOption, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.mtbTime, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(151, 70);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(208, 25);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // dtpOption
            // 
            this.dtpOption.CustomFormat = "dd.MM.yyyy";
            this.dtpOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpOption.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOption.Location = new System.Drawing.Point(4, 4);
            this.dtpOption.Margin = new System.Windows.Forms.Padding(4);
            this.dtpOption.Name = "dtpOption";
            this.dtpOption.Size = new System.Drawing.Size(146, 23);
            this.dtpOption.TabIndex = 0;
            // 
            // mtbTime
            // 
            this.mtbTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtbTime.Location = new System.Drawing.Point(158, 4);
            this.mtbTime.Margin = new System.Windows.Forms.Padding(4);
            this.mtbTime.Mask = "00:00";
            this.mtbTime.Name = "mtbTime";
            this.mtbTime.Size = new System.Drawing.Size(46, 23);
            this.mtbTime.TabIndex = 1;
            this.mtbTime.ValidatingType = typeof(System.DateTime);
            // 
            // cbCabinDef
            // 
            this.cbCabinDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCabinDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCabinDef.FormattingEnabled = true;
            this.cbCabinDef.Items.AddRange(new object[] {
            "Много",
            "Еще есть",
            "Последняя каюта"});
            this.cbCabinDef.Location = new System.Drawing.Point(151, 103);
            this.cbCabinDef.Margin = new System.Windows.Forms.Padding(4);
            this.cbCabinDef.Name = "cbCabinDef";
            this.cbCabinDef.Size = new System.Drawing.Size(208, 24);
            this.cbCabinDef.TabIndex = 7;
            // 
            // tbNomberOptions
            // 
            this.tbNomberOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNomberOptions.Location = new System.Drawing.Point(151, 136);
            this.tbNomberOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tbNomberOptions.Name = "tbNomberOptions";
            this.tbNomberOptions.Size = new System.Drawing.Size(208, 23);
            this.tbNomberOptions.TabIndex = 8;
            // 
            // tbSpecCanc
            // 
            this.tbSpecCanc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSpecCanc.Location = new System.Drawing.Point(151, 169);
            this.tbSpecCanc.Margin = new System.Windows.Forms.Padding(4);
            this.tbSpecCanc.Name = "tbSpecCanc";
            this.tbSpecCanc.Size = new System.Drawing.Size(208, 23);
            this.tbSpecCanc.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(4, 33);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Категория каюты";
            // 
            // tbCabinCategory
            // 
            this.tbCabinCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCabinCategory.Location = new System.Drawing.Point(151, 37);
            this.tbCabinCategory.Margin = new System.Windows.Forms.Padding(4);
            this.tbCabinCategory.Name = "tbCabinCategory";
            this.tbCabinCategory.Size = new System.Drawing.Size(208, 23);
            this.tbCabinCategory.TabIndex = 12;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tlpDataOfBronCruise.SetColumnSpan(this.tableLayoutPanel7, 2);
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Controls.Add(this.cbIsBook, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label10, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.label11, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.cbDocumentGet, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.cbDocumentQuery, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(4, 202);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(355, 101);
            this.tableLayoutPanel7.TabIndex = 14;
            // 
            // cbIsBook
            // 
            this.cbIsBook.AutoSize = true;
            this.cbIsBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbIsBook.Location = new System.Drawing.Point(4, 4);
            this.cbIsBook.Margin = new System.Windows.Forms.Padding(4);
            this.cbIsBook.Name = "cbIsBook";
            this.cbIsBook.Size = new System.Drawing.Size(110, 29);
            this.cbIsBook.TabIndex = 13;
            this.cbIsBook.UseVisualStyleBackColor = true;
            this.cbIsBook.CheckedChanged += new System.EventHandler(this.cbIsBook_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(4, 37);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 64);
            this.label9.TabIndex = 14;
            this.label9.Text = "Опция подтверждена";
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(122, 37);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 64);
            this.label10.TabIndex = 15;
            this.label10.Text = "Документы запрошены";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(240, 37);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 34);
            this.label11.TabIndex = 16;
            this.label11.Text = "Документы получены";
            // 
            // cbDocumentGet
            // 
            this.cbDocumentGet.AutoSize = true;
            this.cbDocumentGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDocumentGet.Location = new System.Drawing.Point(240, 4);
            this.cbDocumentGet.Margin = new System.Windows.Forms.Padding(4);
            this.cbDocumentGet.Name = "cbDocumentGet";
            this.cbDocumentGet.Size = new System.Drawing.Size(111, 29);
            this.cbDocumentGet.TabIndex = 18;
            this.cbDocumentGet.UseVisualStyleBackColor = true;
            // 
            // cbDocumentQuery
            // 
            this.cbDocumentQuery.AutoSize = true;
            this.cbDocumentQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDocumentQuery.Location = new System.Drawing.Point(122, 4);
            this.cbDocumentQuery.Margin = new System.Windows.Forms.Padding(4);
            this.cbDocumentQuery.Name = "cbDocumentQuery";
            this.cbDocumentQuery.Size = new System.Drawing.Size(110, 29);
            this.cbDocumentQuery.TabIndex = 17;
            this.cbDocumentQuery.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(279, 311);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 28);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbCruises
            // 
            this.cbCruises.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCruises.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCruises.DropDownWidth = 700;
            this.cbCruises.FormattingEnabled = true;
            this.cbCruises.IntegralHeight = false;
            this.cbCruises.Location = new System.Drawing.Point(4, 4);
            this.cbCruises.Margin = new System.Windows.Forms.Padding(4);
            this.cbCruises.Name = "cbCruises";
            this.cbCruises.Size = new System.Drawing.Size(384, 24);
            this.cbCruises.TabIndex = 1;
            this.cbCruises.SelectedIndexChanged += new System.EventHandler(this.cbCruises_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(4, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(384, 33);
            this.label6.TabIndex = 2;
            this.label6.Text = "1.Создание брони в круиз компаниях";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gray;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(4, 299);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(384, 23);
            this.label7.TabIndex = 3;
            this.label7.Text = "2. Данные по бронированию";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.tlpCruiseBl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 70);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(384, 225);
            this.panel2.TabIndex = 5;
            // 
            // tlpCruiseBl1
            // 
            this.tlpCruiseBl1.AutoSize = true;
            this.tlpCruiseBl1.ColumnCount = 1;
            this.tlpCruiseBl1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCruiseBl1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpCruiseBl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCruiseBl1.Location = new System.Drawing.Point(0, 0);
            this.tlpCruiseBl1.Margin = new System.Windows.Forms.Padding(4);
            this.tlpCruiseBl1.Name = "tlpCruiseBl1";
            this.tlpCruiseBl1.RowCount = 1;
            this.tlpCruiseBl1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCruiseBl1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlpCruiseBl1.Size = new System.Drawing.Size(380, 0);
            this.tlpCruiseBl1.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.AutoScroll = true;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Controls.Add(this.tlpBonusCruise);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 480);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(386, 151);
            this.panel9.TabIndex = 6;
            // 
            // tlpBonusCruise
            // 
            this.tlpBonusCruise.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpBonusCruise.ColumnCount = 1;
            this.tlpBonusCruise.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBonusCruise.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpBonusCruise.Location = new System.Drawing.Point(0, 0);
            this.tlpBonusCruise.Name = "tlpBonusCruise";
            this.tlpBonusCruise.RowCount = 1;
            this.tlpBonusCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBonusCruise.Size = new System.Drawing.Size(382, 10);
            this.tlpBonusCruise.TabIndex = 0;
            // 
            // tcpHotel
            // 
            this.tcpHotel.Controls.Add(this.tableLayoutPanel9);
            this.tcpHotel.Location = new System.Drawing.Point(4, 25);
            this.tcpHotel.Margin = new System.Windows.Forms.Padding(4);
            this.tcpHotel.Name = "tcpHotel";
            this.tcpHotel.Padding = new System.Windows.Forms.Padding(4);
            this.tcpHotel.Size = new System.Drawing.Size(400, 642);
            this.tcpHotel.TabIndex = 2;
            this.tcpHotel.Text = "Отели";
            this.tcpHotel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.cbHotels, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.panel5, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.label15, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel12, 0, 4);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 5;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(392, 634);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // cbHotels
            // 
            this.cbHotels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbHotels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHotels.FormattingEnabled = true;
            this.cbHotels.Location = new System.Drawing.Point(4, 4);
            this.cbHotels.Margin = new System.Windows.Forms.Padding(4);
            this.cbHotels.Name = "cbHotels";
            this.cbHotels.Size = new System.Drawing.Size(384, 24);
            this.cbHotels.TabIndex = 0;
            this.cbHotels.SelectedIndexChanged += new System.EventHandler(this.cbHotels_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Gray;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(4, 33);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(384, 33);
            this.label14.TabIndex = 1;
            this.label14.Text = "Данные по отелю";
            // 
            // panel5
            // 
            this.panel5.AllowDrop = true;
            this.panel5.AutoScroll = true;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.tlpHotels);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 70);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(384, 366);
            this.panel5.TabIndex = 2;
            // 
            // tlpHotels
            // 
            this.tlpHotels.ColumnCount = 1;
            this.tlpHotels.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHotels.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpHotels.Location = new System.Drawing.Point(0, 0);
            this.tlpHotels.Margin = new System.Windows.Forms.Padding(4);
            this.tlpHotels.Name = "tlpHotels";
            this.tlpHotels.RowCount = 1;
            this.tlpHotels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHotels.Size = new System.Drawing.Size(380, 30);
            this.tlpHotels.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Gray;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(4, 440);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(384, 33);
            this.label15.TabIndex = 3;
            this.label15.Text = "Данные по бронированию";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.cbHotelOk, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnHotelOk, 1, 0);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 476);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.21687F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.78313F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(385, 83);
            this.tableLayoutPanel12.TabIndex = 4;
            // 
            // cbHotelOk
            // 
            this.cbHotelOk.AutoSize = true;
            this.cbHotelOk.Location = new System.Drawing.Point(3, 3);
            this.cbHotelOk.Name = "cbHotelOk";
            this.cbHotelOk.Size = new System.Drawing.Size(156, 21);
            this.cbHotelOk.TabIndex = 0;
            this.cbHotelOk.Text = "отель подтвержден";
            this.cbHotelOk.UseVisualStyleBackColor = true;
            // 
            // btnHotelOk
            // 
            this.btnHotelOk.Location = new System.Drawing.Point(195, 3);
            this.btnHotelOk.Name = "btnHotelOk";
            this.btnHotelOk.Size = new System.Drawing.Size(187, 39);
            this.btnHotelOk.TabIndex = 1;
            this.btnHotelOk.Text = "Ок";
            this.btnHotelOk.UseVisualStyleBackColor = true;
            this.btnHotelOk.Click += new System.EventHandler(this.btnHotelOk_Click);
            // 
            // tcpInshur
            // 
            this.tcpInshur.Controls.Add(this.tableLayoutPanel13);
            this.tcpInshur.Location = new System.Drawing.Point(4, 25);
            this.tcpInshur.Name = "tcpInshur";
            this.tcpInshur.Padding = new System.Windows.Forms.Padding(3);
            this.tcpInshur.Size = new System.Drawing.Size(400, 642);
            this.tcpInshur.TabIndex = 4;
            this.tcpInshur.Text = "Страховки";
            this.tcpInshur.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.cbInshur, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.label18, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel14, 0, 4);
            this.tableLayoutPanel13.Controls.Add(this.btnInshur, 0, 3);
            this.tableLayoutPanel13.Controls.Add(this.panel8, 0, 2);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 5;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(394, 636);
            this.tableLayoutPanel13.TabIndex = 0;
            // 
            // cbInshur
            // 
            this.cbInshur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbInshur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInshur.DropDownWidth = 700;
            this.cbInshur.FormattingEnabled = true;
            this.cbInshur.IntegralHeight = false;
            this.cbInshur.Location = new System.Drawing.Point(3, 3);
            this.cbInshur.Name = "cbInshur";
            this.cbInshur.Size = new System.Drawing.Size(388, 24);
            this.cbInshur.TabIndex = 0;
            this.cbInshur.SelectedIndexChanged += new System.EventHandler(this.cbInshur_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Gray;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(3, 27);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(388, 27);
            this.label18.TabIndex = 1;
            this.label18.Text = "Данные по страховке\r\n";
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.AutoScroll = true;
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.98969F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.01031F));
            this.tableLayoutPanel14.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.cbInshurCreate, 1, 0);
            this.tableLayoutPanel14.Controls.Add(this.btnOkInshur, 1, 1);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 242);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 2;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(388, 391);
            this.tableLayoutPanel14.TabIndex = 2;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(203, 17);
            this.label19.TabIndex = 0;
            this.label19.Text = "Страховка выписана вручную";
            // 
            // cbInshurCreate
            // 
            this.cbInshurCreate.AutoSize = true;
            this.cbInshurCreate.Location = new System.Drawing.Point(227, 3);
            this.cbInshurCreate.Name = "cbInshurCreate";
            this.cbInshurCreate.Size = new System.Drawing.Size(15, 14);
            this.cbInshurCreate.TabIndex = 1;
            this.cbInshurCreate.UseVisualStyleBackColor = true;
            // 
            // btnOkInshur
            // 
            this.btnOkInshur.Location = new System.Drawing.Point(227, 68);
            this.btnOkInshur.Name = "btnOkInshur";
            this.btnOkInshur.Size = new System.Drawing.Size(81, 31);
            this.btnOkInshur.TabIndex = 2;
            this.btnOkInshur.Text = "Ок";
            this.btnOkInshur.UseVisualStyleBackColor = true;
            this.btnOkInshur.Click += new System.EventHandler(this.btnOkInshur_Click);
            // 
            // btnInshur
            // 
            this.btnInshur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInshur.Location = new System.Drawing.Point(3, 207);
            this.btnInshur.Name = "btnInshur";
            this.btnInshur.Size = new System.Drawing.Size(388, 29);
            this.btnInshur.TabIndex = 3;
            this.btnInshur.Text = "Система выписки страховок";
            this.btnInshur.UseVisualStyleBackColor = true;
            this.btnInshur.Click += new System.EventHandler(this.btnInshur_Click);
            // 
            // panel8
            // 
            this.panel8.AutoScroll = true;
            this.panel8.Controls.Add(this.tlpInshur);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 57);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(388, 144);
            this.panel8.TabIndex = 4;
            // 
            // tlpInshur
            // 
            this.tlpInshur.ColumnCount = 1;
            this.tlpInshur.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInshur.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpInshur.Location = new System.Drawing.Point(0, 0);
            this.tlpInshur.Name = "tlpInshur";
            this.tlpInshur.RowCount = 1;
            this.tlpInshur.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInshur.Size = new System.Drawing.Size(388, 11);
            this.tlpInshur.TabIndex = 0;
            // 
            // tcpVisa
            // 
            this.tcpVisa.Controls.Add(this.tableLayoutPanel3);
            this.tcpVisa.Location = new System.Drawing.Point(4, 25);
            this.tcpVisa.Name = "tcpVisa";
            this.tcpVisa.Padding = new System.Windows.Forms.Padding(3);
            this.tcpVisa.Size = new System.Drawing.Size(400, 642);
            this.tcpVisa.TabIndex = 5;
            this.tcpVisa.Text = "Визы";
            this.tcpVisa.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.cbVisa, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel11, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label24, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label34, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(394, 636);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // cbVisa
            // 
            this.cbVisa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbVisa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVisa.FormattingEnabled = true;
            this.cbVisa.Location = new System.Drawing.Point(3, 3);
            this.cbVisa.Name = "cbVisa";
            this.cbVisa.Size = new System.Drawing.Size(388, 24);
            this.cbVisa.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.AutoScroll = true;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel11.Controls.Add(this.tableLayoutPanel18);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 57);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(388, 382);
            this.panel11.TabIndex = 1;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 1;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(384, 18);
            this.tableLayoutPanel18.TabIndex = 0;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Gray;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.Location = new System.Drawing.Point(3, 27);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(388, 27);
            this.label24.TabIndex = 2;
            this.label24.Text = "Данные по услуге";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Gray;
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label34.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label34.Location = new System.Drawing.Point(3, 442);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(388, 27);
            this.label34.TabIndex = 3;
            this.label34.Text = "Данные по бронированию";
            // 
            // tcpDopPaket
            // 
            this.tcpDopPaket.Controls.Add(this.tableLayoutPanel8);
            this.tcpDopPaket.Location = new System.Drawing.Point(4, 25);
            this.tcpDopPaket.Margin = new System.Windows.Forms.Padding(4);
            this.tcpDopPaket.Name = "tcpDopPaket";
            this.tcpDopPaket.Padding = new System.Windows.Forms.Padding(4);
            this.tcpDopPaket.Size = new System.Drawing.Size(400, 642);
            this.tcpDopPaket.TabIndex = 1;
            this.tcpDopPaket.Text = "Доп пакет";
            this.tcpDopPaket.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.cbDopPaket, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.label13, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.panel4, 0, 4);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 5;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(392, 634);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // cbDopPaket
            // 
            this.cbDopPaket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDopPaket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDopPaket.FormattingEnabled = true;
            this.cbDopPaket.Location = new System.Drawing.Point(4, 4);
            this.cbDopPaket.Margin = new System.Windows.Forms.Padding(4);
            this.cbDopPaket.Name = "cbDopPaket";
            this.cbDopPaket.Size = new System.Drawing.Size(384, 24);
            this.cbDopPaket.TabIndex = 1;
            this.cbDopPaket.SelectedIndexChanged += new System.EventHandler(this.cbDopPaket_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Gray;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(4, 33);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(384, 33);
            this.label12.TabIndex = 2;
            this.label12.Text = "Данные по пакету";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Gray;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(4, 440);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(384, 33);
            this.label13.TabIndex = 3;
            this.label13.Text = "Данные по бронированию";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.tlpDopPaket);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 70);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(384, 366);
            this.panel3.TabIndex = 4;
            // 
            // tlpDopPaket
            // 
            this.tlpDopPaket.ColumnCount = 1;
            this.tlpDopPaket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDopPaket.Location = new System.Drawing.Point(4, 4);
            this.tlpDopPaket.Margin = new System.Windows.Forms.Padding(4);
            this.tlpDopPaket.Name = "tlpDopPaket";
            this.tlpDopPaket.RowCount = 1;
            this.tlpDopPaket.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDopPaket.Size = new System.Drawing.Size(447, 23);
            this.tlpDopPaket.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.tableLayoutPanel10);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 477);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(384, 153);
            this.panel4.TabIndex = 5;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.52381F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.47619F));
            this.tableLayoutPanel10.Location = new System.Drawing.Point(4, 0);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.72327F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.27673F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(364, 196);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // tcpAvia
            // 
            this.tcpAvia.Controls.Add(this.tableLayoutPanel19);
            this.tcpAvia.Location = new System.Drawing.Point(4, 25);
            this.tcpAvia.Name = "tcpAvia";
            this.tcpAvia.Size = new System.Drawing.Size(400, 642);
            this.tcpAvia.TabIndex = 6;
            this.tcpAvia.Text = "Авиаперелет";
            this.tcpAvia.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 1;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.Controls.Add(this.cbAvia, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.rtbAvia, 0, 1);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 2;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(400, 642);
            this.tableLayoutPanel19.TabIndex = 0;
            // 
            // cbAvia
            // 
            this.cbAvia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAvia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAvia.FormattingEnabled = true;
            this.cbAvia.Location = new System.Drawing.Point(3, 3);
            this.cbAvia.Name = "cbAvia";
            this.cbAvia.Size = new System.Drawing.Size(394, 24);
            this.cbAvia.TabIndex = 0;
            this.cbAvia.SelectedIndexChanged += new System.EventHandler(this.cbAvia_SelectedIndexChanged);
            // 
            // rtbAvia
            // 
            this.rtbAvia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbAvia.Location = new System.Drawing.Point(3, 36);
            this.rtbAvia.Name = "rtbAvia";
            this.rtbAvia.ReadOnly = true;
            this.rtbAvia.Size = new System.Drawing.Size(394, 603);
            this.rtbAvia.TabIndex = 1;
            this.rtbAvia.Text = "";
            // 
            // tcpOther
            // 
            this.tcpOther.Controls.Add(this.tableLayoutPanel11);
            this.tcpOther.Location = new System.Drawing.Point(4, 25);
            this.tcpOther.Name = "tcpOther";
            this.tcpOther.Padding = new System.Windows.Forms.Padding(3);
            this.tcpOther.Size = new System.Drawing.Size(400, 642);
            this.tcpOther.TabIndex = 3;
            this.tcpOther.Text = "Другие услуги";
            this.tcpOther.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.cbOther, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.panel6, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.label16, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.label17, 0, 3);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 5;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(394, 636);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // cbOther
            // 
            this.cbOther.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbOther.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOther.FormattingEnabled = true;
            this.cbOther.Location = new System.Drawing.Point(3, 3);
            this.cbOther.Name = "cbOther";
            this.cbOther.Size = new System.Drawing.Size(388, 24);
            this.cbOther.TabIndex = 0;
            this.cbOther.SelectedIndexChanged += new System.EventHandler(this.cbOther_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.AutoScroll = true;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.tlpOther);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 57);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(388, 382);
            this.panel6.TabIndex = 1;
            // 
            // tlpOther
            // 
            this.tlpOther.ColumnCount = 1;
            this.tlpOther.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOther.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpOther.Location = new System.Drawing.Point(0, 0);
            this.tlpOther.Name = "tlpOther";
            this.tlpOther.RowCount = 1;
            this.tlpOther.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOther.Size = new System.Drawing.Size(384, 18);
            this.tlpOther.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Gray;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(3, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(388, 27);
            this.label16.TabIndex = 2;
            this.label16.Text = "Данные по услуге";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Gray;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label17.Location = new System.Drawing.Point(3, 442);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(388, 27);
            this.label17.TabIndex = 3;
            this.label17.Text = "Данные по бронированию";
            // 
            // panel12
            // 
            this.panel12.AutoScroll = true;
            this.panel12.Controls.Add(this.tcPartnerPutevka);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(420, 187);
            this.panel12.Margin = new System.Windows.Forms.Padding(4);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1000, 671);
            this.panel12.TabIndex = 1;
            // 
            // tcPartnerPutevka
            // 
            this.tcPartnerPutevka.Controls.Add(this.tcpPartner);
            this.tcPartnerPutevka.Controls.Add(this.tcpPutevka);
            this.tcPartnerPutevka.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPartnerPutevka.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tcPartnerPutevka.Location = new System.Drawing.Point(0, 0);
            this.tcPartnerPutevka.Name = "tcPartnerPutevka";
            this.tcPartnerPutevka.SelectedIndex = 0;
            this.tcPartnerPutevka.Size = new System.Drawing.Size(1000, 671);
            this.tcPartnerPutevka.TabIndex = 0;
            // 
            // tcpPartner
            // 
            this.tcpPartner.Controls.Add(this.tlpWebBrowser);
            this.tcpPartner.Location = new System.Drawing.Point(4, 25);
            this.tcpPartner.Name = "tcpPartner";
            this.tcpPartner.Padding = new System.Windows.Forms.Padding(3);
            this.tcpPartner.Size = new System.Drawing.Size(992, 642);
            this.tcpPartner.TabIndex = 0;
            this.tcpPartner.Text = "Партнер";
            this.tcpPartner.UseVisualStyleBackColor = true;
            // 
            // tlpWebBrowser
            // 
            this.tlpWebBrowser.ColumnCount = 1;
            this.tlpWebBrowser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWebBrowser.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tlpWebBrowser.Controls.Add(this.wbBook, 0, 1);
            this.tlpWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWebBrowser.Location = new System.Drawing.Point(3, 3);
            this.tlpWebBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.tlpWebBrowser.Name = "tlpWebBrowser";
            this.tlpWebBrowser.RowCount = 2;
            this.tlpWebBrowser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpWebBrowser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWebBrowser.Size = new System.Drawing.Size(986, 636);
            this.tlpWebBrowser.TabIndex = 1;
            this.tlpWebBrowser.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel5_Paint);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel6.Controls.Add(this.btnEnter, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbAdress, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(978, 30);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(878, 4);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(96, 22);
            this.btnEnter.TabIndex = 1;
            this.btnEnter.Text = "Перейти";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // tbAdress
            // 
            this.tbAdress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAdress.Location = new System.Drawing.Point(3, 3);
            this.tbAdress.Name = "tbAdress";
            this.tbAdress.Size = new System.Drawing.Size(868, 23);
            this.tbAdress.TabIndex = 2;
            this.tbAdress.Leave += new System.EventHandler(this.tbAdress_Leave);
            // 
            // wbBook
            // 
            this.wbBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbBook.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.wbBook.Location = new System.Drawing.Point(3, 41);
            this.wbBook.Size = new System.Drawing.Size(980, 592);
            this.wbBook.Source = new System.Uri("about:error", System.UriKind.Absolute);
            this.wbBook.TabIndex = 1;
            this.wbBook.InitializeView += new Awesomium.Core.WebViewEventHandler(this.Awesomium_Windows_Forms_WebControl_InitializeView);
            this.wbBook.AddressChanged += new Awesomium.Core.UrlEventHandler(this.Awesomium_Windows_Forms_WebControl_AddressChanged);
            this.wbBook.ShowCreatedWebView += new Awesomium.Core.ShowCreatedWebViewEventHandler(this.Awesomium_Windows_Forms_WebControl_ShowCreatedWebView);
            this.wbBook.LoadingFrameFailed += new Awesomium.Core.LoadingFrameFailedEventHandler(this.Awesomium_Windows_Forms_WebControl_LoadingFrameFailed);
            // 
            // tcpPutevka
            // 
            this.tcpPutevka.Controls.Add(this.tlpPutevka);
            this.tcpPutevka.Location = new System.Drawing.Point(4, 25);
            this.tcpPutevka.Name = "tcpPutevka";
            this.tcpPutevka.Padding = new System.Windows.Forms.Padding(3);
            this.tcpPutevka.Size = new System.Drawing.Size(992, 642);
            this.tcpPutevka.TabIndex = 1;
            this.tcpPutevka.Text = "Путевка";
            this.tcpPutevka.UseVisualStyleBackColor = true;
            // 
            // tlpPutevka
            // 
            this.tlpPutevka.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpPutevka.ColumnCount = 1;
            this.tlpPutevka.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPutevka.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPutevka.Controls.Add(this.scPutevka, 0, 0);
            this.tlpPutevka.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlpPutevka.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPutevka.Location = new System.Drawing.Point(3, 3);
            this.tlpPutevka.Margin = new System.Windows.Forms.Padding(4);
            this.tlpPutevka.Name = "tlpPutevka";
            this.tlpPutevka.RowCount = 1;
            this.tlpPutevka.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPutevka.Size = new System.Drawing.Size(986, 636);
            this.tlpPutevka.TabIndex = 0;
            // 
            // scPutevka
            // 
            this.scPutevka.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scPutevka.Location = new System.Drawing.Point(4, 4);
            this.scPutevka.Name = "scPutevka";
            this.scPutevka.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scPutevka.Panel1
            // 
            this.scPutevka.Panel1.Controls.Add(this.dgvPutevka);
            // 
            // scPutevka.Panel2
            // 
            this.scPutevka.Panel2.Controls.Add(this.tcProblemChanges);
            this.scPutevka.Size = new System.Drawing.Size(978, 628);
            this.scPutevka.SplitterDistance = 173;
            this.scPutevka.TabIndex = 2;
            // 
            // dgvPutevka
            // 
            this.dgvPutevka.AllowUserToAddRows = false;
            this.dgvPutevka.AllowUserToDeleteRows = false;
            this.dgvPutevka.AllowUserToResizeRows = false;
            this.dgvPutevka.BackgroundColor = System.Drawing.Color.White;
            this.dgvPutevka.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPutevka.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvPutevka.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPutevka.Location = new System.Drawing.Point(0, 0);
            this.dgvPutevka.MultiSelect = false;
            this.dgvPutevka.Name = "dgvPutevka";
            this.dgvPutevka.ReadOnly = true;
            this.dgvPutevka.RowHeadersVisible = false;
            this.dgvPutevka.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPutevka.Size = new System.Drawing.Size(978, 173);
            this.dgvPutevka.TabIndex = 0;
            this.dgvPutevka.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPutevka_CellFormatting);
            this.dgvPutevka.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvPutevka_CellPainting);
            // 
            // tcProblemChanges
            // 
            this.tcProblemChanges.Controls.Add(this.tcpProblem);
            this.tcProblemChanges.Controls.Add(this.tcpChanges);
            this.tcProblemChanges.Controls.Add(this.tcpItinerary);
            this.tcProblemChanges.Controls.Add(this.tcpSettings);
            this.tcProblemChanges.Controls.Add(this.tcpMessages);
            this.tcProblemChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcProblemChanges.Location = new System.Drawing.Point(0, 0);
            this.tcProblemChanges.Name = "tcProblemChanges";
            this.tcProblemChanges.SelectedIndex = 0;
            this.tcProblemChanges.Size = new System.Drawing.Size(978, 451);
            this.tcProblemChanges.TabIndex = 1;
            this.tcProblemChanges.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcProblemChanges_Selecting);
            this.tcProblemChanges.Click += new System.EventHandler(this.tcProblemChanges_Click);
            // 
            // tcpProblem
            // 
            this.tcpProblem.Controls.Add(this.dgvProblems);
            this.tcpProblem.Location = new System.Drawing.Point(4, 25);
            this.tcpProblem.Name = "tcpProblem";
            this.tcpProblem.Padding = new System.Windows.Forms.Padding(3);
            this.tcpProblem.Size = new System.Drawing.Size(970, 422);
            this.tcpProblem.TabIndex = 0;
            this.tcpProblem.Text = "Проблемы";
            this.tcpProblem.UseVisualStyleBackColor = true;
            // 
            // dgvProblems
            // 
            this.dgvProblems.AllowUserToAddRows = false;
            this.dgvProblems.AllowUserToDeleteRows = false;
            this.dgvProblems.AllowUserToResizeRows = false;
            this.dgvProblems.BackgroundColor = System.Drawing.Color.White;
            this.dgvProblems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProblems.Location = new System.Drawing.Point(3, 3);
            this.dgvProblems.MultiSelect = false;
            this.dgvProblems.Name = "dgvProblems";
            this.dgvProblems.ReadOnly = true;
            this.dgvProblems.RowHeadersVisible = false;
            this.dgvProblems.Size = new System.Drawing.Size(964, 416);
            this.dgvProblems.TabIndex = 0;
            this.dgvProblems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProblems_CellContentClick);
            this.dgvProblems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProblems_RowPostPaint);
            this.dgvProblems.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvProblems_RowPrePaint);
            // 
            // tcpChanges
            // 
            this.tcpChanges.Controls.Add(this.dgvChanges);
            this.tcpChanges.Location = new System.Drawing.Point(4, 22);
            this.tcpChanges.Name = "tcpChanges";
            this.tcpChanges.Padding = new System.Windows.Forms.Padding(3);
            this.tcpChanges.Size = new System.Drawing.Size(970, 425);
            this.tcpChanges.TabIndex = 1;
            this.tcpChanges.Text = "Изменения";
            this.tcpChanges.UseVisualStyleBackColor = true;
            // 
            // dgvChanges
            // 
            this.dgvChanges.AllowUserToAddRows = false;
            this.dgvChanges.AllowUserToDeleteRows = false;
            this.dgvChanges.AllowUserToResizeRows = false;
            this.dgvChanges.BackgroundColor = System.Drawing.Color.White;
            this.dgvChanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChanges.Location = new System.Drawing.Point(3, 3);
            this.dgvChanges.Name = "dgvChanges";
            this.dgvChanges.ReadOnly = true;
            this.dgvChanges.RowHeadersVisible = false;
            this.dgvChanges.Size = new System.Drawing.Size(964, 419);
            this.dgvChanges.TabIndex = 0;
            this.dgvChanges.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChanges_CellContentClick);
            // 
            // tcpItinerary
            // 
            this.tcpItinerary.Controls.Add(this.dgvItinerary);
            this.tcpItinerary.Location = new System.Drawing.Point(4, 22);
            this.tcpItinerary.Name = "tcpItinerary";
            this.tcpItinerary.Padding = new System.Windows.Forms.Padding(3);
            this.tcpItinerary.Size = new System.Drawing.Size(970, 425);
            this.tcpItinerary.TabIndex = 2;
            this.tcpItinerary.Text = "Маршрут";
            this.tcpItinerary.UseVisualStyleBackColor = true;
            // 
            // dgvItinerary
            // 
            this.dgvItinerary.AllowUserToAddRows = false;
            this.dgvItinerary.AllowUserToDeleteRows = false;
            this.dgvItinerary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvItinerary.BackgroundColor = System.Drawing.Color.White;
            this.dgvItinerary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItinerary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItinerary.Location = new System.Drawing.Point(3, 3);
            this.dgvItinerary.MultiSelect = false;
            this.dgvItinerary.Name = "dgvItinerary";
            this.dgvItinerary.ReadOnly = true;
            this.dgvItinerary.RowHeadersVisible = false;
            this.dgvItinerary.Size = new System.Drawing.Size(964, 419);
            this.dgvItinerary.TabIndex = 0;
            // 
            // tcpSettings
            // 
            this.tcpSettings.AutoScroll = true;
            this.tcpSettings.Location = new System.Drawing.Point(4, 22);
            this.tcpSettings.Name = "tcpSettings";
            this.tcpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tcpSettings.Size = new System.Drawing.Size(970, 425);
            this.tcpSettings.TabIndex = 4;
            this.tcpSettings.Text = "Изменение дат\\статусов";
            this.tcpSettings.UseVisualStyleBackColor = true;
            // 
            // tcpMessages
            // 
            this.tcpMessages.BackColor = System.Drawing.Color.Transparent;
            this.tcpMessages.Location = new System.Drawing.Point(4, 22);
            this.tcpMessages.Name = "tcpMessages";
            this.tcpMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tcpMessages.Size = new System.Drawing.Size(970, 425);
            this.tcpMessages.TabIndex = 3;
            this.tcpMessages.Text = "Переписка";
            this.tcpMessages.Click += new System.EventHandler(this.tcpMessages_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.SetColumnSpan(this.label20, 2);
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(3, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1418, 27);
            this.label20.TabIndex = 2;
            this.label20.Text = "РАБОТА С ЗАКАЗОМ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel15.ColumnCount = 7;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel15, 2);
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 182F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.Controls.Add(this.label21, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.label22, 2, 0);
            this.tableLayoutPanel15.Controls.Add(this.label23, 3, 0);
            this.tableLayoutPanel15.Controls.Add(this.tbDGcod, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.tbDateBegin, 2, 1);
            this.tableLayoutPanel15.Controls.Add(this.tbDateEnd, 3, 1);
            this.tableLayoutPanel15.Controls.Add(this.btnClose, 5, 1);
            this.tableLayoutPanel15.Controls.Add(this.btnExit, 6, 1);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 2;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(1418, 52);
            this.tableLayoutPanel15.TabIndex = 3;
            this.tableLayoutPanel15.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel15_Paint);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(4, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(107, 20);
            this.label21.TabIndex = 0;
            this.label21.Text = "Номер брони";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(149, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(142, 20);
            this.label22.TabIndex = 1;
            this.label22.Text = "Дата начала тура";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.Location = new System.Drawing.Point(298, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(166, 20);
            this.label23.TabIndex = 2;
            this.label23.Text = "Дата окончания тура";
            // 
            // tbDGcod
            // 
            this.tbDGcod.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbDGcod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDGcod.Location = new System.Drawing.Point(4, 25);
            this.tbDGcod.Name = "tbDGcod";
            this.tbDGcod.ReadOnly = true;
            this.tbDGcod.Size = new System.Drawing.Size(107, 23);
            this.tbDGcod.TabIndex = 4;
            // 
            // tbDateBegin
            // 
            this.tbDateBegin.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbDateBegin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDateBegin.Location = new System.Drawing.Point(149, 25);
            this.tbDateBegin.Name = "tbDateBegin";
            this.tbDateBegin.ReadOnly = true;
            this.tbDateBegin.Size = new System.Drawing.Size(142, 23);
            this.tbDateBegin.TabIndex = 5;
            // 
            // tbDateEnd
            // 
            this.tbDateEnd.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbDateEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDateEnd.Location = new System.Drawing.Point(298, 25);
            this.tbDateEnd.Name = "tbDateEnd";
            this.tbDateEnd.ReadOnly = true;
            this.tbDateEnd.Size = new System.Drawing.Size(166, 23);
            this.tbDateEnd.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Location = new System.Drawing.Point(1128, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Вернуться";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(1238, 25);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(176, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Закрыть";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 11;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel16, 2);
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel16.Controls.Add(this.label25, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.label26, 3, 0);
            this.tableLayoutPanel16.Controls.Add(this.label27, 6, 0);
            this.tableLayoutPanel16.Controls.Add(this.tbUslug, 1, 0);
            this.tableLayoutPanel16.Controls.Add(this.tbAccept, 4, 0);
            this.tableLayoutPanel16.Controls.Add(this.tbNoAccept, 7, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(3, 151);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(1418, 29);
            this.tableLayoutPanel16.TabIndex = 4;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(3, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(124, 29);
            this.label25.TabIndex = 0;
            this.label25.Text = "Услуг в заказе";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label26.Location = new System.Drawing.Point(183, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(124, 29);
            this.label26.TabIndex = 1;
            this.label26.Text = "Подтверждено";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.SystemColors.Control;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(363, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(124, 29);
            this.label27.TabIndex = 2;
            this.label27.Text = "Подтвердить";
            // 
            // tbUslug
            // 
            this.tbUslug.BackColor = System.Drawing.SystemColors.Window;
            this.tbUslug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUslug.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbUslug.Location = new System.Drawing.Point(133, 3);
            this.tbUslug.Name = "tbUslug";
            this.tbUslug.ReadOnly = true;
            this.tbUslug.Size = new System.Drawing.Size(24, 23);
            this.tbUslug.TabIndex = 3;
            // 
            // tbAccept
            // 
            this.tbAccept.BackColor = System.Drawing.SystemColors.Window;
            this.tbAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAccept.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tbAccept.Location = new System.Drawing.Point(313, 3);
            this.tbAccept.Name = "tbAccept";
            this.tbAccept.ReadOnly = true;
            this.tbAccept.Size = new System.Drawing.Size(24, 23);
            this.tbAccept.TabIndex = 4;
            // 
            // tbNoAccept
            // 
            this.tbNoAccept.BackColor = System.Drawing.SystemColors.Window;
            this.tbNoAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNoAccept.ForeColor = System.Drawing.Color.Red;
            this.tbNoAccept.Location = new System.Drawing.Point(493, 3);
            this.tbNoAccept.Name = "tbNoAccept";
            this.tbNoAccept.ReadOnly = true;
            this.tbNoAccept.Size = new System.Drawing.Size(24, 23);
            this.tbNoAccept.TabIndex = 5;
            // 
            // panel10
            // 
            this.panel10.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.panel10, 2);
            this.panel10.Controls.Add(this.tlpTurists);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(3, 123);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1418, 21);
            this.panel10.TabIndex = 7;
            // 
            // tlpTurists
            // 
            this.tlpTurists.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tlpTurists.ColumnCount = 7;
            this.tlpTurists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTurists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTurists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTurists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTurists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tlpTurists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tlpTurists.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 581F));
            this.tlpTurists.Controls.Add(this.label29, 0, 0);
            this.tlpTurists.Controls.Add(this.label30, 1, 0);
            this.tlpTurists.Controls.Add(this.label31, 2, 0);
            this.tlpTurists.Controls.Add(this.label32, 3, 0);
            this.tlpTurists.Controls.Add(this.label33, 4, 0);
            this.tlpTurists.Controls.Add(this.label36, 5, 0);
            this.tlpTurists.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTurists.Location = new System.Drawing.Point(0, 0);
            this.tlpTurists.Name = "tlpTurists";
            this.tlpTurists.RowCount = 1;
            this.tlpTurists.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 629F));
            this.tlpTurists.Size = new System.Drawing.Size(1418, 21);
            this.tlpTurists.TabIndex = 6;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.Location = new System.Drawing.Point(5, 2);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(114, 629);
            this.label29.TabIndex = 0;
            this.label29.Text = "Фамилия";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.Location = new System.Drawing.Point(127, 2);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(114, 629);
            this.label30.TabIndex = 1;
            this.label30.Text = "Имя";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label31.Location = new System.Drawing.Point(249, 2);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(114, 629);
            this.label31.TabIndex = 2;
            this.label31.Text = "Отчество";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label32.Location = new System.Drawing.Point(371, 2);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(114, 629);
            this.label32.TabIndex = 3;
            this.label32.Text = "Дата Рождения";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label33.Location = new System.Drawing.Point(493, 2);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(120, 629);
            this.label33.TabIndex = 4;
            this.label33.Text = "Загранпаспорт";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label36.Location = new System.Drawing.Point(621, 2);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(207, 629);
            this.label36.TabIndex = 7;
            this.label36.Text = "Общегражданский паспорт";
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 9;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel17, 2);
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 195F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 195F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 195F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Controls.Add(this.tbPax, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.label28, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.btnDocumenSettings, 2, 0);
            this.tableLayoutPanel17.Controls.Add(this.btnAccount, 3, 0);
            this.tableLayoutPanel17.Controls.Add(this.btnDogovor, 4, 0);
            this.tableLayoutPanel17.Controls.Add(this.btnExtend, 5, 0);
            this.tableLayoutPanel17.Controls.Add(this.btnRegion, 6, 0);
            this.tableLayoutPanel17.Controls.Add(this.btnDownloadedFiles, 7, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(3, 88);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 1;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(1418, 29);
            this.tableLayoutPanel17.TabIndex = 8;
            // 
            // tbPax
            // 
            this.tbPax.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbPax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPax.Location = new System.Drawing.Point(121, 3);
            this.tbPax.Name = "tbPax";
            this.tbPax.ReadOnly = true;
            this.tbPax.Size = new System.Drawing.Size(35, 23);
            this.tbPax.TabIndex = 8;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.Location = new System.Drawing.Point(3, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(112, 29);
            this.label28.TabIndex = 7;
            this.label28.Text = "Туристы";
            // 
            // btnDocumenSettings
            // 
            this.btnDocumenSettings.BackColor = System.Drawing.SystemColors.Control;
            this.btnDocumenSettings.Location = new System.Drawing.Point(162, 3);
            this.btnDocumenSettings.Name = "btnDocumenSettings";
            this.btnDocumenSettings.Size = new System.Drawing.Size(148, 23);
            this.btnDocumenSettings.TabIndex = 10;
            this.btnDocumenSettings.Text = "Сборка документов";
            this.btnDocumenSettings.UseVisualStyleBackColor = false;
            this.btnDocumenSettings.Click += new System.EventHandler(this.btnDocumenSettings_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.Location = new System.Drawing.Point(316, 3);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(153, 23);
            this.btnAccount.TabIndex = 12;
            this.btnAccount.Text = "Распечатать счет";
            this.btnAccount.UseVisualStyleBackColor = true;
            this.btnAccount.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDogovor
            // 
            this.btnDogovor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDogovor.Location = new System.Drawing.Point(476, 3);
            this.btnDogovor.Name = "btnDogovor";
            this.btnDogovor.Size = new System.Drawing.Size(159, 23);
            this.btnDogovor.TabIndex = 11;
            this.btnDogovor.Text = "Распечатать договор";
            this.btnDogovor.UseVisualStyleBackColor = true;
            this.btnDogovor.Click += new System.EventHandler(this.btnDogovor_Click);
            // 
            // btnExtend
            // 
            this.btnExtend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExtend.Location = new System.Drawing.Point(641, 3);
            this.btnExtend.Name = "btnExtend";
            this.btnExtend.Size = new System.Drawing.Size(189, 23);
            this.btnExtend.TabIndex = 13;
            this.btnExtend.Text = "Продлить договор\\оплату";
            this.btnExtend.UseVisualStyleBackColor = true;
            this.btnExtend.Click += new System.EventHandler(this.btnExtend_Click);
            // 
            // btnRegion
            // 
            this.btnRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegion.Location = new System.Drawing.Point(836, 3);
            this.btnRegion.Name = "btnRegion";
            this.btnRegion.Size = new System.Drawing.Size(189, 23);
            this.btnRegion.TabIndex = 14;
            this.btnRegion.Text = "Привязать к региону";
            this.btnRegion.UseVisualStyleBackColor = true;
            this.btnRegion.Click += new System.EventHandler(this.btnRegion_Click);
            // 
            // btnDownloadedFiles
            // 
            this.btnDownloadedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownloadedFiles.Location = new System.Drawing.Point(1031, 3);
            this.btnDownloadedFiles.Name = "btnDownloadedFiles";
            this.btnDownloadedFiles.Size = new System.Drawing.Size(189, 23);
            this.btnDownloadedFiles.TabIndex = 15;
            this.btnDownloadedFiles.Text = "Загруженные док-ты";
            this.btnDownloadedFiles.UseVisualStyleBackColor = true;
            this.btnDownloadedFiles.Click += new System.EventHandler(this.btnDownloadedFiles_Click);
            // 
            // panel7
            // 
            this.panel7.AutoScroll = true;
            this.panel7.Controls.Add(this.tlpCruisePrice);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 481);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(385, 90);
            this.panel7.TabIndex = 6;
            // 
            // tlpCruisePrice
            // 
            this.tlpCruisePrice.ColumnCount = 1;
            this.tlpCruisePrice.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCruisePrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCruisePrice.Location = new System.Drawing.Point(0, 0);
            this.tlpCruisePrice.Name = "tlpCruisePrice";
            this.tlpCruisePrice.RowCount = 1;
            this.tlpCruisePrice.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCruisePrice.Size = new System.Drawing.Size(385, 10);
            this.tlpCruisePrice.TabIndex = 0;
            // 
            // rtbInfo
            // 
            this.rtbInfo.EnableAutoDragDrop = true;
            this.rtbInfo.Location = new System.Drawing.Point(107, -78);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ReadOnly = true;
            this.rtbInfo.Size = new System.Drawing.Size(305, 187);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            this.rtbInfo.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbInfo_LinkClicked);
            // 
            // wspBook
            // 
            webPreferences1.AcceptLanguage = "ru";
            webPreferences1.Databases = true;
            webPreferences1.JavascriptViewChangeSource = false;
            webPreferences1.JavascriptViewEvents = false;
            webPreferences1.JavascriptViewExecute = false;
            webPreferences1.PdfJS = false;
            this.wspBook.Preferences = webPreferences1;
            this.wspBook.Views.Add(this.wbBook);
            // 
            // frmNewOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 862);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmNewOptions";
            this.Activated += new System.EventHandler(this.frmNewOptions_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmNewOptions_FormClosed);
            this.Load += new System.EventHandler(this.frmNewOptions_Load);
            this.Shown += new System.EventHandler(this.frmNewOptions_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tcOptions.ResumeLayout(false);
            this.tcpCruise.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tlpDataOfBronCruise.ResumeLayout(false);
            this.tlpDataOfBronCruise.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.tcpHotel.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tcpInshur.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.tcpVisa.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.tcpDopPaket.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tcpAvia.ResumeLayout(false);
            this.tableLayoutPanel19.ResumeLayout(false);
            this.tcpOther.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.tcPartnerPutevka.ResumeLayout(false);
            this.tcpPartner.ResumeLayout(false);
            this.tlpWebBrowser.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tcpPutevka.ResumeLayout(false);
            this.tlpPutevka.ResumeLayout(false);
            this.scPutevka.Panel1.ResumeLayout(false);
            this.scPutevka.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scPutevka)).EndInit();
            this.scPutevka.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPutevka)).EndInit();
            this.tcProblemChanges.ResumeLayout(false);
            this.tcpProblem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProblems)).EndInit();
            this.tcpChanges.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChanges)).EndInit();
            this.tcpItinerary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItinerary)).EndInit();
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel16.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.tlpTurists.ResumeLayout(false);
            this.tlpTurists.PerformLayout();
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel17.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TableLayoutPanel tlpCruisePrice;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbDGcod;
        private System.Windows.Forms.TextBox tbDateBegin;
        private System.Windows.Forms.TextBox tbDateEnd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tbUslug;
        private System.Windows.Forms.TextBox tbAccept;
        private System.Windows.Forms.TextBox tbNoAccept;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TableLayoutPanel tlpTurists;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.TextBox tbPax;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TableLayoutPanel tlpWebBrowser;
        private System.Windows.Forms.Button btnDocumenSettings;
        private System.Windows.Forms.Panel panel12;
        private TableLayoutPanel tableLayoutPanel6;
        private Button btnEnter;
        private TextBox tbAdress;
        private Awesomium.Windows.Forms.WebControl wbBook;
        private Button btnDogovor;
        private Button btnAccount;
        private Button btnExtend;
        private Button btnClose;
        private Button btnExit;
        public Awesomium.Windows.Forms.WebSessionProvider wspBook;
        internal TabControl tcOptions;
        private TabPage tcpCruise;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private TableLayoutPanel tlpDataOfBronCruise;
        private Label label1;
        private TextBox tbCabinNomber;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TableLayoutPanel tableLayoutPanel4;
        private DateTimePicker dtpOption;
        private MaskedTextBox mtbTime;
        private ComboBox cbCabinDef;
        private TextBox tbNomberOptions;
        private TextBox tbSpecCanc;
        private Label label8;
        private TextBox tbCabinCategory;
        private TableLayoutPanel tableLayoutPanel7;
        private CheckBox cbIsBook;
        private Label label9;
        private Label label10;
        private Label label11;
        private CheckBox cbDocumentGet;
        private CheckBox cbDocumentQuery;
        private Button btnOk;
        private ComboBox cbCruises;
        private Label label6;
        private Label label7;
        private Panel panel2;
        private TableLayoutPanel tlpCruiseBl1;
        private Panel panel9;
        private TableLayoutPanel tlpBonusCruise;
        private TabPage tcpHotel;
        private TableLayoutPanel tableLayoutPanel9;
        private ComboBox cbHotels;
        private Label label14;
        private Panel panel5;
        private TableLayoutPanel tlpHotels;
        private Label label15;
        private TableLayoutPanel tableLayoutPanel12;
        private CheckBox cbHotelOk;
        private Button btnHotelOk;
        private TabPage tcpInshur;
        private TableLayoutPanel tableLayoutPanel13;
        private ComboBox cbInshur;
        private Label label18;
        private TableLayoutPanel tableLayoutPanel14;
        private Label label19;
        private CheckBox cbInshurCreate;
        private Button btnOkInshur;
        private Button btnInshur;
        private Panel panel8;
        private TableLayoutPanel tlpInshur;
        private TabPage tcpVisa;
        private TableLayoutPanel tableLayoutPanel3;
        private ComboBox cbVisa;
        private Panel panel11;
        private TableLayoutPanel tableLayoutPanel18;
        private Label label24;
        private Label label34;
        private TabPage tcpDopPaket;
        private TableLayoutPanel tableLayoutPanel8;
        private ComboBox cbDopPaket;
        private Label label12;
        private Label label13;
        private Panel panel3;
        private TableLayoutPanel tlpDopPaket;
        private Panel panel4;
        private TableLayoutPanel tableLayoutPanel10;
        private TabPage tcpOther;
        private TableLayoutPanel tableLayoutPanel11;
        private ComboBox cbOther;
        private Panel panel6;
        private TableLayoutPanel tlpOther;
        private Label label16;
        private Label label17;
        private TableLayoutPanel tlpPutevka;
        private TabControl tcPartnerPutevka;
        private TabPage tcpPartner;
        private TabPage tcpPutevka;
        private DataGridView dgvPutevka;
        private SplitContainer scPutevka;
        private Button btnRegion;
        private Button btnDownloadedFiles;
        private TabPage tcpAvia;
        private TableLayoutPanel tableLayoutPanel19;
        private ComboBox cbAvia;
        private RichTextBox rtbAvia;
        private TabControl tcProblemChanges;
        private TabPage tcpProblem;
        private DataGridView dgvProblems;
        private TabPage tcpChanges;
        private DataGridView dgvChanges;
        private TabPage tcpItinerary;
        private DataGridView dgvItinerary;
        private TabPage tcpMessages;
        private TabPage tcpSettings;
    }
}