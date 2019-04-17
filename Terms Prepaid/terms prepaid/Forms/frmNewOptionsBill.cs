using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using terms_prepaid.Helpers;
using WpfControlLibrary.Util;


namespace terms_prepaid.Forms
{
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

    public partial class frmNewOptionsBill : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        private string DgCode = "";
        //private int DlKey = 0;
        private int RegNumber = 0;

        private string BillNumber = "";
        private double Summa = 0;
        private double SummaDeposit = 0;
        private double SummaRest = 0;
        private DateTime DatePayDeposit;
        private DateTime DatePayRest;
        private string Dogovor = "";
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

        private DateTime InitDatePayDeposit;
        private DateTime InitDatePayRest;
        private DateTime MinDate;
        private DateTime MaxDate;

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


        public frmNewOptionsBill(int iX, int iY, string iTitle, string iDgCode) 
        {
            StartPosX = iX;
            StartPosY = iY;

            DgCode = iDgCode;
//DgCode = "SPL80909A6";

            InitializeComponent();

            if (iTitle != null)
                this.Text = iTitle;

            DecSign = '.';
            if ((1.2).ToString().IndexOf(',') > 0) DecSign = ',';

        }

        private void frmNewOptionsBill_Load(object sender, EventArgs e)
        {
            int screen_height = Screen.PrimaryScreen.Bounds.Height;
            if (StartPosY + this.Height > screen_height) StartPosY = screen_height - this.Height - 50;

            this.Left = StartPosX;
            this.Top = StartPosY;

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

            //Load_Numbers();
            //Load_Bill();

            Accord_Controls();
        }

        private void frmNewOptionsBill_Deactivate(object sender, EventArgs e)
        {
            //this.Close();
        }

        //private void label1_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}


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

            DogovorIsValid = true;
            if (!string.IsNullOrEmpty(Dogovor) && !string.IsNullOrEmpty(DgCode))
            {
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
            //DataStatus = "Письмо с регистрационным номером <" + RegNumber.ToString() + "> найдено.";
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
                    txt_Status.Text = "Введите регистрационный номер письма и нажмите кнопку  [Найти счет]";
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

                            string status = "Письмо с регистрационным номером <" + RegNumber.ToString() + "> не найдено.";
                            status = status + (char)13 + (char)10 + "Заполните поля и подтвердите создание новой записи.";
                            txt_Status.Text = status;
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
                            txt_Status.Text = DataStatus;
                    }
                    if (DataSavedFlag)
                    {
                        txt_Status.Text = "Данные сохранены (регистрационный номер " + RegNumber.ToString() + ").";
                        NewMode = InterMode.Updated;
                    }
                    break;

                case InterMode.Adding:
                    if (!chk_New.Checked)
                    {
                        Dogovor = "";
                        Reflect_Data();

                        string status = "Письмо с регистрационным номером <" + RegNumber.ToString() + "> не найдено.";
                        status = status + (char)13 + (char)10 + "Введите номер и повторит поиск или внесите новую запись.";
                        txt_Status.Text = status;
                        NewMode = InterMode.NotFound;
                    }
                    else
                    {
                        // check data valid
                        if (DataEditedFlag)
                        {
                            Check_Data();
                            if (!string.IsNullOrEmpty(DataStatus)) 
                                txt_Status.Text = DataStatus;
                        }
                        if (DataSavedFlag)
                        {
                            txt_Status.Text = "Создана новая запись (регистрационный номер " + RegNumber.ToString() + ").";
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
                if (ctl.Name == "txt_Dogovor")
                {
                    if (valid_flag)
                        ctl.BackColor = btn_Search.BackColor;  // Color.System.Control;
                    else
                        ctl.BackColor = Color.MistyRose;
                }
            }
        }


        private void Accord_Controls()
        {
            Check_Mode();

            bool bNumber = false;
            bool bPartner = false;
            bool bSearch = false;
            bool bNew = true;
            bool bEdit = false;
            bool bSave = false;
            bool bOpenFile = false;
            string CancelText = "Отменить";

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
                    CancelText = "Закрыть";
                    break;

                case InterMode.Inserted:
                    CancelText = "Закрыть";
                    break;

                default:

                    break;
            }

            txt_RegNumber.Enabled = bNumber;
            btn_Search.Enabled = bSearch;
            chk_New.Enabled = bNew;
            txt_BillNumber.Enabled = bEdit && bPartner;
            txt_Summa.Enabled = bEdit;
            txt_SummaDeposit.Enabled = bEdit;
            dt_DatePayDeposit.Enabled = bEdit;
            txt_SummaRest.Enabled = bEdit;
            dt_DatePayRest.Enabled = bEdit;
            txt_Dogovor.Enabled = bEdit;
            txt_FilePath.Enabled = bEdit;
            txt_FileName.Enabled = bEdit;
            btn_SelectFile.Enabled = bEdit;
            btn_ShowFile.Enabled = bEdit;

