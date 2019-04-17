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
//using terms_prepaid.Helpers;
//using WpfControlLibrary.Util;


namespace terms_prepaid.UserControls
{
    public delegate void ReloadOptionsForm(string DgCode, int RegNumber);

    public enum InterMode
    {
        Start = 0,
        EnterNumber = 1,
        CanSearch = 2,
        NotFound = 3,
        Editing = 4,
        Adding = 5,
        Updated = 6,
        Inserted = 7
    }

    public partial class ucBillSetting : UserControl, IBillSetting
    {
        public delegate void UpdateVaucher(bool aviaErrorCallback = false, ServiceType serviceType = ServiceType.Unknow);

        private UpdateVaucher _updateVaucher;

        public enum ModeType { vaucher, service };

        private readonly Color _colorEnabled = Color.White;
        private readonly Color _colorDisabled= Color.Silver;

        private decimal Cost { get; set; }

        private string _dgcode = string.Empty;
        private int? _dlKey;

        private DataRow _rowDogovor = null;

        private IContractService _serv;

        public Service Service { get; private set; }

        private Label _serviceTitle;

        private VoucherViewModel _voucherViewModel;

        private DateTime _minDate = new DateTime(1900, 1, 1);

        //--------------------------------------------------------------------------------
        private string DgCode = "";
        //private int DlKey = 0;
        private int RegNumber = 0;
        private int PresetRegNumber = 0;

        private string BillNumber = "";
        private double Summa = 0;
        private double SummaDeposit = 0;
        private double SummaRest = 0;
        private DateTime DatePayDeposit;
        private DateTime DatePayRest;
        private string Dogovor = "";
        private string Comment = "";
        private string BillFilePath = "";

        private bool RegNumberIsValid;
        private bool BillNumberIsValid;
        private bool SummaIsValid;
        private bool SummaDepositIsValid;
        private bool SummaRestIsValid;
        private bool DatePayDepositIsValid;
        private bool DatePayRestIsValid;
        private bool DogovorIsValid;
        private bool BillPathIsValid;
        private bool ConfirmFlag;

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

        private InterMode InMode;

        char DecSign = '.';

        private Timer FocusTime;

        ReloadOptionsForm ReloadCallback;

        //--------------------------------------------------------------------------------


        public ucBillSetting(UpdateVaucher updateVaucher, VoucherViewModel voucherViewModel, string iDgCode, ReloadOptionsForm iReloadCallback)
        {
            _serv = Repository.GetInstance<IContractService>();
            InitializeComponent();

            DecSign = '.';
            if ((1.2).ToString().IndexOf(',') > 0) DecSign = ',';

            _voucherViewModel = voucherViewModel;

            _serviceTitle = lbl_ServiceTitle;
            _serviceTitle.Visible = false;
            _serviceTitle.AutoSize = true;
            _serviceTitle.BackColor = Color.BurlyWood;
            _serviceTitle.Margin = new Padding(5, 5, 5, 5);
            _serviceTitle.Height = 20;
            _serviceTitle.Width = 500;
            _serviceTitle.Dock = DockStyle.Top;

            DgCode = iDgCode;
            if (voucherViewModel.Voucher != null) _dgcode = voucherViewModel.Voucher.DgCode;
            if (!string.IsNullOrEmpty(_dgcode)) DgCode = _dgcode;

            ReloadCallback = iReloadCallback;

            pBill.Dock = DockStyle.Top;
            _updateVaucher = updateVaucher;

            chk_Find.Checked = true;

            InitData();
            //AccordControls();

            ctlTabs.DrawMode = TabDrawMode.OwnerDrawFixed;
            ctlTabs.DrawItem += ctlTabs_DrawItem;

            SetDataPanel(false);
            SetFilePanel(false);

            txt_RegNumber.Focus();

            FocusTime = new Timer();
            FocusTime.Interval = 100;
            FocusTime.Tick += FocusTimer_Tick;

        }

        public void SetFocus()
        {
            SetFocus(0);
        }

        public void SetFocus(int iRegNumber)
        {
            if (iRegNumber > 0) PresetRegNumber = iRegNumber;

            FocusTime.Start();
        }

