using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xaml;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using Brush = System.Windows.Media.Brush;

namespace WpfControlLibrary.Model.Messages
{
    public enum CorrespondenceType
    {
        Manager = 1,
        Client = 2
    }

    public interface ICorrespondenceDocHeaders
    {
        string[] Headers { get; set; }
    }

    public class CorrespondenceDocHeadersBase : ICorrespondenceDocHeaders
    {
        public string[] Headers { get; set; }
    }

    public class CorrespondenceDocHeaders : CorrespondenceDocHeadersBase
    {
        public CorrespondenceDocHeaders()
        {
            Headers = new[] {"Дата", "Время", "Сообщение", "От", "Сотрудник"};
        }
    }

    public class CorrespondenceRequestDocHeaders : CorrespondenceDocHeadersBase
    {
        public CorrespondenceRequestDocHeaders()
        {
            Headers = new[] { "Дата", "Время", "Сообщение", "От", "Почта" };
        }
    }

    public interface ICorrespondenceLoader
    {
        ObservableCollection<MessageBlock> GetMessageBlock(string dgCode, CorrespondenceType type);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IRequestCorrespondenceLoader
    {
        ObservableCollection<MessageBlock> GetMessageBlockMtM(Request request);
        ObservableCollection<MessageBlock> GetMessageBlockMtC(Request request);
        ObservableCollection<MessageBlock> GetMessageBlock(Request request, string mod);
    }

    public class CorrespondenceLoaderBase
    {
        protected MessageBlock GetMessageBlock(IGrouping<DateTime, DataRow> group)
        {
            return new MessageBlock()
            {
                Date = group.Key,
                Messages = new ObservableCollection<Message>(group.Select(GetMessage)),
                Header = group.Key.ToString("dd.MM.yy")
            };
        }

        protected Message GetMessage(DataRow row)
        {
            return new Message
            {
                Date = row.Field<DateTime>("HI_DATE"),
                Autor = row.Field<String>("HI_WHO"),
                Text = row.Field<String>("HI_TEXT"),
                Mod = row.Field<String>("HI_MOD"),
                Role = row.Field<int>("isSuperviser") == 1 ? Message.Rolemember.Superviser :
                        row.Field<int>("isProductManagers") == 1 ? Message.Rolemember.Bronir :
                        row.Field<int>("isSalesManagers") == 1 ? Message.Rolemember.Manager :
                        row.Field<int>("isClient") == 1 ? Message.Rolemember.Client :
                        Message.Rolemember.Other
            };
        }
    }

    public class CorrespondenceLoader : CorrespondenceLoaderBase, ICorrespondenceLoader
    {
        public ObservableCollection<MessageBlock> GetMessageBlock(string dgCode, CorrespondenceType type)
        {
            var service = Repository.GetInstance<ICorrespondenceService>();

            return new ObservableCollection<MessageBlock>(service
                .GetCorrespondence(dgCode, type == CorrespondenceType.Manager ? RequestMessageMod.MTM : null)
                .Select()
                .GroupBy(r => r.Field<DateTime>("HI_DATE").Date)
                .Select(GetMessageBlock));
        }
    }

    public class RequestCorrespondenceLoader : CorrespondenceLoaderBase, IRequestCorrespondenceLoader
    {
        public ObservableCollection<MessageBlock> GetMessageBlockMtMOld(Request request)
        {
            var service = Repository.GetInstance<IRequestJournalService>();

            return new ObservableCollection<MessageBlock>(service
                .GetRequestCorrespondence(request.Number)
                .Select()
                .GroupBy(r => r.Field<DateTime>("HI_DATE").Date)
                .Select(GetMessageBlock));
        }

        public ObservableCollection<MessageBlock> GetMessageBlockMtM(Request request)
        {
            return new ObservableCollection<MessageBlock>(request.Messages.GroupBy(msg => msg.Date).Select(GetMessageBlock));
        }

        public ObservableCollection<MessageBlock> GetMessageBlockMtC(Request request)
        {
            return new ObservableCollection<MessageBlock>(request.Messages.GroupBy(msg => msg.Date).Select(GetMessageBlock));
        }

