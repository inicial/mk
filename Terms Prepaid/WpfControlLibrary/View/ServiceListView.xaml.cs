using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using WpfControlLibrary.Model.Voucher;


namespace WpfControlLibrary.View
{
    public partial class ServiceListView : UserControl
    {
        //====================================================================================================
        #region Delegates
        //----------------------------------------------------------------------------------------------------
        public delegate void ServiceEditCallback(int key_for_serch);

        //----------------------------------------------------------------------------------------------------
        #endregion // Delegates
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------
        private ServiceEditCallback EditCallback;


        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region ServiceListView
        //----------------------------------------------------------------------------------------------------
        public ServiceListView(ServiceEditCallback iEditCallback)
        {
            InitializeComponent();

            EditCallback = iEditCallback;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ServiceListView
        //----------------------------------------------------------------------------------------------------
        #region InitData()
        //----------------------------------------------------------------------------------------------------
        public void InitData()
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InitData
        //----------------------------------------------------------------------------------------------------
        #region _voucherViewModel
        //----------------------------------------------------------------------------------------------------
        private VoucherViewModel _voucherViewModel
        {
            get
            {
                if (DataContext == null) return null;
                if (DataContext.GetType() != typeof(VoucherViewModel)) return null;
                return (VoucherViewModel)DataContext;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // _voucherViewModel
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
        #endregion // Methods
        //====================================================================================================
        #region Controls events
        //----------------------------------------------------------------------------------------------------
        #region ServiceList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        private void ServiceList_SelectionChanged(object sender, EventArgs e)
        {
            //TouristRecord tourist = TouristSelected;

            //DetailsTourist = tourist;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ServiceList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        #region EditImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        private void EditImage_MouseDown(object sender, EventArgs e)
        {
            //if (EditCallback != null) EditCallback();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region EditImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void EditImage_MouseUp(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int dlkey = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) dlkey = int.Parse(tag);
            if (dlkey == 0) return;

            if (_voucher == null) return;
            if (_voucher.ServiceList == null) return;
            if (_voucher.ServiceList.Count == 0) return;

            int key_for_serch = 0;
            foreach (Service svc in _voucher.ServiceList)
            {
                if (svc.DlKey == dlkey)
                {
                    if (svc.EditableFlag)
                        key_for_serch = svc.KeyForSerch;
                    break;
                }
            }

            if (EditCallback != null) EditCallback(key_for_serch);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region NewPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        private void NewPolicyButton_Click(object sender, RoutedEventArgs e)
        {
            

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // NewPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        #region RealisPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        private void RealisPolicyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // RealisPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        #endregion // Controls events
        //====================================================================================================
    }
}
