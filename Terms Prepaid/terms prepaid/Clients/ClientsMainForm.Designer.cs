namespace lanta.Clients
{
    partial class ClientsMainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьСписокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.объединитьДублиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалениеПробеловВКонцеФамилийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сравнитьСПрошлымГодомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.спискиРассылокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подсчётСтатистикиВладельцевКартToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.похожиеКлиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.разделениеНомеровКартНаСериюИНомерToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.флагиСтранToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.продолжитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CL_KEY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CL_NAMERUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CL_SHORTNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CL_SNAMERUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CL_BIRTHDAY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CL_PHONE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl_mail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CD_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CD_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CD_IsValid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CL_MINCOST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CL_MAXCOST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CL_BIRTHDAY_STR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_CL_BIRTHDAY = new System.Windows.Forms.TextBox();
            this.label_CL_BIRTHDAY = new System.Windows.Forms.Label();
            this.textBox_CL_NAMERUS = new System.Windows.Forms.TextBox();
            this.label_CL_NAMERUS = new System.Windows.Forms.Label();
            this.label_CL_FNAMERUS = new System.Windows.Forms.Label();
            this.textBox_CL_FNAMERUS = new System.Windows.Forms.TextBox();
            this.label_CL_SNAMERUS = new System.Windows.Forms.Label();
            this.textBox_CL_SNAMERUS = new System.Windows.Forms.TextBox();
            this.label_CD_NUMBER = new System.Windows.Forms.Label();
            this.textBox_CD_NUMBER = new System.Windows.Forms.TextBox();
            this.label_CL_PHONE = new System.Windows.Forms.Label();
            this.textBox_CL_PHONE = new System.Windows.Forms.TextBox();
            this.label_CL_EMAIL = new System.Windows.Forms.Label();
            this.textBox_CL_EMAIL = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.button_select = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Khaki;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.продолжитьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1359, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStripMenuItem5.Image = global::lanta.Clients.Properties.Resources.Редактировать;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(242, 25);
            this.toolStripMenuItem5.Text = "Просмотр / редактирование";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.AutoToolTip = true;
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStripMenuItem4.Image = global::lanta.Clients.Properties.Resources.Удалить;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(96, 25);
            this.toolStripMenuItem4.Text = "Удалить";
            this.toolStripMenuItem4.ToolTipText = "Удалить клиента";
            this.toolStripMenuItem4.Visible = false;
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStripMenuItem3.Image = global::lanta.Clients.Properties.Resources.Вставить;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(222, 25);
            this.toolStripMenuItem3.Text = "Добавить нового клинета";
            this.toolStripMenuItem3.ToolTipText = "Добавить клиента";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.ForestGreen;
            this.toolStripMenuItem1.Image = global::lanta.Clients.Properties.Resources.Выход;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(69, 25);
            this.toolStripMenuItem1.Text = "Выход";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьСписокToolStripMenuItem,
            this.объединитьДублиToolStripMenuItem,
            this.удалениеПробеловВКонцеФамилийToolStripMenuItem,
            this.поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem,
            this.сравнитьСПрошлымГодомToolStripMenuItem,
            this.спискиРассылокToolStripMenuItem,
            this.подсчётСтатистикиВладельцевКартToolStripMenuItem,
            this.похожиеКлиентыToolStripMenuItem,
            this.разделениеНомеровКартНаСериюИНомерToolStripMenuItem,
            this.флагиСтранToolStripMenuItem});
            this.toolStripMenuItem2.Image = global::lanta.Clients.Properties.Resources.Сервисы;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(84, 25);
            this.toolStripMenuItem2.Text = "Сервисы";
            this.toolStripMenuItem2.Visible = false;
            // 
            // обновитьСписокToolStripMenuItem
            // 
            this.обновитьСписокToolStripMenuItem.Image = global::lanta.Clients.Properties.Resources.Обновить;
            this.обновитьСписокToolStripMenuItem.Name = "обновитьСписокToolStripMenuItem";
            this.обновитьСписокToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.обновитьСписокToolStripMenuItem.Text = "Обновить список";
            this.обновитьСписокToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // объединитьДублиToolStripMenuItem
            // 
            this.объединитьДублиToolStripMenuItem.Name = "объединитьДублиToolStripMenuItem";
            this.объединитьДублиToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.объединитьДублиToolStripMenuItem.Text = "Объединить дубли";
            this.объединитьДублиToolStripMenuItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // удалениеПробеловВКонцеФамилийToolStripMenuItem
            // 
            this.удалениеПробеловВКонцеФамилийToolStripMenuItem.Name = "удалениеПробеловВКонцеФамилийToolStripMenuItem";
            this.удалениеПробеловВКонцеФамилийToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.удалениеПробеловВКонцеФамилийToolStripMenuItem.Text = "Улучшение адресов";
            this.удалениеПробеловВКонцеФамилийToolStripMenuItem.Visible = false;
            this.удалениеПробеловВКонцеФамилийToolStripMenuItem.Click += new System.EventHandler(this.удалениеПробеловВКонцеФамилийToolStripMenuItem_Click);
            // 
            // поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem
            // 
            this.поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem.Name = "поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem";
            this.поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem.Text = "Поиск русских фамилий с иностранными символами";
            this.поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem.Visible = false;
            this.поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem.Click += new System.EventHandler(this.поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem_Click);
            // 
            // сравнитьСПрошлымГодомToolStripMenuItem
            // 
            this.сравнитьСПрошлымГодомToolStripMenuItem.Name = "сравнитьСПрошлымГодомToolStripMenuItem";
            this.сравнитьСПрошлымГодомToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.сравнитьСПрошлымГодомToolStripMenuItem.Text = "Импорт клиентов из других баз";
            this.сравнитьСПрошлымГодомToolStripMenuItem.Visible = false;
            this.сравнитьСПрошлымГодомToolStripMenuItem.Click += new System.EventHandler(this.сравнитьСПрошлымГодомToolStripMenuItem_Click);
            // 
            // спискиРассылокToolStripMenuItem
            // 
            this.спискиРассылокToolStripMenuItem.Name = "спискиРассылокToolStripMenuItem";
            this.спискиРассылокToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.спискиРассылокToolStripMenuItem.Text = "Списки рассылок";
            this.спискиРассылокToolStripMenuItem.Click += new System.EventHandler(this.спискиРассылокToolStripMenuItem_Click);
            // 
            // подсчётСтатистикиВладельцевКартToolStripMenuItem
            // 
            this.подсчётСтатистикиВладельцевКартToolStripMenuItem.Name = "подсчётСтатистикиВладельцевКартToolStripMenuItem";
            this.подсчётСтатистикиВладельцевКартToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.подсчётСтатистикиВладельцевКартToolStripMenuItem.Text = "Подсчёт статистики";
            this.подсчётСтатистикиВладельцевКартToolStripMenuItem.Click += new System.EventHandler(this.подсчётСтатистикиВладельцевКартToolStripMenuItem_Click);
            // 
            // похожиеКлиентыToolStripMenuItem
            // 
            this.похожиеКлиентыToolStripMenuItem.Name = "похожиеКлиентыToolStripMenuItem";
            this.похожиеКлиентыToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.похожиеКлиентыToolStripMenuItem.Text = "Поиск похожих клиентов";
            this.похожиеКлиентыToolStripMenuItem.Click += new System.EventHandler(this.похожиеКлиентыToolStripMenuItem_Click);
            // 
            // разделениеНомеровКартНаСериюИНомерToolStripMenuItem
            // 
            this.разделениеНомеровКартНаСериюИНомерToolStripMenuItem.Name = "разделениеНомеровКартНаСериюИНомерToolStripMenuItem";
            this.разделениеНомеровКартНаСериюИНомерToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.разделениеНомеровКартНаСериюИНомерToolStripMenuItem.Text = "Разделение номеров карт на серию и номер";
            this.разделениеНомеровКартНаСериюИНомерToolStripMenuItem.Visible = false;
            this.разделениеНомеровКартНаСериюИНомерToolStripMenuItem.Click += new System.EventHandler(this.разделениеНомеровКартНаСериюИНомерToolStripMenuItem_Click);
            // 
            // флагиСтранToolStripMenuItem
            // 
            this.флагиСтранToolStripMenuItem.Name = "флагиСтранToolStripMenuItem";
            this.флагиСтранToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.флагиСтранToolStripMenuItem.Text = "Флаги стран";
            this.флагиСтранToolStripMenuItem.Visible = false;
            this.флагиСтранToolStripMenuItem.Click += new System.EventHandler(this.флагиСтранToolStripMenuItem_Click);
            // 
            // продолжитьToolStripMenuItem
            // 
            this.продолжитьToolStripMenuItem.Name = "продолжитьToolStripMenuItem";
            this.продолжитьToolStripMenuItem.Size = new System.Drawing.Size(89, 25);
            this.продолжитьToolStripMenuItem.Text = "Продолжить";
            this.продолжитьToolStripMenuItem.Visible = false;
            this.продолжитьToolStripMenuItem.Click += new System.EventHandler(this.продолжитьToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CL_KEY,
            this.CL_NAMERUS,
            this.CL_SHORTNAME,
            this.CL_SNAMERUS,
            this.CL_BIRTHDAY,
            this.CL_PHONE,
            this.cl_mail,
            this.CD_Code,
            this.CD_Number,
            this.CD_IsValid,
            this.CL_MINCOST,
            this.CL_MAXCOST,
            this.CL_BIRTHDAY_STR});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 176);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1353, 372);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // CL_KEY
            // 
            this.CL_KEY.DataPropertyName = "CL_KEY";
            this.CL_KEY.HeaderText = "Ключ";
            this.CL_KEY.Name = "CL_KEY";
            this.CL_KEY.ReadOnly = true;
            this.CL_KEY.Visible = false;
            // 
            // CL_NAMERUS
            // 
            this.CL_NAMERUS.DataPropertyName = "CL_NAMERUS";
            this.CL_NAMERUS.HeaderText = "Фамилия";
            this.CL_NAMERUS.Name = "CL_NAMERUS";
            this.CL_NAMERUS.ReadOnly = true;
            // 
            // CL_SHORTNAME
            // 
            this.CL_SHORTNAME.DataPropertyName = "CL_FNAMERUS";
            this.CL_SHORTNAME.HeaderText = "Имя";
            this.CL_SHORTNAME.Name = "CL_SHORTNAME";
            this.CL_SHORTNAME.ReadOnly = true;
            // 
            // CL_SNAMERUS
            // 
            this.CL_SNAMERUS.DataPropertyName = "CL_SNAMERUS";
            this.CL_SNAMERUS.HeaderText = "Отчество";
            this.CL_SNAMERUS.Name = "CL_SNAMERUS";
            this.CL_SNAMERUS.ReadOnly = true;
            // 
            // CL_BIRTHDAY
            // 
            this.CL_BIRTHDAY.DataPropertyName = "CL_BIRTHDAY";
            this.CL_BIRTHDAY.HeaderText = "Дата рождения";
            this.CL_BIRTHDAY.Name = "CL_BIRTHDAY";
            this.CL_BIRTHDAY.ReadOnly = true;
            // 
            // CL_PHONE
            // 
            this.CL_PHONE.DataPropertyName = "CL_PHONE";
            this.CL_PHONE.HeaderText = "Телефон";
            this.CL_PHONE.Name = "CL_PHONE";
            this.CL_PHONE.ReadOnly = true;
            // 
            // cl_mail
            // 
            this.cl_mail.DataPropertyName = "cl_mail";
            this.cl_mail.HeaderText = "E-Mail";
            this.cl_mail.Name = "cl_mail";
            this.cl_mail.ReadOnly = true;
            // 
            // CD_Code
            // 
            this.CD_Code.DataPropertyName = "CD_Code";
            this.CD_Code.HeaderText = "Тип карты";
            this.CD_Code.Name = "CD_Code";
            this.CD_Code.ReadOnly = true;
            // 
            // CD_Number
            // 
            this.CD_Number.DataPropertyName = "CD_Number";
            this.CD_Number.HeaderText = "Номер карты";
            this.CD_Number.Name = "CD_Number";
            this.CD_Number.ReadOnly = true;
            // 
            // CD_IsValid
            // 
            this.CD_IsValid.DataPropertyName = "CD_IsValid";
            this.CD_IsValid.FalseValue = "0";
            this.CD_IsValid.HeaderText = "Активна";
            this.CD_IsValid.Name = "CD_IsValid";
            this.CD_IsValid.ReadOnly = true;
            this.CD_IsValid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CD_IsValid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CD_IsValid.TrueValue = "1";
            // 
            // CL_MINCOST
            // 
            this.CL_MINCOST.DataPropertyName = "CL_MINCOST";
            this.CL_MINCOST.HeaderText = "Минимальная стоимость";
            this.CL_MINCOST.Name = "CL_MINCOST";
            this.CL_MINCOST.ReadOnly = true;
            // 
            // CL_MAXCOST
            // 
            this.CL_MAXCOST.DataPropertyName = "CL_MAXCOST";
            this.CL_MAXCOST.HeaderText = "Максимальная стоимость";
            this.CL_MAXCOST.Name = "CL_MAXCOST";
            this.CL_MAXCOST.ReadOnly = true;
            // 
            // CL_BIRTHDAY_STR
            // 
            this.CL_BIRTHDAY_STR.DataPropertyName = "CL_BIRTHDAY_STR";
            this.CL_BIRTHDAY_STR.HeaderText = "Дата для фильтра";
            this.CL_BIRTHDAY_STR.Name = "CL_BIRTHDAY_STR";
            this.CL_BIRTHDAY_STR.ReadOnly = true;
            this.CL_BIRTHDAY_STR.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.PaleGreen;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1359, 551);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.textBox_CL_BIRTHDAY, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_CL_BIRTHDAY, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox_CL_NAMERUS, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_CL_NAMERUS, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_CL_FNAMERUS, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox_CL_FNAMERUS, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_CL_SNAMERUS, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBox_CL_SNAMERUS, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_CD_NUMBER, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBox_CD_NUMBER, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_CL_PHONE, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox_CL_PHONE, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_CL_EMAIL, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBox_CL_EMAIL, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1353, 104);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // textBox_CL_BIRTHDAY
            // 
            this.textBox_CL_BIRTHDAY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_CL_BIRTHDAY.Location = new System.Drawing.Point(1016, 5);
            this.textBox_CL_BIRTHDAY.Name = "textBox_CL_BIRTHDAY";
            this.textBox_CL_BIRTHDAY.Size = new System.Drawing.Size(332, 20);
            this.textBox_CL_BIRTHDAY.TabIndex = 11;
            this.textBox_CL_BIRTHDAY.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_CL_BIRTHDAY
            // 
            this.label_CL_BIRTHDAY.AutoSize = true;
            this.label_CL_BIRTHDAY.Location = new System.Drawing.Point(679, 2);
            this.label_CL_BIRTHDAY.Name = "label_CL_BIRTHDAY";
            this.label_CL_BIRTHDAY.Size = new System.Drawing.Size(86, 13);
            this.label_CL_BIRTHDAY.TabIndex = 10;
            this.label_CL_BIRTHDAY.Text = "Дата рождения";
            // 
            // textBox_CL_NAMERUS
            // 
            this.textBox_CL_NAMERUS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_CL_NAMERUS.Location = new System.Drawing.Point(342, 5);
            this.textBox_CL_NAMERUS.Name = "textBox_CL_NAMERUS";
            this.textBox_CL_NAMERUS.Size = new System.Drawing.Size(329, 20);
            this.textBox_CL_NAMERUS.TabIndex = 2;
            this.textBox_CL_NAMERUS.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_CL_NAMERUS
            // 
            this.label_CL_NAMERUS.AutoSize = true;
            this.label_CL_NAMERUS.Location = new System.Drawing.Point(5, 2);
            this.label_CL_NAMERUS.Name = "label_CL_NAMERUS";
            this.label_CL_NAMERUS.Size = new System.Drawing.Size(56, 13);
            this.label_CL_NAMERUS.TabIndex = 1;
            this.label_CL_NAMERUS.Text = "Фамилия";
            // 
            // label_CL_FNAMERUS
            // 
            this.label_CL_FNAMERUS.AutoSize = true;
            this.label_CL_FNAMERUS.Location = new System.Drawing.Point(5, 27);
            this.label_CL_FNAMERUS.Name = "label_CL_FNAMERUS";
            this.label_CL_FNAMERUS.Size = new System.Drawing.Size(29, 13);
            this.label_CL_FNAMERUS.TabIndex = 3;
            this.label_CL_FNAMERUS.Text = "Имя";
            // 
            // textBox_CL_FNAMERUS
            // 
            this.textBox_CL_FNAMERUS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_CL_FNAMERUS.Location = new System.Drawing.Point(342, 30);
            this.textBox_CL_FNAMERUS.Name = "textBox_CL_FNAMERUS";
            this.textBox_CL_FNAMERUS.Size = new System.Drawing.Size(329, 20);
            this.textBox_CL_FNAMERUS.TabIndex = 4;
            this.textBox_CL_FNAMERUS.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_CL_SNAMERUS
            // 
            this.label_CL_SNAMERUS.AutoSize = true;
            this.label_CL_SNAMERUS.Location = new System.Drawing.Point(5, 52);
            this.label_CL_SNAMERUS.Name = "label_CL_SNAMERUS";
            this.label_CL_SNAMERUS.Size = new System.Drawing.Size(54, 13);
            this.label_CL_SNAMERUS.TabIndex = 5;
            this.label_CL_SNAMERUS.Text = "Отчество";
            // 
            // textBox_CL_SNAMERUS
            // 
            this.textBox_CL_SNAMERUS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_CL_SNAMERUS.Location = new System.Drawing.Point(342, 55);
            this.textBox_CL_SNAMERUS.Name = "textBox_CL_SNAMERUS";
            this.textBox_CL_SNAMERUS.Size = new System.Drawing.Size(329, 20);
            this.textBox_CL_SNAMERUS.TabIndex = 6;
            this.textBox_CL_SNAMERUS.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_CD_NUMBER
            // 
            this.label_CD_NUMBER.AutoSize = true;
            this.label_CD_NUMBER.Location = new System.Drawing.Point(679, 52);
            this.label_CD_NUMBER.Name = "label_CD_NUMBER";
            this.label_CD_NUMBER.Size = new System.Drawing.Size(152, 13);
            this.label_CD_NUMBER.TabIndex = 5;
            this.label_CD_NUMBER.Text = "Номер дисконтной карточки";
            // 
            // textBox_CD_NUMBER
            // 
            this.textBox_CD_NUMBER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_CD_NUMBER.Location = new System.Drawing.Point(1016, 55);
            this.textBox_CD_NUMBER.Name = "textBox_CD_NUMBER";
            this.textBox_CD_NUMBER.Size = new System.Drawing.Size(332, 20);
            this.textBox_CD_NUMBER.TabIndex = 6;
            this.textBox_CD_NUMBER.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_CL_PHONE
            // 
            this.label_CL_PHONE.AutoSize = true;
            this.label_CL_PHONE.Location = new System.Drawing.Point(679, 27);
            this.label_CL_PHONE.Name = "label_CL_PHONE";
            this.label_CL_PHONE.Size = new System.Drawing.Size(114, 13);
            this.label_CL_PHONE.TabIndex = 8;
            this.label_CL_PHONE.Text = "Контактный телефон";
            this.label_CL_PHONE.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox_CL_PHONE
            // 
            this.textBox_CL_PHONE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_CL_PHONE.Location = new System.Drawing.Point(1016, 30);
            this.textBox_CL_PHONE.Name = "textBox_CL_PHONE";
            this.textBox_CL_PHONE.Size = new System.Drawing.Size(332, 20);
            this.textBox_CL_PHONE.TabIndex = 9;
            this.textBox_CL_PHONE.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_CL_EMAIL
            // 
            this.label_CL_EMAIL.AutoSize = true;
            this.label_CL_EMAIL.Location = new System.Drawing.Point(5, 77);
            this.label_CL_EMAIL.Name = "label_CL_EMAIL";
            this.label_CL_EMAIL.Size = new System.Drawing.Size(35, 13);
            this.label_CL_EMAIL.TabIndex = 12;
            this.label_CL_EMAIL.Text = "E-mail";
            // 
            // textBox_CL_EMAIL
            // 
            this.textBox_CL_EMAIL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_CL_EMAIL.Location = new System.Drawing.Point(342, 80);
            this.textBox_CL_EMAIL.Name = "textBox_CL_EMAIL";
            this.textBox_CL_EMAIL.Size = new System.Drawing.Size(329, 20);
            this.textBox_CL_EMAIL.TabIndex = 13;
            this.textBox_CL_EMAIL.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button_select);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1353, 30);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "не указан";
            // 
            // button_select
            // 
            this.button_select.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_select.Location = new System.Drawing.Point(0, 0);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(161, 30);
            this.button_select.TabIndex = 2;
            this.button_select.Text = "Выбрать в туристы";
            this.button_select.UseVisualStyleBackColor = true;
            this.button_select.Click += new System.EventHandler(this.button_select_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1353, 27);
            this.label1.TabIndex = 6;
            this.label1.Text = "Фильтры поиска";
            // 
            // ClientsMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 580);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClientsMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Постоянные клиенты";
            this.Load += new System.EventHandler(this.ClientsMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem обновитьСписокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem объединитьДублиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалениеПробеловВКонцеФамилийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискРусскихФамилийСИностраннымиСимволамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сравнитьСПрошлымГодомToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem спискиРассылокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подсчётСтатистикиВладельцевКартToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem продолжитьToolStripMenuItem;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem похожиеКлиентыToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBox_CL_BIRTHDAY;
        private System.Windows.Forms.Label label_CL_BIRTHDAY;
        private System.Windows.Forms.TextBox textBox_CL_NAMERUS;
        private System.Windows.Forms.Label label_CL_NAMERUS;
        private System.Windows.Forms.Label label_CL_FNAMERUS;
        private System.Windows.Forms.TextBox textBox_CL_FNAMERUS;
        private System.Windows.Forms.Label label_CL_SNAMERUS;
        private System.Windows.Forms.TextBox textBox_CL_SNAMERUS;
        private System.Windows.Forms.Label label_CD_NUMBER;
        private System.Windows.Forms.TextBox textBox_CD_NUMBER;
        private System.Windows.Forms.Label label_CL_PHONE;
        private System.Windows.Forms.TextBox textBox_CL_PHONE;
        private System.Windows.Forms.ToolStripMenuItem разделениеНомеровКартНаСериюИНомерToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_KEY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_NAMERUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_SHORTNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_SNAMERUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_BIRTHDAY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_PHONE;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl_mail;
        private System.Windows.Forms.DataGridViewTextBoxColumn CD_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn CD_Number;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CD_IsValid;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_MINCOST;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_MAXCOST;
        private System.Windows.Forms.DataGridViewTextBoxColumn CL_BIRTHDAY_STR;
        public System.Windows.Forms.Button button_select;
        private System.Windows.Forms.ToolStripMenuItem флагиСтранToolStripMenuItem;
        private System.Windows.Forms.Label label_CL_EMAIL;
        private System.Windows.Forms.TextBox textBox_CL_EMAIL;
        private System.Windows.Forms.Label label1;
    }
}

