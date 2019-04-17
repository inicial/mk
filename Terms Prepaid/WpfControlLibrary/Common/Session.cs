using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataService;

namespace WpfControlLibrary.Common
{
    public class Session : ISession
    {
        private static Session _instance;

        public bool IsSuperviser { get; private set; }
        public int UsKey { get; private set; }

        private Session()
        {
            var accessService = Repository.GetInstance<IAccessService>();
            var userService = Repository.GetInstance<IUsersService>();

            UsKey = userService.GetUsKey();
            IsSuperviser = accessService.isSuperViser;
        }

        public static ISession GetInstance()
        {
            return _instance ?? (_instance = new Session());
        }

        
    }
}
