using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.Forms
{
    public partial class frmDopDogovorChanges : Form
    {
        private string _dgcode;
        public frmDopDogovorChanges(string dgcode)
        {
            InitializeComponent();
            _dgcode = dgcode;
            GetDate();
            this.Refresh();
        }

        DataGridView GetDGV(DataTable dt)
        {
            DataGridView dgv = new DataGridView
            {
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                DataSource = dt,
                Dock = DockStyle.Fill
            };

            return dgv;
        }

        private void GetDate()
        {
            DataSet setChanges = WorkWithData.GetSetChanges(_dgcode);
            int i = 0;
            TabControl tc = new TabControl()
                {
                    Dock = DockStyle.Top,
                    Size = new Size(this.Width-50,this.Height),
                    Location = new Point(0,0),
                   
                    Name = "tc"
                };
           
            this.Controls.Add(tc);
            this.Height += 70;
            Button btnChek = new Button()
                {
                    Text = "Дополнительное соглашение не нужно!",
                    Size = new Size(this.Width -20,27),
                    Location = new Point(0,tc.Height+1)
                };
            btnChek.Click += btnChek_Click;
            this.Controls.Add(btnChek);
            if (setChanges.Tables.IndexOf("GeneralTable") >= 0)
            {
                tc.TabPages.Add(new TabPage()
                    {
                        Name = "general"
                    });
                tc.TabPages["general"].Controls.Add(GetDGV(setChanges.Tables["GeneralTable"]));
            }
            foreach (DataTable table in setChanges.Tables)
            {
                if (table.TableName != "GeneralTable")
                {
                    string tabName = "tab" + i.ToString();
                    tc.TabPages.Add(new TabPage(tabName)
                        {
                            Name = tabName
                        });
                    tc.TabPages[tabName].Text = table.TableName;
                    tc.TabPages[tabName].Controls.Add(GetDGV(table));
                    i++;
                }
            }
        }

        void btnChek_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы  уверены что хотиите погасить дополнительное соглашение?","",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                WorkWithData.ChekDogovorState(null,_dgcode);
                Close();
            }
            
            //WorkWithData.InsertHistory(_dgcode,"","");
        }

        private void frmDopDogovorChanges_Load(object sender, EventArgs e)
        {

        }

        private void frmDopDogovorChanges_Shown(object sender, EventArgs e)
        {
            foreach (TabPage page in (this.Controls["tc"] as TabControl).TabPages)
            {
                foreach (Control control in page.Controls)
                {
                    DataGridView dgv = control as DataGridView;
                    if (dgv != null)
                    {
                        foreach (DataGridViewColumn column in dgv.Columns)
                        {
                            switch (column.Name.ToUpper())
                            {
                                case "CHANGEVALUE":
                                    column.HeaderText = "";
                                    column.DisplayIndex = 0;
                                    break;
                                case "OLDVALUE":
                                    column.HeaderText = "Старое значение";
                                    column.DisplayIndex = 1;
                                    break;
                                case "NEWVALUE":
                                    column.HeaderText = "Новое значение";
                                    column.DisplayIndex = dgv.Columns.Count-1;
                                    break;
                                default:
                                    column.Visible = false;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
