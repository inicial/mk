using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
//using Rep10027.Helpers.WorkWithData;
using System.Windows.Forms;
using Awesomium.Core;
using Awesomium.Core.Data;
using Awesomium.Windows.Forms;
using DocumentServices;
using terms_prepaid.Helpers;
using terms_prepaid.Forms;
using terms_prepaid.Properties;
using terms_prepaid.UserControls;
using terms_prepaid.WebGetInfo;
using lanta.SQLConfig;
using System.Reflection;

namespace terms_prepaid
{

    public partial class frmNewOptions : Form
    {
        private string winINI = "C:\\Windows\\win.ini";
        private string _tempDirectory = Path.GetTempPath();
        private List<int> _updatedChanges = new List<int>();
        private List<OKProblem> _updatedProblem = new List<OKProblem>();
        private AccessClass _access = new AccessClass(WorkWithData.Connection);
        private string _dgCode;
        private string _cabinCategori;
        private int _agencyKey;

        private const string FlightBtnStr = "Добавить услугу";
        private const string FlightStr = "Авиаперелет";
        private const string OtherStr = "Другое";

        private List<dogovorlistline> _cruises = new List<dogovorlistline>(),
                                      _cruisesRiver = new List<dogovorlistline>(),
                                      _dopPakets = new List<dogovorlistline>(),
                                      _hotels = new List<dogovorlistline>(),
                                      _inshur = new List<dogovorlistline>(),
                                      _other = new List<dogovorlistline>(),
                                      _visa = new List<dogovorlistline>(),
                                      _avia = new List<dogovorlistline>();
                                

        private DataTable _servisesmas = new DataTable(),
                          _DogovorList = new DataTable(),
                          _Itinerary = new DataTable(),
                          _changes,
                          _problems;

        private DataTable _cabinLevels = new DataTable();
        private WebSession sessionId;
        private frmMessages messagesForm;
        private int _type = 0;
        //private CefSharp.WinForms.ChromiumWebBrowser wbbrouse = new ChromiumWebBrowser();
        struct OKProblem
        {
            public int problemCode;
            public int problemOkStatus;
            public string problemMessage;
        }

        private WebSession InitializeCoreAndSession()
        {
            WebSession session;
            if (!WebCore.IsInitialized)
                WebCore.Initialize(new WebConfig()
                    {
                        AssetProtocol = "https",
                        LogLevel = LogLevel.Normal
                    });

            // Build a data path string. In this case, a Cache folder under our executing directory.
            // - If the folder does not exist, it will be created.
            // - The path should always point to a writeable location.
            string dataPath = String.Format("{0}{1}Cache", Path.GetTempPath(), Path.DirectorySeparatorChar);

            // Check if a session synchronizing to this data path, is already created;
            // if not, create a new one.
            session = WebCore.Sessions[dataPath] ??
                      WebCore.CreateWebSession(dataPath, new WebPreferences() {});

            session.AddDataSource(DataSource.CATCH_ALL, new frmPopupWindow.MyDataSource());

            // The core must be initialized by now. Print the core version.
            //  Debug.Print(WebCore.Version.ToString());

            // Return the session.
            return session;
        }

        //public frmNewOptions(string dgCode)
        //{
        //    InitializeComponent();
        //     sessionId=InitializeCoreAndSession();
        //    // tcProblemChanges.TabPages.Remove(tcpChanges);
        //    wbBook.WebSession = sessionId;
        //   // wbBook.InitializeView =
        //    _dgCode = dgCode;
        //    SetAccess();
        //    GetDate();


        //}
        /// <summary>
        /// конструктор класса форма Работа с заказом
        /// </summary>
        /// <param name="dgCode">DGCOD договора</param>
        /// <param name="type">
        /// Параметр отвечает за открытие опеределенной вкладки на форме:
        /// 0-Изначальное значение(открывает начальный экран)
        /// 1-Новые сообщения (открывает экран с открытой перепиской)
        /// 2-Изменения по путевке(открывает вкладку с имзменениями по путевке)
        /// 3-Проблемы по путевке 
        /// 4-Маршрут круиза
        /// 5-заявление от клиента
        /// </param>
        public frmNewOptions(string dgCode, int type = 0)
        {
            InitializeComponent();
            sessionId = InitializeCoreAndSession();
            // tcProblemChanges.TabPages.Remove(tcpChanges);
            wbBook.WebSession = sessionId;
            _dgCode = dgCode;
            SetAccess();
            GetDate();
            this._type = type;
            //tcProblemChanges.SelectTab(tcProblemChanges.TabPages[0]);
            if (tcProblemChanges.TabPages.Count == 1 && tcProblemChanges.TabPages[0].Equals(tcpMessages))
            {
                scPutevka.SplitterDistance = panel12.Height - 27;
            }
            else
            {
                tcProblemChanges_Selecting(tcProblemChanges,
                                           new TabControlCancelEventArgs(tcProblemChanges.TabPages[0], 0, false,
                                                                         TabControlAction.Selected));
            }
            CreateContextMenuForOptionButton();

        }

        private void CreateContextMenuForOptionButton()
        {
            btnFlight.Text = FlightBtnStr;
            ToolStripMenuItem flightMenuItem = new ToolStripMenuItem(FlightStr);
            ToolStripMenuItem otherMenuItem = new ToolStripMenuItem(OtherStr);
            contextMenuStrip1.Items.AddRange(new[] { flightMenuItem, otherMenuItem });
            btnFlight.ContextMenuStrip = contextMenuStrip1;
            flightMenuItem.Click += (s, e) => ShowFligth();
            //otherMenuItem.Click += (s, e) => btnFlight.Text = OtherStr;
        }

        private void AddTuristLine()
        {
            tlpTurists.RowCount++;
            tlpTurists.Height += 27;
            tlpTurists.RowStyles.Insert(0,
                                        new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));

            //tableLayoutPanel1.RowStyles[3].Height = tlpTurists.Height;
        }

        #region Права доступа

        /// <summary>
        /// Распределение прав доступа
        /// </summary>
        private void SetAccess()
        {

            if (_access.isSuperViser)
            {
                SetSuper();
            }
            else if (_access.isBronir)
            {
                SetBronir();
            }
            else if (_access.isRealize)
            {
                SetRealize();
            }

        }

        private void SetSuper()
        {
            tlpDataOfBronCruise.Enabled = true;
            tlpBonusCruise.Enabled = true;
            btnRegion.Visible = true;
        }

        private void SetRealize()
        {
            tcPartnerPutevka.TabPages.Remove(tcpPartner);
            tlpDataOfBronCruise.Enabled = false;
            tlpBonusCruise.Enabled = false;
            btnRegion.Visible =false;
        }

        private void SetBronir()
        {
            tlpDataOfBronCruise.Enabled = true;
            tlpBonusCruise.Enabled = true;
            btnRegion.Visible = false;

        }

        #endregion

