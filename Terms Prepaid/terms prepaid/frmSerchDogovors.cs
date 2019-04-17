using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Rep10027.Helpers;
using lanta.Clients;


/*
 * 
 * 
 * 
 * 
 * 
 * id	key_filter	name	group_filter
19	1	Документы выложены	5
20	2	Документы распечатаны	5
21	3	Документы запрошены	5
22	4	Документы не выложены	5
23	0	Все брони	5
 * 
 * 
 * 
 */


namespace terms_prepaid
{

    public partial class frmSerchDogovors : Form
    {

        List<users>_documetList= new List<users>(),_visaList=new List<users>(),_bronirList = new List<users>(), _manegerList = new List<users>(), status = new List<users>(), classes = new List<users>(), dogovors = new List<users>();
        private AccessClass _access = new AccessClass(WorkWithData.Connection);
        private const string selDogovors =
            @"Select DG_CODE,DG_TURDATE,DG_MAINMEN,DG_NDAY,DG_NMEN,DG_CRDATE,DG_PRICE,DG_PAYED
            --,case when (DG_PAYED=0) then 'Не оплачена'
	        --	when (DG_PAYED<>0 and DG_PAYED<DG_PRICE) then 'Предоплата'
		    --	when (DG_PAYED>=DG_PRICE) then 'Оплачена' END as payedstatus
            from tbl_dogovor";
        DataTable _dogovors = new DataTable();

        public frmSerchDogovors()
        {
            InitializeComponent();
        }


        void SetAccess()
        {
            String str = "";
            
            if (_access.isSuperViser)
            {
                str = "cупервайзера";
                SetSuper();
            }
            else if(_access.isBronir)
            {
                str = "бронировщика";
                SetBronir();
            }
            else if (_access.isRealize)
            {
                str = "реализатора";
                SetRealize();
            }
            if (!(_access.isBronir || _access.isRealize || _access.isSuperViser))
            {
                MessageBox.Show("У вас не прописаны права доступа!");
                Close();
            }
            lMain.Text = "Рабочее место " + str; tbName.Text= WorkWithData.GetUserName();
        }
        private void SetSuper()
        {
            btnSetting.Visible = true;
            btnBronir.Visible = true;
            btnRealiz.Visible = true;
        }
        private void SetRealize()
        {
            btnSetting.Visible = false;
            btnBronir.Visible = false;
            btnRealiz.Visible = true;
        }
        private void SetBronir()
        {
            btnSetting.Visible = false;
            btnBronir.Visible = true;
            btnRealiz.Visible = true;
        }
        void GetDate()
        {
            SetAccess();
            
           // lMain.Text = "Рабочее место " + WorkWithData.GetStateUser(); tbName.Text= WorkWithData.GetUserName();
             string selectusers = @"select us_fullName,us_key,isnull(is_realiz,0) as sale, isnull( is_bronir,0) as bron from UserList left join mk_user_rule on UR_USKEY=us_key order by us_fullName  ";
            timeRefreshProb_Tick(null,null);
            using (SqlCommand com = new SqlCommand(selectusers, WorkWithData.Connection))
            {
               SqlDataAdapter adapter = new SqlDataAdapter(com);
               DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    if (row.Field<bool>("bron"))
                    {
                        _bronirList.Add(new users(row.Field<int>("us_key"), row.Field<string>("us_fullName")));
                    }
                    if (row.Field<bool>("sale"))
                    {
                        _manegerList.Add(new users(row.Field<int>("us_key"), row.Field<string>("us_fullName")));
                    }

                }
            }

