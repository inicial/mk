using System;
using System.Net.Mime;
using System.Windows.Forms;
using ltp_v2.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using terms_prepaid;
using terms_prepaid.Helpers;

namespace TermsPrepairTests
{
    [TestClass]
    public class FormNewOptionTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            string user = "s_schekotkov";
            string pass = "wbrnekazzak";
            string name = "TermsPrepaid";
            LogonScreen screen = new LogonScreen(user, pass, name);

            if (screen.Show() == DialogResult.OK)
            {
                WorkWithData.InitConnection(ltp_v2.Framework.SqlConnection.ConnectionUserName,
                                            ltp_v2.Framework.SqlConnection.ConnectionPassword);

                GetAviaTest().Show();
                //Application.Run();
            }

            Assert.IsTrue(true);
        }

        private static Form GetAviaTest()
        {
            const string dgCode = "MSC70217A1";
            frmNewOptions newOptions = new frmNewOptions(dgCode);

            return newOptions;
        }
    }
}
