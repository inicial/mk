using System;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using DataService;
using GalaSoft.MvvmLight.Threading;
using lanta.SQLConfig;
using terms_prepaid.Helpers;
using ltp_v2.Framework;
using WpfControlLibrary;
using WpfControlLibrary.Buttons;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.Util;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using Application = System.Windows.Forms.Application;
using System.Threading;


namespace terms_prepaid
{
    internal static class Program
    {
        //public static frmIntroMain IntroForm;
        //public static string user = "";
        //public static string pass = "";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[LoaderOptimization(LoaderOptimization.MultiDomainHost)]
        [STAThread] 
        private static void Main(params string[] args)
        {
            // Works
            //AppDomain.CurrentDomain.SetShadowCopyFiles();
            //AppDomain.CurrentDomain.SetCachePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Cache\");

            // Dont Work
            //AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles = "true";
            // AppDomain.CurrentDomain.SetupInformation.CachePath =
            //    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Cache\";
            //GetDomainInfo();
            //EnableShadowCopying();
            //if(AppDomain.CurrentDomain.IsDefaultAppDomain())

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            string startupPath = (args.Length > 0) ? args[0] : "";
            string user = (args.Length > 1) ? args[1] : "";
            string pass = (args.Length > 2) ? args[2] : "";
            //user = "";
            //pass = "";
            //user = "VEPIHINA";
            //pass = "qwerty12345";

            if (args.Length > 0 && !startupPath.Equals("") && !startupPath.Equals("null"))
            {
                Config_XML.StartupPath = startupPath + "\\Lanta.SQLConfig.dll";
                ApplicationConf.StartupPath = startupPath;
            }

            bool bEntry = false;
            
            LogonScreen screen = new LogonScreen(user, pass, Application.ProductName);
            if (screen.Show() == DialogResult.OK) bEntry = true;

            //--added this splashscreen for test, because not see when app is loading
            SplashScreen splashScreen = new SplashScreen("Resources\\loading.gif");
            splashScreen.Show(true, true);
            //--
            /*
                        frmIntroMain IntroForm = new frmIntroMain(user, pass, Application.ProductName);
                        IntroForm.Show();
                        bool bWait = true;
                        while (bWait)
                        {
                            Application.DoEvents();
                            bWait = false;
                            if (!IntroForm.ExitFlag) bWait = true;
                        }
                        if (IntroForm.Authorized)
                        {
                            bEntry = true;
                        }
                        else
                        {
                            IntroForm.Close();
                            IntroForm.Dispose();
                            IntroForm = null;
                        }
            */
            if (bEntry)
            {
                string UsingDGCode = ltp_v2.Framework.MasterValue.DGCodeFromASKData;
                if ((args.Length > 2) && (args[2].IndexOf("!DGCODE=") >= 0))
                {
                    UsingDGCode = args[2].Replace("!DGCODE=", "");
                }
              
                WorkWithData.InitConnection(SqlConnection.ConnectionUserName,
                                            SqlConnection.ConnectionPassword);

                Config_XML conf = new Config_XML();

                TpLogger.Enabled = Convert.ToBoolean(conf.Get_Value("appSettings", "logger"));

                TouristRepo.ConnectionString = WorkWithData.Connection.ConnectionString;
                //CallRecordRepo.ConnectionString = WorkWithData.Connection.ConnectionString;

                DependencyInjection();
                DispatcherHelper.Initialize();

                try
                {
                    Form main_form = GetMainForm();
                    main_form.Show();
                    main_form.Activate();

                    //if (IntroForm != null) IntroForm.Close();
                    
                    Application.Run(main_form);
                    
                    //Application.Run(GetTestBron());
                }
                catch (System.Exception ex)
                {
                    //WriteLog("PROGRAM: " + ex.Message + (char)13 + (char)10 + ex.StackTrace);
                    TpLogger.Debug("PROGRAM", ex);
                }

            }
        }

