using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace terms_prepaid.Forms
{
    public delegate void SaveTextFunction(string edit_text);

    public partial class frmInshurControlDesc : Form
    {
        public string EditText = "";
        public SaveTextFunction SaveCallback;

        public frmInshurControlDesc(string iText, SaveTextFunction iSaveCallback = null)
        {
            InitializeComponent();

            EditText = iText;
            SaveCallback = iSaveCallback;

            if (SaveCallback == null)
            {
                txtDesc.ReadOnly = true;
                btnSave.Visible = false;
                btnCancel.Text = "Закрыть";
            }
            txtDesc.TabStop = false;
        }

        private void frmInshurControlDesc_Load(object sender, EventArgs e)
        {
            txtDesc.Text = EditText;
        }

        private void frmInshurControlDesc_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveCallback != null) SaveCallback(txtDesc.Text);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
