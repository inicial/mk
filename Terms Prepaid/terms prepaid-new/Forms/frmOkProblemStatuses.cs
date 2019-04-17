using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.Forms
{
    public partial class frmOkProblemStatuses : Form
    {
        private static int _idOk = -1;
        private int _maxY = 0;
        private static int _x=0, _y=0;
        private Size defSize = new Size(300, 30);
        public frmOkProblemStatuses()
        {
            InitializeComponent();
            GetDate();
        }
        void GetDate()
        {
            DataTable okProblemStatusesTable = WorkWithData.GetProblemOkStatuses();
            foreach (DataRow row in okProblemStatusesTable.Rows)
            {
                if (row.Field<int>("PS_ID") != -1)
                {
                    this.Controls.Add(new Button()
                        {
                            Location = new Point(0, _maxY),
                            Size = defSize,
                            Tag = row.Field<int>("PS_ID"),
                            Text = row.Field<string>("PS_NAME"),
                            Name = row.Field<string>("PS_Name").Replace(" ", "")
                            // Click +=frmOkProblemStatuses_Click
                        });
                    _maxY += defSize.Height;
                }
            }
            this.Controls.Add(new Button()
            {
                Location = new Point(0, _maxY),
                Size = defSize,
                Tag = -1,
                Text = "Закрыть",
                Name = "btnClose",
                BackColor = Color.Red
                // Click +=frmOkProblemStatuses_Click
            });
            _maxY += defSize.Height;
            foreach (Control control in Controls)
            {
                Button btn = control as Button;
                if (btn != null)
                {
                    btn.Click += btn_Click;
                }
            }
            this.Size = new Size(defSize.Width,_maxY);
        }

        void btn_Click(object sender, EventArgs e)
        {
            _idOk = (int) (sender as Button).Tag;
            Close();
        }

        static public int GetIdOKProblem(int x, int y)
        {
            frmOkProblemStatuses frm = new frmOkProblemStatuses();
            _x = x;
            _y = y;
            //frm.SetBounds(x,y,frm.Width,frm.Height);
            frm.ShowDialog();
            return _idOk;
        }

        private void frmOkProblemStatuses_Shown(object sender, EventArgs e)
        {
            this.SetBounds(_x,_y,this.Width,this.Height);
        }
    }
}
