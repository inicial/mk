namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Конфиги почты
    /// </summary>
    public interface IMailConfig
    {
        string Host { get; set; }
        string Address  { get; set; }
        string Password  { get; set; }
        int Port  { get; set; }
        string SenderAddress { get; set; }
    }
}