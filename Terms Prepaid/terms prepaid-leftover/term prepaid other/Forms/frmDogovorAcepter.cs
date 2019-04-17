using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;
using lanta.Clients;

namespace terms_prepaid.Forms
{
    public partial class frmDogovorAcepter : Form
    {
        private const string errorMSG1 = "Подписант не сохранен";
        private const string errorMSG2 = "К путевке не привязан постоянный клинет";
        private const string errorMSG3 = "Постоянный клиент заполнен неполностью";
        private const string errorMSG4 = "Необходимо заключить доп соглашение";
        private PrintDocument printDocument1 = new PrintDocument();
        private Bitmap memoryImage;
        private const string selectcountInHistory = @"select count(hi_id) from History where Hi_DGCOD=@dgcod and Hi_mod =@mod ";
        private const string insertDocumentHistory = @"insert into Lanta_DocumentHistory ([DH_DGКеу],[DH_DOGOVOR],[DH_MANAGER],[DH_CREATEDATE],[DH_TYPE]) output inserted.dh_ID values (@dgkey,@dogovor,@manager,GetDate(),@type)";
        private const string insertHistory =
            @"insert into History(HI_DGCOD,HI_DATE,HI_WHO,HI_TEXT,HI_MOD,HI_REMARK) values (@DGCOD,GetDATE(),@WHO,@text,@mod,@remark)";
        private const string selectDogovors = @"select  [DH_DGКеу]
      ,[DH_DOGOVOR]
      ,[DH_MANAGER]
      ,[DH_CREATEDATE]
      ,[DH_TYPE]  from Lanta_DocumentHistory where DH_DGКеу=@dgkey
        order by DH_CREATEDATE desc ";
        private const string updMainMan = @"update Tbl_Dogovor set [DG_MAINMEN] = substring((SELECT    isnull([CL_NAMERUS],'')+' '+  isnull([CL_FNAMERUS],'')+' '+  isnull([CL_SNAMERUS],'')  FROM [Clients] where cl_key=@clkey ),1,70)
      ,[DG_MAINMENPHONE] = (SELECT  cl_phone  FROM [Clients] where cl_key=@clkey)
      ,[DG_MAINMENADRESS]= substring((SELECT  isnull(CL_POSTINDEX,'')+', г.'+isnull(CL_POSTCITY,'')+' ул.'+isnull(CL_POSTSTREET,'')+' д.'+isnull(CL_POSTBILD,'')+' кв.'+isnull(CL_POSTFLAT,'')  FROM [Clients] where cl_key=@clkey ),1,320)
      ,[DG_MAINMENPASPORT] =substring((SELECT  isnull(CL_PASPRUSER,'')+'№'+isnull(CL_PASPRUNUM,'')+' Выдан '+isnull(convert(varchar(20),CL_PASPRUDATE,105),'')+' '+isnull(CL_PASPRUBYWHOM,'')  FROM [Clients] where cl_key=@clkey),1,70)
      ,DG_MAINMENEMAIL =(SELECT  cl_mail FROM [Clients] where cl_key=@clkey)

where dg_key = @dgkey ";
        
        private const string insertDDep = @"INSERT INTO [Lanta_DogovorDeputat]
           ([DD_DGКеу]
           ,[DD_CLКеу]
           ,[DD_MANAGER]
           ,[DD_CREATEDATE])
     VALUES
           (@DD_DGКеу
           ,@DD_CLКеу
           ,(select dg_owner from tbl_dogovor where dg_key =@DD_DGКеу)
           ,GetDate())";
        private const string updDDep = @"update Lanta_DogovorDeputat set DD_CLКеу= @DD_CLКеу where DD_DGКеу=@DD_DGКеу ";
        private const string selectDD = @"SELECT  [DD_ID]
      ,[DD_DGКеу]
      ,[DD_CLКеу]
      ,[DD_MANAGER]
      ,[DD_CREATEDATE]
  FROM [Lanta_DogovorDeputat] where DD_DGКеу=@dgkey ";
        
