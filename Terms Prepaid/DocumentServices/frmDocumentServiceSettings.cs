using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using DocomentServices;
using HelperClasses;
using lanta.SQLConfig;


namespace DocumentServices
{

    public partial class frmDocumentServiceSettings : Form
    {
        private string _dgcode="";
        ToolTip toolTip1 = new ToolTip();
        bool ToolTipFlag = false;
        //private int x = 0, y = 0, x1 = 0, y1 = 300;
        private SqlConnection _connection;
        private string _savePath = Path.GetTempPath();
        lanta.SQLConfig.Config_XML sqlConfig = new lanta.SQLConfig.Config_XML();
       // private DataTable _documents= new DataTable(), _servises= new DataTable(),_docummentServise;
        private MasterDataContext sqlDataContext;
        private string winINI = "C:\\Windows\\win.ini";
        private string userName;
        private string password;
     //   private EnvironmentVariableTarget _Documents;
 
        void SetAccess()
        {
            AccessClass access = new AccessClass(_connection);
            if (access.isBronir || access.isSuperViser)
            {
                btnVoucherer.Visible = true;
                btnInshur.Visible = true;
                btnFileToFtp.Visible = true;
                btnWordReport.Visible = true;

            }
            else
            {
                btnVoucherer.Visible = false;
                btnInshur.Visible = false;
                btnFileToFtp.Visible = false;
                btnWordReport.Visible = false;
            }

        }
        public frmDocumentServiceSettings(string dg_code,SqlConnection connection)
        {
            InitializeComponent();
            _dgcode = dg_code;
            _connection = connection;
            toolTip1.AutomaticDelay = 100;
            toolTip1.ReshowDelay = 100;
            toolTip1.AutoPopDelay = 10000000;
            dgvServises.ShowCellToolTips = false;
            sqlDataContext = new MasterDataContext(_connection);
            sqlDataContext.CommandTimeout = 60; // 30;
            GetDate();
            string[] strs = _connection.ConnectionString.Split(';');
            foreach (string s in strs)
            {
                if (s.IndexOf("User ID=")>=0)
                {
                    userName = s.Substring("User ID=".Length);
                }else if (s.IndexOf("Password=") >= 0)
                {
                    password = s.Substring("Password=".Length);
                }
            }

        }
        
