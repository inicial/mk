using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Awesomium.Core;
using Awesomium.Core.Data;
using Awesomium.Windows.Forms;
using DocumentServices;
using Gecko;
using Gecko.DOM;
using Gecko.IO;
using WpfControlLibrary.Model.Tourist;
using WpfControlLibrary.Util;
using terms_prepaid.Helpers;
using terms_prepaid.Forms;
using terms_prepaid.Properties;
using terms_prepaid.UserControls;
using terms_prepaid.WebGetInfo;
using lanta.SQLConfig;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Timers;
using DataService;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Gecko.Events;
using terms_prepaid.Helper_Classes;
using WpfControlLibrary;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Flight;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.ViewModel;
using WpfControlLibrary.View;
using WpfControlLibrary.Buttons;
using WpfControlLibrary.Messages;
using WpfControlLibrary.Model.Messages;
using Timer = System.Windows.Forms.Timer;


namespace terms_prepaid
{
    public delegate void SelectServiceText(string ServiceText, string ListTag);
    public delegate void SelectOptionTask(bool bBronir, bool bRealiz, string TaskText, DateTime TaskDate, bool ReflectFlag);

    public partial class frmNewOptions : Form
    {
        /*[DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11; */

        public enum ChangeType
        {
            Voucher = 0, Cruise = 1, Visa = 2, Avia = 3, Inshur = 4, Other = -1
        }

        public enum TabType
        {
            Cruises, DopPakets, Hotels, Vises, Inshur, Avia, Other, Transfer
        }

        public enum CruiseTab
        {
            Old = 0,
            New = 1, 
            Both = 2
        }

        public enum Gui
        {
            Old = 0,
            New = 1,
            Both = 2
        }

#region Properties

        private Voucher _voucher;

        private Service _vaucerServiceSelected;

        private BorderStyle NoEditableBorderStyle = BorderStyle.None;
        private Color NoEditableBackColor = Color.Beige;

        private OptionTabControl _optionTabControl;
        private CruiseControl _cruiseControl;

        private GeckoWebBrowser _gwb;

        private bool NavigatingFlag = false;
        private bool NavigatedFlag = false;
        
        private IDogovorSetting _dogovorSettingControl;
        private IDogovorSetting _dogovorSettingControl2;
        //private IBillSetting _billSettingControl;
        private IBillSetting2 _billSetting2Control;
        
        private FlightViewModel _flightViewModel;
        private VoucherViewModel _voucherViewModel;
        private TouristViewModel _touristViewModel;

        private CorrespondenceViewModel _correspViewModel;

        private bool _flyIsInit;
        private bool _voucherIsInit;
        private bool _touristIsInit;
        private bool _correspIsInit;
        private bool _arrivalsButtonIsInit;

        private Config_XML _conf;

        private AddServiceButtonSimple OptionControl;
        private PostponeOrderButton PostponeControl;
        private OrderTaskButton TaskControl;

        //Form frmPassengers; // форма отображения пассажиров / туристов
        Form frmBonuses; // форма отображения бонусов
        Form frmContacts; // форма отображения контактов
        Form frmService; // форма выборазначений для сервиса
        Form frmBill; // форма обработки счета
        Form frmPrices; // форма изменения цен для круиза

        private const string WinIni = "C:\\Windows\\win.ini";
        private string _tempDirectory = Path.GetTempPath();
        private List<int> _updatedChanges = new List<int>();
        private List<OKProblem> _updatedProblem = new List<OKProblem>();
        private AccessClass _access = new AccessClass(WorkWithData.Connection);
        private string _dgCode = "";
        private string _shipCode = "";
        private string _package = "";
        private DateTime _tourDate;
        private DateTime _sailDate;
        private bool _transferFlag;
        private string _cabinCategori;
        private int _agencyKey;
        private int _regNumber;
        private string _dgRate = "";

        private const string FlightBtnStr = "Добавить услугу";
        private const string FlightStr = "Авиаперелет";
        private const string OtherStr = "Другое";
        private bool _webBrowserIsExpand;

        private List<ServiceInfo> _cruises = new List<ServiceInfo>(),
                                      _cruisesRiver = new List<ServiceInfo>(),
                                      _dopPakets = new List<ServiceInfo>(),
                                      _hotels = new List<ServiceInfo>(),
                                      _inshur = new List<ServiceInfo>(),
                                      _other = new List<ServiceInfo>(),
                                      _visa = new List<ServiceInfo>(),
                                      _avia = new List<ServiceInfo>();
                                

        private DataTable _servisesmas = new DataTable(),
                          _Itinerary = new DataTable(),
                          _changes,
                          _problems;

        private DataTable _cabinLevels = new DataTable();
        private WebSession sessionId;
        private frmMessagesNew messagesForm;
        private int _type = 0;

        private string _aviaConfirm;
        private string _aviaError;
        private string _transferConfirm;
        private string _transferError;

        private List<TabPage> _allTabPages;

        private List<TabPage> _annulationTabs;

        private CruiseService _cruiseService;
        private string CruiseCurrency = "";

        public List<NameValue> _bonusesList;
        public List<NameValue> _foodList;
        public List<NameValue> _passengersList;
        public List<NameValue> _touristList;
        public List<NameValue> _touristKeyList;
        public List<NameValue> _contactsList;

        public List<DopServiceItem> lst_DopServices;
        //public List<NameValue> lst_DopServices;
        public List<NameValue> lst_Names_TK;
        public List<NameValue> lst_Names_VP;
        public List<NameValue> lst_Names_TP;
        public List<NameValue> lst_Names_VK;
        public List<NameValue> lst_Names_KT;

        private static bool _debugStop = false;

        private bool _load;

        private TabPage _oldInternaryPage;

        private CruiseTab _cruiseMode = CruiseTab.New;
        private Gui _gui = Gui.Old;
        private CruiseViewModel cruiseViewModel;

        private CorrespondenceView _correspView;

        private IRequestJournalService _requestJournalService;
        private IUsersService _usersService;

        private int _usKey;
        private string _userId;
        private string _userIdBase64;

        //private CefSharp.WinForms.ChromiumWebBrowser wbbrouse = new ChromiumWebBrowser();
        struct OKProblem
        {
            public int problemCode;
            public int problemOkStatus;
            public string problemMessage;
        }

        ServiceTabViewModel _aviaTabViewModel;
        ServiceTabViewModel _visaTabViewModel;
        ServiceTabViewModel _transferTabViewModel;
        ServiceTabViewModel _hotelTabViewModel;
        ServiceTabViewModel _inshurTabViewModel;
        ServiceTabViewModel _problemTabViewModel;
        ServiceTabViewModel _otherServiceTabViewModel;
        ServiceTabViewModel _cruiseServiceTabViewModel;

        ServiceTabSelectorViewModel _selectorViewModel;

        public frmSerchDogovors MainForm;
        public Form SearchForm;
        public frmNewOptionsTask frmTask;

        public List<TaskRecord> BronirTaskList;
        public List<TaskRecord> RealizTaskList;
        //public List<TaskRecord> AllTaskList;
        public List<OptionTask> OptionTaskList;

        private int BronirKey;   // id бронировщика
        private int RealizKey;   // id реализатора
        public bool flgTaskBronir;   // флаг отбора задач для бронировщика
        public bool flgTaskRealiz;   // флаг отбора задач для реализатора

        private Timer TaskerTimer;
        private frmNewOptionsTasker frmTasker;

        #endregion

#region Constructor

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

            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmSerchDogovors))
                {
                    MainForm = (frmSerchDogovors)frm;
                    break;
                }
            }

            _dgCode = dgCode;

            _conf = new Config_XML();

            _allTabPages = new List<TabPage>();

            _requestJournalService = Repository.GetInstance<IRequestJournalService>();
            _usersService = Repository.GetInstance<IUsersService>();

            _usKey = _usersService.GetUsKey();
            _userId = _usersService.GetUserId();
            _userIdBase64 = Base64Serialization.StringToBase64(_userId);

            tcOptions.TabPages.Remove(tcpDopPaket);
            tcProblemChanges.TabPages.Remove(tcpMessages);

            switch (_cruiseMode)
            {
                case CruiseTab.Old:
                    tcOptions.TabPages.Remove(tcpCruiseNew);
                    tcpCruiseNew.Dispose();
                    break;

                case CruiseTab.New:
                    tcOptions.TabPages.Remove(tcpCruise);
                    break;
            }

            switch (_gui)
            {
                case Gui.Old:
                    tcOptions.TabPages.Remove(tcpTest);
                    tcProblemChanges.TabPages.Remove(tcpSettingNew);
                    tcpTest.Dispose();
                    tcpSettingNew.Dispose();
                    break;
            }

            //tcOptions.TabPages.Remove(tcpTest);
            //tcpTest.Dispose();

            _allTabPages.AddRange(tcOptions.TabPages.Cast<TabPage>());

            tlpTurists.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();

            SizeChanged += OnSizeChanged;

            RegisterMessages();
            InitWpfControls();
            InitWb();

            InitTouristData();

            InitVoucherData();

            SetAccess();

            if (_dgCode != null)
                GetData(false);
            else
                EmptyVoucher(ServiceType.Unknow);

            InitOptionControls(); // after get data about transfer
            InitPostponeControls();
            InitTaskControls();
            InitTaskList();

            UpdateTasks();

            _type = type;

            if (tcProblemChanges.TabPages.Count == 1 && tcProblemChanges.TabPages[0].Equals(tcpMessageNew))
            {
                scPutevka.SplitterDistance = panel12.Height - 27;
            }
            else
            {
                if (_dgCode != null) tcProblemChanges_Selecting(tcProblemChanges,
                                           new TabControlCancelEventArgs(tcProblemChanges.TabPages[0], 0, false,
                                                                         TabControlAction.Selected));
            }

            if (_dgCode != null)
                GoToUrlEmpty();

            tcOptions.DrawMode = TabDrawMode.OwnerDrawFixed;
            tcOptions.DrawItem += tabControl1_DrawItem;

            tcProblemChanges.DrawMode = TabDrawMode.OwnerDrawFixed;
            tcProblemChanges.DrawItem += tcProblemChanges_DrawItem;

            //TestMessage();

            TaskerTimer = new Timer();
            TaskerTimer.Interval = 200;
            TaskerTimer.Tick += TaskerTimer_Tick; 
        }


        #endregion

        public void ReloadForm(string NewDgCode, int RegNumber)
        {
            if (string.IsNullOrEmpty(NewDgCode)) return;

            //MessageBox.Show("Окрыть путевку " + NewDgCode);
            this.Hide();
            this.Close();
            if (SearchForm != null) SearchForm.Refresh();

            string msg = "Подождите, пожалуйста, идет загрузка."; // "Загрузка  путевки  " + NewDgCode
            frmIntro intro = new frmIntro(""); // msg
            intro.Show();
            intro.Refresh();

            frmNewOptions newOptions = new frmNewOptions(NewDgCode, 6);
            newOptions.Text += " ver." + newOptions.GetType().Assembly.GetName().Version.ToString()
                          + " db:" + WorkWithData.Connection.Database;
            newOptions.SearchForm = SearchForm;
            newOptions._regNumber = RegNumber;
            newOptions.Show();

            if (intro != null) intro.Close();
        }

        private void TestMessage()
        {
            MessageBox.Show("Из-за технической ошибки опция не создалась в базе поставщика авиауслуг.\n" +
                        " Повторите бронирование по данным параметрам или измените параметры.",
                        "Ошибка бронирования", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void Dispose(TabPage page)
        {
            if(page != null) page.Dispose();
        }

        public new void Dispose()
        {
            if (_dogovorSettingControl != null) _dogovorSettingControl.Dispose();
            //if (_billSettingControl != null) _billSettingControl.Dispose();
            if (_billSetting2Control != null) _billSetting2Control.Dispose();

            if (tcpPartner != null) Dispose(tcpPartner);
            if (tcpPutevka != null) Dispose(tcpPutevka);

            if (tcpProblem != null) Dispose(tcpProblem);

            if (tcpProblemNew != null) Dispose(tcpProblemNew);
            if (tcpChanges != null) Dispose(tcpChanges);
            if (tcpItinerary != null) Dispose(tcpItinerary);
            if (_oldInternaryPage != null) Dispose(_oldInternaryPage);
            if (tcpSettings != null) Dispose(tcpSettings);
            if (tcpTransfers != null) Dispose(tcpTransfers);

            if (_annulationTabs != null)
                foreach (var page in _annulationTabs)
                    Dispose(page);

            if (tcpSettings != null) Dispose(tcpSettings);
            if (tcpBillSettings != null) Dispose(tcpBillSettings);
            if (tcpMessageNew != null) Dispose(tcpMessageNew);
            if (tcpMessages != null) Dispose(tcpMessages);

            if (tlpWebBrowser != null) tlpWebBrowser.Dispose();
            if (_gwb != null) _gwb.Dispose();

            if (tcOptions != null) tcOptions.Selected -= tcOptions_Selected;
            if (_optionTabControl != null) _optionTabControl.Dispose();
        }

        private void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            tlpTurists.ResumeLayout();
            tableLayoutPanel1.ResumeLayout();
            Win32.ResumePainting(tableLayoutPanel1.Handle);
            Refresh();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<SetVoucherMessage>(this, SetVoucherHandler);
            Messenger.Default.Register<SetServiceMessage>(this, SetServiceHandler);
        }

        private void UnregisterMessages()
        {
            Messenger.Default.Unregister<SetVoucherMessage>(this);
            Messenger.Default.Unregister<SetServiceMessage>(this);
        }

        public void SetVoucherHandler(SetVoucherMessage msg)
        {
            if (msg.DgCode != _dgCode) return;
            DispatcherHelper.CheckBeginInvokeOnUI(() => SetVoucher());
        }

        public void SetServiceHandler(SetServiceMessage msg)
        {
            if (msg.DgCode != _dgCode) return;
            DispatcherHelper.CheckBeginInvokeOnUI(() => SetService(ServiceInfo.GetServiceInfo(msg.Service)));
        }

        public void DeregisterEvents()
        {
            if (tcOptions != null) tcOptions.DrawItem -= tabControl1_DrawItem;
            if (tcProblemChanges != null) tcProblemChanges.DrawItem -= tcProblemChanges_DrawItem;
            if (OptionControl != null) OptionControl.OnButtonClick -= OptionControlOnButtonClick;
            if (PostponeControl != null) PostponeControl.OnButtonClick -= PostponeControlOnButtonClick;
            if (TaskControl != null) TaskControl.OnButtonClick -= TaskControlOnButtonClick;
            if (_buttonCloseClickTimer != null) _buttonCloseClickTimer.Tick -= EnableMouseClicks;
            if (_gwb != null) _gwb.Navigated -= GwbOnNavigated;
            if (_gwb != null) _gwb.ShowContextMenu -= browser_ShowContextMenu;
            if (dgvProblems != null) dgvProblems.CellClick -= dgvProblems_CellContentClick;
        }

        private void EmptyVoucher(ServiceType type)
        {
            tcOptions.TabPages.Clear();
            tcOptions.TabPages.Add(tcpCruise);
            WebBrowserMaximize();
            ShowEmptyFlight();
        }

        private void ShowEmptyFlight()
        {
            Config_XML conf = new Config_XML();
            string baseUrl = conf.Get_Value("appSettings", "flightURL");
            string hash = conf.Get_Value("appSettings", "hash");

            int userId = WorkWithData.GetUserID();

            //string fullUrl = String.Format("{0}?user_id={1}&dgCode={2}", baseUrl, userId, _dgCode);
            string fullUrl = String.Format("{0}?user_id={1}", baseUrl, userId);

            var signDate = new SignData(hash);
            signDate.Add("user_id", userId.ToString());

            var postData = MimeInputStream.Create();
            postData.AddContentLength = true;
            postData.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            postData.SetData(String.Format("signData={0}", signDate.GetBase64()));
            GoToUrl(fullUrl + String.Format("&managerauth={0}", hash), postData);
        }

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();

        private void SetTabHeader(TabPage page, Color color)
        {
            page.BackColor = color;
            TabColors[page] = color;
            tcOptions.Invalidate();
        }

#region Form

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (tcOptions == null) return;
            if (tcOptions.TabPages == null) return;
            if (tcOptions.TabPages.Count <= e.Index) return;

            //tcOptions.SuspendLayout();
            TabPage tp = tcOptions.TabPages[e.Index];
            if (tp == null) return;
            if (tcOptions.SelectedTab == null) return;

            bool activeTab = tcOptions.SelectedTab.Equals(tp);
            Color color = activeTab ? Color.Beige : Color.LightGray;

            //using (Brush br = new SolidBrush(TabColors[tcOptions.TabPages[e.Index]]))
            using (Brush br = new SolidBrush(color))
            {
                tp.BackColor = color;
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tcOptions.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tcOptions.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);

                //e.DrawFocusRectangle();

                if (activeTab)
                {
                    Rectangle topRect = new Rectangle(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, 3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.BurlyWood), topRect);
                }
            }
            //tcOptions.ResumeLayout();
        }

        private void tcProblemChanges_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage tp = tcProblemChanges.TabPages[e.Index];
            Color color = tcProblemChanges.SelectedTab.Equals(tp) ? Color.Beige : Color.LightGray;

            //using (Brush br = new SolidBrush(TabColors[tcOptions.TabPages[e.Index]]))
            using (Brush br = new SolidBrush(color))
            {
                tp.BackColor = color;
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tcProblemChanges.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tcProblemChanges.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
            }
        }

        private void frmNewOptions_Load(object sender, EventArgs e)
        {
            _load = true;
            //this.TopMost = true;
            this.tlpDataOfBronCruise.Width = this.panel1.ClientSize.Width;
            // this.tlpCruiseBl1.Width = this.panel2.ClientSize.Width-3;
            
            //SuspendLayout();
            //SendMessage(this.Handle, WM_SETREDRAW, false, 0);

            //tlpTurists.SuspendLayout();
            //tableLayoutPanel1.SuspendLayout();
            
            //tableLayoutPanel1.ResumeLayout();
            //ResumeLayout();

            //SendMessage(this.Handle, WM_SETREDRAW, true, 0);

            //this.tcOptions.SelectedIndex = (_problems.Rows.Count == 0 && tcOptions.TabCount > 1) ? 1 : 0;
            this.tcOptions.SelectedIndex = tcOptions.TabCount > 1 ? 1 : 0;

            Win32.SuspendPainting(tableLayoutPanel1.Handle);

            this.WindowState = FormWindowState.Maximized;

            switch (_type)
            {
                case 1:
                    // btnMessages_Click(null, null);
                    tcPartnerPutevkaTest.SelectTab(tcpPutevka);
                    tcProblemChanges.SelectTab(tcpMessageNew);
                    break;
                case 2:
                    // btnMessages_Click(null, null);
                    tcPartnerPutevkaTest.SelectTab(tcpPutevka);
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
                    tcPartnerPutevkaTest.SelectTab(tcpPutevka);
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
                    tcPartnerPutevkaTest.SelectTab(tcpPutevka);
                    try
                    {
                        tcProblemChanges.SelectTab(tcpItinerary);
                    }
                    catch (Exception)
                    {


                    }
                    break;
                case 5:
                    tcPartnerPutevkaTest.SelectTab(tcpPutevka);
                    try
                    {
                        TabPage tp = tcProblemChanges.TabPages[tcProblemChanges.TabCount - 2];
                        tcProblemChanges.SelectTab(tp);

                    }

                    catch (Exception)
                    {


                    }
                    break;
                case 6:
                    try
                    {
                        tcProblemChanges.SelectTab(tcpBillSettings);
                        //TabPage tp = tcProblemChanges.TabPages[tcProblemChanges.TabCount - 2];
                        //tcProblemChanges.SelectTab(tp);
                        FocusBillTab();
                    }
                    catch (Exception)
                    {
                    }
                    break;
                case 10:
                case 11:
                    try
                    {
                        OpenTasker();
                    }
                    catch (Exception)
                    {
                    }
                    break;
                default:
                    break;
            }

            if (_type != 0) 
                this.Refresh();

            tcPartnerPutevkaTest.SelectTab(tcpPutevka);
        }

        private void frmNewOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            TaskerTimer.Stop();

            if (frmTasker != null)
            {
                if (!frmTasker.IsDisposed) frmTasker.Dispose();
                frmTasker = null;
            }

            if (frmBonuses != null)
            {
                if (frmBonuses.WindowState == FormWindowState.Normal)
                {
                    frmBonuses.Close();
                }
                if (!frmBonuses.IsDisposed) frmBonuses.Dispose();
                frmBonuses = null;
            }
            if (frmContacts != null)
            {
                if (frmContacts.WindowState == FormWindowState.Normal)
                {
                    frmContacts.Close();
                }
                if (!frmContacts.IsDisposed) frmContacts.Dispose();
                frmContacts = null;
            }
            if (frmService != null)
            {
                if (frmService.WindowState == FormWindowState.Normal)
                {
                    frmService.Close();
                }
                if (!frmService.IsDisposed) frmService.Dispose();
                frmService = null;
            }
            if (frmPrices != null)
            {
                if (frmPrices.WindowState == FormWindowState.Normal)
                {
                    frmPrices.Close();
                }
                if (!frmPrices.IsDisposed) frmPrices.Dispose();
                frmPrices = null;
            }
            if (frmBill != null)
            {
                if (frmBill.WindowState == FormWindowState.Normal)
                {
                    frmBill.Close();
                }
                if (!frmBill.IsDisposed) frmBill.Dispose();
                frmBill = null;
            }

            bool bClose = true;

            if (cruiseViewModel != null)
            {
                if (cruiseViewModel.ChangesFlag)
                {
                    bClose = false;
                    frmNewOptionsConfirmClose frm = new frmNewOptionsConfirmClose("");
                    //frm.StartPosX = this.Left + this.Width - frm.Width - 100;
                    //frm.StartPosY = this.Top + 100;
                    frm.StartPosX = this.Left + tcOptions.Left + tcOptions.Width - 30;
                    frm.StartPosY = this.Top + tcOptions.Top + tcOptions.Height - frm.Height - 100;
                    frm.ShowDialog();
                    if (frm.ConfirmFlag) bClose = true;

                    //string msg = cruiseViewModel.GetChangesMessage("Отменить все изменения ?");
                    //if (Repository.GetInstance<IMessageBoxService>().ShowMessage(msg, "Подтверждение", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Warning))
                    //{
                    //    bClose = true;
                    //}
                }
            }
            
            if (!bClose) e.Cancel = true;
        }

        private void frmNewOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (messagesForm != null)
            {
                Dispose();
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
                        WorkWithData.UpdateProblemOk(i.problemCode, i.problemOkStatus, i.problemMessage, _dgCode);

                    }
                }
            }
            Dispose();
        }

