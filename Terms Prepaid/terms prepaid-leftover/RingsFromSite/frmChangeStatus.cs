using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RingsFromSite
{
    public partial class frmChangeStatus : Form
    {
        private int _idRing;
        private WorkWithData wwd;
        private SqlConnection _connection;
        
        public frmChangeStatus(int idRing,SqlConnection con )
        {
            InitializeComponent();
            _idRing = idRing;
            _connection = con;
            wwd = new WorkWithData(_connection);
            GetDate();
        }

 
        void GetDate()
        {
            cbStatuses.DataSource = wwd.GetStatuses();
            cbStatuses.ValueMember = "id";
            cbStatuses.DisplayMember = "name_ru";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            wwd.ChangeStatus(_idRing,(int)cbStatuses.SelectedValue);
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
