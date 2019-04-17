﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Documents;
using System.Windows.Forms;
//using System.Windows.Input;
using Awesomium.Core;
using HistoryServices;
using Microsoft.VisualBasic.Devices;
using lanta.SQLConfig;
using terms_prepaid.Helpers;
using RingsFromSite;
using lanta.Clients;
using terms_prepaid.Forms;
using Cursor = System.Windows.Forms.Cursor;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using SortOrder = System.Windows.Forms.SortOrder;


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
       
       // private DataTable _option3, _noCreateInshur, _noAcceptUsluga, _noDogovorAccept, _noInsertDocument, _general;
        private const string updteNew = "insert into dbo.mk_messageStatus(MS_HIID,MS_IsRead,MS_USKEY) " +
                                       "select HI_ID,1,@user from History where HI_MOD in ('MTM','WWW') and [HI_DGCOD]=@p2 and HI_ID not in (select MS_HIID from dbo.mk_messageStatus where MS_USKEY=@user ) " +
                                       "update mk_messageStatus set MS_IsRead=1 where MS_HIID in(select HI_ID from History where [HI_DGCOD]=@p2 ) ";
        List<users>_documetList= new List<users>(),_visaList=new List<users>(),_bronirList = new List<users>(), _manegerList = new List<users>(), status = new List<users>(), classes = new List<users>(), dogovors = new List<users>();
        private AccessClass _access = new AccessClass(WorkWithData.Connection);
        DataTable _dogovors = new DataTable();

        private string[] collumns = new string[]
            {
                "BUTTON",
                "BUTTON1",
                "VISASTATUS",
                "WARNING",
                "DG_CRDATE",
                "DG_CODE",
                "DG_TURDATEEXT",
                "DG_MAINMEN",
                "DG_NDAY",
                "DG_NMEN",
                "DH_CREATEDATE",
                "DG_PRICE",
                "DG_PAYED",
                "MANAG",
                "BRONIR",
                "OPTIONDATEEND",
                "BUTTON3",
                "BUTTON4",
                "DG_PAYMENTDATE"
            };
        
        public frmSerchDogovors()
        {
            InitializeComponent();
            GetDate();
        }
        void SortColumn()
        {
            int i = 0;
            foreach (string collumn in collumns)
            {
                try
                {
                    DataGridViewColumn col = dgvDogovor.Columns[collumn];
                    col.DisplayIndex = i;
                    i++;
                }
                catch (Exception)
                {
                    
                }
            }
        }

        void SetAccess()
        {
            String str = "";
            
            if (_access.isSuperViser)
            {
                //str = "cупервайзера";
                SetSuper();
            }
            else if(_access.isBronir)
            {
               //str = "бронировщика";
                SetBronir();
            }
            else if (_access.isRealize)
            {
                //str = "реализатора";
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
            tsmDay.Visible = true;
            btnAll.Visible = true;
            btnRingFromSite.Visible = true;
            tableLayoutPanel2.RowStyles[1].Height = (int)(1.4*tableLayoutPanel2.RowStyles[1].Height);
            tableLayoutPanel13.ColumnStyles[3].Width = 2*tableLayoutPanel13.ColumnStyles[3].Width;
            tableLayoutPanel13.Controls.Remove(tbProblemBron);
            tableLayoutPanel13.Controls.Add(new TableLayoutPanel()
                {
                    Name = "tlpProblems",
                    ColumnCount = 2,
                    RowCount = 2,
                },3,0);
            TableLayoutPanel tlp = tableLayoutPanel13.Controls["tlpProblems"] as TableLayoutPanel;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tlp.Controls.Add(tbProblemBron,0,1);
            tlp.Controls.Add(new Label()
                {
                    Text = "Все",
                    Name = "lbAllPr",
                    ForeColor = Color.Blue,
                    BorderStyle = BorderStyle.FixedSingle,
                   // Font = new Font(Font.FontFamily,Font.Size,{}),
                    Dock = DockStyle.Bottom
                },0,0);
            Label lbTemp = tlp.Controls["lbAllPr"] as Label;
            //lbTemp.Font.Bold = true;
            lbTemp.Click += lbTemp_Click;
            tlp.Controls.Add(new Label()
            {
                Text = "Мои",
                Name = "lbMyPr",
                ForeColor = Color.Red,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Bottom
            }, 1, 0);
            Label lbTemp1 = tlp.Controls["lbMyPr"] as Label;
            lbTemp1.Click += lbTemp1_Click;
            tlp.Controls.Add(new TextBox()
                {
                    Name = "tbMyProb",
                    Dock = DockStyle.Bottom
                },1,1);
            tableLayoutPanel2.RowStyles[3].Height = (int)(1.3 * tableLayoutPanel2.RowStyles[3].Height);
            tableLayoutPanel3.ColumnStyles[2].Width = 2 * tableLayoutPanel3.ColumnStyles[2].Width;
            tableLayoutPanel3.Controls.Remove(tbMessages);

            tableLayoutPanel3.Controls.Add(new TableLayoutPanel()
            {
                Name = "tlpMessage",
                ColumnCount = 2,
                RowCount = 2,
            }, 2, 0);

            tlp = tableLayoutPanel3.Controls["tlpMessage"] as TableLayoutPanel;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tlp.Controls.Add(tbMessages, 0, 1);
            tlp.Controls.Add(new Label()
            {
                Text = "Все",
                Name = "lbAllM",
                ForeColor = Color.Blue,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Bottom
            }, 0, 0);
           Label lbTempM = tlp.Controls["lbAllM"] as Label;
           lbTempM.Click += lbTempM_Click;
            tlp.Controls.Add(new Label()
            {
                Text = "Мои",
                Name = "lbMyM",
                ForeColor = Color.Red,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Bottom
            }, 1, 0);
            Label lbTempM1 = tlp.Controls["lbMyM"] as Label;
            lbTempM1.Click += lbTempM1_Click;
            tlp.Controls.Add(new TextBox()
            {
                Name = "tbMyM",
                Dock = DockStyle.Bottom
            }, 1, 1);

        }

        void lbTempM_Click(object sender, EventArgs e)
        {
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@newmessage", true);
                com.Parameters.AddWithValue("@count", 0);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                _dogovors = new DataTable();
                //_dogovors.Columns.Clear();
                //_dogovors.Clear();
                adapter.Fill(_dogovors);
                tbMessages.Text = com.Parameters["@count"].Value.ToString();

            }
            _dogovors.Columns.Add("Button", typeof(Image));
            _dogovors.Columns.Add("Button1", typeof(Image));
            foreach (DataRow row in _dogovors.Rows)
            {
                row["Button"] = Properties.Resources.mailIconMin;
                row["Button1"] = Properties.Resources.delete; //GetButton(row.Field<string>("DG_CODE"));
            }
            UpdateDataGrid();
        }

        void lbTempM1_Click(object sender, EventArgs e)
        {
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@newmessage", true);
                com.Parameters.AddWithValue("@OnlyMY", true);
                com.Parameters.AddWithValue("@count", 0);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                _dogovors = new DataTable();
                //_dogovors.Columns.Clear();
                //_dogovors.Clear();
                adapter.Fill(_dogovors);
                ((tableLayoutPanel3.Controls["tlpMessage"] as TableLayoutPanel).Controls["tbMyM"] as TextBox).Text = com.Parameters["@count"].Value.ToString();

            }
            _dogovors.Columns.Add("Button", typeof(Image));
            _dogovors.Columns.Add("Button1", typeof(Image));
            foreach (DataRow row in _dogovors.Rows)
            {
                row["Button"] = Properties.Resources.mailIconMin;
                row["Button1"] = Properties.Resources.delete; //GetButton(row.Field<string>("DG_CODE"));
            }
            UpdateDataGrid();
        }

        void lbTemp1_Click(object sender, EventArgs e)
        {
            //new frmProblemBron(1).ShowDialog();
            _dogovors = frmProblemBron.GetProblemBron(_dogovors, 1);
            UpdateDataGrid();
        }

        void lbTemp_Click(object sender, EventArgs e)
        {
            _dogovors = frmProblemBron.GetProblemBron(_dogovors, 0);
            UpdateDataGrid();
        }
        private void SetRealize()
        {
            btnSetting.Visible = false;
            btnBronir.Visible = false;
            btnRealiz.Visible = true;
            tsmDay.Visible = false;
            btnAll.Visible = false;
            btnRingFromSite.Visible = false;
        }
        private void SetBronir()
        {
            btnSetting.Visible = false;
            btnBronir.Visible = true;
            btnRealiz.Visible = true;
            tsmDay.Visible = false;
            btnAll.Visible = false;
            btnRingFromSite.Visible = false;
        }
        void GetDate()
        {
            SetAccess();
            // lMain.Text = "Рабочее место " + WorkWithData.GetStateUser(); tbName.Text= WorkWithData.GetUserName();
             string selectusers = @"select us_fullName,us_key,isnull(is_realiz,0) as sale, isnull( is_bronir,0) as bron from UserList left join mk_user_rule on UR_USKEY=us_key order by us_fullName  ";
            
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
            _manegerList.Insert(0,new users(-1,"Без реализатора"));
            _manegerList.Insert(0,new users(0, "Все брони"));
            
            cbBronir.DataSource = _bronirList;
            cbRealizator.DataSource = _manegerList;
            
            
            
            lbBronir.DataSource = _bronirList; 
            lbRealizator.DataSource = _manegerList;
            DataTable _filters = WorkWithData.GetFiltersData();
            SetList(dogovors,3,_filters);
            cbDogovor.DataSource = dogovors;

            lbDogovor.DataSource = dogovors;
            SetList(classes,1,_filters);
            SetList(_visaList,4,_filters);
            lbVisa.DataSource = _visaList;
            cbVisa.DataSource = _visaList;
            //classes.Add(new users(1,"Круиз"));
            //classes.Add(new users(2, "Проживание"));
            //classes.Add(new users(3, "Трансфер"));
            //classes.Add(new users(4, "Экскурсия"));
            //classes.Add(new users(5, "Перелет"));
            //classes.Add(new users(6, "Прочие услуги"));
            clbClasses.DataSource = classes;
            SetList(status, 2, _filters);
            cbStatus.DataSource = status;
            SetList(_documetList,5,_filters);
            cbDocument.DataSource = _documetList;
            lbDocument.DataSource = _documetList;
            //status.Add(new users(0,"Все брони"));
            //status.Add(new users(1,"Закончатся за 3 часа"));
            //status.Add(new users(2,"Закончатся за день"));
            //status.Add(new users(3, "Подтвержденные"));
            //status.Add(new users(4,"Необработанные"));
            lbStatus.DataSource = status;
            cbDateCreate.Checked = true;
            timeRefreshMessage.Enabled = true;
            timeRefreshProb.Enabled = true;
            timeRefreshProb_Tick(null, null);
            timeRefreshMessage_Tick(null, null);
            dayTasksTime_Tick(null,null);
            dtpTurDatePo.Value = WorkWithData.GetMaxDateDogovor();
            cbTurDate.Checked = true;
            btnDay.Text = "Установка дня от " + DateTime.Now.ToString("dd.MM.yy");
            instalitionTime_Tick(null,null);
            
            //using (SqlCommand com = new SqlCommand("mk_is_ustanovky",WorkWithData.Connection))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.AddWithValue("@us_key", WorkWithData.GetUserID());
            //    bool flag = (bool) com.ExecuteScalar();
            //    if (!flag)
            //    {
            //        btnDay_Click(null,null);
            //    }
            //}

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
           
            
            
            
            //this.btnBronir.
            SetFilter();
            dgvDogovor.RowHeadersWidth += 10;
        }

        void UpdateDataGrid()
        {
            try
            {
                _dogovors.Columns.Add("Button3", typeof(Image));
            }
            catch (Exception)
            {
                
              
            }
            try
            {
                _dogovors.Columns.Add("DG_TURDATEEXT", typeof(string));
            }
            catch (Exception)
            {


            }
            try
            {
                _dogovors.Columns.Add("warning", typeof(Image));
            }
            catch (Exception)
            {
                
                
            }
            try
            {
               // _dogovors.Columns.Add("Button4", typeof (Image));
            }
            catch (Exception)
            {
                
            }
            
            foreach (DataRow row in _dogovors.Rows)
            {
                row["Button3"] = Properties.Resources.gear;
                if (row.Field<int>("countProblem") > 0 || row.Field<int>("countChanges") > 0)
                {
                    row["warning"] = Properties.Resources.warning_2;
                }
                else
                {
                    row["warning"] = Properties.Resources.empty;
                }
                if (row.Field<DateTime>("DG_TURDATE").Date.Equals(new DateTime(1899, 12, 30)))
                {
                    row["DG_TURDATEEXT"] = "Аннулированна";
                }
                else
                {
                    row["DG_TURDATEEXT"] = row.Field<DateTime>("DG_TURDATE").ToString("dd.MM.yy");
                }

                //  row["Button4"] = Properties.Resources.history;
            }
           
            dgvDogovor.DataSource = _dogovors;

            foreach (DataGridViewColumn column in dgvDogovor.Columns)
            {
                switch (column.Name.ToUpper())
                {
                    
                    case "DH_CREATEDATE":
                        {
                            column.DisplayIndex = 6;
                            column.Width = 80;
                            column.HeaderText = "Договор";


                        }
                        break;
                    case "WARNING":
                        {
                            column.DisplayIndex = 1;
                            column.Width = 60;
                            column.HeaderText = "Новые изменения";

                        }
                        break;
                    case "DG_CODE":
                        {
                            column.DisplayIndex = 2;
                            column.Width = 88;
                            column.HeaderText = "Номер путевки";

                        }
                        break;
                    case "DG_TURDATEEXT":
                        {
                            column.DisplayIndex = 2;
                            column.Width = 60;
                            column.HeaderText = "Дата заезда";
                            column.DefaultCellStyle.Format = "dd.MM.yy";
                        }
                        break;
                    case "DG_MAINMEN":
                        {
                            column.DisplayIndex = 3;
                            column.Width = 210;
                            column.HeaderText = "Покупатель";

                        }
                        break;
                    case "DG_NDAY":
                        {
                            column.DisplayIndex = 4;
                            column.Width = 40;
                            column.HeaderText = "н / дн";

                        }
                        break;
                    case "DG_PRICE":
                        {
                            column.DisplayIndex = 9;
                            column.Width = 60;
                            column.HeaderText = "Полная цена, у.е";
                            column.DefaultCellStyle.Format = "F0";
                        }
                        break;

                    case "DG_PAYED":
                        {
                            column.DisplayIndex = 10;
                            column.Width = 60;
                            column.HeaderText = "Оплачено, у.е";
                            column.DefaultCellStyle.Format = "F0";

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
                            column.Width = 42;
                            column.HeaderText = "Кол-во pax";

                        }
                        break;
                    case "DG_CRDATE":
                        {
                            column.DisplayIndex = 0;
                            column.Width = 90;
                            column.HeaderText = "Дата создания";
                            column.DefaultCellStyle.Format = "dd.MM.yy HH:mm";
                        }
                        break; 
                    case "DG_PAYMENTDATE":
                        {
                            column.DisplayIndex = 16;
                            column.Width = 60;
                            column.HeaderText = "Дата полной оплаты";
                            column.DefaultCellStyle.Format = "dd.MM.yy";
                        }
                        break;
                    case "BRONIR":
                        {
                            column.DisplayIndex = 13;
                            column.Width = 80;
                            column.HeaderText = "Бронировщик";

                        }
                        break;
                    case "MANAG":
                        {
                            column.DisplayIndex = 12;
                            column.Width = 80;
                            column.HeaderText = "Реализатор";

                        }
                        break;
                    case "OPTIONDATEEND":
                        {
                            column.DisplayIndex = 13;
                            column.Width = 80;
                            column.HeaderText = "Окончание опции";

                        }
                        break;
                    case "BUTTON":
                        {
                            column.DisplayIndex = 0;
                            column.Width = 50;
                            column.HeaderText = "Переписка";
                        }
                        break;
                    case "BUTTON1":
                        {
                            column.DisplayIndex = 0;
                            column.Width = 50;
                            column.HeaderText = "Удалить";
                        }
                        break;
                    //case "COUNTPROBLEM":
                    //    {
                    //        column.DisplayIndex = 14;
                    //        column.Width = 62;
                    //        column.HeaderText = "Проблемы";
                    //    }
                    //    break;
                    //case "COUNTCHANGES":
                    //    {
                    //        column.DisplayIndex = 15;
                    //        column.Width = 69;
                    //        column.HeaderText = "Изменения";
                    //    }
                    //    break;
                    case "BUTTON3":
                        {
                            column.DisplayIndex = 16;
                            column.Width = 70;
                            column.HeaderText = "Продлить договор\\ оплату";
                        }
                        break;
                    case "BUTTON4":
                        {
                            column.DisplayIndex = 16;
                            column.Width = 70;
                            column.HeaderText = "История по путевке";
                        }
                        break;
                    case "VISASTATUS":
                        {
                            column.DisplayIndex = 0;
                            column.Width = 220;
                            column.HeaderText = "Статус визы";
                        }
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
                column.SortMode=DataGridViewColumnSortMode.Programmatic;
            }
            SortColumn();
        }

        private void frmSerchDogovors_Move(object sender, EventArgs e)
        {
            
           // this.Location = new System.Drawing.Point(width - this.Size.Width, 0); 
        }



   

        
    

        private void dgvDogovor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Clipboard.GetFileDropList();
            if (e.RowIndex >= 0&&e.ColumnIndex>=0)
            {
                DataGridViewColumn column = dgvDogovor.Columns[e.ColumnIndex];
                if (column.Name == "Button")
                {

                    frmNewOptions msg = new frmNewOptions(
                    dgvDogovor.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString(), 1);
                    //this.Hide();
                    msg.ShowDialog();
                    this.Show();
                    btnNewMessages_Click(null, null);
                    // timeRefreshMessage_Tick(null, null);
                }
                else if (column.Name == "DG_CODE")
                {
                    try
                    {
                        Clipboard.SetText(dgvDogovor.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString());
                    }
                    catch (Exception)
                    {

                    }


                }
                else if (column.Name == "Button1")
                {
                    using (SqlCommand com = new SqlCommand(updteNew, WorkWithData.Connection))
                    {
                        com.Parameters.AddWithValue("@p2", dgvDogovor.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString());
                        com.Parameters.AddWithValue("@user", WorkWithData.GetUserID());
                        com.ExecuteNonQuery();
                    }
                    DataRow row = _dogovors.Rows[e.RowIndex];
                    _dogovors.Rows.Remove(row);
                    int meescount = int.Parse(tbMessages.Text);
                    meescount--;
                    tbMessages.Text = meescount.ToString();
                    //btnNewMessages_Click(null, null);
                }
                else if (column.Name == "optionDateEnd")
                {

                    int x = Cursor.Position.X, y = Cursor.Position.Y;
                    new frmOptionsDates(dgvDogovor.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString(), x,y).ShowDialog();
                }
                else if (column.Name == "Button3")
                {
                    string _dgCode = dgvDogovor.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString();

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
                            DateTime date = (DateTime)com.ExecuteScalar();
                            text = string.Format(text, date.ToString("dd.MM.yyyy"));

                        }
                     
                        WorkWithData.InsertHistory(_dgCode,text,"MTC","");
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
                }
                else if (column.Name == "Button4")
                {
                 // string _dgCode = dgvDogovor.Rows[e.RowIndex].Cells["DG_CODE"].Value.ToString();
                  //  frmHistory frm = new frmHistory(_dgCode,WorkWithData.Connection);
                   // frm.ShowDialog();
                }
                else if (column.Name.ToUpper() == "DG_MAINMEN")
                {
                    
                }
            }

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
            string classes = null, dgCode = null,partnerNumber = null,turist = null;
            int? bron= null, manag = null,status=null, dogovor = null,document=null,visa=null;
            if (!string.IsNullOrEmpty(tbdgcode.Text))
            {
                dgCode = tbdgcode.Text.Trim();
            }
            if (!string.IsNullOrEmpty(tbParnerNumber.Text))
            {
                partnerNumber = tbParnerNumber.Text.Trim();
            }

            if (!string.IsNullOrEmpty(tbTurist.Text))
            {
                turist = tbTurist.Text.Trim();
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
            users visSel = lbVisa.SelectedItem as users;
            try
            {
                if (visSel.id != 0)
                {
                    visa = visSel.id;
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
                com.Parameters.AddWithValue("@partnernumber", partnerNumber );
                com.Parameters.AddWithValue("@dogovor", dogovor);
                com.Parameters.AddWithValue("@document", document);
                com.Parameters.AddWithValue("@visa", visa);
                com.Parameters.AddWithValue("@turist", turist);
                com.Parameters.AddWithValue("@count", count);
                com.Parameters.AddWithValue("@NonDep", cbNonDep.Checked);
                com.Parameters["@count"].Direction =ParameterDirection.Output;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                //_dogovors.Columns.Clear();
                //_dogovors.Clear();
                _dogovors = new DataTable();
                adapter.Fill(_dogovors);
                count = (int) com.Parameters["@count"].Value;

            }
            

            
            UpdateDataGrid();
            dgvDogovor.Refresh();
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            lStatus.Text = "";
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
            lStatus.Text = "Новые брони";
            
            SetListBox(lbStatus,-1);
            SetListBox(lbBronir,-1);
            SetListBox(lbRealizator,-1);
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
            lStatus.Text = "Все мои брони";
            SetListBox(lbStatus, 0);
            SetListBox(lbBronir, WorkWithData.GetUserID());
            SetListBox(lbRealizator, WorkWithData.GetUserID());
            SetListBox(lbDogovor, 0);
            SetListBox(lbDocument, 0);
            cbDateCreate.Checked = false;
            dtpTurDateS.Value = DateTime.Now.Date;
            dtpTurDatePo.Value = WorkWithData.GetMaxDateDogovor().Date;
            cbTurDate.Checked = true;
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

        //Button GetButton(string dgCode)
        //{
        //    Button btn = new Button();
        //    btn.Dock = DockStyle.Fill;
        //    btn.Tag = dgCode;
        //    btn.Image = Properties.Resources.mailIconMin;
        //    btn.Click += btn_Click;
        //    btn.ImageAlign = ContentAlignment.MiddleCenter;
        //    // btn.Image
        //    return btn;
        //}
        //void btn_Click(object sender, EventArgs e)
        //{
        //    Button btn = sender as Button;
        //    string dgCod = btn.Tag as string;
        //    frmMessages mess = new frmMessages(dgCod);
        //    mess.ShowDialog();
        //    timeRefreshMessage_Tick(null, null);
        //   // GetData();
        //}
 

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnProblemBrony_Click(object sender, EventArgs e)
        {
            _dogovors = frmProblemBron.GetProblemBron(_dogovors);
            if (_dogovors.TableName != "")
            {
                lStatus.Text = "Проблемные брони";
                string tableName = _dogovors.TableName.Replace("MY", "");
                string problemName = string.Empty;
                if (tableName != "ALL")
                {
                    try
                    {
                        using (SqlCommand com = new SqlCommand("select top 1 mpc_name from mk_problemcodes where mpc_TableName=@p1 ", WorkWithData.Connection))
                        {
                            com.Parameters.AddWithValue("@p1", tableName);
                            problemName = (string)com.ExecuteScalar();
                        }
                    }
                    catch (Exception)
                    {

                    }

                    
                }
                else
                {
                    problemName = "Все";
                }
                lStatus.Text += " (" + problemName + ")";
                int p1 = lStatus.Text.IndexOf("(");
                lStatus.Select(p1,lStatus.Text.Length-p1);
                lStatus.SelectionFont = new Font(lStatus.Font.FontFamily,7);
            }

            //lStatus.Text = "Проблемные брони";
            UpdateDataGrid();
            //if (WorkWithData.GetUserID() == 2011)
            //{
            //    new frmDopDogovorChanges("NCL51231A2").ShowDialog();
            //}
            
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
            DataSet problemSet = WorkWithData.GetAllProblems();
            ProblemCount = _access.isSuperViser ? problemSet.Tables["All"].Rows.Count : problemSet.Tables["AllMY"].Rows.Count;
            tbProblemBron.Text = ProblemCount.ToString();

            if (_access.isSuperViser)
            {
                int ProblemCountMy = 0;
                ProblemCountMy = problemSet.Tables["AllMY"].Rows.Count;
                TextBox tbMyProblemsCount =
                    (tableLayoutPanel13.Controls["tlpProblems"] as TableLayoutPanel).Controls["tbMyProb"] as TextBox;
                tbMyProblemsCount.Text = ProblemCountMy.ToString();
                if (ProblemCountMy > 0)
                {
                    btnProblemBrony.BackColor = Color.Red;
                    tbMyProblemsCount.ForeColor = Color.Red;
                }
                else
                {
                    btnProblemBrony.BackColor = SystemColors.Control;
                    tbMyProblemsCount.ForeColor = SystemColors.ControlText;
                }

            }
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

        private void timeRefreshMessage_Tick(object sender, EventArgs e)
        {
            int count = 0;
            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@newmessage", true);
                com.Parameters.AddWithValue("@count", 0);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                com.ExecuteNonQuery();
                count += (int) com.Parameters["@count"].Value;


            }

            tbMessages.Text = count.ToString();
            if (_access.isSuperViser)
            {

                int count1 = 0;
                using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@newmessage", true);
                    com.Parameters.AddWithValue("@OnlyMy", true);
                    com.Parameters.AddWithValue("@count", 0);
                    com.Parameters["@count"].Direction = ParameterDirection.Output;
                    com.ExecuteNonQuery();
                    count1 += (int) com.Parameters["@count"].Value;
                    ((tableLayoutPanel3.Controls["tlpMessage"] as TableLayoutPanel).Controls["tbMyM"] as TextBox).Text =
                        count1.ToString();


                }
            }
        }

        //if (int.Parse(tbMessages.Text) > 0)
            //{
            //    timePulse.Enabled = true;
            //}
            //else
            //{
            //    btnNewMessages.BackColor = SystemColors.Control;
            //    timePulse.Enabled = false;
            //}
        
        
        private void btnNewMessages_Click(object sender, EventArgs e)
        {



            //frmNewMessages nmess = new frmNewMessages();
            //nmess.ShowDialog();
            //timeRefreshMessage_Tick(null, null);

            using (SqlCommand com = new SqlCommand("mk_search_dogovor", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@newmessage", true);
                com.Parameters.AddWithValue("@count", 0);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                com.Parameters["@count"].Direction = ParameterDirection.Output;
                _dogovors = new DataTable();
                //_dogovors.Columns.Clear();
                //_dogovors.Clear();
                adapter.Fill(_dogovors);
                tbMessages.Text = com.Parameters["@count"].Value.ToString();

            }
            _dogovors.Columns.Add("Button", typeof(Image));
            _dogovors.Columns.Add("Button1", typeof(Image));
            foreach (DataRow row in _dogovors.Rows)
            {
                row["Button"] = Properties.Resources.mailIconMin;
                row["Button1"] = Properties.Resources.delete; //GetButton(row.Field<string>("DG_CODE"));
            }
            UpdateDataGrid();
        }

        private void timePulse_Tick(object sender, EventArgs e)
        {

            if (btnNewMessages.BackColor == Color.Green)
            {
                btnNewMessages.BackColor = SystemColors.Control;

            }
            else
            {
                btnNewMessages.BackColor = Color.Green;
            }
        }

        private void tms15Min_Click(object sender, EventArgs e)
        {
            timePulse.Enabled = false;
            timeRefreshMessage.Enabled = false;
            timePause.Interval = 15*60*1000;
            timePause.Enabled = true;

        }

        private void tms20Min_Click(object sender, EventArgs e)
        {
            timePulse.Enabled = false;
            timeRefreshMessage.Enabled = false;
            timePause.Interval = 20*60*1000;
            timePause.Enabled = true;
        }

        private void timePause_Tick(object sender, EventArgs e)
        {
            timeRefreshMessage_Tick(null,null);
            timeRefreshMessage.Enabled = true;
            timePause.Enabled = false;
        }

        private void tsmDay_Click(object sender, EventArgs e)
        {
            timePulse.Enabled = false;
            timeRefreshMessage.Enabled = false;
        }

        private void tableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDogovor_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = dgvDogovor.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                dgvDogovor.Rows[index].HeaderCell.Value = indexStr; 
            
        }

        private void frmSerchDogovors_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null,null);
            }
        }

        private void tbMessages_TextChanged(object sender, EventArgs e)
        {
            int i = int.Parse(((TextBox) sender).Text);
            if (i == 0)
            {
                timePulse.Enabled = false;
                btnNewMessages.BackColor=SystemColors.Control;
            }
            else
            {
                timePulse.Enabled = true;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SetListBox(lbStatus, 0);
            SetListBox(lbBronir, 0);
            SetListBox(lbRealizator, 0);
            SetListBox(lbDogovor, 0);
            SetListBox(lbDocument, 0);
            cbDateCreate.Checked = false;
            cbTurDate.Checked = false;
            tbdgcode.Text = "";
            ClearSelectClasses();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            lStatus.Text = "Все брони";
            SetListBox(lbStatus, 0);
            SetListBox(lbBronir, 0);
            SetListBox(lbRealizator, 0);
            SetListBox(lbDogovor, 0);
            SetListBox(lbDocument, 0);
            cbDateCreate.Checked = false;
            dtpTurDateS.Value = DateTime.Now.Date;
            dtpTurDatePo.Value = WorkWithData.GetMaxDateDogovor().Date;
            cbTurDate.Checked = true;
            tbdgcode.Text = "";
            ClearSelectClasses();
            SetFilter();
        }

        private void frmSerchDogovors_FormClosed(object sender, FormClosedEventArgs e)
        {
            WebCore.Shutdown();
            System.Threading.Thread.Sleep(10);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDogovor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgvDogovor.Columns[e.ColumnIndex].Name != "Button1" &&
                    dgvDogovor.Columns[e.ColumnIndex].Name != "Button" && dgvDogovor.Columns[e.ColumnIndex].Name != "Button3" && dgvDogovor.Columns[e.ColumnIndex].Name != "optionDateEnd")
                {
                    string UsingDGCode = dgvDogovor.Rows[e.RowIndex].Cells["dg_code"].Value.ToString();
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
            }
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            lanta.SQLConfig.Config_XML conf = new Config_XML();
            string url = conf.Get_Value("appSettings", "instalationURL"); 
            frmPopupWindow frm = new frmPopupWindow(new Uri(url));
            frm.TopMost = true;
            frm.ShowDialog();
        }

        private void instalitionTime_Tick(object sender, EventArgs e)
        {
            using (SqlCommand com = new SqlCommand("mk_is_ustanovky", WorkWithData.Connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@us_key", WorkWithData.GetUserID());
                bool flag = (bool)com.ExecuteScalar();
                if (!flag)
                {
                    btnDay_Click(null, null);
                }
            }
            WebClient client = new WebClient();
           // byte[] data = client.DownloadData("http://bit.mcruises.ru/installations/titleslist.txt ");
            try
            {
                
                
                lanta.SQLConfig.Config_XML conf = new Config_XML();
                string url = conf.Get_Value("appSettings", "instalationTitleURL");
                byte[] data =
                    client.DownloadData(url);
               
                
                Stream stream = new MemoryStream(data);
                StreamReader reader = new StreamReader(stream);
                string str = reader.ReadToEnd();
                label16.Text = str;
            }
            catch (Exception ex)
            {
                
            }





        }

        private void label16_Click(object sender, EventArgs e)
        {
            string url = "http://bit.mcruises.ru/installations/";
            frmPopupWindow frm = new frmPopupWindow(new Uri(url));
            frm.ShowDialog();
            
        }

        private void frmSerchDogovors_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (int.Parse(tbProblemBron.Text) > 0)
            //{
            //    string text = string.Format("У вас {0} проблемных брони Вы увернеы что хотите закрыть рабочее место?",tbProblemBron.Text);
                
            //    if (MessageBox.Show(text, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    {
            //        e.Cancel = true;
            //    }
            //}
            try
            {
                DialogResult rez = new frmSuperProblem().ShowDialog();
                if (rez != DialogResult.Yes)
                {
                    e.Cancel = true;
                }

            }
            catch (Exception)
            {
                
               
            }


        }

        private void btnAnnulate_Click(object sender, EventArgs e)
        {
            new frmAnnulateJornal().ShowDialog();
        }

        private void timePaydDogovor_Tick(object sender, EventArgs e)
        {
            if (!_access.isBronir)
            {
                (sender as Timer).Enabled = false;
                return;
            }
            DataTable payddogovors = WorkWithData.GetPaydDogovors();
            if (payddogovors.Rows.Count > 0)
            {
                new frmNewChanges(0, payddogovors){TopMost = true}.ShowDialog();
            }
        }

        private void cbStatus_ChangeUICues(object sender, UICuesEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            cb.DropDownHeight = cb.Items.Count * 25;
        }

        private void btnTuristsByItinerary_Click(object sender, EventArgs e)
        {
            new frmTouristsByItinerary().ShowDialog();
        }

        private void dayTasksTime_Tick(object sender, EventArgs e)
        {
            daysTasks.RefreshData();
        }

        private void btnRingFromSite_Click(object sender, EventArgs e)
        {
            new frmJournal(WorkWithData.Connection).ShowDialog();
        }

        private void dgvDogovor_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn =
                dgvDogovor.Columns[e.ColumnIndex];
            if (newColumn == dgvDogovor.Columns["DG_TURDATEEXT"])
            {
                newColumn = dgvDogovor.Columns["DG_TURDATE"];
            }

                DataGridViewColumn oldColumn = dgvDogovor.SortedColumn;
                ListSortDirection direction;
                if (newColumn.SortMode ==DataGridViewColumnSortMode.NotSortable) return;
                
                if (oldColumn != null)
                {
                    
                    if (oldColumn == newColumn &&
                        dgvDogovor.SortOrder == SortOrder.Ascending)
                    {
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        
                        direction = ListSortDirection.Ascending;
                        oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                }

        
            if (newColumn == null)
            {
                MessageBox.Show("Select a single column and try again.",
                                "Error: Invalid Selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                dgvDogovor.Sort(newColumn, direction);

                if ( newColumn== dgvDogovor.Columns["DG_TURDATE"])
                {
                    dgvDogovor.Columns["DG_TURDATEEXT"].HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending
                        ? SortOrder.Ascending
                        : SortOrder.Descending;
                }
                newColumn.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending
                        ? SortOrder.Ascending
                        : SortOrder.Descending;
               
            }
        }


    }
}
