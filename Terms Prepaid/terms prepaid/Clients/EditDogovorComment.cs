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
    public partial class EditDogovorComment : Form
    {
        public string ret_comment = "";
        public EditDogovorComment()
        {
            InitializeComponent();
        }
        public EditDogovorComment(string DG_CODE,string comment)
        {
            InitializeComponent();
            label_DG_CODE.Text = DG_CODE;
            textBox_comment.Text = comment;
        }

        private void EditDogovorComment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( textBox_comment.Text.Length<=200)
                ret_comment = textBox_comment.Text;
            else
                ret_comment = textBox_comment.Text.Substring(0,200);
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
