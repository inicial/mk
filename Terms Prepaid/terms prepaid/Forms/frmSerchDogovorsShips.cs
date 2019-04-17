using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Helpers;


namespace terms_prepaid.Forms
{
    public partial class frmSerchDogovorsShips : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        public List<users> _cruiseList; // коллекция круизных компаний для отображения в списке, формируется в Accord_Cruise_List
        public List<users> _shipList;   // коллекция лайнеров для отображения в списке, формируется в Accord_Ship_List
        private List<cruise_record> _cruiseArray; // коллекция всех круизных компаний для формирования списка
        private List<ship_record> _shipArray;     // коллекция всех лайнеров для формирования списка
        private List<users> _settingsArray;       // коллекция опций для круизных компаний (морские, речные и т.п.)

        private bool flgSee;    // флаг отбора морских круизных компаний
        private bool flgRiver;  // флаг отбора речных круизных компаний

        private int SelectedCruise;   // выбранная круизная компания - id  
        private int SelectedShip;     // выбранный лайнер - id
        private int CurrentShip;     // текущий лайнер (выбранная строка) - id

        private bool CruiseClickFlag;  // флаг клика списка круизных компаний - для обработки при смене выбранной строки
        private bool ShipClickFlag;    // флаг клика списка лайнеров - для обработки при смене выбранной строки

        SelectShipFiltr ResponseFunc;  // функция для передачи выбранных данных в главную форму

        public frmSerchDogovorsShips(int iX, int iY, List<cruise_record> lstCruise, List<ship_record> lstShip, List<users> lstSettings, SelectShipFiltr iResponse, bool bSee, bool bRiver, int iCruise, int iShip)
        {
            StartPosX = iX;
            StartPosY = iY;

            _cruiseList = new List<users>();
            _shipList = new List<users>();
            _cruiseArray = lstCruise;
            _shipArray = lstShip;
            _settingsArray = lstSettings;

            ResponseFunc = iResponse;

            SelectedCruise = iCruise;
            SelectedShip = iShip;
            flgSee = bSee;
            flgRiver = bRiver;

            InitializeComponent();

            flg_All.Checked = true;
            if (bSee && !bRiver)
            {
                flg_All.Checked = false;
                flg_See.Checked = true;
            }
            if (!bSee && bRiver)
            {
                flg_All.Checked = false;
                flg_River.Checked = true;
            }
        }

        private void frmSerchDogovorsShips_Load(object sender, EventArgs e)
        {
            this.Left = StartPosX;
            this.Top = StartPosY;

            panelForm.Width = this.Width;
            panelForm.Height = this.Height;

            Accord_Cruise_List();
        }

        private void frmSerchDogovorsShips_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Accord_Cruise_List()
        {
            _cruiseList.Clear();
            int selected_index = -1;
            int i = 0;
            _cruiseList.Insert(0, new users(0, "Все лайнеры"));
            foreach (cruise_record cruise in _cruiseArray)
            {
                bool SettingsFlag = false;
                foreach (users setting in _settingsArray)
                {
                    if (setting.UserName == cruise.cruise_code)
                    {
                        if (flgSee && flgRiver || !flgSee && !flgRiver || flgSee && setting.id == 1 || flgRiver && (setting.id == 3 || setting.id == 6))
                        {
                            SettingsFlag = true;
                            break;
                        }
                    }
                }
                if (SettingsFlag)
                {
                    _cruiseList.Add(new users(cruise.cruise_id, cruise.cruise_name));
                    i++;
                    if (cruise.cruise_id == SelectedCruise) selected_index = i;
                }
            }

            lst_Cruise.DataSource = null;
            lst_Cruise.DataSource = _cruiseList;
            if (lst_Cruise.Items.Count > 0) lst_Cruise.SelectedIndex = 0;
            if (selected_index > 0 && selected_index < lst_Cruise.Items.Count) lst_Cruise.SelectedIndex = selected_index;
        }