        private void FocusTimer_Tick(Object myObject, EventArgs myEventArgs)
        {
            FocusTime.Stop();
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

        private void ctlTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage tp = ctlTabs.TabPages[e.Index];
            Color color = ctlTabs.SelectedTab.Equals(tp) ? Color.Moccasin : Color.LightGray; // Beige

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

        private void SetDataPanel(bool visible_flag)
        {
            pUpdateData.Visible = visible_flag;
            if (visible_flag)
            {
                lblUpdateData.Text = "Заполните  данные  счета";
                picUpdateData.Image = Properties.Resources.ico_arrow_up.ToBitmap();

            }
            else
            {
                lblUpdateData.Text = "Заполните  данные  счета";
                picUpdateData.Image = Properties.Resources.ico_arrow_down.ToBitmap();
            }
        }
        private void SetFilePanel(bool visible_flag)
        {
            pUpdateFile.Visible = visible_flag;
            if (visible_flag)
            {
                lblUpdateFile.Text = "Прикрепите  файл  со  счетом";
                picUpdateFile.Image = Properties.Resources.ico_arrow_up.ToBitmap();
            }
            else
            {
                lblUpdateFile.Text = "Прикрепите  файл  со  счетом";
                picUpdateFile.Image = Properties.Resources.ico_arrow_down.ToBitmap();
            }

        }


        private void SetStatus(string status)
        {
            //txt_Status.Text = status;
            lbl_Status.Text = status;
        }

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

            return String.Format("{0} по {1}", baseName, additionName);
        }

        private void InitData()
        {
            dt_DatePayDeposit.Format = DateTimePickerFormat.Custom;
            dt_DatePayDeposit.CustomFormat = "dd.MM.yyyy";
            dt_DatePayRest.Format = DateTimePickerFormat.Custom;
            dt_DatePayRest.CustomFormat = "dd.MM.yyyy";

            MinDate = DateTime.Now.AddYears(-3);
            MaxDate = DateTime.Now.AddYears(5);
            InitDatePayDeposit = DateTime.Now.AddDays(1);
            InitDatePayRest = DateTime.Now.AddDays(10);
            DatePayDeposit = InitDatePayDeposit;
            DatePayRest = InitDatePayRest;
            dt_DatePayDeposit.Value = DatePayDeposit;
            dt_DatePayRest.Value = DatePayRest;

            InMode = InterMode.Start;

            AccordControls();
        }

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


        private string NormDec(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            if (DecSign == '.') return text.Replace(',', '.');
            if (DecSign == ',') return text.Replace('.', ',');

            return text;
        }


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


        private bool Read_RegNumber()
        {
            RegNumberIsValid = Read_Int_Field(txt_RegNumber.Text, ref RegNumber);

            return RegNumberIsValid;
        }


        private bool Read_BillNumber()
        {
            BillNumber = "";
            int number = 0;

            bool bValid = Read_Int_Field(txt_BillNumber.Text, ref number);

            if (bValid && number > 0)
            {
                BillNumber = number.ToString();
            }

            return bValid;
        }

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
            Comment = txt_Comment.Text;
            ConfirmFlag = chkConfirm.Checked;

