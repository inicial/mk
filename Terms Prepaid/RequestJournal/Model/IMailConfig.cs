namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// ������� �����
    /// </summary>
    public interface IMailConfig
    {
        string Host { get; set; }
        string Address  { get; set; }
        string Password  { get; set; }
        int Port  { get; set; }
    }
}