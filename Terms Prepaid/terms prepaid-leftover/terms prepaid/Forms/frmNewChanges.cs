using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.UserControls;

namespace terms_prepaid.Forms
{
    public partial class frmNewChanges : Form
    {
        string[] namesArray = new string[] { "Пришла оплата - Подтведить/продлить опцию!" };
        private int staticAdd = 50;
        private int _maxY =0;
        private int _type = 0;
        private DataTable _table;
        Size defSize = new Size(800,40);
        public frmNewChanges(int type,DataTable table)
        {
            InitializeComponent();
            if(type>namesArray.Length-1)Close();
            _type = type;
            _table = table;
            foreach (var openForm in Application.OpenForms)
            {
                Form frm = openForm as Form;
                frm.Hide();
            }
            BuidForm();
        }
        void BuidForm()
        {
            lbName.AutoSize = false;
            lbName.Size = defSize;
            lbName.TextAlign= ContentAlignment.MiddleCenter;
            lbName.Text = namesArray[_type];
            this.Width = defSize.Width+20;
            lbName.Location = new Point(0, 5);
            pbWarning.Location = new Point(defSize.Width / 2 - pbWarning.Width / 2, lbName.Location.Y + lbName.Height + 5);
            _maxY = pbWarning.Location.Y + pbWarning.Height + (int)(staticAdd * 0.8);
            foreach (DataRow row in _table.Rows)
            {
                switch (_type)
                {
                    case 0:
                        {
                            string dgcode = row.Field<string>("dgcode");
                            string text = "Пришла" + (row.Field<int>("paytype") == 0 ? " оплата " : " предоплата ") +
                                          row.Field<DateTime>("pay_date").ToString("dd.MM.yy") +
                                          " подтвердите у партнера!";


                            this.Controls.Add(new ucChange(dgcode,text, 2)
                                {
                                    Size = defSize,
                                    Location = new Point(0, _maxY)

                                });

                            _maxY += defSize.Height;
                        }
                        break;
                    default:
                        break;
                } 
            }
            

            btnOk.Location = new Point(defSize.Width / 2 - btnOk.Width / 2, _maxY + defSize.Height + 5);

            this.Size = new Size(this.Width, (btnOk.Location.Y + btnOk.Height) > 500 ? 500 : (btnOk.Location.Y + btnOk.Height) + 5 + staticAdd);
          
            
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmNewChanges_Shown(object sender, EventArgs e)
        {
            try
            {
                this.VerticalScroll.Value = 0;
              //  this.VerticalScroll.
                this.Refresh();
            }
            catch (Exception)
            {


            }
        }

        private void frmNewChanges_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var openForm in Application.OpenForms)
            {
                Form frm = openForm as Form;
                frm.Show();
            }
        }
    }
}
