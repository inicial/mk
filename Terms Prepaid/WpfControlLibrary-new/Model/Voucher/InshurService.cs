using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class TouristForInshur : Data
    {
        private string _firstName;
        private string _secondName;
        private string _inshurNumber;

        public string FirstName
        {
            get { return _firstName; }
            set { SetValue(ref _firstName, value); }
        }

        public string SecondName
        {
            get { return _secondName; }
            set { SetValue(ref _secondName, value); }
        }

        public string InshurNumber
        {
            get { return _inshurNumber; }
            set { SetValue(ref _inshurNumber, value); }
        }
    }

    public interface IInsurLoader
    {
        IEnumerable<NameValue> GetUralsibData(string dgCode);
        IEnumerable<TouristForInshur> GetTourists(string dgCode);
        bool GetCreatedStatus(int dlkey);
    }

    public class InsurLoader : IInsurLoader
    {
        IVoucherService _serv;

        public InsurLoader()
        {
            _serv = Repository.GetInstance<IVoucherService>();
        }

        public IEnumerable<NameValue> GetUralsibData(string dgCode)
        {
            return _serv.GetUralsibInshurs(dgCode)
                .Select()
                .Select(r => new NameValue(r.Field<string>("INS_Numder"), r.Field<bool>("INS_Status") ? "Выписана" : "Аннулирована"));
        }

        public IEnumerable<TouristForInshur> GetTourists(string dgCode)
        {
            return _serv.GetInshurs(dgCode).Select().Select(r => new TouristForInshur()
            {
                FirstName = r.Field<string>("TU_FNAMELAT"),
                SecondName = r.Field<string>("TU_NAMELAT"),
                InshurNumber = r.Field<string>("INS_Numder")
            });
        }

        public bool GetCreatedStatus(int dlkey)
        {
            return _serv.GetInshurCreatedStatus(dlkey);
        }
    }

    public class InshurService : Service
    {
        public static int Index = 0;

        public bool IsCreated
        {
            get { return _isCreated; }
            set { SetValue(ref _isCreated, value); }
        }

        private readonly IInsurLoader _loader;

        private ObservableCollection<NameValue> _uralsibData;
        private ObservableCollection<TouristForInshur> _tourists;
        private bool _isCreated;

        public ObservableCollection<NameValue> UralsibData
        {
            get { return _uralsibData; }
            set { SetValue(ref _uralsibData, value); }
        }

        public ObservableCollection<TouristForInshur> Tourists
        {
            get { return _tourists; }
            set { SetValue(ref _tourists, value); }
        }

        public InshurService(Service source, IInsurLoader loader = null) : base(source)
        {
            _loader = loader ?? Repository.GetInstance<IInsurLoader>();
            
            Index++;

            GetData();
            GetFullName(Index);
        }

        public void GetData()
        {
            UralsibData = new ObservableCollection<NameValue>(_loader.GetUralsibData(DgCode));
            Tourists = new ObservableCollection<TouristForInshur>(_loader.GetTourists(DgCode));
            IsCreated = _loader.GetCreatedStatus(DlKey);
        }

        public sealed override void GetFullName(int number)
        {
            FullName = String.Format("№{0} {1}", number, ServiceName);
        }
    }
}