            _manegerList.Insert(0, new users(WorkWithData.GetUserID(), "Мои брони"));
            _bronirList.Insert(0, new users(WorkWithData.GetUserID(), "Мои брони"));
            _bronirList.Insert(0,new users(-1, "Без бронировщика"));
            _bronirList.Insert(0,new users(0,"Все брони"));
            _manegerList.Insert(0,new users(0, "Все брони"));
            lbBronir.DataSource = _bronirList; 
            lbRealizator.DataSource = _manegerList;
            DataTable _filters = WorkWithData.GetFiltersData();
            SetList(dogovors,3,_filters);
            lbDogovor.DataSource = dogovors;
            SetList(classes,1,_filters);
            SetList(_visaList,4,_filters);
            lbVisa.DataSource = _visaList;
            //classes.Add(new users(1,"Круиз"));
            //classes.Add(new users(2, "Проживание"));
            //classes.Add(new users(3, "Трансфер"));
            //classes.Add(new users(4, "Экскурсия"));
            //classes.Add(new users(5, "Перелет"));
            //classes.Add(new users(6, "Прочие услуги"));
            clbClasses.DataSource = classes;
            SetList(status, 2, _filters);
            SetList(_documetList,5,_filters);
            lbDocument.DataSource = _documetList;
            //status.Add(new users(0,"Все брони"));
            //status.Add(new users(1,"Закончатся за 3 часа"));
            //status.Add(new users(2,"Закончатся за день"));
            //status.Add(new users(3, "Подтвержденные"));
            //status.Add(new users(4,"Необработанные"));
            lbStatus.DataSource = status;
        }
        void SetList(List<users> list, int group, DataTable filters)
        {
            foreach (DataRow row in filters.Select("group_filter="+group.ToString()))
            {
                list.Add((new users(row.Field<int>("key_filter"), row.Field<string>("name"))));
            }
        }
        private void frmSerchDogovors_Load(object sender, EventArgs e)
        {
            foreach (var control in Controls)
            {
                DateTimePicker co = control as DateTimePicker;
                if (co != null)
                {
                    co.Value = DateTime.Now.Date;
                }
            }
            this.Location = new System.Drawing.Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
            //dtpDate2s_ValueChanged(sender, e);
           
            GetDate();
            
            
            //this.btnBronir.
            SetFilter();
        }
        void UpdateDataGrid()
        {
            dgvDogovor.DataSource = _dogovors;
            foreach (DataGridViewColumn column in dgvDogovor.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    case "DH_CREATEDATE":
                        {
                            column.DisplayIndex = 6;
                            column.HeaderText = "Договор";

                        }
                        break;
                    case "DG_CODE":
                        {
                            column.DisplayIndex = 1;
                            column.HeaderText = "Номер путевки";

                        }
                        break;
                    case "DG_TURDATE":
                        {
                            column.DisplayIndex = 2;
                            column.HeaderText = "Дата заезда";

                        }
                        break;
                    case "DG_MAINMEN":
                        {
                            column.DisplayIndex = 3;
                            column.HeaderText = "Покупатель";

                        }
                        break;
                    case "DG_NDAY":
                        {
                            column.DisplayIndex = 4;
                            column.HeaderText = "Продолжительность";

                        }
                        break;
                    case "DG_PRICE":
                        {
                            column.DisplayIndex = 9;
                            column.HeaderText = "Полная цена";
                        }
                        break;

                    case "DG_PAYED":
                        {
                            column.DisplayIndex = 10;
                            column.HeaderText = "Оплачено";
                        }
                        break;
                    //case "PAYEDSTATUS":
                    //    {
                    //        column.DisplayIndex = 6;
                    //        column.HeaderText = "Статус";
                    //    }
                    //    break;
                    case "DG_NMEN":
                        {
                            column.DisplayIndex = 5;
                            column.HeaderText = "Кол-во человек";

                        }
                        break;
                    case "DG_CRDATE":
                        {
                            column.DisplayIndex = 0;
                            column.HeaderText = "Дата создания";

                        }
                        break;
                    case "BRONIR":
                        {
                            column.DisplayIndex = 13;
                            column.HeaderText = "Бронировщик";

                        }
                        break;
                    case "MANAG":
                        {
                            column.DisplayIndex = 12;
                            column.HeaderText = "Реализатор";

                        }
                        break;
                    case "OPTIONDATEEND":
                        {
                            column.DisplayIndex = 13;
                            column.HeaderText = "Дата окончания опции";

                        }
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }
       




        private void dgvDogovor_DoubleClick(object sender, EventArgs e)
        {
            string UsingDGCode = dgvDogovor.SelectedRows[0].Cells["dg_code"].Value.ToString();
            //MainForm mainForm = new MainForm(UsingDGCode);
            //mainForm.Text = mainForm.Text
            //                + " ver." + mainForm.GetType().Assembly.GetName().Version.ToString()
            //                + " db:" + WorkWithData.Connection.Database;
            frmNewOptions newOptions = new frmNewOptions(UsingDGCode);
            //this.Hide();
            newOptions.Text += " ver." + newOptions.GetType().Assembly.GetName().Version.ToString()
                          + " db:" + WorkWithData.Connection.Database;
            newOptions.Show();
            //mainForm.ShowDialog();
            //this.Show();
        }

        private void frmSerchDogovors_Move(object sender, EventArgs e)
        {
            
           // this.Location = new System.Drawing.Point(width - this.Size.Width, 0); 
        }



   

        
    

        private void dgvDogovor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Clipboard.GetFileDropList();
        }


       

       
        void SetFilter()
        {
            DateTime? crDateS = null, crDatePo = null, turDateS= null, turDatePo=null;
            if (cbDateCreate.Checked)
            {
                crDatePo = dtpCreateDatePo.Value.Date;
                crDateS = dtpCreateDateS.Value.Date;
            }
            if (cbTurDate.Checked)
            {
                turDatePo = dtpTurDatePo.Value.Date;
                turDateS = dtpTurDateS.Value.Date;
            }
            string classes = null, dgCode = null;
            int? bron= null, manag = null,status=null, dogovor = null,document=null;
            if (!string.IsNullOrEmpty(tbdgcode.Text))
            {
                dgCode = tbdgcode.Text;
            }
            foreach (var item in clbClasses.CheckedItems)
            {
                users classChek = item as users;
                //MessageBox.Show(classChek.UserName + ' ' + classChek.id.ToString());
                if (classes == null)
                {
                    classes = classChek.id.ToString();
                }
                else
                {
                    classes +=","+ classChek.id.ToString();
                }
            }
            users statusSelect = lbStatus.SelectedItem as users;

            try
            {
                status = statusSelect.id;
            }
            catch (Exception)
            {
                
            }


            users documentSelected = lbDocument.SelectedItem as users;
            try
            {
                if (documentSelected.id != 0)
                {
                    document = documentSelected.id;
                }

            }
            catch (Exception)
            {


            }



            users bronSelected = lbBronir.SelectedItem as users;
            try
            {
                if (bronSelected.id != 0)
                {
                    bron = bronSelected.id;
                }

            }
            catch (Exception)
            {

              
            }

            users dogovorSel = lbDogovor.SelectedItem as users;
            try
            {
                if (dogovorSel.id != 0)
                {
                    dogovor = dogovorSel.id;
                }

            }
            catch (Exception)
            {


            }

            users managSelected = lbRealizator.SelectedItem as users;
            try
            {
                if (managSelected.id != 0)
                {
                    manag = managSelected.id;
                }

            }
            catch (Exception)
            {

              
            }
            bool isАnnul = cbAnnul.Checked;
            int count = 0;
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bron", bron);
                com.Parameters.AddWithValue("@maneg", manag);
                com.Parameters.AddWithValue("@crDateBeg", crDateS);
                com.Parameters.AddWithValue("@crDateEnd", crDatePo);
                com.Parameters.AddWithValue("@DateBeg", turDateS);
                com.Parameters.AddWithValue("@DateEnd", turDatePo);
                com.Parameters.AddWithValue("@classes", classes);
                com.Parameters.AddWithValue("@dgcode", dgCode);
                com.Parameters.AddWithValue("@status", status);
                com.Parameters.AddWithValue("@annul", isАnnul);
                com.Parameters.AddWithValue("@dogovor", dogovor);
                com.Parameters.AddWithValue("@document", document);
                com.Parameters.AddWithValue("@count", count);
                com.Parameters["@count"].Direction =ParameterDirection.Output;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                _dogovors.Rows.Clear();
                adapter.Fill(_dogovors);
                count = (int) com.Parameters["@count"].Value;

            }
            UpdateDataGrid();
            dgvDogovor.Refresh();
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            SetFilter();
        }




        private void dtpCreateDateS_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCreateDatePo.Value < dtpCreateDateS.Value)
            {
                dtpCreateDatePo.Value = dtpCreateDateS.Value;
            }
            cbDateCreate.Checked = true;
        }

        private void dtpCreateDatePo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCreateDatePo.Value< dtpCreateDateS.Value)
            {
                dtpCreateDateS.Value = dtpCreateDatePo.Value;
            }
            cbDateCreate.Checked = true;
        }

        private void dtpTurDateS_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTurDatePo.Value < dtpTurDateS.Value)
            {
                dtpTurDatePo.Value = dtpTurDateS.Value;
            }
            cbTurDate.Checked = true;
        }

        private void dtpTurDatePo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTurDatePo.Value < dtpTurDateS.Value)
            {
                dtpTurDateS.Value = dtpTurDatePo.Value;
            }
            cbTurDate.Checked = true;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
           if(dgvDogovor.SelectedRows.Count<1){return;}
            DataGridViewRow row = dgvDogovor.SelectedRows[0];
            int? bron = null;
            try
            {
              bron= int.Parse((row.Cells["bron_key"].Value.ToString()).ToString());
            }
            catch (Exception)
            {

                bron = null;
            }

            int? realize = null;
            try
            {
                realize = (int.Parse(row.Cells["manag_key"].Value.ToString()));
            }
            catch (Exception)
            {

                realize = null;
            }
            
            string dgCode = row.Cells["DG_CODE"].Value.ToString();
            
            frmDogovorSettings setting = new frmDogovorSettings(dgCode,bron,realize);
            setting.ShowDialog();
            SetFilter();
        }

        private void btnBronir_Click(object sender, EventArgs e)
        {
            if (dgvDogovor.SelectedRows.Count < 1) { return; }
            DataGridViewRow row = dgvDogovor.SelectedRows[0];
            string dgCode = row.Cells["DG_CODE"].Value.ToString();
            using (SqlCommand com = new SqlCommand("mk_set_bronir", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dgcode", dgCode);
                com.Parameters.AddWithValue("@user", WorkWithData.GetUserID());
                com.ExecuteNonQuery();
            }
            if (
                MessageBox.Show("Поменять бронировщика на все услуги?", "", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlCommand com = new SqlCommand("update mk_DogovorListAdd set bron=@user where dg_code=@dgcode", WorkWithData.Connection))
                {
                    com.Parameters.AddWithValue("@dgcode", dgCode);
                    com.Parameters.AddWithValue("@user", WorkWithData.GetUserID());
                    com.ExecuteNonQuery();
                }
            }
            SetFilter();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnNewBrony_Click(object sender, EventArgs e)
        {
            SetListBox(lbStatus,4);
            SetListBox(lbBronir,-1);
            SetListBox(lbRealizator,0);
            SetListBox(lbDogovor,0);
            SetListBox(lbDocument, 0);
            cbDateCreate.Checked = false;
            cbTurDate.Checked = false;
            tbdgcode.Text = "";
            ClearSelectClasses();
            SetFilter();
      
            
        }
        /// <summary>
        /// Выставление значения в лист боксе
        /// </summary>
        /// <param name="lb">Листобокс</param>
        /// <param name="id">Значение</param>
        void SetListBox(ListBox lb,int id)
        {
                  for (int i = 0; i < lb.Items.Count; i++)
            {
                users tmp = lb.Items[i] as users;
                if (tmp.id == id)
                {
                    lb.SelectedItem = lb.Items[i];
                    break;
                }
            }
        }
        private void btnRealiz_Click(object sender, EventArgs e)
        {
            if (dgvDogovor.SelectedRows.Count < 1) { return; }
            DataGridViewRow row = dgvDogovor.SelectedRows[0];
            string dgCode = row.Cells["DG_CODE"].Value.ToString();
            using (SqlCommand com = new SqlCommand("update tbl_Dogovor set DG_OWNER=@user where DG_CODE=@dgcode", WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@dgcode", dgCode);
                com.Parameters.AddWithValue("@user", WorkWithData.GetUserID());
                com.ExecuteNonQuery();
            }
            SetFilter();
        }

        private void btnMyBrony_Click(object sender, EventArgs e)
        {
            SetListBox(lbStatus, 0);
            SetListBox(lbBronir, WorkWithData.GetUserID());
            SetListBox(lbRealizator, WorkWithData.GetUserID());
            SetListBox(lbDogovor, 0);
            SetListBox(lbDocument, 0);
            cbDateCreate.Checked = false;
            cbTurDate.Checked = false;
            tbdgcode.Text = "";
            ClearSelectClasses();
            SetFilter();
        }
        void ClearSelectClasses()
        {
            for (int i = 0; i < clbClasses.Items.Count; i++)
            {
                clbClasses.SetItemChecked(i,false);
            }
            
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnProblemBrony_Click(object sender, EventArgs e)
        {
            _dogovors = frmProblemBron.GetProblemBron(_dogovors);
            UpdateDataGrid();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Height = tb.Text.Length;

          
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            ClientsMainForm scl = new ClientsMainForm("",WorkWithData.GetUserID(), WorkWithData.Connection,true);
            scl.SetButtonSelectText("", "Выбор информации о постоянном клиенте");
            scl.needCheck = false;
            scl.ShowDialog();

        }

        private void timeRefreshProb_Tick(object sender, EventArgs e)
        {
            int ProblemCount = 0;
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@status", 1);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                ProblemCount+= (int)com.Parameters["@count"].Value;

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@status", 6);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                ProblemCount += (int)com.Parameters["@count"].Value;

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@status", 7);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                ProblemCount += (int)com.Parameters["@count"].Value;

            }
            //using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@status", 5);
            //    com.Parameters.AddWithValue("@problem", true);
            //    com.Parameters.AddWithValue("@count", 0);
            //    com.Parameters["@count"].Direction = ParameterDirection.Output;
            //    com.ExecuteNonQuery();
            //    ProblemCount += (int)com.Parameters["@count"].Value;

            //}
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@dogovor", 2);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                ProblemCount += (int)com.Parameters["@count"].Value;

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@document", 1);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                ProblemCount += (int)com.Parameters["@count"].Value;

            }
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@document", 4);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters.AddWithValue("@problem", true);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                ProblemCount += (int)com.Parameters["@count"].Value;

            }
            tbProblemBron.Text = ProblemCount.ToString();
            if (ProblemCount > 0)
            {
                btnProblemBrony.BackColor = Color.Red;
                tbProblemBron.ForeColor = Color.Red;
            }
            else
            {
                btnProblemBrony.BackColor = SystemColors.Control;
                tbProblemBron.ForeColor = SystemColors.ControlText;
            }
        }


    }
}