            DogovorIsValid = true;
            if (!string.IsNullOrEmpty(DgCode))
            {
                if (string.IsNullOrEmpty(Dogovor)) 
                    Dogovor = DgCode;
                else
                    DogovorIsValid = (Dogovor == DgCode);
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

        private bool Check_Mode()
        {
            if (NumberChangedFlag) Read_RegNumber();

            InterMode NewMode = InMode;
            bool bCheck = false;

            switch (InMode)
            {
                case InterMode.Start:
                    Clear_Data(true);
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
                        if (chk_New.Checked)
                        {
                            Dogovor = DgCode;
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
                    if (!chk_New.Checked)
                    {
                        Dogovor = "";
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
                if (ctl.Name == "lbl_Dogovor") // txt_Dogovor
                {
                    //lbl_Bron.Visible = !valid_flag;
                    //lbl_Dogovor.Visible = !valid_flag;
                    //pWarning.Visible = !valid_flag;

                    if (valid_flag)
                    {
                        pMissmatch.Visible = false;
                        //lblWarning.Visible = false;
                        lblWarning.Text = "";
                    }
                    else
                    {
                        lblWarning.Text = "Привязанный  к  счету  номер  брони  не  совпадает!" + (char)13 + (char)10 + "Номер  брони  для  счета:   " + Dogovor;
                        //lblWarning.Visible = true;
                        pMissmatch.Visible = true;
                    }
                }
            }
        }

        private void AccordControls()
        {
            Check_Mode();

            bool bNumber = false;
            bool bPartner = false;
            bool bSearch = false;
            bool bNew = true;
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
                    //bNew = true;
                    break;

                case InterMode.Editing:
                    bEdit = true;
                    //if (DataValidFlag) bSave = true;
                    bSave = true;
                    break;

                case InterMode.Adding:
                    //bNew = true;
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
            if (chk_New.Checked) NewBillFlag = true;

            //txt_RegNumber.Enabled = bFind;
            //btn_Search.Enabled = bFind;


            txt_RegNumber.Enabled = bNumber;
            btn_Search.Enabled = bSearch;

            txt_RegNumber.Visible = !NewBillFlag;
            lbl_RegNumber.Visible = !NewBillFlag;
            btn_Search.Visible = !NewBillFlag;

            chk_New.Enabled = bNew;
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
            txt_Comment.Enabled = bEdit;
            chkConfirm.Enabled = bEdit;

            btn_Save.Enabled = bSave && ConfirmFlag;
            btn_Clear.Text = ClearText;

            SetControlColor(txt_RegNumber, RegNumberIsValid);

            SetControlColor(txt_RegNumber, RegNumberIsValid);
            SetControlColor(txt_BillNumber, BillNumberIsValid);
            SetControlColor(txt_Summa, SummaIsValid);
            SetControlColor(txt_SummaDeposit, SummaDepositIsValid);
            SetControlColor(txt_SummaRest, SummaRestIsValid);
            SetControlColor(dt_DatePayDeposit, DatePayDepositIsValid);
            SetControlColor(dt_DatePayRest, DatePayRestIsValid);
//            SetControlColor(txt_Dogovor, DogovorIsValid);
            SetControlColor(lbl_Dogovor, DogovorIsValid);
            SetControlColor(txt_FileName, BillPathIsValid);
        }

        private void Clear_Data(bool number_flag)
        {
            if (number_flag) RegNumber = 0;
            BillNumber = "";
            Summa = 0;
            SummaDeposit = 0;
            DatePayDeposit = InitDatePayDeposit;
            SummaRest = 0;
            DatePayRest = InitDatePayRest;
            Dogovor = "";
Dogovor = DgCode;
            BillFilePath = "";
            BillFileFlag = false;
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
            ConfirmFlag = false;

            Reflect_Data();
        }


        private void Reflect_Data()
        {
            DataNotEditingFlag = true;

            txt_RegNumber.Text = "";
            txt_BillNumber.Text = "";
            txt_Summa.Text = "";
            txt_SummaDeposit.Text = "";
            dt_DatePayDeposit.Value = InitDatePayDeposit;
            txt_SummaRest.Text = "";
            dt_DatePayRest.Value = InitDatePayRest;
            //txt_Dogovor.Text = "";
            //txt_FilePath.Text = "";
            txt_FileName.Text = "";
            txt_Comment.Text = "";
            chkConfirm.Checked = false;

            if (RegNumber > 0) txt_RegNumber.Text = RegNumber.ToString();
            if (!string.IsNullOrEmpty(BillNumber)) txt_BillNumber.Text = BillNumber;
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
            }
            if (!string.IsNullOrEmpty(BillFilePath))
            {
//                txt_FilePath.Text = BillFilePath;
                string file_name = BillFilePath;
                if (!string.IsNullOrEmpty(file_name)) file_name = file_name.Substring(file_name.LastIndexOf("\\") + 1);
                txt_FileName.Text = file_name;
            }
            if (!string.IsNullOrEmpty(Comment)) txt_Comment.Text = Comment;
            if (ConfirmFlag) chkConfirm.Checked = true;

            DataNotEditingFlag = false;
        }

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

                _serviceTitle.Text = Service.FullName ??
                                     (Service.ServType == ServiceType.Avia
                                         ? "Авиаперелет: " + ((AviaService) service).BookingAvia.Route
                                         : service.ServiceName);
                //_serviceTitle.Text = Service.FullName ?? (ServiceInfo.SType == ServiceType.Avia ? "Авиаперелет: " + ServiceInfo.dl_name : ServiceInfo.dl_name);

                _serviceTitle.Visible = true;
                
                //gbStatuses.Text = GetPanelName(_gbStatusesName, Service.SvKey, Service.TypeId);
///                gbFile.Text = GetPanelName(_gbFileName, Service.SvKey, Service.TypeId);
            }
            this.AutoScrollPosition = new Point(0, 0);

            Reset_Bill();
        }

