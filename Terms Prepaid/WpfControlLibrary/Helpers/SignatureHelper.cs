using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Helpers
{
    public class SignatureNotFoundException : Exception
    {
        public int UsKey { get; private set; }

        public SignatureNotFoundException(int usKey) : base(string.Format("Подпись не найдена usKey={0}", usKey))
        {
            UsKey = usKey;
        }
    }

    public class SignatureInfo
    {
        public Dictionary<string, string> Info { get; set; }

        public SignatureInfo()
        {
            Info = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            Info.Add(key, value);
        }
    }

    public class SignatureInfoFactory
    {
        private IUsersService _service;

        public SignatureInfoFactory()
        {
            _service = Repository.GetInstance<IUsersService>();
        }

        public SignatureInfo GetFromUser(User user)
        {
            var info = new SignatureInfo();
            info.Add("%USERNAME_RU%", string.Format("{0} {1} {2}", user.FName, user.SName, user.MName));
            info.Add("%USERNAME_EN%", "");
            info.Add("%MAIL%", user.Email);
            info.Add("%POSITION%", user.Job);
            info.Add("%DEPARTAMENT%", user.Departament);
            info.Add("%TELEPHONE%", Convert.ToString(user.Phone));
            info.Add("%DMAIL%", "info@mruises.ru");
            return info;
        }

        public SignatureInfo GetFromUsKey(int usKey)
        {
            var row = _service.GetUserSignature(usKey);

            if(row == null)
                throw new SignatureNotFoundException(usKey);

            var info = new SignatureInfo();
            info.Add("%USERNAME_RU%", row.Field<string>("USERNAME_RU"));
            info.Add("%USERNAME_EN%", row.Field<string>("USERNAME_RU"));
            info.Add("%MAIL%", row.Field<string>("EMAIL"));
            info.Add("%POSITION%", row.Field<string>("POSITION"));

            if (string.IsNullOrEmpty(row.Field<string>("DEPARTAMENT"))) 
                info.Add("%DEPARTAMENT%", "");
            else
                info.Add("%DEPARTAMENT%", row.Field<string>("DEPARTAMENT") + "<br>");

            info.Add("%TELEPHONE%", row.Field<string>("PHONE"));
            info.Add("%DMAIL%", row.Field<string>("DEPARTAMENT_EMAIL"));
            return info;
        }
    }

    public class SignatureHelper
    {
        public SignatureInfo GetTestInfo()
        {
            var info = new SignatureInfo();
            info.Add("%USERNAME_RU%", "Щекотков Сергей Владимирович");
            info.Add("%USERNAME_EN%", "Sergey Chekotkov");
            info.Add("%MAIL%", "s.chekotkov@mcruises.ru");
            info.Add("%POSITION%", "Специалист");
            info.Add("%DEPARTAMENT%", "Отдел IT");
            info.Add("%TELEPHONE%", "405");
            info.Add("%DMAIL%", "");
            return info;
        }

        public string LoadTemplate(string patch)
        {
            if (!File.Exists(patch))
                throw new FileNotFoundException("File not found", patch);
            
            return File.ReadAllText(patch);
        }

        public string GetSignature(User user, string templatePatch = "Resources\\Templates\\rus-v2.html")
        {
            try
            {
                var info = new SignatureInfoFactory().GetFromUsKey(user.Key);
                var template = LoadTemplate(templatePatch);
                return info.Info.Aggregate(template, (current, row) => current.Replace(row.Key, row.Value));
            }
            catch (SignatureNotFoundException e)
            {
                //Repository.GetInstance<IWindowsHelper>().ShowMessage(e.Message, "Ваша подпись не найдена");
                return string.Empty;
            }
        }

        public string Test()
        {
            return null;
        }
    }
}
