using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataService;
using terms_prepaid.Helpers;
using terms_prepaid.UserControls;
using WpfControlLibrary.Common;

namespace terms_prepaid.Forms
{
    public partial class frmUnhandledCorrespondence : Form
    {
        private Action<DataTable> _callback;
        private IUsersService _userService;
        private ComboBox _managers;

        public frmUnhandledCorrespondence(Action<DataTable> callback)
        {
            InitializeComponent();
            _callback = callback;
            _userService = Repository.GetInstance<IUsersService>();
            GetData();
        }

        private void GetData()
        {
            var dtAll = AddButton(tpAll, null);

            /*var dgCodes = dtAll.Select().Select(r => r.Field<string>("DG_CODE")).ToArray();
            var managers = WorkWithData.GetManagerWithUnhandledCorrespondence(dgCodes);*/
            
            var managers = dtAll.Select().Select(r => new { UsKey = r.Field<int>("responsible"), FullName = r.Field<string>("responsibleName") }).Distinct().ToArray(); 

            AddButton(tpMy, WorkWithData.GetUserID());

            AddManagers(managers);

            if ( managers.Length == 0)
                return;

            var usKey = managers.FirstOrDefault().UsKey;
            
            AddButton(tpManager, usKey, 21);
        }

        private DataTable AddButton(TabPage page, int? usKey, int y = 0)
        {
            //int count;
            const int widh = 470;
            const int heigth = 40;

            /*var dt = WorkWithData.GetUnhandledCorrespondence(out count, usKey);*/
            var dt = WorkWithData.GetUnhandledCorrespondence2(usKey);

            page.Controls.Add(new ucProblemButon(dt, page.Text, _callback)
            {
                Location = new Point(0, y),
                Size = new Size(widh, heigth)
            });

            return dt;
        }

        public static void GetUnhandledCorrespondence(Action<DataTable> callback)
        {
            var frm = new frmUnhandledCorrespondence(callback);
            frm.ShowDialog();
        }

        private void UpdateManagerTab()
        {
            const int yOffset = 0;

            var usKey = (int)_managers.SelectedValue;

            for (int i = tpManager.Controls.Count - 1; i > 0; i--)
                tpManager.Controls.RemoveAt(i);

            AddButton(tpManager, usKey, _managers.Height + yOffset);
        }

        private void AddManagers(object[] managers)
        {
            _managers = new ComboBox
            {
                DataSource = managers,
                ValueMember = "UsKey",
                DisplayMember = "FullName"
            };

            _managers.SelectedValueChanged += (sender, args) => UpdateManagerTab();
            tpManager.Controls.Add(_managers);
        }
    }
}