        void GetDate()
        {
            this.Text = "Сборка документов по путевке " + _dgcode;
            sqlDataContext.mk_status_documents();
            sqlDataContext.Refresh(RefreshMode.KeepChanges);
            UpdateDataGrid();
            SetAccess();
        }
        /*
         * id	NameDocument	Turists	TuristsKeys	Servises	ServisesKeys	TypeOfDocument	Document	StatusDocument	CodeStatusDocument
        1	Памятка круизного пассажира,Generated report	Тестов Тест,	1028016,	Не првязан к сервису		1	lanta_1504161696759384.pdf	ВЫЛОЖЕН	1
        2	Памятка круизного пассажира,Generated report	Тестов Тест,	1028016,	Не првязан к сервису		1	lanta_150416449556965.pdf	ВЫЛОЖЕН	1
        3	Приглашение на визу,Generated report	Тестов Тест,	1028016,	Не првязан к сервису		1	lanta_1504161065476887.pdf	ВЫЛОЖЕН	1
        4	Приглашение на визу,Generated report	Тестов Тест,	1028016,	Не првязан к сервису		1	lanta_1504161454687212.pdf	ВЫЛОЖЕН	1
        5	Программа пребывания,Generated report	Тестов Тест,Фамилия Имечко,	1028016,1028653,	Не првязан к сервису		1	mk_150525182915483.pdf	ВЫЛОЖЕН	1
         * 
         */
        mk_docoment_by_dogovorRusultExtendet[] GetArrayExtendet(mk_documents_by_dogovorResult[] arr)
        {
            List<mk_docoment_by_dogovorRusultExtendet> list=new List<mk_docoment_by_dogovorRusultExtendet>();

            DateTime? dateAccept = sqlDataContext.mk_DogovorAdds.First(x => x.DA_DGCODE == _dgcode).DA_DocumentAccept;
            foreach (mk_documents_by_dogovorResult mkDocumentsByDogovorResult in arr)
            {
                list.Add(new mk_docoment_by_dogovorRusultExtendet(mkDocumentsByDogovorResult));
                if (dateAccept != null)
                {
                    list.Last().ChekStatus(dateAccept.Value);
                }
                else
                {
                    list.Last().ChekStatus(new DateTime(2000,01,01));
                }
                
            }
            return list.ToArray();
        }
        void UpdateDataGrid()
        {

            mk_docoment_by_dogovorRusultExtendet[] rezult = GetArrayExtendet(sqlDataContext.mk_documents_by_dogovor(_dgcode).ToArray());

            int countGeneral = rezult.Count();
            int insetedCout = rezult.Where(x => x.CodeStatusDocument == 1).Count();
            int notInsertedCount = countGeneral - insetedCout;
            int coutInsetedLk = rezult.Where(x => x.subStatus == "Обработано и выложено в ЛК").Count(), countNotInsetedLk = rezult.Where(x => x.subStatus == "Обработано").Count(), countNotProcessed = countGeneral - countNotInsetedLk - coutInsetedLk;
            tbCount.Text = countGeneral.ToString();
            tbInserted.Text = insetedCout.ToString();
            tbNotInserted.Text = notInsertedCount.ToString();
            tbNotInsertedLk.Text = countNotInsetedLk.ToString();
            tbCountIsertedLk.Text = coutInsetedLk.ToString();
            tbNotProcessed.Text = countNotProcessed.ToString();
          //  dgvServises.AutoGenerateColumns = true;
            dgvServises.DataSource = rezult;
          //  dgvServises.DataSource
            //try
            //{
            //    var rez = sqlDataContext.mk_DogovorAdds.Where(x => x.DA_DGCODE == _dgcode).First();
            //    if (rez.DA_DocumentAccept != null)
            //    {
            //        try
            //        {
            //            History his =
            //                sqlDataContext.Histories.Where(x => x.HI_MOD == "ILK" && x.HI_DGCOD == _dgcode)
            //                              .OrderByDescending(x => x.HI_DATE)
            //                              .FirstOrDefault();
            //            lbMessage.Text = "Выставлено в ЛК " + rez.DA_DocumentAccept.Value.ToString("dd.MM.yyyy HH:mm") +
            //                             " Пользователь:" + his.HI_WHO;
            //        }
            //        catch (Exception)
            //        {

            //            lbMessage.Text = "Выставлено в ЛК " + rez.DA_DocumentAccept.Value.ToString("dd.MM.yyyy HH:mm");
            //        }
            //    }
            //    else
            //    {
            //        lbMessage.Text = "Документы не выставлены в ЛК";
            //    }
            //}
            //catch (Exception)
            //{

            //    lbMessage.Text = "Документы не выставлены в ЛК";
            //}

            foreach (DataGridViewColumn column in dgvServises.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "namedocument":
                        column.HeaderText = "Документ";
                        column.Width = 200;
                        column.DisplayIndex = 0;
                        break;
                    case "turists":
                        column.HeaderText = "Туристы";
                        column.Width = 250;
                        column.DisplayIndex = 1;
                        break;
                    case "servises":
                        column.HeaderText = "Услуги";
                        column.Width = 250;
                        column.DisplayIndex = 2;
                        break;
                    case "statusdocument":
                        column.HeaderText = "Статус";
                        column.Width = 100;
                        column.DisplayIndex = 3;
                        break;
                    case "button1":
                        column.HeaderText = "Посмотреть";
                        column.Width = 70;
                        column.DisplayIndex = 4;
                        break; 
                    case "button2":
                        column.HeaderText = "Корректировка привязки";
                        column.Width = 90;
                        column.DisplayIndex = 5;
                        break;                  
                    case "button3":
                        column.HeaderText = "Удалить документ";
                        column.Width = 50;
                        column.DisplayIndex = 6;
                        break;
                    case "datecreated":
                        column.HeaderText = "Дата выкладки";
                        column.Width =100;
                        column.DisplayIndex = 7;
                        break;
                    case "whocreated":
                        column.HeaderText = "Пользователь";
                        column.Width = 130;
                        column.DisplayIndex = 8;
                        break;
                    case "substatus":
                        column.HeaderText = "Статус обработки";
                        column.Width = 170;
                        column.DisplayIndex = 9;
                        break;
                    default:
                        column.Visible = false;
                        break;

                }
                //dgvServises.Refresh();
            }

