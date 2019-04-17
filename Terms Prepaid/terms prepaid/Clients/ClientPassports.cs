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
    public partial class ClientPassports : Form
    {
        long CL_KEY; 
        SqlDataAdapter adapter;
        DataTable ClientPaspInfo = new DataTable("ClientPaspInfo");
        DataTable Passports = new DataTable("Passports");
        public DataRow drPaspInfo = null;
        public ClientPassports(long CL_KEY,SqlDataAdapter adapter)
        {
            InitializeComponent();
            this.CL_KEY = CL_KEY;
            this.adapter = adapter;
        }

        private void ReloadPassports()
        {
            Passports.Clear();
            adapter.SelectCommand.CommandText = @"SELECT     CP_ID, CP_CLКеу, CP_CLPASPORTSER, CP_CLPASPORTNUM, CP_CLNAMELAT, CP_CLFNAMELAT, CP_CLSNAMELAT, CP_CLPASPORTDATE, 
                      CP_CLPASPORTDATEEND, CP_CLPASPORTBYWHOM, CP_COMMENT
            FROM         Lanta_ClientPassport
            WHERE     CP_CLКеу ="+ CL_KEY;
            adapter.Fill(Passports);

        }
        private void ClientPassports_Load(object sender, EventArgs e)
        {
            adapter.SelectCommand.CommandText = @"SELECT CL_NAMELAT, CL_FNAMELAT, CL_SNAMELAT, CL_PASPORTSER, CL_PASPORTNUM, CL_PASPORTDATE, CL_PASPORTDATEEND, 
                      CL_PASPORTBYWHOM
                FROM Clients WHERE CL_KEY = " + CL_KEY;
            adapter.Fill(ClientPaspInfo);

            ReloadPassports();

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();

            //Если паспорта клиента ещё нет в списке паспортов - добавлем его как отдельную сущность
            if (ClientPaspInfo.Rows.Count > 0)
            {
                DataRow dr = ClientPaspInfo.Rows[0];
                drPaspInfo = dr;
                label_Name.Text = Convert.ToString(dr["CL_NAMELAT"]) + " " + Convert.ToString(dr["CL_FNAMELAT"]) + " " + Convert.ToString(dr["CL_SNAMELAT"]);
                string CL_PASPORTSER = Convert.ToString(dr["CL_PASPORTSER"]);
                string CL_PASPORTNUM = Convert.ToString(dr["CL_PASPORTNUM"]);
                DataRow[] hasCurrent = Passports.Select("CP_CLPASPORTSER=" + CL_PASPORTSER + " AND CP_CLPASPORTNUM=" + CL_PASPORTNUM);
                if (hasCurrent.Length == 0)
                    AddNew(dr);
            }
            dataGridView_psp.DataSource = Passports;
        }

        private void AddNew(DataRow dr)
        {
            DataRow newPassport =  Passports.NewRow();
            newPassport["CP_CLКеу"] = CL_KEY;
            newPassport["CP_CLPASPORTSER"] = dr["CL_PASPORTSER"];
            newPassport["CP_CLPASPORTNUM"] = dr["CL_PASPORTNUM"];
            newPassport["CP_CLNAMELAT"] = dr["CL_NAMELAT"];
            newPassport["CP_CLFNAMELAT"] = dr["CL_FNAMELAT"];
            newPassport["CP_CLSNAMELAT"] = dr["CL_SNAMELAT"];
            newPassport["CP_CLPASPORTDATE"] = dr["CL_PASPORTDATE"];
            newPassport["CP_CLPASPORTDATEEND"] = dr["CL_PASPORTDATEEND"];
            newPassport["CP_CLPASPORTBYWHOM"] = dr["CL_PASPORTBYWHOM"];
            
            Passports.Rows.Add(newPassport);

            adapter.Update(Passports);
        }

        private void button_Select_Click(object sender, EventArgs e)
        {
            if (dataGridView_psp.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_psp.SelectedRows[0];
                drPaspInfo["CL_PASPORTSER"] = row.Cells["CP_CLPASPORTSER"].Value;
                drPaspInfo["CL_PASPORTNUM"] = row.Cells["CP_CLPASPORTNUM"].Value;
                drPaspInfo["CL_NAMELAT"] = row.Cells["CP_CLNAMELAT"].Value;
                drPaspInfo["CL_FNAMELAT"] = row.Cells["CP_CLFNAMELAT"].Value;               
                drPaspInfo["CL_SNAMELAT"] = row.Cells["CP_CLSNAMELAT"].Value;                
                drPaspInfo["CL_PASPORTDATE"] = row.Cells["CP_CLPASPORTDATE"].Value; 
                drPaspInfo["CL_PASPORTDATEEND"] = row.Cells["CP_CLPASPORTDATEEND"].Value;                
                drPaspInfo["CL_PASPORTBYWHOM"] = row.Cells["CP_CLPASPORTBYWHOM"].Value; 

                this.DialogResult = DialogResult.OK;
            }
        }

        private void button_Del_Click(object sender, EventArgs e)
        {
            if (dataGridView_psp.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_psp.SelectedRows[0];
                /*
                
                int CP_ID = Convert.ToInt32(row.Cells["CP_ID"].Value);
                DataRow[] toDelete = Passports.Select("CP_ID=" + CP_ID);
                if (toDelete.Length > 0)
                    toDelete[0].Delete();*/
                DataRow toDelete = ((DataRowView)row.DataBoundItem).Row;
                toDelete.Delete();
                adapter.Update(Passports);

            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {

            DataRow toAdd = Passports.NewRow();
            toAdd["CP_CLКеу"] = CL_KEY;
            ClientPassportEdit cpe = new ClientPassportEdit(toAdd);
            if (cpe.ShowDialog() == DialogResult.OK)
            {
                Passports.Rows.Add(toAdd);
                adapter.Update(Passports);
                ReloadPassports();
            }
        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            Edit();
        }
        private void Edit()
        {
                    if (dataGridView_psp.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView_psp.SelectedRows[0];
                DataRow Info = ((DataRowView)row.DataBoundItem).Row;
                ClientPassportEdit cpe = new ClientPassportEdit(Info);
                if (cpe.ShowDialog() == DialogResult.OK)
                     adapter.Update(Passports);
            }
        }

        private void dataGridView_psp_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
    }
}