        private static void DependencyInjection()
        {
            Repository.Register<IAviaCardsService>(WorkWithData.GetInstance);
            Repository.Register<IMilesAirlineListViewModel>(() => new MilesAirlineListViewModel());
            Repository.Register<IContractService>(WorkWithData.GetInstance);
            Repository.Register<IVoucherService>(WorkWithData.GetInstance);
            Repository.Register<IUsersService>(WorkWithData.GetInstance);
            Repository.Register<ICollectingLowcostAirlinesService>(WorkWithData.GetInstance);
            Repository.Register<ICorrespondenceService>(WorkWithData.GetInstance);
            Repository.Register<ICollectingLowcostAirlinesViewModel>(() => new CollectingLowcostAirlinesViewModel());
            Repository.Register<TouristDataDataContext>(TouristRepo.GetDb);
            Repository.Register<IInsurLoader>(() => new InsurLoader());
            Repository.Register<ICruiseLoader>(() => new CruiseLoader());
            Repository.Register<ICorrespondenceLoader>(() => new CorrespondenceLoader());
            Repository.Register<IMessageBoxService>(() => new MessageBoxService());

            Repository.Register<IFlightControl>(() => new FlightControl());
            Repository.Register<ICruiseControl>(() => new CruiseControl());
            Repository.Register<IHotelControl>(() => new HotelControl());
            Repository.Register<IVisaControl>(() => new VisaControl());
            Repository.Register<IInshurControl>(() => new InshurControl());
            Repository.Register<IOtherServiceControl>(() => new OtherServiceControl());

            Repository.Register<IVoucherTabView>(() => new VoucherTabView());
            Repository.Register<IServiceTabView>(() => new ServiceTabView());

            Repository.Register<IRequestJournalService>(WorkWithData.GetInstance);
            Repository.Register<IRequestJournalLoader>(() => new TestMailLoader2());
            Repository.Register<IRequestCorrespondenceLoader>(() => new RequestCorrespondenceLoader());

            Repository.Register<IAccessService>(() => new AccessClass(WorkWithData.Connection));
            Repository.Register<IWindowsHelper>(WindowsHelper.GetInstance);
            Repository.Register<ISelectManagerView>(() => new SelectManagerView() { ResizeMode = ResizeMode.NoResize, Topmost = true });

            Repository.Register<IRequestJournalSender>(() => new RequestJournalSender(Repository.GetInstance<IMailConfig>()));

            Repository.Register<IMailConfig>(() => new MyWorkMail());
            Repository.Register<IRequestsJournalViewModel>(() => new RequestsJournalViewModel());
            Repository.Register<IRequestJournalView>(() => { var view = new RequestsJournalView(); view.InitializeComponent(); return view; });
            
            Repository.Register<IRequestNewMessageView>(RequestNewMessageView.GetInstance);
            Repository.Register<IProblemRequestsView>(ProblemRequestsView.GetInstance);

            Repository.Register<IUnseenRequestMessageService>(WorkWithData.GetInstance);

            Repository.Register<IRequestJournalButton>(() => { var view = new RequestJournalButton2(); view.InitializeComponent(); return view; });
            Repository.Register<IRequestJournalButtonViewModel>(() => new RequestJournalButtonViewModel());

            Repository.Register<ISelectCancelationReasonView>(() => { var view = new SelectCancelationReasonView(); view.InitializeComponent(); return view; });
            Repository.Register<IChangeSenderAddressView>(() => { var view = new ChangeSenderAddressView(); view.InitializeComponent(); return view; });

            Repository.Register<IUnansweredRequestsService>(WorkWithData.GetInstance);

            Repository.Register<ISession>(Session.GetInstance);

            Repository.Register<ICallRecordsView>( () => { var view = new CallRecordsView(); view.InitializeComponent(); return view; } );
            Repository.Register<CallRecordsViewModelBase>(() => new CallRecordsViewModel());
            Repository.Register<ICallRecordService>(WorkWithData.GetInstance);

            Repository.Register<IDialog>(() => { var view = new DialogView(); view.InitializeComponent(); return view; });
        }

        public static Form GetMainForm()
        {
            frmSerchDogovors mainForm = new frmSerchDogovors();
            mainForm.Text = mainForm.Text
                            + " ver." + mainForm.GetType().Assembly.GetName().Version.ToString();
#if DEBUG
            mainForm.Text += " db:" + WorkWithData.Connection.Database;
#endif
            return mainForm;
        }


        public static Form GetTestBron()
        {
            const string dgCode = "CSL70511A1";
            
            frmNewOptions newOptions = new frmNewOptions(dgCode);

            newOptions.Text += " ver." + newOptions.GetType().Assembly.GetName().Version.ToString()
                          + " db:" + WorkWithData.Connection.Database;

            return newOptions;
        }


        //====================================================================================================
        #region WriteLog
        //----------------------------------------------------------------------------------------------------
        static void WriteLog(string log_msg)
        {
            string app_path = Application.ExecutablePath;
            string dir = app_path.Substring(0, app_path.LastIndexOf('\\') + 1);
            string log_dir = dir + "logs";
            if (!System.IO.Directory.Exists(log_dir))
                System.IO.Directory.CreateDirectory(log_dir);

            string path = log_dir + (char)92 + "exceptions.txt";
            string login = ltp_v2.Framework.SqlConnection.ConnectionUserName;

            if (!File.Exists(path)) File.Create(path).Close();
            StreamWriter LogFile = File.AppendText(path);
            LogFile.WriteLine( DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + " (" + login + "): " + log_msg);
            LogFile.Close();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion
        //====================================================================================================

    }

}