            //mk_docoment_by_dogovorRusultExtendet[] rezult = GetArrayExtendet(sqlDataContext.mk_documents_by_dogovor(_dgcode).ToArray());
            foreach (mk_docoment_by_dogovorRusultExtendet row in rezult)
            {
                string str = row.Servises;
                str = str.Replace("Российская Федерация", "РФ");
                str = str.Replace("медицинская", "мед.");
                str = str.Replace("дней", "дн.");
                row.Servises = str;
            }

            // var rez = sqlDataContext.MK_lk_servises_putevka(_dgcode).Where(x => (x.tipe!=1)||(x.tipe==1&&x.order==1));
            // //sqlDataContext.LTV_Vouchers.Join(sqlDataContext.LTV_VoucherServiceLists, voucher => voucher.VI_ID,
            //   //                               list => list.VS_VIID, (voucher, list) => voucher.VI_ID == list.VS_VIID);
            //     //sqlDataContext.tbl_DogovorLists.Where(
            // //        x => x.DL_DGCOD == _dgcode && x.DL_SVKEY != 1059 && x.DL_SVKEY != 1238 && x.DL_SVKEY != 1520 && (x.DL_ATTRIBUTE& 64)==0);
            // var rez1 = rez.ToArray();
            // List<ToursExtended> tours = new List<ToursExtended>();
            // foreach (Tours tourse in rez1)
            // {

            //     ToursExtended tour = new ToursExtended(tourse);
            //     //Выложенные файлы  по этой услуге
            //     string insertedDocument ="";

            //     var vouchers = sqlDataContext.LT_V_Services.Where(x => x.VS_DLKey == tourse.dl_KEY&&x.LT_V_Voucher.V_AnnulDate==null);

            //     if (tourse.dl_svkey == 6)
            //     {
            //         var inshur = sqlDataContext.Lanta_PersonalAreas.Where(x => x.pa_ddgID == 100600&&x.pa_DG_Code==_dgcode);
            //         Dictionary<string,string> inshurList = new Dictionary<string, string>();
            //         foreach (Lanta_PersonalArea lantaPersonalArea in inshur)
            //         {
            //             if (inshurList.Where(x => x.Key == lantaPersonalArea.pa_FileName).Count() == 0)
            //             {
            //                 inshurList.Add(lantaPersonalArea.pa_FileName,lantaPersonalArea.Lanta_DictDocSpr.ddgName);
            //             }
            //         }

            //         foreach (KeyValuePair<string, string> keyValuePair in inshurList)
            //         {
            //             insertedDocument += (insertedDocument != "" ? " " : "") + keyValuePair.Value;// +" : " + keyValuePair.Key;
            //         }

            //     }
            //     if (vouchers.Count() > 0)
            //     {
            //        string vouchernumber = vouchers.First().LT_V_Voucher.V_Number;
            //         insertedDocument +=(insertedDocument!=""?" ":"") + "Ваучер № " + vouchernumber;

            //     }
            //     var documentsDl = sqlDataContext.mk_DocumentServices.Where(x => x.DS_DLKEY == tour.dl_KEY);
            //     if (documentsDl.Count()>0)
            //     {
            //         if (documentsDl.First().DS_FileName == null)
            //         {
            //             insertedDocument += "В документах не нуждается!";
            //         }
            //         foreach (mk_DocumentService mkDocumentService in documentsDl)
            //         {
            //             var file =
            //                 sqlDataContext.Lanta_PersonalAreas.FirstOrDefault(x => x.pa_FileName == mkDocumentService.DS_FileName);
            //             if (file != null)
            //             {
            //                 insertedDocument += (insertedDocument != "" ? "\n" : "") + file.Lanta_DictDocSpr.ddgName;// +" : " + file.pa_FileName;
            //             }
            //         }
            //     }