#endregion

        private void InitWpfControls()
        {
            WpfAdapter.AttachButton(GoButtonHost, typeof(GoButton), s =>
            {
                GoToUrl(tbAdress.Text); /*GetDate(false, false);*/
            });

            WpfAdapter.AttachButton(ButtonBackHost, typeof(BackButton), s =>
            {
                DisableMouseClicks();
                if (_correspView != null) _correspView.Close();
                if (_dogovorSettingControl != null) _dogovorSettingControl.Dispose();
                //if (_billSettingControl != null) _billSettingControl.Dispose();
                if (_billSetting2Control != null) _billSetting2Control.Dispose();
                DeregisterEvents();
                UnregisterMessages();
                Close();
            });

            WpfAdapter.AttachButton(GoToLkHost, typeof(GotoLkButton), s =>
            {
                if (_voucher.VoucherSettingInfo.ClKey == 0 && _voucher.VoucherSettingInfo.PartnerKey == 0)
                {
                    MessageBox.Show("К путевке не привязан постоянный клинет");
                    return;
                }

                if (_voucher.VoucherSettingInfo.LkUserId == 0)
                {
                    MessageBox.Show("У агентства нет личного кабинета");
                    return;
                }

                if(_access.isRealize)
                    ShowPartnerTab();

                var goToLkURL = _conf.Get_Value("appSettings", "goToLkURL");
                var url = string.Format("{0}?usKey={1}&dgCode={2}&userId={3}", goToLkURL, _usKey, _dgCode, _userIdBase64);
                tcPartnerPutevkaTest.SelectTab(tcpPartner);
                GoToUrlEmpty();
                GoToUrl(url);
            });

            WpfAdapter.AttachButton(LoupeButtonHost, typeof(LoupeButton), s =>
            {
                WebBrowserChangeState();
            });

            WpfAdapter.AttachButton(ReservationButtonHost, new ReservationButton(_dgCode), null);

            //InitOptionControls(); // after get data about transfer
            WpfTabsInit();
        }

        private CruiseViewModel.Permissions GetPermissionLevel()
        {
            return _access.isBronir || _access.isSuperViser
                ? CruiseViewModel.Permissions.Level1
                : CruiseViewModel.Permissions.Level0;
        }

        private void WpfTabsInit()
        {
            var permission = GetPermissionLevel();

            _aviaTabViewModel = WpfAdapter.AttachServiceTab(AviaTabNewHost, new FlightControl(), 
                new FlightViewModel(), SetVoucherService);

            _visaTabViewModel = WpfAdapter.AttachServiceTab(VisaTabNewHost, new VisaControl(), 
                new VisaViewModel(), SetVoucherService);

            _hotelTabViewModel = WpfAdapter.AttachServiceTab(HotelTabNewHost, new HotelControl(),
                new HotelViewModel(), SetVoucherService);

            _inshurTabViewModel = WpfAdapter.AttachServiceTab(InshurTabHost, new InshurControl(),
                new InshurViewModel(ShowInsuranceSystem), SetVoucherService);

            _transferTabViewModel = WpfAdapter.AttachServiceTab(TransferTabHost, new TransferControl(),
                new TranfserViewModel(permission), SetVoucherService);

            _otherServiceTabViewModel = WpfAdapter.AttachServiceTab(OtherServiceTabHost, new OtherServiceControl(), 
                new OtherServiceViewModel(), SetVoucherService);

            if (_cruiseMode != CruiseTab.Old)
            {
                //_cruiseServiceTabViewModel = WpfAdapter.AttachServiceTab(CruiseTabHostNew, new CruiseControl(),
                //    new CruiseViewModel(GenerateBlok1ForVoucher, permission), SetVoucherService);

                //CruiseControl cruiseControl = new CruiseControl();
                _cruiseControl = new CruiseControl();
                _cruiseControl.OnButtonClick_Passengers += btnPassengers_ClickHandler;
                _cruiseControl.OnButtonClick_Prices += btnPrices_ClickHandler;
                _cruiseControl.OnButtonClick_Bill += btnBill_ClickHandler;
                _cruiseControl.OnButtonClick_Bonuses += btnBonuses_ClickHandler;
                _cruiseControl.OnButtonClick_Services += btnServices_ClickHandler;
                _cruiseControl.OnButtonClick_Food += btnFood_ClickHandler;
                _cruiseControl.OnButtonClick_Passport1 += btnPassport1_ClickHandler;
                _cruiseControl.OnButtonClick_Passport2 += btnPassport2_ClickHandler;
                _cruiseControl.OnButtonClick_Service += btnService_ClickHandler;
                //_cruiseControl.OnGotFocus_Service += textService_FocusHandler;
                _cruiseControl.UndoConfirmFunction += UndoConfirmRequest;

                cruiseViewModel = new CruiseViewModel(GenerateBlok1ForVoucher, permission);
                cruiseViewModel.ConfirmSaveFunction += NewOptions_ConfirmSave;

                _cruiseServiceTabViewModel = WpfAdapter.AttachServiceTab(CruiseTabHostNew, _cruiseControl,
                    cruiseViewModel, SetVoucherService);

                _cruiseControl.ScrollToBottom();
            }

            _problemTabViewModel = WpfAdapter.AttachServiceTab(ProblemHost, new ProblemView(), 
                new ProblemViewModel(), null);

            if (_gui != Gui.Old) _dogovorSettingControl2 = WpfAdapter.AttachDogovorSetting(DogovorSettingHost);
           
        }

        private void NewServiceSelectorInit()
        {
            if (_selectorViewModel == null)
            {
                _selectorViewModel = new ServiceTabSelectorViewModel(SetVoucherService, GenerateBlok1ForVoucher,
                    ShowInsuranceSystem, GetPermissionLevel(), _voucher, _voucherViewModel);
                var view = new ServiceTypeSelectorView {DataContext = _selectorViewModel};
                view.InitializeComponent();
                WpfHost.Child = view;
            }
        }

        private int NewOptions_ConfirmSave(string text)
        {
            int iConfirm = 0;

            frmNewOptionsConfirmSave frm = new frmNewOptionsConfirmSave();
            frm.StartPosX = this.Left + tcOptions.Left + tcOptions.Width - 30;
            frm.StartPosY = this.Top + tcOptions.Top + tcOptions.Height - frm.Height - 100;
            frm.ShowDialog();
            if (frm.ConfirmFlag) iConfirm = 1;
            if (frm.CancelFlag) iConfirm = -1;

            bool bBron = cruiseViewModel.BronConfirmFlag();

            if (iConfirm == 1 && bBron)
            {
                iConfirm = 0;
                frmNewOptionsConfirmBron frm2 = new frmNewOptionsConfirmBron();
                frm2.StartPosX = this.Left + tcOptions.Left + tcOptions.Width - 30;
                frm2.StartPosY = this.Top + tcOptions.Top + tcOptions.Height - frm2.Height - 100;
                frm2.ShowDialog();
                if (frm2.ConfirmFlag) iConfirm = 1;
                if (frm2.CancelFlag) iConfirm = -1;
            }

            return iConfirm;
        }

        private void SetVoucherService(CaruselData voucherService)
        {
            var service = ServiceInfo.GetServiceInfo((Service)voucherService);
            //if(_optionTabControl.CurrentPage != null) _optionTabControl.CurrentPage.CurrentService = service;
            SetService(service);

            //SetSelectedTabService(service);
        }

        private void SetSelectedTabService(ServiceInfo service)
        {
            string service_name = "(название выбранной услуги)";
            if (service != null) service_name = service.dl_name;

            if (_voucherViewModel != null)
            {
                _voucherViewModel.SelectedServiceName = service_name;
            }
            
            // set control for service list



        }

        private ChangeType GetChangeType(ServiceInfo serviceInfo)
        {
            if (serviceInfo == null)
                return ChangeType.Voucher;

            switch (serviceInfo.SType)
            {
                case ServiceType.Avia:
                    return ChangeType.Avia;

                case ServiceType.Cruise:
                    return ChangeType.Cruise;

                case ServiceType.Visa:
                    return ChangeType.Visa;

                default:
                    return ChangeType.Other;
            }
        }

        private string GetServiceTabName(ServiceInfo serviceInfo)
        {
            switch (serviceInfo.SType)
            {
                case ServiceType.Avia:
                    return "A/П";

                case ServiceType.Cruise:
                    return "Круизу";

                case ServiceType.Visa:
                    return "Визам";

                case ServiceType.Hotel:
                    return "Отелям";

                /*case ServiceType.DopPaket:
                    return "Доп. пакету";*/

                case ServiceType.Inshur:
                    return "Страховкам";

                default:
                    return "Прочим услугам";
            }
        }

        private void SetService(ServiceInfo serviceInfo)
        {
            if (_dogovorSettingControl != null && _load)
            {
                var service = _voucher.GetService(serviceInfo.dlkey);
                _dogovorSettingControl.SetService(service);
                if (_gui != Gui.Old) _dogovorSettingControl2.SetService(service);
                SetProblemChangesTabs(serviceInfo);
            }
            //if (_billSettingControl != null && _load)
            //{
            //    var service = _voucher.GetService(serviceInfo.dlkey);
            //    _billSettingControl.SetService(service);
            //}
            if (_billSetting2Control != null && _load)
            {
                var service = _voucher.GetService(serviceInfo.dlkey);
                _billSetting2Control.SetService(service);
            }
        }

        private void SetVoucher()
        {
            if (_dogovorSettingControl != null)
            {
                _dogovorSettingControl.SetVoucher(_voucher.GeneralServiceList.Count);
                SetProblemChangesTabs(null);
                UpdateDataGridProblem();
            }
            //if (_billSettingControl != null)
            //{
            //    _billSettingControl.SetVoucher(_voucher.GeneralServiceList.Count);
            //}
            if (_billSetting2Control != null)
            {
                _billSetting2Control.SetVoucher(_voucher.GeneralServiceList.Count);
            }
        }

        private bool TabExist(TabPage page)
        {
            return tcProblemChanges.TabPages.IndexOf(page) != -1;
        }


        private void SetTabState(TabPage page, bool condition, ref int index)
        {
            if (page == null) return;

            if (condition)
            {
                if (!TabExist(page))
                    try
                    {
                        tcProblemChanges.TabPages.Insert(index, page);
                    }
                    catch (Exception e)
                    {
                        TpLogger.Error("tcProblemChanges.TabPages.Insert error", "tcProblemChanges.TabPages.Insert error", e);
                    }
                index++;
            }
            else if (TabExist(page))
            {
                tcProblemChanges.TabPages.Remove(page);
                //page.Dispose();
            }
        }

        private void SetProblemChangesTabs(ServiceInfo serviceInfo)
        {
            tcProblemChanges.DrawItem -= tcProblemChanges_DrawItem;

            var tabIndex = tcProblemChanges.SelectedIndex;
            var tab = tcProblemChanges.SelectedTab;

            int index = 0;

            var changeType = GetChangeType(serviceInfo);

            var p = serviceInfo != null ? _voucher.GetProblemServicesByType(serviceInfo.SType).ToList() : null;

            var dv = changeType == ChangeType.Voucher ? new DataView(_changes) : new DataView(_changes) { RowFilter = String.Format("ChangeType = {0}", (int)changeType) };
            tcpChanges.Text = serviceInfo != null ? String.Format("Изменения по {0} ({1})", GetServiceTabName(serviceInfo), dv.Count) :
                String.Format("Изменения ({0})", dv.Count);

            SetTabState(tcpProblem, serviceInfo == null && _problems.Rows.Count > 0, ref index);
            SetTabState(tcpProblemNew, serviceInfo != null && p.Count > 0, ref index);

            SetTabState(tcpChanges, dv.Count > 0, ref index);

            //bool bShow = true;
            //if (serviceInfo != null && serviceInfo.SType != ServiceType.Cruise) bShow = false;
            //SetTabState(tcpItinerary, bShow, ref index);
            SetTabState(tcpItinerary, !(serviceInfo != null && serviceInfo.SType != ServiceType.Cruise), ref index);
            SetTabState(_oldInternaryPage, serviceInfo != null && serviceInfo.SType == ServiceType.Cruise && _oldInternaryPage != null, ref index);

            if (_annulationTabs != null && _annulationTabs.Count > 0)
            {
                SetTabState(tcpSettings, true, ref index);
                foreach (var page in _annulationTabs)
                {
                    SetTabState(page, serviceInfo == null, ref index);
                }
                SetTabState(tcpMessageNew, true, ref index);
            }

            if(serviceInfo != null && p.Count > 0)
            {
                tcpProblemNew.Text = String.Format("Проблемы по {0} ({1})", GetServiceTabName(serviceInfo), p.Count);
                _problemTabViewModel.Services = new ObservableCollection<CaruselData>(_voucher.GetProblemServiceGroup(serviceInfo.SType));
            }

            dgvChanges.DataSource = dv;

            UpdateDataGridChanges();

            if (!tcProblemChanges.SelectedTab.Equals(tab))
            {
                tcProblemChanges.SelectedIndex = tabIndex < tcProblemChanges.TabCount && tcProblemChanges.TabPages[tabIndex] != tcpMessageNew ? tabIndex : 0;
            }

            tcProblemChanges.DrawItem += tcProblemChanges_DrawItem;
            tcProblemChanges.Refresh();
        }

        /// Добавление кнопки "Добавить услугу"
        private void InitOptionControls()
        {
            OptionControl = new AddServiceButtonSimple(_transferFlag, true);
            OptionControl.OnButtonClick += OptionControlOnButtonClick;
            OptionControl.InitializeComponent();
            OptionHost.Child = OptionControl;
        }

        /// Добавление кнопки "Отложить заказ"
        private void InitPostponeControls()
        {
            PostponeControl = null;
            PostponeControl = new PostponeOrderButton();
            PostponeControl.OnButtonClick += PostponeControlOnButtonClick;
            PostponeControl.InitializeComponent();
            PostponeHost.Child = PostponeControl;
        }

        /// Добавление кнопки "Задача к заказу"
        private void InitTaskControls()
        {
            TaskControl = new OrderTaskButton();
            TaskControl.OnButtonClick += TaskControlOnButtonClick;
            TaskControl.InitializeComponent();
            TaskHost.Child = TaskControl;
        }

        private void OptionControlOnButtonClick(object sender)
        {
            int selected = 0;
            if (sender.GetType() == typeof(AddServiceButtonSimple))
            {
                AddServiceButtonSimple ServiceButton = (AddServiceButtonSimple)sender;
                selected = ServiceButton.Selected;
            }
            if (selected == 1)
            {
                ShowPartnerTab();
                //_optionTabControl.SelectPage(TabType.Avia);
                tcOptions.SelectedIndex = 0;
                ShowFligth();
                AddingAviaServiceEnable();
            }
            if (selected == 2)
            {
                ShowPartnerTab();
                //_optionTabControl.SelectPage(TabType.Avia);
                tcOptions.SelectedIndex = 0;
                ShowTransfer();
                AddingTransferServiceEnable();
            }

            if (selected == 3)
            {
                //MessageBox.Show("Добавление медицинской страховки (в разработке).", "Сообщение", MessageBoxButtons.OK);

                frmIntro intro = new frmIntro("");
                intro.Show();
                intro.Refresh();

                _voucherViewModel.Voucher.TourDate = _tourDate;
                frmNewOptionsEditInsurance frmInsEditor = new frmNewOptionsEditInsurance(_voucherViewModel, UpdateInsData); 
                if (intro != null) intro.Close();
                frmInsEditor.ShowDialog();
            }

        }

        private void PostponeControlOnButtonClick(object sender)
        {
            int selected = 0;
            if (sender.GetType() == typeof(PostponeOrderButton))
            {
                PostponeOrderButton PostponeButton = (PostponeOrderButton)sender;
                selected = PostponeButton.Selected;
            }
            if (selected == 1)
            {

            }
            if (selected == 2)
            {

            }
            if (selected == 3)
            {

            }

            int prior = selected;
            if (prior < 1  || prior > 3) prior = 3;
            string msg = "Заказ  " + _dgCode + "  будет  отложен.";
            //string msg = "Заказ  будет  отложен  с  приоритетом  " + prior.ToString() + ".";
            //if (prior >= 3) msg = "Заказ  будет  отложен  ( без  приоритета ).";
            //msg = msg + (char)13 + (char)10;
            //msg = msg + (char)13 + (char)10;
            //msg = msg + "Отложить  заказ ?" + (char)13 + (char)10;

            frmNewOptionsConfirmPostpone frm = new frmNewOptionsConfirmPostpone(msg);
            frm.ShowDialog();
            if (frm.ConfirmFlag) 
            {
                PostponeOrder(prior);
                this.Close();
            }
        }

        private void PostponeOrder(int prior)
        {
            AddDogovorTask(prior, DateTime.Now.Date, "Отложенный заказ", "", 10, false);
        }

        private void AddDogovorTask(int prior, DateTime task_date, string name, string stat, int code, bool reflect)
        {
            bool bDone = false;
            string res = "";
            try
            {
                res = Do_AddDogovorTask(prior, task_date, name, stat, code, reflect);
                if (string.IsNullOrEmpty(res)) bDone = true;
            }
            catch (System.Exception ex)
            {
                res = ex.Message;
            }

            if (!bDone)
            {
                string msg = "Не удалось добавить задачу.";
                if (!string.IsNullOrEmpty(res)) 
                    msg = msg + (char)13 + (char)10 + "Сообщение об ошибке: " + (char)13 + (char)10 + res;
                MessageBox.Show(msg, "Предупреждение", MessageBoxButtons.OK);
            }
        }

        private string Do_AddDogovorTask(int prior, DateTime task_date, string name, string stat, int code, bool reflect)
        {
            int task_key = 0;

            string sel_query = @"SELECT * FROM mk_DogovorTask WHERE DG_CODE=@DG_CODE AND IsNull([DT_CLOSED],0)=0";

            using (SqlCommand com = new SqlCommand(sel_query, WorkWithData.Connection))
            {
                try
                {
                    com.Parameters.AddWithValue("@DG_CODE", _dgCode);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        string task_name = row.Field<string>("DT_NAME");
                        int id = row.Field<int>("DT_KEY");
                        if (task_name == name)
                        {
                            task_key = id;
                            break;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    TpLogger.Debug("AddDogovorTask > SELECT", ex);
                    return "Ошибка при обращении к базе";
                }

            }

            if (task_key > 0)
            {
                string qry = "UPDATE mk_DogovorTask SET";
                qry = qry + "[DT_PRIOR]=@prior";
                qry = qry + ",[DT_CREATE_DATE]=GETDATE()";
                qry = qry + ",[DT_TASK_DATE]=@task_date";
                qry = qry + ",[DT_NAME]=@name";
                qry = qry + ",[DT_STAT]=@stat";
                qry = qry + ",[DT_CREATOR]=SUSER_SNAME()";
                qry = qry + " WHERE [DG_CODE]=@DG_CODE AND DT_KEY=@KEY";

                using (SqlCommand com = new SqlCommand(qry, WorkWithData.Connection))
                {
                    var kesh = cbCruises.SelectedItem as ServiceInfo;
                    com.Parameters.AddWithValue("@DG_CODE", _dgCode);
                    com.Parameters.AddWithValue("@KEY", task_key);
                    com.Parameters.AddWithValue("@prior", prior);
                    com.Parameters.AddWithValue("@task_date", task_date);
                    com.Parameters.AddWithValue("@name", name);
                    com.Parameters.AddWithValue("@stat", stat);
                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("AddDogovorTask > UPDATE", ex);
                        return "Ошибка записи задачи в базу";
                    }
                }
            }
            else
            {
                int number = 0;

                string qry = "INSERT INTO mk_DogovorTask (";
                qry = qry + "[DG_CODE],[DT_PRIOR],[DT_NUMBER],[DT_CREATE_DATE],[DT_TASK_DATE],[DT_NAME],[DT_STAT],[DT_CREATOR],[DT_CODE]";
                qry = qry + ") VALUES (";
                qry = qry + "@DG_CODE, @prior, @number, GETDATE(), @task_date, @name, @stat, SUSER_SNAME(), @code";
                qry = qry + " )";

                using (SqlCommand com = new SqlCommand(qry, WorkWithData.Connection))
                {
                    var kesh = cbCruises.SelectedItem as ServiceInfo;
                    com.Parameters.AddWithValue("@DG_CODE", _dgCode);
                    com.Parameters.AddWithValue("@prior", prior);
                    com.Parameters.AddWithValue("@number", number);
                    com.Parameters.AddWithValue("@task_date", task_date);
                    com.Parameters.AddWithValue("@name", name);
                    com.Parameters.AddWithValue("@stat", stat);
                    com.Parameters.AddWithValue("@code", code);
                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("AddDogovorTask > INSERT", ex);
                        return "Ошибка записи задачи в базу";
                    }
                }
            }

            if (reflect)
            {
                string msg = "Поставлена задача: " + name + ".  Срок до " + task_date.ToString("dd.MM.yy");
                try
                {
                    SendTask(msg);
                }
                catch (System.Exception ex)
                {
                    TpLogger.Debug("AddDogovorTask > SendTask", ex);
                    //return "Ошибка отправки сообщения";
                    return "";
                }
            }

            if (MainForm != null) MainForm.UpdateDayTasks();

            UpdateTasks();

            return "";
        }

        private IEnumerable<string> SplitString(string str, int maxSymbols)
        {
            return maxSymbols > 0 ?
                Enumerable.Range(0, (int)Math.Ceiling((double)str.Length / maxSymbols)).Select(i => str.Substring(i * maxSymbols, Math.Min(str.Length - i * maxSymbols, maxSymbols)))
                : null;
        }

        private void SendTask(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;

            ICorrespondenceService _service = Repository.GetInstance<ICorrespondenceService>();
            CorrespondenceBase Corresp = new Correspondence(_dgCode, CorrespondenceType.Manager);

            int _maxSymbols = int.MaxValue;
            var lbuff = new List<string>();
            var sb = new StringBuilder();
            var words = msg.Split(' ');

            foreach (var word in words)
            {
                if (sb.Length + word.Length > _maxSymbols)
                {
                    if (sb.Length > 0)
                    {
                        lbuff.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                    if (word.Length > _maxSymbols)
                    {
                        lbuff.AddRange(SplitString(word, _maxSymbols));
                        sb = new StringBuilder();
                        continue;
                    }
                }

                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(word);
            }

            if (sb.Length > 0)
                lbuff.Add(sb.ToString());

            foreach (string s in lbuff)
                _service.InsertHistory2(((Correspondence)Corresp).DgCode, s, Corresp.Mod, "");
        }

        private void TaskControlOnButtonClick(object sender)
        {
            flgTaskBronir = false;
            flgTaskRealiz = false;
            bool bBronir = _access.isBron || _access.isBronir || _access.isAviaBron;
            bool bRealiz = _access.isRealize;
            bool bSuper = _access.isSuperViser;
            if (bSuper || bBronir) flgTaskBronir = true;
            if (bSuper || bRealiz) flgTaskRealiz = true;
            if (BronirKey > 0 && BronirKey == _access.userKey) flgTaskBronir = true;
            if (RealizKey > 0 && RealizKey == _access.userKey) flgTaskRealiz = true;
            if (!flgTaskBronir && !flgTaskRealiz)
            {
                //flgTaskBronir = true;
                flgTaskRealiz = true;
            }
//flgTaskBronir = false;
//flgTaskRealiz = false;

            if (frmTask != null)
            {
                if (frmTask.WindowState == FormWindowState.Normal)
                {
                    frmTask.Close();
                }
                if (!frmTask.IsDisposed) frmTask.Dispose();
                frmTask = null;
            }
            if (frmTask == null)
            {
                int x = this.Left + TaskHost.Left - 50;
                int y = this.Top + tableLayoutOption.Top + tableLayoutOption.Height + 30;
                int maxy = this.Top + this.Height - 15;

                frmTask = new frmNewOptionsTask(x, y, BronirTaskList, RealizTaskList, SetTask, flgTaskBronir, flgTaskRealiz);
                frmTask.Show();
                if (frmTask.Top + frmTask.Height > maxy) frmTask.Top = maxy - frmTask.Height;
            }
        }

        public void SetTask(bool bBronir, bool bRealiz, string TaskText, DateTime TaskDate, bool ReflectFlag)
        {
            //flgTaskBronir = bBronir;
            //flgTaskRealiz = bRealiz;

            if (string.IsNullOrEmpty(TaskText)) return;

            //string msg = "Назначение зачачи к заказу: " + (char)13 + (char)10 + TaskText + (char)13 + (char)10;
            //msg = msg + "Время исполнения: " + TaskDate.ToString("dd.MM.yy") + (char)13 + (char)10;
            //if (ReflectFlag)
            //    msg = msg + "Отразить задачу в переписке." + (char)13 + (char)10;
            //msg = msg + (char)13 + (char)10 + "Назначить  задачу ?" + (char)13 + (char)10;
            //if (MessageBox.Show(msg, "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    bConfirmed = true;

            string msg = TaskText;
            string msg2 = "Deadline: " + TaskDate.ToString("dd.MM.yy");
            string msg3 = "";
            if (ReflectFlag)
                msg3 = "Отразить задачу в переписке." + (char)13 + (char)10;

            bool bConfirmed = false;
            frmNewOptionsConfirmTask frm = new frmNewOptionsConfirmTask(msg, msg2, msg3); // string TaskMsg, string DedalineMsg, string ReflectMsg
            //frm.StartPosX = this.Left + tcOptions.Left + tcOptions.Width - 30;
            //frm.StartPosY = this.Top + tcOptions.Top + tcOptions.Height - frm.Height - 100;
            frm.ShowDialog();
            if (frm.ConfirmFlag) bConfirmed = true;

            if (bConfirmed)
            {
                AddDogovorTask(3, TaskDate, TaskText, "Назначенная задача", 11, ReflectFlag);
            }
        }

        private void BronirTaskList_Add(string task_text, int group)
        {
            TaskRecord task = new TaskRecord();
            task.TaskName = task_text;
            task.TaskGroup = group;

            BronirTaskList.Add(task);
        }

        private void RealizTaskList_Add(string task_text, int group)
        {
            TaskRecord task = new TaskRecord();
            task.TaskName = task_text;
            task.TaskGroup = group;

            RealizTaskList.Add(task);
        }

        //private void AllTaskList_Add(string task_text, int group)
        //{
        //    TaskRecord task = new TaskRecord();
        //    task.TaskName = task_text;
        //    task.TaskGroup = group;

        //    AllTaskList.Add(task);
        //}

        private void AddTaskToList(string task_text, int group, bool bBronir, bool bRealiz)
        {
            if (bBronir)
                BronirTaskList_Add(task_text, group);

            if (bRealiz)
                RealizTaskList_Add(task_text, group);

            //AllTaskList_Add(task_text, group);
        }

        private void AddBronirTask(string task_text, int group)
        {
            AddTaskToList(task_text, group, true, false);
        }

        private void AddRealizTask(string task_text, int group)
        {
            AddTaskToList(task_text, group, false, true);
        }

        private void InitTaskList()
        {
            if (BronirTaskList == null) BronirTaskList = new List<TaskRecord>();
            BronirTaskList.Clear();
            if (RealizTaskList == null) RealizTaskList = new List<TaskRecord>();
            RealizTaskList.Clear();
            //if (AllTaskList == null) AllTaskList = new List<TaskRecord>();
            //AllTaskList.Clear();

            string query = @"SELECT [DTN_KEY],[DTN_NAME],[DTN_ORDER],[DTN_BLOCK],[DTN_GROUP],[DTN_BRONIR],[DTN_REALIZ]";
            query = query + " FROM [dbo].[mk_DogovorTaskName] ORDER BY [DTN_ORDER]";
            //query = query + " WHERE [DTN_BRONIR] > 0";

            try
            {
                using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    int prev_block = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row.Field<string>("DTN_NAME");
                        int block = row.Field<int>("DTN_BLOCK");
                        //int group = row.Field<int>("DTN_GROUP");
                        int bronir = row.Field<int>("DTN_BRONIR");
                        int realiz = row.Field<int>("DTN_REALIZ");
                        if (!string.IsNullOrEmpty(name) && block > 0)
                        {
                            if (bronir > 0)
                            {
                                if (block != prev_block && prev_block > 0)
                                    if (BronirTaskList.Count > 0) AddBronirTask("", 0);
                                AddBronirTask(name, block);
                            }
                            if (realiz > 0)
                            {
                                if (block != prev_block && prev_block > 0)
                                    if (RealizTaskList.Count > 0) AddRealizTask("", 0);
                                AddRealizTask(name, block);
                            }
                        }
                        prev_block = block;
                    }
                }
            }
            catch (System.Exception ex)
            {
                TpLogger.Debug("InitTaskList", ex);
            }

            /*
            AddBronirTask("Выслать список экскурсий", 1);
            AddBronirTask("Запросить цены на экскурсии", 1);
            AddBronirTask("Запросить цены на отель", 1);
            AddBronirTask("Запросить цены на трансфер", 1);
            AddBronirTask("Запросить услугу у партнера", 1);
            AddBronirTask("Запросить данные от клиента для бронирования отеля", 1);
            AddBronirTask("Получить данные от клиента для бронирования трансфера", 1);
            AddBronirTask("", 0);
            AddBronirTask("Забронировать экскурсии", 2);
            AddBronirTask("Забронировать отель", 2);
            AddBronirTask("Забронировать трансфер", 2);
            AddBronirTask("Забронировать авиаперелет", 2);
            AddBronirTask("", 0);
            AddBronirTask("Проверить ответ партнера по забронированной услуге: экскурсия", 3);
            AddBronirTask("Проверить ответ партнера по забронированной услуге: отель", 3);
            AddBronirTask("Проверить ответ партнера по забронированной услуге: трансфер", 3);
            AddBronirTask("Проверить ответ партнера по забронированной услуге: круиз", 3);
            AddBronirTask("Уточнить визы к маршруту", 3);
            AddBronirTask("", 0);
            AddBronirTask("Выставить счет клиенту/агентству", 4);
            AddBronirTask("Запросить счет у партнера", 4);
            //AddBronirTask("", 0);

            //RealizTaskList_Add("выслать клиенту список экскурсий", 5);
            //RealizTaskList_Add("", 0);
            AddRealizTask("Выслать клиенту список экскурсий", 5);
            AddRealizTask("", 0);
            AddRealizTask("Предложить авиаперелет", 6);
            AddRealizTask("Предложить отель", 6);
            AddRealizTask("Предложить трансфер", 6);
            AddRealizTask("Предложить страховку от невыезда", 6);
            AddRealizTask("Предложить пакет напитков", 6);
            AddRealizTask("Предложить забронировать услугу", 6);
            AddRealizTask("", 0);
            AddRealizTask("Предупредить клиента", 7);
            AddRealizTask("Позвонить клиенту", 7);
            AddRealizTask("Написать клиенту", 7);
            AddRealizTask("", 0);
            AddRealizTask("Уточнить визы к маршруту", 7);
            //AddRealizTask("", 0);
            */
        }

        private void InitTouristData()
        {
            if (!_touristIsInit)
            {
                //tlpTurists.Controls.Clear();
                //tlpTurists.Height = 27;

                var touristView = new TouristView();
                _touristViewModel = new TouristViewModel();
                touristView.DataContext = _touristViewModel;
                touristView.InitializeComponent();
            }
        }

        private void InitVoucherData()
        {
            if (!_voucherIsInit)
            {
                bool bPartner = false;
                if (_access.isBronir || _access.isSuperViser) bPartner = true;
//bPartner = false;

                _voucherViewModel = new VoucherViewModel(OnVaucerServiceSelected, "Вся путевка");
                _voucherViewModel.ServiceCurrency = CruiseCurrency;
                _voucherViewModel.SetPartnerFlag(bPartner);
                
//        var voucherView = new VoucherView(bPartner, OnServiceMouseTask) { DataContext = _voucherViewModel };
//        voucherView.InitializeComponent();
//        VaucherTabHost.Child = voucherView;

                var voucherView = new ServiceListView(OnServiceEditClick) { DataContext = _voucherViewModel };
                voucherView.InitializeComponent();
                VaucherTabHost.Child = voucherView;
                
                var voucherTabView = new VoucherTabView { DataContext = _voucherViewModel };
                voucherTabView.InitializeComponent();
                VoucherTabHost.Child = voucherTabView;

                /*var voucherStatusView = new VoucherStatusView();
                voucherTabView.DataContext = _voucherViewModel;
                voucherTabView.InitializeComponent();
                VoucherTabHost.Child = voucherTabView;*/

                _voucherIsInit = true;
            }
        }

        private void InitArrivalsButton_()
        {
            if (!_arrivalsButtonIsInit)
            {
                ArrivalsButtonViewModel arrivalsButtonViewModel = new ArrivalsButtonViewModel();
                var arrivalsButton = new ArrivalsButton { DataContext = arrivalsButtonViewModel };
                arrivalsButton.InitializeComponent();
                ArrivalsButtonHost.Child = arrivalsButton;
                _arrivalsButtonIsInit = true;
            }
        }

        private void InitVoucherStatus()
        {

        }

