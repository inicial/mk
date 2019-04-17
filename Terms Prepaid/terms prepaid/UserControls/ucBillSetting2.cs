using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Forms;
using terms_prepaid.Helper_Classes;
using terms_prepaid.Helpers;
using lanta.SQLConfig;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;
using System.Threading.Tasks;
using DataService;
using GalaSoft.MvvmLight.Messaging;
using WpfControlLibrary.Messages;
using WpfControlLibrary.Util;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using TextFormat = WpfControlLibrary.Util.TextFormat;
using UserControl = System.Windows.Forms.UserControl;

using System.Data.SqlClient;



namespace terms_prepaid.UserControls
{
    //public delegate void ReloadOptionsForm(string DgCode, int RegNumber);

    public partial class ucBillSetting2 : UserControl, IBillSetting2
    {
        //====================================================================================================
        #region Variables
        //----------------------------------------------------------------------------------------------------
        public delegate void UpdateVaucher(bool aviaErrorCallback = false, ServiceType serviceType = ServiceType.Unknow);

        private UpdateVaucher _updateVaucher;

        public enum ModeType { vaucher, service };

        private readonly Color _colorEnabled = Color.White;
        private readonly Color _colorDisabled = Color.Silver;

        private decimal Cost { get; set; }

        private string _dgcode = string.Empty;
        private string _bronNumber = string.Empty;
        private int? _dlKey;
        private int _partnerKey;
        private int UserKey;

        private DataRow _rowDogovor = null;

        private IContractService _serv;

        public Service Service { get; private set; }

        private Label _serviceTitle;

        private VoucherViewModel _voucherViewModel;

        private DateTime _minDate = new DateTime(1900, 1, 1);

        //----------------------------------------------------------------------------------------------------
        private string DgCode = "";
        private int RegNumber = 0;
        private int PresetRegNumber = 0;
        private string DgRate = "";

        private string BillNumber = "";
        private DateTime BillDate;
        private double Summa = 0;
        private double SummaDeposit = 0;
        private double SummaRest = 0;
        private DateTime DatePayDeposit;
        private DateTime DatePayRest;
        private string Dogovor = "";
        private int Partner = 0;
        private string RateCode = "";
        private string Bron = "";
        private string Subject = "";
        private string Comment = "";
        private int BillFileKey = 0;
        private string BillFilePath = "";
        private string BillFileName = "";

        private bool RegNumberIsValid;
        private bool BillNumberIsValid;
        private bool SummaIsValid;
        private bool SummaDepositIsValid;
        private bool SummaRestIsValid;
        private bool DatePayDepositIsValid;
        private bool DatePayRestIsValid;
        private bool DogovorIsValid;
        private bool BillPathIsValid;

        private DateTime InitDateBill;
        private DateTime InitDatePayDeposit;
        private DateTime InitDatePayRest;
        private DateTime MinDate;
        private DateTime MaxDate;

        private bool NewBillFlag = false;        // внесение нового счета
        private bool NumberSearchFlag = false;   // выполнен поиск письма по ноиеру
        private bool NumberFoundFlag = false;    // письмо с номером найдено в базе 
        private bool NumberChangedFlag = false;  // номер изменен после того, как найден 
        private bool DataEditedFlag = false;     // даные изменены/введены после того, как найден номер
        private bool DataValidFlag = false;      // даные введены корректно
        private bool DataSavedFlag = false;      // даные сохранены
        private bool BillFileFlag = false;       // задан файл счета для загрузки

        private string DataStatus = "";          // статус ввода данных
        private bool DataNotEditingFlag = false; // даные задаются программно (не редактированием)
        private bool DataClearingFlag = false;   // даные задаются программно при очистке (не редактированием)

        private InterMode InMode;

        private bool InsertModeFlag;             // внесение нового счета
        private string CheckTitle = "Проверка  данных  привязки  к  счету";
        private string CheckStatus = "";

        //----------------------------------------------------------------------------------------------------
        private bool CheckPanelFlag = true;
        private int CheckPanelHeight = 35;
        private bool MissmatchPanelFlag = true;
        private int MissmatchPanelHeight = 95;
        
        char DecSign = '.';

        private Timer FocusTimer;

        ReloadOptionsForm ReloadCallback;

        //----------------------------------------------------------------------------------------------------
        #endregion // Variables
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        public ucBillSetting2(UpdateVaucher updateVaucher, VoucherViewModel voucherViewModel, string iDgCode, string iDgRate, int iUserKey, ReloadOptionsForm iReloadCallback)
        {
            _serv = Repository.GetInstance<IContractService>();
            InitializeComponent();

            DecSign = '.';
            if ((1.2).ToString().IndexOf(',') > 0) DecSign = ',';

            _voucherViewModel = voucherViewModel;

            _serviceTitle = lbl_ServiceTitle;
            _serviceTitle.Visible = false;
            _serviceTitle.AutoSize = true;
            //_serviceTitle.BackColor = Color.BurlyWood;
            _serviceTitle.Margin = new Padding(5, 5, 5, 5);
            _serviceTitle.Height = 20;
            _serviceTitle.Width = 500;
            _serviceTitle.Dock = DockStyle.Top;

            DgCode = iDgCode;
            if (voucherViewModel.Voucher != null) _dgcode = voucherViewModel.Voucher.DgCode;
            if (!string.IsNullOrEmpty(_dgcode)) DgCode = _dgcode;
            
            DgRate = iDgRate;
            UserKey = iUserKey;

            ReloadCallback = iReloadCallback;

            //pBill.Dock = DockStyle.Top;
            _updateVaucher = updateVaucher;

            InitData();
            //AccordControls();

            ctlTabs.DrawMode = TabDrawMode.OwnerDrawFixed;
            ctlTabs.DrawItem += ctlTabs_DrawItem;

            SetDataPanel(false);
            SetFilePanel(false);

            txt_RegNumber.Focus();

            SetMissmatchPanel(false);
            //SetCheckPanel(false);

            FocusTimer = new Timer();
            FocusTimer.Interval = 100;
            FocusTimer.Tick += FocusTimer_Tick; 
        }

        //----------------------------------------------------------------------------------------------------
        public void SetFocus()
        {
            SetFocus(0);
        }

        //----------------------------------------------------------------------------------------------------
        public void SetFocus(int iRegNumber)
        {
            if (iRegNumber > 0) PresetRegNumber = iRegNumber;

            FocusTimer.Start();
        }

