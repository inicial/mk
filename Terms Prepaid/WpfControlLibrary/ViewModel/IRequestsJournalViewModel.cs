namespace WpfControlLibrary.ViewModel
{
    public interface IRequestsJournalViewModel
    {
        void ChangeSenderAddress(string newSenderAddress);
        void ShowDataGrid();

        void ShowNewRequests();
        void ShowAllRequests();
        void ShowAllMyRequests();
        void ShowAllCompanions();
        void ShowAllMyCompanions();
    }
}