        /// <summary>
        /// Получение данных
        /// </summary>
        private void GetDate(bool showWeb = true)
        {

            //Заполнение второй строки
            tbDGcod.Text = _dgCode;
            if (_access.isBron)
            {
                tcPartnerPutevka.SelectTab(tcpPartner);
            }
            else
            {
                tcPartnerPutevka.SelectTab(tcpPutevka);
            }

            // rbPutevka.Checked = true;
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(
                        @"select DG_CODE,DG_NMEN,DG_TURDATE,DG_NDAY,DG_PARTNERKEY from  dbo.tbl_Dogovor where DG_CODE=@dgcode",
                        WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", _dgCode);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    tbDateBegin.Text = dt.Rows[0].Field<DateTime>("DG_TURDATE").Date.ToString("dd MMMM yy");
                    tbDateEnd.Text =
                        dt.Rows[0].Field<DateTime>("DG_TURDATE")
                                  .AddDays((int) dt.Rows[0].Field<double>("DG_NDAY") - 1)
                                  .Date.ToString("dd MMMM yy");
                    tbPax.Text = dt.Rows[0].Field<Int16>("DG_NMEN").ToString();
                    _agencyKey = dt.Rows[0].Field<int>("DG_PARTNERKEY");
                }
            }
            if (_agencyKey != 0)
            {
                btnDogovor.Visible = false;
            }
            //Заполнение туристов


            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.selectTurists, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", _dgCode);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                int i = 1;
                tlpTurists.Controls.Clear();
                tlpTurists.RowCount = 2;
                tlpTurists.RowStyles.Clear();
                tlpTurists.Height = 27;
                tlpTurists.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
                this.tlpTurists.Controls.Add(this.label29, 0, 0);
                this.tlpTurists.Controls.Add(this.label30, 1, 0);
                this.tlpTurists.Controls.Add(this.label31, 2, 0);
                this.tlpTurists.Controls.Add(this.label32, 3, 0);
                this.tlpTurists.Controls.Add(this.label33, 4, 0);
                this.tlpTurists.Controls.Add(this.label36, 5, 0);
                tlpTurists.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                foreach (DataRow row in dt.Rows)
                {
                    AddTuristLine();
                    TextBox tbName = GetTextBox(row.Field<string>("TU_NAMELAT"));
                    tlpTurists.Controls.Add(tbName, 0, i);
                    tbName.Tag = row.Field<int>("TU_KEY");
                    tbName.Name = "tbName" + "::" + row.Field<int>("TU_KEY").ToString();
                    TextBox tbFName = GetTextBox(row.Field<string>("TU_FNAMELAT"));
                    tlpTurists.Controls.Add(tbFName, 1, i);
                    tbFName.Tag = row.Field<int>("TU_KEY");
                    tbFName.Name = "tbFName" + "::" + row.Field<int>("TU_KEY").ToString();
                    TextBox tbSName = GetTextBox(row.Field<string>("TU_SNAMELAT"));
                    tlpTurists.Controls.Add(tbSName, 2, i);
                    tbSName.Tag = row.Field<int>("TU_KEY");
                    tbSName.Name = "tbSName" + "::" + row.Field<int>("TU_KEY").ToString();
                    TextBox tbBIRTHDAY = GetTextBox(row.Field<string>("TU_BIRTHDAY"));
                    tlpTurists.Controls.Add(tbBIRTHDAY, 3, i);
                    tbBIRTHDAY.Tag = row.Field<int>("TU_KEY");
                    tbBIRTHDAY.Name = "tbBIRTHDAY" + "::" + row.Field<int>("TU_KEY").ToString();
                    //Загран паспорт 
                    tlpTurists.Controls.Add(GetButton("Анкета", row.Field<int>("TU_KEY")), 4, i);

                    //////TextBox tbPasportType = GetTextBox(row.Field<string>("TU_PASPORTTYPE"));
                    //////tbPasportType.Tag = row.Field<int>("TU_KEY");
                    //////TextBox tbPasportNum = GetTextBox(row.Field<string>("TU_PASPORTNUM"));
                    //////tbPasportNum.Tag = row.Field<int>("TU_KEY");
                    //////TableLayoutPanel tlpPasport = GetTablePanel(new List<object> { tbPasportType, GetLabel("№"), tbPasportNum }.ToArray());
                    //////tlpPasport.Tag = row.Field<int>("TU_KEY");
                    //////tlpTurists.Controls.Add(tlpPasport, 4, i);
                    //////tbPasportType.Name = "tbPasportType::" + row.Field<int>("TU_KEY").ToString();
                    //////tbPasportNum.Name = "tbPasportNum::" + row.Field<int>("TU_KEY").ToString();
                    //////tlpPasport.Name = "tlpPasport::" + row.Field<int>("TU_KEY").ToString();
                    //////TextBox tbPasportDate = GetTextBox(row.Fwield<string>("TU_PASPORTDATE"));
                    //////tbPasportDate.Tag = row.Field<int>("TU_KEY");
                    //////tlpTurists.Controls.Add(tbPasportDate, 5, i);
                    //////tbPasportDate.Name = "tbPasportDate::" + row.Field<int>("TU_KEY").ToString();
                    //////TextBox tbPasportDateEnd = GetTextBox(row.Field<string>("TU_PASPORTDATEEND"));
                    //////tbPasportDateEnd.Tag = row.Field<int>("TU_KEY");
                    //////tlpTurists.Controls.Add(tbPasportDateEnd, 6, i);
                    //////tbPasportDate.Name = "tbPasportDateEnd::" + row.Field<int>("TU_KEY").ToString();
                    //Российский паспорт
                    tlpTurists.Controls.Add(GetButtonRus("Анкета", row.Field<int>("TU_KEY")), 5, i);

                    //TextBox tbPasportTypeRu = GetTextBox(row.Field<string>("TU_PASPRUSER"));
                    //tbPasportTypeRu.Tag = row.Field<int>("TU_KEY");
                    //TextBox tbPasportNumRU = GetTextBox(row.Field<string>("TU_PASPRUNUM"));
                    //tbPasportNumRU.Tag = row.Field<int>("TU_KEY");
                    //TableLayoutPanel tlpPasportRU = GetTablePanel(new List<object> { tbPasportTypeRu, GetLabel("№"), tbPasportNumRU }.ToArray());
                    //tlpPasportRU.Tag = row.Field<int>("TU_KEY");
                    //tlpTurists.Controls.Add(tlpPasportRU, 7, i);
                    //tbPasportTypeRu.Name = "tbPasportTypeRU::" + row.Field<int>("TU_KEY").ToString();
                    //tbPasportNumRU.Name = "tbPasportNumRU::" + row.Field<int>("TU_KEY").ToString();
                    //tlpPasportRU.Name = "tlpPasportRU::" + row.Field<int>("TU_KEY").ToString();
                    //tlpTurists.Controls.Add(GetButton("Анкета", row.Field<int>("TU_KEY")),8,i);
                    i++;
                }
            }
            tlpTurists.Height = (int.Parse(tbPax.Text) + 1)*30;
            //List<string> dList = new List<string>();
            //foreach (var control in tlpTurists.Controls)
            //{
            //    TextBox tb = control as TextBox;
            //    if (tb!=null)
            //    {
            //        dList.Add(tb.Name);
            //    }
            //}
            if ((int.Parse(tbPax.Text) + 1) > 3)
            {
                tableLayoutPanel1.RowStyles[3].Height = 90;
                panel10.Height = 90;
                tableLayoutPanel1.AutoScroll = true;
            }

            else
            {
                panel10.Height = (int.Parse(tbPax.Text) + 1)*30;
                tableLayoutPanel1.RowStyles[3].Height = (int.Parse(tbPax.Text) + 1)*30;
            }

            //Заполнение услуг/потверждено /не подтверждно
            _servisesmas.Clear();


            using (SqlCommand com = new SqlCommand("MK_lk_servises_putevka", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", _dgCode);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(_servisesmas);
            }
            DataRow[] Port = _servisesmas.Select("tipe=1 and order<>1");
            foreach (DataRow row in Port)
            {
                _servisesmas.Rows.Remove(row);
            }
            int ob = _servisesmas.Rows.Count, pod = _servisesmas.Select("DL_CONTROL=0").Count(), nepod = ob - pod;
            tbUslug.Text = ob.ToString();
            tbAccept.Text = pod.ToString();
            tbNoAccept.Text = nepod.ToString();
            //Разбор по услугам и заполнение вкладок

            foreach (DataRow row in _servisesmas.Rows)
            {
                if (row.Field<int>("tipe") == 1 && row.Field<int>("order") == 1)
                {
                    //string brandCode = "";
                    //using (
                    //    SqlDataAdapter adapter =
                    //        new SqlDataAdapter(
                    //            "Select brandcode from  mk_DogovorListAdd where tbl_dogovor_list_key = " +
                    //            row.Field<int>("dl_key").ToString(), WorkWithData.Connection))
                    //{
                    //    DataTable dt = new DataTable();
                    //    adapter.Fill(dt);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        if (dt.Rows[0].Field<string>("brandcode") != null)
                    //        {
                    //            brandCode = dt.Rows[0].Field<string>("brandcode");
                    //        }
                    //    }
                    //}

                    //using (
                    //    SqlDataAdapter adapter =
                    //        new SqlDataAdapter(
                    //            "Select * from  Mk_setting_serch_modul where id_serch_modul = 3 and valye ='" +
                    //            brandCode + "'", WorkWithData.ConnectionTS))
                    //{
                    //    DataTable dt = new DataTable();
                    //    adapter.Fill(dt);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        _cruisesRiver.Add(new dogovorlistline(row.Field<int>("dl_key"), row.Field<string>("dl_name")));
                    //    }
                    //else
                    //{
                    _cruises.Add(new dogovorlistline(row.Field<int>("dl_key"), row.Field<string>("dl_name")));
                    //}
                    //}

                }
                else if (row.Field<int?>("tl_tip") == 149 || row.Field<int?>("tl_tip") == 172)
                {
                    _dopPakets.Add(new dogovorlistline(row.Field<int>("dl_key"), row.Field<string>("dl_name")));
                }
                else if (row.Field<int>("tipe") == 6)
                {
                    _hotels.Add(new dogovorlistline(row.Field<int>("dl_key"), row.Field<string>("dl_name")));
                }
                else if (row.Field<int?>("dl_svkey") == 5)
                {
                    _visa.Add(new dogovorlistline(row.Field<int>("dl_key"), row.Field<string>("dl_name")));
                }
                else if (row.Field<int?>("dl_svkey") == 6)
                {
                    _inshur.Add(new dogovorlistline(row.Field<int>("dl_key"), row.Field<string>("dl_name")));
                }
                else if (row.Field<int?>("dl_svkey") == 3184)
                {

                    DataTable tempTable = WorkWithData.GetAviaName(row.Field<int>("dl_key"));
                    if (_avia.Select(x => x.dlkey == tempTable.Rows[0].Field<int>("BronId")).Count() == 0)
                    {
                        _avia.Add(new dogovorlistline(tempTable.Rows[0].Field<int>("BronId"),
                                                      tempTable.Rows[0].Field<string>("Name")));
                    }

                }
                else if (row.Field<int?>("tipe") != 1)
                {
                    _other.Add(new dogovorlistline(row.Field<int>("dl_key"), row.Field<string>("dl_name")));
                }





            }
            //using (SqlCommand command = new SqlCommand(@"select * from mk_level"))
            //{
            //}
            if (_avia.Count > 0)
            {
                cbAvia.DataSource = _avia;

            }
            else
            {
                tcOptions.TabPages.Remove(tcpAvia);
            }

            if (_cruises.Count > 0)
            {
                cbCruises.DataSource = _cruises;
            }
            else
            {
                //tcpCruise.Visible = false;
                tcOptions.TabPages.Remove(tcpCruise);
            }
            if (_dopPakets.Count > 0)
            {
                cbDopPaket.DataSource = _dopPakets;
            }
            else
            {
                //tcpDopPaket.Visible= false;
                tcOptions.TabPages.Remove(tcpDopPaket);
            }
            if (_hotels.Count > 0)
            {
                cbHotels.DataSource = _hotels;
            }
            else
            {
                //tсpHotel.Visible = false;
                tcOptions.TabPages.Remove(tcpHotel);
            }

            if (_inshur.Count > 0)
            {
                cbInshur.DataSource = _inshur;
            }
            else
            {
                tcOptions.TabPages.Remove(tcpInshur);
            }
            if (_visa.Count > 0)
            {
                cbVisa.DataSource = _visa;
            }
            else
            {
                tcOptions.TabPages.Remove(tcpVisa);
            }
            if (_other.Count > 0)
            {
                cbOther.DataSource = _other;
            }
            else
            {
                tcOptions.TabPages.Remove(tcpOther);
            }
            if (_cruises.Count > 0)
            {
                cbCruises.DataSource = _cruises;
                generateBlock1ToCruise(dogovorlistline.Parse(cbCruises.SelectedItem).dlkey, showWeb);
            }
            else
            {
                //tcpCruise.Visible = false;
                tcOptions.TabPages.Remove(tcpCruise);
            }
            // Заполнение состава путевки и проблем\изменений
            _DogovorList = WorkWithData.GetDogovorList(_dgCode);


            _problems = WorkWithData.GetAllProblems(_dgCode);
            if (_problems.Rows.Count < 1)
            {
                tcProblemChanges.TabPages.Remove(tcpProblem);
            }

            tcpProblem.Text = "Проблемы (" + _problems.Rows.Count.ToString() + ")";


            _changes = WorkWithData.GetChanges(_dgCode);
            if (_changes.Rows.Count < 1)
            {
                tcProblemChanges.TabPages.Remove(tcpChanges);
            }
            tcpChanges.Text = "Изменения (" + _changes.Rows.Count.ToString() + ")";
            if (_changes.Select("MCD_CHANGE_CODE = 2").Count() > 0)
            {
                int itineraryindex = tcProblemChanges.TabPages.IndexOf(tcpItinerary);
                tcpItinerary.Text = "Новое расписание";
                tcProblemChanges.TabPages.Remove(tcpMessages);
                tcProblemChanges.TabPages.Remove(tcpSettings);
                tcProblemChanges.TabPages.Remove(tcpItinerary);
                tcProblemChanges.TabPages.Add(new TabPage()
                    {
                        Text = "Старое расписание",
                        Name = "tcpOldItinerary",
                        TabIndex = itineraryindex

                    });

                tcProblemChanges.TabPages["tcpOldItinerary"].Controls.Add(new DataGridView()
                    {
                        Dock = DockStyle.Fill,
                        Name = "dgvOldItinerary",
                        RowHeadersVisible = false,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                        BackgroundColor = Color.White
                    });
                tcProblemChanges.TabPages.Add(tcpItinerary);
                tcProblemChanges.TabPages.Add(tcpSettings);
                tcProblemChanges.TabPages.Add(tcpMessages);
                
            }

            //Заполнение маршрута
            
            if (_cruises.Count() > 0)
            {
                // _Itinerary = WorkWithData.GetItinerary(_dgCode);
            }
            else
            {
                tcProblemChanges.TabPages.Remove(tcpItinerary);
            }
            //updateDataGrid();
            updateDataGridPutevka();
            //проверка на новые сообщения
            if (WorkWithData.IsNewMessage(_dgCode))
            {
                tcpMessages.Text = "Необработанная переписка";
            }
            else
            {
                tcpMessages.Text = "Переписка";
            }
            //Обработка аннуляции по путевку
            if (WorkWithData.IsAnnulation(_dgCode))
            {
                
                DataTable annulTable = WorkWithData.GetAnnulationByDogovor(_dgCode);
                tcProblemChanges.TabPages.Remove(tcpMessages);
                foreach ( DataRow row in  annulTable.Rows)
                {
                    TabPage tcp = new TabPage()
                        {
                            Name = "tcAnnul"+ row.Field<int>("AN_KEY").ToString(),
                            Text = "Заявление "+ row.Field<string>("PT_name")
                        };
                    tcp.Controls.Add(new ucAnnulation(row.Field<int>("AN_KEY"))
                        {
                            Dock = DockStyle.Fill
                        });
                    tcProblemChanges.TabPages.Add(tcp);
                }
                tcProblemChanges.TabPages.Add(tcpMessages);
                

            }
            //Настройки путевки 
            tcpSettings.Controls.Add(new ucDogovorSetting(_dgCode)
                {
                    Location = new Point(0,0)
                });

        }

       
        private void updateDataGridPutevka()
        {
            dgvPutevka.AutoGenerateColumns = true;
            dgvPutevka.DataSource = _DogovorList;
            dgvPutevka.AutoGenerateColumns = false;
            foreach (DataGridViewColumn column in dgvPutevka.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "DL_TURDATE":
                        {
                            column.DisplayIndex = 1;
                            column.HeaderText = "Дата заезда";
                            column.Width = 80;
                            column.DefaultCellStyle.Format = "dd.MM.yy";
                        }
                        break;
                    case "DL_DAY":
                        {
                            column.DisplayIndex = 2;
                            column.HeaderText = "День";
                            column.Width = 50;
                        }
                        break;
                    case "DL_NDAYS":
                        {
                            column.DisplayIndex = 3;
                            column.Width = 50;
                            column.HeaderText = "н/дн";

                        }
                        break;
                    case "DL_NAME":
                        {

                            column.DisplayIndex = 4;
                            column.HeaderText = "Услуга";

                            column.Width = 400;
                        }
                        break;
                    case "DL_NMEN":
                        {
                            column.DisplayIndex = 5;
                            column.HeaderText = "Кол-во pax";
                            column.Width = 50;
                        }
                        break;
                    case "DL_COST":
                        {
                            column.DisplayIndex = 6;
                            column.HeaderText = "Нетто";
                            column.Width = 60;
                            column.DefaultCellStyle.Format = "#0";
                            if (!(_access.isBronir || _access.isSuperViser))
                            {
                                column.Visible = false;
                            }
                        }
                        break;
                    case "DL_BRUTTO":
                        {
                            column.DisplayIndex = 7;
                            column.HeaderText = "Брутто";
                            column.DefaultCellStyle.Format = "#0";
                            column.Width = 60;
                        }
                        break;
                    case "CR_NAME":
                        {
                            column.DisplayIndex = 8;
                            column.HeaderText = "Состояние";
                            column.Width = 110;
                        }
                        break;
                    default:
                        column.Visible = false;
                        break;
                }

            }
            dgvPutevka.Refresh();
        }

        private void frmNewOptions_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            this.tlpDataOfBronCruise.Width = this.panel1.ClientSize.Width;
            // this.tlpCruiseBl1.Width = this.panel2.ClientSize.Width-3;
            this.WindowState = FormWindowState.Maximized;
            this.tcOptions.SelectedIndex = 0;

            switch (_type)
            {
                case 1:
                     // btnMessages_Click(null, null);
                     tcPartnerPutevka.SelectTab(tcpPutevka);
                     tcProblemChanges.SelectTab(tcpMessages);
                     break;
                case 2:
                     // btnMessages_Click(null, null);
                     tcPartnerPutevka.SelectTab(tcpPutevka);
                    try
                    {
                        tcProblemChanges.SelectTab(tcpChanges);
                    }
                    catch (Exception)
                    {
                        
                        
                    }
                    break;
                case 3:
                    // btnMessages_Click(null, null);
                    tcPartnerPutevka.SelectTab(tcpPutevka);
                    try
                    {
                        tcProblemChanges.SelectTab(tcpProblem);
                    }
                    catch (Exception)
                    {


                    }
                     break;
                case 4:
                     // btnMessages_Click(null, null);
                     tcPartnerPutevka.SelectTab(tcpPutevka);
                     try
                     {
                         tcProblemChanges.SelectTab(tcpItinerary);
                     }
                     catch (Exception)
                     {


                     }
                    break;
                case 5:
                        tcPartnerPutevka.SelectTab(tcpPutevka);
                        try
                        {
                            TabPage tp = tcProblemChanges.TabPages[tcProblemChanges.TabCount - 2];
                            tcProblemChanges.SelectTab(tp);

                        }

                        catch (Exception)
                        {


                        }
                        break;
                default:
                    break;
            }

            this.Refresh();
        }

        private const string selAction = @"SELECT distinct [DL_key]
      ,[actions_id]
      ,[Text]
      ,[isBonus]
  FROM [dbo].[mk_actions_options]
  where actions_id >0 and  DL_key =@dlkey and  isBonus= @bon";

        private void generateBlock1ToCruise(int dlKey, bool showWeb = true)
        {



            tlpCruiseBl1.RowStyles.Clear();
            tlpCruiseBl1.Height = 15;
            tlpCruiseBl1.Controls.Clear();
            tlpCruiseBl1.RowCount = 1;
            tlpCruiseBl1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            GetBonusAndService(dlKey);
            DataTable _turists = new DataTable(),
                      _servises = new DataTable(),
                      _servisescrin = new DataTable(),
                      _cruiseinfo = new DataTable();
            int nmen;
            SqlCommand selcruiseinfo =
                new SqlCommand(@"select * from mk_dogovorlistadd where tbl_dogovor_list_key=@dlKey",
                               WorkWithData.Connection);
            selcruiseinfo.Parameters.AddWithValue("@dlKey", dlKey);
            SqlDataAdapter cruiseinfoadapter = new SqlDataAdapter(selcruiseinfo);
            DateTime turdate, enddate;
            int _ndays = 0;
            string dg_code;
            using (SqlCommand com = new SqlCommand(@"SELECT 
          [DL_DGCOD]
         ,[DL_TURDATE]
         ,[DL_KEY]
         ,[DL_DAY]
         ,[DL_NDAYS]
         ,[DL_NMEN]
        FROM [dbo].[tbl_DogovorList]
        where  DL_KEY = @dlkey", WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
                turdate =
                    dt.Rows[0].Field<DateTime>("DL_TURDATE").AddDays(dt.Rows[0].Field<Int16>("DL_DAY") - 1);
                nmen = dt.Rows[0].Field<Int16>("DL_NMEN");
                _ndays = dt.Rows[0].Field<Int16>("DL_NDAYS");
                enddate = turdate.AddDays(_ndays);
                dg_code = dt.Rows[0].Field<string>("DL_DGCOD");


            }
            cruiseinfoadapter.Fill(_cruiseinfo);
            DataTable _servisesmas = new DataTable();
            _servisesmas.Clear();


            using (SqlCommand com = new SqlCommand("MK_lk_servises_putevka", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", dg_code);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(_servisesmas);

            }


            if (_cruiseinfo.Rows.Count < 1) return;
            string ship = string.Empty, crline = string.Empty, brandCode = string.Empty;
            if (_cruiseinfo.Rows[0].Field<string>("brandcode") != null)
            {
                using (
                    SqlCommand com = new SqlCommand(@"select name_en,mnemo from  CruiseLines where mnemo = @crline ",
                                                    WorkWithData.ConnectionTS))
                {
                    com.Parameters.AddWithValue("@crline", _cruiseinfo.Rows[0].Field<string>("brandcode"));
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    crline = dt.Rows[0].Field<string>("name_en");
                    brandCode = dt.Rows[0].Field<string>("mnemo");
                }
            }
            int id_ship = 0;
            if (_access.isBronir || _access.isSuperViser)
            {
                DataTable _SystemDate = new DataTable();
                SqlCommand systemCommand =
                    new SqlCommand(@"select * from dbo.CruiseLines_Sys where brandCode = @crl order by parent ",
                                   WorkWithData.ConnectionTS);
                systemCommand.Parameters.AddWithValue("@crl", brandCode);
                SqlDataAdapter sysadapter = new SqlDataAdapter(systemCommand);
                sysadapter.Fill(_SystemDate);

                foreach (DataRow row in _SystemDate.Rows)
                {
                    if (showWeb)
                    {
                        if ((row.Field<string>("Parametr_name") == "URL"))
                        {
                            if (!string.IsNullOrEmpty(row.Field<string>("Parametr_value").Trim()))
                            {
                                //abBook.Text = row.Field<string>("Parametr_value").Trim();

                                string adress = row.Field<string>("Parametr_value").Trim();
                                tbAdress.Text = adress;
                                wbBook.Source = new Uri(adress);
                            }
                            continue;
                        }
                    }
                    //wbBook.LoadHTML()
                    //tbAdress.Text = row.Field<string>("Parametr_value").Trim();
                    // wbBook.Navigate(row.Field<string>("Parametr_value").Trim());


                    controlCruiseAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel(row.Field<string>("Parametr_name") + ":"),
                                    GetTextBox(row.Field<string>("Parametr_value"))
                                }.ToArray()));
                }
            }
            if (_cruiseinfo.Rows[0].Field<int?>("cl_id") != null &&
                _cruiseinfo.Rows[0].Field<string>("shipcode") != null)
            {
                using (
                    SqlCommand com =
                        new SqlCommand(
                            @"select id,name_en from  Ships where code = @shipcode and cruise_line_id = @crline ",
                            WorkWithData.ConnectionTS))
                {
                    com.Parameters.AddWithValue("@crline", _cruiseinfo.Rows[0].Field<int>("cl_id"));
                    com.Parameters.AddWithValue("@shipcode", _cruiseinfo.Rows[0].Field<string>("shipcode"));
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    ship = dt.Rows[0].Field<string>("name_en");
                    id_ship = dt.Rows[0].Field<int>("id");
                }
            }
            string cabinNomber = string.Empty,
                   cabinCategory = string.Empty,
                   optionNumer = string.Empty,
                   cabinDef = string.Empty;
            DateTime? optionDate = null;
            bool isBook = false;
            bool quryDoc = false, getDoc = false;
            using (SqlCommand com = new SqlCommand(@"SELECT top 1
		[OP_ID]
        ,[OP_DLKEY]
        ,[OP_Descript]
        ,[OP_number]
        ,[OP_N_cabin]
        ,[OP_date_end]
        ,[OP_WHO]
        ,[OP_LastUpdate]
        ,[OP_category]
        ,[OP_IsBook]
        ,isnull(OP_DOCUMENT_QUERY,0) as OP_DOCUMENT_QUERY
        ,isnull(OP_DOCUMENT_GET,0) as OP_DOCUMENT_GET
        ,[OP_LEVEL_CABIN]
        FROM [dbo].[mk_options] where  OP_DLKEY = @dlkey
        order by OP_ID desc", WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                DataTable dt = new DataTable();

                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cabinNomber = dt.Rows[0].Field<string>("OP_N_cabin");
                    cabinCategory = dt.Rows[0].Field<string>("OP_category");
                    optionNumer = dt.Rows[0].Field<string>("OP_number");
                    optionDate = dt.Rows[0].Field<DateTime?>("OP_date_end");
                    isBook = dt.Rows[0].Field<bool>("OP_IsBook");
                    cabinDef = dt.Rows[0].Field<string>("OP_LEVEL_CABIN");
                    quryDoc = dt.Rows[0].Field<bool>("OP_DOCUMENT_QUERY");
                    getDoc = dt.Rows[0].Field<bool>("OP_DOCUMENT_GET");

                }
            }
            string cabinClass = string.Empty;
            if (cabinCategory != string.Empty && cabinCategory != null)
            {
                using (SqlCommand com = new SqlCommand(@"SELECT  name
             FROM [dbo].[CabinCategories]
            inner join dbo.CabinClasses as cc on class_id = cc.id where ship_id = @id and code =@code",
                                                       WorkWithData.ConnectionTS))
                {
                    com.Parameters.AddWithValue("@id", id_ship);
                    com.Parameters.AddWithValue("@code", cabinCategory);
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cabinClass = dt.Rows[0].Field<string>("name");
                    }
                }
            }
            spliterCruiseAdd();
            //controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("Круизная компания:"), GetTextBox(crline) }.ToArray()));
            controlCruiseAdd(
                GetTablePanel(
                    new List<object>
                        {
                            GetLabel("Лайнер:"),
                            GetTextBox(ship),
                            GetLabel("код лайнера:"),
                            GetTextBox(_cruiseinfo.Rows[0].Field<string>("shipcode"))
                        }.ToArray()));
            controlCruiseAdd(
                GetTablePanel(
                    new List<object> {GetLabel("Дата круиза:"), GetTextBox(turdate.ToString("dd.MM.yyyy"))}.ToArray()));
            controlCruiseAdd(
                GetTablePanel(
                    new List<object> {GetLabel("Дата окончания круиза:"), GetTextBox(enddate.ToString("dd.MM.yyyy"))}
                        .ToArray()));
            controlCruiseAdd(
                GetTablePanel(
                    new List<object>
                        {
                            GetLabel("Продолжительность круиза:"),
                            GetTextBox(_ndays.ToString()),
                            GetLabel("ночей")
                        }.ToArray()));
            controlCruiseAdd(
                GetTablePanel(
                    new List<object>
                        {
                            GetLabel("Категория каюты:"),
                            GetTextBox(cabinCategory),
                            GetLabel("с размещением"),
                            GetTextBox(nmen.ToString()),
                            GetLabel("чел.")
                        }.ToArray()));
            controlCruiseAdd(GetTablePanel(new List<object> {GetLabel("Класс каюты:"), GetTextBox(cabinClass)}.ToArray()));


            if (optionNumer != null && optionNumer != string.Empty)
            {
                controlCruiseAdd(
                    GetTablePanel(new List<object> {GetLabel("№ каюты:"), GetTextBox(cabinNomber)}.ToArray()));
                controlCruiseAdd(
                    GetTablePanel(new List<object> {GetLabel("Номер опции:"), GetTextBox(optionNumer)}.ToArray()));
                controlCruiseAdd(
                    GetTablePanel(new List<object> {GetLabel("Уровень дефицита кают:"), GetTextBox(cabinDef)}.ToArray()));
                if (!isBook)
                {
                    controlCruiseAdd(
                        GetTablePanel(
                            new List<object> {GetLabel("Опция до:"), GetTextBox(optionDate.ToString())}.ToArray()));
                }
                else
                {
                    controlCruiseAdd(
                        GetTablePanel(
                            new List<object> {GetLabel("Опция до:"), GetTextBox("Опция подтверждена")}.ToArray()));
                }
                if (getDoc)
                {
                    controlCruiseAdd(GetLabel("Документы получены"));
                }
                else
                {
                    if (quryDoc)
                    {
                        controlCruiseAdd(GetLabel("Документы запрошены"));
                    }
                }
            }
            float totalSum = 0;
            foreach (DataRow row in _servisesmas.Rows)
            {
                if (row.Field<int>("tipe") == 1)
                {
                    if (row.Field<int>("order") == 1)
                    {
                        controlCruiseAdd(
                            GetTablePanel(
                                new List<object>
                                    {
                                        GetLabel("Круизный тариф:"),
                                        GetTextBox(row.Field<float?>("DL_BRUTTO").ToString())
                                    }.ToArray()));
                        totalSum += row.Field<float?>("DL_BRUTTO") == null ? 0 : row.Field<float?>("DL_BRUTTO").Value;
                    }
                    else
                    {
                        controlCruiseAdd(
                            GetTablePanel(
                                new List<object>
                                    {
                                        GetLabel(row.Field<string>("DL_NAME") + ":"),
                                        GetTextBox(row.Field<float?>("DL_BRUTTO").ToString())
                                    }.ToArray()));
                        totalSum += row.Field<float?>("DL_BRUTTO") == null ? 0 : row.Field<float?>("DL_BRUTTO").Value;
                    }
                }
            }
            controlCruiseAdd(
                GetTablePanel(new List<object> {GetLabel("Общая сумма:"), GetTextBox(totalSum.ToString())}.ToArray()));
            SqlCommand servisecrinCommand = new SqlCommand(@"SELECT distinct [DL_key]
      ,[actions_id]
      ,[Text]
      ,CDP_NAME
      ,CDP_ORDER
      ,CDP_BLOCK
  FROM [dbo].[mk_actions_options] 
  inner join dbo.MK_Cruise_DOPServise on actions_id=CDS_ID
  where actions_id <0 and
  DL_key = @dlkey and
  CDP_BLOCK = 1
  order by CDP_ORDER  ", WorkWithData.Connection);
            servisecrinCommand.Parameters.AddWithValue("@dlkey", dlKey);
            SqlDataAdapter serviccrineAdapter = new SqlDataAdapter(servisecrinCommand);
            serviccrineAdapter.Fill(_servisescrin);
            foreach (DataRow row in _servisescrin.Rows)
            {
                controlCruiseAdd(
                    GetTablePanel(
                        new List<object>
                            {
                                GetLabel(row.Field<string>("CDP_NAME") + ":"),
                                GetTextBox(row.Field<string>("Text"))
                            }.ToArray()));
            }

            //для изменениея цены круиза
            tlpCruisePrice.Controls.Clear();
            tlpCruisePrice.RowStyles.Clear();
            tlpCruisePrice.Height = 1;

            foreach (DataRow row in _servisesmas.Rows)
            {
                if (row.Field<int>("tipe") == 1)
                {
                    if (row.Field<int>("order") == 1)
                    {
                        tlpCruisePrice.RowCount++;
                        tlpCruisePrice.Height += 27;
                        tlpCruisePrice.RowStyles.Insert(0,
                                                        new System.Windows.Forms.RowStyle(
                                                            System.Windows.Forms.SizeType.Absolute, 27F));
                        tlpCruisePrice.Controls.Add(
                            GetTablePanel(
                                new List<object>
                                    {
                                        GetLabel("Круизный тариф:"),
                                        GetTextBox(row.Field<float?>("DL_BRUTTO").ToString())
                                    }.ToArray()));
                        //controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("Круизный тариф:"), GetTextBox(row.Field<float>("DL_BRUTTO").ToString()) }.ToArray()));
                        //totalSum += row.Field<float>("DL_BRUTTO");
                    }
                    else
                    {
                        tlpCruisePrice.RowCount++;
                        tlpCruisePrice.Height += 27;
                        tlpCruisePrice.RowStyles.Insert(0,
                                                        new System.Windows.Forms.RowStyle(
                                                            System.Windows.Forms.SizeType.Absolute, 27F));
                        tlpCruisePrice.Controls.Add(
                            GetTablePanel(
                                new List<object>
                                    {
                                        GetLabel(row.Field<string>("DL_NAME") + ":"),
                                        GetTextBox(row.Field<float?>("DL_BRUTTO").ToString())
                                    }.ToArray()));
                        //controlCruiseAdd(GetTablePanel(new List<object> { GetLabel(row.Field<string>("DL_NAME") + ":"), GetTextBox(row.Field<float>("DL_BRUTTO").ToString()) }.ToArray()));
                        //totalSum += row.Field<float>("DL_BRUTTO");
                    }
                }
            }





            //Акции

            using (SqlCommand com = new SqlCommand(selAction, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                com.Parameters.AddWithValue("@bon", false);
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    spliterCruiseAdd();
                    controlCruiseAdd(GetLabel("Акции"));
                    int ii = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        controlCruiseAdd(GetLabel(ii.ToString() + "." + row.Field<string>("Text").Replace("\\", "")));
                        ii++;
                    }
                }
            }
            //Бонусы

            using (SqlCommand com = new SqlCommand(selAction, WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                com.Parameters.AddWithValue("@bon", true);
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    spliterCruiseAdd();
                    controlCruiseAdd(GetLabel("Бонусы"));
                    int ii = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        controlCruiseAdd(GetLabel(ii.ToString() + "." + row.Field<string>("Text").Replace("\\", "")));
                        ii++;
                    }
                }


            }

            //Загрузка сервисов

            SqlCommand serviseCommand = new SqlCommand(@"SELECT distinct [DL_key]
            ,[actions_id]
            ,[Text]
            ,CDP_NAME
            ,CDP_ORDER
            ,CDP_BLOCK
            FROM [dbo].[mk_actions_options] 
            inner join dbo.MK_Cruise_DOPServise on actions_id=CDS_ID
            where actions_id <0 and
            DL_key = @dlkey and
            CDP_BLOCK = 2 
            order by CDP_ORDER  ", WorkWithData.Connection);
            serviseCommand.Parameters.AddWithValue("@dlkey", dlKey);
            SqlDataAdapter serviceAdapter = new SqlDataAdapter(serviseCommand);

            serviceAdapter.Fill(_servises);
            if (_servises.Rows.Count > 0)
            {
                spliterCruiseAdd();
                controlCruiseAdd(GetLabel("Сервис"));
                foreach (DataRow row in _servises.Rows)
                {
                    controlCruiseAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel(row.Field<string>("CDP_NAME") + ":"),
                                    GetTextBox(row.Field<string>("Text"))
                                }.ToArray()));

                }
            }





            //Загрузка туристов
            selctedTurists(dlKey);

            //            SqlCommand selTurist = new SqlCommand(@"SELECT 
            //	        TU_NAMELAT
            //	        ,TU_FNAMELAT
            //	        ,TU_BIRTHDAY
            //	        ,TU_BIRTHCITY
            //            ,TU_BIRTHCOUNTRY
            //            ,TU_PASPORTTYPE
            //            ,TU_PASPORTNUM
            //            ,TU_PASPORTDATE
            //            ,TU_PASPORTDATEEND
            //            ,TU_PASPORTBYWHOM
            //            ,TU_CITIZEN
            //            FROM [dbo].[TuristService] 
            //            inner join dbo.tbl_Turist on TU_KEY = tu_tukey
            //            where tu_dlkey=@dlkey", WorkWithData.Connection);
            //            selTurist.Parameters.AddWithValue("@dlkey", dlKey);
            //            SqlDataAdapter turistadapter = new SqlDataAdapter(selTurist);
            //            turistadapter.Fill(_turists);


            //            int i = 0;
            //            foreach (DataRow row in _turists.Rows)
            //            {
            //                ++i;
            //                spliterCruiseAdd();
            //                controlCruiseAdd(GetLabel("Турист " + i.ToString()));
            //                controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("Фамилия:"), GetTextBox(row.Field<string>("TU_NAMELAT")) }.ToArray()));
            //                controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("Имя:"), GetTextBox(row.Field<string>("TU_FNAMELAT")) }.ToArray()));
            //                if (row.Field<DateTime?>("TU_BIRTHDAY") != null)
            //                {
            //                    DateTime birrthday = row.Field<DateTime>("TU_BIRTHDAY");
            //                    controlCruiseAdd(
            //                        GetTablePanel(
            //                            new List<object>
            //                                {
            //                                    GetLabel("Дата:"),
            //                                    GetTextBox(birrthday.Date.ToString("dd.MM")),
            //                                    GetLabel("год рождения:"),
            //                                    GetTextBox(birrthday.Date.Year.ToString())
            //                                }.ToArray()));
            //                }
            //                if ((row.Field<string>("TU_CITIZEN") != string.Empty) && (row.Field<string>("TU_CITIZEN") != null))
            //                {
            //                    controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("Национальность:"), GetTextBox(row.Field<string>("TU_CITIZEN")) }.ToArray()));
            //                }
            //                if ((row.Field<string>("TU_BIRTHCITY") != string.Empty) && (row.Field<string>("TU_BIRTHCITY") != null))
            //                {
            //                    controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("Mесто рождения:"), GetTextBox(row.Field<string>("TU_BIRTHCITY") + "(" + row.Field<string>("TU_BIRTHCOUNTRY") + ")") }.ToArray()));
            //                }

            //                controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("Загранпаспорт:"), GetTextBox(row.Field<string>("TU_PASPORTTYPE")), GetLabel("№"), GetTextBox(row.Field<string>("TU_PASPORTNUM")) }.ToArray()));
            //                if (row.Field<DateTime?>("TU_PASPORTDATE") != null)
            //                {
            //                    controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("дата выдачи з\\паспорта:"), GetTextBox(row.Field<DateTime>("TU_PASPORTDATE").Date.ToString("dd.MM.yyyy")) }.ToArray()));
            //                }
            //                else
            //                {
            //                    controlCruiseAdd(GetLabel("дата выдачи з\\паспорта:"));
            //                }

            //                if (row.Field<DateTime?>("TU_PASPORTDATEEND") != null)
            //                {
            //                    controlCruiseAdd(GetTablePanel(new List<object> { GetLabel("дата окончания действия паспорта:"), GetTextBox(row.Field<DateTime>("TU_PASPORTDATEEND").Date.ToString("dd.MM.yyyy")) }.ToArray()));
            //                }
            //                else
            //                {
            //                    controlCruiseAdd(GetLabel("дата окончания действия паспорта:"));
            //                }

            //            }




            //tlpCruiseBl1.Controls.Add(rtbInfo);
        }

        #region ControlAdd

        private void controlBonusCruiseAdd(Control con)
        {
            tlpBonusCruise.RowCount++;
            tlpBonusCruise.Height += 27;
            tlpBonusCruise.RowStyles.Insert(0,
                                            new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,
                                                                              27F));
            tlpBonusCruise.Controls.Add(con);
        }

        private void spliterBonusCruiseAdd()
        {
            //tlpBonusCruise.RowCount++;
            //tlpBonusCruise.Height += 27;
            //tlpBonusCruise.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            //tlpBonusCruise.Controls.Add(GetLabel(""));
        }

        private void controlInshurAdd(Control con)
        {
            tlpInshur.RowCount++;
            tlpInshur.Height += 27;
            tlpInshur.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            tlpInshur.Controls.Add(con);
        }

        private void spliterInshurAdd()
        {
            //tlpInshur.RowCount++;
            //tlpInshur.Height += 27;
            //tlpInshur.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            //tlpInshur.Controls.Add(GetLabel(""));
        }


        private void controlCruiseAdd(Control con)
        {
            tlpCruiseBl1.RowCount++;
            tlpCruiseBl1.Height += 27;
            tlpCruiseBl1.RowStyles.Insert(0,
                                          new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            tlpCruiseBl1.Controls.Add(con);
        }

        private void spliterCruiseAdd()
        {
            //tlpCruiseBl1.RowCount++;
            //tlpCruiseBl1.Height += 27;
            //tlpCruiseBl1.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            //tlpCruiseBl1.Controls.Add(GetLabel(""));
        }

        private void controlPaketAdd(Control con)
        {
            tlpDopPaket.RowCount++;
            tlpDopPaket.Height += 27;
            tlpDopPaket.RowStyles.Insert(0,
                                         new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            tlpDopPaket.Controls.Add(con);
        }

        private void spliterPaketAdd()
        {
            //tlpDopPaket.RowCount++;
            //tlpDopPaket.Height += 27;
            //tlpDopPaket.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            //tlpDopPaket.Controls.Add(GetLabel(""));
        }

        private void controlOtherAdd(Control con)
        {
            tlpOther.RowCount++;
            tlpOther.Height += 27;
            tlpOther.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            tlpOther.Controls.Add(con);
        }

        private void spliterOtherAdd()
        {
            //tlpOther.RowCount++;
            //tlpOther.Height += 27;
            //tlpOther.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            //tlpOther.Controls.Add(GetLabel(""));
        }

        private void controlHotelAdd(Control con)
        {
            tlpHotels.RowCount++;
            tlpHotels.Height += 27;
            tlpHotels.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            tlpHotels.Controls.Add(con);
        }

        private void spliterHotelAdd()
        {
            //tlpHotels.RowCount++;
            //tlpHotels.Height += 27;
            //tlpHotels.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            //tlpHotels.Controls.Add(GetLabel(""));
        }

        #endregion

        #region NewDinamicControl

        private CheckBox GetChekBox(int idBonus, bool state)
        {
            CheckBox cb = new CheckBox();
            cb.Tag = idBonus;
            cb.Checked = state;

            cb.Width = 10;
            cb.UseVisualStyleBackColor = true;
            cb.Text = "";
            return cb;
        }


        private Label GetLabel(string caption)
        {
            Label curLab = new Label();
            curLab.AutoSize = true;
            curLab.MaximumSize = new Size(panel2.ClientSize.Width - 5, 27);
            curLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold,
                                                  System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            curLab.Location = new System.Drawing.Point(3, 0);
            curLab.Width = (caption.Length + 1)*9;
            if (curLab.Width > panel2.ClientSize.Width)
            {
                curLab.Width = panel2.ClientSize.Width;
            }
            curLab.Size = new System.Drawing.Size(88, 15);
            curLab.TabIndex = 0;
            if (caption == null)
            {
                return null;
            }
            curLab.Text = caption.Trim();
            return curLab;
        }

        private TextBox GetTextBox(string caption, int id)
        {
            TextBox tb = GetTextBox(caption);
            tb.ReadOnly = false;
            tb.Tag = id;
            tb.TextChanged += tb_TextChanged;
            return tb;

        }

        private Button GetButtonRus(string caption, int id)
        {
            Button btn = new Button();
            btn.Text = caption;
            btn.Tag = id;
            btn.BackColor = Color.DarkGray;
            btn.AutoSize = true;
            btn.Height = 27;
            //btn.Dock = DockStyle.Fill;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            // btn.Enabled = false;
            btn.Click += btn_ClickRus;
            return btn;
        }

        private Button GetButton(string caption, int id)
        {
            Button btn = new Button();
            btn.Text = caption;
            btn.Tag = id;
            btn.BackColor = Color.DarkGray;
            //btn.Dock = DockStyle.Fill;
            btn.AutoSize = true;
            btn.Height = 27;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            // btn.Enabled = false;
            btn.Click += btn_Click;
            return btn;
        }

        private void btn_ClickRus(object sender, EventArgs e)
        {

            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = false;

            Button btn = sender as Button;
            if (btn != null)
            {
                frmQuestionnaire questionnaire = new frmQuestionnaire(Convert.ToInt32(btn.Tag), 0);
                questionnaire.Show();
            }
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = true;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = false;
            Button btn = sender as Button;
            if (btn != null)
            {
                frmQuestionnaire questionnaire = new frmQuestionnaire(Convert.ToInt32(btn.Tag), 1);
                questionnaire.Show();
            }
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = true;
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.ForeColor = Color.Crimson;
        }

        private TextBox GetTextBox(string caption)
        {
            TextBox tb = new TextBox();

            //tb.AutoSize = true;
            tb.WordWrap = true;
            tb.Dock = System.Windows.Forms.DockStyle.Fill;
            tb.TabIndex = 1;
            tb.ReadOnly = true;
            //tb.BorderStyle = BorderStyle.None;
            tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular,
                                              System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            if (caption == null)
            {
                return null;
            }
            tb.MaxLength = caption.Length + 1;
            tb.Width = (int) ((caption.Length + 1)*8);
            tb.Text = caption.Trim();
            return tb;

        }

        private TableLayoutPanel GetTablePanel(object[] childs)
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.ColumnCount = childs.Length;
            //tlp.MaximumSize = new Size(panel2.ClientSize.Width-3,40);
            int i = 0;
            foreach (object child in childs)
            {

                Control cnt = child as Control;
                if (cnt != null)
                {
                    tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
                    tlp.Controls.Add(cnt, i, 0);
                    i++;
                }

            }
            tlp.Dock = DockStyle.Fill;
            tlp.RowCount = 1;
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));


            return tlp;
        }

        #endregion

        private void tcOptions_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Name)
            {
                case "tcpPutevka":
                    tlpWebBrowser.Visible = false;
                    tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                    tableLayoutPanel1.ColumnStyles[0].Width = 50;
                    tableLayoutPanel1.ColumnStyles[1].Width = 50;
                    break;
                default:
                    if (new AccessClass(WorkWithData.Connection).isBronir ||
                        new AccessClass(WorkWithData.Connection).isSuperViser)
                    {
                        tlpWebBrowser.Visible = true;
                        tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                        tableLayoutPanel1.ColumnStyles[0].Width = 416;
                        tableLayoutPanel1.ColumnStyles[1].Width = 100;
                    }
                    else
                    {

                    }
                    break;
            }
            switch (e.TabPage.Name)
            {
                case "tcpCruise":
                    generateBlock1ToCruise(dogovorlistline.Parse(cbCruises.SelectedItem).dlkey);
                    break;
                case "tcpHotel":
                    generateBlok1ForHotel(dogovorlistline.Parse(cbHotels.SelectedItem).dlkey);
                    break;
            }
            //foreach (var control in e.TabPage.Controls)
            //{
            //    TableLayoutPanel tlp = control as TableLayoutPanel;
            //    if (tlp != null)
            //    {
            //        foreach (var control1 in tlp.Controls)
            //        {
            //            ComboBox cb = control1 as ComboBox;
            //            if (cb != null)
            //            {
            //                cb.SelectedIndex = 0;
            //            }
            //        }
            //    }
            //}


        }

        private void correctTextBolt()
        {
            string[] codesWord = new string[]
                {"Турист", "Сервис", "1.Создание брони в круиз.компаниях", "Бонусы", "Акции"};
            for (int i = 0; i < rtbInfo.Lines.Count(); i++)
            {

                foreach (string s in codesWord)
                {
                    if (rtbInfo.Lines[i].IndexOf(s) >= 0)
                    {
                        int n = rtbInfo.Text.IndexOf(rtbInfo.Lines[i]), n1 = rtbInfo.Lines[i].Length;
                        rtbInfo.Select(n, n1);
                        //string str = rtbInfo.SelectedText;
                        //MessageBox.Show(string.Format("c {0} po {1} " +str,n,n1));
                        rtbInfo.SelectionFont = new Font(rtbInfo.Font, System.Drawing.FontStyle.Bold);
                    }
                }
                if (rtbInfo.Lines[i].IndexOf(":") >= 0)
                {
                    int n = rtbInfo.Text.IndexOf(rtbInfo.Lines[i]), n1 = rtbInfo.Lines[i].IndexOf(":");
                    rtbInfo.Select(n, n1);
                    rtbInfo.SelectionFont = new Font(rtbInfo.Font, System.Drawing.FontStyle.Bold);
                }
            }
        }

        private void cbCruises_SelectedIndexChanged(object sender, EventArgs e)
        {
            dogovorlistline line = cbCruises.SelectedItem as dogovorlistline;
            if (line != null)
            {
                //rtbInfo.Text = WorkWithData.GenerateBlock1ForCruise(line.dlkey);
                generateBlock1ToCruise(line.dlkey);
                //foreach (string s in rtbInfo.Lines)
                //{
                //    if (s.IndexOf("URL :") >= 0)
                //    {
                //        string url = s.Substring(s.IndexOf("URL :") + "URL :".Length);
                //        wbForBook.Navigate(url.Trim());
                //        tbAdress.Text = url.Trim();
                //        break;
                //    }
                //}
                mtbTime.Text = "00:00";
                dtpOption.Value = DateTime.Now.Date;
                cbCabinDef.SelectedIndex = 0;
                //  correctTextBolt();


                string category = string.Empty, cabinNom = string.Empty, optionNumber = string.Empty;
                string cabinSel = @"SELECT top 1
		                            [OP_ID]
                                    ,[OP_DLKEY]
                                    ,[OP_Descript]
                                    ,[OP_number]
                                    ,[OP_N_cabin]
                                    ,[OP_date_end]
                                    ,[OP_WHO]
                                    ,[OP_LastUpdate]
                                    ,[OP_category]
                                    ,[OP_IsBook]
                                    ,isnull(OP_DOCUMENT_QUERY,0) as OP_DOCUMENT_QUERY
                                    ,isnull(OP_DOCUMENT_GET,0) as OP_DOCUMENT_GET
                                    ,isnull([OP_LEVEL_CABIN],'') as OP_LEVEL_CABIN
                                    FROM [dbo].[mk_options] where  OP_DLKEY = @dlkey 
                                    --and OP_category is not null and OP_category <>''
                                    order by OP_ID desc";
                using (SqlCommand com1 = new SqlCommand(cabinSel, WorkWithData.Connection))
                {
                    com1.Parameters.AddWithValue("@dlkey", line.dlkey);
                    SqlDataAdapter ad = new SqlDataAdapter(com1);
                    DataTable dt1 = new DataTable();
                    ad.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        category = dt1.Rows[0].Field<string>("OP_category");
                        cabinNom = dt1.Rows[0].Field<string>("OP_N_cabin");
                        optionNumber = dt1.Rows[0].Field<string>("OP_number");
                        if (dt1.Rows[0].Field<DateTime?>("OP_date_end") != null)
                        {
                            dtpOption.Value = dt1.Rows[0].Field<DateTime>("OP_date_end");
                            cbIsBook.Checked = dt1.Rows[0].Field<bool>("OP_IsBook");
                            cbDocumentGet.Checked = dt1.Rows[0].Field<bool>("OP_DOCUMENT_GET");
                            cbDocumentQuery.Checked = dt1.Rows[0].Field<bool>("OP_DOCUMENT_QUERY");
                            mtbTime.Text = dt1.Rows[0].Field<DateTime>("OP_date_end").ToString("HH:mm");
                            if (dt1.Rows[0].Field<string>("OP_LEVEL_CABIN") != "")
                            {
                                cbCabinDef.SelectedIndex =
                                    cbCabinDef.Items.IndexOf(dt1.Rows[0].Field<string>("OP_LEVEL_CABIN"));
                            }
                        }

                    }
                }
                _cabinCategori = category;
                tbNomberOptions.Text = optionNumber;
                tbCabinCategory.Text = category;
                tbCabinNomber.Text = cabinNom;

            }
            else
            {
                MessageBox.Show("В данной путевке не найдено подходящей услуги");
                Close();
            }
        }

        private void updateDataGrid()
        {
            //dgvServ.DataSource = _servisesmas;

            dgvItinerary.DataSource = _Itinerary;
            foreach (DataGridViewColumn column in dgvItinerary.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "activitydate":
                        {
                            column.DisplayIndex = 0;
                            column.HeaderText = "День";
                            column.DefaultCellStyle.Format = "dd.MM.yy";
                        }
                        break;
                    case "locname_ru":
                        {
                            column.DisplayIndex = 1;
                            column.HeaderText = "Порт";
                        }
                        break;
                    case "arrival":
                        {
                            column.DisplayIndex = 2;
                            column.HeaderText = "Прибытие";
                        }
                        break;
                    case "departure":
                        {
                            column.DisplayIndex = 3;
                            column.HeaderText = "Отправление";
                        }
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {

            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = false;
            if (
                MessageBox.Show("Вы уверены что хотите добавить опцию?", "Проверка!", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_cabinCategori != tbCabinCategory.Text)
                {
                    if (
                        MessageBox.Show("Категория каюты была изменена. Подтвердить?", "Изменение категории каюты!",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                    }
                }
                string sqlQuery = @"INSERT INTO [dbo].[mk_options]
                                         ([OP_DLKEY]
                                         ,[OP_Descript]
                                         ,[OP_number]
                                         ,[OP_N_cabin]
                                         ,[OP_date_end]
                                         ,[OP_WHO]
                                         ,[OP_LastUpdate]
                                         ,[OP_category]
                                         ,OP_LEVEL_CABIN
                                         ,OP_IsBook
                                         ,OP_DOCUMENT_QUERY
                                         ,OP_DOCUMENT_GET)
                             VALUES
                                         (@OP_DLKEY 
                                         ,@OP_Descript
                                         ,@OP_number
                                         ,@OP_N_cabin
                                         ,@OP_date_end
                                         ,(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())
                                         ,getdate()
                                         ,@OP_category
                                         ,@OP_LEVEL_CABIN
                                         ,@OP_IsBook
                                         ,@OP_DOCUMENT_QUERY
                                         ,@OP_DOCUMENT_GET)";
                SqlCommand com = new SqlCommand(sqlQuery, WorkWithData.Connection);
                dogovorlistline line = cbCruises.SelectedItem as dogovorlistline;
                com.Parameters.AddWithValue("@OP_DLKEY", line.dlkey);
                com.Parameters.AddWithValue("@OP_Descript", label5.Text + tbSpecCanc.Text);
                com.Parameters.AddWithValue("@OP_number", tbNomberOptions.Text);
                com.Parameters.AddWithValue("@OP_N_cabin", tbCabinNomber.Text);
                int mm = 0, hh = 0;
                try
                {
                    mm = Convert.ToInt32(mtbTime.Text.Split(':')[1]);
                    hh = Convert.ToInt32(mtbTime.Text.Split(':')[0]);
                }
                catch (Exception)
                {

                    if (!cbIsBook.Checked)
                    {
                        MessageBox.Show("Ошибка заполнения");
                        return;
                    }
                    else
                    {
                        mm = 0;
                        hh = 0;
                    }
                }


                DateTime dt = new DateTime(dtpOption.Value.Date.Year, dtpOption.Value.Date.Month,
                                           dtpOption.Value.Date.Day, hh, mm, 0);

                com.Parameters.AddWithValue("@OP_date_end", dt);
                com.Parameters.AddWithValue("@OP_LEVEL_CABIN", cbCabinDef.Text);
                com.Parameters.AddWithValue("@OP_category", tbCabinCategory.Text);
                com.Parameters.AddWithValue("@OP_IsBook", cbIsBook.Checked);
                com.Parameters.AddWithValue("@OP_DOCUMENT_QUERY", cbDocumentQuery.Checked);
                com.Parameters.AddWithValue("@OP_DOCUMENT_GET", cbDocumentGet.Checked);
                com.ExecuteNonQuery();

            }
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = true;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {


                wbBook.Source = new Uri(tbAdress.Text);


                //try
                //{
                //    while (_pageReady == false) // YEAH!!!!!! IT IS WORKING!!!!
                //    {
                //        System.Windows.Forms.Application.DoEvents();
                //    }
                //    //Thread.Sleep(WaitForWebsite); --- It works but....
                //    //while (web.ReadyState != WebBrowserReadyState.Complete) --- it gives an ERROR
                //    //    System.Windows.Forms.Application.DoEvents();
                //}
                //catch (Exception)
                //{
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            //wbBook.Navigate(tbAdress.Text);
        }

        private void tbAdress_Leave(object sender, EventArgs e)
        {
            wbBook.Source = new Uri(tbAdress.Text);
            // wbBook.
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbInfo.SelectedText);
        }

        private void cbIsBook_CheckedChanged(object sender, EventArgs e)
        {
            dtpOption.Enabled = mtbTime.Enabled = !cbIsBook.Checked;

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rtbInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            // tbAdress.Text = e.LinkText;
            // wbBook.Navigate(e.LinkText);

        }

        private void dgvServ_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //foreach (DataGridViewRow row in dgvServ.Rows)
            //{
            //    row.Selected = false;
            //}

            //if (e.Button == MouseButtons.Right)
            //    dgvServ.Rows[e.RowIndex].Selected = true;
        }

        private void tcmsiChangePrice_Click(object sender, EventArgs e)
        {
            //            if(dgvServ.SelectedRows.Count<0)return;
            //            double brutto, netto;
            //            int sv_key;
            //            List<int> svkeys = new List<int>{1059,1520,3143,3149,3175};
            //            int dl_key = Convert.ToInt32(dgvServ.SelectedRows[0].Cells["dl_key"].Value) ;
            //            Console.WriteLine(dl_key);
            //            string selectString = @"DECLARE @svkey int  
            //                              DECLARE @code  int  
            //                              DECLARE @subcode1 int  
            //                              DECLARE @subcode2  int  
            //                              DECLARE @trkey  int  
            //                              DECLARE @paketkey  int
            //                              DECLARE @date datetime  
            //                              select @paketkey=DL_PAKETKEY,
            //                              @trkey= DL_TRKEY,
            //                              @svkey= DL_SVKEY,
            //                              @code= DL_CODE,
            //                              @subcode1= DL_SUBCODE1,                                 
            //                              @date =DL_TURDATE + DL_DAY -1,
            //                              @subcode2=DL_SUBCODE2
            //                              from tbl_DogovorList where DL_KEY = @dlkey
            //                                select * from tbl_costs
            //                                WHERE CS_SVKEY = @svkey 
            //                                AND CS_CODE = @code 
            //                                AND CS_SUBCODE1 = @subcode1 
            //                                AND CS_SUBCODE2 =@subcode2 
            //                                AND CS_PKKEY =@paketkey 
            //                                AND @date BETWEEN isnull(CS_DATE,@date) AND isnull(CS_DATEEND,@date+1) ";
            //            using (SqlCommand com = new SqlCommand(selectString,WorkWithData.Connection))
            //            {
            //                com.Parameters.AddWithValue("@dlkey", dl_key);
            //                DataTable dt = new DataTable();
            //                SqlDataAdapter adapter = new SqlDataAdapter(com);
            //                adapter.Fill(dt);
            //                brutto = dt.Rows[0].Field<double>("cs_cost");
            //                netto = dt.Rows[0].Field<double>("cs_costnetto");
            //                sv_key = dt.Rows[0].Field<int>("cs_svkey");
            //            }
            //            if (svkeys.IndexOf(sv_key) >= 0)
            //            {
            //                frmPriceChange frmPricechange = new frmPriceChange(brutto,netto,dl_key,_dgCode);
            //                frmPricechange.ShowDialog();
            //                GetDate();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Вы не можете изменять цену на эту услугу!");
            //            }

        }

        private void dgvServ_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //foreach (DataGridViewRow row in dgvServ.Rows)
            //{
            //    row.Selected = false;
            //}
            //if (e.RowIndex >= 0 && e.RowIndex< dgvServ.RowCount)
            //    dgvServ.Rows[e.RowIndex].Selected = true;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void cbDopPaket_SelectedIndexChanged(object sender, EventArgs e)
        {
            dogovorlistline line = cbDopPaket.SelectedItem as dogovorlistline;
            if (line != null)
                generateBlok1ForPaket(line.dlkey);
        }

        private void generateBlok1ForOther(int dlKey)
        {
            tlpOther.RowStyles.Clear();
            tlpOther.Height = 15;
            tlpOther.Controls.Clear();
            tlpOther.RowCount = 1;
            tlpOther.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            //Загрузка туристов
            selctedTurists(dlKey);
            DataTable _turists = new DataTable();
            SqlCommand selTurist = new SqlCommand(@"SELECT 
	        TU_NAMELAT
	        ,TU_FNAMELAT
	        ,TU_BIRTHDAY
	        ,TU_BIRTHCITY
            ,TU_BIRTHCOUNTRY
            ,TU_PASPORTTYPE
            ,TU_PASPORTNUM
            ,TU_PASPORTDATE
            ,TU_PASPORTDATEEND
            ,TU_PASPORTBYWHOM
            ,TU_CITIZEN
            FROM [dbo].[TuristService] 
            inner join dbo.tbl_Turist on TU_KEY = tu_tukey
            where tu_dlkey=@dlkey", WorkWithData.Connection);
            selTurist.Parameters.AddWithValue("@dlkey", dlKey);
            SqlDataAdapter turistadapter = new SqlDataAdapter(selTurist);
            turistadapter.Fill(_turists);

            int i = 0;
            foreach (DataRow row in _turists.Rows)
            {
                ++i;
                spliterOtherAdd();
                controlOtherAdd(GetLabel("Турист " + i.ToString()));
                controlOtherAdd(
                    GetTablePanel(
                        new List<object> {GetLabel("Фамилия:"), GetTextBox(row.Field<string>("TU_NAMELAT"))}.ToArray()));
                controlOtherAdd(
                    GetTablePanel(
                        new List<object> {GetLabel("Имя:"), GetTextBox(row.Field<string>("TU_FNAMELAT"))}.ToArray()));
                if (row.Field<DateTime?>("TU_BIRTHDAY") != null)
                {
                    DateTime birrthday = row.Field<DateTime>("TU_BIRTHDAY");

                    controlOtherAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("Дата:"),
                                    GetTextBox(birrthday.Date.ToString("dd.MM")),
                                    GetLabel("год рождения:"),
                                    GetTextBox(birrthday.Date.Year.ToString())
                                }.ToArray()));
                }
                if ((row.Field<string>("TU_CITIZEN") != string.Empty) && (row.Field<string>("TU_CITIZEN") != null))
                {
                    controlOtherAdd(
                        GetTablePanel(
                            new List<object> {GetLabel("Национальность:"), GetTextBox(row.Field<string>("TU_CITIZEN"))}
                                .ToArray()));
                }
                if ((row.Field<string>("TU_BIRTHCITY") != string.Empty) && (row.Field<string>("TU_BIRTHCITY") != null))
                {
                    controlOtherAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("Mесто рождения:"),
                                    GetTextBox(row.Field<string>("TU_BIRTHCITY") + "(" +
                                               row.Field<string>("TU_BIRTHCOUNTRY") + ")")
                                }.ToArray()));
                }

                controlOtherAdd(
                    GetTablePanel(
                        new List<object>
                            {
                                GetLabel("Загранпаспорт:"),
                                GetTextBox(row.Field<string>("TU_PASPORTTYPE")),
                                GetLabel("№"),
                                GetTextBox(row.Field<string>("TU_PASPORTNUM"))
                            }.ToArray()));
                if (row.Field<DateTime?>("TU_PASPORTDATE") != null)
                {
                    controlOtherAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("дата выдачи з\\паспорта:"),
                                    GetTextBox(row.Field<DateTime>("TU_PASPORTDATE").Date.ToString("dd.MM.yyyy"))
                                }.ToArray()));
                }
                else
                {
                    controlOtherAdd(GetLabel("дата выдачи з\\паспорта:"));
                }

                if (row.Field<DateTime?>("TU_PASPORTDATEEND") != null)
                {
                    controlOtherAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("дата окончания действия паспорта:"),
                                    GetTextBox(row.Field<DateTime>("TU_PASPORTDATEEND").Date.ToString("dd.MM.yyyy"))
                                }
                                .ToArray()));
                }
                else
                {
                    controlOtherAdd(GetLabel("дата окончания действия паспорта:"));
                }

            }
        }

        private void generateBlok1ForHotel(int dlKey)
        {
            tlpHotels.RowStyles.Clear();
            tlpHotels.Height = 15;
            tlpHotels.Controls.Clear();
            tlpHotels.RowCount = 1;
            tlpHotels.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            using (SqlCommand com = new SqlCommand(@"SELECT top 1
		[OP_ID]
        ,[OP_DLKEY]
        ,[OP_Descript]
        ,[OP_number]
        ,[OP_N_cabin]
        ,[OP_date_end]
        ,[OP_WHO]
        ,[OP_LastUpdate]
        ,[OP_category]
        ,[OP_IsBook]
        ,isnull(OP_DOCUMENT_QUERY,0) as OP_DOCUMENT_QUERY
        ,isnull(OP_DOCUMENT_GET,0) as OP_DOCUMENT_GET
        ,[OP_LEVEL_CABIN]
        FROM [dbo].[mk_options] where  OP_DLKEY = @dlkey
        order by OP_ID desc", WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dlkey", dlKey);
                DataTable dt = new DataTable();

                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cbHotelOk.Checked = dt.Rows[0].Field<bool>("OP_IsBook");

                }
            }
            //Загрузка туристов
            selctedTurists(dlKey);
            DataTable _turists = new DataTable();
            SqlCommand selTurist = new SqlCommand(@"SELECT 
	        TU_NAMELAT 
	        ,TU_FNAMELAT
	        ,TU_BIRTHDAY
	        ,TU_BIRTHCITY
            ,TU_BIRTHCOUNTRY
            ,TU_PASPORTTYPE
            ,TU_PASPORTNUM
            ,TU_PASPORTDATE
            ,TU_PASPORTDATEEND
            ,TU_PASPORTBYWHOM
            ,TU_CITIZEN
            FROM [dbo].[TuristService] 
            inner join dbo.tbl_Turist on TU_KEY = tu_tukey
            where tu_dlkey=@dlkey", WorkWithData.Connection);
            selTurist.Parameters.AddWithValue("@dlkey", dlKey);
            SqlDataAdapter turistadapter = new SqlDataAdapter(selTurist);
            turistadapter.Fill(_turists);

            int i = 0;
            foreach (DataRow row in _turists.Rows)
            {
                ++i;
                spliterHotelAdd();
                controlHotelAdd(GetLabel("Турист " + i.ToString()));
                controlHotelAdd(
                    GetTablePanel(
                        new List<object> {GetLabel("Фамилия:"), GetTextBox(row.Field<string>("TU_NAMELAT"))}.ToArray()));
                controlHotelAdd(
                    GetTablePanel(
                        new List<object> {GetLabel("Имя:"), GetTextBox(row.Field<string>("TU_FNAMELAT"))}.ToArray()));
                if (row.Field<DateTime?>("TU_BIRTHDAY") != null)
                {
                    DateTime birrthday = row.Field<DateTime>("TU_BIRTHDAY");

                    controlHotelAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("Дата:"),
                                    GetTextBox(birrthday.Date.ToString("dd.MM")),
                                    GetLabel("год рождения:"),
                                    GetTextBox(birrthday.Date.Year.ToString())
                                }.ToArray()));
                }
                if ((row.Field<string>("TU_CITIZEN") != string.Empty) && (row.Field<string>("TU_CITIZEN") != null))
                {
                    controlHotelAdd(
                        GetTablePanel(
                            new List<object> {GetLabel("Национальность:"), GetTextBox(row.Field<string>("TU_CITIZEN"))}
                                .ToArray()));
                }
                if ((row.Field<string>("TU_BIRTHCITY") != string.Empty) && (row.Field<string>("TU_BIRTHCITY") != null))
                {
                    controlHotelAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("Mесто рождения:"),
                                    GetTextBox(row.Field<string>("TU_BIRTHCITY") + "(" +
                                               row.Field<string>("TU_BIRTHCOUNTRY") + ")")
                                }.ToArray()));
                }

                controlHotelAdd(
                    GetTablePanel(
                        new List<object>
                            {
                                GetLabel("Загранпаспорт:"),
                                GetTextBox(row.Field<string>("TU_PASPORTTYPE")),
                                GetLabel("№"),
                                GetTextBox(row.Field<string>("TU_PASPORTNUM"))
                            }.ToArray()));
                if (row.Field<DateTime?>("TU_PASPORTDATE") != null)
                {
                    controlHotelAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("дата выдачи з\\паспорта:"),
                                    GetTextBox(row.Field<DateTime>("TU_PASPORTDATE").Date.ToString("dd.MM.yyyy"))
                                }.ToArray()));
                }
                else
                {
                    controlHotelAdd(GetLabel("дата выдачи з\\паспорта:"));
                }

                if (row.Field<DateTime?>("TU_PASPORTDATEEND") != null)
                {
                    controlHotelAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("дата окончания действия паспорта:"),
                                    GetTextBox(row.Field<DateTime>("TU_PASPORTDATEEND").Date.ToString("dd.MM.yyyy"))
                                }
                                .ToArray()));
                }
                else
                {
                    controlHotelAdd(GetLabel("дата окончания действия паспорта:"));
                }

            }
        }

        private void generateBlok1ForInshur(int dlKey)
        {
            tlpInshur.RowStyles.Clear();
            tlpInshur.Height = 15;
            tlpInshur.Controls.Clear();
            tlpInshur.RowCount = 1;
            tlpInshur.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            //Страховки в системе Уралсиб
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(
                        "Select distinct INS_Numder,INS_Status from URS_Insurance where INS_DGCode=@p1 ",
                        WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p1", _dgCode);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    controlInshurAdd(GetLabel("Страховки в системе Уралсиб"));
                    foreach (DataRow row in dt.Rows)
                    {
                        controlInshurAdd(
                            GetTablePanel(
                                new List<object>
                                    {
                                        GetLabel(row.Field<string>("INS_Numder")),
                                        GetLabel(row.Field<bool>("INS_Status") ? "Выписана" : "Аннулирована")
                                    }.ToArray()));
                    }
                }

            }
            //Туристы
            selctedTurists(dlKey);
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter(
                        @"select distinct ui.INS_Numder,tl.TU_NAMELAT,tl.TU_FNAMELAT  from TuristService as ts 
                                                                inner join tbl_DogovorList as dl on ts.TU_DLKEY=dl.DL_KEY
                                                                inner join tbl_Turist as tl on tl.TU_KEY =ts.TU_TUKEY
                                                                left join URS_Insurance as ui on ts.TU_TUKEY=ui.INS_tukey
                                                                where ((ui.INS_Status=1)or(ui.INS_Status is null)) and dl.DL_SVKEY=6 and dl.DL_DGCOD=@p1",
                        WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p1", _dgCode);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    controlInshurAdd(GetLabel("Туристы по услуге страховка"));
                    foreach (DataRow row in dt.Rows)
                    {
                        controlInshurAdd(
                            GetTablePanel(
                                new List<object>
                                    {
                                        GetTextBox(row.Field<string>("TU_NAMELAT")),
                                        GetTextBox(row.Field<string>("TU_FNAMELAT")),
                                        GetLabel(":" + row.Field<string>("INS_Numder"))
                                    }.ToArray()));
                    }
                }

            }

            //Options
            bool inshurCreate = false;
            using (
                SqlDataAdapter adapter =
                    new SqlDataAdapter("select top 1 op_Isbook from mk_options where op_dlkey=@p1 order by op_id desc",
                                       WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p1", dlKey);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    inshurCreate = dt.Rows[0].Field<bool>("op_Isbook");
                }

            }
            cbInshurCreate.Checked = inshurCreate;


        }

        private void GetBonusAndService(int dlKey)
        {
            tlpBonusCruise.RowStyles.Clear();
            tlpBonusCruise.Height = 15;
            tlpBonusCruise.Controls.Clear();
            tlpBonusCruise.RowCount = 1;
            tlpBonusCruise.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            DataTable dt = new DataTable();
            String selectBonus = @"SELECT actions_id,isNull(isRight,0) as isRight,CDP_NAME,Text
  FROM [dbo].[mk_actions_options]
left join dbo.MK_Cruise_DOPServise on actions_id=CDS_ID where dl_key=@p1";
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectBonus, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@p1", dlKey);
                adapter.Fill(dt);

            }
            if (dt.Rows.Count < 1) return;
            foreach (DataRow row in dt.Rows)
            {
                if (row.Field<string>("CDP_NAME") == null)
                {
                    controlBonusCruiseAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetChekBox(row.Field<int>("actions_id"), row.Field<int>("isRight") == 1 ? true : false),
                                    GetLabel(row.Field<string>("Text"))
                                }.ToArray()));
                }
                else
                {
                    controlBonusCruiseAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetChekBox(row.Field<int>("actions_id"), row.Field<int>("isRight") == 1 ? true : false),
                                    GetLabel(row.Field<string>("CDP_NAME")),
                                    GetTextBox(row.Field<string>("Text"), row.Field<int>("actions_id"))
                                }.ToArray()));
                }

            }

            Button btnBonusCruiseOK = new Button();
            btnBonusCruiseOK.Dock = DockStyle.Right;
            btnBonusCruiseOK.Text = "ОК";
            btnBonusCruiseOK.Width = 150;
            btnBonusCruiseOK.Height = 27;
            btnBonusCruiseOK.Click += BtnBonusCruiseOkOnClick;
            controlBonusCruiseAdd(GetTablePanel(new List<object> {GetLabel(""), btnBonusCruiseOK}.ToArray()));
            tlpBonusCruise.Height += 13;
            tlpBonusCruise.RowStyles[tlpBonusCruise.RowCount - 2].Height = 40;

        }

        private void BtnBonusCruiseOkOnClick(object sender, EventArgs eventArgs)
        {
            List<int> isOk = new List<int>(), isNo = new List<int>();



            //Опрос прочеканых галочек
            foreach (var control in tlpBonusCruise.Controls)
            {
                TableLayoutPanel tlpBuff = control as TableLayoutPanel;
                if (tlpBuff != null)
                {
                    foreach (var control1 in tlpBuff.Controls)
                    {


                        CheckBox buff = control1 as CheckBox;
                        if (buff != null)
                        {
                            if (buff.Checked)
                            {
                                isOk.Add(Convert.ToInt32(buff.Tag));
                            }
                            else
                            {
                                isNo.Add(Convert.ToInt32(buff.Tag));
                            }
                        }
                    }
                }
            }
            //проверка измененых полей
            foreach (var control in tlpBonusCruise.Controls)
            {
                TableLayoutPanel tlpBuff = control as TableLayoutPanel;
                if (tlpBuff != null)
                {
                    foreach (var control1 in tlpBuff.Controls)
                    {


                        TextBox buff = control1 as TextBox;
                        if (buff != null)
                        {
                            if (buff.ForeColor == Color.Crimson)
                            {

                                using (SqlCommand com = new SqlCommand(@"update mk_actions_options set Text=@text,
                WhoChangeText =(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())
                 where DL_key =@p1 and actions_id = @action_id", WorkWithData.Connection))
                                {
                                    dogovorlistline kesh = cbCruises.SelectedItem as dogovorlistline;
                                    com.Parameters.AddWithValue("@p1", kesh.dlkey);
                                    com.Parameters.AddWithValue("@action_id", Convert.ToInt32(buff.Tag));
                                    com.Parameters.AddWithValue("@text", buff.Text);
                                    com.ExecuteNonQuery();
                                }
                                buff.ForeColor = SystemColors.WindowText;
                            }
                        }
                    }
                }
            }
            //измениние галочек в базе
            if (isOk.Count > 0)
            {
                String inString = "";
                foreach (int i in isOk)
                {
                    if (inString == "")
                    {
                        inString = "(" + i.ToString();
                    }
                    else
                    {
                        inString += "," + i.ToString();
                    }
                }
                inString += ")";
                using (SqlCommand com = new SqlCommand(@"update mk_actions_options set
                WhoRight =(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME()),
                [isRight]=1 where DL_key =@p1 and actions_id in " + inString + " and (isRight =0 or isRight is null)",
                                                       WorkWithData.Connection))
                {
                    dogovorlistline kesh = cbCruises.SelectedItem as dogovorlistline;
                    com.Parameters.AddWithValue("@p1", kesh.dlkey);
                    com.ExecuteNonQuery();
                }
            }
            if (isNo.Count > 0)
            {
                String inString = "";
                foreach (int i in isNo)
                {
                    if (inString == "")
                    {
                        inString = "(" + i.ToString();
                    }
                    else
                    {
                        inString += "," + i.ToString();
                    }
                }
                inString += ")";
                using (SqlCommand com = new SqlCommand(@"update mk_actions_options set
                WhoRight =(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME()),
                [isRight]=0 where DL_key =@p1 and actions_id in " + inString + " and isRight =1",
                                                       WorkWithData.Connection))
                {
                    dogovorlistline kesh = cbCruises.SelectedItem as dogovorlistline;
                    com.Parameters.AddWithValue("@p1", kesh.dlkey);
                    com.ExecuteNonQuery();
                }
            }





            
        }

        private void generateBlok1ForAvia(int bronId)
        {
            string text = string.Empty;
            DataTable tmp = WorkWithData.GetAviaTable(bronId);

            text += "Номер брони партенера : " + WorkWithData.GetAviaBron(bronId)+"\n\n";
            foreach (DataRow row in tmp.Rows)
            {
                string line = string.Format("{6}:{0}({1})-{2}({3})  {4}-{5}\n\n", row.Field<string>("CountryFrom"),
                                                                            row.Field<string>("CityFrom"),
                                                                            row.Field<string>("CountryTo"),
                                                                            row.Field<string>("CityTo"),
                                                                            row.Field<string>("reis"),
                                                                            row.Field<string>("AL_NAME"),
                                                                            row.Field<DateTime>("date_from").ToString("dd.MM.yy HH:mm"));
                text += line;
            }
            DataTable turists = WorkWithData.GetAviaTurist(bronId);
            foreach (DataRow row in turists.Rows)
            {
                string line = string.Format("{0} {1} {2} : {3}\n\n",row.Field<string>("NAME"),
                                                                row.Field<string>("FNAME"),
                                                                row.Field<string>("SNAME"),
                                                                row.Field<string>("TICKET"));
                text += line;
            }
            rtbAvia.Text = text;
        }
        private void generateBlok1ForPaket(int dlKey)
        {
            tlpDopPaket.RowStyles.Clear();
            tlpDopPaket.Height = 15;
            tlpDopPaket.Controls.Clear();
            tlpDopPaket.RowCount = 1;
            tlpDopPaket.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            //Загрузка состава пакета
            controlPaketAdd(GetLabel("СОСТАВ ПАКЕТА:"));
            using (
                SqlCommand com =
                    new SqlCommand(
                        "select dl_name from tbl_DogovorList where dl_paketkey = @paket and dl_dgcod =@dgcod ",
                        WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@paket", dlKey);
                com.Parameters.AddWithValue("@dgcod", _dgCode);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    controlPaketAdd(GetLabel(row.Field<string>("dl_name")));
                }
            }
            //Загрузка туристов
            selctedTurists(dlKey);
            DataTable _turists = new DataTable();
            SqlCommand selTurist = new SqlCommand(@"SELECT 
	        distinct
            TU_NAMELAT
	        ,TU_FNAMELAT
	        ,TU_BIRTHDAY
	        ,TU_BIRTHCITY
            ,TU_BIRTHCOUNTRY
            ,TU_PASPORTTYPE
            ,TU_PASPORTNUM
            ,TU_PASPORTDATE
            ,TU_PASPORTDATEEND
            ,TU_PASPORTBYWHOM
            ,TU_CITIZEN
            FROM [dbo].[TuristService] 
            inner join dbo.tbl_Turist on TU_KEY = tu_tukey
            inner join dbo.tbl_dogovorlist on dl_key = tu_dlkey
            where dl_paketkey =@dlkey and dl_dgcod =@dgcod", WorkWithData.Connection);
            selTurist.Parameters.AddWithValue("@dlkey", dlKey);
            selTurist.Parameters.AddWithValue("@dgcod", _dgCode);
            SqlDataAdapter turistadapter = new SqlDataAdapter(selTurist);
            turistadapter.Fill(_turists);

            int i = 0;
            foreach (DataRow row in _turists.Rows)
            {
                ++i;
                spliterPaketAdd();
                controlPaketAdd(GetLabel("Турист " + i.ToString()));
                controlPaketAdd(
                    GetTablePanel(
                        new List<object> {GetLabel("Фамилия:"), GetTextBox(row.Field<string>("TU_NAMELAT"))}.ToArray()));
                controlPaketAdd(
                    GetTablePanel(
                        new List<object> {GetLabel("Имя:"), GetTextBox(row.Field<string>("TU_FNAMELAT"))}.ToArray()));
                DateTime birrthday = row.Field<DateTime>("TU_BIRTHDAY");
                controlPaketAdd(
                    GetTablePanel(
                        new List<object>
                            {
                                GetLabel("Дата:"),
                                GetTextBox(birrthday.Date.ToString("dd.MM")),
                                GetLabel("год рождения:"),
                                GetTextBox(birrthday.Date.Year.ToString())
                            }.ToArray()));
                if ((row.Field<string>("TU_CITIZEN") != string.Empty) && (row.Field<string>("TU_CITIZEN") != null))
                {
                    controlPaketAdd(
                        GetTablePanel(
                            new List<object> {GetLabel("Национальность:"), GetTextBox(row.Field<string>("TU_CITIZEN"))}
                                .ToArray()));
                }
                if ((row.Field<string>("TU_BIRTHCITY") != string.Empty) && (row.Field<string>("TU_BIRTHCITY") != null))
                {
                    controlPaketAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("Mесто рождения:"),
                                    GetTextBox(row.Field<string>("TU_BIRTHCITY") + "(" +
                                               row.Field<string>("TU_BIRTHCOUNTRY") + ")")
                                }.ToArray()));
                }

                controlPaketAdd(GetTablePanel(new List<object>
                    {
                        GetLabel("Загранпаспорт:"),
                        GetTextBox(row.Field<string>("TU_PASPORTTYPE")),
                        GetLabel("№"),
                        GetTextBox(row.Field<string>("TU_PASPORTNUM"))
                    }.ToArray()));
                if (row.Field<DateTime?>("TU_PASPORTDATE") != null)
                {
                    controlPaketAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("дата выдачи з\\паспорта:"),
                                    GetTextBox(row.Field<DateTime>("TU_PASPORTDATE").Date.ToString("dd.MM.yyyy"))
                                }.ToArray()));
                }
                else
                {
                    controlPaketAdd(GetLabel("дата выдачи з\\паспорта:"));
                }

                if (row.Field<DateTime?>("TU_PASPORTDATEEND") != null)
                {
                    controlPaketAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetLabel("дата окончания действия паспорта:"),
                                    GetTextBox(row.Field<DateTime>("TU_PASPORTDATEEND").Date.ToString("dd.MM.yyyy"))
                                }
                                .ToArray()));
                }
                else
                {
                    controlPaketAdd(GetLabel("дата окончания действия паспорта:"));
                }

            }

        }

        private void cbHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            dogovorlistline line = cbHotels.SelectedItem as dogovorlistline;
            if (line != null)
                generateBlok1ForHotel(line.dlkey);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            dogovorlistline line = cbOther.SelectedItem as dogovorlistline;
            if (line != null)
                generateBlok1ForOther(line.dlkey);
        }


        /// <summary>
        /// Отлов ошибок  из объекта  wbBook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wbBook_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser) sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
        }



        /// <summary>
        /// Обработка ошибок от браузера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            // MessageBox.Show(e.Description);
            //e.Handled = true;
        }



        private void btnHotelOk_Click(object sender, EventArgs e)
        {
            dogovorlistline line = cbHotels.SelectedItem as dogovorlistline;
            if (line != null)
            {
                using (
                    SqlCommand com = new SqlCommand("insert into mk_options(OP_DLKEY," +
                                                    "OP_IsBook," +
                                                    "OP_WHO," +
                                                    "OP_LastUpdate) " +
                                                    "values " +
                                                    "(@dl_key" +
                                                    ",@p2" +
                                                    ",(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())" +
                                                    ",GetDate() ) ",
                                                    WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@dl_key", line.dlkey);
                    com.Parameters.AddWithValue("@p2", cbHotelOk.Checked);
                    com.ExecuteNonQuery();
                }
            }
        }

        private void cbInshur_SelectedIndexChanged(object sender, EventArgs e)
        {
            dogovorlistline line = cbInshur.SelectedItem as dogovorlistline;
            if (line != null)
                generateBlok1ForInshur(line.dlkey);
        }

        private void btnInshur_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(winINI);
            String masterPath = ini.IniReadValue("master", "ReportPath", "");
            if (masterPath == "")
            {
                MessageBox.Show("Система выписки страховок не найдена!");
                return;
            }
            try
            {
                //string userName = WorkWithData.Connection.ConnectionString;
                Process.Start(masterPath + "\\rep6050.exe",
                              ltp_v2.Framework.SqlConnection.ConnectionUserName + " " +
                              ltp_v2.Framework.SqlConnection.ConnectionPassword + " !DGCODE=" + _dgCode);
                // ltp_v2.Framework.SqlConnection.ConnectionUserName+" "+ltp_v2.Framework.SqlConnection.ConnectionPassword +" !DGCODE="+_dgCode);
                //Console.WriteLine(userName);

            }
            catch (Exception)
            {

                MessageBox.Show("Система выписки страховок не найдена!");
                return;
            }

        }

        private void selctedTurists(int dlKey)
        {
            SqlCommand selTurist = new SqlCommand(@"SELECT tbl_Turist.tu_key
         FROM [dbo].[TuristService] 
          inner join dbo.tbl_Turist on TU_KEY = tu_tukey
          where tu_dlkey=@dlkey", WorkWithData.Connection);
            selTurist.Parameters.AddWithValue("@dlkey", dlKey);
            SqlDataAdapter adapter = new SqlDataAdapter(selTurist);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (var control in tlpTurists.Controls)
            {
                if (control.GetType() == typeof (TextBox))
                {
                    TextBox tb = control as TextBox;
                    tb.BackColor = dt.Select("tu_key=" + Convert.ToInt32(tb.Tag)).Count() > 0
                                       ? Color.Cyan
                                       : SystemColors.Control;
                }
                else if (control.GetType() == typeof (TableLayoutPanel))
                {
                    TableLayoutPanel tlp = control as TableLayoutPanel;
                    foreach (var control1 in tlp.Controls)
                    {
                        if (control1.GetType() == typeof (TextBox))
                        {
                            TextBox tb = control1 as TextBox;
                            tb.BackColor = dt.Select("tu_key=" + Convert.ToInt32(tb.Tag)).Count() > 0
                                               ? Color.Cyan
                                               : SystemColors.Control;
                        }
                        //else if (control.GetType() == typeof(Label))
                        //{
                        //    Label lb= control as Label;
                        //    lb.BackColor = dt.Select("tu_key=" + Convert.ToInt32(lb.Tag)).Count() > 0
                        //                       ? Color.Cyan
                        //                       : SystemColors.Control;
                        //}
                    }
                }

            }
        }

        private void btnOkInshur_Click(object sender, EventArgs e)
        {
            dogovorlistline line = cbInshur.SelectedItem as dogovorlistline;
            if (line != null)
            {
                using (
                    SqlCommand com = new SqlCommand("insert into mk_options(OP_DLKEY," +
                                                    "OP_IsBook," +
                                                    "OP_WHO," +
                                                    "OP_LastUpdate) " +
                                                    "select dl_key" +
                                                    ",@p2" +
                                                    ",(select top 1 isnull(US_FullName,'Администратор') from UserList where US_USERID = SUSER_SNAME())" +
                                                    ",GetDate() from tbl_dogovorlist where dl_svkey=6 and dl_dgcod =@p1 ",
                                                    WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@p1", _dgCode);
                    com.Parameters.AddWithValue("@p2", cbInshurCreate.Checked);
                    com.ExecuteNonQuery();
                }
            }
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {

            //messagesForm = new ucMessages(_dgCode, panel12.Width, Screen.PrimaryScreen.Bounds.Width-panel12.Width, Screen.PrimaryScreen.Bounds.Height - panel12.Height - 35);
            //messagesForm.Show();
            // messagesForm.TopMost = true;
        }

        private void btnDocumenSettings_Click(object sender, EventArgs e)
        {
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = false;
            frmDocumentServiceSettings frmDocumentSettings = new frmDocumentServiceSettings(_dgCode,
                                                                                            WorkWithData.Connection);
            frmDocumentSettings.ShowDialog();
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = true;
        }

        private void frmNewOptions_Activated(object sender, EventArgs e)
        {

        }

        private void Awesomium_Windows_Forms_WebControl_LoadingFrameFailed(object sender, LoadingFrameFailedEventArgs e)
        {

        }

        private void Awesomium_Windows_Forms_WebControl_ShowCreatedWebView(object sender, ShowCreatedWebViewEventArgs e)
        {
            if ((wbBook == null) || !wbBook.IsLive)
                return;

            if (e.IsPopup)
            {
                frmChildWindow child = new frmChildWindow();
                child.wcChild.WebSession = sessionId;
                child.wcChild.NativeView = e.NewViewInstance;
                child.wspChild = wspBook;
                child.wcChild.Source = e.TargetURL;

                if (!child.IsDisposed) child.ShowDialog();
            }else if (e.IsWindowOpen == false && e.IsNavigationCanceled == true)
            {
                wbBook.Source = e.TargetURL;
            }



        }

        private void btnDogovor_Click(object sender, EventArgs e)
        {
            //string tempPath = Path.GetTempPath();
            //WebClient client =  new WebClient();
            //String url = string.Format("http://www.mcruises.ru/cabinet/classes/extentions/index.php?dgCode={0}&dirDirector=off",_dgCode);

            //Byte[] data = client.DownloadData(url);
            //BinaryWriter bw = new BinaryWriter(File.Create(tempPath+_dgCode+".pdf"));
            //bw.Write(data);
            //bw.Close();
            //System.Diagnostics.Process.Start(tempPath + _dgCode + ".pdf");
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = false;
            new frmDogovorAcepter(_dgCode).ShowDialog();
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = false;
            new frmAccounts(_dgCode, _agencyKey).ShowDialog();
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = true;
        }

        private void btnExtend_Click(object sender, EventArgs e)
        {
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = false;
            string message = @"Вы продлеваете опцию \договор\ Оплата заказа \Внести данные туристов на 24 часа.
Вы уверены что
- опция у партнера продлена ?
- срок внесения данных туристов не просрочен на визовые оформления? 
- не возникает штрафа у партнеров по замене фамилий туристов?";

            if (
                MessageBox.Show(message, "",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string updDatedite = "declare @date datetime " +
                                     "set @date =GetDate() +1  " +
                                     "update dbo.mk_DogovorAdd set da_editDAte = @date where da_dgcode=@dgcode";

                using (SqlCommand com = new SqlCommand(updDatedite, WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@dgcode", _dgCode);
                    com.ExecuteNonQuery();
                }
                string text = @"Вам продлено время оформления данных в Вашем ""Личном кабинете"" до 19:00 {0} .
При не соблюдении сроков оформления заказа и оплаты, круизная компания может аннулировать Вашу бронь. ";
                using (
                    SqlCommand com =
                        new SqlCommand("select top 1 DA_EditDate  FROM [dbo].[mk_DogovorAdd] where DA_DGCODE =@p1 ",
                                       WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@p1", _dgCode);
                    DateTime date = (DateTime) com.ExecuteScalar();
                    text = string.Format(text, date.ToString("dd.MM.yyyy"));

                }
                WorkWithData.InsertHistory(_dgCode, text, "MTC", "");
                //using (SqlCommand com = new SqlCommand(insert_history, WorkWithData.Connection))
                //{
                //    com.Parameters.AddWithValue("@dgcode", _dgCode);
                //    com.Parameters.AddWithValue("@who", WorkWithData.GetUserName());
                //    com.Parameters.AddWithValue("@text", text);
                //    com.Parameters.AddWithValue("@mod", "MTC");
                //    com.Parameters.AddWithValue("@remark", "");
                //    com.ExecuteNonQuery();
                //}
            }
            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (messagesForm != null)
            {
                messagesForm.Close();
            }
            if (_updatedChanges.Count() > 0)
            {
                string message = @"У вас имеются отмеченные обработанными изменения. Хотите сохранить?";
                if (MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (int updatedChange in _updatedChanges)
                    {
                        WorkWithData.AcceptChange(updatedChange);
                    }
                }
            }
            if (_updatedProblem.Count() > 0)
            {
                string message = @"У вас имеются отмеченные обработанными проблемы. Хотите сохранить?";
                if (MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (OKProblem i in _updatedProblem)
                    {
                        WorkWithData.UpdateProblemOk(i.problemCode, i.problemOkStatus,i.problemMessage,_dgCode);

                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            frmSerchDogovors frm = null;
            foreach (var openForm in Application.OpenForms)
            {
                frm = openForm as frmSerchDogovors;
                if (frm != null)
                {
                    break;
                }
            }
            this.Close();
            frm.Close();
        }

        private void Awesomium_Windows_Forms_WebControl_InitializeView(object sender, WebViewEventArgs e)
        {
            foreach (var control in wbBook.Controls)
            {
                MessageBox.Show(control.GetType().ToString());
            }
        }

        private void Awesomium_Windows_Forms_WebControl_AddressChanged(object sender, UrlEventArgs e)
        {
            tbAdress.Text = e.Url.ToString();
        }




        private void frmNewOptions_Shown(object sender, EventArgs e)
        {
            updateDataGridPutevka();
            UpdateDataGridChanges();
            UpdateDataGridProblem();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void dgvPutevka_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            // Пропуск заголовков колонок и строк, и первой строки
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;

            if (IsRepeatedCellValue(e.RowIndex, e.ColumnIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            }
        }

        private void dgvPutevka_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Первую строку всегда показывать
            if (e.RowIndex == 0)
                return;


            if (IsRepeatedCellValue(e.RowIndex, e.ColumnIndex))
            {
                e.Value = string.Empty;
                e.FormattingApplied = true;
            }
        }


        private bool IsRepeatedCellValue(int rowIndex, int colIndex)
        {
            DataGridViewCell currCell = dgvPutevka.Rows[rowIndex].Cells[0];
            DataGridViewCell prevCell = dgvPutevka.Rows[rowIndex - 1].Cells[0];

            if (dgvPutevka.Rows[rowIndex].Cells[colIndex].Value.Equals(dgvPutevka.Rows[rowIndex - 1].Cells[colIndex].Value)&&1==2)
                
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void UpdateDataGridProblem()
        {

            dgvProblems.DataSource = _problems;
            foreach (DataGridViewColumn column in dgvProblems.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "ROWNUM":
                        column.HeaderText = "№ проблемы";
                        column.Width = 100;
                        column.DisplayIndex = 1;
                        break;
                    case "MPC_NAME":
                        column.HeaderText = "Проблема";
                        column.Width = 350;
                        column.DisplayIndex = 2;
                        break;
                    //case "IMAGE1":
                    //    //column = new DataGridViewComboBoxColumn();

                        
                    //    column.HeaderText = "Гарантия клиента";
                    //    column.Width = 140;
                    //    column.DisplayIndex = dgvProblems.Columns.Count-2;
                        
                    //    break;
                    case "PROBLEMSTATUS":
                        column.HeaderText = "Статус";
                        column.Width = 350;
                        column.DisplayIndex = dgvProblems.Columns.Count - 2;
                        break;
                    case "IMAGE2":
                        column.HeaderText = "Посмотреть";
                        column.Width = 100;
                        column.DisplayIndex = dgvProblems.Columns.Count - 1;
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
           
        }

        

        private void UpdateDataGridChanges()
        {
            dgvChanges.DataSource = _changes;
            foreach (DataGridViewColumn column in dgvChanges.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "ROWNUM":
                        column.HeaderText = "№ изменения";
                        column.Width = 100;
                        column.DisplayIndex = 1;
                        break;
                    case "MCC_NAME":
                        column.HeaderText = "Изменение";
                        column.Width = 600;
                        column.DisplayIndex = 2;
                        break;
                    case "MCD_ACCEPT":
                        column.HeaderText = "Обработано";
                        column.Width = 90;
                        column.DisplayIndex = 3;
                        break;
                    case "VIEW":
                        column.HeaderText = "Посмотреть";
                        column.Width = 90;
                        column.DisplayIndex = 4;
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }

        private void dgvChanges_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            switch (dgvChanges.Columns[e.ColumnIndex].Name.ToUpper())
            {
                case "MCD_ACCEPT":
                    DataGridViewRow row = dgvChanges.Rows[e.RowIndex];
                    row.Cells["MCD_ACCEPT"].Value = !((bool) row.Cells["MCD_ACCEPT"].Value);
                    if ((bool) row.Cells["MCD_ACCEPT"].Value)
                    {
                        _updatedChanges.Add((int) row.Cells["MCD_id"].Value);
                    }
                    else
                    {
                        _updatedChanges.RemoveAll(x => x == (int) row.Cells["MCD_id"].Value);
                    }
                    break;
                case "VIEW":
                    switch ((int) dgvChanges.Rows[e.RowIndex].Cells["MCD_CHANGE_CODE"].Value)
                    {
                        case 1:
                            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = false;
                            int idChanges = (int) dgvChanges.Rows[e.RowIndex].Cells["MCD_ID"].Value;
                            new frmChangesTurist(idChanges).ShowDialog();
                            if (messagesForm != null && (!messagesForm.IsDisposed)) messagesForm.TopMost = true;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

        }

        private void tableLayoutPanel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tcpMessages_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("2134");
        }

        private void tcProblemChanges_Click(object sender, EventArgs e)
        {
            if (tcProblemChanges.TabPages.Count == 1 && tcProblemChanges.TabPages[0].Equals(tcpMessages))
            {
                int x = this.Location.X + (this.Width - panel12.Width),
                    y = this.Location.Y + (this.Height - panel12.Height),
                    h = panel12.Height,
                    w = panel12.Width;
                new frmMessages(_dgCode, x, y, h, w).ShowDialog();
            }
        }

        private void tcProblemChanges_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // scPutevka.SplitterDistance =(int) 0.3* panel12.Height;
            if (e.TabPage.Equals(tcpItinerary))
            {
                if (dgvItinerary.DataSource == null)
                {
                    _Itinerary = WorkWithData.GetItinerary(_dgCode);
                    updateDataGrid();
                }
            }
            else if (e.TabPage.Equals(tcpMessages))
            {
                int x = this.Location.X + (this.Width - panel12.Width),
                    y = this.Location.Y + (this.Height - panel12.Height),
                    h = panel12.Height,
                    w = panel12.Width;
                messagesForm = new frmMessages(_dgCode, x, y, h, w);
                messagesForm.TopMost = true;
                messagesForm.Show();

                e.Cancel = true;
                //scPutevka.SplitterDistance =(int)0.7*panel12.Height;
                //tcpMessages.Controls.Add(new ucMessages(_dgCode)
                //{
                //    Dock = DockStyle.Fill
                //});
            }
            else if (e.TabPage.Name.Equals("tcpOldItinerary"))
            {
                DataGridView dgv = e.TabPage.Controls["dgvOldItinerary"] as DataGridView;
                dgv.DataSource = WorkWithData.GetOldItinerary(_dgCode);

            }

        }

        private void dgvProblems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            int problem = (int)dgvProblems.Rows[e.RowIndex].Cells["mp_problemCode"].Value;
            switch (dgvProblems.Columns[e.ColumnIndex].Name)
            {
                case "ProblemStatus":
                    
                    switch (problem)
                    {
                        case 7:
                        case 14:
                        case 15:
                            if (((int)dgvProblems.Rows[e.RowIndex].Cells["ProblemStatusCode"].Value) == -1)
                            {
                                int okStatus = frmOkProblemStatuses.GetIdOKProblem(Cursor.Position.X, Cursor.Position.Y);
                                if (okStatus != -1)
                                {
                                    string message = Microsoft.VisualBasic.Interaction.InputBox("Введите коментарий","Коментарий");
                                    if (string.IsNullOrEmpty(message.Trim()))
                                    {
                                        MessageBox.Show("Коментарий не может быть пустым!", "", MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                                        return;
                                    }
                                    dgvProblems.Rows[e.RowIndex].Cells["ProblemStatusCode"].Value = okStatus;
                                    dgvProblems.Rows[e.RowIndex].Cells["ProblemStatus"].Value =
                                        WorkWithData.GetProblemOkStatuses().Select("PS_ID=" + okStatus.ToString())[0]
                                            .Field<string>("PS_NAME");
                                    _updatedProblem.Add(new OKProblem()
                                        {
                                            problemCode = problem,
                                            problemOkStatus = okStatus,
                                            problemMessage = message.Trim()

                                        });
                                }
                            }
                            break;
                    }
                    break;
                case "image2":
                     
                    switch (problem)
                    {
                        case 7:
                        case 14:
                        case 15:
                            if (((int) dgvProblems.Rows[e.RowIndex].Cells["ProblemStatusCode"].Value) != -1)
                            {
                                MessageBox.Show(dgvProblems.Rows[e.RowIndex].Cells["ProblemMessage"].Value.ToString());
                            }
                            break;
                        case 12:
                            new frmDopDogovorChanges(_dgCode).ShowDialog();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;

                    
            }
        }

        private void dgvProblems_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    DataGridViewRow row = dgvProblems.Rows[e.RowIndex];
            //    if (((int) row.Cells["ProblemStatusCode"].Value) != -1)
            //    {
            //        DataGridViewCell cell = row.Cells["status"];
            //        cell = new DataGridViewTextBoxCell();
            //        cell.Value = row.Cells["ProblemStatus"].Value;
            //    }
            //}
        }

        private void dgvProblems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void btnRegion_Click(object sender, EventArgs e)
        {
            new frmDogovorRegionSetting(_dgCode).ShowDialog();
        }

        private void btnDownloadedFiles_Click(object sender, EventArgs e)
        {
            new frmDownloadedFiles(_dgCode).ShowDialog();
        }

       

        private void cbAvia_SelectedIndexChanged(object sender, EventArgs e)
        {
            dogovorlistline dll = (dogovorlistline) cbAvia.SelectedItem;
            generateBlok1ForAvia(dll.dlkey);
        }

        private void btnFlight_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position);
            /*
            MethodInfo mi = typeof(Button).GetMethod("ShowContextMenu",
                BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(btnFlight, null);
            */
        }

        private void ShowFligth()
        {
            lanta.SQLConfig.Config_XML conf = new Config_XML();
            string baseUrl = conf.Get_Value("appSettings", "flightURL");
            //string dgCode = "CAR61117A1";
            int managerauth = WorkWithData.GetUserID();
            int managerauth2 = WorkWithData.GetDogovorCreater(_dgCode);
            //uint managerauth = 1234;
            string fullUrl = String.Format("{0}?dgCode={1}&managerauth={2}", baseUrl, _dgCode, managerauth);
            wbBook.Source = new Uri(fullUrl);
            //frmFlight frm = new frmFlight(new Uri(fullUrl));
            //frm.ShowDialog();
            //this.SuspendLayout();
            //GetDate(false);
            //this.ResumeLayout();
        }

    }
}
