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

namespace HistoryServices
{
    public partial class frmHistory : Form
    {
        private string _dgcod;
        private SqlConnection _connection;
        DataTable _history = new DataTable();
        private const string selectHistoryDetail = @"SELECT [HD_ID]
      ,[HD_HIID]
      ,[HD_Alias]
      ,[HD_Text]
      ,[HD_ValueOld]
      ,[HD_ValueNew]
       FROM [dbo].[HistoryDetail]
       where [HD_HIID] = @hiid";
        private const string selectHistory = @"SELECT [HI_DATE]
      ,[HI_WHO]
      ,[HI_TEXT]
      ,[HI_MOD]
      ,[HI_REMARK]
      ,[HI_DLKEY]
      ,[HI_SVKEY]
      ,[HI_CODE]
      ,[HI_CODE1]
      ,[HI_CODE2]
      ,[HI_DAY]
      ,[HI_NDAYS]
      ,[HI_NMEN]
      ,[HI_PRKEY]
      ,[HI_ID]
      ,[HI_Type]
      ,[HI_TypeCode]
      ,[HI_MessEnabled]
      ,[HI_Invisible]
      ,[HI_DGKEY]
      ,[HI_TextLat]
      ,[HI_DocumentName]
      ,[HI_OAId]
  FROM [dbo].[History]
  where HI_DGCOD =@dgcode";
        private const string usedmodes = "'WWW','MTC','MTM'";
        private const string DogovorCondition = " and HI_Type='DOGOVOR'";
        private const string DogovorListCondition = " and HI_Type='DOGOVORLIST'";
        private const string usedtypes = "'DOGOVOR'";
        public frmHistory(string dgcode,SqlConnection connection)
        {
            InitializeComponent();
            _connection = connection;
            _dgcod = dgcode;
            GetDate(DogovorListCondition);
        }
        void GetDate(string condition)
        {
            _history.Clear();

            using (SqlDataAdapter adapter = new SqlDataAdapter(selectHistory+condition,_connection) )
            {
                adapter.SelectCommand.Parameters.AddWithValue("@dgcode", _dgcod);
                adapter.Fill(_history);
            }
            dgvHistory.DataSource = _history;
            UpdateDataGridHistory();
            dgvHistory_CellContentClick(dgvHistory,new DataGridViewCellEventArgs(0,0));
            //  dgvHistory.DataSource = _changeDogovor;
            // dgvHistoryDetails.DataSource = _other;

        }
        void UpdateDataGridHistory()
        {
            foreach (DataGridViewColumn column in dgvHistory.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "hi_date":
                        {
                            column.HeaderText = "Дата";
                            column.DisplayIndex = 0;
                        }
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }
        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectHistoryDetail,_connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@hiid",
                                                                  dgvHistory.Rows[e.RowIndex].Cells["HI_ID"].Value);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvHistoryDetails.DataSource = dt;
                }
            }
        }

        private void dgvHistory_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           Close();
        }
    }
}
