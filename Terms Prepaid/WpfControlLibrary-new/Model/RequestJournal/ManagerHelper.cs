using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataService;
using Org.BouncyCastle.Bcpg.OpenPgp;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class ManagerHelper
    {
        private static ManagerHelper _instance;
        public static ManagerHelper Instance
        {
            get { return _instance ?? (_instance = new ManagerHelper()); }
        }
        
        public IEnumerable<User> Managers { get; private set; }

        private ManagerHelper()
        {
            var userService = Repository.GetInstance<IUsersService>();
           
            Managers = new ObservableCollection<User>(userService.GetManagerList()
                .Select()
                .Select(row => new UserFactoryTest().GetUser(row)));
        }

        public User GetManager(IEnumerable<User> managers, int? usKey, string eMail)
        {
            return usKey != null ? managers.FirstOrDefault(m => m.Key == (int)usKey) : managers.FirstOrDefault(m => m.Email == eMail);
        }
    }
}