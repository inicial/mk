using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using DataService;
using GalaSoft.MvvmLight.Messaging;
using WpfControlLibrary.Common;
using WpfControlLibrary.Messages;
using WpfControlLibrary.Model.Flight;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.ViewModel
{
    public class MilesAirlineListViewModel : ViewModelBase, IMilesAirlineListViewModel
    {
        private string _skyTeamText;
        public string SkyTeamText
        {
            get { return _skyTeamText; }
            set { SetValue(ref _skyTeamText, value); }
        }

        private string _oneWorldText;
        public string OneWorldText
        {
            get { return _oneWorldText; }
            set { SetValue(ref _oneWorldText, value); }
        }

        private string _starAllianceText;
        public string StarAllianceText
        {
            get { return _starAllianceText; }
            set { SetValue(ref _starAllianceText, value); }
        }

        public MilesAirlineListViewModel()
        {
            Init();
            //Messenger.Default.Register<DlKeyInfo>(this, HandleDgCodeMessage);
        }

        public void HandleDgCodeMessage(DlKeyInfo info)
        {
            //SkyTeamText = info.DlKey.ToString();
        }

        private void Init()
        {
            var serv = Repository.GetInstance<IAviaCardsService>();

            var oneWorldList = serv.GetOneWorld()
                .Select()
                .Select(s => new { Subsidiary = s.Field<string>("Subsidiary"), Company = s.Field<string>("Company") })
                .GroupBy(c => c.Company)
                .Select(g => String.Format("{0} ({1})", g.Key, string.Join(", ", g.Select(c => c.Subsidiary).ToList())).Replace("()", ""));
            OneWorldText = string.Join(Environment.NewLine, oneWorldList.ToArray());

            var skyTeamList = serv.GetSkyTeam()
                .Select()
                .Select(s => s.Field<string>("Company"));
            SkyTeamText = string.Join(Environment.NewLine, skyTeamList.ToArray());

            var starAllianceTeamList = serv.GetStarAliance()
                .Select()
                .Select(s => s.Field<string>("Company"));
            StarAllianceText = string.Join(Environment.NewLine, starAllianceTeamList.ToArray());
        }
    }
}
