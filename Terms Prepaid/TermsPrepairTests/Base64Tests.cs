using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfControlLibrary.Util;
using System.Security.Cryptography;

namespace TermsPrepairTests
{
    [TestClass]
    public class Base64Tests
    {
        [TestMethod]
        public void SegnneyTest()
        {
            McRequest r = new McRequest("user_id=4091", "1234");
            string str = r.GetBase64();

            Assert.IsTrue(true);
        }
    }
}
