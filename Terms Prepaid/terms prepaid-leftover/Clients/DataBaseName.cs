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
    public partial class DataBaseName : Form
    {
        public DataBaseName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void textBox_db_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
