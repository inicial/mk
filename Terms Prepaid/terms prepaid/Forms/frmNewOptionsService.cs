using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.Common;


namespace terms_prepaid.Forms
{
    public partial class frmNewOptionsService : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        private int FormWidth; 
        private int FormHeight;

        public List<NameValue> _nameList; // коллекция значений
        private List<NameValue> _nameArray; // коллекция всех значений для формирования списка

        private string SelectedCode = "";
        private string SelectedName = "";
        private string ListTag = "";

        private bool NameClickFlag;    // флаг клика списка - для обработки при смене выбранной строки

        SelectServiceText ResponseFunc;  // функция для передачи выбранных данных в главную форму


        public frmNewOptionsService(int iX, int iY, List<NameValue> lstCruise, string iName, string iTag, SelectServiceText iResponse)
        {
            StartPosX = iX;
            StartPosY = iY;

            _nameList = new List<NameValue>();
            _nameArray = lstCruise;

            SelectedName = iName;
            ListTag = iTag;

            ResponseFunc = iResponse;


            InitializeComponent();
        }

        private void frmNewOptionsService_Load(object sender, EventArgs e)
        {
            this.Left = StartPosX;
            this.Top = StartPosY;

            FormWidth = this.Width;
            FormHeight = this.Height;

            Accord_Name_List();

            int ch = lst_Name.Height;
            int lh = 13; 
            int h = 1 * lh;
            int count = 1;
            if (_nameList != null)
                if (_nameList.Count > 1)
                    count = _nameList.Count;
            if (count > 10) count = 10;
            h = count * lh;
            int dh = (h + 4) - ch;
            this.Height = this.Height + dh;
            //lst_Name.Height = lst_Name.Height + dh;
        }

        private void Accord_Name_List()
        {
            _nameList.Clear();
            int selected_index = -1;
            int i = 0;
            //_nameList.Add(0, new NameValue(0, "нет значения"));
            foreach (NameValue name in _nameArray)
            {
                _nameList.Add(new NameValue(name.Name.ToString(), name.Value));
                if ((string)(name.Value) == SelectedName) selected_index = i;
                if (((string)(name.Value)).IndexOf(SelectedName) >= 0) selected_index = i;
                if (SelectedName.IndexOf((string)(name.Value)) >= 0) selected_index = i;
                i++;
            }
            _nameList.Add(new NameValue("new", "ввести значение..."));

            lst_Name.DataSource = null;
            lst_Name.DataSource = _nameList;
            lst_Name.SelectedIndex = 0;
            if (selected_index > 0 && selected_index < lst_Name.Items.Count) lst_Name.SelectedIndex = selected_index;
        }

        private void frmNewOptionsService_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Select_Name()
        {
            string ServiceText = SelectedName;
            SelectServiceText func = ResponseFunc;

            if (SelectedCode == "new") ServiceText = "INSERT";

            this.Close();


            if (func != null) func(ServiceText, ListTag);
        }


        private void lst_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedName = "";
            int i = lst_Name.SelectedIndex;
            if (i >= 0 && i < _nameList.Count)
            {
                SelectedName = (string)_nameList[i].Value;
                SelectedCode = (string)_nameList[i].Name;
            }

            if (NameClickFlag) Select_Name();
            NameClickFlag = false;
        }

        private void lst_Name_MouseClick(object sender, EventArgs e)
        {
            NameClickFlag = true;
        }

        private void frmNewOptionsService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                Select_Name();
            }
            if (e.KeyCode == Keys.Space)
            {
                //Select_Name();
            }
        }
    }
}
