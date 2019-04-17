using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rep10027.Helpers;

namespace terms_prepaid
{
    public partial class frmQuestionnaire : Form
    {
        private int _tu_key;
        private const string selectTurist = @"  select 
  
        TU_NUMPENSIA
        , t.TU_KEY as TU_KEY
        , TU_DATEPENSIA
        , TU_WHOPENSIA
        , TU_PHONEHOUSE
        , TU_CITY_INDEX
        , TU_SETTLEMENT
        , TU_BIRTHDAY
        , TU_STREET
        , TU_HOUSE
        , TU_BUILDING
        , TU_APARTMENTS
        , TU_FNAMERUS
        , TU_SNAMELAT
        , TU_NAMELAT
        , TU_NAMERUS   
        , TU_FNAMELAT 
        , TU_PASPORTDATEEND  
        , TU_SNAMERUS 
        , TU_PASPORTTYPE  
        , TU_PASPORTNUM  
        , TU_BIRTHDAY
        , TU_BIRTHCOUNTRY  
        , TU_BIRTHCITY  
        , TU_PASPORTDATE
        , TU_CITIZEN  
        , TU_PHONE
        , TU_PASPRUSER
        , TU_PASPRUNUM 
        , TU_PASPORTBYWHOM
        , TU_PASPRUBYWHOM
        , TU_PASPRUDATE  
        , TU_POSTINDEX 
        , TU_POSTCITY  
        , TU_POSTSTREET  
        , isnull(TU_POSTBILD,'') as TU_POSTBILD
        , TU_POSTFLAT 
        , TU_EMAIL 
        , TU_CODEOFFICE
        from tbl_turist as t
        left join dbo.mk_turist_add as ta on  t.TU_KEY =ta.TU_KEY 
        where  t.TU_KEY =@tu_key ";
        const string select_country = @"select CN_NAME from  tbl_Country";


        private const string updateTurist = @"update [mk_turist_add] set
        [TU_NUMPENSIA] = @TUNUMPENSIA
        ,[TU_DATEPENSIA] =@TUDATEPENSIA
        ,[TU_WHOPENSIA] =@TUWHOPENSIA
        ,[TU_PHONEHOUSE] =@TUPHONEHOUSE
        ,[TU_CITY_INDEX] =@TUCITY_INDEX
        ,[TU_SETTLEMENT] =@TUSETTLEMENT
        ,[TU_STREET] =@TUSTREET
        ,[TU_HOUSE] =@TUHOUSE
        ,[TU_BUILDING]=@TUBUILDING
        ,[TU_APARTMENTS]=@TUAPARTMENTS
        ,[TU_CODEOFFICE] =@TUCODEOFFICE
        where TU_KEY=@tukey