            //     tour.documents = insertedDocument;
            //     tours.Add(tour);
            // }
            // //Выложенные но не привязанные к услугам файлы
            // var documents = sqlDataContext.Lanta_PersonalAreas.Where(x => x.pa_DG_Code == _dgcode&&x.pa_ddgID!=100600);
            //Dictionary<string,string> files = new Dictionary<string, string>();

            // foreach (Lanta_PersonalArea lantaPersonalArea in documents)
            // {
            //     if (files.Where(x => x.Key == lantaPersonalArea.pa_FileName).Count() == 0)
            //     {
            //         files.Add(lantaPersonalArea.pa_FileName, lantaPersonalArea.Lanta_DictDocSpr.ddgName);
            //     }

            // }
            // string filesNotAdd = "";
            // foreach (KeyValuePair<string, string> keyValuePair in files)
            // {
            //     if (sqlDataContext.mk_DocumentServices.Where(x => x.DS_FileName == keyValuePair.Key).Count() == 0)
            //     {
            //         filesNotAdd += (filesNotAdd != "" ? " " : "") + keyValuePair.Value;// +" : " + keyValuePair.Key;
            //     }

            // }
            // tours.Add(new ToursExtended("Документы непривязанные к услугам!",filesNotAdd));
            // dgvServises.DataSource = tours;

