using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace lanta.Clients
{
   
    public partial class ReportSendList : Form
    { 
        SqlCommand command;
        public ReportSendList(SqlCommand command)
        {
            InitializeComponent();
            this.command = command;
        }

        private void ReportSendList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'lantaDataSet.Clients' table. You can move, or remove it, as needed.
            //this.ClientsTableAdapter.Fill(this.lantaDataSet.Clients,command);

            this.reportViewer1.RefreshReport();
        }
    }
}
