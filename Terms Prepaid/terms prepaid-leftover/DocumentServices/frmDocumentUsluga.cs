using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DocumentServices
{
    public partial class frmDocumentUsluga : Form
    {
        private string _fileaname, _dgcode;
        private MasterDataContext _dc;
        private tbl_DogovorList[] _services;
        private mk_DocumentService[] _documetnsServices;

        public frmDocumentUsluga(string filename,string dgcode,MasterDataContext dc)
        {
            InitializeComponent();
            _dc = dc;
            _fileaname = filename;
            _dgcode = dgcode;
            GetDate();
        }
        void GetDate()
        {
            _services = _dc.tbl_DogovorLists.Where(x => x.DL_DGCOD == _dgcode && x.DL_SVKEY != 1059 && x.DL_SVKEY != 1238 && x.DL_SVKEY != 1520 && (x.DL_ATTRIBUTE & 64) == 0).ToArray();
            List<tbl_DogovorList> serviseList = new List<tbl_DogovorList>(_services);
            _documetnsServices = _dc.mk_DocumentServices.Where(x => x.DS_FileName == _fileaname).ToArray();
            serviseList.Add(new tbl_DogovorList { DL_NAME = "Применить ко всей путевке", DL_KEY = -1 });
            
            _services = serviseList.ToArray();
            clbServices.DataSource = _services;
            for (int i = 0; i < clbServices.Items.Count; i++)
            {
                tbl_DogovorList item = clbServices.Items[i] as tbl_DogovorList;
                if (_documetnsServices.Where(x => x.DS_DLKEY == item.DL_KEY).Count() > 0)
                {
                    clbServices.SetItemChecked(i,true);
                }
            }
         

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clbServices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            tbl_DogovorList dlItem = clbServices.Items[e.Index] as tbl_DogovorList;
            if (dlItem.DL_KEY == -1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    for (int i = 0; i < clbServices.Items.Count; i++)
                    {
                        tbl_DogovorList tbldlItem = clbServices.Items[i] as tbl_DogovorList;
                        if (tbldlItem.DL_KEY != -1)
                        {
                            clbServices.SetItemChecked(i, false);
                        }

                    }
                    //  clbUslugi.SetItemChecked(e.Index,true);

                }
            }
            else
            {
                for (int i = 0; i < clbServices.Items.Count; i++)
                {
                    tbl_DogovorList tbldlItem = clbServices.Items[i] as tbl_DogovorList;
                    if (tbldlItem.DL_KEY == -1)
                    {
                        clbServices.SetItemChecked(i, false);
                    }

                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (clbServices.CheckedItems.Count > 0)
            {
                tbl_DogovorList chekItem = clbServices.CheckedItems[0] as tbl_DogovorList;
                if (chekItem.DL_KEY == -1)
                {
                    _dc.mk_updateDocumetServices(_dgcode, null, _fileaname);
                }
                else
                {
                    string dlkeys = "";
                    foreach (var checkedItem in clbServices.CheckedItems)
                    {
                        tbl_DogovorList item = checkedItem as tbl_DogovorList;
                        dlkeys += (dlkeys != "" ? "," : "") + item.DL_KEY.ToString();
                    }
                    _dc.mk_updateDocumetServices(_dgcode, dlkeys, _fileaname);
                }
                _dc.SubmitChanges();
                Close();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать услуги");
            }

        }
    }
}
