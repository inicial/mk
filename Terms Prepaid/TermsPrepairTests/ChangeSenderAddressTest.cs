using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfControlLibrary.Helpers;

namespace TermsPrepairTests
{
    [TestClass]
    public class ChangeSenderAddressTest
    {
        [TestMethod]
        public void SenderAddressTest()
        {
            var helper = new MailAddressHelper();
            var mail = helper.GetMailUrl("mailto:shchekotkov@gmail.com");
        }
    }
}
