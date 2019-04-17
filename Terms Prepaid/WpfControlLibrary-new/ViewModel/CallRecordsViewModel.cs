using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.UI;
using DataService;
using GalaSoft.MvvmLight.Command;
using Gecko.WebIDL;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.CallRecordJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public abstract class CallRecordsViewModelBase : ViewModelBase
    {
        public RelayCommand ClearFilterCmd { get; protected set; }
        public RelayCommand UpdateCmd { get; protected set; }

        public RelayCommand<ICallRecordBase> GoToUrlCmd { get; protected set; }
        public RelayCommand<ICallRecordBase> ListenCmd { get; protected set; }
        public RelayCommand<ICallRecordBase> RecordCmd { get; protected set; }

        private ObservableCollection<ICallRecordBase> _callRecords;
        public ObservableCollection<ICallRecordBase> CallRecords
        {
            get { return _callRecords; }
            set { SetValue(ref _callRecords, value); }
        }

        private ICallRecordBase _selectedItem;
        public ICallRecordBase SelectedItem
        {
            get { return _selectedItem; }
            set { SetValue(ref _selectedItem, value); }
        }

        private DateFilter _dateFilter;
        public DateFilter DateFilter
        {
            get { return _dateFilter; }
            set { SetValue(ref _dateFilter, value); }
        }

        protected CallRecordsViewModelBase()
        {
            GoToUrlCmd = new RelayCommand<ICallRecordBase>(GoToUrl);
            ListenCmd = new RelayCommand<ICallRecordBase>(Listen);
            RecordCmd = new RelayCommand<ICallRecordBase>(Record);

            ClearFilterCmd = new RelayCommand(ClearFilter);
            UpdateCmd = new RelayCommand(Update);
        }

        protected abstract void GoToUrl(ICallRecordBase callRecord);
        protected abstract void Listen(ICallRecordBase callRecord);
        protected abstract void Record(ICallRecordBase callRecord);

        public abstract void Update();
        public abstract void ClearFilter();
    }

    public class CallRecordsMkViewModel : CallRecordsViewModelBase
    {
        private ComboBoxFilter<mk_CallRecordsStatusFilter> _statusFilter;
        public ComboBoxFilter<mk_CallRecordsStatusFilter> StatusFilter
        {
            get { return _statusFilter; }
            set { SetValue(ref _statusFilter, value); }
        }

        private List<mk_CallRecordStatus> _statuses;

        public CallRecordsMkViewModel()
        {
            var db = Repo<CallRecordsContext>.GetDb();

            DateFilter = new DateFilter(DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(1))
            {
                Name = "по времени звонка",
                Callback = Update
            };

            StatusFilter = new ComboBoxFilter<mk_CallRecordsStatusFilter>()
            {
                Name = "По статусу",
                Values = new ObservableCollection<mk_CallRecordsStatusFilter>(db.mk_CallRecordsStatusFilters.AsQueryable()),
                Callback = Update
            };

            _statuses = db.mk_CallRecordStatuses.AsQueryable().ToList();

            Update();
        }

        public override void Update()
        {
            var db = Repo<CallRecordsContext>.GetDb();

            var callRecords = db.mk_CallRecords.AsQueryable().Where(c => c.Date > DateFilter.DateBeginForQuery && c.Date < DateFilter.DateEndForQuery);
            if (StatusFilter.Value != null && StatusFilter.Value.Id != -1) callRecords = callRecords.Where(c => c.StatusId == StatusFilter.Value.Id);
            CallRecords = new ObservableCollection<ICallRecordBase>(callRecords);

            foreach (var r in CallRecords.Cast<mk_CallRecord>())
                r.Status = _statuses.Find(s => s.Id == r.StatusId);
        }

        public override void ClearFilter()
        {

        }

        protected override void GoToUrl(ICallRecordBase callRecord)
        {
            var record = callRecord as mk_CallRecord;
            if (record == null)
                return;

            if (record.PlaceOnline != null)
                System.Diagnostics.Process.Start(record.PlaceOnline);
        }

        protected override void Listen(ICallRecordBase callRecord)
        {
            
        }

        protected override void Record(ICallRecordBase callRecord)
        {
            
        }
    }

    public class CallRecordsViewModel : CallRecordsViewModelBase
    {
        private CallRecordFilter _filter;
        public CallRecordFilter Filter
        {
            get { return _filter; }
            set { SetValue(ref _filter, value); }
        }

        public CallRecordsViewModel()
        {
            DateFilter = new DateFilter(DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(1))
            {
                Name = "по времени звонка",
                Callback = Update
            };

            Filter = new CallRecordFilterCreator().GetFilter(null, null, null);

            Update();
        }

        public override void Update()
        {
            var creator = new CallRecordCreator();
            CallRecords = new ObservableCollection<ICallRecordBase>(creator.GetRecords(Filter));
        }

        public override void ClearFilter()
        {
            Filter.Set(null, null, -1);
        }

        protected override void GoToUrl(ICallRecordBase callRecord)
        {
            var record = callRecord as ICallRecord;
            if (record == null)
                return;

            if (record.RingUrl != null)
                System.Diagnostics.Process.Start(record.RingUrl);
        }

        protected override void Listen(ICallRecordBase callRecord)
        {

        }

        protected override void Record(ICallRecordBase callRecord)
        {

        }
    }
}
