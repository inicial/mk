using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using DocomentServices;
using lanta.SQLConfig;
using Microsoft.VisualBasic;

namespace DocumentServices
{
    public partial class frmFileToFtp : Form
    {
        private SqlConnection _connection;
        private MasterDataContext sqlDataContex;
        private int _dlkey;
        private string _dg_code;
        private tbl_DogovorList[] _services;
        private Lanta_DictDocSpr[] _dictDoc;
        private tbl_Turist[] _turists;
        private string userName, password;
        [HostProtectionAttribute(SecurityAction.LinkDemand, Resources = HostProtectionResource.UI)]

        void GetDate()
        {
            
            _services = sqlDataContex.tbl_DogovorLists.Where(x => x.DL_DGCOD == _dg_code && x.DL_SVKEY != 1059 && x.DL_SVKEY != 1238 && x.DL_SVKEY != 1520 && (x.DL_ATTRIBUTE & 64) == 0).ToArray();
            List<tbl_DogovorList> serviseList = new List<tbl_DogovorList>(_services);
            serviseList.Add(new tbl_DogovorList{DL_NAME = "Применить ко всей путевке",DL_KEY = -1});
            _services = serviseList.ToArray();
            _dictDoc = sqlDataContex.Lanta_DictDocSprs.Where(x => x.ddgForPersonalArea == 1&&(x.ddgCN_Key==1111111||x.ddgCN_Key==-1)&&x.ddgIDDepartament==1).ToArray();
            _turists = sqlDataContex.tbl_Turists.Where(x => x.TU_DGCOD == _dg_code).ToArray();
            clbUslugi.DataSource = _services;
            lbType.DataSource = _dictDoc;
            clbTurists.DataSource = _turists;
            for (int i = 0; i < clbTurists.Items.Count; i++)
            {
                clbTurists.SetItemChecked(i,true);
            }
            //if (_dlkey != 0)
            //{
            //    lbUslugi.SelectedItem = _services.First(x => x.dl_KEY == _dlkey);
            //}
            string[] strs = _connection.ConnectionString.Split(';');
            foreach (string s in strs)
            {
                if (s.IndexOf("User ID=") >= 0)
                {
                    userName = s.Substring("User ID=".Length);
                }
                else if (s.IndexOf("Password=") >= 0)
                {
                    password = s.Substring("Password=".Length);
                }
            }
        }

        //public frmFileToFtp(SqlConnection connection,int dl_key)
        //{
        //    InitializeComponent();
        //    _connection = connection;
        //    sqlDataContex = new MasterDataContext(_connection);
        //    _dlkey = dl_key;
        //    _dg_code = sqlDataContex.tbl_DogovorLists.FirstOrDefault(x => x.DL_KEY == _dlkey).DL_DGCOD;
        //    GetDate();
        //}

