using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Messages;

namespace WpfControlLibrary.ViewModel
{
    public class CorrespondenceTabViewModel : CorrespondenceBaseViewModel
    {
        private string _dgCode;

        private const int MAX_SYMBOLS = 252;
        
       // Constructor
        public CorrespondenceTabViewModel(string dgCode, CorrespondenceType type, int maxSymbols = MAX_SYMBOLS, ICorrespondenceService service = null)
            : base(service)
        {
            _maxSymbols = maxSymbols;
            SetCorrespondence(dgCode, type);
        }

        public void SetCorrespondence(string dgCode, CorrespondenceType type)
        {
            _dgCode = dgCode;
            _type = type;
            UpdateCorrespodence();
            //CloseCorrespVisible = type == CorrespondenceType.Client;
            CloseCorrespVisible = true;
            ButtonStyle = (int)type;
        }

        protected override void UpdateCorrespodence()
        {
            Corresp = new Correspondence(_dgCode, _type);
            CloseCorresp = GetCorresponseClosedStatus();
        }

        protected override void Send(string msg)
        {
            if (msg == null || msg.Equals(string.Empty))
                return;

            var lbuff = new List<string>();
            var sb = new StringBuilder();
            var words = msg.Split(' ');

            foreach (var word in words)
            {
                if (sb.Length + word.Length > _maxSymbols)
                {
                    if (sb.Length > 0)
                    {
                        lbuff.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                    if (word.Length > _maxSymbols)
                    {
                        lbuff.AddRange(SplitString(word, _maxSymbols));
                        sb = new StringBuilder();
                        continue;
                    }
                }

                if (sb.Length > 0) 
                    sb.Append(" ");
                
                sb.Append(word);
            }

            if (sb.Length > 0) 
                lbuff.Add(sb.ToString());

            foreach (string s in lbuff)
                _service.InsertHistory2(((Correspondence)Corresp).DgCode, s, Corresp.Mod, "");

            NewMessage = "";

            UpdateCorrespodence();
        }
        
    }
}
