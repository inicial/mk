using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using WpfControlLibrary.Model.Voucher;


namespace terms_prepaid
{
    public partial class frmNewOptionsEditCruise : Form
    {
        //----------------------------------------------------------------------------------------------------
        #region Properties
        //----------------------------------------------------------------------------------------------------
        public int InitLeft = 100;
        public int InitTop = 100;

        private VoucherViewModel _voucherViewModel;


        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //----------------------------------------------------------------------------------------------------
        #region frmNewOptionsEditCruise()
        //----------------------------------------------------------------------------------------------------
        public frmNewOptionsEditCruise(VoucherViewModel iVoucherViewModel)
        {
            InitializeComponent();

            _voucherViewModel = iVoucherViewModel;

            ServiceEditorCruise uc = new ServiceEditorCruise(this);
            uc.DataContext = _voucherViewModel;
            ServiceListHost.Child = uc;


        }

        //----------------------------------------------------------------------------------------------------
        #endregion // frmNewOptionsEditCruise
        //----------------------------------------------------------------------------------------------------
        #region InitPosition()
        //----------------------------------------------------------------------------------------------------
        public void InitPosition(int iLeft, int iTop)
        {
            InitLeft = iLeft;
            InitTop = iTop;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InitPosition
        //----------------------------------------------------------------------------------------------------
        #region frmNewOptionsEditCruise_Load()
        //----------------------------------------------------------------------------------------------------
        private void frmNewOptionsEditCruise_Load(object sender, EventArgs e)
        {
            this.Left = InitLeft;
            this.Top = InitTop;


            foreach (Service svc in _voucher.ServiceList)
            {
                svc.EditableFlag = false;
                if (svc.KeyForSerch == 7) svc.EditableFlag = true;
                svc.EditFlag = false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // frmNewOptionsEditCruise_Load()
        //----------------------------------------------------------------------------------------------------
        #region _voucher
        //----------------------------------------------------------------------------------------------------
        private Voucher _voucher
        {
            get
            {
                if (_voucherViewModel == null) return null;
                return _voucherViewModel.Voucher;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // _voucher
        //----------------------------------------------------------------------------------------------------

    }
}
