using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lanta.Clients
{
    public partial class ExceptionForm : Form
    {
        public ExceptionForm(string text)
        {
            InitializeComponent();
            this.textBox1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