        public void SetVoucher(int serviceCount)
        {
            _serviceTitle.Visible = false;

            //btn_Clear();

            Reset_Bill();
        }

        private void Search_Bill()
        {
            NumberSearchFlag = true;
            NumberFoundFlag = false;

            Load_Bill();

            AccordControls();

            if (NumberFoundFlag)
            {
                SetDataPanel(true);
                SetFilePanel(false);
                txt_Summa.Focus();
            }
            else
            {
                SetDataPanel(false);
                SetFilePanel(false);
                txt_RegNumber.Focus();
            }
        }

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
            query = query + ",IsNull([BPMK_SumRest],0) AS [BPMK_SumRest],IsNull([BPMK_DatePayRest],'2000.01.01') AS [BPMK_DatePayRest],IsNull([BPMK_FilePath],'') AS [BPMK_FilePath],IsNull([BPMK_Comment],'') AS [BPMK_Comment]";
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
                        BillNumber = dt.Rows[0].Field<string>("BPMK_Number");
                        Summa = dt.Rows[0].Field<double>("BPMK_Sum");
                        SummaDeposit = dt.Rows[0].Field<double>("BPMK_SumDeposit");
                        DatePayDeposit = dt.Rows[0].Field<DateTime>("BPMK_DatePayDeposit");
                        SummaRest = dt.Rows[0].Field<double>("BPMK_SumRest");
                        DatePayRest = dt.Rows[0].Field<DateTime>("BPMK_DatePayRest");
                        BillFilePath = dt.Rows[0].Field<string>("BPMK_FilePath");
                        Comment = dt.Rows[0].Field<string>("BPMK_Comment");
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Load_Bill", ex);
                    }

