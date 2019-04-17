using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// ��� �������
    /// </summary>
    public class RequestType : Data
    {
        private string _value;
        public string Value
        {
            get { return _value; }
            set { SetValue(ref _value, value); }
        }

        public static RequestType River { get { return new RequestType("������ �� ������ ������� ������"); } }
        public static RequestType Sea { get { return new RequestType("������ �� ������ �������� ������"); } }

        public RequestType(string value) { Value = value; }
    }
}