        public frmFileToFtp(SqlConnection connection, string dg_code)
        {
            InitializeComponent();
            _dg_code = dg_code;
            _connection = connection;
            sqlDataContex = new MasterDataContext(_connection);
            _dlkey = 0;
            GetDate();
            
         //   List<string> uslugList = new List<string>();
          //foreach (Tours tourse in _services)
          //      uslugList.Add(tourse.DL_NAME);
          //  }
            

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!(clbUslugi.CheckedItems.Count > 0))
            {
                MessageBox.Show("Выберете услугу к кторой будет привязан файл!");
                return;
            }
            if (!(clbTurists.CheckedItems.Count > 0))
            {
                MessageBox.Show("Выберете туристов к кторой будет привязан файл!");
                return;
            }
            FileDialog fld = new OpenFileDialog();
            if (fld.ShowDialog() == DialogResult.OK)
            {
                string newFilename = "MK_" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                lanta.SQLConfig.Config_XML xmlConfig = new Config_XML();
                WorkWithFTP ftp = new WorkWithFTP(xmlConfig.Get_Value("appSettings","ftp"));
                string str = Microsoft.VisualBasic.Interaction.InputBox("Введите комментарий!", "Комментарий");
                string errorMsg = "";
                WorkWithFTP.FTP_ERROR error = WorkWithFTP.FTP_ERROR.ERROR_NO;
                error = ftp.GetFilesOnFTPAndCreateNewDir(_dg_code,out errorMsg);
                if (error != WorkWithFTP.FTP_ERROR.ERROR_NO)
                {
                    MessageBox.Show(errorMsg);
                    return;
                }
                
                
                
                error=ftp.Upload(_dg_code, fld.FileName, newFilename, out errorMsg);
                FileInfo fileInf = new FileInfo(fld.FileName);
                
                if (error==WorkWithFTP.FTP_ERROR.ERROR_NO)
                {
                     
                    foreach (var  item in clbTurists.CheckedItems)
                    {

                       
                        Lanta_DictDocSpr dict = lbType.SelectedItem as Lanta_DictDocSpr;
                        tbl_Turist turist = item as tbl_Turist;
                        var pa = sqlDataContex.Lanta_PersonalAreas.Where(
                            x => x.pa_DG_Code == _dg_code && x.pa_TU_Key == turist.TU_KEY && x.pa_ddgID == dict.ddgID);

                        int number = 0;
                        foreach (Lanta_PersonalArea lantaPersonalArea in pa)
                        {
                            if (lantaPersonalArea.pa_Number > number)
                            {
                                number = lantaPersonalArea.pa_Number;
                            }
                        }
                        number++;

                        Lanta_PersonalArea lpaItem = new Lanta_PersonalArea
                            {
                               
                                Lanta_DictDocSpr =dict,
                                pa_ddgID = dict.ddgID,
                                pa_Description = str,
                                pa_DG_Code = _dg_code,
                                pa_FileName = newFilename + fileInf.Extension,
                                pa_Number = number,
                                pa_TU_Key = turist.TU_KEY,
                                pa_UserUpdate = sqlDataContex.UserLists.First(x=>x.US_USERID==userName).US_FullName,
                                pa_WasPrint = 0,
                                tbl_Dogovor = sqlDataContex.tbl_Dogovors.First(x=>x.DG_CODE==_dg_code)
                                
                            };
                        sqlDataContex.Lanta_PersonalAreas.InsertOnSubmit(lpaItem);
                        
                    }
                   // sqlDataContex.

                    foreach (var checkedItem in clbUslugi.CheckedItems)
                    {
                        tbl_DogovorList dlitem = checkedItem as tbl_DogovorList;
                        if (dlitem.DL_KEY != -1)
                        {
                            mk_DocumentService document = new mk_DocumentService
                                {
                                    DS_DGCODE = _dg_code,
                                    DS_DLKEY = dlitem.DL_KEY,
                                    DS_FileName = newFilename + fileInf.Extension
                                };
                            sqlDataContex.mk_DocumentServices.InsertOnSubmit(document);
                        }
                    }

                    sqlDataContex.SubmitChanges();
                }
                else
                {
                    MessageBox.Show(errorMsg);
                }
            }
        }

        private void lbUslugi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Tours tour = lbUslugi.SelectedItem as Tours;
            //_dlkey = (int)tour.dl_KEY;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clbUslugi_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            tbl_DogovorList dlItem = clbUslugi.Items[e.Index] as tbl_DogovorList;
            if (dlItem.DL_KEY == -1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    for (int i = 0; i < clbUslugi.Items.Count; i++)
                    {
                        tbl_DogovorList tbldlItem = clbUslugi.Items[i] as tbl_DogovorList;
                        if (tbldlItem.DL_KEY != -1)
                        {
                            clbUslugi.SetItemChecked(i,false);
                        }
                       
                    }
                  //  clbUslugi.SetItemChecked(e.Index,true);
    
                }
            }
            else
            {
                for (int i = 0; i < clbUslugi.Items.Count ; i++)
                {
                    tbl_DogovorList tbldlItem = clbUslugi.Items[i] as tbl_DogovorList;
                    if (tbldlItem.DL_KEY == -1)
                    {
                        clbUslugi.SetItemChecked(i, false);
                    }

                }
            }

        }

        

    }
}