                    if (string.IsNullOrEmpty(Dogovor)) Dogovor = DgCode;
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
                    com.Parameters.AddWithValue("@regnumber", RegNumber);
                    com.Parameters.AddWithValue("@comment", Comment);
                    com.ExecuteNonQuery();
                }

                if (BillFileFlag) Save_BillFile();
            }

            if (InMode == InterMode.Adding)  // Insert new record
            {
                int reg_number = 0;
                string query = @"INSERT INTO [dbo].[FIN_BillsFromEmail] ([BFE_Name],[BFE_Number]) VALUES ('Inserted by user '+SUSER_SNAME(), 0) SELECT  CONVERT(integer, scope_identity())";
                using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
                {
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
                query = query + " [BPMK_BFEKey],[BPMK_Number]";
                query = query + ",[BPMK_Sum],[BPMK_SumRate],[BPMK_Rate]";
                query = query + ",[BPMK_SumDoplata],[BPMK_Partner]";
                query = query + ",[BPMK_SumDeposit],[BPMK_DatePayDeposit]";
                query = query + ",[BPMK_SumRest],[BPMK_DatePayRest]";
                query = query + ",[BPMK_Dogovor],[BPMK_Comment]";
                query = query + ") VALUES (";
                query = query + "@regnumber, @billnumber";
                query = query + ", @summa, 0, ''";
                query = query + ", 0, 0";
                query = query + ", @summadeposit, @dtdeposit";
                query = query + ", @summarest, @dtrest";
                query = query + ", @dgcod, @comment)";
                using (SqlCommand com = new SqlCommand(query, WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@regnumber", RegNumber);
                    com.Parameters.AddWithValue("@billnumber", BillNumber);
                    com.Parameters.AddWithValue("@summa", Summa);
                    com.Parameters.AddWithValue("@summadeposit", SummaDeposit);
                    com.Parameters.AddWithValue("@dtdeposit", DatePayDeposit);
                    com.Parameters.AddWithValue("@summarest", SummaRest);
                    com.Parameters.AddWithValue("@dtrest", DatePayRest);
                    com.Parameters.AddWithValue("@dgcod", Dogovor);
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

        private void Save_BillFile()  // write bill file to database
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



        private void flowLayoutPanel1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            
        }

        private void UpdateValues()
        {


        }

        private void DataEditingHandler(object sender, EventArgs e)
        {
            if (DataNotEditingFlag) return;

            DataEditedFlag = true;
            AccordControls();
        }

        private void txt_RegNumber_TextChanged(object sender, EventArgs e)
        {
            if (DataNotEditingFlag) return;

            NumberChangedFlag = true;
            AccordControls();
        }

        private void chk_Find_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chk_New_CheckedChanged(object sender, EventArgs e)
        {
            if (DataNotEditingFlag) return;

            if (chk_New.Checked)
            {
                Clear_Data(true);
                Dogovor = DgCode;
                Reflect_Data();
                SetStatus("Заполните поля и подтвердите создание новой записи.");
                InMode = InterMode.Adding;
            }
            else
            {
                Clear_Data(true);
                InMode = InterMode.Start;
            }
            AccordControls();
        }

        private void chk_Find_Click(object sender, EventArgs e)
        {
            if (chk_Find.Checked)
            {
                chk_New.Checked = false;
            }
            else
            {
                chk_Find.Checked = true;
            }
            AccordControls();
        }

        private void chk_New_Click(object sender, EventArgs e)
        {
            if (chk_New.Checked)
            {
                chk_Find.Checked = false;
            }
            else
            {
                chk_New.Checked = true;
            }
            AccordControls();
        }

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

        private void btn_Clear_1_Click(object sender, EventArgs e)
        {
            Reset_Bill();
        }

        private void btn_Clear_2_Click(object sender, EventArgs e)
        {
            Reset_Bill();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Reset_Bill();
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            Dogovor = DgCode;
            DataEditingHandler(sender, e);
            AccordControls();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (ReloadCallback == null) return;
            if (string.IsNullOrEmpty(Dogovor)) return;

            ReloadCallback(Dogovor, RegNumber);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (_dlKey == null)
                return;

            if (MessageBox.Show("Данные  будут  внесены  в  журнал  счетов.", "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            //if (_updateVaucher != null)
            //    _updateVaucher.Invoke(serviceType: Service.ServType);

            Save_Bill();

            DataSavedFlag = true;
            btn_Clear.Focus();

            AccordControls();
        }

        private void chkConfirm_CheckedChanged(object sender, EventArgs e)
        {
            DataEditingHandler(sender, e);

            if (chkConfirm.Checked)
            {
                pConfirm.BackColor = Color.PaleGreen;
            }
            else
            {
                pConfirm.BackColor = Color.PeachPuff;
            }
        }

        private void ChangeDataPanel()
        {
            if (!NewBillFlag)
                if (!NumberFoundFlag) return;

            SetDataPanel(!pUpdateData.Visible);
            if (pUpdateData.Visible)
            {
                SetFilePanel(false);
                this.AutoScrollPosition = new Point(0, 60);
            }
        }

        private void ChangeFilePanel()
        {
            if (!NewBillFlag)
                if (!NumberFoundFlag) return;

            SetFilePanel(!pUpdateFile.Visible);
            if (pUpdateFile.Visible)
            {
                SetDataPanel(false);
                this.AutoScrollPosition = new Point(0, 60);
            }
        }

        private void lblUpdateData_Click(object sender, EventArgs e)
        {
            ChangeDataPanel();
        }

        private void lblUpdateFile_Click(object sender, EventArgs e)
        {
            ChangeFilePanel();
        }

        private void picUpdateData_Click(object sender, EventArgs e)
        {
            ChangeDataPanel();
        }

        private void picUpdateFile_Click(object sender, EventArgs e)
        {
            ChangeFilePanel();
        }

        private void ctlTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ctlTabs.SelectedIndex == 0)
            {
                chk_Find.Checked = true;
                chk_New.Checked = false;
                SetDataPanel(false);
                SetFilePanel(false);
                AccordControls();
            }
            if (ctlTabs.SelectedIndex == 1)
            {
                chk_Find.Checked = false;
                chk_New.Checked = true;
                SetDataPanel(true);
                SetFilePanel(false);
                AccordControls();
            }
        }

    }
}