#region MouseClickDelay

        private static MouseClickMessageFilter _filter;
        private static System.Windows.Forms.Timer _buttonCloseClickTimer;

        /// <summary>
        /// Отключение обработчика клика мыши для приложения
        /// </summary>
        private void DisableMouseClicks()
        {
            if (_filter == null)
            {
                _filter = new MouseClickMessageFilter();
                Application.AddMessageFilter(_filter);
            }

            int closingDelay;

            try
            {
                Config_XML conf = new Config_XML();
                closingDelay = Convert.ToInt32(conf.Get_Value("appSettings", "newOptionClosingDelay"));
            }
            catch (Exception e)
            {
                closingDelay = 0;
            }

            if (closingDelay != 0)
            {
                _buttonCloseClickTimer = new System.Windows.Forms.Timer {Interval = closingDelay};
                _buttonCloseClickTimer.Tick += EnableMouseClicks;
                _buttonCloseClickTimer.Start();
            }
            else
            {
                EnableMouseClicks(null, null);
            }
        }

        /// <summary>
        /// Включение обработчика клика мыши для приложения
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArgs"></param>
        private static void EnableMouseClicks(Object myObject, EventArgs myEventArgs)
        {
            if (_buttonCloseClickTimer != null)
                _buttonCloseClickTimer.Stop();

            if ((_filter != null))
            {
                Application.RemoveMessageFilter(_filter);
                _filter = null;
            }
        }

        #endregion