        update [tbl_Turist] set 
        [TU_NAMERUS]=@TUNAMERUS
        ,[TU_NAMELAT]=@TUNAMELAT
        ,[TU_FNAMERUS]=@TUFNAMERUS
        ,[TU_FNAMELAT]=@TUFNAMELAT
        ,[TU_SNAMERUS]=@TUSNAMERUS
        ,[TU_SNAMELAT]=@TUSNAMELAT
        ,[TU_BIRTHDAY]=@TUBIRTHDAY
        ,[TU_BIRTHCOUNTRY]=@TUBIRTHCOUNTRY
        ,[TU_BIRTHCITY]=@TUBIRTHCITY
        ,[TU_CITIZEN]=@TUCITIZEN
        ,[TU_POSTINDEX]=@TUPOSTINDEX
        ,[TU_POSTCITY]=@TUPOSTCITY
        ,[TU_POSTSTREET]=@TUPOSTSTREET
        ,[TU_POSTBILD]=@TUPOSTBILD
        ,[TU_POSTFLAT]=@TUPOSTFLAT
        ,[TU_PHONE]=@TUPHONE
        ,[TU_PASPORTTYPE]=@TUPASPORTTYPE
        ,[TU_PASPORTNUM]=@TUPASPORTNUM
        ,[TU_PASPORTDATE]=@TUPASPORTDATE
        ,[TU_PASPORTDATEEND]=@TUPASPORTDATEEND
        ,[TU_PASPORTBYWHOM]=@TUPASPORTBYWHOM
        ,[TU_PASPRUSER]=@TUPASPRUSER
        ,[TU_PASPRUNUM]=@TUPASPRUNUM
        ,[TU_PASPRUDATE]=@TUPASPRUDATE
        ,[TU_PASPRUBYWHOM]=@TUPASPRUBYWHOM
        ,[TU_EMAIL]=@TUEMAIL
        where TU_KEY=@tukey";
        private List<string> _Countrys;
        public frmQuestionnaire(int tu_key)
        {
            InitializeComponent();
            _tu_key = tu_key;
            GetDate();
            SetEdit(false);
            this.Dock = DockStyle.Left;
            this.Location = new Point(0,0);

           
        }
        void GetDate()
        {
            
            using (SqlDataAdapter adapter = new SqlDataAdapter(select_country, WorkWithData.Connection))
            {
               DataTable dt = new DataTable();
                adapter.Fill(dt);
                _Countrys = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    _Countrys.Add(row.Field<string>("CN_NAME"));
                }
            }
            cbCitizen.DataSource = _Countrys;
            using (SqlDataAdapter adapter = new SqlDataAdapter(selectTurist,WorkWithData.Connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@tu_key", _tu_key);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (row.Field<DateTime?>("TU_BIRTHDAY") != null)
                    {
                        TimeSpan diff1 =  DateTime.Now.Date-row.Field<DateTime>("TU_BIRTHDAY") ;
                        if ((diff1.Days/365) >= 14)
                        {
                            label9.Text = "Российский паспорт";
                        }
                        else
                        {
                            label9.Text = "Свидетельство о рождении";
                        }
                    }
                    //Фамили, Имя, Отчество
                    tbName.Text = row.Field<string>("TU_NAMELAT");
                    tbFName.Text = row.Field<string>("TU_FNAMELAT");
                    tbSName.Text = row.Field<string>("TU_SNAMELAT");
                    tbNameRus.Text = row.Field<string>("TU_NAMERUS");
                    tbFnameRus.Text = row.Field<string>("TU_FNAMERUS");
                    tbSNameRus.Text = row.Field<string>("TU_SNAMERUS");
                    //Рождние, телефоны, e-mail
                    tbBirthCity.Text = row.Field<string>("TU_BIRTHCITY");
                    tbBirthCountry.Text = row.Field<string>("TU_BIRTHCOUNTRY");
                    cbCitizen.Text = row.Field<string>("TU_CITIZEN");
                    mtbPhone.Text = row.Field<string>("TU_PHONE");
                    mtbPhoneHouse.Text = row.Field<string>("TU_PHONEHOUSE");
                    tbEmail.Text = row.Field<string>("TU_EMAIL");
                    mtbBirthDay.Text = row.Field<DateTime?>("TU_BIRTHDAY") != null
                        ? row.Field<DateTime>("TU_BIRTHDAY").ToString("dd.MM.yyyy") : "";
                    //Загранпаспорт 
                    tbPasportType.Text = row.Field<string>("TU_PASPORTTYPE");
                    tbPasportNum.Text = row.Field<string>("TU_PASPORTNUM");
                    tbPasportWho.Text = row.Field<string>("TU_PASPORTBYWHOM");
                    mtbPassportDate.Text = row.Field<DateTime?>("TU_PASPORTDATE") != null
                        ? row.Field<DateTime>("TU_PASPORTDATE").ToString("dd.MM.yyyy"):"";
                    mtbPassportDateEnd.Text = row.Field<DateTime?>("TU_PASPORTDATEEND")!=null?row.Field<DateTime>("TU_PASPORTDATEEND").ToString("dd.MM.yyyy"):"";
                    //Российский паспорт
                    tbPasportTypeRus.Text = row.Field<string>("TU_PASPRUSER");
                    tbPasportNumRus.Text = row.Field<string>("TU_PASPRUNUM");
                    tbPasportWhoRus.Text = row.Field<string>("TU_PASPRUBYWHOM");
                    tbCodeOffice.Text = row.Field<string>("TU_CODEOFFICE");
                    mtbPasportDateRus.Text = row.Field<DateTime?>("TU_PASPRUDATE") != null
                        ? row.Field<DateTime>("TU_PASPRUDATE").ToString("dd.MM.yyyy") : "";
                    //Пенсионное
                    tbPensiaNum.Text = row.Field<string>("TU_NUMPENSIA");
                    tbPensiaWho.Text = row.Field<string>("TU_WHOPENSIA");
                    mtbPensiaDate.Text = row.Field<DateTime?>("TU_DATEPENSIA") != null
                                             ? row.Field<DateTime>("TU_DATEPENSIA").ToString("dd.MM.yyyy")
                                             : "";
                    //Адрес регистрации

                    tbPostCity.Text = row.Field<string>("TU_POSTCITY");
                    tbPostStreet.Text = row.Field<string>("TU_POSTSTREET");
                    string[] strs = row.Field<string>("TU_POSTBILD").Split('-');
                    if (strs.Length > 0)
                    {
                        tbPostBild.Text = strs[0];
                        if (strs.Length > 1)
                        {
                            tbPostCorpus.Text = strs[1];
                        }
                    }
                    tbPostFlat.Text = row.Field<string>("TU_POSTFLAT");
                    tbPostIndex.Text = row.Field<string>("TU_POSTINDEX");

                    //Адрес фактический

                    tbCity.Text = row.Field<string>("TU_SETTLEMENT");
                    tbStreet.Text = row.Field<string>("TU_STREET");
                    tbFlat.Text = row.Field<string>("TU_HOUSE");
                    tbCorpus.Text = row.Field<string>("TU_BUILDING");
                    tbFlat.Text = row.Field<string>("TU_APARTMENTS");
                    tbIndex.Text = row.Field<string>("TU_CITY_INDEX");


                }
                

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetEdit(true);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            using (SqlCommand com = new SqlCommand(updateTurist,WorkWithData.Connection))
            {
                com.Parameters.AddWithValue("@tukey",_tu_key);
                
                if (tbPensiaNum.Text == "")
                {
                    com.Parameters.AddWithValue("@TUNUMPENSIA", DBNull.Value);
                }
                else
                {
                    com.Parameters.AddWithValue("@TUNUMPENSIA", tbPensiaNum.Text);
                }
                if (mtbPensiaDate.Text == "  .  .")
                {
                    com.Parameters.AddWithValue("@TUDATEPENSIA", DBNull.Value);
                }
                else
                {
                    try
                    {
                        com.Parameters.AddWithValue("@TUDATEPENSIA", DateTime.Parse(mtbPensiaDate.Text));
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Неправельно введена дата выдачи пенсионного");
                        return;
                    }
                }
                if (tbPensiaWho.Text == "")
                {
                    com.Parameters.AddWithValue("@TUWHOPENSIA", DBNull.Value);
                }
                else
                {
                    com.Parameters.AddWithValue("@TUWHOPENSIA", tbPensiaWho.Text);
                }

                com.Parameters.AddWithValue("@TUPHONEHOUSE", mtbPhoneHouse.Text);
                com.Parameters.AddWithValue("@TUCITY_INDEX", tbIndex.Text);
                com.Parameters.AddWithValue("@TUSETTLEMENT", tbCity.Text);
                com.Parameters.AddWithValue("@TUSTREET", tbStreet.Text);
                com.Parameters.AddWithValue("@TUHOUSE", tbBild.Text);
                com.Parameters.AddWithValue("@TUBUILDING", tbCorpus.Text);
                com.Parameters.AddWithValue("@TUAPARTMENTS", tbFlat.Text);
                com.Parameters.AddWithValue("@TUCODEOFFICE", tbCodeOffice.Text);
                
                com.Parameters.AddWithValue("@TUNAMERUS", tbNameRus.Text);
                com.Parameters.AddWithValue("@TUNAMELAT", tbName.Text);
                com.Parameters.AddWithValue("@TUFNAMERUS", tbFnameRus.Text);
                com.Parameters.AddWithValue("@TUFNAMELAT", tbFName.Text);
                com.Parameters.AddWithValue("@TUSNAMERUS", tbSNameRus.Text);
                com.Parameters.AddWithValue("@TUSNAMELAT", tbSName.Text);
                if (mtbBirthDay.Text == "  .  .")
                {
                    com.Parameters.AddWithValue("@TUBIRTHDAY", DBNull.Value);
                }
                else
                {
                    try
                    {
                        com.Parameters.AddWithValue("@TUBIRTHDAY", DateTime.Parse(mtbBirthDay.Text));
                    }
                    catch (Exception)
                    {


                        MessageBox.Show("Неправельно введена дата рождения");
                        return;
                    }
                }
                
                com.Parameters.AddWithValue("@TUBIRTHCOUNTRY", tbBirthCountry.Text);
                com.Parameters.AddWithValue("@TUBIRTHCITY", tbBirthCity.Text);
                
                com.Parameters.AddWithValue("@TUCITIZEN",cbCitizen.Text);
                
                com.Parameters.AddWithValue("@TUPOSTINDEX", tbPostIndex.Text);
                com.Parameters.AddWithValue("@TUPOSTCITY", tbPostCity.Text);
                com.Parameters.AddWithValue("@TUPOSTSTREET",tbPostStreet.Text);
                com.Parameters.AddWithValue("@TUPOSTBILD", tbPostBild.Text);
                com.Parameters.AddWithValue("@TUPOSTFLAT", tbPostFlat.Text);
                com.Parameters.AddWithValue("@TUPHONE", mtbPhone.Text);
                com.Parameters.AddWithValue("@TUPASPORTTYPE", tbPasportType.Text);
                com.Parameters.AddWithValue("@TUPASPORTNUM", tbPasportNum.Text);
                if (mtbPassportDate.Text == "  .  .")
                {
                    com.Parameters.AddWithValue("@TUPASPORTDATE", DBNull.Value);
                }
                else
                {
                    com.Parameters.AddWithValue("@TUPASPORTDATE", DateTime.Parse(mtbPassportDate.Text));
                }

                if (mtbPassportDateEnd.Text == "  .  .")
                {
                    com.Parameters.AddWithValue("@TUPASPORTDATEEND", DBNull.Value);
                }
                else
                {
                    com.Parameters.AddWithValue("@TUPASPORTDATEEND", DateTime.Parse(mtbPassportDateEnd.Text));
                }

                
                com.Parameters.AddWithValue("@TUPASPORTBYWHOM", tbPasportWho.Text);
                com.Parameters.AddWithValue("@TUPASPRUSER", tbPasportTypeRus.Text);
                com.Parameters.AddWithValue("@TUPASPRUNUM", tbPasportNumRus.Text);

                if (mtbPasportDateRus.Text == "  .  .")
                {
                    com.Parameters.AddWithValue("@TUPASPRUDATE", DBNull.Value);
                }
                else
                {
                    com.Parameters.AddWithValue("@TUPASPRUDATE", DateTime.Parse(mtbPasportDateRus.Text));
                }

                
                com.Parameters.AddWithValue("@TUPASPRUBYWHOM", tbPasportWhoRus.Text);
                com.Parameters.AddWithValue("@TUEMAIL", tbEmail.Text);
                com.ExecuteNonQuery();
            }
            SetEdit(false);
        }
        void SetEdit(bool flag)
        {
            btnOk.Enabled = flag;
            tbFName.ReadOnly = !flag;
            tbName.ReadOnly = !flag;
            tbSName.ReadOnly = !flag;
            tbFnameRus.ReadOnly = !flag;
            tbNameRus.ReadOnly = !flag;
            tbSNameRus.ReadOnly = !flag;
            tbPasportNum.ReadOnly = !flag;
            tbPasportWho.ReadOnly = !flag;
            tbPasportType.ReadOnly = !flag;
            mtbPassportDate.ReadOnly = !flag;
            mtbPassportDateEnd.ReadOnly = !flag;
            tbPasportNumRus.ReadOnly = !flag;
            tbPasportWhoRus.ReadOnly = !flag;
            tbPasportTypeRus.ReadOnly = !flag;
            mtbPasportDateRus.ReadOnly = !flag;
            tbPensiaNum.ReadOnly = !flag;
            tbPensiaWho.ReadOnly = !flag;
            mtbPensiaDate.ReadOnly = !flag;
            tbBirthCity.ReadOnly = !flag;
            tbBirthCountry.ReadOnly = !flag;
            mtbPhone.ReadOnly = !flag;
            mtbPhoneHouse.ReadOnly = !flag;
            tbEmail.ReadOnly = !flag;
            cbCitizen.Enabled = flag;
            tbCodeOffice.ReadOnly = !flag;
            btnCopyAdress.Enabled = flag;
            tbIndex.ReadOnly = !flag;
            tbPostIndex.ReadOnly = !flag;
            tbCity.ReadOnly = !flag; 
            tbPostCity.ReadOnly = !flag;
            tbStreet.ReadOnly = !flag; 
            tbPostStreet.ReadOnly = !flag;
            tbBild.ReadOnly = !flag;  
            tbPostBild.ReadOnly = !flag;
            tbCorpus.ReadOnly = !flag; 
            tbPostCorpus.ReadOnly = !flag;
            tbFlat.ReadOnly = !flag;
            tbPostFlat.ReadOnly = !flag;
            mtbBirthDay.ReadOnly = !flag;

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbCopyAdress_Click(object sender, EventArgs e)
        {
            
            tbIndex.Text = tbPostIndex.Text;
            tbCity.Text = tbPostCity.Text;
            tbStreet.Text = tbPostStreet.Text;
            tbBild.Text = tbPostBild.Text;
            tbCorpus.Text = tbPostCorpus.Text;
            tbFlat.Text = tbPostFlat.Text;
            



        }
    }
}
 


