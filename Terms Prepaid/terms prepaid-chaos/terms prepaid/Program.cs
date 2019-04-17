using System;
using System.Collections.Generic;
using System.Windows.Forms;
using terms_prepaid.Helpers;
using ltp_v2.Framework;
using terms_prepaid.Forms;
using SqlConnection = System.Data.SqlClient.SqlConnection;

namespace terms_prepaid
{
    internal static class Program
    {

  
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] 
        private static void Main(params string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            string user = (args.Length > 0) ? args[0] : "";
            string pass = (args.Length > 1) ? args[1] : "";
            
            LogonScreen screen = new LogonScreen(user, pass, Application.ProductName);

            if (screen.Show() == DialogResult.OK)
            {
                string UsingDGCode = ltp_v2.Framework.MasterValue.DGCodeFromASKData;
                if ((args.Length > 2) && (args[2].IndexOf("!DGCODE=") >= 0))
                {
                    UsingDGCode = args[2].Replace("!DGCODE=", "");
                }
              
                WorkWithData.InitConnection(ltp_v2.Framework.SqlConnection.ConnectionUserName,
                                            ltp_v2.Framework.SqlConnection.ConnectionPassword);
                

                //if (args.Length > 1)
                //{
                  
                
                frmSerchDogovors mainForm =new  frmSerchDogovors();
                mainForm.Text = mainForm.Text
                                + " ver." + mainForm.GetType().Assembly.GetName().Version.ToString();
#if DEBUG
                mainForm.Text += " db:" + WorkWithData.Connection.Database;
#endif
                    Application.Run(mainForm);
                }
                //else
                //{

              
               // Application.Run(new frmSerchDogovors());
                //}

            }
        }


    }

