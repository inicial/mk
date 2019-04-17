using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Awesomium.Core;
using Awesomium.Core.Data;

namespace terms_prepaid.Forms
{
    public partial class frmFlight : Form
    {
        private WebSession sessionId;

        private WebSession InitializeCoreAndSession()
        {
            WebSession session;
            if (!WebCore.IsInitialized)
                WebCore.Initialize(new WebConfig()
                {
                    AssetProtocol = "https",
                    LogLevel = LogLevel.Normal
                });

            // Build a data path string. In this case, a Cache folder under our executing directory.
            // - If the folder does not exist, it will be created.
            // - The path should always point to a writeable location.
            string dataPath = String.Format("{0}{1}Cache", Path.GetTempPath(), Path.DirectorySeparatorChar);

            // Check if a session synchronizing to this data path, is already created;
            // if not, create a new one.
            session = WebCore.Sessions[dataPath] ??
                      WebCore.CreateWebSession(dataPath, new WebPreferences() { });

            session.AddDataSource(DataSource.CATCH_ALL, new frmPopupWindow.MyDataSource());

            // The core must be initialized by now. Print the core version.
            //  Debug.Print(WebCore.Version.ToString());

            // Return the session.
            return session;
        }

        public frmFlight(Uri url)
        {
            InitializeComponent();
            sessionId = InitializeCoreAndSession();
            wbBook.WebSession = sessionId;
            wbBook.Source = url;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Awesomium_Windows_Forms_WebControl_ShowCreatedWebView(object sender, ShowCreatedWebViewEventArgs e)
        {
            if ((wbBook == null) || !wbBook.IsLive)
                return;

            if (e.IsPopup)
            {
                frmChildWindow child = new frmChildWindow();
                child.wcChild.WebSession = sessionId;
                child.wcChild.NativeView = e.NewViewInstance;
                child.wspChild = wspBook;
                child.wcChild.Source = e.TargetURL;

                if (!child.IsDisposed) child.ShowDialog();
            }
            else if (e.IsWindowOpen == false && e.IsNavigationCanceled == true)
            {
                wbBook.Source = e.TargetURL;
            }
        }
    }
}
