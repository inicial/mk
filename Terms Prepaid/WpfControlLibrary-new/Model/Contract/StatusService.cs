using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Contract
{
    public class StatusService
    {
        private IContractService _service;
        private DataRow _rowDogovor;

        public StatusService(IContractService service, DataRow rowDogovor)
        {
            _service = service;
            _rowDogovor = rowDogovor;
        }

        public Status GetStatus(string dgcode)
        {
            DateTime date = _service.GetDateForStatus(dgcode, _rowDogovor.Field<string>("NS_QUERYFORDATE"));
            string lbStatus = _rowDogovor.Field<string>("NS_Name") + " " + date.ToString("dd.MM.yy HH:mm");
            string lbStatusD= (_rowDogovor["StatusWHY"] == DBNull.Value ? "" : 
                _rowDogovor.Field<DateTime>("StatusWHY").ToString("dd.MM.yy HH:mm"));

            string lbStatusM = _rowDogovor.Field<string>("StatusWHO");

            DataTable statuses = _service.GetStatuses();

            return new Status()
                {
                    Date = date,
                    LbStatus = lbStatus,
                    LbStatusD = lbStatusD,
                    LbStatusM = lbStatusM
                };
        }
    }
}