#region WebBrowser

        private void InitWb()
        {
            //const string startUrl = "http://google.ru";
            _gwb = new GeckoWebBrowser {Dock = DockStyle.Fill};
            tlpWebBrowser.Controls.Add(_gwb);

            //this.Load += delegate { _gwb.Navigate(startUrl); };
            Xpcom.Initialize("Firefox");

            _gwb.Navigating += GwbOnNavigating;
            _gwb.Navigated += GwbOnNavigated;
            _gwb.ShowContextMenu += browser_ShowContextMenu;

            Config_XML conf = new Config_XML();

            try
            {
                bool geckoCaching = Convert.ToBoolean(conf.Get_Value("appSettings", "geckoCaching"));

                GeckoPreferences.User["browser.cache.disk.enable"] = geckoCaching;
                GeckoPreferences.User["places.history.enabled"] = geckoCaching;
            }
            catch (Exception e)
            {
                TpLogger.Error("set gecko caching", "set state", e);
            }
        }

        private void SetAddressText(string url)
        {
            string text = url;
            if (url.IndexOf("destroy_session") > -1) text = "";
            tbAdress.Text = text;
            tbAdress.Refresh();
        }

        private void GwbOnNavigating(object sender, GeckoNavigatingEventArgs geckoNavigatingEventArgs)
        {
            //tbAdress.Text = _gwb.Url.ToString();
            SetAddressText(_gwb.Url.ToString());
            NavigatingFlag = true;
            NavigatedFlag = false;
        }

        private void GwbOnNavigated(object sender, GeckoNavigatedEventArgs geckoNavigatedEventArgs)
        {
            //tbAdress.Text = _gwb.Url.ToString();
            SetAddressText(_gwb.Url.ToString());
            NavigatingFlag = false;
            NavigatedFlag = true;
        }

        private void AddingAviaServiceEnable()
        {
            Config_XML conf = new Config_XML();
            _aviaConfirm = conf.Get_Value("appSettings", "aviaConfirm");
            _aviaError = conf.Get_Value("appSettings", "aviaError");
            _gwb.Navigated -= gwb_Navigated;
            _gwb.Navigated += gwb_Navigated;
            //_gwb.CreateWindow -= new EventHandler<GeckoCreateWindowEventArgs>(gwb_CreateWindow);
            //_gwb.CreateWindow += new EventHandler<GeckoCreateWindowEventArgs>(gwb_CreateWindow);
        }

        private void AddingAviaServiceDisable()
        {
            _gwb.Navigated -= gwb_Navigated;
            //_gwb.CreateWindow -= new EventHandler<GeckoCreateWindowEventArgs>(gwb_CreateWindow);
        }

        private void AddingTransferServiceEnable()
        {
            Config_XML conf = new Config_XML();
            _transferConfirm = conf.Get_Value("appSettings", "transferConfirm");
            _transferError = conf.Get_Value("appSettings", "transferError");
            _gwb.Navigated -= gwb_Navigated_T;
            _gwb.Navigated += gwb_Navigated_T;
            //_gwb.CreateWindow -= new EventHandler<GeckoCreateWindowEventArgs>(gwb_CreateWindow);
            //_gwb.CreateWindow += new EventHandler<GeckoCreateWindowEventArgs>(gwb_CreateWindow);
        }

        private void AddingTransferServiceDisable()
        {
            _gwb.Navigated -= gwb_Navigated_T;
            //_gwb.CreateWindow -= new EventHandler<GeckoCreateWindowEventArgs>(gwb_CreateWindow);
        }

        private void browser_ShowContextMenu(object sender, GeckoContextMenuEventArgs e)
        {
            if (sender != null && e != null && e.TargetNode != null)
            {
                if (e.TargetNode.NodeType == NodeType.Element & e.TargetNode.NodeName == "INPUT")
                {
                    GeckoInputElement input = e.TargetNode as GeckoInputElement;

                    if (input != null)
                    {
                        MenuItem menuitem = new MenuItem("Вставить");

                        menuitem.Click += delegate
                            {
                                int begin = input.SelectionStart;
                                int end = input.SelectionEnd;

                                string cur = input.Value;
                                string beginStr =  begin > 0 ? cur.Substring(0, begin): "";
                                string endStr = end < cur.Length ? cur.Substring(end) : "";

                                string newStr = beginStr + Clipboard.GetText() + endStr;
                                input.Value = newStr;
                        };

                        e.ContextMenu.MenuItems.Add(0, menuitem);
                    }
                }
            }
        }

        private void gwb_Navigated(object sender, GeckoNavigatedEventArgs e)
        {
            //const string confirm = "http://beta.mcruises.ru/confirm/";

            string uri = e.Uri.ToString();

            /*
            if (uri.IndexOf(_aviaError, StringComparison.Ordinal) > -1)
            {
                //uri.IndexOf(" ")
            }
            */

            if (uri.IndexOf(_aviaConfirm, StringComparison.Ordinal) > -1)
            {
                GetData(false, false, true);
                AddingAviaServiceDisable();
                if (_webBrowserIsExpand) WebBrowserToPanel();
                tcPartnerPutevkaTest.SelectTab(tcpPutevka);
            }
            //tbAdress.Text = _gwb.Url.ToString();
            SetAddressText(_gwb.Url.ToString());
        }

        private void gwb_Navigated_T(object sender, GeckoNavigatedEventArgs e)
        {
            //const string confirm = "http://beta.mcruises.ru/confirm/";

            string uri = e.Uri.ToString();

            /*
            if (uri.IndexOf(_transferError, StringComparison.Ordinal) > -1)
            {
                //uri.IndexOf(" ")
            }
            */

            int i1 = uri.IndexOf(_transferConfirm, StringComparison.Ordinal);
            int i2 = uri.IndexOf("cabinet", StringComparison.Ordinal);

            if (i1 > -1 || i2 > -1)
            {
                GetData(false, false, true); // need to acord to transfer
                AddingTransferServiceDisable();
                if (_webBrowserIsExpand) WebBrowserToPanel();
                tcPartnerPutevkaTest.SelectTab(tcpPutevka);
            }
            //tbAdress.Text = _gwb.Url.ToString();
            SetAddressText(_gwb.Url.ToString());
        }

        private void gwb_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {
            //const string confirm = "http://beta.mcruises.ru/confirm/";
            if (e.Uri.IndexOf(_aviaConfirm, StringComparison.Ordinal) > -1)
            {
                GetData(false, false);
                _gwb.Navigate(e.Uri);
                e.Cancel = true;
                AddingAviaServiceDisable();
            }
        }

        #endregion

        private void OnVaucerServiceSelected(Service selected)
        {
            _vaucerServiceSelected = selected;
            //_dogovorSettingControl.SetService(selected);
        }

        private void OnServiceMouseTask()
        {
            string service_name = "<имя сервиса>";
            if (_vaucerServiceSelected != null) service_name = _vaucerServiceSelected.ServiceName;

            MessageBox.Show("Команды для сервиса " + service_name + "...");
        }

        private void OnServiceEditClick(int key_for_serch)
        {
            Config_XML conf = new Config_XML();
            string partner_list = conf.Get_Value("appSettings", "InsPartnersKeys");
            if (string.IsNullOrEmpty(partner_list)) partner_list = "55166"; // 51457,51458,52106 - URALSIB

            string[] partners = partner_list.Split(',');
            bool bAlfaFound = false;
            bool bNonAlfaFound = false;
            foreach (Service service in _voucherViewModel.Voucher.InshurServiceList)
            {
                if (service.PartnerKey > 0)
                {
                    string partner_key = service.PartnerKey.ToString();
                    for (int i = 0; i < partners.Length; i++)
                    {
                        if (string.Compare(partners[i], partner_key) == 0)
                            bAlfaFound = true;
                        else
                            bNonAlfaFound = true;
                    }
                }
            }
            string msg = "Страховки УРАЛСИБа обрабатываются в прежнем режиме.";
            if (bNonAlfaFound)
            {
                if (bAlfaFound) msg = msg + (char)13 + (char)10 + "В списке есть услуги от разных партнеров.";
                MessageBox.Show(msg, "Сообщение", MessageBoxButtons.OK);
                return;
            }

            //string service_name = "<имя сервиса>";
            //if (_vaucerServiceSelected != null) service_name = _vaucerServiceSelected.ServiceName;

            //MessageBox.Show("Команды для редактирования сервиса " + service_name + "...");

            int w = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int top = 250;

            if (key_for_serch == 7)
            {
                bool bPartner = false;
                if (_access.isBronir || _access.isSuperViser) bPartner = true;
//bPartner = false;

                frmIntro intro = new frmIntro(""); 
                intro.Show();
                intro.Refresh();

                _voucherViewModel.Voucher.TourDate = _tourDate;
                _voucherViewModel.SetPartnerFlag(bPartner);

                frmNewOptionsEditInsurance frmInsEditor = new frmNewOptionsEditInsurance(_voucherViewModel, UpdateInsData); //@
                //frmInsEditor.InitPosition(w - frmInsEditor.Width - 50, top);
                if (intro != null) intro.Close();
                frmInsEditor.ShowDialog();

                return;
            }

            frmNewOptionsEditCruise frmEditor = new frmNewOptionsEditCruise(_voucherViewModel);
            frmEditor.InitPosition(w - frmEditor.Width - 50, top);
            frmEditor.ShowDialog();
        }

        private void GenerateBlok1ForVoucher(bool showAviaError = false, ServiceType serviceType = ServiceType.Unknow)
        {
            bool bPartner = false;
            if (_access.isBronir || _access.isSuperViser) bPartner = true;

            Voucher.ErrorBronCallback aviaBronErrorCallback = showAviaError ?
                AviaBronError : (Voucher.ErrorBronCallback) null;

            _voucher = new Voucher(_dgCode, bPartner, aviaBronErrorCallback);

            _voucherViewModel.Voucher = _voucher;
            _aviaTabViewModel.Services = new ObservableCollection<CaruselData>(_voucher.AviaServiceList);
            _visaTabViewModel.Services = new ObservableCollection<CaruselData>(_voucher.VisaServiceList);
            _hotelTabViewModel.Services = new ObservableCollection<CaruselData>(_voucher.HotelServiceList);
            _inshurTabViewModel.Services = new ObservableCollection<CaruselData>(_voucher.InshurServiceList);
            foreach (CaruselData data in _inshurTabViewModel.Services)
            {
                InshurService iservice = (InshurService)data;
                int n = iservice.Number;
                
            }
            _transferTabViewModel.Services = new ObservableCollection<CaruselData>(_voucher.TransferServiceList);
            _otherServiceTabViewModel.Services = new ObservableCollection<CaruselData>(_voucher.OtherServiceList);
            if (_cruiseMode != CruiseTab.Old)
            {
                _cruiseServiceTabViewModel.Services = new ObservableCollection<CaruselData>(_voucher.CruiseServiceList.Where(s => s.BeginClass || s.MidClass));
                foreach (Service srv in _voucher.CruiseServiceList)
                {
                    if (srv.GetType() == typeof(CruiseService))
                    {
                        _cruiseService = (CruiseService)srv;
                        CruiseService cs = (CruiseService)srv;
                        CruiseCurrency = cs.Rate;
                        if (_voucherViewModel != null)
                            _voucherViewModel.ServiceCurrency = CruiseCurrency;

                        if (cs.Bonuses != null)
                        {
                            GenerateBonusesList(cs.Bonuses);
                        }
                        if (cs.Services != null)
                        {
                            GenerateFoodList(cs.Services);
                        }
                        GeneratePassengersList(cs);
                    }
                }
            }
            if (_gui != Gui.Old)NewServiceSelectorInit();

            switch (serviceType)
            {
                case ServiceType.Cruise:

                    if (_cruiseMode == CruiseTab.New)
                    {
                        _cruiseServiceTabViewModel.InvokeSelectedEvent();
                    }
                    else
                    {
                        if (_dogovorSettingControl != null) SetService(ServiceInfo.GetServiceInfo(_dogovorSettingControl.Service));
                        //if (_billSettingControl != null) SetService(ServiceInfo.GetServiceInfo(_billSettingControl.Service));
                        if (_billSetting2Control != null) SetService(ServiceInfo.GetServiceInfo(_billSetting2Control.Service));
                    }
                    break;
                case ServiceType.Avia:
                    _aviaTabViewModel.InvokeSelectedEvent();
                    break;
                case ServiceType.Visa:
                    _visaTabViewModel.InvokeSelectedEvent();
                    break;
                case ServiceType.Hotel:
                    _hotelTabViewModel.InvokeSelectedEvent();
                    break;
                case ServiceType.Inshur:
                    _inshurTabViewModel.InvokeSelectedEvent();
                    break;
                case ServiceType.Transfer:
                    _transferTabViewModel.InvokeSelectedEvent();
                    break;
                case ServiceType.Unknow:
                    _otherServiceTabViewModel.InvokeSelectedEvent();
                    break;
                default:
                    if (_dogovorSettingControl != null) SetService(ServiceInfo.GetServiceInfo(_dogovorSettingControl.Service));
                    //if (_billSettingControl != null) SetService(ServiceInfo.GetServiceInfo(_billSettingControl.Service));
                    if (_billSetting2Control != null) SetService(ServiceInfo.GetServiceInfo(_billSetting2Control.Service));
                    break;
            }
        }

        private void AviaBronError(Flight flight)
        {
            MessageBox.Show("Из-за технической ошибки опция не создалась в базе поставщика авиауслуг.\n" +
                        " Повторите бронирование по данным параметрам или измените параметры.",
                        "Ошибка бронирования", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Заполнение блока туристов
        /// </summary>
        private void GenerateBlockToTourist()
        {
            //tableLayoutPanel1.SuspendLayout();
            using (SqlDataAdapter adapter = new SqlDataAdapter(WorkWithData.selectTurists, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", _dgCode);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                _touristViewModel.TouristList = TouristService.GetTouristList(dt);

                int i = 1;
                //tlpTurists.SuspendLayout();
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

                GenerateTouristList(dt);
            }
            tlpTurists.Height = (int.Parse(tbPax.Text) + 1) * 30;
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
                panel10.Height = (int.Parse(tbPax.Text) + 1) * 30;
                tableLayoutPanel1.RowStyles[3].Height = (int.Parse(tbPax.Text) + 1) * 30;
            }
            //tlpTurists.ResumeLayout();
            //tableLayoutPanel1.ResumeLayout();
        }

        /*
        private void CreateContextMenuForOptionButton()
        {
            btnFlight.Text = FlightBtnStr;
            ToolStripMenuItem flightMenuItem = new ToolStripMenuItem(FlightStr);
            ToolStripMenuItem otherMenuItem = new ToolStripMenuItem(OtherStr);
            contextMenuStrip1.Items.AddRange(new[] { flightMenuItem, otherMenuItem });
            btnFlight.ContextMenuStrip = contextMenuStrip1;
            flightMenuItem.Click += (s, e) => ShowFligth();
            //otherMenuItem.Click += (s, e) => btnFlight.Text = OtherStr;
        }*/

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
            //tcPartnerPutevkaTest.TabPages.Remove(tcpPartner);
            tcPartnerPutevkaTest.TabPages.Remove(tcpPartner);
            //tcpPartner.Dispose();

            /*tcPartnerPutevkaTest.TabPages.Remove(tcpPutevka);
            tcpPutevka.Dispose();*/
            
            tlpDataOfBronCruise.Enabled = false;
            tlpBonusCruise.Enabled = false;
            btnRegion.Visible = false;
        }

        private void SetBronir()
        {
            tlpDataOfBronCruise.Enabled = true;
            tlpBonusCruise.Enabled = true;
            btnRegion.Visible = false;
        }

        #endregion
        
        private void ShowPartnerTab()
        {
            tcPartnerPutevkaTest.TabPages.Clear();
            tcPartnerPutevkaTest.TabPages.Add(tcpPartner);
            tcPartnerPutevkaTest.TabPages.Add(tcpPutevka);
        }

        private void GenerateBonusesList(ObservableCollection<string> bonuses_list)
        {
            //Заполнение бонусов
            if (_bonusesList == null) _bonusesList = new List<NameValue>();
            
            _bonusesList.Clear();

            ObservableCollection<string> bonuses = bonuses_list;
            if (bonuses != null)
            {
                foreach (string bonus in bonuses)
                {
                    _bonusesList.Add(new NameValue("Бонус", bonus));
                }
            }
        }

        private void GenerateFoodList(ObservableCollection<NameValue> food_list)
        {
            //Заполнение питания
            if (_foodList == null) _foodList = new List<NameValue>();

            _foodList.Clear();

            ObservableCollection<NameValue> food = food_list;
            if (food != null)
            {
                foreach (NameValue srv in food)
                {
                    if (srv.Name.ToString().IndexOf("Вариант") > -1 && srv.Name.ToString().IndexOf("питан") > -1) // Вариант питания
                        _foodList.Add(new NameValue(srv.Name, srv.Value));
                }
            }
            //_foodList.Add(new NameValue("Вариант питания", "Вариант питания - Содержание"));
        }

        private void GeneratePassengersList(CruiseService cs)
        {
            cs.Passengers = "";

            //Заполнение пассажиров
            if (cs != null)
            {
                if (cs.CruiseTouristInfo != null)
                {
                    cs.CruiseTouristInfo.Clear();
                    if (_touristList != null)
                    {
                        if (_touristList.Count > 0)
                        {
                            int pas = _touristList.Count;
                            string comm = "пассажиров";
                            int p = pas;
                            if (p > 10) p = p - 10;
                            if (p > 10) p = p - 10;
                            if (p > 10) p = p - 10;
                            if (p == 1) comm = "пассажир";
                            if (p == 2 || p == 3 || p == 4) comm = "пассажира";
                            cs.Passengers = pas.ToString() + " " + comm;

                            int count = 0;
                            foreach (NameValue tourist in _touristList)
                            {
                                count++;
                                //cs.CruiseTouristInfo.Add(new NameValue(count.ToString() + ". " + tourist.Name, tourist.Value));
                                cs.CruiseTouristInfo.Add(new NameValue(tourist.Name, tourist.Value));
                            }
                        }
                    }
                }
            }
        }

        private void GenerateTouristList(DataTable dt)
        {
            //Заполнение пассажиров
            if (_touristList == null) _touristList = new List<NameValue>();
            if (_touristKeyList == null) _touristKeyList = new List<NameValue>();

            _touristList.Clear();
            _touristKeyList.Clear();

            if (dt != null)
            {
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    count++;
                    string name = row.Field<string>("TU_NAMELAT");
                    string fname = row.Field<string>("TU_FNAMELAT");
                    string sname = row.Field<string>("TU_SNAMELAT");
                    string birthday = row.Field<string>("TU_BIRTHDAY");
                    int tu_key = row.Field<int>("TU_KEY");
                    string tu_name = count.ToString() + ". " + name + " " + fname;

                    _touristList.Add(new NameValue(tu_name, birthday));
                    _touristKeyList.Add(new NameValue(tu_name, tu_key.ToString()));
                }
            }
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        private void GetData(bool showWeb = true, bool updateTourists = true, bool showAviaError = false)
        {
            try
            {
                Do_GetData(showWeb, updateTourists, showAviaError);
            }
            catch (System.Exception ex)
            {
                TpLogger.Debug("frmNewOptions.GetData >> ", ex);
            }
        }

        private void Do_GetData(bool showWeb = true, bool updateTourists = true, bool showAviaError = false)
        {
            //Заполнение второй строки
            tbDGcod.Text = _dgCode;
            //tcPartnerPutevkaTest.SelectTab(_access.isBron ? tcpPartner : tcpPutevka);

            string mainmen = "";
            string mainmenphone = "";
            string mainmenemail = "";
            // rbPutevka.Checked = true;
            int broner_key = 0;
            int realiz_key = 0;
            string broner_name = "";
            string realiz_name = "";
             
            //string query = @"select DG_CODE,DG_NMEN,DG_MAINMEN,DG_MAINMENPHONE,DG_MAINMENEMAIL,DG_TURDATE,DG_NDAY,DG_PARTNERKEY  from  dbo.tbl_Dogovor where DG_CODE=@dgcode";
            string main_query = @"select DG_CODE,DG_NMEN,DG_MAINMEN,DG_MAINMENPHONE,DG_MAINMENEMAIL,DG_TURDATE,DG_NDAY,DG_PARTNERKEY,DG_RATE,";
            main_query = main_query + " IsNull(broniro.US_KEY, 0) as bron_key, broniro.US_FullNameLat as bronir,";
            main_query = main_query + " IsNull(MANAGER.US_KEY, 0) as manag_key, MANAGER.US_FullNameLat as manag";
            main_query = main_query + " from  dbo.tbl_Dogovor with (nolock)";
            main_query = main_query + " Left join mk_DogovorAdd with (nolock) on dg_code COLLATE Cyrillic_General_CS_AS = da_dgcode COLLATE Cyrillic_General_CS_AS";
            main_query = main_query + " Left join UserList as broniro with (nolock) on da_bron  = broniro.us_key";
            main_query = main_query + " left join UserList as manager with (nolock) on dg_owner = manager.us_key";
            main_query = main_query + " where DG_CODE=@dgcode";

            using (var adapter = new SqlDataAdapter(main_query, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", _dgCode);
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    _tourDate = dt.Rows[0].Field<DateTime>("DG_TURDATE");

                    tbDateBegin.Text = dt.Rows[0].Field<DateTime>("DG_TURDATE").Date.ToString("dd MMMM yy");
                    tbDateEnd.Text =
                        dt.Rows[0].Field<DateTime>("DG_TURDATE")
                            .AddDays((int) dt.Rows[0].Field<double>("DG_NDAY") - 1)
                            .Date.ToString("dd MMMM yy");
                    tbPax.Text = dt.Rows[0].Field<Int16>("DG_NMEN").ToString();
                    _agencyKey = dt.Rows[0].Field<int>("DG_PARTNERKEY");
                    mainmen = dt.Rows[0].Field<String>("DG_MAINMEN");
                    mainmenphone = dt.Rows[0].Field<String>("DG_MAINMENPHONE");
                    mainmenemail = dt.Rows[0].Field<String>("DG_MAINMENEMAIL");
                    _dgRate = dt.Rows[0].Field<String>("DG_RATE");
                     
                    broner_key = dt.Rows[0].Field<int>("bron_key");
                    realiz_key = dt.Rows[0].Field<int>("manag_key");
                    broner_name = dt.Rows[0].Field<String>("bronir");
                    realiz_name = dt.Rows[0].Field<String>("manag");
                    if (broner_name == null) broner_name = "";
                    if (realiz_name == null) realiz_name = "";
                }
            }

            _sailDate = _tourDate;

            string sail_query = @"SELECT [DL_DATEBEG] FROM [dbo].[tbl_DogovorList] WHERE DL_DGCOD = @dgcode AND (DL_SVKEY = 3143 OR DL_SVKEY = 3)";
            using (var adapter =
                new SqlDataAdapter(sail_query, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", _dgCode);
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    _sailDate = dt.Rows[0].Field<DateTime>("DL_DATEBEG");
                }
            }

            // контактные данные для вывода в окне "Контакты"
            if (_contactsList == null) _contactsList = new List<NameValue>();
            _contactsList.Clear();

            if (_agencyKey == 0) // прямой покупатель - не агент
            {
                tbBuyer.Text = mainmen;
                _contactsList.Add(new NameValue("Покупатель", mainmen));
                _contactsList.Add(new NameValue("Договор", _dgCode));
                _contactsList.Add(new NameValue("E-mail", mainmenemail));
                _contactsList.Add(new NameValue("Тел.", mainmenphone));
            }
            else // покупатель - агент 
            {
                string pr_name = "";
                string pr_dogovor = "";
                string pr_boss = "";
                string pr_bossname = "";
                string pr_phones = "";
                string pr_fax = "";
                string pr_email = "";
                
                using (var adapter =
                    new SqlDataAdapter(
                        @"select PR_NAME,PR_BOSS,PR_BOSSNAME,PR_PHONES,PR_FAX,PR_EMAIL  from  dbo.tbl_Partners where PR_KEY=@prkey",
                        WorkWithData.Connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@prkey", _agencyKey);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        pr_name = dt.Rows[0].Field<String>("PR_NAME");
                        pr_boss = dt.Rows[0].Field<String>("PR_BOSS");
                        pr_bossname = dt.Rows[0].Field<String>("PR_BOSSNAME");
                        pr_phones = dt.Rows[0].Field<String>("PR_PHONES");
                        pr_fax = dt.Rows[0].Field<String>("PR_FAX");
                        pr_email = dt.Rows[0].Field<String>("PR_EMAIL");
                    }
                }

                // номер договора для окна контактов
                string query = "SELECT pdg.PD_PRKey, pdg.PD_DogNumber, pdg.pd_Datebeg, pdg.pd_dateend ";
                query = query + " FROM prtDogs AS pdg ";
                query = query + " WHERE pdg.pd_key IN ";
                query = query + " (SELECT max(pd.pd_key) FROM ";
                query = query + "   (SELECT * FROM prtDogs ";
                query = query + "     WHERE PD_PRKey = @prkey ";
                query = query + "   )  AS pd ";
                query = query + "   INNER JOIN PrtDogTypes as pdt with (nolock) on pd.PD_DogType=pdt.PDT_ID ";
                query = query + "   WHERE (pdt.PDT_Name like 'Агентский%' or pdt.PDT_Name like 'Договор с сетью' or pdt.PDT_Name like 'для нестандартных договоров') ";
                query = query + "   group by pd.PD_PRKey)";
                using (var adapter =
                    new SqlDataAdapter(query, WorkWithData.Connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@prkey", _agencyKey);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        pr_dogovor = dt.Rows[0].Field<String>("PD_DogNumber");
                        pr_dogovor = pr_dogovor + "   от " + dt.Rows[0].Field<DateTime>("pd_Datebeg").Date.ToString("dd MMMM yyyy");
                    }
                }

                // заполнение списка полей для окна контактов
                tbBuyer.Text = pr_name;
                _contactsList.Add(new NameValue("Покупатель", pr_name));
                _contactsList.Add(new NameValue("Договор", pr_dogovor));
                //_contactsList.Add(new NameValue("Руководитель группы", "Иванов Иван Иванович"));
                if (pr_boss.Length > 0)
                    _contactsList.Add(new NameValue("Должность руководителя", pr_boss));
                _contactsList.Add(new NameValue("Имя руководителя", pr_bossname));
                //_contactsList.Add(new NameValue("Тел1.", "+78452277620"));
                //_contactsList.Add(new NameValue("Тел2.", "+78452618230"));
                _contactsList.Add(new NameValue("Телефоны", pr_phones));
                if (pr_fax.Length > 0)
                    _contactsList.Add(new NameValue("Факс", pr_fax));
                _contactsList.Add(new NameValue("E-mail", pr_email));
                if (pr_bossname.Length > 0)
                    _contactsList.Add(new NameValue("Контактное лицо", pr_bossname));
                if (pr_phones.Length > 0)
                    _contactsList.Add(new NameValue("Тел.", pr_phones));
            }   

            // Бронировщик и реализатор
            lbl_RealizName.Text = realiz_name;
            lbl_BronirName.Text = broner_name;
            BronirKey = broner_key;
            RealizKey = realiz_key;
             
            // списки значений для выпадающих окон
            // Тип кровати
            lst_Names_TK = new List<NameValue>();
            lst_Names_TK.Add(new NameValue(1, "1 большая двухместная кровать"));
            lst_Names_TK.Add(new NameValue(2, "1 большая кровать"));
            lst_Names_TK.Add(new NameValue(3, "1 двуспальная кровать"));
            lst_Names_TK.Add(new NameValue(4, "1 кровать"));
            lst_Names_TK.Add(new NameValue(5, "2 отдельные кровати"));
            lst_Names_TK.Add(new NameValue(6, "2 кровати вместе"));
            lst_Names_TK.Add(new NameValue(7, "2 односпальные кровати"));
            lst_Names_TK.Add(new NameValue(8, "2 кровати"));
            lst_Names_TK.Add(new NameValue(9, "2-х ярусная"));
            lst_Names_TK.Add(new NameValue(10, "2-х ярусные кровати"));
            lst_Names_TK.Add(new NameValue(11, "King size"));
            lst_Names_TK.Add(new NameValue(12, "Железная"));
            lst_Names_TK.Add(new NameValue(13, "Кровати стоят вместе"));
            lst_Names_TK.Add(new NameValue(14, "Кровати стоят отдельно"));
            lst_Names_TK.Add(new NameValue(15, "Не могу определиться"));

            // Вариант питания
            lst_Names_VP = new List<NameValue>();
            string vp_qry = "SELECT [id],[pansion],[order_pans],[code_pans] FROM [total_services].[dbo].[Type_pansion]";
            using (var adapter = new SqlDataAdapter(vp_qry, WorkWithData.ConnectionTS))
            {
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        int id = dt.Rows[i].Field<int>("id");
                        string name = dt.Rows[i].Field<string>("pansion");
                        lst_Names_VP.Add(new NameValue(id, name));
                    }
                }
            }
            // Тип питания - загружается при открытии круиза по данным путевки
            lst_Names_TP = new List<NameValue>();
            lst_Names_TP.Add(new NameValue(1, "Тип питания..."));
            // Вид каюты
            lst_Names_VK = new List<NameValue>();
            lst_Names_VK.Add(new NameValue(1, "Вид каюты..."));
            // Круизный тариф
            lst_Names_KT = new List<NameValue>();
            lst_Names_KT.Add(new NameValue(1, "Круизный тариф..."));

            // список дополнительных услуг - названия и коды
            //lst_DopServices = new List<NameValue>();
            lst_DopServices = new List<DopServiceItem>();
            string dop_qry = "SELECT [CDS_ID],[CDP_NAME],[CDP_ORDER],[CDP_BLOCK],[CDP_VISIBLE_LK],isnull([CDP_QWERY],'') AS [CDP_QWERY],isnull([CDP_IS_default],0) AS [CDP_IS_default] FROM [dbo].[MK_Cruise_DOPServise] ORDER BY CDS_ID DESC";
            using (var adapter = new SqlDataAdapter(dop_qry, WorkWithData.Connection))
            {
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        int id = dt.Rows[i].Field<int>("CDS_ID");
                        string name = dt.Rows[i].Field<string>("CDP_NAME");
                        string qry = dt.Rows[i].Field<string>("CDP_QWERY");
                        int isdefault = dt.Rows[i].Field<int>("CDP_IS_default");
                        //lst_DopServices.Add(new NameValue(id, name));
                        DopServiceItem item = new DopServiceItem(id, name, qry, isdefault);
                        lst_DopServices.Add(item);
                    }
                }
            }

            // get additional data
            using (var adapter =
                new SqlDataAdapter(
                    @"SELECT [Pacage], [shipcode] FROM dbo.mk_DogovorListAdd WHERE DG_CODE=@dgcode",
                    WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", _dgCode);
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    _shipCode = dt.Rows[0].Field<string>("shipCode");
                    _package = dt.Rows[0].Field<string>("pacage");
                }
            }

