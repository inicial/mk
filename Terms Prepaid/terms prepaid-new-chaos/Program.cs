using System;
using System.Windows;
using System.Windows.Forms;
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

namespace terms_prepaid
{
    internal static class Program
    {
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

            if (args.Length > 0 && !startupPath.Equals("") && !startupPath.Equals("null"))
            {
                Config_XML.StartupPath = startupPath + "\\Lanta.SQLConfig.dll";
                ApplicationConf.StartupPath = startupPath;
            }
            
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

                Config_XML conf = new Config_XML();

                TpLogger.Enabled = Convert.ToBoolean(conf.Get_Value("appSettings", "logger"));

                TouristRepo.ConnectionString = WorkWithData.Connection.ConnectionString;
                //CallRecordRepo.ConnectionString = WorkWithData.Connection.ConnectionString;
                
                DependencyInjection();
                DispatcherHelper.Initialize();

                Application.Run(GetMainForm());
                //Application.Run(GetTestBron());
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
            Repository.Register<IRequestsJournalViewModel>(() => new RequestsJournalViewModel2());
            Repository.Register<IRequestJournalView>(() => { var view = new RequestsJournalView2(); view.InitializeComponent(); return view; });
            
            Repository.Register<IRequestNewMessageView>(RequestNewMessageView3.GetInstance);
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
            //const string dgCode = "MSC70217A1";
            //const string dgCode = "CAR61014A1";
            const string dgCode = "TCO70316A1";
            
            frmNewOptions newOptions = new frmNewOptions(dgCode);

            newOptions.Text += " ver." + newOptions.GetType().Assembly.GetName().Version.ToString()
                          + " db:" + WorkWithData.Connection.Database;

            return newOptions;
        }
    }

}