            // foreach (DataGridViewColumn column in dgvServises.Columns)
            // {
            //     switch (column.Name.ToLower())
            //     {
            //         case "dl_name":
            //             column.HeaderText = "услуга";
            //             column.DisplayIndex = 0;
            //             break;
            //         case "documents":
            //             column.HeaderText = "Выложенные документы";
            //             column.DisplayIndex = 1;
            //             column.Width = 400;
            //             break;
            //         default:
            //             column.Visible = false;
            //             break;
            //     }
            // }
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
                Process.Start(masterPath + "\\rep6050.exe", userName + " " + password + " !DGCODE=" + _dgcode);
                // ltp_v2.Framework.SqlConnection.ConnectionUserName+" "+ltp_v2.Framework.SqlConnection.ConnectionPassword +" !DGCODE="+_dgCode);
                //Console.WriteLine(userName);
                GetDate();

            }
            catch (Exception)
            {

                MessageBox.Show("Система выписки страховок не найдена!");
                return;
            }
        }

        private void btnVoucherer_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(winINI);

            String masterPath = ini.IniReadValue("master", "ReportPath", "");
            if (masterPath == "")
            {
                MessageBox.Show("Система выписки ваучеров не найдена!");
                return;
            }
            try
            {
                //string userName = WorkWithData.Connection.ConnectionString;
                Process.Start(masterPath + "\\rep25991.exe", userName + " " + password + " !DGCODE=" + _dgcode);
                // ltp_v2.Framework.SqlConnection.ConnectionUserName+" "+ltp_v2.Framework.SqlConnection.ConnectionPassword +" !DGCODE="+_dgCode);
                //Console.WriteLine(userName);
                GetDate();

            }
            catch (Exception)
            {

                MessageBox.Show("Система выписки ваучеров не найдена!");
                return;
            }
        }

        private void btnWordReport_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(winINI);
            String masterPath = ini.IniReadValue("master", "ReportPath", "");
            int dl_key = 0;
            //if (dgvServises.SelectedRows.Count > 0)
            //{
            //    dl_key =(int) dgvServises.SelectedRows[0].Cells["dl_key"].Value;
            //}
             
            if (masterPath == "")
            {
                MessageBox.Show("Унивесальный отчет не найден!");
                return;
            }
            try
            {
                //string userName = WorkWithData.Connection.ConnectionString;

                Process.Start(masterPath + "\\rep99011.exe", userName + " " + password + " !DGCODE=" + _dgcode + " " + dl_key);
                // ltp_v2.Framework.SqlConnection.ConnectionUserName+" "+ltp_v2.Framework.SqlConnection.ConnectionPassword +" !DGCODE="+_dgCode);
                //Console.WriteLine(userName);
                GetDate();

            }
            catch (Exception)
            {

                MessageBox.Show("Унивесальный отчет не найден!");
                return;
            }
        }

        private void btnFileToFtp_Click(object sender, EventArgs e)
        {
           // if (dgvServises.SelectedRows.Count <= 0)
           // {
          //      return;
          //  }
           // int dl_key = 0; //Convert.ToInt32(dgvServises.SelectedRows[0].Cells["Dl_key"].Value);
           // if(dl_key==0)return;
            frmFileToFtp frmFile = new frmFileToFtp(_connection,_dgcode);
            frmFile.ShowDialog();
            GetDate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvServises.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvServises.SelectedRows[0];
                if (row.Cells["TypeOfDocument"].Value.Equals(1))
                {

                    string filename = (string)row.Cells["DOCUMENT"].Value;
                    try
                    {
                        string[] servises = row.Cells["ServisesKeys"].Value.ToString().Split(',');
                        var rez = sqlDataContext.tbl_DogovorLists.First(x => x.DL_KEY == int.Parse(servises[0]));
                        int cc =
                            sqlDataContext.URS_Insurances.Where(x => x.INS_DGCode == _dgcode && x.INS_Status == true)
                                          .Count();
                        if (rez.DL_SVKEY == 6&&cc>0)
                        {
                            btnInshur_Click(null,null);
                            GetDate();
                            return;
                        }
                    }
                    catch (Exception)
                    {


                    }
                    //new frmDocumentUsluga(filename, _dgcode, sqlDataContext).ShowDialog();

                    if (
                        MessageBox.Show("Вы точно хотите удалить файл?", "Проверка", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        lanta.SQLConfig.Config_XML config = new Config_XML();
                        WorkWithFTP ftpClient = new WorkWithFTP(config.Get_Value("appSettings", "Ftp"));
                        string error;
                        if (ftpClient.Delete(_dgcode, filename, out error) == WorkWithFTP.FTP_ERROR.ERROR_NO)
                        {
                            var mkdocser = sqlDataContext.mk_DocumentServices.Where(x => x.DS_FileName == filename);

                            foreach (mk_DocumentService mkDocumentService in mkdocser)
                            {
                                sqlDataContext.mk_DocumentServices.DeleteOnSubmit(mkDocumentService);
                            }
                            var mkper = sqlDataContext.Lanta_PersonalAreas.Where(x => x.pa_FileName == filename);
                            foreach (Lanta_PersonalArea lantaPersonalArea in mkper)
                            {
                                sqlDataContext.Lanta_PersonalAreas.DeleteOnSubmit(lantaPersonalArea);
                            }
                            foreach (mk_DocumentService mkDocumentService in mkdocser)
                            {
                                sqlDataContext.mk_DocumentServices.DeleteOnSubmit(mkDocumentService);
                            }
                            sqlDataContext.SubmitChanges();
                        }
                        else
                        {
                            MessageBox.Show(error);
                        }
                    }
                    GetDate();
                    //Вызов формы по привяке услуг
                }
                else if (row.Cells["TypeOfDocument"].Value.Equals(2))
                {
                    btnVoucherer_Click(null,null);
                    GetDate();
                }
                else
                {
                    MessageBox.Show("Неизвестный тип документа!");
                }
                
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvServises.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvServises.SelectedRows[0];
                if (row.Cells["TypeOfDocument"].Value.Equals(1))
                {
                    string filename = (string) row.Cells["DOCUMENT"].Value;
                    lanta.SQLConfig.Config_XML config = new Config_XML();
                    WorkWithFTP ftpClient = new WorkWithFTP(config.Get_Value("appSettings","Ftp"));
                    string error, filepath;
                    if (ftpClient.Download(_dgcode, filename, _savePath, out error, out filepath) ==
                        WorkWithFTP.FTP_ERROR.ERROR_NO)
                    {
                        Process.Start(filepath);
                    }
                    else
                    {
                        MessageBox.Show(error);
                        return;
                    }
                }else if (row.Cells["TypeOfDocument"].Value.Equals(2))
                {
                    WebClient client = new WebClient();
                    string filename = (string)row.Cells["DOCUMENT"].Value;
                    int us_key = sqlDataContext.UserLists.First(x => x.US_USERID == userName).US_KEY;
                    string url = String.Format("http://mcruises.ru/cabinet/contracts/voucher/?v_id={0}&us_key={1}",filename,us_key);
                    byte[] data = client.DownloadData(url);
                    string  filepath = string.Format(_savePath+"voucher{0}.pdf",filename);
                    BinaryWriter bw = new BinaryWriter(File.Create(filepath));
                    bw.Write(data);
                    bw.Close();
                    Process.Start(filepath);

                }
                else
                {
                    MessageBox.Show("Неизвестный тип документа!");
                }

            }
        }

        private void btnDocumentUsluga_Click(object sender, EventArgs e)
        {
            if (dgvServises.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvServises.SelectedRows[0];
                if (row.Cells["TypeOfDocument"].Value.Equals(1))
                {
                    
                    string filename = (string)row.Cells["DOCUMENT"].Value;
                    try
                    {
                        string[] servises = row.Cells["ServisesKeys"].Value.ToString().Split(',');
                        var rez = sqlDataContext.tbl_DogovorLists.First(x => x.DL_KEY == int.Parse(servises[0]));
                        if (rez.DL_SVKEY == 6)
                        {
                            MessageBox.Show("Вы не можете привязывать услуги к этому типу документа!");
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        
                        
                    }
                    new frmDocumentUsluga(filename, _dgcode, sqlDataContext).ShowDialog();
                    GetDate();
                    //Вызов формы по привяке услуг
                }
                else if (row.Cells["TypeOfDocument"].Value.Equals(2))
                {
                    MessageBox.Show("Вы не можете привязывать услуги к этому типу документа!");
                }
                else
                {
                    MessageBox.Show("Неизвестный тип документа!");
                }

            }
        }

        private void dgvServises_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            try
            {
                //if (dgvServises.Columns[e.ColumnIndex].Name.ToLower() == "servises" ||
                //    dgvServises.Columns[e.ColumnIndex].Name.ToLower() == "turists")
                //{
                //    dgvServises.Rows[e.RowIndex].Cells[e.ColumnIndex].

                //}
                 if (dgvServises.Columns[e.ColumnIndex].Name.ToLower() == "statusdocument" )
                 {
                     if ((int) dgvServises.Rows[e.RowIndex].Cells["CodeStatusDocument"].Value == 1)
                     {
                        e.CellStyle.BackColor = Color.Green; 
                     }
                     else
                     {
                         e.CellStyle.BackColor = Color.Red;
                     }
                     
                 }
                 


            }
            catch (Exception)
            {


            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvServises_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex==-1||e.ColumnIndex==-1){return;}
            try
            {
                if (Equality((Image)dgvServises.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, DocumentServices.Properties.Resources.empty))
                {
                    return;
                }
            }
            catch (Exception)
            {
                
            }

            switch (dgvServises.Columns[e.ColumnIndex].Name)
            {
                case "button3":
                    {
                        DataGridViewRow row = dgvServises.Rows[e.RowIndex];

                        if (row.Cells["TypeOfDocument"].Value.Equals(1))
                        {

                            string filename = (string) row.Cells["DOCUMENT"].Value;
                            try
                            {
                                string[] servises = row.Cells["ServisesKeys"].Value.ToString().Split(',');
                                var rez = sqlDataContext.tbl_DogovorLists.First(x => x.DL_KEY == int.Parse(servises[0]));
                                int cc =
                                    sqlDataContext.URS_Insurances.Where(
                                        x => x.INS_DGCode == _dgcode && x.INS_Status == true)
                                                  .Count();
                                if (rez.DL_SVKEY == 6 && cc > 0)
                                {
                                    btnInshur_Click(null, null);
                                    GetDate();
                                    return;
                                }
                            }
                            catch (Exception)
                            {


                            }
                            //new frmDocumentUsluga(filename, _dgcode, sqlDataContext).ShowDialog();

                            if (
                                MessageBox.Show("Вы точно хотите удалить файл?", "Проверка", MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                lanta.SQLConfig.Config_XML config = new Config_XML();
                                WorkWithFTP ftpClient = new WorkWithFTP(config.Get_Value("appSettings", "Ftp"));
                                string error;
                                if (ftpClient.Delete(_dgcode, filename, out error) == WorkWithFTP.FTP_ERROR.ERROR_NO)
                                {
                                    var mkdocser =
                                        sqlDataContext.mk_DocumentServices.Where(x => x.DS_FileName == filename);

                                    foreach (mk_DocumentService mkDocumentService in mkdocser)
                                    {
                                        sqlDataContext.mk_DocumentServices.DeleteOnSubmit(mkDocumentService);
                                    }
                                    var mkper = sqlDataContext.Lanta_PersonalAreas.Where(x => x.pa_FileName == filename);
                                    foreach (Lanta_PersonalArea lantaPersonalArea in mkper)
                                    {
                                        sqlDataContext.Lanta_PersonalAreas.DeleteOnSubmit(lantaPersonalArea);
                                    }
                                    foreach (mk_DocumentService mkDocumentService in mkdocser)
                                    {
                                        sqlDataContext.mk_DocumentServices.DeleteOnSubmit(mkDocumentService);
                                    }
                                    sqlDataContext.SubmitChanges();
                                }
                                else
                                {
                                    MessageBox.Show(error);
                                }
                            }
                            GetDate();
                            //Вызов формы по привяке услуг
                        }
                        else if (row.Cells["TypeOfDocument"].Value.Equals(2))
                        {
                            btnVoucherer_Click(null, null);
                            GetDate();
                        }
                        else
                        {
                            MessageBox.Show("Неизвестный тип документа!");
                        }
                    }
                    break;
                case "button2":
                    {
                        DataGridViewRow row = dgvServises.Rows[e.RowIndex];
                        if (row.Cells["TypeOfDocument"].Value.Equals(1))
                        {

                            string filename = (string)row.Cells["DOCUMENT"].Value;
                            try
                            {
                                string[] servises = row.Cells["ServisesKeys"].Value.ToString().Split(',');
                                var rez = sqlDataContext.tbl_DogovorLists.First(x => x.DL_KEY == int.Parse(servises[0]));
                                if (rez.DL_SVKEY == 6)
                                {
                                    MessageBox.Show("Вы не можете привязывать услуги к этому типу документа!");
                                    return;
                                }
                            }
                            catch (Exception)
                            {


                            }
                            new frmDocumentUsluga(filename, _dgcode, sqlDataContext).ShowDialog();
                            GetDate();
                            //Вызов формы по привяке услуг
                        }
                        else if (row.Cells["TypeOfDocument"].Value.Equals(2))
                        {
                            MessageBox.Show("Вы не можете привязывать услуги к этому типу документа!");
                        }
                        else
                        {
                            MessageBox.Show("Неизвестный тип документа!");
                        }
                    }
                    break;
                case "button1":
                    {
                        DataGridViewRow row = dgvServises.Rows[e.RowIndex];
                        if (row.Cells["TypeOfDocument"].Value.Equals(1))
                        {
                            string filename = (string)row.Cells["DOCUMENT"].Value;
                            lanta.SQLConfig.Config_XML config = new Config_XML();
                            WorkWithFTP ftpClient = new WorkWithFTP(config.Get_Value("appSettings", "Ftp"));
                            string error, filepath;
                            if (ftpClient.Download(_dgcode, filename, _savePath, out error, out filepath) ==
                                WorkWithFTP.FTP_ERROR.ERROR_NO)
                            {
                                Process.Start(filepath);
                            }
                            else
                            {
                                MessageBox.Show(error);
                                return;
                            }
                        }
                        else if (row.Cells["TypeOfDocument"].Value.Equals(2))
                        {
                            WebClient client = new WebClient();
                            string filename = (string)row.Cells["DOCUMENT"].Value;
                            int us_key = sqlDataContext.UserLists.First(x => x.US_USERID == userName).US_KEY;
                            string url = String.Format("http://mcruises.ru/cabinet/contracts/voucher/?v_id={0}&us_key={1}", filename, us_key);
                            byte[] data = client.DownloadData(url);
                            string filepath = string.Format(_savePath + "voucher{0}.pdf", filename);
                            BinaryWriter bw = new BinaryWriter(File.Create(filepath));
                            bw.Write(data);
                            bw.Close();
                            Process.Start(filepath);

                        }
                        else
                        {
                            MessageBox.Show("Неизвестный тип документа!");
                        }
                    }

                    break;
                default:
                    break;
            }
        }
        bool Equality(Image Img1, Image Img2)
        {
            Bitmap Bmp1 = (Bitmap)Img1;
            Bitmap Bmp2 = (Bitmap)Img2;
            if (Bmp1.Size == Bmp2.Size)
            {
                for (int i = 0; i < Bmp1.Width; i++)
                    for (int j = 0; j < Bmp1.Height; j++)
                        if (Bmp1.GetPixel(i, j) != Bmp2.GetPixel(i, j)) return false;
                return true;
            }
            else return false;
        }

        private void dgvServises_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = dgvServises.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                dgvServises.Rows[index].HeaderCell.Value = indexStr; 
        }

        private void dgvServises_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                (dgvServises.Columns[e.ColumnIndex].Name.ToLower() == "servises" ||
                 dgvServises.Columns[e.ColumnIndex].Name.ToLower() == "turists"))
            {
                int x = Cursor.Position.X - this.Left;
                int y = Cursor.Position.Y - this.Top;
                //toolTip1.SetToolTip(dgvServises, dgvServises[e.ColumnIndex, e.RowIndex].Value.ToString()); // tooltip showtime limited 5 seconds 
                toolTip1.Show(dgvServises[e.ColumnIndex, e.RowIndex].Value.ToString(), this, x, y, 1000000);
                ToolTipFlag = true;
            }

        }

        private void dgvServises_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (ToolTipFlag)
            {
                ToolTipFlag = false;
                toolTip1.Hide(this);
            }
        }

        private void btnIntoLK_Click(object sender, EventArgs e)
        {
            string message = "Вы уверены в правильности информации в документах, которые будут выложены в ЛК? Тогда выкладывайте.";
            int countnotinserted = int.Parse(tbNotInserted.Text),coutnInserted = int.Parse(tbInserted.Text);
            if (coutnInserted == 0)
            {
                MessageBox.Show(this, "У вас не выложено ни одного документа!","",MessageBoxButtons.OK,MessageBoxIcon.Error); 
                return;
            }
            //if (countnotinserted > 0)
            //{
            //    MessageBox.Show(this, "У вас выложены не все документы!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            
            if (MessageBox.Show(this, message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                var rez = sqlDataContext.mk_DogovorAdds.Where(x => x.DA_DGCODE == _dgcode).First();
                if (rez != null)
                {
                    if (countnotinserted > 0)
                    {
                        if (
                            !(MessageBox.Show(this, "У вас выложены не все документы!Вы уверены что хотите продолжить?", "", MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            return;
                        }
                        
                    }
                    rez.DA_DocumentAccept= sqlDataContext.MK_NOW();
                    string documents = "Всего документов : " + tbCount.Text + " Выложено документов : " +
                                       tbInserted.Text + " Выложить документов : " + tbNotInserted.Text;

                    
                    History hist = new History()
                        {
                            HI_DGCOD = _dgcode,
                            HI_WHO = sqlDataContext.UserLists.First(x => x.US_USERID == userName).US_FullName,
                            HI_MOD = "ILK",
                            HI_DATE = sqlDataContext.MK_NOW(),
                            HI_TEXT = documents
                        };
                    sqlDataContext.Histories.InsertOnSubmit(hist);
                    sqlDataContext.SubmitChanges();
                    GetDate();
                }
            }
        }
    
    }


}
