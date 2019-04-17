﻿using System.Windows.Forms;
using terms_prepaid.UserControls;


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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSerchDogovors));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDogovor = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.daysTasks = new terms_prepaid.UserControls.ucDayTasks();
            this.TasksTitlePanel = new System.Windows.Forms.Panel();
            this.lblDayAllTasksCount = new System.Windows.Forms.Label();
            this.lblDayAllTasks = new System.Windows.Forms.Label();
            this.lblDayTasksCount = new System.Windows.Forms.Label();
            this.lblDayTasksDate = new System.Windows.Forms.Label();
            this.lblDayTasks = new System.Windows.Forms.Label();
            this.TasksBottomPanel = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.picWarning2 = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.picWarning1 = new System.Windows.Forms.PictureBox();
            this.TaskMorePanel = new System.Windows.Forms.Panel();
            this.lblShowTasks = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDateCreate = new System.Windows.Forms.CheckBox();
            this.dtpCreateDateS = new System.Windows.Forms.DateTimePicker();
            this.dtpCreateDatePo = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbNonDep = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.cbTurDate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpTurDatePo = new System.Windows.Forms.DateTimePicker();
            this.dtpTurDateS = new System.Windows.Forms.DateTimePicker();
            this.cbAnnul = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tbShip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbdgcode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbParnerNumber = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbTurist = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.clbClasses = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.cbRealizator = new System.Windows.Forms.ComboBox();
            this.cbBronir = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.cbDocument = new System.Windows.Forms.ComboBox();
            this.cbVisa = new System.Windows.Forms.ComboBox();
            this.cbDogovor = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labelClear = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.tbProblemBron = new System.Windows.Forms.TextBox();
            this.btnMyBrony = new System.Windows.Forms.Button();
            this.btnNewBrony = new System.Windows.Forms.Button();
            this.btnProblemBrony = new System.Windows.Forms.Button();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.AdditionalServiceHost = new System.Windows.Forms.Integration.ElementHost();
            this.OptionHost = new System.Windows.Forms.Integration.ElementHost();
            this.RequestJournalButtonHost = new System.Windows.Forms.Integration.ElementHost();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnBronir = new System.Windows.Forms.Button();
            this.btnRealiz = new System.Windows.Forms.Button();
            this.btnNewMessages = new System.Windows.Forms.Button();
            this.cmsNewMessages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tms15Min = new System.Windows.Forms.ToolStripMenuItem();
            this.tms20Min = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDay = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMessages = new System.Windows.Forms.TextBox();
            this.lStatus = new terms_prepaid.Helper_Classes.RichTextBoxEx();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDay = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.lbDocument = new System.Windows.Forms.ListBox();
            this.lbVisa = new System.Windows.Forms.ListBox();
            this.lbRealizator = new System.Windows.Forms.ListBox();
            this.lbDogovor = new System.Windows.Forms.ListBox();
            this.lbBronir = new System.Windows.Forms.ListBox();
            this.lbStatus = new System.Windows.Forms.ListBox();
            this.lMain = new System.Windows.Forms.Label();
            this.btnAllBron = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBonusesAll = new System.Windows.Forms.Label();
            this.lblBonusesMy = new System.Windows.Forms.Label();
            this.timeRefreshProb = new System.Windows.Forms.Timer(this.components);
            this.timeRefreshMessage = new System.Windows.Forms.Timer(this.components);
            this.timeRefreshRequestJournal = new System.Windows.Forms.Timer(this.components);
            this.timePulse = new System.Windows.Forms.Timer(this.components);
            this.timePause = new System.Windows.Forms.Timer(this.components);
            this.instalitionTime = new System.Windows.Forms.Timer(this.components);
            this.timePaydDogovor = new System.Windows.Forms.Timer(this.components);
            this.dayTasksTime = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDogovor)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.TasksTitlePanel.SuspendLayout();
            this.TasksBottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning1)).BeginInit();
            this.TaskMorePanel.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.cmsNewMessages.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1424, 851);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // dgvDogovor
            // 
            this.dgvDogovor.AllowUserToAddRows = false;
            this.dgvDogovor.AllowUserToDeleteRows = false;
            this.dgvDogovor.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDogovor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDogovor.ColumnHeadersHeight = 50;
            this.dgvDogovor.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDogovor.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDogovor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDogovor.Location = new System.Drawing.Point(3, 283);
            this.dgvDogovor.MultiSelect = false;
            this.dgvDogovor.Name = "dgvDogovor";
            this.dgvDogovor.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDogovor.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDogovor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDogovor.Size = new System.Drawing.Size(1418, 565);
            this.dgvDogovor.TabIndex = 2;
            this.dgvDogovor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDogovor_CellContentClick);
            this.dgvDogovor.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDogovor_CellContentClick_1);
            this.dgvDogovor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDogovor_CellDoubleClick);
            this.dgvDogovor.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDogovor_ColumnHeaderMouseClick);
            this.dgvDogovor.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvDogovor_RowPrePaint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 246F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 247F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel8, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel9, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnFind, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel10, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelClear, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 75);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1418, 163);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.daysTasks, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.TasksTitlePanel, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.TasksBottomPanel, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.TaskMorePanel, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1092, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 6;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(326, 163);
            this.tableLayoutPanel4.TabIndex = 25;
            // 
            // daysTasks
            // 
            this.daysTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.daysTasks.Location = new System.Drawing.Point(2, 33);
            this.daysTasks.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.daysTasks.Name = "daysTasks";
            this.daysTasks.Size = new System.Drawing.Size(324, 89);
            this.daysTasks.TabIndex = 25;
            // 
            // TasksTitlePanel
            // 
            this.TasksTitlePanel.Controls.Add(this.lblDayAllTasksCount);
            this.TasksTitlePanel.Controls.Add(this.lblDayAllTasks);
            this.TasksTitlePanel.Controls.Add(this.lblDayTasksCount);
            this.TasksTitlePanel.Controls.Add(this.lblDayTasksDate);
            this.TasksTitlePanel.Controls.Add(this.lblDayTasks);
            this.TasksTitlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TasksTitlePanel.Location = new System.Drawing.Point(0, 13);
            this.TasksTitlePanel.Margin = new System.Windows.Forms.Padding(0);
            this.TasksTitlePanel.Name = "TasksTitlePanel";
            this.TasksTitlePanel.Size = new System.Drawing.Size(326, 20);
            this.TasksTitlePanel.TabIndex = 29;
            // 
            // lblDayAllTasksCount
            // 
            this.lblDayAllTasksCount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDayAllTasksCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDayAllTasksCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDayAllTasksCount.Location = new System.Drawing.Point(251, 0);
            this.lblDayAllTasksCount.Name = "lblDayAllTasksCount";
            this.lblDayAllTasksCount.Size = new System.Drawing.Size(32, 18);
            this.lblDayAllTasksCount.TabIndex = 31;
            this.lblDayAllTasksCount.Text = "-";
            this.lblDayAllTasksCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDayAllTasks
            // 
            this.lblDayAllTasks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDayAllTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDayAllTasks.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDayAllTasks.Location = new System.Drawing.Point(217, 1);
            this.lblDayAllTasks.Name = "lblDayAllTasks";
            this.lblDayAllTasks.Size = new System.Drawing.Size(35, 17);
            this.lblDayAllTasks.TabIndex = 30;
            this.lblDayAllTasks.Text = "ВСЕ";
            this.lblDayAllTasks.Click += new System.EventHandler(this.lblDayAllTasks_Click);
            // 
            // lblDayTasksCount
            // 
            this.lblDayTasksCount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDayTasksCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDayTasksCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDayTasksCount.Location = new System.Drawing.Point(104, 0);
            this.lblDayTasksCount.Name = "lblDayTasksCount";
            this.lblDayTasksCount.Size = new System.Drawing.Size(32, 18);
            this.lblDayTasksCount.TabIndex = 29;
            this.lblDayTasksCount.Text = "-";
            this.lblDayTasksCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDayTasksDate
            // 
            this.lblDayTasksDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDayTasksDate.Location = new System.Drawing.Point(140, 1);
            this.lblDayTasksDate.Name = "lblDayTasksDate";
            this.lblDayTasksDate.Size = new System.Drawing.Size(75, 17);
            this.lblDayTasksDate.TabIndex = 28;
            this.lblDayTasksDate.Text = "НА ДАТУ";
            this.lblDayTasksDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDayTasks
            // 
            this.lblDayTasks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDayTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDayTasks.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDayTasks.Location = new System.Drawing.Point(5, 1);
            this.lblDayTasks.Name = "lblDayTasks";
            this.lblDayTasks.Size = new System.Drawing.Size(100, 17);
            this.lblDayTasks.TabIndex = 26;
            this.lblDayTasks.Text = "ОТЛ. ЗАДАЧИ";
            this.lblDayTasks.Click += new System.EventHandler(this.lblDayTasks_Click);
            // 
            // TasksBottomPanel
            // 
            this.TasksBottomPanel.Controls.Add(this.label20);
            this.TasksBottomPanel.Controls.Add(this.picWarning2);
            this.TasksBottomPanel.Controls.Add(this.label18);
            this.TasksBottomPanel.Controls.Add(this.picWarning1);
            this.TasksBottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TasksBottomPanel.Location = new System.Drawing.Point(0, 142);
            this.TasksBottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TasksBottomPanel.Name = "TasksBottomPanel";
            this.TasksBottomPanel.Size = new System.Drawing.Size(326, 1);
            this.TasksBottomPanel.TabIndex = 27;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(130, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Приоритет 2";
            // 
            // picWarning2
            // 
            this.picWarning2.Image = global::terms_prepaid.Properties.Resources.warning_2;
            this.picWarning2.Location = new System.Drawing.Point(108, 2);
            this.picWarning2.Name = "picWarning2";
            this.picWarning2.Size = new System.Drawing.Size(16, 16);
            this.picWarning2.TabIndex = 2;
            this.picWarning2.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(25, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Приоритет 1";
            // 
            // picWarning1
            // 
            this.picWarning1.Image = global::terms_prepaid.Properties.Resources.warning_2;
            this.picWarning1.Location = new System.Drawing.Point(5, 2);
            this.picWarning1.Name = "picWarning1";
            this.picWarning1.Size = new System.Drawing.Size(16, 16);
            this.picWarning1.TabIndex = 0;
            this.picWarning1.TabStop = false;
            // 
            // TaskMorePanel
            // 
            this.TaskMorePanel.BackColor = System.Drawing.Color.AliceBlue;
            this.TaskMorePanel.Controls.Add(this.lblShowTasks);
            this.TaskMorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TaskMorePanel.Location = new System.Drawing.Point(2, 122);
            this.TaskMorePanel.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.TaskMorePanel.Name = "TaskMorePanel";
            this.TaskMorePanel.Size = new System.Drawing.Size(324, 20);
            this.TaskMorePanel.TabIndex = 30;
            // 
            // lblShowTasks
            // 
            this.lblShowTasks.AutoSize = true;
            this.lblShowTasks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblShowTasks.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblShowTasks.Location = new System.Drawing.Point(135, 1);
            this.lblShowTasks.Name = "lblShowTasks";
            this.lblShowTasks.Size = new System.Drawing.Size(89, 15);
            this.lblShowTasks.TabIndex = 4;
            this.lblShowTasks.Text = "Показать еще";
            this.lblShowTasks.Click += new System.EventHandler(this.lblShowTasks_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbDateCreate, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.dtpCreateDateS, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.dtpCreateDatePo, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.cbNonDep, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.btnClear, 1, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(154, 127);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(28, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "По дате создания";
            // 
            // cbDateCreate
            // 
            this.cbDateCreate.AutoSize = true;
            this.cbDateCreate.Location = new System.Drawing.Point(3, 3);
            this.cbDateCreate.Name = "cbDateCreate";
            this.cbDateCreate.Size = new System.Drawing.Size(15, 13);
            this.cbDateCreate.TabIndex = 12;
            this.cbDateCreate.UseVisualStyleBackColor = true;
            // 
            // dtpCreateDateS
            // 
            this.dtpCreateDateS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCreateDateS.Location = new System.Drawing.Point(28, 22);
            this.dtpCreateDateS.Name = "dtpCreateDateS";
            this.dtpCreateDateS.Size = new System.Drawing.Size(123, 20);
            this.dtpCreateDateS.TabIndex = 10;
            this.dtpCreateDateS.ValueChanged += new System.EventHandler(this.dtpCreateDateS_ValueChanged);
            // 
            // dtpCreateDatePo
            // 
            this.dtpCreateDatePo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCreateDatePo.Location = new System.Drawing.Point(28, 48);
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
            this.label10.Location = new System.Drawing.Point(3, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 26);
            this.label10.TabIndex = 15;
            this.label10.Text = "С";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 26);
            this.label1.TabIndex = 16;
            this.label1.Text = "По";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cbNonDep
            // 
            this.cbNonDep.AutoSize = true;
            this.cbNonDep.Checked = true;
            this.cbNonDep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel5.SetColumnSpan(this.cbNonDep, 2);
            this.cbNonDep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbNonDep.Location = new System.Drawing.Point(3, 86);
            this.cbNonDep.Name = "cbNonDep";
            this.cbNonDep.Size = new System.Drawing.Size(148, 32);
            this.cbNonDep.TabIndex = 17;
            this.cbNonDep.Text = "Не показывать уехавших";
            this.cbNonDep.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(28, 74);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(123, 6);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Очистить фильтр";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.cbTurDate, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.dtpTurDatePo, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.dtpTurDateS, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.cbAnnul, 0, 4);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(163, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 6;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(154, 127);
            this.tableLayoutPanel7.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(3, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 26);
            this.label11.TabIndex = 16;
            this.label11.Text = "С";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbTurDate
            // 
            this.cbTurDate.AutoSize = true;
            this.cbTurDate.Location = new System.Drawing.Point(3, 3);
            this.cbTurDate.Name = "cbTurDate";
            this.cbTurDate.Size = new System.Drawing.Size(15, 13);
            this.cbTurDate.TabIndex = 13;
            this.cbTurDate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(28, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "По дате заезда";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(3, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 26);
            this.label4.TabIndex = 15;
            this.label4.Text = "По";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpTurDatePo
            // 
            this.dtpTurDatePo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTurDatePo.Location = new System.Drawing.Point(28, 48);
            this.dtpTurDatePo.Name = "dtpTurDatePo";
            this.dtpTurDatePo.Size = new System.Drawing.Size(123, 20);
            this.dtpTurDatePo.TabIndex = 3;
            this.dtpTurDatePo.ValueChanged += new System.EventHandler(this.dtpTurDatePo_ValueChanged);
            // 
            // dtpTurDateS
            // 
            this.dtpTurDateS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTurDateS.Location = new System.Drawing.Point(28, 22);
            this.dtpTurDateS.Name = "dtpTurDateS";
            this.dtpTurDateS.Size = new System.Drawing.Size(123, 20);
            this.dtpTurDateS.TabIndex = 7;
            this.dtpTurDateS.ValueChanged += new System.EventHandler(this.dtpTurDateS_ValueChanged);
            // 
            // cbAnnul
            // 
            this.cbAnnul.AutoSize = true;
            this.cbAnnul.Checked = true;
            this.cbAnnul.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel7.SetColumnSpan(this.cbAnnul, 2);
            this.cbAnnul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbAnnul.Location = new System.Drawing.Point(3, 86);
            this.cbAnnul.Name = "cbAnnul";
            this.cbAnnul.Size = new System.Drawing.Size(148, 32);
            this.cbAnnul.TabIndex = 6;
            this.cbAnnul.Text = "Не показывать аннулированые";
            this.cbAnnul.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbAnnul.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tbShip, 0, 7);
            this.tableLayoutPanel6.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbdgcode, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label15, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.tbParnerNumber, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label17, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.tbTurist, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.label19, 0, 6);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(323, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 9;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel6, 2);
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(142, 157);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // tbShip
            // 
            this.tbShip.BackColor = System.Drawing.SystemColors.Window;
            this.tbShip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbShip.Location = new System.Drawing.Point(3, 133);
            this.tbShip.Name = "tbShip";
            this.tbShip.ReadOnly = true;
            this.tbShip.Size = new System.Drawing.Size(136, 20);
            this.tbShip.TabIndex = 20;
            this.tbShip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbShip_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "По номеру путевки";
            // 
            // tbdgcode
            // 
            this.tbdgcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbdgcode.Location = new System.Drawing.Point(3, 22);
            this.tbdgcode.Name = "tbdgcode";
            this.tbdgcode.Size = new System.Drawing.Size(136, 20);
            this.tbdgcode.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(3, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 14);
            this.label15.TabIndex = 7;
            this.label15.Text = "По № брони у партнера";
            this.label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tbParnerNumber
            // 
            this.tbParnerNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbParnerNumber.Location = new System.Drawing.Point(3, 59);
            this.tbParnerNumber.Name = "tbParnerNumber";
            this.tbParnerNumber.Size = new System.Drawing.Size(136, 20);
            this.tbParnerNumber.TabIndex = 8;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(3, 79);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(136, 14);
            this.label17.TabIndex = 17;
            this.label17.Text = "По фамилии туриста";
            this.label17.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tbTurist
            // 
            this.tbTurist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTurist.Location = new System.Drawing.Point(3, 96);
            this.tbTurist.Name = "tbTurist";
            this.tbTurist.Size = new System.Drawing.Size(136, 20);
            this.tbTurist.TabIndex = 18;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(3, 116);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(136, 14);
            this.label19.TabIndex = 19;
            this.label19.Text = "По названию лайнера";
            this.label19.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Controls.Add(this.clbClasses, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(471, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel8, 2);
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(125, 157);
            this.tableLayoutPanel8.TabIndex = 5;
            // 
            // clbClasses
            // 
            this.clbClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbClasses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clbClasses.FormattingEnabled = true;
            this.clbClasses.Location = new System.Drawing.Point(3, 23);
            this.clbClasses.Name = "clbClasses";
            this.clbClasses.Size = new System.Drawing.Size(119, 131);
            this.clbClasses.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 20);
            this.label9.TabIndex = 23;
            this.label9.Text = "По услугам";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.cbRealizator, 0, 5);
            this.tableLayoutPanel9.Controls.Add(this.cbBronir, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.cbStatus, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(602, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 7;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel9, 2);
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(240, 157);
            this.tableLayoutPanel9.TabIndex = 6;
            // 
            // cbRealizator
            // 
            this.cbRealizator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbRealizator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRealizator.DropDownWidth = 300;
            this.cbRealizator.FormattingEnabled = true;
            this.cbRealizator.Location = new System.Drawing.Point(3, 128);
            this.cbRealizator.Name = "cbRealizator";
            this.cbRealizator.Size = new System.Drawing.Size(234, 21);
            this.cbRealizator.TabIndex = 25;
            this.cbRealizator.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.cbStatus_ChangeUICues);
            // 
            // cbBronir
            // 
            this.cbBronir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbBronir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBronir.DropDownWidth = 300;
            this.cbBronir.FormattingEnabled = true;
            this.cbBronir.Location = new System.Drawing.Point(3, 78);
            this.cbBronir.Name = "cbBronir";
            this.cbBronir.Size = new System.Drawing.Size(234, 21);
            this.cbBronir.TabIndex = 24;
            this.cbBronir.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.cbStatus_ChangeUICues);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(234, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "По реализатору";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(3, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(234, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "По бронировщику";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(234, 25);
            this.label8.TabIndex = 22;
            this.label8.Text = "По статусу";
            // 
            // cbStatus
            // 
            this.cbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.DropDownWidth = 300;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(3, 28);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(234, 21);
            this.cbStatus.TabIndex = 23;
            this.cbStatus.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.cbStatus_ChangeUICues);
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.SpringGreen;
            this.btnFind.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFind.Location = new System.Drawing.Point(163, 136);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(150, 24);
            this.btnFind.TabIndex = 24;
            this.btnFind.Text = "Обновить";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.cbDocument, 0, 5);
            this.tableLayoutPanel10.Controls.Add(this.cbVisa, 0, 3);
            this.tableLayoutPanel10.Controls.Add(this.cbDogovor, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.label14, 0, 4);
            this.tableLayoutPanel10.Controls.Add(this.label13, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(848, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 7;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel10, 2);
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(241, 157);
            this.tableLayoutPanel10.TabIndex = 7;
            // 
            // cbDocument
            // 
            this.cbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDocument.DropDownWidth = 300;
            this.cbDocument.FormattingEnabled = true;
            this.cbDocument.Location = new System.Drawing.Point(3, 128);
            this.cbDocument.Name = "cbDocument";
            this.cbDocument.Size = new System.Drawing.Size(235, 21);
            this.cbDocument.TabIndex = 26;
            this.cbDocument.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.cbStatus_ChangeUICues);
            // 
            // cbVisa
            // 
            this.cbVisa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbVisa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVisa.DropDownWidth = 300;
            this.cbVisa.FormattingEnabled = true;
            this.cbVisa.Location = new System.Drawing.Point(3, 78);
            this.cbVisa.Name = "cbVisa";
            this.cbVisa.Size = new System.Drawing.Size(235, 21);
            this.cbVisa.TabIndex = 25;
            this.cbVisa.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.cbStatus_ChangeUICues);
            // 
            // cbDogovor
            // 
            this.cbDogovor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDogovor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDogovor.DropDownWidth = 300;
            this.cbDogovor.FormattingEnabled = true;
            this.cbDogovor.Location = new System.Drawing.Point(3, 28);
            this.cbDogovor.Name = "cbDogovor";
            this.cbDogovor.Size = new System.Drawing.Size(235, 21);
            this.cbDogovor.TabIndex = 24;
            this.cbDogovor.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.cbStatus_ChangeUICues);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(3, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(235, 25);
            this.label14.TabIndex = 0;
            this.label14.Text = "По статусу док-ов";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(3, 50);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(235, 25);
            this.label13.TabIndex = 0;
            this.label13.Text = "По статусу визы";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(235, 25);
            this.label12.TabIndex = 0;
            this.label12.Text = "По статусу дог-ра";
            // 
            // labelClear
            // 
            this.labelClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelClear.Location = new System.Drawing.Point(3, 133);
            this.labelClear.Name = "labelClear";
            this.labelClear.Size = new System.Drawing.Size(154, 30);
            this.labelClear.TabIndex = 18;
            this.labelClear.Text = "Очистить фильтр";
            this.labelClear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelClear.Click += new System.EventHandler(this.labelClear_Click);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 10;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.tbProblemBron, 3, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnMyBrony, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnNewBrony, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnProblemBrony, 2, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnClients, 5, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnAll, 4, 0);
            this.tableLayoutPanel13.Controls.Add(this.AdditionalServiceHost, 7, 0);
            this.tableLayoutPanel13.Controls.Add(this.OptionHost, 6, 0);
            this.tableLayoutPanel13.Controls.Add(this.RequestJournalButtonHost, 9, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 36);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(1418, 36);
            this.tableLayoutPanel13.TabIndex = 6;
            this.tableLayoutPanel13.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel13_Paint);
            // 
            // tbProblemBron
            // 
            this.tbProblemBron.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbProblemBron.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbProblemBron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbProblemBron.Location = new System.Drawing.Point(441, 13);
            this.tbProblemBron.Name = "tbProblemBron";
            this.tbProblemBron.ReadOnly = true;
            this.tbProblemBron.Size = new System.Drawing.Size(34, 20);
            this.tbProblemBron.TabIndex = 6;
            // 
            // btnMyBrony
            // 
            this.btnMyBrony.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnMyBrony.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMyBrony.Location = new System.Drawing.Point(133, 10);
            this.btnMyBrony.Name = "btnMyBrony";
            this.btnMyBrony.Size = new System.Drawing.Size(124, 23);
            this.btnMyBrony.TabIndex = 4;
            this.btnMyBrony.Text = "Все мои брони";
            this.btnMyBrony.UseVisualStyleBackColor = true;
            this.btnMyBrony.Click += new System.EventHandler(this.btnMyBrony_Click);
            // 
            // btnNewBrony
            // 
            this.btnNewBrony.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnNewBrony.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewBrony.Location = new System.Drawing.Point(3, 10);
            this.btnNewBrony.Name = "btnNewBrony";
            this.btnNewBrony.Size = new System.Drawing.Size(124, 23);
            this.btnNewBrony.TabIndex = 3;
            this.btnNewBrony.Text = "Новые брони";
            this.btnNewBrony.UseVisualStyleBackColor = true;
            this.btnNewBrony.Click += new System.EventHandler(this.btnNewBrony_Click);
            // 
            // btnProblemBrony
            // 
            this.btnProblemBrony.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnProblemBrony.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnProblemBrony.Location = new System.Drawing.Point(263, 10);
            this.btnProblemBrony.Name = "btnProblemBrony";
            this.btnProblemBrony.Size = new System.Drawing.Size(172, 23);
            this.btnProblemBrony.TabIndex = 5;
            this.btnProblemBrony.Text = "Проблемные брони";
            this.btnProblemBrony.UseVisualStyleBackColor = true;
            this.btnProblemBrony.Click += new System.EventHandler(this.btnProblemBrony_Click);
            // 
            // btnClients
            // 
            this.btnClients.BackColor = System.Drawing.Color.DarkGray;
            this.btnClients.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClients.Location = new System.Drawing.Point(506, 10);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(134, 23);
            this.btnClients.TabIndex = 17;
            this.btnClients.Text = "История клиента";
            this.btnClients.UseVisualStyleBackColor = false;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // btnAll
            // 
            this.btnAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAll.Location = new System.Drawing.Point(481, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(19, 23);
            this.btnAll.TabIndex = 19;
            this.btnAll.Text = "Все брони";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Visible = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // AdditionalServiceHost
            // 
            this.AdditionalServiceHost.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AdditionalServiceHost.Location = new System.Drawing.Point(773, 10);
            this.AdditionalServiceHost.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.AdditionalServiceHost.Name = "AdditionalServiceHost";
            this.AdditionalServiceHost.Size = new System.Drawing.Size(170, 24);
            this.AdditionalServiceHost.TabIndex = 21;
            this.AdditionalServiceHost.Text = "elementHost1";
            this.AdditionalServiceHost.Child = null;
            // 
            // OptionHost
            // 
            this.OptionHost.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OptionHost.Location = new System.Drawing.Point(643, 10);
            this.OptionHost.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.OptionHost.Name = "OptionHost";
            this.OptionHost.Size = new System.Drawing.Size(130, 24);
            this.OptionHost.TabIndex = 22;
            this.OptionHost.Text = "elementHost1";
            this.OptionHost.Child = null;
            // 
            // RequestJournalButtonHost
            // 
            this.RequestJournalButtonHost.Dock = System.Windows.Forms.DockStyle.Right;
            this.RequestJournalButtonHost.Location = new System.Drawing.Point(953, 0);
            this.RequestJournalButtonHost.Margin = new System.Windows.Forms.Padding(0);
            this.RequestJournalButtonHost.Name = "RequestJournalButtonHost";
            this.RequestJournalButtonHost.Size = new System.Drawing.Size(465, 36);
            this.RequestJournalButtonHost.TabIndex = 23;
            this.RequestJournalButtonHost.Child = null;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 301F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 235F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btnSetting, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnBronir, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRealiz, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnNewMessages, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbMessages, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lStatus, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 244);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1418, 33);
            this.tableLayoutPanel3.TabIndex = 7;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSetting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSetting.Location = new System.Drawing.Point(1186, 4);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(229, 26);
            this.btnSetting.TabIndex = 26;
            this.btnSetting.Text = "Назначить бронировщика \\реализатора";
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnBronir
            // 
            this.btnBronir.BackColor = System.Drawing.Color.Chocolate;
            this.btnBronir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBronir.Location = new System.Drawing.Point(986, 4);
            this.btnBronir.Name = "btnBronir";
            this.btnBronir.Size = new System.Drawing.Size(194, 26);
            this.btnBronir.TabIndex = 1;
            this.btnBronir.Text = "Назначить меня бронировщиком";
            this.btnBronir.UseVisualStyleBackColor = false;
            this.btnBronir.Click += new System.EventHandler(this.btnBronir_Click);
            // 
            // btnRealiz
            // 
            this.btnRealiz.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRealiz.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRealiz.Location = new System.Drawing.Point(795, 4);
            this.btnRealiz.Name = "btnRealiz";
            this.btnRealiz.Size = new System.Drawing.Size(185, 26);
            this.btnRealiz.TabIndex = 2;
            this.btnRealiz.Text = "Назначить меня реализатором";
            this.btnRealiz.UseVisualStyleBackColor = false;
            this.btnRealiz.Click += new System.EventHandler(this.btnRealiz_Click);
            // 
            // btnNewMessages
            // 
            this.btnNewMessages.ContextMenuStrip = this.cmsNewMessages;
            this.btnNewMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnNewMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewMessages.Location = new System.Drawing.Point(449, 4);
            this.btnNewMessages.Name = "btnNewMessages";
            this.btnNewMessages.Size = new System.Drawing.Size(295, 26);
            this.btnNewMessages.TabIndex = 7;
            this.btnNewMessages.Text = "Переписка \\ комментарии";
            this.btnNewMessages.UseVisualStyleBackColor = true;
            this.btnNewMessages.Click += new System.EventHandler(this.btnNewMessages_Click);
            // 
            // cmsNewMessages
            // 
            this.cmsNewMessages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tms15Min,
            this.tms20Min,
            this.tsmDay});
            this.cmsNewMessages.Name = "cmsNewMessages";
            this.cmsNewMessages.Size = new System.Drawing.Size(204, 70);
            // 
            // tms15Min
            // 
            this.tms15Min.Name = "tms15Min";
            this.tms15Min.Size = new System.Drawing.Size(203, 22);
            this.tms15Min.Text = "Отвечу через 15 минут";
            this.tms15Min.Click += new System.EventHandler(this.tms15Min_Click);
            // 
            // tms20Min
            // 
            this.tms20Min.Name = "tms20Min";
            this.tms20Min.Size = new System.Drawing.Size(203, 22);
            this.tms20Min.Text = "Отвечу через 20 минут";
            this.tms20Min.Click += new System.EventHandler(this.tms20Min_Click);
            // 
            // tsmDay
            // 
            this.tsmDay.Name = "tsmDay";
            this.tsmDay.Size = new System.Drawing.Size(203, 22);
            this.tsmDay.Text = "Отложить до конца дня";
            this.tsmDay.Click += new System.EventHandler(this.tsmDay_Click);
            // 
            // tbMessages
            // 
            this.tbMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMessages.Location = new System.Drawing.Point(750, 10);
            this.tbMessages.Name = "tbMessages";
            this.tbMessages.Size = new System.Drawing.Size(39, 20);
            this.tbMessages.TabIndex = 8;
            this.tbMessages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbMessages.TextChanged += new System.EventHandler(this.tbMessages_TextChanged);
            // 
            // lStatus
            // 
            this.lStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStatus.Location = new System.Drawing.Point(3, 3);
            this.lStatus.Name = "lStatus";
            this.lStatus.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lStatus.Size = new System.Drawing.Size(440, 27);
            this.lStatus.TabIndex = 27;
            this.lStatus.Text = "";
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 9;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel15.Controls.Add(this.tbName, 4, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnClose, 8, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnDay, 7, 0);
            this.tableLayoutPanel15.Controls.Add(this.label16, 6, 0);
            this.tableLayoutPanel15.Controls.Add(this.tableLayoutPanel11, 1, 0);
            this.tableLayoutPanel15.Controls.Add(this.lMain, 3, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnAllBron, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.panel1, 5, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(1418, 30);
            this.tableLayoutPanel15.TabIndex = 8;
            // 
            // tbName
            // 
            this.tbName.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbName.Location = new System.Drawing.Point(563, 0);
            this.tbName.Margin = new System.Windows.Forms.Padding(0);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(140, 26);
            this.tbName.TabIndex = 6;
            this.tbName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Location = new System.Drawing.Point(1337, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDay
            // 
            this.btnDay.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDay.Location = new System.Drawing.Point(1159, 3);
            this.btnDay.Name = "btnDay";
            this.btnDay.Size = new System.Drawing.Size(172, 24);
            this.btnDay.TabIndex = 8;
            this.btnDay.Text = "Установка Дня!";
            this.btnDay.UseVisualStyleBackColor = false;
            this.btnDay.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.Control;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Location = new System.Drawing.Point(896, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(257, 30);
            this.label16.TabIndex = 9;
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.lbDocument, 0, 5);
            this.tableLayoutPanel11.Controls.Add(this.lbVisa, 0, 4);
            this.tableLayoutPanel11.Controls.Add(this.lbRealizator, 0, 3);
            this.tableLayoutPanel11.Controls.Add(this.lbDogovor, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.lbBronir, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.lbStatus, 0, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(223, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 6;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(14, 14);
            this.tableLayoutPanel11.TabIndex = 8;
            this.tableLayoutPanel11.Visible = false;
            // 
            // lbDocument
            // 
            this.lbDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbDocument.FormattingEnabled = true;
            this.lbDocument.ItemHeight = 16;
            this.lbDocument.Location = new System.Drawing.Point(3, 104);
            this.lbDocument.Name = "lbDocument";
            this.lbDocument.Size = new System.Drawing.Size(1, 4);
            this.lbDocument.TabIndex = 1;
            // 
            // lbVisa
            // 
            this.lbVisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbVisa.FormattingEnabled = true;
            this.lbVisa.ItemHeight = 16;
            this.lbVisa.Location = new System.Drawing.Point(3, 84);
            this.lbVisa.Name = "lbVisa";
            this.lbVisa.Size = new System.Drawing.Size(1, 4);
            this.lbVisa.TabIndex = 1;
            // 
            // lbRealizator
            // 
            this.lbRealizator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbRealizator.FormattingEnabled = true;
            this.lbRealizator.ItemHeight = 16;
            this.lbRealizator.Location = new System.Drawing.Point(3, 64);
            this.lbRealizator.Name = "lbRealizator";
            this.lbRealizator.Size = new System.Drawing.Size(1, 4);
            this.lbRealizator.TabIndex = 22;
            // 
            // lbDogovor
            // 
            this.lbDogovor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbDogovor.FormattingEnabled = true;
            this.lbDogovor.ItemHeight = 16;
            this.lbDogovor.Location = new System.Drawing.Point(3, 44);
            this.lbDogovor.Name = "lbDogovor";
            this.lbDogovor.Size = new System.Drawing.Size(1, 4);
            this.lbDogovor.TabIndex = 1;
            // 
            // lbBronir
            // 
            this.lbBronir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbBronir.FormattingEnabled = true;
            this.lbBronir.ItemHeight = 16;
            this.lbBronir.Location = new System.Drawing.Point(3, 24);
            this.lbBronir.Name = "lbBronir";
            this.lbBronir.Size = new System.Drawing.Size(1, 4);
            this.lbBronir.TabIndex = 21;
            // 
            // lbStatus
            // 
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbStatus.FormattingEnabled = true;
            this.lbStatus.HorizontalExtent = 1;
            this.lbStatus.ItemHeight = 16;
            this.lbStatus.Location = new System.Drawing.Point(3, 3);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(1, 4);
            this.lbStatus.TabIndex = 23;
            // 
            // lMain
            // 
            this.lMain.AutoSize = true;
            this.lMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.lMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lMain.Location = new System.Drawing.Point(560, 0);
            this.lMain.Name = "lMain";
            this.lMain.Size = new System.Drawing.Size(0, 30);
            this.lMain.TabIndex = 5;
            this.lMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAllBron
            // 
            this.btnAllBron.BackColor = System.Drawing.Color.Cornsilk;
            this.btnAllBron.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAllBron.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAllBron.Location = new System.Drawing.Point(3, 4);
            this.btnAllBron.Name = "btnAllBron";
            this.btnAllBron.Size = new System.Drawing.Size(214, 23);
            this.btnAllBron.TabIndex = 10;
            this.btnAllBron.Text = "Все  актуальные  брони";
            this.btnAllBron.UseVisualStyleBackColor = false;
            this.btnAllBron.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBonusesAll);
            this.panel1.Controls.Add(this.lblBonusesMy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(703, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 30);
            this.panel1.TabIndex = 11;
            // 
            // lblBonusesAll
            // 
            this.lblBonusesAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBonusesAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBonusesAll.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblBonusesAll.Location = new System.Drawing.Point(98, 6);
            this.lblBonusesAll.Name = "lblBonusesAll";
            this.lblBonusesAll.Size = new System.Drawing.Size(89, 17);
            this.lblBonusesAll.TabIndex = 32;
            this.lblBonusesAll.Text = "Все  бонусы";
            this.lblBonusesAll.Visible = false;
            this.lblBonusesAll.Click += new System.EventHandler(this.lblBonusesAll_Click);
            // 
            // lblBonusesMy
            // 
            this.lblBonusesMy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBonusesMy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBonusesMy.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblBonusesMy.Location = new System.Drawing.Point(8, 6);
            this.lblBonusesMy.Name = "lblBonusesMy";
            this.lblBonusesMy.Size = new System.Drawing.Size(92, 17);
            this.lblBonusesMy.TabIndex = 31;
            this.lblBonusesMy.Text = "Мои  бонусы";
            this.lblBonusesMy.Click += new System.EventHandler(this.lblBonusesMy_Click);
            // 
            // timeRefreshProb
            // 
            this.timeRefreshProb.Interval = 300000;
            this.timeRefreshProb.Tick += new System.EventHandler(this.timeRefreshProb_Tick);
            // 
            // timeRefreshMessage
            // 
            this.timeRefreshMessage.Interval = 300000;
            this.timeRefreshMessage.Tick += new System.EventHandler(this.timeRefreshMessage_Tick);
            // 
            // timeRefreshRequestJournal
            // 
            this.timeRefreshRequestJournal.Interval = 5000;
            this.timeRefreshRequestJournal.Tick += new System.EventHandler(this.timeRefreshRequestJournal_Tick);
            // 
            // timePulse
            // 
            this.timePulse.Interval = 500;
            this.timePulse.Tick += new System.EventHandler(this.timePulse_Tick);
            // 
            // timePause
            // 
            this.timePause.Tick += new System.EventHandler(this.timePause_Tick);
            // 
            // instalitionTime
            // 
            this.instalitionTime.Enabled = true;
            this.instalitionTime.Interval = 300000;
            this.instalitionTime.Tick += new System.EventHandler(this.instalitionTime_Tick);
            // 
            // timePaydDogovor
            // 
            this.timePaydDogovor.Enabled = true;
            this.timePaydDogovor.Interval = 1200000;
            this.timePaydDogovor.Tick += new System.EventHandler(this.timePaydDogovor_Tick);
            // 
            // dayTasksTime
            // 
            this.dayTasksTime.Enabled = true;
            this.dayTasksTime.Interval = 300000;
            this.dayTasksTime.Tick += new System.EventHandler(this.dayTasksTime_Tick);
            // 
            // frmSerchDogovors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 851);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1440, 600);
            this.Name = "frmSerchDogovors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рабочее место ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSerchDogovors_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSerchDogovors_FormClosed);
            this.Load += new System.EventHandler(this.frmSerchDogovors_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSerchDogovors_KeyDown);
            this.Move += new System.EventHandler(this.frmSerchDogovors_Move);
            this.Resize += new System.EventHandler(this.frmSerchDogovors_Resize);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDogovor)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.TasksTitlePanel.ResumeLayout(false);
            this.TasksBottomPanel.ResumeLayout(false);
            this.TasksBottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning1)).EndInit();
            this.TaskMorePanel.ResumeLayout(false);
            this.TaskMorePanel.PerformLayout();
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
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.cmsNewMessages.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
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
        private Label label13;
        private ListBox lbVisa;
        private Label label14;
        private ListBox lbDocument;
        private TableLayoutPanel tableLayoutPanel15;
        private Label lMain;
        private TextBox tbName;
        private Button btnClients;
        private TextBox tbProblemBron;
        private Button btnNewMessages;
        private Timer timeRefreshMessage;
        private Timer timeRefreshRequestJournal;
        private Timer timePulse;
        private ContextMenuStrip cmsNewMessages;
        private ToolStripMenuItem tms15Min;
        private ToolStripMenuItem tms20Min;
        private Timer timePause;
        private ToolStripMenuItem tsmDay;
        private Timer timeRefreshProb;
        private Button btnClear;
        private Button btnAll;
        private Label label15;
        private TextBox tbParnerNumber;
        private Button btnClose;
        private Button btnDay;
        private Label label16;
        private Timer instalitionTime;
        private Label label17;
        private TextBox tbTurist;
        private TextBox tbMessages;
        private Timer timePaydDogovor;
        private ComboBox cbRealizator;
        private ComboBox cbBronir;
        private ComboBox cbStatus;
        private ComboBox cbDocument;
        private ComboBox cbVisa;
        private ComboBox cbDogovor;
        private Helper_Classes.RichTextBoxEx lStatus;
        private UserControls.ucDayTasks daysTasks;
        private Timer dayTasksTime;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblDayTasks;
        private CheckBox cbNonDep;
        private System.Windows.Forms.Integration.ElementHost AdditionalServiceHost;
        private System.Windows.Forms.Integration.ElementHost OptionHost;
        private System.Windows.Forms.Integration.ElementHost RequestJournalButtonHost;
        private Label labelClear;
        private Label label19;
        private TextBox tbShip;
        private Button btnAllBron;
        private Panel TasksBottomPanel;
        private Label label20;
        private PictureBox picWarning2;
        private Label label18;
        private PictureBox picWarning1;
        private Panel TasksTitlePanel;
        private Label lblDayTasksDate;
        private Label lblDayTasksCount;
        private Label lblShowTasks;
        private Panel TaskMorePanel;
        private Label lblDayAllTasksCount;
        private Label lblDayAllTasks;
        private Label lblBonusesMy;
        private Panel panel1;
        private Label lblBonusesAll;


    }
}