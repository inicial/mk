namespace WpfControlLibrary.Model.RequestJournal
{
    public class MyWorkMail : MailConfig
    {
        public MyWorkMail()
        {
            Address = "mrequest_baza@mcruises.ru";
            Password = "super123";
            Host = "mail.mcruises.ru";
            Port = 143;
            SenderAddress = "mrequest@mcruises.ru";
        }
    }

    public class MyWorkMailTest : MailConfig
    {
        public MyWorkMailTest()
        {
            Address = "test_zaiav@mcruises.ru";
            Password = "123456789";
            Host = "mail.mcruises.ru";
            Port = 143;
            SenderAddress = "mrequest@mcruises.ru";
        }
    }
}