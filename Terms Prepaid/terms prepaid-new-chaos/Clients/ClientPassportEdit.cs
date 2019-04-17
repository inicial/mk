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
    public partial class ClientPassportEdit : Form
    {
        public DataRow drPaspInfo;
        public ClientPassportEdit(DataRow drPaspInfo)
        {
            InitializeComponent();
            this.drPaspInfo = drPaspInfo;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            drPaspInfo["CP_CLPASPORTSER"] = Validate(textBox_CL_PASPORTSER.Text, 5);
            drPaspInfo["CP_CLPASPORTNUM"] = Validate(textBox_CL_PASPORTNUM.Text, 13);
            drPaspInfo["CP_CLNAMELAT"] = Validate(textBox_CL_NAMELAT.Text, 35);
            drPaspInfo["CP_CLFNAMELAT"] = Validate(textBox_CL_FNAMELAT.Text, 20);
            drPaspInfo["CP_CLSNAMELAT"] = Validate(textBox_CL_SNAMELAT.Text, 15);
            drPaspInfo["CP_CLPASPORTDATE"] = dateTimePicker_CL_PASPORTDATE.Value;
            drPaspInfo["CP_CLPASPORTDATEEND"] = dateTimePicker_CL_PASPORTDATEEND.Value;
            drPaspInfo["CP_CLPASPORTBYWHOM"] = Validate(textBox_CL_PASPORTBYWHOM.Text, 15);
            drPaspInfo["CP_COMMENT"] = Validate(textBox_CP_COMMENT.Text, 300);
            this.DialogResult = DialogResult.OK;
        }
        private string  Validate(string Text, int size)
        {
            if (Text.Length > size)
                return Text.Substring(0, size);
            else
                return Text;
        }
        private void ClientPassportEdit_Load(object sender, EventArgs e)
        {
            textBox_CL_PASPORTSER.Text = Convert.ToString(drPaspInfo["CP_CLPASPORTSER"]);
            textBox_CL_PASPORTNUM.Text = Convert.ToString(drPaspInfo["CP_CLPASPORTNUM"]);
            textBox_CL_NAMELAT.Text = Convert.ToString(drPaspInfo["CP_CLNAMELAT"]);
            textBox_CL_FNAMELAT.Text = Convert.ToString(drPaspInfo["CP_CLFNAMELAT"]);
            textBox_CL_SNAMELAT.Text = Convert.ToString(drPaspInfo["CP_CLSNAMELAT"]);
            if (drPaspInfo["CP_CLPASPORTDATE"]!=System.DBNull.Value)
                dateTimePicker_CL_PASPORTDATE.Value = Convert.ToDateTime(drPaspInfo["CP_CLPASPORTDATE"]);
            if (drPaspInfo["CP_CLPASPORTDATEEND"] != System.DBNull.Value)
                dateTimePicker_CL_PASPORTDATEEND.Value = Convert.ToDateTime(drPaspInfo["CP_CLPASPORTDATEEND"]);
            textBox_CL_PASPORTBYWHOM.Text = Convert.ToString(drPaspInfo["CP_CLPASPORTBYWHOM"]);
            textBox_CP_COMMENT.Text = Convert.ToString(drPaspInfo["CP_COMMENT"]);
        }

    }
}
