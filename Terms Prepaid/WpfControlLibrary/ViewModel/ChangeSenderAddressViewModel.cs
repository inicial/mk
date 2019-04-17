using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.ViewModel
{
    /// <summary>
    /// ChangeSenderAddressViewModel with using IRequestJournalService
    /// </summary>
    public class ChangeSenderAddressViewModel : ChangeSenderAddressBaseViewModel
    {
        private readonly RequestMessage _message;
        private readonly IRequestJournalService _service;
        
        public ChangeSenderAddressViewModel(RequestMessage message, string newSenderAddress = null)
        {
            _message = message;
            _service = Repository.GetInstance<IRequestJournalService>();
            if (message != null) SenderAddress = newSenderAddress ?? message.SenderAddress;
            
        }

        public override void SaveChanges()
        {
            _service.ChangeSenderAddress(_message.Id, SenderAddress);
        }
    }
}