        public ObservableCollection<MessageBlock> GetMessageBlock(Request request, string mod)
        {
            return new ObservableCollection<MessageBlock>(request.Messages.GroupBy(msg => msg.Date).Select(g => GetMessageBlock(g, mod)));
        }

        private MessageBlock GetMessageBlock(IGrouping<DateTime, RequestMessage> group, string mod)
        {
            return new MessageBlock
            {
                Date = group.Key,
                Messages = new ObservableCollection<Message>(group.Where(m => m.Mod.Equals(mod, StringComparison.Ordinal)).Select(GetMessage)),
                Header = group.Key.ToString("dd.MM.yy")
            };
        }

        private MessageBlock GetMessageBlock(IGrouping<DateTime, RequestMessage> group)
        {
            return new MessageBlock()
            {
                Date = group.Key,
                Messages = new ObservableCollection<Message>(group.Select(GetMessage)),
                Header = group.Key.ToString("dd.MM.yy")
            };
        }

        private Message GetMessage(RequestMessage msg)
        {
            return new Message
            {
                Date = msg.Date,
                Autor = GetAutor(msg),
                Text = msg.Text,
                Html = msg.Html,
                Role = GetRole(msg),
                Attachments = msg.Attachments
            };
        }

        private string GetAutor(RequestMessage msg)
        {
            return msg.User != null ? String.Format("{0}\n[{1}]", msg.User.Name, msg.SenderAddress) : msg.SenderAddress;
        }

        private Message.Rolemember GetRole(RequestMessage msg)
        {
            return msg.User == null ? Message.Rolemember.Client : GetRole(msg.User);
        }

        private Message.Rolemember GetRole(User user)
        {
            return Message.Rolemember.Manager;
        }
    }

    public static class CorrespondenceText
    {
        public static string GetText(IEnumerable<MessageBlock> messageBlocks)
        {
            var sb = new StringBuilder();
            foreach (var block in messageBlocks)
            {
                sb.Append(block.Date.ToString("dd.MM.yy\n"));
                foreach (var m in block.Messages)
                {
                    sb.Append(string.Format("{0}:{1}:{2}\n", m.Autor, m.Date, m.Text));
                }
            }
            return sb.ToString();
        }
    }

    public interface ICorrespondenceBrushSelector
    {
        System.Windows.Media.Brush GetBrush(Message msg);
    }

    public class CorrespondenceBrushSelector : ICorrespondenceBrushSelector
    {
        public System.Windows.Media.Brush GetBrush(Message msg)
        {
            switch (msg.Role)
            {
                case Message.Rolemember.Superviser:
                    return System.Windows.Media.Brushes.SaddleBrown;

                case Message.Rolemember.Manager:
                    return System.Windows.Media.Brushes.Blue;

                case Message.Rolemember.Client:
                    return System.Windows.Media.Brushes.Indigo;

                default:
                    return System.Windows.Media.Brushes.Green;
            }
        }
    }

    public static class CorrespondenceDoc
    {
        private static readonly System.Windows.Media.FontFamily _font = new System.Windows.Media.FontFamily("Segoe UI");
        private static readonly System.Windows.Media.Brush _headerBrush = System.Windows.Media.Brushes.Green;

        public static FlowDocument GetDocument(IEnumerable<MessageBlock> messageBlocks)
        {
            var doc = new FlowDocument();
            foreach (var block in messageBlocks)
            {
                doc.Blocks.Add(GetParagraph(string.Format("{0}\t", block.Header), block.Messages[0]));

                foreach (var msg in block.Messages.Skip(1))
                    doc.Blocks.Add(GetParagraph("\t", msg));

            }

            return doc;
        }

        public static Paragraph GetParagraph(string header, Message msg)
        {
            var p = new Paragraph();
            p.Inlines.Add(GetRun(string.Format("{0}", header), _headerBrush));
            p.Inlines.Add(GetRun(string.Format("{0:HH:mm}  ", msg.Date), GetBrush(msg)));
            p.Inlines.Add(new Bold(GetRun(string.Format("{0}:  ", msg.Autor.Equals("www.mcruises.ru") ? "mcruises" : msg.Autor), GetBrush(msg))));
            p.Inlines.Add(GetRun(string.Format("{0}", msg.Text), GetBrush(msg)));
            p.TextAlignment = TextAlignment.Left;
            return p;
        }