        private const string selectClient = @"SELECT [CL_KEY]
      ,[CL_NAMERUS]
      ,[CL_FNAMERUS]
      ,[CL_SNAMERUS]
      ,[CL_CITIZEN]
      ,[CL_POSTINDEX]
      ,[CL_POSTCITY]
      ,[CL_POSTSTREET]
      ,[CL_POSTBILD]
      ,[CL_POSTFLAT]
      ,[CL_PHONE]
      ,[CL_PASPRUSER]
      ,[CL_PASPRUNUM]
      ,[CL_PASPRUDATE]
      ,[CL_PASPRUBYWHOM]
      ,[cl_mail]
  FROM [dbo].[Clients] where [CL_KEY]=@clkey";
        private string _dgcod;
        private int _dgkey;
        private int? _clkey=null;
        private int? _dogdepkey=null;
        public frmDogovorAcepter(string dgcod)
        {
            InitializeComponent();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            _dgcod = dgcod;
            GetDate();
        }
        private void printDocument1_PrintPage(System.Object sender,
       System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void GetDate()
        {
            using (
                SqlCommand com = new SqlCommand("select top 1 dg_key from tbl_dogovor where dg_code=@dgcode",
                                                WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dgcode", _dgcod);
                _dgkey = (int) com.ExecuteScalar();
            }
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectDD, WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgkey", _dgkey);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    _clkey = dt.Rows[0].Field<int?>("DD_CLКеу");
                    _dogdepkey = dt.Rows[0].Field<int>("DD_ID");
                }
            }
            CheckNeedDopAgreement();
            SetClientView();

        }

