using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfControlLibrary.Model.RequestJournal;

namespace TermsPrepairTests
{
    [TestClass]
    public class InlineImgHelperTest
    {
        private readonly string message = "<Img src=\"file:///D:/Test2/accept-icon.jpg\"/> <erserse></erserse>";
        private readonly string testSrc = "D:/Test2/accept-icon.jpg";

        [TestMethod]
        public void TestInlineImgHelper()
        {
            //string pattern = "<IMG[^>]*src='file:///([^']*)'";

            string pattern = "<IMG[^>]*src=\"file:///([^']*)\"";
            
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(message);
            
            Assert.IsTrue(match.Success && match.Groups[1].Value.Equals(testSrc));
        }
    }
}