        private void Accord_Ship_List()
        {
            _shipList.Clear();
            int selected_index = -1;
            int i = 0;
            _shipList.Insert(0, new users(0, "Все лайнеры"));
            foreach (ship_record ship in _shipArray)
            {
                if (ship.cruise_id == SelectedCruise) // || SelectedCruise == 0)
                {
                    _shipList.Add(new users(ship.ship_id, ship.ship_name));
                    i++;
                    if (ship.ship_id == SelectedShip) selected_index = i;
                }
            }

            lst_Ship.DataSource = null;
            lst_Ship.DataSource = _shipList;
            if (lst_Ship.Items.Count > 0) lst_Ship.SelectedIndex = 0;
            if (selected_index > 0 && selected_index < lst_Ship.Items.Count) lst_Ship.SelectedIndex = selected_index;
        }

        private void Select_Cruise()
        {
            if (SelectedCruise == 0 && CurrentShip != 0) CurrentShip = 0;

            if (ResponseFunc != null) ResponseFunc(flg_See.Checked, flg_River.Checked, SelectedCruise, CurrentShip);

            this.Close();
        }

        private void Select_Ship()
        {
            if (ResponseFunc != null)
            {
                if (CurrentShip == 0)
                    ResponseFunc(flg_See.Checked, flg_River.Checked, SelectedCruise, CurrentShip);
                else
                    ResponseFunc(flg_See.Checked, flg_River.Checked, SelectedCruise, CurrentShip);
            }

            this.Close();
        }

        private void lst_Cruise_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCruise = 0;
            int i = lst_Cruise.SelectedIndex;
            if (i >= 0 && i < _cruiseList.Count) SelectedCruise = _cruiseList[i].id;

            Accord_Ship_List();

            if (CruiseClickFlag && SelectedCruise == 0) Select_Cruise();
            CruiseClickFlag = false;
        }

        private void lst_Ship_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentShip = 0;
            int i = lst_Ship.SelectedIndex;
            if (i >= 0 && i < _shipList.Count) CurrentShip = _shipList[i].id;

            if (ShipClickFlag) Select_Ship();
            ShipClickFlag = false;
        }

        private void lst_Cruise_MouseClick(object sender, MouseEventArgs e)
        {
            CruiseClickFlag = true;
        }

        private void lst_Cruise_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Select_Cruise();
        }

        private void lst_Ship_MouseClick(object sender, MouseEventArgs e)
        {
            ShipClickFlag = true;
        }

        private void lst_Ship_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Select_Ship();
        }

        private void Get_Flags()
        {
            flgSee = flg_See.Checked;
            flgRiver = flg_River.Checked;
        }

        private void flg_All_Click(object sender, EventArgs e)
        {
            if (flg_All.Checked)
            {
                if (flg_See.Checked) flg_See.Checked = false;
                if (flg_River.Checked) flg_River.Checked = false;
            }
            else
            {
                flg_All.Checked = true;
            }

            Get_Flags();
            Accord_Cruise_List();
        }

        private void flg_See_Click(object sender, EventArgs e)
        {
            if (flg_See.Checked)
            {
                if (flg_All.Checked) flg_All.Checked = false;
                if (flg_River.Checked) flg_River.Checked = false;
            }
            else
            {
                if (!flg_All.Checked) flg_All.Checked = true;
                if (flg_River.Checked) flg_River.Checked = false;
            }

            Get_Flags();
            Accord_Cruise_List();
        }

        private void flg_River_Click(object sender, EventArgs e)
        {
            if (flg_River.Checked)
            {
                if (flg_All.Checked) flg_All.Checked = false;
                if (flg_See.Checked) flg_See.Checked = false;
            }
            else
            {
                if (!flg_All.Checked) flg_All.Checked = true;
                if (flg_See.Checked) flg_See.Checked = false;
            }

            Get_Flags();
            Accord_Cruise_List();
        }

        private void lst_Cruise_MouseEnter(object sender, EventArgs e)
        {
            lst_Cruise.Focus();
        }

        private void lst_Ship_MouseEnter(object sender, EventArgs e)
        {
            lst_Ship.Focus();
        }

        private void frmSerchDogovorsShips_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                Select_Ship();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (lst_Cruise.Focused || lst_Ship.Focused) Select_Ship();
            }
        }
    }
}
