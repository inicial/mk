using System;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Подстатус заявки
    /// </summary>
    public class RequestSubStatus : RequestStatus
    {
        private int _requestStatusId;
        public int RequestStatusId
        {
            get { return _requestStatusId; }
            set { SetValue(ref _requestStatusId, value); }
        }

        private bool _alarm;
        public bool Alarm
        {
            get { return _alarm; }
            set { SetValue(ref _alarm, value); }
        }

        private byte _colorIndex;
        public byte ColorIndex
        {
            get { return _colorIndex; }
            set { SetValue(ref _colorIndex, value); }
        }

        public RequestSubStatus(int id, int requestStatusId, string name, DateTime date, byte colorIndex = 0, bool alarm = false)
            : base(id, name, date)
        {
            RequestStatusId = requestStatusId;
            ColorIndex = colorIndex;
            Alarm = alarm;
        }
    }
}