using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Util;


namespace WpfControlLibrary.ViewModel
{
    public class RequestJournalButtonViewModel : ViewModelBase, IRequestJournalButtonViewModel
    {
        public event MyControlEventHandlerSample OnButtonClick;
        private Action<int> _callback;

        private bool _selfOnly;
        public bool SelfOnly
        {
            get { return _selfOnly; }
            set
            {
                SetValue(ref _selfOnly, value);
                Update();
            }
        }

        private int _newRequests;
        public int NewRequests
        {
            get { return _newRequests; }
            set { SetValue(ref _newRequests, value); }
        }

        private int _newClientMessages;
        public int NewClientMessages
        {
            get { return _newClientMessages; }
            set { SetValue(ref _newClientMessages, value); }
        }

        private int _newManagerMessages;
        public int NewManagerMessages
        {
            get { return _newManagerMessages; }
            set { SetValue(ref _newManagerMessages, value); }
        }

        private bool _flash;
        public bool Flash
        {
            get { return _flash; }
            set { SetValue(ref _flash, value); }
        }

        private int _unansweredRequestsMtMCount;
        public int UnansweredRequestsMtMCount
        {
            get { return _unansweredRequestsMtMCount; }
            set { SetValue(ref _unansweredRequestsMtMCount, value); }
        }

        private int _unansweredRequestsMtCCount;
        public int UnansweredRequestsMtCCount
        {
            get { return _unansweredRequestsMtCCount; }
            set { SetValue(ref _unansweredRequestsMtCCount, value); }
        }

        //private RequestJournalHelper.NewRequestMessagesInfo _info;
        private IUnansweredRequestsService _service;
        private int _usKey;
        private bool _flashForSelfOnly;
        private IUnansweredRequestsService _unansweredRequestsService;

        private int _mtMCountSelf;
        public int MtMCountSelf
        {
            get { return _mtMCountSelf; }
            set { SetValue(ref _mtMCountSelf, value); }
        }

        private int _mtMCountAll;
        public int MtMCountAll
        {
            get { return _mtMCountAll; }
            set { SetValue(ref _mtMCountAll, value); }
        }

        private int _mtCCountSelf;
        public int MtCCountSelf
        {
            get { return _mtCCountSelf; }
            set { SetValue(ref _mtCCountSelf, value); }
        }

        private int _mtCCountAll;
        public int MtCCountAll
        {
            get { return _mtCCountAll; }
            set { SetValue(ref _mtCCountAll, value); }
        }

        private int _mtMCountSelfComp;
        public int MtMCountSelfComp
        {
            get { return _mtMCountSelfComp; }
            set { SetValue(ref _mtMCountSelfComp, value); }
        }
        //public string MtMCountSelfComp { get { return ""; } }

        private int _mtMCountAllComp;
        public int MtMCountAllComp
        {
            get { return _mtMCountAllComp; }
            set { SetValue(ref _mtMCountAllComp, value); }
        }
        //public string MtMCountAllComp { get { return ""; } }

        private int _mtCCountSelfComp;
        public int MtCCountSelfComp
        {
            get { return _mtCCountSelfComp; }
            set { SetValue(ref _mtCCountSelfComp, value); }
        }
        //public string MtCCountSelfComp { get { return ""; } }

        private int _mtCCountAllComp;
        public int MtCCountAllComp
        {
            get { return _mtCCountAllComp; }
            set { SetValue(ref _mtCCountAllComp, value); }
        }
        //public string MtCCountAllComp { get { return ""; } }

        public RelayCommand ButtonClickCommand { get; set; }
        public RelayCommand ShowAllCommand { get; set; }
        public RelayCommand ShowOnlyMyCommand { get; set; }
        public RelayCommand ShowAllCompanionsCommand { get; set; }
        public RelayCommand ShowOnlyMyCompanionsCommand { get; set; }
        

        public RequestJournalButtonViewModel()
        {
            _flashForSelfOnly = !Repository.GetInstance<IAccessService>().isSuperViser;
            _usKey = Repository.GetInstance<IUsersService>().GetUsKey();
            _unansweredRequestsService = Repository.GetInstance<IUnansweredRequestsService>();
        }

        public void Update()
        {
            //_info = RequestJournalHelper.GetNewMessagesCount();

            // mk_GetUnansweredRequestsCount  @mod, @usKey

            MtMCountSelf = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTM, _usKey); // MTOM
            MtMCountAll = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTM, null);    

            MtCCountSelf = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTC, _usKey); // MTOC
            MtCCountAll = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTC, null);

            MtMCountSelfComp = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTCM, _usKey, true);
            MtMCountAllComp = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTCM, null, true);

            MtCCountSelfComp = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTCC, _usKey, true);
            MtCCountAllComp = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTCC, null, true);
            
            UnansweredRequestsMtMCount = SelfOnly ? MtMCountSelf : MtMCountAll;
            UnansweredRequestsMtCCount = SelfOnly ? MtCCountSelf : MtCCountAll;

            Flash = _flashForSelfOnly
                ? MtMCountSelf > 0 || MtCCountSelf > 0
                : MtMCountAll > 0 || MtCCountAll > 0;
            
           // NewRequests = _info.NewRequestCount;
            //NewClientMessages = SelfOnly ? _info.NewClientMessagesSelf : _info.NewClientMessages;
            //NewManagerMessages = SelfOnly ? _info.NewManagerMessagesSelf : _info.NewManagerMessages;
        }

        public void SetCallback(MyControlEventHandlerSample buttonClickHandler)
        {
            OnButtonClick = buttonClickHandler;
            ButtonClickCommand = new RelayCommand(() =>
            {
                if (OnButtonClick != null) OnButtonClick.Invoke(this);
            });
        }

        public void SetCallback2(Action<int> callback)
        {
            _callback = callback;
            ShowAllCommand = new RelayCommand(() =>
            {
                if(_callback != null)
                    _callback.Invoke(1);
            });

            ShowOnlyMyCommand = new RelayCommand(() =>
            {
                if (_callback != null)
                    _callback.Invoke(2);
            });

            ShowAllCompanionsCommand = new RelayCommand(() =>
            {
                if (_callback != null)
                    _callback.Invoke(3);
            });

            ShowOnlyMyCompanionsCommand = new RelayCommand(() =>
            {
                if (_callback != null)
                    _callback.Invoke(4);
            });

            
            /*OnButtonClick = buttonClickHandler;
            ButtonClickCommand = new RelayCommand(() =>
            {
                if (OnButtonClick != null) OnButtonClick.Invoke(this);
            });*/
        }
    }
}