if (!String.IsNullOrEmpty(_package) && _sailDate != null && !String.IsNullOrEmpty(_shipCode))
{
            using (var adapter =
                new SqlDataAdapter(
                    @"SELECT is_transfer from [dbo].[CRUISES_serch] WHERE [package]=@package and sailDate=@saildate and shipCode=@shipcode",
                    WorkWithData.ConnectionTS))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@package", _package);
                adapter.SelectCommand.Parameters.AddWithValue("@saildate", _sailDate);
                adapter.SelectCommand.Parameters.AddWithValue("@shipcode", _shipCode);
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    _transferFlag = (dt.Rows[0].Field<int>("is_transfer") > 0);
                }
            }
}

            if (_agencyKey != 0)
                btnDogovor.Visible = false;

            //Заполнение туристов
            if (updateTourists)
                GenerateBlockToTourist();

            //Заполнение услуг/потверждено /не подтверждно
            _servisesmas.Clear();

            using (var com = new SqlCommand("MK_lk_servises_putevka", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dg_code", _dgCode);
                var adapter = new SqlDataAdapter(com);
                adapter.Fill(_servisesmas);
            }

            DataRow[] port = _servisesmas.Select("tipe=1 and order<>1");

            foreach (DataRow row in port)
                _servisesmas.Rows.Remove(row);

            int ob = _servisesmas.Rows.Count;
            int pod = _servisesmas.Select("DL_CONTROL=0").Count();
            int nepod = ob - pod;

            tbUslug.Text = ob.ToString();
            tbAccept.Text = pod.ToString();
            tbNoAccept.Text = nepod.ToString();

            if (_dogovorSettingControl != null)
                _dogovorSettingControl.Dispose();

            //if (_billSettingControl != null)
            //    _billSettingControl.Dispose();

            if (_billSetting2Control != null)
                _billSetting2Control.Dispose();

            tcpSettings.Controls.Clear();
            tcpBillSettings.Controls.Clear();

            _dogovorSettingControl = new ucDogovorSetting(GenerateBlok1ForVoucher, _voucherViewModel)
            {
                Location = new Point(0, 0)
            };

            if (_dogovorSettingControl is Control)
            {
                tcpSettings.Controls.Add((Control) _dogovorSettingControl);
                ((Control) _dogovorSettingControl).Dock = DockStyle.Fill;
            }

            //_billSettingControl = new ucBillSetting(GenerateBlok1ForVoucher, _voucherViewModel, _dgCode, ReloadForm)
            //{
            //    Location = new Point(0, 0)
            //};

            //if (_billSettingControl is Control)
            //{
            //    tcpBillSettings.Controls.Add((Control)_billSettingControl);
            //    ((Control)_billSettingControl).Dock = DockStyle.Fill;
            //}

            _billSetting2Control = new ucBillSetting2(GenerateBlok1ForVoucher, _voucherViewModel, _dgCode, _dgRate, _access.userKey, ReloadForm)
            {
                Location = new Point(0, 0)
            };

            if (_billSetting2Control is Control)
            {
                tcpBillSettings.Controls.Add((Control)_billSetting2Control);
                ((Control)_billSetting2Control).Dock = DockStyle.Fill;
            }

            GenerateBlok1ForVoucher(showAviaError);

            _dogovorSettingControl.SetDogovorSetting();
            //_billSettingControl.SetBillSetting();
            _billSetting2Control.SetBillSetting();

            GetOptionTabControl();

            // Заполнение состава путевки и проблем\изменений
            //_DogovorList = WorkWithData.GetDogovorList(_dgCode);

            _problems = WorkWithData.GetAllProblems(_dgCode);
            if (_problems.Rows.Count < 1)
                tcProblemChanges.TabPages.Remove(tcpProblem);

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
                tcProblemChanges.TabPages.Remove(tcpMessageNew);
                tcProblemChanges.TabPages.Remove(tcpSettings);
                tcProblemChanges.TabPages.Remove(tcpItinerary);
                _oldInternaryPage = new TabPage()
                {
                    Text = "Старое расписание",
                    Name = "tcpOldItinerary",
                    TabIndex = itineraryindex

                };
                tcProblemChanges.TabPages.Add(_oldInternaryPage);

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
                tcProblemChanges.TabPages.Add(tcpMessageNew);
            }

            //Заполнение маршрута
            if (_cruises.Count == 0)
                tcProblemChanges.TabPages.Remove(tcpItinerary);

            //updateDataGrid();
            //updateDataGridPutevka();
            //проверка на новые сообщения 
            int manager_messages = 0;
            int client_messages = 0;
            bool bNew = WorkWithData.CountNewMessage(_dgCode, ref manager_messages, ref client_messages);
            //if (WorkWithData.IsNewMessage(_dgCode))
            if (bNew)
            {
                string title = "Необработанная переписка";
                if (client_messages > 0)
                    title = title + " с клиентом(" + client_messages.ToString() + ")";
                if (manager_messages > 0)
                    title = title + " с сотрудником(" + manager_messages.ToString() + ")";
                tcpMessageNew.Text = title;
            }
            else
            {
                tcpMessageNew.Text = "Переписка";
            }

            //Обработка аннуляции по путевку
            if (WorkWithData.IsAnnulation(_dgCode))
            {
                _annulationTabs = new List<TabPage>();
                DataTable annulTable = WorkWithData.GetAnnulationByDogovor(_dgCode);
                tcProblemChanges.TabPages.Remove(tcpMessageNew);
                //tcpMessageNew.Dispose();
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
                    _annulationTabs.Add(tcp);
                }
                tcProblemChanges.TabPages.Add(tcpMessageNew);
            }

            if (showAviaError)
            {
                tcOptions.SelectTab(tcpAllVaucher);
                _dogovorSettingControl.SetVoucher(_voucher.GeneralServiceList.Count);
            }
        }

        private Color GetColor(string hex)
        {
            return ColorTranslator.FromHtml("#FF" + hex.Remove(0, 1));
        }

        private Color GetColor(string tabName, Config_XML config)
        {
            try
            {
                string hex = config.Get_Value("tabColors", tabName);
                return GetColor(hex);
            }
            catch(Exception e)
            {
                return Color.White;
            }
        }

        private void GetOptionTabControl()
        {
            int selectedTabIndext = tcOptions.SelectedIndex;
            tcOptions.SuspendLayout();
            _optionTabControl = new OptionTabControl(tcOptions, ChangeCurrentService, _allTabPages);

            Config_XML conf = new Config_XML();

            // Круизы

            OptionTabPage cruises;
            
            cruises = _cruiseMode == CruiseTab.New ? 
                GetTabPage(ServiceType.Cruise, "tipe=1 and order=1", tcpCruiseNew, null) : 
                GetTabPage(ServiceType.Cruise, "tipe=1 and order=1", tcpCruise, cbCruises);

            if (_cruiseMode == CruiseTab.Both || _cruiseMode == CruiseTab.New) cruises.ServiceTabViewModel = _cruiseServiceTabViewModel;
            _cruises = cruises.ServiceList;

            // Доппакеты
            //var dopPakets = GetTabPage(ServiceType.DopPaket, "tl_tip=149 or tl_tip=172", tcpDopPaket, cbDopPaket);
            //_dopPakets = dopPakets.ServiceList;

            // Отели
            var hotels = GetTabPage(ServiceType.Hotel, "tipe=6", tcpHotelNew, null);
            hotels.ServiceTabViewModel = _hotelTabViewModel;
            //_hotels = hotels.ServiceList;

            // Визы
            List<ServiceInfo> visaServiceList = new List<ServiceInfo>();
            foreach (var visaService in _voucher.VisaServiceList)
                visaServiceList.Add(ServiceInfo.GetServiceInfo(visaService));
            
            var vises = GetTabPage(ServiceType.Visa, visaServiceList, tcpVisaNew, null);
            vises.ServiceTabViewModel = _visaTabViewModel;
            //_visa = vises.ServiceList;
            string title = CutCounter(vises.TabPage.Text);
            if (_visaTabViewModel.Services.Count > 0)
                title = title + " (" + _visaTabViewModel.Services.Count.ToString() + ")";
            vises.TabPage.Text = title;

            // Трансферы
            List<ServiceInfo> transferServiceList = new List<ServiceInfo>();
            foreach (var transferService in _voucher.TransferServiceList)
                transferServiceList.Add(ServiceInfo.GetServiceInfo(transferService));

            var transfers = GetTabPage(ServiceType.Transfer, transferServiceList, tcpTransfers, null); //"dl_svkey in (2,3177)"
            transfers.ServiceTabViewModel = _transferTabViewModel;
            title = CutCounter(transfers.TabPage.Text);
            if (_transferTabViewModel.Services.Count > 0)
            {
                int count = _transferTabViewModel.Services.Count;
                foreach (CaruselData cd in _transferTabViewModel.Services)
                {
                    TransferService tsc = (TransferService)cd;
                    if (tsc.Transfers != null)
                        if (tsc.Transfers.Count > count) count = tsc.Transfers.Count;
                }
                if (count > 0)
                    title = title + " (" + count.ToString() + ")";
            }
            transfers.TabPage.Text = title;

            // Страховки
            var inshur = GetTabPage(ServiceType.Inshur, "dl_svkey=6", tcpInshurNew, null);
            inshur.ServiceTabViewModel = _inshurTabViewModel;
            //_inshur = inshur.ServiceList;
            title = CutCounter(inshur.TabPage.Text);
            if (_inshurTabViewModel.Services.Count > 0)
                title = title + " (" + _inshurTabViewModel.Services.Count.ToString() + ")";
            inshur.TabPage.Text = title;

            // Авиа
            //List<Service> aviaServiceList = GetServiceListForAvia("dl_svkey=3184");
            List<ServiceInfo> aviaServiceList = new List<ServiceInfo>();
            foreach (var aviaService in _voucher.AviaServiceList)
                aviaServiceList.Add(ServiceInfo.GetServiceInfo(aviaService));

            if (aviaServiceList != null)
            {
                aviaServiceList = aviaServiceList
                    .Where(l => l.dl_name != null)
                    .GroupBy(x => x.BronId)
                    .Select(x => x.First())
                    .ToList();
            }

            var avia = GetTabPage(ServiceType.Avia, aviaServiceList, tcpAviaNew, null);
            avia.ServiceTabViewModel = _aviaTabViewModel;
            //_avia = avia.ServiceList;

            // Прочие

            List<ServiceInfo> otherServiceList = GetServiceList("tipe <> 1 AND dl_svkey <> 3184 AND key_for_serch <> 3", ServiceType.Unknow);

            if (otherServiceList != null)
            {
                var comparer = new ServiceInfoComparerByDlKey();
                otherServiceList = Except(otherServiceList, cruises.ServiceList, comparer);
                otherServiceList = Except(otherServiceList, avia.ServiceList, comparer);
                //otherServiceList = Except(otherServiceList, dopPakets.ServiceList, comparer);
                otherServiceList = Except(otherServiceList, hotels.ServiceList, comparer);
                otherServiceList = Except(otherServiceList, vises.ServiceList, comparer);
                otherServiceList = Except(otherServiceList, inshur.ServiceList, comparer);
                otherServiceList = Except(otherServiceList, transfers.ServiceList, comparer);
            }

            var other = GetTabPage(ServiceType.Unknow, otherServiceList, tcpOtherNew, null);
            other.ServiceTabViewModel = _otherServiceTabViewModel;
            //_other = other.ServiceList;

            _optionTabControl.Add(cruises, TabType.Cruises, cbOption_SelectedIndexChanged);
            //_optionTabControl.Add(dopPakets, TabType.DopPakets, cbOption_SelectedIndexChanged);
            _optionTabControl.Add(hotels, TabType.Hotels, cbOption_SelectedIndexChanged);
            _optionTabControl.Add(vises, TabType.Vises, cbOption_SelectedIndexChanged);
            _optionTabControl.Add(inshur, TabType.Inshur, cbOption_SelectedIndexChanged);
            _optionTabControl.Add(avia, TabType.Avia, cbOption_SelectedIndexChanged);
            _optionTabControl.Add(other, TabType.Other, cbOption_SelectedIndexChanged);
            _optionTabControl.Add(transfers, TabType.Transfer, cbOption_SelectedIndexChanged);

            _optionTabControl.SetPagesState();
            _optionTabControl.GenerateAllFirstBlocks();

            if (tcOptions.SelectedIndex != selectedTabIndext && selectedTabIndext < tcOptions.TabPages.Count)
                tcOptions.SelectedIndex = selectedTabIndext;

            tcOptions.ResumeLayout();
        }

        private string CutCounter(string text)
        {
            if (text.Length < 3) return text;
            string str = text.Substring(text.Length - 1);
            if (string.Compare(str, ")") != 0) return text;
            str = text.Substring(text.Length - 3, 1);
            if (string.Compare(str, "(") != 0) return text;

            return text.Substring(0, text.Length - 3).Trim();
        }

        #region GetServices

        private ServiceInfo GetService(DataRow r, ServiceType sType)
        {
            return new ServiceInfo((int)r["dl_key"], (string)r["dl_name"], sType);
        }

        private List<ServiceInfo> GetServiceList(string condition, ServiceType sType)
        {
            try
            {
                //var rows = _servisesmas.Select().ToArray();
                //var rows2 = _servisesmas.Select(condition).ToArray();
                //return rows.Select(r => GetService(r, sType)).ToList();

                return _servisesmas.Select(condition).Select(r => GetService(r, sType)).ToList();
            }
            catch (EvaluateException e)
            {
                throw;
                return null;
            }
        }

        private OptionTabPage GetTabPage(ServiceType sType, string condition, TabPage tabPage, ComboBox cb)
        {
            var page = new OptionTabPage(sType, tabPage, cb);
            page.ServiceList = GetServiceList(condition, sType);
            //SetTabHeader(tabPage, color);
            return page;
        }

        private OptionTabPage GetTabPage(ServiceType sType, List<ServiceInfo> serviceList, TabPage tabPage, ComboBox cb)
        {
            var page = new OptionTabPage(sType, tabPage, cb);
            page.ServiceList = serviceList;
            //SetTabHeader(tabPage, color);
            return page;
        }

        private List<ServiceInfo> Except(IEnumerable<ServiceInfo> src, IEnumerable<ServiceInfo> targ, IEqualityComparer<ServiceInfo> comparer)
        {
            if (targ != null)
                return src.Except(targ, comparer).ToList();
            return src.ToList();
        }

        #endregion

        private const string selAction = @"SELECT distinct [DL_key]
      ,[actions_id]
      ,[Text]
      ,[isBonus]
  FROM [dbo].[mk_actions_options]
  where actions_id >0 and  DL_key =@dlkey and  isBonus= @bon";

        private void generateBlock1ToCruise(int dlKey, bool showWeb = true)
        {
            //TpLogger.Logger.Debug("--- generateBlock1ToCruise");
            TpLogger.StartWatch();

            tlpCruiseBl1.SuspendLayout();

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
                turdate = dt.Rows[0].Field<DateTime>("DL_TURDATE").AddDays(dt.Rows[0].Field<Int16>("DL_DAY") - 1);
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
                                //tbAdress.Text = adress;
                                SetAddressText(adress);

                                _optionTabControl.GetTabPage(TabType.Cruises).url = adress;

                                //GoToUrl(adress);
                                //wbBook.Source = new Uri(adress);
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
                using (SqlCommand com = new SqlCommand(@"SELECT name FROM [dbo].[CabinCategories]
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


            if (!string.IsNullOrEmpty(optionNumer))
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

            tlpCruisePrice.SuspendLayout();

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

            // списки значений для выпадающих окон
            // Тип кровати
            if (lst_Names_TK == null) lst_Names_TK = new List<NameValue>();
            //lst_Names_TK.Clear();

            // Вариант питания
            if (lst_Names_VP == null) lst_Names_VP = new List<NameValue>();
            //lst_Names_VP.Clear();
            
            // Тип питания - загружается при открытии круиза по данным путевки
            if (lst_Names_TP == null) lst_Names_TP = new List<NameValue>();
            lst_Names_TP.Clear();
            if (cabinCategory != string.Empty && cabinCategory != null)
            {
                using (SqlCommand com = new SqlCommand(@"Select * FROM get_dining_list(@id, @code)", WorkWithData.ConnectionTS))
                {
                    com.Parameters.AddWithValue("@id", id_ship);
                    com.Parameters.AddWithValue("@code", cabinCategory);
                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(com);
                    ad.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lst_Names_TP.Add(new NameValue(i + 1, dt.Rows[0].Field<string>("description_ru")));
                        }
                    }
                }
            }            

            // Вид каюты
            if (lst_Names_VK == null) lst_Names_VK = new List<NameValue>();
            //lst_Names_VK.Clear();
            //lst_Names_VK.Add(new NameValue(1, "Вид каюты..."));
            
            // Круизный тариф
            if (lst_Names_KT == null) lst_Names_KT = new List<NameValue>();
            //lst_Names_KT.Clear();
            //lst_Names_KT.Add(new NameValue(1, "Круизный тариф..."));

            // список дополнительных услуг - названия и коды
            //lst_DopServices = new List<NameValue>();
            if (lst_DopServices == null) lst_DopServices = new List<DopServiceItem>();
            for (int i = 0; i < lst_DopServices.Count; i++)
            {
                DopServiceItem item = lst_DopServices[i];

                if (item.ID == -101) // Круизный тариф
                {
                    item.TextList = lst_Names_KT;
                }
                if (item.ID == -104) // Смена питания // Тип питания
                {
                    item.TextList = lst_Names_TP;
                }
                if (item.ID == -105) // Тип питания
                {
                    item.TextList = lst_Names_TP;
                }
                if (item.ID == -106) // Тип кровати //
                {
                    item.TextList = lst_Names_TK;
                }
                if (item.ID == -110) // Вариант питания //
                {
                    item.TextList = lst_Names_VP;
                }
                //if (item.ID == -120) // Вид каюты
                //{
                //    item.TextList = lst_Names_VK;
                //}

                string qry = item.Query;

                if (!string.IsNullOrEmpty(qry))
                {
                    if (item.TextList == null) item.TextList = new List<NameValue>();
                    item.TextList.Clear();

                    using (var q_adapter = new SqlDataAdapter(qry, WorkWithData.Connection))
                    {
                        q_adapter.SelectCommand.Parameters.AddWithValue("@dlKey", dlKey); 
                        var q_dt = new DataTable();
                        q_adapter.Fill(q_dt);
                        if (q_dt.Rows.Count > 0)
                        {
                            for (int t = 0; t < q_dt.Rows.Count; t++)
                            {
                                //item.TextList.Add(new NameValue(t + 1, q_dt.Rows[t].Field<String>("ListText")));
                                item.TextList.Add(new NameValue(t + 1, q_dt.Rows[t].Field<String>(0)));
                            }
                        }
                    }
                }
            }


            string category = string.Empty
                , cabinNom = string.Empty
                , optionNumber = string.Empty;

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
                com1.Parameters.AddWithValue("@dlkey", dlKey);
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
                            cbCabinDef.SelectedIndex = cbCabinDef.Items.IndexOf(dt1.Rows[0].Field<string>("OP_LEVEL_CABIN"));
                    }

                }
            }
            _cabinCategori = category;
            tbNomberOptions.Text = optionNumber;
            tbCabinCategory.Text = category;
            tbCabinNomber.Text = cabinNom;

            /*
            tbNomberOptions.BorderStyle = NoEditableBorderStyle;
            tbNomberOptions.BackColor = NoEditableBackColor;

            tbCabinCategory.BorderStyle = NoEditableBorderStyle;
            tbCabinCategory.BackColor = NoEditableBackColor;

            tbCabinNomber.BorderStyle = NoEditableBorderStyle;
            tbCabinNomber.BackColor = NoEditableBackColor;
            */

            tlpCruiseBl1.ResumeLayout();
            tlpCruisePrice.ResumeLayout();
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

        private void spliterOtherAdd()
        {
            //tlpOther.RowCount++;
            //tlpOther.Height += 27;
            //tlpOther.RowStyles.Insert(0, new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            //tlpOther.Controls.Add(GetLabel(""));
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

        public void btnPassengers_ClickHandler(object sender)
        {
            if (_cruiseService == null) return;

            _cruiseService.TouristInfoFlag = !_cruiseService.TouristInfoFlag;

            /*
            if (frmPassengers != null)
            {
                if (frmPassengers.WindowState == FormWindowState.Normal)
                {
                    frmPassengers.Close();
                }
                if (!frmPassengers.IsDisposed) frmPassengers.Dispose();
                frmPassengers = null;
            }
            if (frmPassengers == null)
            {
                int ix = tcOptions.Left + tcOptions.Width - 100;
                int iy = Cursor.Position.Y - 40; // tcOptions.Top + tcOptions.Height;

                frmPassengers = new frmNewOptionsTourists(ix, iy, _touristList, "Туристы");
                frmPassengers.Show();
            }
            */
        }

        public void btnPrices_ClickHandler(object sender) 
        {
return;
///
            if (frmPrices != null)
            {
                if (frmPrices.WindowState == FormWindowState.Normal)
                {
                    frmPrices.Close();
                }
                if (!frmPrices.IsDisposed) frmPrices.Dispose();
                frmPrices = null;
            }

            if (frmPrices == null && !string.IsNullOrEmpty(_dgCode))
            {
                int ix = tcOptions.Left + tcOptions.Width - 100;
                int iy = Cursor.Position.Y - 300;

                try
                {
                    frmPrices = new frmNewOptionsBill(ix, iy, "Обработка  счета", _dgCode);
                    frmPrices.ShowDialog();
                }
                catch (System.Exception ex)
                {
                    TpLogger.Debug("frmNewOptionsBill", ex);
                }
            }
        }

        public void btnBill_ClickHandler(object sender) // object sender, EventArgs e)
        {
//return;
///
            if (frmBill != null)
            {
                if (frmBill.WindowState == FormWindowState.Normal)
                {
                    frmBill.Close();
                }
                if (!frmBill.IsDisposed) frmBill.Dispose();
                frmBill = null;
            }

            if (frmBill == null && !string.IsNullOrEmpty(_dgCode))
            {
                int ix = tcOptions.Left + tcOptions.Width - 100;
                int iy = Cursor.Position.Y - 300;

                try
                {
                    frmBill = new frmNewOptionsBill(ix, iy, "Обработка  счета", _dgCode);
                    frmBill.ShowDialog();
                }
                catch (System.Exception ex)
                {
                    TpLogger.Debug("frmNewOptionsBill", ex);
                }
            }
        }

        public void btnBonuses_ClickHandler(object sender) // object sender, EventArgs e)
        {
            if (frmBonuses != null)
            {
                if (frmBonuses.WindowState == FormWindowState.Normal)
                {
                    frmBonuses.Close();
                }
                if (!frmBonuses.IsDisposed) frmBonuses.Dispose();
                frmBonuses = null;
            }
            if (frmBonuses == null)
            {
                int ix = tcOptions.Left + tcOptions.Width - 100;
                int iy = Cursor.Position.Y - 40; // tcOptions.Top + tcOptions.Height;

                frmBonuses = new frmNewOptionsBonuses(ix, iy, _bonusesList, "Бонусы");
                frmBonuses.Show();
                //frmBonuses.ShowDialog();
            }
        }

        public void btnServices_ClickHandler(object sender)
        {
            if (_cruiseService == null) return;

            _cruiseService.ServiceInfoFlag = !_cruiseService.ServiceInfoFlag;
        }

        public void btnFood_ClickHandler(object sender) // object sender, EventArgs e)
        {
            if (frmBonuses != null)
            {
                if (frmBonuses.WindowState == FormWindowState.Normal)
                {
                    frmBonuses.Close();
                }
                if (!frmBonuses.IsDisposed) frmBonuses.Dispose();
                frmBonuses = null;
            }
            if (frmBonuses == null)
            {
                int ix = tcOptions.Left + tcOptions.Width - 100;
                int iy = Cursor.Position.Y - 40; // tcOptions.Top + tcOptions.Height;

                //_bonusesList = new List<NameValue>();
                //_bonusesList.Add(new NameValue("Name 1", "Value 1"));
                //_bonusesList.Add(new NameValue("Name 2", "Value 2"));
                //_bonusesList.Add(new NameValue("Name 3", "Value 3"));
                //_bonusesList.Add(new NameValue("Name 4", "Value 4"));

                frmBonuses = new frmNewOptionsBonuses(ix, iy, _foodList, "Вариант питания");
                frmBonuses.Show();
            }
        }

        public void btnPassport1_ClickHandler(object sender)
        {
            if (sender.GetType() != typeof(System.Windows.Controls.Button)) return;

            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
            string tag = btn.Tag.ToString();
            int tu_key = 0;

            foreach (NameValue tourist in _touristKeyList)
            {
                if (tag == tourist.Name.ToString())
                {
                    if (tourist.Value != null) tu_key = int.Parse(tourist.Value.ToString());
                    break;
                }
            }

            if (tu_key > 0)
            {
                frmQuestionnaire questionnaire = new frmQuestionnaire(Convert.ToInt32(tu_key), 1);
                questionnaire.Show();
            }
        }

        public void btnPassport2_ClickHandler(object sender)
        {
            if (sender.GetType() != typeof(System.Windows.Controls.Button)) return;

            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
            string tag = btn.Tag.ToString();
            int tu_key = 0;

            foreach (NameValue tourist in _touristKeyList)
            {
                if (tag == tourist.Name.ToString())
                {
                    if (tourist.Value != null) tu_key = int.Parse(tourist.Value.ToString());
                    break;
                }
            }

            if (tu_key > 0)
            {
                frmQuestionnaire questionnaire = new frmQuestionnaire(Convert.ToInt32(tu_key), 0);
                questionnaire.Show();
            }
        }

        public static T FindChild<T>(System.Windows.DependencyObject parent, string childName)
           where T : System.Windows.DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as System.Windows.FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        private int GetDopServiceID(string service_name)
        {
            if (lst_DopServices == null) return 0;
            if (lst_DopServices.Count == 0) return 0;

            for (int i = 0; i < lst_DopServices.Count; i++)
            {
                //NameValue row = lst_DopServices[i];
                //if (row.Value.ToString() == service_name)
                //{
                //    return (int)row.Name;
                //}
                DopServiceItem item = lst_DopServices[i];
                if (item.Name == service_name)
                {
                    return item.ID;
                }
            }

            return 0;
        }

        private DopServiceItem GetDopServiceItem(string service_name)
        {
            if (lst_DopServices == null) return null;
            if (lst_DopServices.Count == 0) return null;

            for (int i = 0; i < lst_DopServices.Count; i++)
            {
                DopServiceItem item = lst_DopServices[i];
                if (item.Name == service_name)
                {
                    return item;
                }
            }

            return null;
        }

        public void btnService_ClickHandler(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(System.Windows.Controls.Image)) return;

            // Set focus to TextBos, assosiated with drop button
            System.Windows.Controls.Image btn = (System.Windows.Controls.Image)sender;
            string ListTag = btn.Tag.ToString();
            if (btn.Parent.GetType() == typeof(System.Windows.Controls.Border))
            {
                System.Windows.Controls.Border border = (System.Windows.Controls.Border)btn.Parent;
                if (border.Parent.GetType() == typeof(System.Windows.Controls.Grid))
                {
                    System.Windows.Controls.Grid grid = (System.Windows.Controls.Grid)border.Parent;
                    int ch_count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(grid);
                    for (int c = 0; c < ch_count; c++)
                    {
                        var child = System.Windows.Media.VisualTreeHelper.GetChild(grid, c);
                        if (child != null)
                        {
                            if (child.GetType() == typeof(System.Windows.Controls.TextBox))
                            {
                                System.Windows.Controls.TextBox box = (System.Windows.Controls.TextBox)child;
                                if (box.Name == "tblName")
                                {
                                    box.Focus();
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            string ValueText = "";
            int ListWidth = 400;
            List<NameValue> lst_Names = new List<NameValue>();
            lst_Names.Add(new NameValue(1, "Список значений не определен..."));

            //int ServiceID = GetDopServiceID(ListTag);
            int ServiceID = 0;
            DopServiceItem ds = GetDopServiceItem(ListTag);
            if (ds != null)
            {
                ServiceID = ds.ID;
                lst_Names = ds.TextList;
            }
            //if (ServiceID == -106) { lst_Names = lst_Names_TK; ListWidth = 220; } // Тип кровати
            //else if (ServiceID == -110) { lst_Names = lst_Names_VP; ListWidth = 340; } // Вариант питания
            //else if (ServiceID == -104) { lst_Names = lst_Names_TP; ListWidth = 270; } // Смена питания
            //else if (ServiceID == -105) { lst_Names = lst_Names_TP; ListWidth = 270; } // Тип питания
            //else if (ServiceID == -101) { lst_Names = lst_Names_KT; ListWidth = 270; } // Круизный тариф
            //else if (ListTag.IndexOf("Вид каюты") >= 0) lst_Names = lst_Names_VK;
            if (ServiceID == -106) ListWidth = 220; // Тип кровати
            else if (ServiceID == -110) ListWidth = 340; // Вариант питания
            else if (ServiceID == -104) ListWidth = 270; // Смена питания
            else if (ServiceID == -105) ListWidth = 270; // Тип питания
            else if (ServiceID == -101) ListWidth = 270; // Круизный тариф
            else if (ListTag.IndexOf("Вид каюты") >= 0) ListWidth = 270;

            if (cruiseViewModel != null)
            {
                if (cruiseViewModel.BonusesAndServices != null)
                {
                    foreach (BonusAndService item in cruiseViewModel.BonusesAndServices)
                    {
                        if (item.Name.IndexOf(ListTag) >= 0)
                        {
                            ValueText = item.Text;

                            if (ServiceID == -106)  // ListTag.IndexOf("Тип кровати") >= 0
                            {
                                string txt = ValueText;
                                string list_text = ValueText;
                                if (txt == "1 большая" || txt == "одна большая" || txt == "одна большая кровать")  list_text = "1 большая кровать";
                                if (txt == "Одна большая двухместная кровать")  list_text = "1 большая двухместная кровать";
                                if (txt == "двуспальная кровать" || txt == "двуспальная кровать," || txt == "Двухспальная" || txt == "Двухспальная кровать" || txt == "Одна двуспальная кровать")  list_text = "1 двуспальная кровать";
                                if (txt == "Две кровати стоят вместе")  list_text = "2 кровати вместе";
                                if (txt == "2 односпальные кровати " || txt == "Две односпальные кровати")  list_text = "2 односпальные кровати";
                                if (txt == "2 раздельные кровати" || txt == "Две кровати отдельно" || txt == "Две кровати стоят раздельно")  list_text = "2 отдельные кровати";
                                if (txt == "Кровати 2-х ярусные")  list_text = "2-х ярусные кровати";
                                if (txt == "King size bed" || txt == "Кровати king size")  list_text = "King size";
                                if (txt == "Кровати стоят отдельно (запрошено)" || txt == "Кровати отдельно" || txt == "Кровати раздельные" || txt == "отдельные кровати" || txt == "раздельные кровати")  list_text = "Кровати стоят отдельно";
                            }
                        }
                    }
                }
            }

            if (frmService != null)
            {
                if (frmService.WindowState == FormWindowState.Normal)
                {
                    frmService.Close();
                }
                if (!frmService.IsDisposed) frmService.Dispose();
                frmService = null;
            }
            if (frmService == null)
            {
                int ix = tcOptions.Left + tcOptions.Width - 30;
                int iy = Cursor.Position.Y - 10; // tcOptions.Top + tcOptions.Height;

                frmService = new frmNewOptionsService(ix, iy, lst_Names, ValueText, ListTag, OnSelectServiceText);
                frmService.Width = ListWidth;
                frmService.Show();
            }


        }

        public bool UndoConfirmRequest(string message)
        {
            bool bConfirm = false;

            //if (MessageBox.Show(message, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) bConfirm = true;

            frmNewOptionsConfirmUndo frm = new frmNewOptionsConfirmUndo();
            frm.StartPosX = this.Left + tcOptions.Left + tcOptions.Width - 30;
            frm.StartPosY = this.Top + tcOptions.Top + tcOptions.Height - frm.Height - 100;
            frm.ShowDialog();
            if (frm.ConfirmFlag) bConfirm = true;
            if (frm.CancelFlag) bConfirm = false;

            return bConfirm;
        }

        public void OnSelectServiceText(string ServiceText, string ListTag)
        {
            //MessageBox.Show("selected text: " + ServiceText);

            if (cruiseViewModel != null)
            {
                if (cruiseViewModel.BonusesAndServices != null)
                {
                    foreach (BonusAndService item in cruiseViewModel.BonusesAndServices)
                    {
                        if (item.Name.IndexOf(ListTag) >= 0)
                        {
                            if (ServiceText == "INSERT")
                            {
                                item.IsEditable = true;
                                item.Text = "";
                            }
                            else
                            {
                                item.IsEditable = false;
                                item.Text = ServiceText;
                            }
                        }
                    }
                }
            }
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
            tb.BorderStyle = NoEditableBorderStyle;
            tb.BackColor = NoEditableBackColor;
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

        public void UpdateInsData()
        {
            GetData();
        }

        private void tcOptions_Selected(object sender, TabControlEventArgs e)
        {
            //tableLayoutPanel1.SuspendLayout();
            if (e.TabPage == null)
                return;

            OptionTabPage page = (OptionTabPage)e.TabPage.Tag;
            _optionTabControl.CurrentPage = page;

            if (page != null && page.ServiceTabViewModel != null)
                SetVoucherService(page.ServiceTabViewModel.SelectedService);
            else if (page != null && page.CurrentService != null)
                SetService(page.CurrentService);

            if(page != null && page.url != null)
                GoToUrl(page.url);
            else
                GoToUrlEmpty();

            /*
            OptionTabPage page = (OptionTabPage)e.TabPage.Tag;

            if (_optionTabControl != null && _optionTabControl.CurrentPage != null && page != null)
                _optionTabControl.CurrentPage.url = _gwb.Url.ToString();

            if (_optionTabControl != null && page != null)
            {
                _optionTabControl.CurrentPage = page;
                if (_optionTabControl.CurrentPage.url != null)
                    GoToUrl(_optionTabControl.CurrentPage.url);
                else
                    GoToUrlEmpty();
            }*/

            switch (e.TabPage.Name)
            {
                case "tcpAllVaucher":
                    SetVoucher();
                    //GoToUrlEmpty();
                    break;
                default:
                    //GoToUrlEmpty();
                    break;
            }

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
                        new AccessClass(WorkWithData.Connection).isSuperViser || 
                        new AccessClass(WorkWithData.Connection).isRealize)
                    {
                        tlpWebBrowser.Visible = true;
                        tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                        tableLayoutPanel1.ColumnStyles[0].Width = 416;
                        tableLayoutPanel1.ColumnStyles[1].Width = 100;
                    }
                    break;
            }

            int FilterKey = 0;
            string service_name = "(название выбранной услуги)";
            //if (service != null) service_name = service.dl_name;
            switch (e.TabPage.Name)
            {
                case "tcpAllVaucher":
                    service_name = "Вся путевка";
                    FilterKey = 0;
                    break;
                case "tcpCruise":
                    service_name = "Круиз";
                    FilterKey = 1;
                    break;
                case "tcpCruiseNew":
                    service_name = "Круиз";
                    FilterKey = 1;
                    break;
                case "tcpHotelNew":
                    service_name = "Отели";
                    FilterKey = 4;
                    break;
                case "tcpAviaNew":
                    service_name = "Авиаперелеты";
                    FilterKey = 2;
                    break;
                case "tcpInshurNew":
                    service_name = "Страховки";
                    FilterKey = 7;
                    break;
                case "tcpVisaNew":
                    service_name = "Визы";
                    FilterKey = 8;
                    break;
                case "tcpTransfers":
                    service_name = "Трансферы";
                    FilterKey = 3;
                    break;
                default:
                    service_name = "Вся путевка";
                    FilterKey = 0;
                    break;
            }
            if (_voucherViewModel != null)
            {
                _voucherViewModel.SelectedServiceName = service_name;
                _voucherViewModel.SetFilterKey(FilterKey);
            }
            /*
            switch (e.TabPage.Name)
            {
                case "tcpCruise":
                    generateBlock1ToCruise(Service.Parse(cbCruises.SelectedItem).dlkey);
                    break;
                case "tcpHotel":
                    generateBlok1ForHotel(Service.Parse(cbHotels.SelectedItem).dlkey);
                    break;
            }*/
            /*
            foreach (var control in e.TabPage.Controls)
            {
                TableLayoutPanel tlp = control as TableLayoutPanel;

                if (tlp != null)
                {
                    foreach (var control1 in tlp.Controls)
                    {
                        ComboBox cb = control1 as ComboBox;
                        if (cb != null)
                        {
                            cb.SelectedIndex = 0;

                            cbOption_SelectedIndexChanged(cb, null);

                            /*Service line = cb.SelectedItem as Service;
                            SetService(line.dlkey, ServiceType.Unknow);*/
                        /*}
                    }
                }
            }*/


            //tableLayoutPanel1.ResumeLayout();
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

        private void updateDataGrid()
        {
            //dgvServ.DataSource = _servisesmas;

            dgvItinerary.DataSource = _Itinerary;
            foreach (DataGridViewColumn column in dgvItinerary.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "rank":
                        {
                            column.DisplayIndex = 0;
                            column.HeaderText = "День";
                        }
                        break;
                    case "activitydate":
                        {
                            column.DisplayIndex = 1;
                            column.HeaderText = "Дата";
                            column.DefaultCellStyle.Format = "dd.MM.yy";
                        }
                        break;
                    case "locname_ru":
                        {
                            column.DisplayIndex = 2;
                            column.HeaderText = "Порт";
                        }
                        break;
                    case "arrival":
                        {
                            column.DisplayIndex = 3;
                            column.HeaderText = "Прибытие";
                        }
                        break;
                    case "departure":
                        {
                            column.DisplayIndex = 4;
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
                var line = cbCruises.SelectedItem as ServiceInfo;
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

        private void tbAdress_Leave(object sender, EventArgs e)
        {
            //wbBook.Source = new Uri(tbAdress.Text);
            // wbBook.
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbInfo.SelectedText);
        }

        private void cbIsBook_CheckedChanged(object sender, EventArgs e)
        {
            dtpOption.Enabled = mtbTime.Enabled = !cbIsBook.Checked;

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
                                    GetChekBox(row.Field<int>("actions_id"), row.Field<int>("isRight") == 1),
                                    GetLabel(row.Field<string>("Text"))
                                }.ToArray()));
                }
                else
                {
                    controlBonusCruiseAdd(
                        GetTablePanel(
                            new List<object>
                                {
                                    GetChekBox(row.Field<int>("actions_id"), row.Field<int>("isRight") == 1),
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
                                    var kesh = cbCruises.SelectedItem as ServiceInfo;
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
                    var kesh = cbCruises.SelectedItem as ServiceInfo;
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
                    var kesh = cbCruises.SelectedItem as ServiceInfo;
                    com.Parameters.AddWithValue("@p1", kesh.dlkey);
                    com.ExecuteNonQuery();
                }
            }
           
        }

        private void BtnContact_OnClick(object sender, EventArgs eventArgs)
        {
            if (frmContacts != null)
            {
                if (frmContacts.WindowState == FormWindowState.Normal)
                {
                    frmContacts.Close();
                }
                if (!frmContacts.IsDisposed) frmContacts.Dispose();
                frmContacts = null;
            }
            if (frmContacts == null)
            {
                int ix = BtnContact.Left + this.Left;
                int iy = BtnContact.Top + BtnContact.Height + this.Top + 65;

                //_contactsList = new List<NameValue>();
                //_contactsList.Add(new NameValue("Покупатель", "Интур-Саратов"));
                //_contactsList.Add(new NameValue("Договор", "17-00267-2"));
                //_contactsList.Add(new NameValue("Руководитель группы", "Иванов Иван Иванович"));
                //_contactsList.Add(new NameValue("Тел1.", "+78452277620"));
                //_contactsList.Add(new NameValue("Тел2.", "+78452618230"));
                //_contactsList.Add(new NameValue("E-mail", "intour@overta.ru"));
                //_contactsList.Add(new NameValue("Контактное лицо", "Иванова Мария Ивановна"));
                //_contactsList.Add(new NameValue("Тел.", "+74535555554"));

                frmContacts = new frmNewOptionsContacts(ix, iy, _contactsList, "Контакты");
                frmContacts.Show();
            }

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

        private void ShowInsuranceSystem(object sender) //@
        {
            IniFile ini = new IniFile(WinIni);
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

        private void btnInshur_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(WinIni);
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
                WorkWithData.InsertHistory(_dgCode, text, RequestMessageMod.MTC, "");
                //using (SqlCommand com = new SqlCommand(insert_history, WorkWithData.Connection))
                //{
                //    com.Parameters.AddWithValue("@dgcode", _dgCode);
                //    com.Parameters.AddWithValue("@who", WorkWithData.GetUserName());
                //    com.Parameters.AddWithValue("@text", text);
                //    com.Parameters.AddWithValue("@mod", RequestMessageMod.MTC);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            FormClose();
        }

        private void FormClose()
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
            //tbAdress.Text = e.Url.ToString();
            SetAddressText(e.Url.ToString());
        }

        private void frmNewOptions_Shown(object sender, EventArgs e)
        {
            //updateDataGridPutevka();
            //UpdateDataGridChanges(ChangeType.Voucher);
            //UpdateDataGridProblem();
        }

        #region putevkaGridView_old

        /*
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

            if (dgvPutevka.Rows[rowIndex].Cells[colIndex].Value.Equals(dgvPutevka.Rows[rowIndex - 1].Cells[colIndex].Value) && 1==2)
                
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */

        #endregion

        private void UpdateDataGridProblem()
        {
            if (tcpProblem.IsDisposed)
                return;

            dgvProblems.CellClick -= dgvProblems_CellContentClick;
            dgvProblems.CellClick += dgvProblems_CellContentClick;

            /*_problems = WorkWithData.GetAllProblems(_dgCode);
            if (_problems.Rows.Count < 1)
            {
                tcProblemChanges.TabPages.Remove(tcpProblem);
            }*/

            tcpProblem.Text = "Проблемы (" + _problems.Rows.Count.ToString() + ")";

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
            if (e.TabPage == null)
                return;

            if (dgvItinerary.DataSource == null)
            {
                _Itinerary = WorkWithData.GetItinerary(_dgCode);
                updateDataGrid();
            }
            else if (e.TabPage.Equals(tcpMessages))
            {
                /*int x = this.Location.X + (this.Width - panel12.Width),
                    y = this.Location.Y + (this.Height - panel12.Height),
                    h = panel12.Height,
                    w = panel12.Width;
                messagesForm = new frmMessages(_dgCode, x, y, h, w);
                messagesForm.TopMost = true;
                messagesForm.Show();

                e.Cancel = true;*/
                //scPutevka.SplitterDistance =(int)0.7*panel12.Height;
                //tcpMessages.Controls.Add(new ucMessages(_dgCode)
                //{
                //    Dock = DockStyle.Fill
                //});
            }
            else if (e.TabPage.Equals(tcpMessageNew))
            {
                int x = this.Location.X + (this.Width - panel12.Width);
                int y = this.Location.Y + (this.Height - panel12.Height);
                int h = panel12.Height;
                int w = panel12.Width;

                messagesForm = new frmMessagesNew(_dgCode, x, y, h, w) {TopMost = true};
                messagesForm.Show();

                tcpMessageNew.Text = "Переписка";

                /*_correspView = new CorrespondenceView() {Left = x, Top = y, Width = w, Height = h};
                ElementHost.EnableModelessKeyboardInterop(_correspView);
                _correspViewModel = new CorrespondenceViewModel(_dgCode);
                _correspView.InitializeComponent();
                _correspView.DataContext = _correspViewModel;
                _correspView.Topmost = true;
                _correspView.ShowDialog();*/
                //_correspView.Show();
               
                e.Cancel = true;
            }
            else if (e.TabPage.Name.Equals("tcpOldItinerary"))
            {
                DataGridView dgv = e.TabPage.Controls["dgvOldItinerary"] as DataGridView;
                dgv.DataSource = WorkWithData.GetOldItinerary(_dgCode);
            }
            //else if (e.TabPage.Equals(tcpBillSettings) && _billSettingControl != null)
            //{
            //    FocusBillTab();
            //}
            else if (e.TabPage.Equals(tcpBillSettings) && _billSetting2Control != null)
            {
                FocusBillTab();
            }

        }

        private void FocusBillTab()
        {
            int number = 0;
            if (_regNumber > 0)
            {
                number = _regNumber;
                _regNumber = 0;
            }

            //if (_billSettingControl.GetType() == typeof(ucBillSetting))
            //{
            //    ucBillSetting bill = (ucBillSetting)_billSettingControl;
            //    bill.SetFocus(number);
            //}
            if (_billSetting2Control.GetType() == typeof(ucBillSetting2))
            {
                ucBillSetting2 bill = (ucBillSetting2)_billSetting2Control;
                bill.SetFocus(number);
            }
        }

        private void OpenTasker()
        {
            TaskerTimer.Start();
        }

        //----------------------------------------------------------------------------------------------------
        private void TaskerTimer_Tick(Object myObject, EventArgs myEventArgs)
        {
            TaskerTimer.Stop();

            ShowTasker();
        }

        private void tcProblemChanges_SelectedIndexChanged(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == null)
                return;

            //if (e.TabPage.Equals(tcpBillSettings) && _billSettingControl != null)
            //{
            //    FocusBillTab();
            //}
            if (e.TabPage.Equals(tcpBillSettings) && _billSetting2Control != null)
            {
                FocusBillTab();
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
        
        public void GenerateBlock(ServiceInfo service)
        {
            switch (service.SType)
            {
                case ServiceType.Avia:
                    break;

                case ServiceType.Hotel:
                    break;

                /*case ServiceType.DopPaket:
                    generateBlok1ForPaket(service.dlkey);
                    break;*/

                case ServiceType.Inshur:
                    break;

                case ServiceType.Cruise:
                    generateBlock1ToCruise(service.dlkey);
                    break;

                case ServiceType.Unknow:
                    break;
            }
        }

        private void ChangeCurrentService(OptionTabPage pageControl, ServiceInfo service)
        {
            if (service != null)
            {
                if (pageControl.CurrentService == null || !pageControl.CurrentService.Equals(service))
                    GenerateBlock(service);

                SetService(service);
                pageControl.CurrentService = service;
            }
        }

        public void cbOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox) sender;
            var service = (ServiceInfo)cb.SelectedItem;

            TabPage page = (TabPage) cb.Parent.Parent;
            OptionTabPage pageControl = (OptionTabPage) page.Tag;

            ChangeCurrentService(pageControl, service);
        }
       
        /*
        private void cbAvia_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitFlyData();
            var line = (ServiceInfo)cbAvia.SelectedItem;
            if (line != null)
            {
                if (line.BronId != null) GenerateBlok1ForAvia((int)line.BronId, null);
                //SetService(line.dlkey, ServiceType.Avia);
            }
        }
          */

        /*
        private void btnFlight_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position);
            
            MethodInfo mi = typeof(Button).GetMethod("ShowContextMenu",
                BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(btnFlight, null);
            
        }
        */

        private void ShowFligth()
        {
            string flightBaseUrl = _conf.Get_Value("appSettings", "flightURL");
            string hash = _conf.Get_Value("appSettings", "hash");
            int hashMode = Convert.ToInt32(_conf.Get_Value("appSettings", "hashMode"));
            int userId = WorkWithData.GetUserID();

            //test POST
            //baseUrl = "http://www.htmlcodetutorial.com/cgi-bin/mycgi.pl";

            string fullUrl = String.Format("{0}?user_id={1}&dgCode={2}", flightBaseUrl, userId, _dgCode);
            tcPartnerPutevkaTest.SelectTab(0);

            MimeInputStream postData;

            switch (hashMode)
            {
                case 0:
                    GoToUrl(fullUrl + String.Format("&managerauth={0}", hash));
                    return;

                case 1:
                    postData = MimeInputStream.Create();
                    postData.AddContentLength = true;
                    postData.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    postData.SetData(String.Format("managerauth={0}", hash));
                    GoToUrl(fullUrl + String.Format("&managerauth={0}", hash), postData);
                    return;

                case 2:
                    var signDate = new SignData(hash);
                    signDate.Add("user_id", userId.ToString());

                    postData = MimeInputStream.Create();
                    postData.AddContentLength = true;
                    postData.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    postData.SetData(String.Format("signData={0}", signDate.GetBase64()));
                    GoToUrl(fullUrl + String.Format("&managerauth={0}", hash), postData);
                    break;
            }

        }

        private void ShowTransfer()
        {
            string transferBaseUrl = _conf.Get_Value("appSettings", "transferURL");
            string transferClearUrl = _conf.Get_Value("appSettings", "transferclearURL");
            string hash = _conf.Get_Value("appSettings", "hash");
            int hashMode = Convert.ToInt32(_conf.Get_Value("appSettings", "hashMode"));
            int userId = WorkWithData.GetUserID();

            string fullUrl = String.Format("{0}?user_id={1}&dgCode={2}", transferBaseUrl, userId, _dgCode);
            tcPartnerPutevkaTest.SelectTab(0);

            // destrot session params on server side
            string clearUrl = transferClearUrl; 
//#if DEBUG
//            clearUrl = @"http://bit.mcruises.ru/api/hidden/destroy_session.php";
//#endif
            GoToUrl(clearUrl);
            bool bWait = true;
            DateTime dtNavigate = DateTime.Now;
            while (bWait)
            {
                bWait = true;
                if (NavigatedFlag) bWait = false; // waiting for navigated
                if (DateTime.Now > dtNavigate.AddSeconds(5)) bWait = false; // timeout N seconds
                if (DateTime.Now < dtNavigate.AddSeconds(1)) bWait = true; // waiting anyway 2 seconds
                Application.DoEvents();
            }
            SetAddressText("");
            //GoToUrlEmpty();

            MimeInputStream postData;

            switch (hashMode)
            {
                case 0:
                    GoToUrl(fullUrl + String.Format("&managerauth={0}", hash));
                    return;

                case 1:
                    postData = MimeInputStream.Create();
                    postData.AddContentLength = true;
                    postData.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    postData.SetData(String.Format("managerauth={0}", hash));
                    GoToUrl(fullUrl + String.Format("&managerauth={0}", hash), postData);
                    return;

                case 2:
                    var signDate = new SignData(hash);
                    signDate.Add("user_id", userId.ToString());

                    postData = MimeInputStream.Create();
                    postData.AddContentLength = true;
                    postData.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    postData.SetData(String.Format("signData={0}", signDate.GetBase64()));
                    GoToUrl(fullUrl + String.Format("&managerauth={0}", hash), postData);
                    break;
            }
            
        }

        
        private void GoToUrlEmpty()
        {
            if (_gwb != null && !_gwb.IsDisposed) 
                _gwb.Navigate("about:blank");
        }

        private void GoToUrl(string url, MimeInputStream postData)
        {
            NavigatingFlag = false;
            NavigatedFlag = false;

            try
            {
                _gwb.Navigate(url, GeckoLoadFlags.None, null, postData);
                //_gwb.Navigate(url);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void GoToUrl(string url)
        {
            NavigatingFlag = false;
            NavigatedFlag = false;

            try
            {
                if (_gwb!= null)
                    _gwb.Navigate(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) 
                GoToUrl(tbAdress.Text);
        }

        private void DogovorStateTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetDogovorControlState((ucDogovorSetting.State)DogovorStateTest.SelectedItem);
        }

        private void WebBrowserMaximize()
        {
            tcpPartner.Controls.Remove(tlpWebBrowser);
            panelFullScreen.Controls.Add(tlpWebBrowser);
            panelFullScreen.Dock = DockStyle.Fill;
            panelFullScreen.Visible = true;
            tableLayoutPanelHeader.Visible = false;
            //Controls.Remove(tableLayoutPanelHeader);
            _webBrowserIsExpand = true;
            tlpBrowserCmd.ColumnStyles[1].Width = 60;
            tlpBrowserCmd.ColumnStyles[2].Width = 0;
        }

        private void WebBrowserToPanel()
        {
            panelFullScreen.Controls.Remove(tlpWebBrowser);
            tcpPartner.Controls.Add(tlpWebBrowser);
            panelFullScreen.Visible = false;
            tableLayoutPanelHeader.Visible = true;
            //Controls.Add(tableLayoutPanelHeader);
            _webBrowserIsExpand = false;
            tlpBrowserCmd.ColumnStyles[1].Width = 40;
            tlpBrowserCmd.ColumnStyles[2].Width = 70;
        }

        private void WebBrowserChangeState()
        {
            if (_webBrowserIsExpand)
                WebBrowserToPanel();
            else
                WebBrowserMaximize();
        }

        private void UpdateTasks()
        {
            if (OptionTaskList == null) OptionTaskList = new List<OptionTask>();
            OptionTaskList.Clear();

            int all_count = 0;
            int day_count = 0;
            int PostponeCount = 0;
            //DataTable _taskTable = WorkWithData.GetDaysTasks();
            DataTable taskTable = WorkWithData.GetDaysTasks();
            if (taskTable != null)
            {
                int num = 0;
                for (int i = 0; i < taskTable.Rows.Count; i++)
                {
                    DataRow row = taskTable.Rows[i];
                    string code = "";
                    if (row["DG_CODE"] != null) code = (string)row["DG_CODE"];
                    if (code == _dgCode)
                    {
                        num++;
                        int id = (int)row["task_id"];
                        int prior = (int)row["task_prior"];
                        string dgcode = (string)row["DG_CODE"];
                        DateTime dates = (DateTime)row["create_date"];
                        DateTime task_date = (DateTime)row["task_date"];
                        string name = (string)row["NAME"];
                        string stat = (string)row["STAT"];
                        int task_flag = (int)row["task_flag"];
                        int open_code = (int)row["code"];
                        if (string.IsNullOrEmpty(name)) name = stat;

                        string name_date = name;
                        //if (task_date.Year > 2010) name_date = name + "  до " + task_date.ToString("dd.MM.yy");

                        OptionTaskList.Add(new OptionTask(id, num, prior, dgcode, dates, task_date, name_date, stat, task_flag, open_code));

                        if (name == "Отложенный заказ" || name == "Отложить заказ") PostponeCount++;
                        if (task_date.Date <= DateTime.Now.Date) day_count++;
                    }
                }
            }

            if (OptionTaskList != null) all_count = OptionTaskList.Count;
            lblDayTasksCount.Text = day_count.ToString();
            lblAllTasksCount.Text = all_count.ToString();

            if (PostponeControl != null)
                PostponeControl.SetTitleCount(PostponeCount);
        }

        public void TaskerClosed(bool UpdateFlag)
        {
            if (UpdateFlag) UpdateTasks();
        }

        public void TaskerCloseTask(int TaskID)
        {
            if (MainForm != null) MainForm.UpdateDayTasks();

            UpdateTasks();
        }

        private void lblTaskStatus_Click(object sender, EventArgs e)
        {
            ShowTasker();
        }

        private void lblAllTasks_Click(object sender, EventArgs e)
        {
            ShowTasker();
        }

        private void ShowTasker()
        {
            if (OptionTaskList == null) return;
            if (OptionTaskList.Count == 0) return;

            int x = lblTaskStatus.Left + panelTasker.Left + this.Left - 350;
            int y = this.Top + 280;

            frmTasker = new frmNewOptionsTasker(x, y, _dgCode, OptionTaskList, TaskerClosed, TaskerCloseTask);
            frmTasker.Show();
        }

    }
}