        //----------------------------------------------------------------------------------------------------
        private void FocusTimer_Tick(Object myObject, EventArgs myEventArgs)
        {
            FocusTimer.Stop();
            txt_RegNumber.Focus();

            if (PresetRegNumber > 0)
            {
                RegNumber = PresetRegNumber;
                PresetRegNumber = 0;

                Search_Bill();

                InMode = InterMode.Editing;
                Check_Data();
                AccordControls();
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void ctlTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage tp = ctlTabs.TabPages[e.Index];
            Color color = ctlTabs.SelectedTab.Equals(tp) ? Color.Cornsilk : Color.LightGray; // .Beige .Moccasin

            using (Brush br = new SolidBrush(color))
            {
                tp.BackColor = color;
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(ctlTabs.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(ctlTabs.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Methods
        //====================================================================================================
        #region Form controlling
        //----------------------------------------------------------------------------------------------------
        private void SetCheckPanel(bool state_flag)
        {
            if (state_flag == CheckPanelFlag) return;

            if (state_flag)
            {
                //this.Height = this.Height + CheckPanelHeight;

                pTabs.Height = pTabs.Height + CheckPanelHeight;
                ctlTabs.Height = ctlTabs.Height + CheckPanelHeight;
                pCheck.Height = pCheck.Height + CheckPanelHeight;
                pInsert.Height = pInsert.Height + CheckPanelHeight;
            }
            else
            {
                pTabs.Height = pTabs.Height - CheckPanelHeight;
                ctlTabs.Height = ctlTabs.Height - CheckPanelHeight;
                pCheck.Height = pCheck.Height - CheckPanelHeight;
                pInsert.Height = pInsert.Height - CheckPanelHeight;

                if (MissmatchPanelFlag) SetMissmatchPanel(false);

                //this.Height = this.Height - CheckPanelHeight;
            }

            CheckPanelFlag = state_flag;
        }

        //----------------------------------------------------------------------------------------------------
        private void SetMissmatchPanel(bool state_flag)
        {
            if (state_flag == MissmatchPanelFlag) return;

            if (state_flag)
            {
                //this.Height = this.Height + MissmatchPanelHeight;

                if (!CheckPanelFlag) SetCheckPanel(true);

                pTabs.Height = pTabs.Height + MissmatchPanelHeight;
                ctlTabs.Height = ctlTabs.Height + MissmatchPanelHeight;
                pCheck.Height = pCheck.Height + MissmatchPanelHeight;
                pInsert.Height = pInsert.Height + MissmatchPanelHeight;
            }
            else
            {
                pTabs.Height = pTabs.Height - MissmatchPanelHeight;
                ctlTabs.Height = ctlTabs.Height - MissmatchPanelHeight;
                pCheck.Height = pCheck.Height - MissmatchPanelHeight;
                pInsert.Height = pInsert.Height - MissmatchPanelHeight;

                //this.Height = this.Height - CheckPanelHeight;
            }

            MissmatchPanelFlag = state_flag;
        }

        //----------------------------------------------------------------------------------------------------
        private void SetDataPanel(bool visible_flag)
        {
            pUpdateData.Visible = visible_flag;
            pFile.Visible = visible_flag;
            if (visible_flag)
            {
                //lblUpdateData.Text = "Заполните  данные  счета";
                picUpdateData.Image = Properties.Resources.ico_arrow_up.ToBitmap();

                SetFilePanel(true);
            }
            else
            {
                //lblUpdateData.Text = "Заполните  данные  счета";
                picUpdateData.Image = Properties.Resources.ico_arrow_down.ToBitmap();
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void SetFilePanel(bool visible_flag)
        {
            pUpdateFile.Visible = visible_flag;
            if (visible_flag)
            {
                //lblUpdateFile.Text = "Прикрепите  файл  со  счетом";
                picUpdateFile.Image = Properties.Resources.ico_arrow_up.ToBitmap();
            }
            else
            {
                //lblUpdateFile.Text = "Прикрепите  файл  со  счетом";
                picUpdateFile.Image = Properties.Resources.ico_arrow_down.ToBitmap();
            }

        }

        //----------------------------------------------------------------------------------------------------
        private void SetStatus(string status)
        {
            lbl_Status.Text = status;
        }

        //----------------------------------------------------------------------------------------------------
        private void AccordCheckStatus()
        {
            string status = CheckTitle;
            if (!string.IsNullOrEmpty(CheckStatus)) status = CheckTitle + ":    " + CheckStatus;
            lblCheck.Text = status;
        }

        //----------------------------------------------------------------------------------------------------
        private void SetCheckStatus(string status)
        {
            CheckStatus = status;

            AccordCheckStatus();
        }

        //----------------------------------------------------------------------------------------------------
        private void SetControlColor(Control ctl, bool valid_flag)
        {
            if (ctl == null) return;

            Color clr = Color.Black;
            if (!valid_flag) clr = Color.Red;

            if (ctl.ForeColor != clr)
            {
                ctl.ForeColor = clr;
                if (ctl.GetType() == typeof(DateTimePicker))
                {
                    DateTimePicker dtp = (DateTimePicker)ctl;
                    //dtp.CalendarForeColor = clr;
                    //dtp.CalendarTrailingForeColor = clr;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void AccordControls()
        {
            Check_Mode();

            bool bNumber = false;
            bool bPartner = false;
            bool bSearch = false;
            bool bEdit = false;
            bool bSave = false;
            //string ClearText = "Отменить  изменения";
            string ClearText = "Очистить  форму";

            switch (InMode)
            {
                case InterMode.EnterNumber:
                    bNumber = true;
                    break;

                case InterMode.CanSearch:
                    bNumber = true;
                    bSearch = true;
                    break;

                case InterMode.NotFound:
                    bNumber = true;
                    break;

                case InterMode.Editing:
                    bEdit = true;
                    //if (DataValidFlag) bSave = true;
                    bSave = true;
                    break;

                case InterMode.Adding:
                    bEdit = true;
                    bPartner = true;
                    //if (DataValidFlag) bSave = true;
                    bSave = true;
                    break;

                case InterMode.Updated:
                    ClearText = "Очистить  форму";
                    break;

                case InterMode.Inserted:
                    ClearText = "Очистить  форму";
                    break;

                default:

                    break;
            }

            NewBillFlag = false;
            if (InsertModeFlag) NewBillFlag = true;

            //txt_RegNumber.Enabled = bFind;
            //btn_Search.Enabled = bFind;

            if (bSearch && !btn_Search.Enabled) SetCheckStatus("");

            txt_RegNumber.Enabled = bNumber;
            btn_Search.Enabled = bSearch;

            txt_RegNumber.Visible = !NewBillFlag;
            lbl_RegNumber.Visible = !NewBillFlag;
            btn_Search.Visible = !NewBillFlag;

            dt_DateBill.Enabled = bEdit;
            lst_Rate.Enabled = bEdit;
            txt_BillNumber.Enabled = bEdit && bPartner;
            txt_Summa.Enabled = bEdit;
            txt_SummaDeposit.Enabled = bEdit;
            dt_DatePayDeposit.Enabled = bEdit;
            txt_SummaRest.Enabled = bEdit;
            dt_DatePayRest.Enabled = bEdit;
//            txt_Dogovor.Enabled = bEdit;
//            txt_FilePath.Enabled = bEdit;
            txt_FileName.Enabled = bEdit;
            btn_SelectFile.Enabled = bEdit;
//            btn_ShowFile.Enabled = bEdit;
            txt_Subject.Enabled = bEdit;
            txt_Comment.Enabled = bEdit;

            btn_Save.Enabled = bSave; // && ConfirmFlag;
            btn_Save.Visible = bSave; 
            btn_Clear.Text = ClearText;

            SetControlColor(txt_RegNumber, RegNumberIsValid);

            SetControlColor(txt_RegNumber, RegNumberIsValid);
            SetControlColor(txt_BillNumber, BillNumberIsValid);
            SetControlColor(txt_Summa, SummaIsValid);
            SetControlColor(txt_SummaDeposit, SummaDepositIsValid);
            SetControlColor(txt_SummaRest, SummaRestIsValid);
            SetControlColor(dt_DatePayDeposit, DatePayDepositIsValid);
            SetControlColor(dt_DatePayRest, DatePayRestIsValid);
            SetControlColor(txt_FileName, BillPathIsValid);
            
            //SetControlColor(lbl_Dogovor, DogovorIsValid);
            if (DogovorIsValid)
            {
                SetMissmatchPanel(false);
                lblWarning.Text = "";
            }
            else
            {
                string msg = "Привязанный  к  счету  номер  брони  НЕ  СОВПАДАЕТ!";
                msg = msg + (char)13 + (char)10 + "Номер  брони  для  счета:   " + Dogovor;
                //msg = msg + (char)13 + (char)10;
                msg = msg + (char)13 + (char)10 + "Проверьте  указанный  счет  или  перепривяжите  к  нужной  брони.";
                //lblWarning.Text = "Привязанный  к  счету  номер  брони  НЕ  СОВПАДАЕТ!" + (char)13 + (char)10 + "Номер  брони  для  счета:   " + Dogovor;
                lblWarning.Text = msg;
                lblReload.Text = "Перейти  в  путевку  " + Dogovor;
                SetMissmatchPanel(true);
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void SetInsertMode(bool mode_flag)
        {
            if (mode_flag == InsertModeFlag) return;

            InsertModeFlag = mode_flag;
            Clear_Data(true);

            if (InsertModeFlag)
            {
                NewBillFlag = true;
                //-chk_New.Checked = true;
                //-chk_Find.Checked = false;
                Dogovor = DgCode;
                RateCode = DgRate;
                Bron = _bronNumber;
                Reflect_Data();
                SetStatus("Заполните поля и подтвердите создание новой записи.");
                InMode = InterMode.Adding;

                SetCheckPanel(false);
                SetDataPanel(true);
            }
            else
            {
                NewBillFlag = false;
                //-chk_Find.Checked = true;
                //-chk_New.Checked = false;
                InMode = InterMode.Start;

                SetCheckPanel(true);
                SetDataPanel(false);
            }
            SetFilePanel(false);
            AccordControls();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Form controlling
        //====================================================================================================
        #region Interface data functions
        //----------------------------------------------------------------------------------------------------
        private string GetPanelName(string baseName, int svKey, int idType)
        {
            string additionName;

            switch ((Voucher.SvType)svKey)
            {
                case Voucher.SvType.Avia:
                    additionName = "авиаперелету";
                    break;

                case Voucher.SvType.Cruise:
                    additionName = "круизу";
                    break;

                case Voucher.SvType.Insur:
                    additionName = "страховке";
                    break;

                case Voucher.SvType.Visa:
                    additionName = "визе";
                    break;

                default:
                    additionName = "услуге";
                    break;
            }

            if (idType == (int)Voucher.IdTypes.Cruise)
                additionName = "круизу";

            return String.Format("{0} к {1}", baseName, additionName);
        }

        //----------------------------------------------------------------------------------------------------
        public void SetService(Service service)
        {
            //SetDiscountControlsState(_access.isSuperViser || _accessisBronir || (_access.isRealize && service.SType == ServiceType.Cruise));
            //SetPrePaymentControlsState((_access.isSuperViser || _access.isBron || _access.isBronir) && service.SType != ServiceType.Avia);
//            SetPrePaymentControlsState(true);
            //btnSave.Enabled = service.SType != ServiceType.Avia;

            Service = service;

            ServiceSetting ss = null;

            try
            {
                if (Service.Group != null)
                    ss = Service.Group.Setting;
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
                ss = Service.ServiceSetting;
            }

            //ServiceSetting ss = _serv.GetServiceSetting(_dgcode, service.dlkey);

            //ServiceInfo = serviceInfo;

            if (ss == null)
            {
                _dlKey = null;
            }
            else
            {
                SetStatusSetting(Service, ss.Status);

                _dlKey = ss.DlKey;

                //service is AviaService ? ((AviaService)service).BookingAvia.Route : service.ServiceName

                string title = Service.FullName ??
                               (Service.ServType == ServiceType.Avia
                                         ? "Авиаперелет: " + ((AviaService)service).BookingAvia.Route
                                         : service.ServiceName);
                //_serviceTitle.Text = Service.FullName ?? (ServiceInfo.SType == ServiceType.Avia ? "Авиаперелет: " + ServiceInfo.dl_name : ServiceInfo.dl_name);
                if (title.IndexOf("№") == 0) title = title.Substring(title.IndexOf(" ") + 1);
                if (title.IndexOf(":") > 0) title = title.Substring(title.IndexOf(":"));
                title = GetPanelName("Обработка счета", Service.SvKey, Service.TypeId) + title;
                _serviceTitle.Text = title;

                _bronNumber = "";
                if (Service.ServType == ServiceType.Cruise)
                {
                    CruiseService cruise = (CruiseService)service;
                    _bronNumber = cruise.OptionNumber;
                    _partnerKey = cruise.PartnerKey;
                }
                else
                {
                    //CruiseService cruise = (CruiseService)service;
                    _bronNumber = service.DlComment;
                    _partnerKey = service.PartnerKey;
                }
                //if (Service.ServType == ServiceType.Avia)

                _serviceTitle.Visible = true;

                //gbStatuses.Text = GetPanelName(_gbStatusesName, Service.SvKey, Service.TypeId);
///                gbFile.Text = GetPanelName(_gbFileName, Service.SvKey, Service.TypeId);
            }
            this.AutoScrollPosition = new Point(0, 0);

//            Reset_Bill();
        }

        public void SetVoucher(int serviceCount)
        {
            _serviceTitle.Visible = false;

            //btn_Clear();

//            Reset_Bill();
        }

        public void SetBillSetting()
        {
            //_dgcode = _voucherViewModel.Voucher.DgCode;
            //_rowDogovor = _serv.GetDogovorSettings(_dgcode);

            //GetVaucher();
        }

        private void GetVaucher()
        {
            //Cost = _rowDogovor.Field<decimal>("DG_PRICE");

            //GetStatus();
        }

        private void SetStatusSetting(Service serv, StatusSetting s)
        {
            //gbFile.Text = _gbFileName;
            //gbStatuses.Text = "Статус по услуге";

            // Статус


        }

        private void GetStatus()
        {
            //var status = _rowDogovor.Field<string>("NS_Name");
            //DateTime date = _serv.GetDateForStatus(_dgcode, _rowDogovor.Field<string>("NS_QUERYFORDATE"));

            //btn_Save.Enabled = false;
        }


        //----------------------------------------------------------------------------------------------------
        #endregion // Interface data functions
        //====================================================================================================
        #region Controls events
        //----------------------------------------------------------------------------------------------------
        private void ctlTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ctlTabs.SelectedIndex == 0)
            {
                SetInsertMode(false);
            }
            if (ctlTabs.SelectedIndex == 1)
            {
                SetInsertMode(true);
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void ChangeDataPanel()
        {
            if (!NewBillFlag)
                if (!NumberFoundFlag) return;

            SetDataPanel(!pUpdateData.Visible);
            if (pUpdateData.Visible)
            {
                SetFilePanel(true);
                if (MissmatchPanelFlag)
                    this.AutoScrollPosition = new Point(0, 160);
                else
                    this.AutoScrollPosition = new Point(0, 60);
            }
            else
            {
                SetFilePanel(false);
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void ChangeFilePanel()
        {
            if (!NewBillFlag)
                if (!NumberFoundFlag) return;

            SetFilePanel(!pUpdateFile.Visible);
            if (pUpdateFile.Visible)
            {
                //SetDataPanel(false);
                //if (MissmatchPanelFlag)
                //    this.AutoScrollPosition = new Point(0, 160);
                //else
                //    this.AutoScrollPosition = new Point(0, 60);

                int range = 200;
                if (!InsertModeFlag)
                {
                    range = range + CheckPanelHeight;
                    if (MissmatchPanelFlag) range = range + MissmatchPanelHeight;
                }
                if (this.AutoScrollPosition.Y < range) this.AutoScrollPosition = new Point(0, range);
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void lblUpdateData_Click(object sender, EventArgs e)
        {
            ChangeDataPanel();
        }

        //----------------------------------------------------------------------------------------------------
        private void lblUpdateFile_Click(object sender, EventArgs e)
        {
            ChangeFilePanel();
        }

        //----------------------------------------------------------------------------------------------------
        private void picUpdateData_Click(object sender, EventArgs e)
        {
            ChangeDataPanel();
        }

        //----------------------------------------------------------------------------------------------------
        private void picUpdateFile_Click(object sender, EventArgs e)
        {
            ChangeFilePanel();
        }

        //----------------------------------------------------------------------------------------------------
        private void DataEditingHandler(object sender, EventArgs e)
        {
            if (DataNotEditingFlag) return;

            DataEditedFlag = true;
            AccordControls();
        }

        //----------------------------------------------------------------------------------------------------
        private void txt_RegNumber_TextChanged(object sender, EventArgs e)
        {
            if (DataNotEditingFlag) return;

            NumberChangedFlag = true;
            AccordControls();
        }

        //----------------------------------------------------------------------------------------------------
        private void dt_DateBill_ValueChanged(object sender, EventArgs e)
        {
            DataEditingHandler(sender, e);

            string text = "";
            //if (!DataClearingFlag)
            //{
                DateTime dt = dt_DateBill.Value.Date;
                //if (dt != InitDateBill && dt.Year > 2010) 
                    text = dt.ToString("dd.MM.yy");
            //}
            txt_DateBill.Text = text;
        }

        //----------------------------------------------------------------------------------------------------
        private void dt_DatePayDeposit_ValueChanged(object sender, EventArgs e)
        {
            DataEditingHandler(sender, e);

            string text = "";
            if (!DataClearingFlag)
            {
                DateTime dt = dt_DatePayDeposit.Value.Date;
                if (dt != InitDatePayDeposit && dt.Year > 2010) text = dt.ToString("dd.MM.yy");
            }
            txt_DatePayDeposit.Text = text;
        }

        //----------------------------------------------------------------------------------------------------
        private void dt_DatePayRest_ValueChanged(object sender, EventArgs e)
        {
            DataEditingHandler(sender, e);

            string text = "";
            if (!DataClearingFlag)
            {
                DateTime dt = dt_DatePayRest.Value.Date;
                if (dt != InitDatePayRest && dt.Year > 2010) text = dt.ToString("dd.MM.yy");
            }
            txt_DatePayRest.Text = text;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Controls events
        //====================================================================================================
        #region Commands events
        //----------------------------------------------------------------------------------------------------
        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileForm = new OpenFileDialog();

            System.Windows.Forms.DialogResult res = FileForm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                BillPathIsValid = true;
                string path = FileForm.FileName;
                if (!string.IsNullOrEmpty(path))
                {
                    if (System.IO.File.Exists(path))
                    {
                        BillFilePath = path;
//                        txt_FilePath.Text = BillFilePath;
                        BillFileName = BillFilePath.Substring(BillFilePath.LastIndexOf('\\') + 1);
                        BillFileFlag = true;

                        string file_name = path;
                        if (!string.IsNullOrEmpty(path))
                            file_name = Path.GetFileName(file_name);
                        //file_name = path.Substring(path.LastIndexOf("\\") + 1);
                        txt_FileName.Text = file_name;
                    }
                    else
                    {
                        BillPathIsValid = false;
                        //MessageBox.Show("Файл не найден. (" + path + ")");
                    }
                }
            }

            FileForm.Dispose();
        }

        //----------------------------------------------------------------------------------------------------
        private void lblBack_Click(object sender, EventArgs e)
        {
            string number = txt_RegNumber.Text;
            //SetInsertMode(false);
            if (ctlTabs.SelectedIndex != 0) ctlTabs.SelectedIndex = 0;
            Reset_Bill();
            if (!string.IsNullOrEmpty(number)) txt_RegNumber.Text = number;
        }

        //----------------------------------------------------------------------------------------------------
        private void lblClear_Click(object sender, EventArgs e)
        {
            Reset_Bill();
        }

        //----------------------------------------------------------------------------------------------------
        private void btn_Clear_1_Click(object sender, EventArgs e)
        {
            Reset_Bill();
        }

        //----------------------------------------------------------------------------------------------------
        private void btn_Clear_2_Click(object sender, EventArgs e)
        {
            Reset_Bill();
        }

        //----------------------------------------------------------------------------------------------------
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Reset_Bill();
        }

        //----------------------------------------------------------------------------------------------------
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (!Read_RegNumber())
            {
                string status = "Регистрационный номер <" + txt_RegNumber.Text + "> не подходит.";
                status = status + (char)13 + (char)10 + "Введите корректный номер.";
                SetStatus(status);
                return;
            }

            Search_Bill();
        }

        //----------------------------------------------------------------------------------------------------
        private void btnLink_Click(object sender, EventArgs e)
        {
            Dogovor = DgCode;
            Bron = _bronNumber;
            DataEditingHandler(sender, e);
            AccordControls();
        }

        //----------------------------------------------------------------------------------------------------
        private void btnReload_Click(object sender, EventArgs e)
        {
            if (ReloadCallback == null) return;
            if (string.IsNullOrEmpty(Dogovor)) return;

            ReloadCallback(Dogovor, RegNumber);
        }

        //----------------------------------------------------------------------------------------------------
        private void lblLink_Click(object sender, EventArgs e)
        {
            Dogovor = DgCode;
            Bron = _bronNumber;
            DataEditingHandler(sender, e);
            AccordControls();
        }

        //----------------------------------------------------------------------------------------------------
        private void lblReload_Click(object sender, EventArgs e)
        {
            if (ReloadCallback == null) return;
            if (string.IsNullOrEmpty(Dogovor)) return;

            ReloadCallback(Dogovor, RegNumber);
        }

        //----------------------------------------------------------------------------------------------------
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (_dlKey == null)
                return;
            
            if (!CheckSaveData()) return;

            //if (MessageBox.Show("Данные  будут  внесены  в  журнал  счетов.", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            if (MessageBox.Show("Данные  будут  внесены  в  журнал  счетов." + (char)13 + (char)10 + "Вы уверены, что внесли данные корректно ?", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            //if (_updateVaucher != null)
            //    _updateVaucher.Invoke(serviceType: Service.ServType);

            Save_Bill();

            DataSavedFlag = true;
            btn_Clear.Focus();

            AccordControls();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Commands events
        //====================================================================================================
        #region Data finctions
        //----------------------------------------------------------------------------------------------------
        private void InitData()
        {
            dt_DateBill.Format = DateTimePickerFormat.Custom;
            dt_DateBill.CustomFormat = "dd.MM.yyyy";
            dt_DatePayDeposit.Format = DateTimePickerFormat.Custom;
            dt_DatePayDeposit.CustomFormat = "dd.MM.yyyy";
            dt_DatePayRest.Format = DateTimePickerFormat.Custom;
            dt_DatePayRest.CustomFormat = "dd.MM.yyyy";

            MinDate = DateTime.Now.AddYears(-3);
            MaxDate = DateTime.Now.AddYears(5);
            DateTime ndt = DateTime.Now.Date;
            InitDateBill = ndt;
            ndt = ndt.AddDays(1 - ndt.Day);
            InitDatePayDeposit = ndt.AddMinutes(10);
            InitDatePayRest = ndt.AddMinutes(10);

            BillDate = InitDateBill;
            DatePayDeposit = InitDatePayDeposit;
            DatePayRest = InitDatePayRest;

            Config_XML conf = new Config_XML();
            string rate_keys = conf.Get_Value("appSettings", "RateKeys");
            if (string.IsNullOrEmpty(rate_keys)) rate_keys = "38,14,37";

            List<string> RateList = WorkWithData.GetRateList(rate_keys);
            int rate_index = -1;
            for (int i = 0; i < RateList.Count; i++)
            {
                string rate = RateList[i];
                lst_Rate.Items.Add(rate);
                if (DgRate == rate) rate_index = i;
            }
            if (rate_index >= 0) lst_Rate.SelectedIndex = rate_index;

            InMode = InterMode.Start;

            AccordControls();
        }

        //----------------------------------------------------------------------------------------------------
        private bool Read_Int_Field(string text, ref int field)
        {
            field = 0;
            if (string.IsNullOrEmpty(text)) return false;

            try
            {
                field = int.Parse(text);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                field = 0;
                return false;
            }

            if (field <= 0) return false;

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        private string NormDec(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            if (DecSign == '.') return text.Replace(',', '.');
            if (DecSign == ',') return text.Replace('.', ',');

            return text;
        }

        //----------------------------------------------------------------------------------------------------
        private bool Read_Double_Field(string text, ref double field)
        {
            field = 0;
            if (string.IsNullOrEmpty(text)) return false;

            text = NormDec(text);

            try
            {
                field = double.Parse(text);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                field = 0;
                return false;
            }

            if (field <= 0) return false;

            return true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Data finctions
        //====================================================================================================
        #region Data procedures
        //----------------------------------------------------------------------------------------------------
        private bool Read_RegNumber()
        {
            RegNumberIsValid = Read_Int_Field(txt_RegNumber.Text, ref RegNumber);

            return RegNumberIsValid;
        }

        //----------------------------------------------------------------------------------------------------
        private bool Read_BillNumber()
        {
            BillNumber = "";
            int number = 0;
            string num = "";

            //bool bValid = Read_Int_Field(txt_BillNumber.Text, ref number);
            //if (bValid && number > 0)
            //{
            //    BillNumber = number.ToString();
            //}

            bool bValid = true;
            num = txt_BillNumber.Text;
            if (bValid && !string.IsNullOrEmpty(num))
            {
                BillNumber = num;
            }

            return bValid;
        }

        //----------------------------------------------------------------------------------------------------
        private bool Check_Data()
        {
            DataValidFlag = false;
            bool IsValid = true;
            string status = "";

            BillNumberIsValid = Read_BillNumber();
            SummaIsValid = Read_Double_Field(txt_Summa.Text, ref Summa);
            SummaDepositIsValid = Read_Double_Field(txt_SummaDeposit.Text, ref SummaDeposit);
            SummaRestIsValid = Read_Double_Field(txt_SummaRest.Text, ref SummaRest);

            if (!BillNumberIsValid)
            {
                if (string.IsNullOrEmpty(txt_BillNumber.Text))
                {
                    status = "Введите номер счета партнера.";
                }
                else
                {
                    status = "Номер счета партнера <" + txt_BillNumber.Text + "> не подходит.";
                    status = status + (char)13 + (char)10 + "Введите корректный номер.";
                }
                if (IsValid) DataStatus = status;
                IsValid = false;
            }
            if (!SummaIsValid)
            {
                if (string.IsNullOrEmpty(txt_Summa.Text))
                {
                    status = "Введите сумму к оплате.";
                }
                else
                {
                    status = "Сумма к оплате <" + txt_Summa.Text + "> не подходит.";
                    status = status + (char)13 + (char)10 + "Введите корректную сумму.";
                }
                if (IsValid) DataStatus = status;
                IsValid = false;
            }
            if (!SummaDepositIsValid)
            {
                if (string.IsNullOrEmpty(txt_SummaDeposit.Text))
                {
                    status = "Введите сумму предоплаты.";
                }
                else
                {
                    status = "Сумма предоплаты <" + txt_SummaDeposit.Text + "> не подходит.";
                    status = status + (char)13 + (char)10 + "Введите корректную сумму.";
                }
                if (IsValid) DataStatus = status;
                IsValid = false;
            }

            BillDate = dt_DateBill.Value;

            RateCode = lst_Rate.SelectedItem.ToString();

            bool bValid = true;
            DatePayDeposit = dt_DatePayDeposit.Value;
            if (DatePayDeposit == InitDatePayDeposit)
            {
                bValid = false;
                status = "Введите дату предоплаты.";
            }
            else
            {
                //if (DatePayDeposit < MinDate)
                //{
                //    bValid = false;
                //    status = "Введите корректную дату предоплаты ( с " + MinDate.Year.ToString() + " года).";
                //}
                //if (DatePayDeposit > MaxDate)
                //{
                //    bValid = false;
                //    status = "Введите корректную дату предоплаты ( по " + MaxDate.Year.ToString() + " год).";
                //}
            }
            DatePayDepositIsValid = bValid;
            if (IsValid && !bValid)
            {
                DataStatus = status;
                IsValid = false;
            }
            if (!SummaRestIsValid)
            {
                if (string.IsNullOrEmpty(txt_SummaRest.Text))
                {
                    status = "Введите сумму конечной оплаты.";
                }
                else
                {
                    status = "Сумма конечной оплаты <" + txt_SummaRest.Text + "> не подходит.";
                    status = status + (char)13 + (char)10 + "Введите корректную сумму.";
                }
                if (IsValid) DataStatus = status;
                IsValid = false;
            }
            bValid = true;
            DatePayRest = dt_DatePayRest.Value;
            if (DatePayRest == InitDatePayRest)
            {
                bValid = false;
                status = "Введите дату конечной оплаты.";
            }
            else
            {
                //if (DatePayRest < MinDate)
                //{
                //    bValid = false;
                //    status = "Введите корректную дату конечной оплаты ( с " + MinDate.Year.ToString() + " года).";
                //}
                //if (DatePayRest > MaxDate)
                //{
                //    bValid = false;
                //    status = "Введите корректную дату конечной оплаты ( по " + MaxDate.Year.ToString() + " год).";
                //}
            }
            DatePayRestIsValid = bValid;
            if (IsValid && !bValid)
            {
                DataStatus = status;
                IsValid = false;
            }
            Subject = txt_Subject.Text;
            Comment = txt_Comment.Text;

            DogovorIsValid = true;
            if (!string.IsNullOrEmpty(DgCode))
            {
                if (string.IsNullOrEmpty(Dogovor))
                {
                    Dogovor = DgCode;
                    RateCode = DgRate;
                    Bron = _bronNumber;
                }
                else
                {
                    DogovorIsValid = (Dogovor == DgCode);
                }
            }

            //BillPathIsValid = true;
            //if (!string.IsNullOrEmpty(BillFilePath))
            //{
            //    BillPathIsValid = System.IO.File.Exists(BillFilePath);
            //}

            if (IsValid)
            {
                DataValidFlag = true;
                DataStatus = "После заполнения полей подтвердите сохранение данных.";
            }
            else
            {

            }
            //DataStatus = "Письмо с регистрационным номером " + RegNumber.ToString() + " найдено.";
            //DataStatus = DataStatus + (char)13 + (char)10 + "Заполните поля и подтвердите сохранение данных.";

            return DataValidFlag;
        }

        //----------------------------------------------------------------------------------------------------
        private void Clear_Data(bool number_flag)
        {
            DataClearingFlag = true;

            if (number_flag) RegNumber = 0;
            BillNumber = "";
            //BillDate = DateTime.Now.Date;
            BillDate = InitDateBill;
            Summa = 0;
            SummaDeposit = 0;
            DatePayDeposit = InitDatePayDeposit;
            SummaRest = 0;
            DatePayRest = InitDatePayRest;
            Dogovor = "";
            Dogovor = DgCode;
            RateCode = DgRate;
            Bron = _bronNumber;
            BillFileKey = 0;
            BillFilePath = "";
            BillFileName = "";
            BillFileFlag = false;
            Subject = "";
            Comment = "";

            BillNumberIsValid = false;
            SummaIsValid = false;
            SummaDepositIsValid = false;
            SummaRestIsValid = false;
            DatePayDepositIsValid = false;
            DatePayRestIsValid = false;
            DogovorIsValid = true;
            BillPathIsValid = true;

            NumberFoundFlag = false;

            Reflect_Data();

            DataClearingFlag = false;
        }

        //----------------------------------------------------------------------------------------------------
        private void Reflect_Data()
        {
            DataNotEditingFlag = true;

            txt_RegNumber.Text = "";
            txt_BillNumber.Text = "";
            dt_DateBill.Value = InitDateBill;
            txt_Summa.Text = "";
            txt_SummaDeposit.Text = "";
            dt_DatePayDeposit.Value = InitDatePayDeposit;
            txt_SummaRest.Text = "";
            dt_DatePayRest.Value = InitDatePayRest;
            //txt_Dogovor.Text = "";
            //txt_FilePath.Text = "";
            txt_FileName.Text = "";
            txt_Subject.Text = "";
            txt_Comment.Text = "";
            lblReload.Text = "Перейти  в  путевку";

            if (RegNumber > 0) txt_RegNumber.Text = RegNumber.ToString();
            if (!string.IsNullOrEmpty(BillNumber)) txt_BillNumber.Text = BillNumber;
            if (BillDate > dt_DateBill.MinDate && BillDate < dt_DateBill.MaxDate)
                dt_DateBill.Value = BillDate;
            if (Summa > 0) txt_Summa.Text = Summa.ToString();
            if (SummaDeposit > 0) txt_SummaDeposit.Text = SummaDeposit.ToString();
            if (DatePayDeposit > dt_DatePayDeposit.MinDate && DatePayDeposit < dt_DatePayDeposit.MaxDate)
                dt_DatePayDeposit.Value = DatePayDeposit;
            if (SummaRest > 0) txt_SummaRest.Text = SummaRest.ToString();
            if (DatePayRest > dt_DatePayRest.MinDate && DatePayRest < dt_DatePayRest.MaxDate)
                dt_DatePayRest.Value = DatePayRest;
            if (!string.IsNullOrEmpty(Dogovor))
            {
                lbl_Dogovor.Text = Dogovor; // txt_Dogovor.Text = Dogovor;
                lbl_Dogovor_2.Text = Dogovor;
                lblReload.Text = "Перейти  в  путевку  " + Dogovor;
            }
            if (!string.IsNullOrEmpty(BillFileName))
            {
                txt_FileName.Text = BillFileName;
                //string file_name = BillFilePath;
                //if (!string.IsNullOrEmpty(file_name)) file_name = file_name.Substring(file_name.LastIndexOf("\\") + 1);
                //txt_FileName.Text = file_name;
            }
            if (!string.IsNullOrEmpty(Subject)) txt_Subject.Text = Subject;
            if (!string.IsNullOrEmpty(Comment)) txt_Comment.Text = Comment;

            string rate = DgRate;
            if (!string.IsNullOrEmpty(RateCode)) rate = RateCode;
            for (int i = 0; i < lst_Rate.Items.Count; i++)
            {
                if (lst_Rate.Items[i].ToString() == rate)
                {
                    lst_Rate.SelectedIndex = i;
                }
            }

            DataNotEditingFlag = false;
        }

        //----------------------------------------------------------------------------------------------------
        private void Reset_Bill()
        {
            if (!NewBillFlag)
            {
                InMode = InterMode.Start;
            }
            else
            {
                InMode = InterMode.Adding;
            }

            //bool bFlag = DataNotEditingFlag;
            //DataNotEditingFlag = true;
            //chk_New.Checked = false;
            //DataNotEditingFlag = bFlag;

            //btn_Save.Focus();
            btn_Search.Focus();
            SetCheckStatus("");

            Clear_Data(true);

            AccordControls();

            if (!NewBillFlag)
            {
                this.AutoScrollPosition = new Point(0, 0);
                SetDataPanel(false);
                SetFilePanel(false);
                txt_RegNumber.Focus();
            }
        }

        //----------------------------------------------------------------------------------------------------
        private string ConcatList(string list, string text)
        {
            if (!string.IsNullOrEmpty(list) && !string.IsNullOrEmpty(text)) list = list + (char)13 + (char)10;
            return list + text;
        }

        //----------------------------------------------------------------------------------------------------
        private bool CheckSaveData()
        {
            string list = "";

            if (InsertModeFlag)
                if (!BillNumberIsValid) list = ConcatList(list, "номер  счета  партнера");
            if (!SummaIsValid) list = ConcatList(list, "сумма к оплате");
            if (!SummaDepositIsValid) list =  ConcatList(list, "сумма предоплаты");
            if (!SummaRestIsValid) list =  ConcatList(list, "сумма конечной оплаты");
            if (!DatePayDepositIsValid) list =  ConcatList(list, "дата предоплаты");
            if (!DatePayRestIsValid) list =  ConcatList(list, "дата конечной оплаты");
            //if (!DogovorIsValid) list =  ConcatList(list, "номер путевки");
            //if (!BillPathIsValid) list =  ConcatList(list, "прикрепленный файл");
            if (string.IsNullOrEmpty(BillFileName)) list = ConcatList(list, "прикрепленный файл");
            //if (string.IsNullOrEmpty(BillFilePath)) list = ConcatList(list, "прикрепленный файл");

            if (string.IsNullOrEmpty(list)) return true;

            string msg = "Данные внесены не полность. Введите поля: " + (char)13 + (char)10 + list;

            MessageBox.Show(msg, "Проверка данных", MessageBoxButtons.OK);

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Data procedures
        //====================================================================================================
        #region Procedures
        //----------------------------------------------------------------------------------------------------
        private bool Check_Mode()
        {
            if (NumberChangedFlag) Read_RegNumber();

            InterMode NewMode = InMode;
            bool bCheck = false;

            switch (InMode)
            {
                case InterMode.Start:
                    Clear_Data(true);
                    SetCheckStatus("");
                    SetStatus("Введите регистрационный номер письма и нажмите кнопку  [Найти_счет]");
                    NewMode = InterMode.EnterNumber;
                    break;

                case InterMode.EnterNumber:
                    if (RegNumberIsValid)
                    {
                        NumberSearchFlag = false;
                        NewMode = InterMode.CanSearch;
                    }
                    break;

                case InterMode.CanSearch:
                    if (RegNumberIsValid)
                    {
                        if (NumberSearchFlag)
                        {
                            if (NumberFoundFlag)
                            {
                                Check_Data();
                                NewMode = InterMode.Editing;
                            }
                            else
                            {
                                Clear_Data(false);
                                NewMode = InterMode.NotFound;
                            }
                        }
                    }
                    else
                    {
                        NewMode = InterMode.EnterNumber;
                    }
                    break;

                case InterMode.NotFound:
                    if (NumberChangedFlag)
                    {
                        NewMode = InterMode.EnterNumber;
                        bCheck = true;
                    }
                    else
                    {
                        //-if (chk_New.Checked)
                        if (InsertModeFlag)
                        {
                            Dogovor = DgCode;
                            RateCode = DgRate;
                            Bron = _bronNumber;

                            Reflect_Data();

                            string status = "Письмо с регистрационным номером " + RegNumber.ToString() + " не найдено.";
                            status = status + (char)13 + (char)10 + "Заполните поля и подтвердите создание новой записи.";
                            SetStatus(status);
                            NewMode = InterMode.Adding;
                        }
                    }
                    break;

                case InterMode.Editing:
                    // check data valid
                    if (DataEditedFlag)
                    {
                        Check_Data();
                        if (!string.IsNullOrEmpty(DataStatus))
                            SetStatus(DataStatus);
                    }
                    if (DataSavedFlag)
                    {
                        SetStatus("Данные сохранены (регистрационный номер " + RegNumber.ToString() + ").");
                        NewMode = InterMode.Updated;
                    }
                    break;

                case InterMode.Adding:
                    //-if (!chk_New.Checked)
                    if (!InsertModeFlag)
                    {
                        Dogovor = "";
                        RateCode = "";
                        Bron = "";

                        Reflect_Data();

                        string status = "Письмо с регистрационным номером " + RegNumber.ToString() + " не найдено.";
                        status = status + (char)13 + (char)10 + "Введите номер и повторит поиск или внесите новую запись.";
                        SetStatus(status);
                        NewMode = InterMode.NotFound;
                    }
                    else
                    {
                        // check data valid
                        if (DataEditedFlag)
                        {
                            Check_Data();
                            if (!string.IsNullOrEmpty(DataStatus))
                                SetStatus(DataStatus);
                        }
                        if (DataSavedFlag)
                        {
                            SetStatus("Создана новая запись (регистрационный номер " + RegNumber.ToString() + ").");
                            NewMode = InterMode.Inserted;
                        }
                    }
                    break;

                case InterMode.Updated:

                    break;

                case InterMode.Inserted:

                    break;

                default:
                    NewMode = InterMode.EnterNumber;
                    break;
            }

            NumberChangedFlag = false;
            NumberSearchFlag = false;
            DataEditedFlag = false;
            DataSavedFlag = false;

            if (NewMode != InMode)
            {
                InMode = NewMode;
                if (bCheck) Check_Mode();
                return true;
            }

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        private void Search_Bill()
        {
            NumberSearchFlag = true;
            NumberFoundFlag = false;

            Load_Bill();

            AccordControls();

            if (NumberFoundFlag)
            {
                SetCheckStatus("для  этого  №  есть  данные");
                SetDataPanel(true);
                SetFilePanel(false);
                if (DogovorIsValid)
                {
                    txt_Summa.Focus();
                }
                else
                {
                    btnLink.Focus();
                }
            }
            else
            {
                SetCheckStatus("по  этому  №  данных  нет");
                SetDataPanel(false);
                SetFilePanel(false);
                txt_RegNumber.Focus();
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void Load_Bill()  // read bill data 
        {
            Clear_Data(false);

            if (RegNumber <= 0) return;

            string query = @"SELECT [BFE_KEY],[BFE_Number],[BFE_Date],[BFE_Name],[BFE_EmailDate],[BFE_EmailFrom],[BFE_EmailTo]";
            query = query + ",[BFE_Identity],[BFE_FileKey],[BFE_FileName],[BFE_Type],[BFE_StatusKey],[BFE_LastUserKey]";
            query = query + ",[BFE_LastUpdateDate],[BFE_MessageKey],[BFE_Archive]";
            query = query + ",[BPMK_KEY],IsNull([BPMK_Number],'') AS [BPMK_Number],[BPMK_Date],[BPMK_Bron]";
            query = query + ",IsNull([BPMK_Sum],0) AS [BPMK_Sum],[BPMK_SumRate],[BPMK_Rate]";
            query = query + ",[BPMK_SumDoplata],[BPMK_Partner],[BPMK_BFEKey],[BPMK_DateTour],[BPMK_Comiss],[BPMK_BronNew]";
            query = query + ",[BPMK_Course],[BPMK_PayPurpose],IsNull([BPMK_Dogovor],'') AS [BPMK_Dogovor],IsNull([BPMK_SumDeposit],0) AS [BPMK_SumDeposit],IsNull([BPMK_DatePayDeposit],'2000.01.01') AS [BPMK_DatePayDeposit]";
            query = query + ",IsNull([BPMK_SumRest],0) AS [BPMK_SumRest],IsNull([BPMK_DatePayRest],'2000.01.01') AS [BPMK_DatePayRest]";
            query = query + ",IsNull([BPMK_FilePath],'') AS [BPMK_FilePath],IsNull([BPMK_Subject],'') AS [BPMK_Subject],IsNull([BPMK_Comment],'') AS [BPMK_Comment]";
            query = query + "  FROM [dbo].[FIN_BillsFromEmail]";
            query = query + "    LEFT JOIN [dbo].[FIN_BillsParseMKFromEmail]";
            query = query + "	   ON [FIN_BillsFromEmail].[BFE_KEY] = [FIN_BillsParseMKFromEmail].[BPMK_BFEKey]";
            query = query + "  WHERE [BFE_KEY] = @key";

            using (var adapter = new SqlDataAdapter(query, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@key", RegNumber);
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    NumberFoundFlag = true;

                    string status = "Письмо с регистрационным номером " + RegNumber.ToString() + " найдено.";
                    status = status + (char)13 + (char)10 + "Заполните поля и подтвердите сохранение данных.";
                    SetStatus(status);

                    try
                    {
                        Dogovor = dt.Rows[0].Field<string>("BPMK_Dogovor");
                        Bron = dt.Rows[0].Field<string>("BPMK_Bron");
                        BillNumber = dt.Rows[0].Field<string>("BPMK_Number");
                        BillDate = dt.Rows[0].Field<DateTime>("BFE_Date");
                        RateCode = dt.Rows[0].Field<string>("BPMK_Rate");
                        Summa = dt.Rows[0].Field<double>("BPMK_Sum");
                        Partner = dt.Rows[0].Field<int>("BPMK_Partner");
                        SummaDeposit = dt.Rows[0].Field<double>("BPMK_SumDeposit");
                        DatePayDeposit = dt.Rows[0].Field<DateTime>("BPMK_DatePayDeposit");
                        SummaRest = dt.Rows[0].Field<double>("BPMK_SumRest");
                        DatePayRest = dt.Rows[0].Field<DateTime>("BPMK_DatePayRest");
                        //BillFilePath = dt.Rows[0].Field<string>("BPMK_FilePath");
                        BillFileName = dt.Rows[0].Field<string>("BFE_FileName");
                        BillFileKey = dt.Rows[0].Field<int>("BFE_FileKey");
                        //Subject = dt.Rows[0].Field<string>("BPMK_Subject");
                        Subject = dt.Rows[0].Field<string>("BFE_Name");
                        Comment = dt.Rows[0].Field<string>("BPMK_Comment");
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Load_Bill", ex);
                    }

                    if (string.IsNullOrEmpty(Dogovor))
                    {
                        Dogovor = DgCode;
                        RateCode = DgRate;
                        Bron = _bronNumber;
                    }
                    if (string.IsNullOrEmpty(Bron))
                    {
                        Bron = _bronNumber;
                    }
                    if (string.IsNullOrEmpty(RateCode))
                    {
                        RateCode = DgRate;
                    }
                }
                else
                {
                    NumberFoundFlag = false;

                    string status = "Письмо с регистрационным номером " + RegNumber.ToString() + " не найдено.";
                    status = status + (char)13 + (char)10 + "Введите номер и повторит поиск или внесите новую запись.";
                    SetStatus(status);
                }
            }

            Reflect_Data();
        }

        //----------------------------------------------------------------------------------------------------
        private void Save_Bill()  // write bill data to database
        {
            if (string.IsNullOrEmpty(Dogovor)) return;

            if (InMode == InterMode.Editing) // Update existing record
            {
                string query = @"UPDATE [dbo].[FIN_BillsParseMKFromEmail] SET ";
                query = query + " [BPMK_Number]=@billnumber";
                query = query + ",[BPMK_Sum]=@summa";
                query = query + ",[BPMK_SumDeposit]=@summadeposit";
                query = query + ",[BPMK_DatePayDeposit]=@dtdeposit";
                query = query + ",[BPMK_SumRest]=@summarest";
                query = query + ",[BPMK_DatePayRest]=@dtrest";
                query = query + ",[BPMK_Dogovor]=@dgcod";
                query = query + ",[BPMK_Rate]=@dgrate";
                query = query + ",[BPMK_Bron]=@bronnumber";
                //query = query + ",[BPMK_Subject]=@subject";
                query = query + ",[BPMK_Comment]=@comment";
                query = query + " WHERE [BPMK_BFEKey] = @regnumber";

                using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@billnumber", BillNumber);
                    com.Parameters.AddWithValue("@summa", Summa);
                    com.Parameters.AddWithValue("@summadeposit", SummaDeposit);
                    com.Parameters.AddWithValue("@dtdeposit", DatePayDeposit);
                    com.Parameters.AddWithValue("@summarest", SummaRest);
                    com.Parameters.AddWithValue("@dtrest", DatePayRest);
                    com.Parameters.AddWithValue("@dgcod", Dogovor);
                    com.Parameters.AddWithValue("@dgrate", RateCode);
                    com.Parameters.AddWithValue("@bronnumber", Bron);
                    //com.Parameters.AddWithValue("@subject", Subject);
                    com.Parameters.AddWithValue("@comment", Comment);
                    com.Parameters.AddWithValue("@regnumber", RegNumber);

                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Save_Bill", ex);
                    }
                }

                string qry = @"UPDATE [dbo].[FIN_BillsFromEmail] SET ";
                qry = qry + " [BFE_Name]=@subject";
                qry = qry + ",[BFE_Date]=@billdate";
                qry = qry + ",[BFE_LastUserKey]=@userkey";
                qry = qry + " WHERE [BFE_Key] = @regnumber";

                using (SqlCommand com = new SqlCommand(qry, WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@regnumber", RegNumber);
                    com.Parameters.AddWithValue("@subject", Subject);
                    com.Parameters.AddWithValue("@billdate", BillDate);
                    com.Parameters.AddWithValue("@userkey", UserKey);
                    
                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Save_Bill", ex);
                    }
                }

                if (BillFileFlag) Save_BillFile();
            }

            //----------------------------------------------------------------------------------------------------
            if (InMode == InterMode.Adding)  // Insert new record
            {
                int PartnerType = 3;
                if (_partnerKey > 0)
                {
                    string sel_qry = "SELECT BPMK_Partner,[BFE_Type], COUNT([BFE_KEY]) AS BFE_COUNT ";
                    sel_qry = sel_qry + "  FROM [dbo].[FIN_BillsFromEmail] ";
                    sel_qry = sel_qry + "    LEFT JOIN [dbo].[FIN_BillsParseMKFromEmail] ";
                    sel_qry = sel_qry + "      ON  [BPMK_BFEKey] =  [BFE_KEY] ";
                    sel_qry = sel_qry + "  WHERE  BPMK_Partner=@partnerkey ";
                    sel_qry = sel_qry + "  GROUP BY BPMK_Partner,[BFE_Type] ";
                    sel_qry = sel_qry + "  ORDER BY BFE_COUNT DESC";

                    using (var adapter = new SqlDataAdapter(sel_qry, WorkWithData.Connection))
                    {
                        try
                        {
                            var dt = new DataTable();
                            adapter.SelectCommand.Parameters.AddWithValue("@partnerkey", _partnerKey);
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                PartnerType = dt.Rows[0].Field<int>("BFE_Type");
                            }
                        }
                        catch (System.Exception ex)
                        {
                            TpLogger.Debug("Save_Bill", ex);
                        }
                    }
                }

                int reg_number = 0;
                string query = @"INSERT INTO [dbo].[FIN_BillsFromEmail] (";
                query = query + @"[BFE_Number],[BFE_Name],[BFE_Date],[BFE_Type],[BFE_StatusKey],[BFE_LastUserKey]";
                query = query + @") VALUES (";
                query = query + @" (SELECT MAX([BFE_Number])+1 FROM [dbo].[FIN_BillsFromEmail]), ";
                //query = query + @" 'Inserted by user '+SUSER_SNAME() )";
                query = query + @" @subject, @billdate, @partnertype, 1, @userkey )";
                query = query + @" SELECT CONVERT(integer, scope_identity())";
                using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@subject", Subject);
                    com.Parameters.AddWithValue("@billdate", BillDate);
                    com.Parameters.AddWithValue("@partnertype", PartnerType);
                    com.Parameters.AddWithValue("@userkey", UserKey);

                    try
                    {
                        reg_number = (int)com.ExecuteScalar();
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Save_Bill", ex);
                    }
                }
                if (reg_number <= 0)
                {
                    SetStatus("Регистрационный номер для новой записи не получен.");
                    return;
                }

                RegNumber = reg_number;
                txt_RegNumber.Text = RegNumber.ToString();

                query = @"INSERT INTO [dbo].[FIN_BillsParseMKFromEmail] (";
                query = query + " [BPMK_BFEKey],[BPMK_Number],[BPMK_Rate]";
                query = query + ",[BPMK_Sum],[BPMK_SumRate]";
                query = query + ",[BPMK_SumDoplata],[BPMK_Partner]";
                query = query + ",[BPMK_SumDeposit],[BPMK_DatePayDeposit]";
                query = query + ",[BPMK_SumRest],[BPMK_DatePayRest]";
                //query = query + ",[BPMK_Subject]";
                query = query + ",[BPMK_Dogovor],[BPMK_Bron],[BPMK_Comment]";
                query = query + ") VALUES (";
                query = query + "@regnumber, @billnumber, @ratecode";
                query = query + ", @summa, 0";
                query = query + ", 0, @partner";
                query = query + ", @summadeposit, @dtdeposit";
                query = query + ", @summarest, @dtrest";
                //query = query + ", @subject";
                query = query + ", @dgcod, @bronnumber, @comment)";
                using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@regnumber", RegNumber);
                    com.Parameters.AddWithValue("@billnumber", BillNumber);
                    com.Parameters.AddWithValue("@ratecode", RateCode);
                    com.Parameters.AddWithValue("@summa", Summa);
                    com.Parameters.AddWithValue("@partner", _partnerKey);
                    com.Parameters.AddWithValue("@summadeposit", SummaDeposit);
                    com.Parameters.AddWithValue("@dtdeposit", DatePayDeposit);
                    com.Parameters.AddWithValue("@summarest", SummaRest);
                    com.Parameters.AddWithValue("@dtrest", DatePayRest);
                    //com.Parameters.AddWithValue("@subject", Subject);
                    com.Parameters.AddWithValue("@dgcod", Dogovor);
                    com.Parameters.AddWithValue("@bronnumber", Bron);
                    com.Parameters.AddWithValue("@comment", Comment);

                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Save_Bill", ex);
                    }
                }

                if (BillFileFlag) Save_BillFile();
            }
        }

        //----------------------------------------------------------------------------------------------------
        private void Save_BillFile()  // write bill file to database
        {
            if (string.IsNullOrEmpty(BillFilePath)) return;
            if (!System.IO.File.Exists(BillFilePath))
            {
                SetStatus("Файл не найден. (" + BillFilePath + ")");
                //MessageBox.Show("Файл не найден. (" + BillFilePath + ")");
                return;
            }

            if (RegNumber <= 0)
            {
                SetStatus("Не удалось сохранить файл в базе (регистрационный номер не определен).");
                return;
            }

            //----------------------------------------------------------------------------------------------------
            if (BillFileKey <= 0)
            {
                string ins_qry = @"INSERT INTO [dbo].[FIN_BlobFiles] ([BF_FileName]) VALUES (@filename)";
                ins_qry = ins_qry + @" SELECT CONVERT(integer, scope_identity())";
                using (SqlCommand com = new SqlCommand(ins_qry, WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@filename", BillFileName);

                    try
                    {
                        //com.ExecuteNonQuery();
                        BillFileKey = (int)com.ExecuteScalar();
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Save_BillFile", ex);
                    }
                }

                if (BillFileKey > 0)
                {
                    string upd_qry = @"UPDATE [dbo].[FIN_BillsFromEmail] SET ";
                    upd_qry = upd_qry + " [BFE_FileKey]=@filekey";
                    upd_qry = upd_qry + " ,[BFE_FileName]=@filename";
                    upd_qry = upd_qry + " WHERE [BFE_Key] = @regnumber";

                    using (SqlCommand com = new SqlCommand(upd_qry, WorkWithData.Connection))
                    {
                        com.Parameters.AddWithValue("@regnumber", RegNumber);
                        com.Parameters.AddWithValue("@filekey", BillFileKey);
                        com.Parameters.AddWithValue("@filename", BillFileName);

                        try
                        {
                            com.ExecuteNonQuery();
                        }
                        catch (System.Exception ex)
                        {
                            TpLogger.Debug("Save_BillFile", ex);
                        }
                    }
                }
            }

            if (BillFileKey <= 0)
            {
                SetStatus("Не удалось сохранить файл в базе.");
                return;
            }

            //----------------------------------------------------------------------------------------------------
            byte[] fileByteArray = File.ReadAllBytes(BillFilePath);

            String query = "UPDATE [dbo].[FIN_BlobFiles] ";
            query = query + " SET [BF_FileName] = @filename";
            query = query + " ,[BF_File] = @filedata";
            query = query + " WHERE [BF_KEY] = @filekey";

            int res = 0;
            using (SqlCommand cmd = new SqlCommand(query, WorkWithData.Connection))
            {
                cmd.Parameters.Add("@filename", System.Data.SqlDbType.NVarChar, 150).Value = BillFileName;
                cmd.Parameters.Add("@filedata", System.Data.SqlDbType.VarBinary).Value = fileByteArray;
                cmd.Parameters.AddWithValue("@filekey", BillFileKey);

                try
                {
                    res = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    TpLogger.Debug("Save_BillFile", ex);
                }
            }

            query = @"UPDATE [dbo].[FIN_BillsFromEmail] SET ";
            query = query + " [BFE_FileName]=@filename";
            query = query + " WHERE [BFE_Key] = @regnumber";

            using (SqlCommand cmd = new SqlCommand(query, WorkWithData.Connection))
            {
                cmd.Parameters.Add("@filename", System.Data.SqlDbType.NVarChar, 150).Value = BillFileName;
                cmd.Parameters.AddWithValue("@regnumber", RegNumber);

                try
                {
                    res = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    TpLogger.Debug("Save_BillFile", ex);
                }
            }

        }

        //----------------------------------------------------------------------------------------------------
        private void Save_BillFile_Old()  // write bill file to database
        {
            if (string.IsNullOrEmpty(BillFilePath)) return;
            if (!System.IO.File.Exists(BillFilePath))
            {
                //MessageBox.Show("Файл не найден. (" + path + ")");
                return;
            }
            //            return;
            // adjust procedure for real work

            byte[] fileByteArray = File.ReadAllBytes(BillFilePath);

            String query = "UPDATE [dbo].[FIN_BillsParseMKFromEmail] ";
            query = query + " SET [BPMK_FilePath] = @filepath";
            query = query + " ,[BPMK_FileData] = @filedata";
            query = query + " WHERE [BPMK_BFEKey] = @regnumber";

            int res = 0;
            using (SqlCommand cmd = new SqlCommand(query, WorkWithData.Connection))
            {
                cmd.Parameters.Add("@filepath", System.Data.SqlDbType.NVarChar, 150).Value = BillFilePath;
                cmd.Parameters.Add("@filedata", System.Data.SqlDbType.VarBinary).Value = fileByteArray;
                cmd.Parameters.AddWithValue("@regnumber", RegNumber);

                try
                {
                    res = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    TpLogger.Debug("Save_BillFile", ex);
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Procedures
        //====================================================================================================
    }
}
