using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace terms_prepaid
{
    public delegate void SelectListItem(int selected_index);


    public partial class frmNewOptionsEditInsList : Form
    {
        private int StartPosX = 0;  // начальные координаты для открытия формы
        private int StartPosY = 0;

        private SelectListItem SelectCallback;


        public frmNewOptionsEditInsList(int iX, int iY, List<String> ItemsList, int selected_index, SelectListItem iSelectCallback)
        {
            InitializeComponent();

            StartPosX = iX;
            StartPosY = iY;

            if (ItemsList != null && ItemsList.Count > 0)
                foreach (string item in ItemsList)
                    lstServices.Items.Add(item);

            if (selected_index >= 0 && selected_index < lstServices.Items.Count)
                lstServices.SelectedIndex = selected_index;

            SelectCallback = iSelectCallback;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.Left = StartPosX;
            this.Top = StartPosY;

            //Accord_List();
        }


        private void Form_Deactivate(object sender, EventArgs e)
        {
            this.Close();
            if (SelectCallback != null) SelectCallback(-1);
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
            if (SelectCallback != null) SelectCallback(-1);
        }

        private void lstServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected_index = lstServices.SelectedIndex;

            if (SelectCallback != null)
            {
                this.Close();
                SelectCallback(selected_index);
            }
        }

    }
}
