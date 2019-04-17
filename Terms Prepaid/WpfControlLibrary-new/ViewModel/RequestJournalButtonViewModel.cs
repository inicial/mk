using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public RelayCommand ButtonClickCommand { get; set; }

        public RequestJournalButtonViewModel()
        {
            _flashForSelfOnly = !Repository.GetInstance<IAccessService>().isSuperViser;
            _usKey = Repository.GetInstance<IUsersService>().GetUserID2();
            _unansweredRequestsService = Repository.GetInstance<IUnansweredRequestsService>();
        }

        public void Update()
        {
            //_info = RequestJournalHelper.GetNewMessagesCount();

            MtMCountSelf = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTM, _usKey);
            MtMCountAll = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTM);

            MtCCountSelf = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTC, _usKey);
            MtCCountAll = _unansweredRequestsService.GetUnansweredRequestsCount(RequestMessageMod.MTC);
            
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
    }
}
