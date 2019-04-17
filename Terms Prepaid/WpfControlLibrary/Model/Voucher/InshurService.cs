using System;
using System.Collections.ObjectModel;
using System.Text;
using DataService;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
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

        public void InshurOk(bool isOk)
        {
            VoucherService.InshurOk(DgCode, isOk);
        }

        public string GetInshurStatus()
        {
            // 1. В работе
            // 2. Оформлена
            // 3. Опубликована в ЛК
            // 4. Аннулирована

            string status = "В работе";
            if (UralsibData == null) return status;
            if (UralsibData.Count == 0) return status;

            foreach (NameValue item in UralsibData)
            {
                if (item.Value.ToString().IndexOf("Выписана") >= 0)
                    return "Оформлена";
            }
            return status;
        }
    }
}
