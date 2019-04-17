namespace WpfControlLibrary.Model.RequestJournal
{
    public class MailConfig : IMailConfig
    {
        public string Host  { get; set; }
        public string Address  { get; set; }
        public string Password  { get; set; }
        public int Port  { get; set; }
        public string SenderAddress { get; set; }
    }
}