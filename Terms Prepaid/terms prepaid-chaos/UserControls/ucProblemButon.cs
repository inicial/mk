using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace terms_prepaid.UserControls
{
    public partial class ucProblemButon : UserControl
    {
        private Action<DataTable> _callback;

        private DataTable _problemTable;

        public ucProblemButon(DataTable table, string buttonName, Action<DataTable> callback = null)
        {
            InitializeComponent();
            tb.TextChanged += tb_TextChanged;
            _problemTable = table;
            btn.Text = buttonName;
            _callback = callback;
            GetDate();
        }

        void GetDate()
        {
            tb.Text = _problemTable.Rows.Count.ToString();
        }

        void tb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(tb.Text) > 0)
                {
                    tb.ForeColor = Color.Red;
                }
                else
                {
                    tb.ForeColor = SystemColors.ControlText;
                }

            }
            catch (Exception)
            {

                tb.ForeColor = Color.Red;
            }
            
        }

        public void resetTable(DataTable newTable)
        {
            _problemTable = newTable;
            GetDate();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (_callback != null)
            {
                _callback.Invoke(_problemTable);
                return;
            }
            
            Control frm = Parent;
            while (frm.GetType()!=typeof(frmProblemBron)||frm==null)
            {
                frm = frm.Parent;
            }
            frmProblemBron frm1 = frm as frmProblemBron;
            if (frm1 != null)
            {
                frm1.SetTable(_problemTable);
                frm1.Close();
            }
        }
    }
}