            btn_Save.Enabled = bSave;
            if (btn_Cancel.Text != CancelText) btn_Cancel.Text = CancelText;

            SetControlColor(txt_RegNumber, RegNumberIsValid);

            SetControlColor(txt_RegNumber, RegNumberIsValid);
            SetControlColor(txt_BillNumber, BillNumberIsValid);
            SetControlColor(txt_Summa, SummaIsValid);
            SetControlColor(txt_SummaDeposit, SummaDepositIsValid);
            SetControlColor(txt_SummaRest, SummaRestIsValid);
            SetControlColor(dt_DatePayDeposit, DatePayDepositIsValid);
            SetControlColor(dt_DatePayRest, DatePayRestIsValid);
            SetControlColor(txt_Dogovor, DogovorIsValid);
            SetControlColor(txt_FileName, BillPathIsValid);
        }


        private void Load_Numbers()  // read bills numbers 
        {
            if (string.IsNullOrEmpty(DgCode)) return;

            string query = "SELECT [BFE_KEY] FROM [lanta].[dbo].[FIN_BillsFromEmail] ";
            query = query + "  LEFT JOIN [lanta].[dbo].[FIN_BillsParseMKFromEmail] ";
            query = query + "    ON [FIN_BillsFromEmail].[BFE_KEY] = [FIN_BillsParseMKFromEmail].[BPMK_BFEKey] ";
            query = query + "  WHERE [BPMK_Dogovor] = @dgcode ";
            query = query + "  ORDER BY [BFE_Date] DESC";

            using (var adapter = new SqlDataAdapter(query, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", DgCode);
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        RegNumber = dt.Rows[0].Field<int>("BFE_KEY");
                    }
                    catch (System.Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }
            }
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
            BillFilePath = "";
            BillFileFlag = false;

            BillNumberIsValid = false;
            SummaIsValid = false;
            SummaDepositIsValid = false;
            SummaRestIsValid = false;
            DatePayDepositIsValid = false;
            DatePayRestIsValid = false;
            DogovorIsValid = true;
            BillPathIsValid = true;

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
            txt_Dogovor.Text = "";
            txt_FilePath.Text = "";
            txt_FileName.Text = "";

            if (RegNumber > 0) txt_RegNumber.Text = RegNumber.ToString();
            if (!string.IsNullOrEmpty(BillNumber)) txt_BillNumber.Text = BillNumber;
            if (Summa > 0) txt_Summa.Text = Summa.ToString();
            if (SummaDeposit > 0) txt_SummaDeposit.Text = SummaDeposit.ToString();
            if (DatePayDeposit > dt_DatePayDeposit.MinDate && DatePayDeposit < dt_DatePayDeposit.MaxDate)
                dt_DatePayDeposit.Value = DatePayDeposit;
            if (SummaRest > 0) txt_SummaRest.Text = SummaRest.ToString();
            if (DatePayRest > dt_DatePayRest.MinDate && DatePayRest < dt_DatePayRest.MaxDate)
                dt_DatePayRest.Value = DatePayRest;
            if (!string.IsNullOrEmpty(Dogovor)) txt_Dogovor.Text = Dogovor;
            if (!string.IsNullOrEmpty(BillFilePath))
            {
                txt_FilePath.Text = BillFilePath;
                string file_name = BillFilePath;
                if (!string.IsNullOrEmpty(file_name)) file_name = file_name.Substring(file_name.LastIndexOf("\\") + 1);
                txt_FileName.Text = file_name;
            }

            DataNotEditingFlag = false;
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
            query = query + ",IsNull([BPMK_SumRest],0) AS [BPMK_SumRest],IsNull([BPMK_DatePayRest],'2000.01.01') AS [BPMK_DatePayRest],IsNull([BPMK_FilePath],'') AS [BPMK_FilePath]";
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

                    string status = "Письмо с регистрационным номером <" + RegNumber.ToString() + "> найдено.";
                    status = status + (char)13 + (char)10 + "Заполните поля и подтвердите сохранение данных.";
                    txt_Status.Text = status;

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
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Load_Bill", ex);
                    }
                }
                else
                {
                    NumberFoundFlag = false;

                    string status = "Письмо с регистрационным номером <" + RegNumber.ToString() + "> не найдено.";
                    status = status + (char)13 + (char)10 + "Введите номер и повторит поиск или внесите новую запись.";
                    txt_Status.Text = status;
                }
            }

            Reflect_Data();
        }

        private string Load_Bill_File()
        {
            if (RegNumber <= 0) return "";

            string BillPath = "";
            byte[] BillData = null;

            string query = @"SELECT IsNull([BPMK_FilePath],0) AS [BPMK_FilePath], IsNull([BPMK_FileData],0) AS [BPMK_FileData]";
            query = query + "  FROM [dbo].[FIN_BillsParseMKFromEmail]";
            query = query + "  WHERE [BPMK_BFEKey] = @key";

            using (var adapter = new SqlDataAdapter(query, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@key", RegNumber);
                var dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        BillPath= dt.Rows[0].Field<string>("BPMK_FilePath");
                        BillData = dt.Rows[0].Field<byte[]>("BPMK_FileData");
                    }
                    catch (System.Exception ex)
                    {
                        TpLogger.Debug("Load_Bill_File", ex);
                    }
                }
            }

            if (string.IsNullOrEmpty(BillPath)) return "";
            if (BillData == null) return "";
            if (BillData.Length < 10) return "";

            string app_path = Application.ExecutablePath;
            string dir = app_path.Substring(0, app_path.LastIndexOf('\\') + 1);
            string temp_dir = dir + "temp";
            if (!System.IO.Directory.Exists(temp_dir))
                System.IO.Directory.CreateDirectory(temp_dir);
            string bill_dir = temp_dir + (char)92 + "bills_" + TpLogger.UserLogin;
            if (!System.IO.Directory.Exists(bill_dir))
                System.IO.Directory.CreateDirectory(bill_dir);

            string file_name = Path.GetFileName(BillPath);

            string path = bill_dir + (char)92 + file_name;

            if (File.Exists(path)) File.Delete(path);
            File.WriteAllBytes(path, BillData);

            if (!File.Exists(path)) return "";

            return path;
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
                    txt_Status.Text = "Регистрационный номер для новой записи не получен.";
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
                query = query + ",[BPMK_Dogovor]";
                query = query + ") VALUES (";
                query = query + "@regnumber, @billnumber";
                query = query + ", @summa, 0, ''";
                query = query + ", 0, 0";
                query = query + ", @summadeposit, @dtdeposit";
                query = query + ", @summarest, @dtrest";
                query = query + ", @dgcod)";
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
        
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btn_Save_Click(object sender, EventArgs e)
        {
            //if (!RegNumberIsValid && InMode == InterMode.Editing) // Update existing record
            //{
            //    string status = "Регистрационный номер <" + txt_RegNumber.Text + "> не подходит.";
            //    status = status + (char)13 + (char)10 + "Введите корректный номер.";
            //    txt_Status.Text = status;
            //    return;
            //}

            //if (!DataValidFlag)
            //{
            //    string status = "Данные не подходят для сохранения.";
            //    status = status + (char)13 + (char)10 + "Введите корректные данные.";
            //    txt_Status.Text = status;
            //    return;
            //}

            Save_Bill();

            DataSavedFlag = true;

            Accord_Controls();

            //this.Close();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (!Read_RegNumber())
            {
                string status = "Регистрационный номер <" + txt_RegNumber.Text + "> не подходит.";
                status = status + (char)13 + (char)10 + "Введите корректный номер.";
                txt_Status.Text = status;
                return;
            }

            NumberSearchFlag = true;
            NumberFoundFlag = false;

            Load_Bill();

            Accord_Controls();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            InMode = InterMode.Start;

            bool bFlag = DataNotEditingFlag;
            DataNotEditingFlag = true;
            chk_New.Checked = false;
            DataNotEditingFlag = bFlag;

            Accord_Controls();
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
                        txt_FilePath.Text = BillFilePath;
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

        private void btn_ShowFile_Click(object sender, EventArgs e)
        {
            if (BillFileFlag) // если выбран новый файл
            {
                if (string.IsNullOrEmpty(BillFilePath))
                {
                    txt_Status.Text = "Файл не указан.";
                    return;
                }
                if (!System.IO.File.Exists(BillFilePath))
                {
                    txt_Status.Text = "Файл не найден (" + BillFilePath + ").";
                    return;
                }

                System.Diagnostics.Process.Start(BillFilePath);
            }
            else
            {
                if (string.IsNullOrEmpty(BillFilePath))
                {
                    txt_Status.Text = "Файл не загружен в базу.";
                    return;
                }

                string bill_path = Load_Bill_File();
                if (string.IsNullOrEmpty(bill_path)) return;
                if (!File.Exists(bill_path)) return;

                try
                {
                    System.Diagnostics.Process.Start(bill_path);
                }
                catch (System.Exception ex)
                {
                    string msg = ex.Message;
                    txt_Status.Text = "Открыть файл не получилось. " + ex.Message;
                }
            }

        }

        private void txt_RegNumber_TextChanged(object sender, EventArgs e)
        {
            if (DataNotEditingFlag) return;

            NumberChangedFlag = true;
            Accord_Controls();
        }

        private void chk_New_CheckedChanged(object sender, EventArgs e)
        {
            if (DataNotEditingFlag) return;

            if (chk_New.Checked)
            {
                Clear_Data(true);
                Dogovor = DgCode;
                Reflect_Data();
                txt_Status.Text = "Заполните поля и подтвердите создание новой записи.";
                InMode = InterMode.Adding;
            }
            else
            {
                Clear_Data(true);
                InMode = InterMode.Start;
            }
            Accord_Controls();
        }

        private void DataEditingHandler(object sender, EventArgs e)
        {
            if (DataNotEditingFlag) return;

            DataEditedFlag = true;
            Accord_Controls();
        }

    }
}
