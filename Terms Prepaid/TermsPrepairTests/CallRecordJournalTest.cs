using System;
using System.Globalization;
using System.Text;
using DataService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using terms_prepaid.Helpers;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.CallRecordJournal;

namespace TermsPrepairTests
{
    [TestClass]
    public class CallRecordJournalTest
    {
        [TestMethod]
        public void TestCallRecordCreator()
        {
            DbAdapter.Init();
            var creator = new CallRecordCreator();

            var filter1 = new CallRecordFilterCreator().GetFilter(new DateTime(2017, 1, 1), new DateTime(2017, 1, 19), null);
            var testCount1 = 118;
            var records1 = creator.GetRecords(filter1);
            //InfoHelper.GetInfo(records1);

            var filter2 = new CallRecordFilterCreator().GetFilter(new DateTime(2017, 1, 1), new DateTime(2017, 1, 19), 2);
            var testCount2 = 98;
            var records2 = creator.GetRecords(filter2);
            //InfoHelper.GetInfo(records2);

            var filter3 = new CallRecordFilterCreator().GetFilter(null, new DateTime(2017, 1, 19), 3);
            var testCount3 = 314;
            var records3 = creator.GetRecords(filter3);
            //InfoHelper.GetInfo(records3);

            Assert.IsTrue(records1.Length == testCount1 && records2.Length == testCount2 && records3.Length == testCount3);
        }
    }
}
