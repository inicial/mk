using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class CollectingLowcostAirlinesViewModel : ViewModelBase, ICollectingLowcostAirlinesViewModel
    {
        //ICollectingLowcostAirlinesService
        private DataTable _table;
        private DataTable _notes;
        private ObservableCollection<string> _noteStrings;
        private ObservableCollection<CollectingInfo> _info;

        public class CollectingInfo : Data
        {
            private string _airline;
            private string _chekIn;
            private string _seatSelection;
            private string _freeCarryOnBaggage;
            private string _baggage;
            private string _food;

            public String Airline
            {
                get { return _airline; }
                set { SetValue(ref _airline, value); }
            }

            public String Check_in
            {
                get { return _chekIn; }
                set { SetValue(ref _chekIn, value); }
            }

            public String SeatSelection
            {
                get { return _seatSelection; }
                set { SetValue(ref _seatSelection, value); }
            }

            public String FreeCarryOnBaggage
            {
                get { return _freeCarryOnBaggage; }
                set { SetValue(ref _freeCarryOnBaggage, value); }
            }

            public String Baggage
            {
                get { return _baggage; }
                set { SetValue(ref _baggage, value); }
            }

            public String Food
            {
                get { return _food; }
                set { SetValue(ref _food, value); }
            }
        }

        public DataTable Table
        {
            get { return _table; }
            set { SetValue(ref _table, value); }
        }

        public DataTable Notes
        {
            get { return _notes; }
            set { SetValue(ref _notes, value); }
        }

        public ObservableCollection<CollectingInfo> Info
        {
            get { return _info; }
            set { SetValue(ref _info, value); }
        }

        public ObservableCollection<string> NoteStrings
        {
            get { return _noteStrings; }
            set { SetValue(ref _noteStrings, value); }
        }

        public CollectingLowcostAirlinesViewModel()
        {
            Init();
        }

        private void Init()
        {
            var serv = Repository.GetInstance<ICollectingLowcostAirlinesService>();

            Table = serv.GetCollectingLowcostAirlinesTable();
            Notes = serv.GetCollectingLowcostAirlinesNotes();

            Info = new ObservableCollection<CollectingInfo>(Table.Select().Select(s => new CollectingInfo()
            {
                Airline = s.Field<string>("Airline"),
                Baggage = s.Field<string>("Baggage"),
                Check_in = s.Field<string>("Check_in"),
                Food = s.Field<string>("Food"),
                FreeCarryOnBaggage = s.Field<string>("FreeCarryOnBaggage"),
                SeatSelection = s.Field<string>("FreeCarryOnBaggage")
            }));

            NoteStrings = new ObservableCollection<string>(Notes.Select().Select(s =>
                string.Format("{0} - {1}", s.Field<string>("Note"), s.Field<string>("Text"))
            ));
        }
    }
}
