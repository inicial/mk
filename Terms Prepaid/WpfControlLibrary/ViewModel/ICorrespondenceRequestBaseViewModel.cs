using System.Windows.Documents;

namespace WpfControlLibrary.ViewModel
{
    public interface ICorrespondenceRequestBaseViewModel : ICorrespondenceBaseViewModel
    {
        FlowDocument Document { get; set; }
        string ContentHtml { get; set; }
        void GetAttachment(string param);
        void RemoveAttachments();
    }
}