        public static Run GetRun(string text, System.Windows.Media.Brush brush)
        {
            return new Run(text)
            {
                Foreground = brush,
                FontSize = 14,
                FontFamily = _font
            };
        }

        public static System.Windows.Media.Brush GetBrush(Message msg)
        {
            switch (msg.Role)
            {
                case Message.Rolemember.Superviser:
                    return System.Windows.Media.Brushes.SaddleBrown;

                case Message.Rolemember.Manager:
                    return System.Windows.Media.Brushes.Blue;

                case Message.Rolemember.Client:
                    return System.Windows.Media.Brushes.Indigo;

                default:
                    return System.Windows.Media.Brushes.Green;
            }
        }

        public static string GetAutorRole(Message msg)
        {
            switch (msg.Role)
            {
                case Message.Rolemember.Superviser:
                    return "Супервайзер";

                case Message.Rolemember.Manager:
                    return "Реализатор";

                case Message.Rolemember.Bronir:
                    return "Бронировщик";

                case Message.Rolemember.Client:
                    return "Клиент";

                default:
                    return "";
            }
        }
    }

    /// <summary>
    /// Абстрактная фабрика FlowDocument переписки
    /// </summary>
    public abstract class CorrespondenceTableBase
    {
        protected readonly System.Windows.Media.FontFamily _font = new System.Windows.Media.FontFamily("Segoe UI");
        protected readonly Brush _headerBrush = System.Windows.Media.Brushes.Green;

        public FlowDocument GetDocument(MessageBlock[] messageBlocks, CorrespondenceDocHeadersBase headers)
        {
            var doc = new FlowDocument();

            var t = new Table
            {
                CellSpacing = 0,
                BorderBrush = System.Windows.Media.Brushes.Gainsboro,
                BorderThickness = new Thickness(0.5)
            };

            t.Columns.Add(new TableColumn { Width = new GridLength(70) });
            t.Columns.Add(new TableColumn { Width = new GridLength(55) });
            t.Columns.Add(new TableColumn());
            t.Columns.Add(new TableColumn { Width = new GridLength(120) });
            t.Columns.Add(new TableColumn { Width = new GridLength(120) });

            var rg = new TableRowGroup();

            rg.Rows.Add(GetHeaders(headers));

            if (messageBlocks.Length > 0)
            {
                var block = messageBlocks;
                var blocks = messageBlocks.First();
                //block.Messages = new ObservableCollection<Message>(block.Messages.SkipWhile(m => m.Autor.Equals("www.mcruises.ru") && !m.Text.Contains("Комментарий к заказу")));
                blocks.Messages = new ObservableCollection<Message>(Filter(block));
            }

            foreach (var block in messageBlocks)
            {
                if (block.Messages.Count == 0)
                    continue;

                AddRow(rg, GetRow(string.Format("{0}", block.Header), block.Messages[0], block.Messages.Count));

                foreach (var msg in block.Messages.Skip(1))
                    AddRow(rg, GetRow("", msg, block.Messages.Count));
            }

            t.RowGroups.Add(rg);
            doc.Blocks.Add(t);

            return doc;
        }

        /* Фильтрация вывода сообщений по переписке в заказе */
        protected IEnumerable<Message> Filter(MessageBlock[] block)
        {
            var checkMessage = new Message();
            foreach (var blocks in block) {
                foreach (var msg in blocks.Messages)
                {
                    if (msg.Autor.Equals("www.mcruises.ru"))
                    {
                        if (msg.Text.Contains("Комментарий к заказу")) yield return checkMessage;
                    }
                    else
                    {
                        if (msg.Date.ToString() == checkMessage.Date.ToString())
                        {
                            checkMessage.Text = msg.Text + " " + checkMessage.Text;
                        }
                        else
                        {
                            checkMessage = msg;
                            yield return checkMessage;
                        }
                    }
                }
            }
        }

        protected TableRow GetHeaders(CorrespondenceDocHeadersBase headers)
        {
            TableRow row = new TableRow();

            Brush brush = System.Windows.Media.Brushes.Black;

            foreach (var header in headers.Headers)
            {
                row.Cells.Add(GetCell(header, brush));
            }

            row.Background = System.Windows.Media.Brushes.Gainsboro;

            return row;
        }

