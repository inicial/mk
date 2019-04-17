using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace terms_prepaid
{
    public partial class frmIntroMain : Form
    {
        private string Login;
        private string Password;
        private string ApplicationName;

        public bool Authorized;
        public bool ExitFlag;

        //Timer FormTimer;


        public frmIntroMain(string iLogin, string iPassword, string iApplicationName)
        {
            InitializeComponent();

            Login = iLogin;
            Password = iPassword;
            ApplicationName = iApplicationName;
            Authorized = false;

            pic_CloseFrame.BackColor = Color.FromArgb(100, 10, 10);
            pic_Close.BackColor = Color.FromArgb(167, 25, 23);
            pic_Close.Image = Properties.Resources.img_button_close_grey;

            pic_CloseFrame.Visible = true;
            pic_Close.Visible = true;

            //FormTimer = new Timer();
            //FormTimer.Interval = 10;
            //FormTimer.Tick += FormTimer_Tick;
        }

        private void frmIntroMain_Load(object sender, EventArgs e)
        {
            edt_Login.Text = Login;
            edt_Password.Text = Password;

            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password)) StartAuth();

            //FormTimer.Start();
        }

        private void FormTimer_Tick(Object myObject, EventArgs myEventArgs)
        {
            Application.DoEvents();
            //pic_Loader.Refresh();
            //this.Refresh();
        }

        private void Status(string status)
        {
            txt_Status.Text = status;
        }

        //====================================================================================================
        #region Form events
        //----------------------------------------------------------------------------------------------------
        private void pic_Close_MouseEnter(object sender, EventArgs e)
        {
            pic_Close.Image = Properties.Resources.img_button_close;
            Cursor = Cursors.Hand;
            pic_Close.Refresh();
            //this.Refresh();
        }

        //----------------------------------------------------------------------------------------------------
        private void pic_Close_MouseLeave(object sender, EventArgs e)
        {
            pic_Close.Image = Properties.Resources.img_button_close_grey;
            Cursor = Cursors.Default;
            pic_Close.Refresh();
            //this.Refresh();
        }

        //----------------------------------------------------------------------------------------------------
        private void pic_Close_Click(object sender, EventArgs e)
        {
            ExitFlag = true;
            this.Close();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Form events
        //====================================================================================================

        private void StartAuth()
        {
            ltp_v2.Framework.SqlConnection.ConnectionProgram = ApplicationName;
            ltp_v2.Framework.SqlConnection.ConnectionUserName = edt_Login.Text;
            ltp_v2.Framework.SqlConnection.ConnectionPassword = edt_Password.Text;

            if (edt_Password.Text != "")
            {
                if (ltp_v2.Framework.SqlConnection.CheckConnectionValid())
                {
                    Authorized = true;
                    ExitFlag = true;

                    txt_Status.Top = txt_Status.Top - 80;
                    Status(@"Загрузка рабочего места...");
                    pLogin.Visible = false;
                    pic_Close.Visible = false;
                    pic_CloseFrame.Visible = false;
                    //pic_Loader.Visible = true;

                    this.Refresh();
                }
                else
                {
                    Status(@"Не верно введен логин/пароль");
                }
            }
            else
            {

            }
        }


        private void btn_Enter_Click(object sender, EventArgs e)
        {
            StartAuth();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            ExitFlag = true;
            this.Close();
        }

        private void edt_Login_TextChanged(object sender, EventArgs e)
        {
            Status("");
        }

        private void edt_Password_TextChanged(object sender, EventArgs e)
        {
            Status("");
        }

    }
}