        void CheckNeedDopAgreement()
        {
            int? needdop = 0;
            using (SqlCommand com = new SqlCommand("select dbo.mk_needdopagreement_new(@dgcode)", WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dgcode", _dgcod);
                needdop = (int?)com.ExecuteScalar();
            }
            if (needdop != 0)
            {
                lbMessage.Text = errorMSG4;
            }
        }
        bool ChekClient()
        {
            foreach (var control in tableLayoutPanel1.Controls)
            {
                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    if(tb==tbCitezen){continue;}
                    if (string.IsNullOrEmpty(tb.Text))
                    {
                        return false;
                    }
                    
                }
            }
            return true;
        }
        void SetClientView()
        {
            if (_clkey != null)
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectClient,WorkWithData.Connection) )
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@clkey", _clkey);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        tbCitezen.Text = row.Field<string>("CL_CITIZEN");
                        tbName.Text = row.Field<string>("CL_NAMERUS");
                        tbFname.Text = row.Field<string>("CL_FNAMERUS");
                        tbSname.Text = row.Field<string>("CL_SNAMERUS");
                        tbPaspSer.Text = row.Field<string>("CL_PASPRUSER");
                        tbPaspNum.Text = row.Field<string>("CL_PASPRUNUM");
                        tbPaspWho.Text = row.Field<string>("CL_PASPRUBYWHOM");
                        tbPaspDate.Text =row["CL_PASPRUDATE"]!=DBNull.Value? row.Field<DateTime>("CL_PASPRUDATE").ToString("yyyy-MM-dd"):"";
                        tbPhone.Text = row.Field<string>("CL_PHONE");
                        tbIndex.Text = row.Field<string>("CL_POSTINDEX");
                        tbCity.Text = row.Field<string>("CL_POSTCITY");
                        tbStreet.Text = row.Field<string>("CL_POSTSTREET");
                        tbBild.Text = row.Field<string>("CL_POSTBILD");
                        tbFlat.Text = row.Field<string>("CL_POSTFLAT");
                        tbE_mail.Text = row.Field<string>("cl_mail");
                        if (!ChekClient())
                        {
                            lbMessage.Text = errorMSG3;
                            return;
                        }
                    }
                    else
                    {
                        lbMessage.Text = errorMSG2;
                        return;
                    }
                }
            }
            else
            {
                lbMessage.Text = errorMSG2;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           // if (lbMessage.Text == errorMSG1||string.IsNullOrEmpty(lbMessage.Text))
            //{
                //Сохраниение подписанта
                if (_dogdepkey == null)
                {
                    using (SqlCommand com = new SqlCommand(insertDDep,WorkWithData.Connection))
                    {
                        com.Parameters.AddWithValue("@DD_DGКеу", _dgkey);
                        com.Parameters.AddWithValue("@DD_CLКеу", _clkey);
                        com.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (SqlCommand com = new SqlCommand(updDDep, WorkWithData.Connection))
                    {
                        com.Parameters.AddWithValue("@DD_DGКеу", _dgkey);
                        com.Parameters.AddWithValue("@DD_CLКеу", _clkey);
                        com.ExecuteNonQuery();
                    }
                }
                using (SqlCommand com = new SqlCommand(updMainMan,WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@dgkey", _dgkey);
                    com.Parameters.AddWithValue("@clkey", _clkey);
                    com.ExecuteNonQuery();
                }
                lbMessage.Text = "";
                MessageBox.Show("Сохранение прошло успешно");
          //  }
           // else
            //{
             //   MessageBox.Show("Сохранение не произошло");
         //   }
            GetDate();
        }

        private void btnPersons_Click(object sender, EventArgs e)
        {
            ClientsMainForm scl = new ClientsMainForm("", WorkWithData.GetUserID(), WorkWithData.Connection, true);
            scl.SetButtonSelectText("Выбрать", "Выбор постоянного клиента");
            scl.needCheck = false;
            if (scl.ShowDialog()==DialogResult.OK)
            {
                if (scl.return_CL_KEY != _clkey)
                {
                    lbMessage.Text = errorMSG1;
                    _clkey = scl.return_CL_KEY;
                }
            }
            SetClientView();

        }
         
        private void CaptureScreen()
        {
            foreach (var control in tableLayoutPanel1.Controls)
            {
                Button btn = control as Button;
                if (btn != null)
                {
                    btn.Visible = false;
                }
            }
            
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            s.Height = s.Height - 22;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y+22, 0, 0, s);
                       
            foreach (var control in tableLayoutPanel1.Controls)
            {
                Button btn = control as Button;
                if (btn != null)
                {
                    btn.Visible = true;
                }
            }
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_clkey != null)
            {
                EditClient edc = new EditClient((int)_clkey, WorkWithData.GetUserID(), WorkWithData.Connection, true, false);
                edc.ShowDialog();
            }
            SetClientView();
        }

        private void btnDogovor_Click(object sender, EventArgs e)
        {
            if (lbMessage.Text == ""||lbMessage.Text==errorMSG4)
            {
                string tempPath = Path.GetTempPath();
                
                DataTable dogovorsTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectDogovors, WorkWithData.Connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@dgkey", _dgkey);
                    adapter.Fill(dogovorsTable);
                }
                 
                if (!(dogovorsTable.Rows.Count > 0))
                {
                    WebClient client=new WebClient();
                   //http://mcruises.ru/cabinet/classes/extentions/index.php?dgCode=CAR60214A1&dirDirector=off&print=dopDogovor
                    string url = string.Format("http://mcruises.ru/cabinet/classes/extentions/index.php?dgCode={0}&dirDirector=off",_dgcod);
                   
                    String ext = ".pdf";
                    string filename = tempPath + "Dogovor " + _dgcod + ext;
                    int i;
                    using (SqlCommand com = new SqlCommand(selectcountInHistory,WorkWithData.Connection))
                    {
                        com.Parameters.AddWithValue("@MOD", "cpu");
                        com.Parameters.AddWithValue("@dgcod", _dgcod);
                        i = (int) com.ExecuteScalar();
                    }
                    if (!(i > 0))
                    {

                        CaptureScreen();
                        PrintDialog prd = new PrintDialog();
                        if (prd.ShowDialog()==DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = prd.PrinterSettings;
                            printDocument1.Print();
                            
                        }
                        if (
                            MessageBox.Show("Клиент подтвердил данные?", "Подтверждение данных", MessageBoxButtons.YesNo,MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            WorkWithData.InsertHistory(_dgcod, "Клиент подтвердил данные и дал согласие на заключение нового договора", "CPU", "");
                            //using (SqlCommand com = new SqlCommand(insertHistory,WorkWithData.Connection))
                            //{
                            //    com.Parameters.AddWithValue("@DGCOD", _dgcod);
                            //    com.Parameters.AddWithValue("@WHO", WorkWithData.GetUserName());
                            //    com.Parameters.AddWithValue("@text", "Клиент подтвердил данные и дал согласие на заключение нового договора");
                            //    com.Parameters.AddWithValue("@mod", "CPU");
                            //    com.Parameters.AddWithValue("@remark", "");
                            //    com.ExecuteNonQuery();

                            //}
                            i++;
                        }
                        // PrintDialog ptr = new PrintDialog();

                        //  ptr.PrintToFile();
                        // if(MessageBox.Show(""))
                    }
                    
                    byte[] data = client.DownloadData(url);
                    BinaryWriter bw = new BinaryWriter(File.Create(filename));
                    bw.Write(data);
                    bw.Close();
                    Process.Start(filename);
                    if (i > 0)
                    {
                        if (
                            MessageBox.Show("Клиент подписал договор?", "Подтверждение данных", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            string text = string.Format("Клиент, {0}, заключил договор № {1} {2}",
                                                            tbName.Text + " " + tbFname.Text + " " + tbSname.Text, _dgcod, DateTime.Now.ToString("dd.MM.yyyy"));
                            WorkWithData.InsertHistory(_dgcod, text, "NDG", tbName.Text);
                            //using (SqlCommand com = new SqlCommand(insertHistory, WorkWithData.Connection))
                            //{
                            //    com.Parameters.AddWithValue("@DGCOD", _dgcod);
                            //    com.Parameters.AddWithValue("@WHO", WorkWithData.GetUserName());
                               
                            //    com.Parameters.AddWithValue("@text", text);
                            //    com.Parameters.AddWithValue("@mod", "NDG");
                            //    com.Parameters.AddWithValue("@remark", tbName.Text);
                            //    com.ExecuteNonQuery();

                            //}
                            int? dogovorID = null;
                            using (SqlCommand com = new SqlCommand(insertDocumentHistory, WorkWithData.Connection))
                            {
                                com.Parameters.AddWithValue("@dgkey", _dgkey);
                                com.Parameters.AddWithValue("@dogovor", data);
                                com.Parameters.AddWithValue("@manager", WorkWithData.GetUserID());
                                com.Parameters.AddWithValue("@type", 5);
                                dogovorID = (int?)com.ExecuteScalar();
                            }
                            WorkWithData.ChekDogovorState(dogovorID,_dgcod);
                        }
                    }
                    

                }
                else
                {

                    int? needdop = 0;
                    using (SqlCommand com = new SqlCommand("select dbo.mk_needdopagreement(@dgcode)", WorkWithData.Connection))
                    {
                        com.Parameters.AddWithValue("@dgcode", _dgcod);
                        needdop = (int?) com.ExecuteScalar();
                    }
                    if (needdop == 0)
                    {
                        DataRow row = dogovorsTable.Rows[0];
                        String ext = row.Field<int>("DH_TYPE") == 5 || row.Field<int>("DH_TYPE") == 6
                                         ? ".pdf"
                                         : row.Field<int>("DH_TYPE") == 0 || row.Field<int>("DH_TYPE") == 4 ? ".doc" : "";
                        string filename = tempPath + "Dogovor " + _dgcod + ext;
                        byte[] data = row.Field<byte[]>("DH_DOGOVOR");
                        BinaryWriter bw = new BinaryWriter(File.Create(filename));
                        bw.Write(data);
                        bw.Close();
                        Process.Start(filename);
                    }
                    else
                    {
                        if (MessageBox.Show("Необходимо заключить допсоглашение! Заключить?", "", MessageBoxButtons.YesNo,MessageBoxIcon.Question) ==DialogResult.Yes)
                        {
                            using (WebClient client = new WebClient())
                            {
                                String ext = ".pdf";
                                string url =
                                    string.Format(
                                        "http://mcruises.ru/cabinet/classes/extentions/index.php?dgCode={0}&dirDirector=off&print=dopDogovor",
                                        _dgcod);
                                byte[] data = client.DownloadData(url);
                                string filename = tempPath + "DopDogovor" + _dgcod + ext;
                                using (BinaryWriter bw = new BinaryWriter(File.Create(filename)))
                                {
                                    bw.Write(data);
                                    bw.Close();

                                }
                                Process.Start(filename);
                            }


                            if (MessageBox.Show("Клиент подписал допольнительное соглашение?", "Подтверждение данных",MessageBoxButtons.YesNo,MessageBoxIcon.Question) ==DialogResult.Yes)
                            {
                                using (WebClient client = new WebClient())
                                {
                                    string url =
                                        string.Format(
                                            "http://mcruises.ru/cabinet/classes/extentions/index.php?dgCode={0}&dirDirector=off",
                                            _dgcod);

                                    String ext = ".pdf";
                                    string filename = tempPath + "Dogovor " + _dgcod + ext;
                                    byte[] data = client.DownloadData(url);
                                    using (BinaryWriter bw = new BinaryWriter(File.Create(filename)))
                                    {
                                        bw.Write(data);
                                        bw.Close();

                                    }
                                    string text = string.Format("Клиент, {0}, заключил договор № {1} {2}",
                                                                tbName.Text + " " + tbFname.Text + " " + tbSname.Text,
                                                                _dgcod, DateTime.Now.ToString("dd.MM.yyyy"));
                                    WorkWithData.InsertHistory(_dgcod, text, "NDG", tbName.Text);
                                    int? dogovorID = null;
                                    using (
                                        SqlCommand com = new SqlCommand(insertDocumentHistory, WorkWithData.Connection))
                                    {
                                        com.Parameters.AddWithValue("@dgkey", _dgkey);
                                        com.Parameters.AddWithValue("@dogovor", data);
                                        com.Parameters.AddWithValue("@manager", WorkWithData.GetUserID());
                                        com.Parameters.AddWithValue("@type", 6);
                                        dogovorID = (int?) com.ExecuteScalar();
                                    }
                                    WorkWithData.ChekDogovorState(dogovorID,_dgcod);
                                    if (
                                        MessageBox.Show("Открыть договор польностью?", "",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question) ==
                                        DialogResult.Yes)
                                    {
                                        Process.Start(filename);
                                    }
                                }

                            }
                        }
                        else
                        {
                            DataRow row = dogovorsTable.Rows[0];
                            String ext = row.Field<int>("DH_TYPE") == 5 || row.Field<int>("DH_TYPE") == 6
                                             ? ".pdf"
                                             : row.Field<int>("DH_TYPE") == 0 || row.Field<int>("DH_TYPE") == 4 ? ".doc" : "";
                            string filename = tempPath + "Dogovor " + _dgcod + ext;
                            byte[] data = row.Field<byte[]>("DH_DOGOVOR");
                            BinaryWriter bw = new BinaryWriter(File.Create(filename));
                            bw.Write(data);
                            bw.Close();
                            Process.Start(filename);
                        }

                    }
                   
                }

            }
           
        }
        //void ChekDogovorState(int? dogovorID)
        //{
            
        //    using (SqlCommand com = new SqlCommand("dbo.mk_CheckStateDogovor", WorkWithData.Connection))
        //    {
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@dgcode", _dgcod);
        //        if (dogovorID != null)
        //        {
        //            com.Parameters.AddWithValue("@dogovorId", dogovorID);
        //        }
        //        com.ExecuteNonQuery();
        //    }
        //}
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbMessage_TextChanged(object sender, EventArgs e)
        {
            Font tmpFont = lbMessage.Font;
            if (lbMessage.Text == errorMSG4)
            {
                lbMessage.Font = new Font(tmpFont.FontFamily,tmpFont.Size,FontStyle.Underline);
            }
            else
            {
                lbMessage.Font = new Font(tmpFont.FontFamily, tmpFont.Size);
            }
        }

        private void lbMessage_Click(object sender, EventArgs e)
        {
            if (lbMessage.Text == errorMSG4)
            {
                new frmDopDogovorChanges(_dgcod).ShowDialog();
                
            }
            GetDate();
        }
    }
}