        protected void AddRow(TableRowGroup group, TableRow row)
        {
            group.Rows.Add(row);
            //if (!(firstBlock && autor.Equals("www.mcruises.ru"))) group.Rows.Add(row);
        }

        protected TableCell GetCell(string text, Brush brush)
        {
            return new TableCell(new Paragraph(CorrespondenceDoc.GetRun(text, brush)))
            {
                BorderBrush = System.Windows.Media.Brushes.Gray,
                BorderThickness = new Thickness(0.5),
                Padding = new Thickness(5)
            };
        }

        protected abstract TableRow GetRow(string header, Message msg, int messagesCount);
    }

    /// <summary>
    /// Фабрика FlowDocument переписки по договору
    /// </summary>
    public class CorrespondenceTable : CorrespondenceTableBase
    {
        protected override TableRow GetRow(string header, Message msg, int messagesCount)
        {
            TableRow row = new TableRow();

            Brush brush = CorrespondenceDoc.GetBrush(msg);

            string status = "тест";

            if (!header.Equals(string.Empty))
            {
                var headerCell = GetCell(string.Format("{0}", header), _headerBrush);
                headerCell.RowSpan = messagesCount;
                row.Cells.Add(headerCell);
            }

            row.Cells.Add(GetCell(string.Format("{0:HH:mm}  ", msg.Date), brush));
            row.Cells.Add(GetCell(string.Format("{0}", msg.Text), brush));
            row.Cells.Add(GetCell(string.Format("{0}", CorrespondenceDoc.GetAutorRole(msg)), brush));
            row.Cells.Add(GetCell(string.Format("{0}:  ", msg.Autor.Equals("www.mcruises.ru") ? "mcruises" : msg.Autor), brush));

            return row;
        }
    }

    /// <summary>
    /// Фабрика FlowDocument переписки по заявке
    /// </summary>
    public class CorrespondenceRequestTable : CorrespondenceTableBase
    {
        protected override TableRow GetRow(string header, Message msg, int messagesCount)
        {
            TableRow row = new TableRow();

            Brush brush = CorrespondenceDoc.GetBrush(msg);

            string status = "тест";

            if (!header.Equals(string.Empty))
            {
                var headerCell = GetCell(string.Format("{0}", header), _headerBrush);
                headerCell.RowSpan = messagesCount;
                row.Cells.Add(headerCell);
            }

            row.Cells.Add(GetCell(string.Format("{0:HH:mm}  ", msg.Date), brush));

            row.Cells.Add(msg.Html != null
                ? GetCellHtml(string.Format("{0}", msg.Html), brush)
                : GetCell(string.Format("{0:HH:mm}  ", msg.Text), brush));

            row.Cells.Add(GetCell(string.Format("{0}", CorrespondenceDoc.GetAutorRole(msg)), brush));
            row.Cells.Add(GetCell(string.Format("{0}:  ", msg.Autor.Equals("www.mcruises.ru") ? "mcruises" : msg.Autor), brush));

            return row;
        }

        protected TableCell GetCellHtml(string html, Brush brush)
        {
            var view = new HtmlControlView { DataContext = new HtmlControlViewModel { HtmlToDisplay = html } };

            return new TableCell(new Paragraph(new InlineUIContainer(view)))
            {
                BorderBrush = System.Windows.Media.Brushes.Gray,
                BorderThickness = new Thickness(0.5),
                Padding = new Thickness(5)
            };
        }
    }

