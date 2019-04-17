using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;

namespace terms_prepaid.UserControls
{
    public partial class ucAnnulation : UserControl
    {
        private int _idAnnul = 0;
        private string _tempDirectory = Path.GetTempPath();
        AccessClass _access = new AccessClass(WorkWithData.Connection);
        private bool _needPen = true;
        private string _quetion = string.Empty;
        public ucAnnulation(int idAnnul)
        {
            InitializeComponent();
            _idAnnul = idAnnul;
            SetAnnulation();
        }
        private void SetAnnulation()
        {
            DataRow row = WorkWithData.GetAnnulationById(_idAnnul);
            int num = row.Field<int>("AN_KEY");
            btnAnnulationClose.Tag = num;
            btnPetitionView.Tag = num;
            try
            {

            }
            catch (Exception)
            {
                
                
            }
            if (!row.Field<bool>("PT_NEEDBRON"))
            {

                int rowNum = tableLayoutPanel5.GetRow(lAnnulationBronir);
                tableLayoutPanel5.Controls.Remove(lAnnulationBronir);
                tableLayoutPanel5.Controls.Remove(btnAnnulatuionSetBronir);
                tableLayoutPanel5.RowStyles[rowNum].Height = 0;
            }

            if (!row.Field<bool>("PT_NEEDREALIZE"))
            {

                int rowNum = tableLayoutPanel5.GetRow(lAnnulationRealze);
                tableLayoutPanel5.Controls.Remove(lAnnulationRealze);
                tableLayoutPanel5.Controls.Remove(btnAnnulationSetRealize);
                tableLayoutPanel5.RowStyles[rowNum].Height = 0;
            }
            _needPen = row.Field<bool>("PT_NEEDPENALTY");
            _quetion = row.Field<string>("PT_QuetionText");
            btnAnnulationCalc.Text = row.Field<string>("PT_ButtonText");
            if (row["CloseManag"] == DBNull.Value)
            {
                lPetition.Text = "Заявление № " + num.ToString() +" "+row.Field<string>("PT_name") + "  от " +
                                 row.Field<DateTime>("DATE_OF_CREATE").ToString("dd.MM.yy HH:mm") + "!";

                

                if (row["ManagOk"] == DBNull.Value)
                {
                    lAnnulationRealze.Text = "Не взята в работу реализитором";
                    btnAnnulationSetRealize.Tag = num;
                    btnAnnulationSetRealize.Enabled = true;
                }
                else
                {
                    btnAnnulationSetRealize.Enabled = false;
                    lAnnulationRealze.Text = "Взята в работу реализитором " + row.Field<string>("ManagOk") + " " +
                                   row.Field<DateTime>("DATE_OF_OK").ToString("dd.MM.yy HH:mm");
                }



                if (row["BronOk"] == DBNull.Value)
                {
                    lAnnulationBronir.Text = "Не взята в работу бронировщиком";
                    if (_access.isBronir || _access.isSuperViser)
                    {
                        btnAnnulatuionSetBronir.Tag = num;
                        btnAnnulatuionSetBronir.Enabled = true;
                    }
                    else
                    {
                        btnAnnulatuionSetBronir.Enabled = false;
                    }

                }
                else
                {
                    btnAnnulatuionSetBronir.Enabled = false;
                    lAnnulationBronir.Text = "Взята в работу бронировщиком " + row.Field<string>("BronOk") + " " +
                                             row.Field<DateTime>("BRONIR_OK_DATE").ToString("dd.MM.yy HH:mm");
                }



                if (row["ManagCalc"] == DBNull.Value)
                {
                    lAnnulationCalculate.Text = row.Field<string>("PT_MessageNoText");
                    if (((row["ManagOk"] != DBNull.Value) || !row.Field<bool>("PT_NEEDREALIZE")) && ((row["BronOk"] != DBNull.Value) || !row.Field<bool>("PT_NEEDBron")))
                    {
                        btnAnnulationCalc.Tag = num;
                        btnAnnulationCalc.Enabled = true;
                    }
                    else
                    {
                        btnAnnulationCalc.Enabled = false;
                    }
                }
                else
                {
                    btnAnnulationCalc.Enabled = false;
                    lAnnulationCalculate.Text = row.Field<string>("PT_MessageText") +" "+ row.Field<string>("ManagCalc") + " " +
                                      row.Field<DateTime>("DATE_OF_CALCULATE").ToString("dd.MM.yy HH:mm") +
                                      ". " +
                                      ((row.Field<int>("IS_PENALTY") == 1) ? " со штрафом" : "") + "!";
                    btnAnnulationClose.Enabled = false;
                }
            }
            else
            {
                lPetition.Text = "Заявка " + num.ToString() + " закрыта " + row.Field<string>("CloseManag") + " " +
                            row.Field<DateTime>("CLOSETASK_DATE").ToString("dd.MM.yy HH:mm");
                btnAnnulationCalc.Enabled = false;
                btnAnnulationSetRealize.Enabled = false;
                btnAnnulatuionSetBronir.Enabled = false;
                btnAnnulationClose.Enabled = false;

            }
        }
        
        
        private void btnPetitionView_Click(object sender, EventArgs e)
        {
            int num = (int)((Button)sender).Tag;
            byte[] data = WorkWithData.GetPetition(num);
            string fileName = _tempDirectory + Path.PathSeparator + "Petition" + num.ToString() + ".pdf";
            BinaryWriter bw = new BinaryWriter(File.Create(fileName));
            bw.Write(data);
            bw.Close();
            Process.Start(fileName);
        }

        private void btnSetRealize_Click(object sender, EventArgs e)
        {
            int num = (int)((Button)sender).Tag;
            WorkWithData.UpdateAnnulateOk(num);
            SetAnnulation();
        }

        private void btnSetBronir_Click(object sender, EventArgs e)
        {
            int num = (int)((Button)sender).Tag;
            WorkWithData.UpdateAnnulateBronOk(num);
            SetAnnulation();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_quetion, "", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                int num = (int) ((Button) sender).Tag;
                WorkWithData.UpdateAnnulateCalculate(num, _needPen);
                SetAnnulation();
            }
        }

        private void btnAnnulationClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите закрыть заявление?", "", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                int num = (int) ((Button) sender).Tag;
                WorkWithData.UpdateAnnulateClose(num);
                SetAnnulation();
            }
        }
    }
}
