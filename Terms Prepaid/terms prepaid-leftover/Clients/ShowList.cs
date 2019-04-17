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
    public partial class ShowList : Form
    {
        public ShowList(string list)
        {
            InitializeComponent();
            textBox1.Text = list;
        }
    }
}