    public class CorrespondenceBase : Data
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetValue(ref _text, value); }
        }

        public string Mod { get; set; }

        private ObservableCollection<MessageBlock> _messageBlocks;
        public ObservableCollection<MessageBlock> MessageBlocks
        {
            get { return _messageBlocks; }
            set { SetValue(ref _messageBlocks, value); }
        }

        private FlowDocument _doc;
        public FlowDocument Doc
        {
            get { return _doc; }
            set { SetValue(ref _doc, value); }
        }

        public Message GetLastMessage()
        {
            if (MessageBlocks == null)
                return null;

            var lastBlock = MessageBlocks.LastOrDefault();

            if (lastBlock == null)
                return null;

            return lastBlock.Messages == null ? null : lastBlock.Messages.LastOrDefault();
        }
    }

    public class Correspondence : CorrespondenceBase
    {
        private string _dgCode;
        public string DgCode
        {
            get { return _dgCode; }
            set { SetValue(ref _dgCode, value); }
        }

        public Correspondence(string dgCode, CorrespondenceType type, ICorrespondenceLoader correspondenceLoader = null)
        {
            var loader = correspondenceLoader ?? Repository.GetInstance<ICorrespondenceLoader>();
            MessageBlocks = loader.GetMessageBlock(dgCode, type);
            Doc = new CorrespondenceTable().GetDocument(MessageBlocks.ToArray(), new CorrespondenceDocHeaders());
            Mod = type == CorrespondenceType.Manager ? RequestMessageMod.MTM : RequestMessageMod.MTC;
            Repository.GetInstance<ICorrespondenceService>().CheckUnreadMessages(dgCode);
        }
    }

    public class RequestCorrespondenceBase : CorrespondenceBase
    {
        private int _requestId;
        public int RequestId
        {
            get { return _requestId; }
            set { SetValue(ref _requestId, value); }
        }
    }

    public class RequestCorrespondence : RequestCorrespondenceBase
    {
        public RequestCorrespondence(Request request, CorrespondenceType type = CorrespondenceType.Client, IRequestCorrespondenceLoader correspLoader = null)
        {
            var loader = correspLoader ?? Repository.GetInstance<IRequestCorrespondenceLoader>();
            MessageBlocks = loader.GetMessageBlockMtC(request);
            Doc = new CorrespondenceRequestTable().GetDocument(MessageBlocks.ToArray(), new CorrespondenceRequestDocHeaders());
            Mod = RequestMessageMod.MTC;
        }
    }

    public class RequestCorrespondenceMtM : CorrespondenceBase
    {
        public RequestCorrespondenceMtM(Request request, CorrespondenceType type = CorrespondenceType.Manager, IRequestCorrespondenceLoader correspLoader = null)
        {
            var loader = correspLoader ?? Repository.GetInstance<IRequestCorrespondenceLoader>();
            MessageBlocks = loader.GetMessageBlockMtM(request);
            Doc = new CorrespondenceRequestTable().GetDocument(MessageBlocks.ToArray(), new CorrespondenceDocHeaders());
            Mod = RequestMessageMod.MTM;
        }
    }

    public class RequestCorrespondence3 : RequestCorrespondenceBase
    {
        private ObservableCollection<string> _messages;
        public ObservableCollection<string> Messages
        {
            get { return _messages; }
            set { SetValue(ref _messages, value); }
        }

        public RequestCorrespondence3(Request request, CorrespondenceType type = CorrespondenceType.Client, IRequestCorrespondenceLoader correspLoader = null)
        {
            var loader = correspLoader ?? Repository.GetInstance<IRequestCorrespondenceLoader>();
            MessageBlocks = loader.GetMessageBlockMtC(request);
            Doc = new CorrespondenceRequestTable().GetDocument(MessageBlocks.ToArray(), new CorrespondenceRequestDocHeaders());
            Mod = RequestMessageMod.MTC;

            Messages = new ObservableCollection<string>(
                    new RequestCorrespondenceHtmlHelper().GetMessages(MessageBlocks.ToArray()));
        }
    }

    public class RequestCorrespondenceHtml : RequestCorrespondenceBase
    {
        private string _html;
        public string Html
        {
            get { return _html; }
            set { SetValue(ref _html, value); }
        }

        public RequestCorrespondenceHtml(Request request, CorrespondenceType type, IRequestCorrespondenceLoader correspLoader = null)
        {
            var loader = correspLoader ?? Repository.GetInstance<IRequestCorrespondenceLoader>();

            switch (type)
            {
                case CorrespondenceType.Client:
                    Mod = RequestMessageMod.MTC;
                    MessageBlocks = loader.GetMessageBlock(request, Mod);
                    break;

                case CorrespondenceType.Manager:
                    Mod = RequestMessageMod.MTM;
                    MessageBlocks = loader.GetMessageBlock(request, Mod);
                    break;

                default:
                    throw new Exception("unknown correspondence type");
            }

            Html = new RequestCorrespondenceTableCreator().GetHtml(MessageBlocks.ToArray());
        }
    }
}
