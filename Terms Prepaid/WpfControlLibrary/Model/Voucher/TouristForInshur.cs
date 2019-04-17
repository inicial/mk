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

        public int ServiceCount;

    }
}