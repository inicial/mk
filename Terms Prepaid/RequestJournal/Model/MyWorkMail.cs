namespace WpfControlLibrary.Model.RequestJournal
{
    public class MyWorkMail : MailConfig
    {
        public MyWorkMail()
        {
            Address = "test_zaiav@mcruises.ru";
            Password = "123456789";
            Host = "mail.mcruises.ru";
            Port = 143;
        }
    }
}