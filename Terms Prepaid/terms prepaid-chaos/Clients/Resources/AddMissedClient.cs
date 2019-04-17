using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lanta.DirectClientDogovor
{
    public partial class AddMissedClient : Form
    {
        public AddMissedClient(DataRow dr)
        {
            InitializeComponent();
            textBox1_CL_KEY.Text = Convert.ToString(dr["CL_KEY"]);
            textBox1_CL_NAMERUS.Text = Convert.ToString(dr["CL_NAMERUS"]);
            textBox1_CL_FNAMERUS.Text = Convert.ToString(dr["CL_FNAMERUS"]);
            textBox1_CL_SNAMERUS.Text = Convert.ToString(dr["CL_SNAMERUS"]);
            textBox1_CL_BIRTHDAY.Text = Convert.ToString(dr["CL_BIRTHDAY"]);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
        }
    }
}
