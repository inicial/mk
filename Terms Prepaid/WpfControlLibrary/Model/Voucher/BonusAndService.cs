using System;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class BonusAndService : Data, ICloneable
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private bool _isRight;
        public bool IsRight
        {
            get { return _isRight; }
            set { SetValue(ref _isRight, value); }
        }

        private bool _textChanged;
        public bool TextChanged
        {
            get { return _textChanged; }
            set { SetValue(ref _textChanged, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                SetValue(ref _text, value);
                TextChanged = true;
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }

        private bool _isAdded;
        public bool IsAdded
        {
            get { return _isAdded; }
            set { SetValue(ref _isAdded, value); }
        }

        private bool _isToInsert;
        public bool IsToInsert
        {
            get { return IsAdded && !string.IsNullOrEmpty(Text); }
        }

        private bool _isEditable;
        public bool IsEditable
        {
            get { return _isEditable; }
            set { SetValue(ref _isEditable, value); }
        }

        private bool _hasList;
        public bool HasList
        {
            get { return _hasList; }
            set { SetValue(ref _hasList, value); }
        }

        public BonusAndService()
        {
            
        }

        public BonusAndService(int id, string text, bool isRight, string name, bool isAdded, bool isEditable, bool hasList)
        {
            Id = id;
            Text = text;
            IsRight = isRight;
            Name = name;
            IsAdded = isAdded;
            IsEditable = isEditable;
            HasList = hasList;
            TextChanged = false;
        }

        public object Clone()
        {
            return new BonusAndService(Id, Text, IsRight, Name, IsAdded, IsEditable, HasList);
        }
    }
}