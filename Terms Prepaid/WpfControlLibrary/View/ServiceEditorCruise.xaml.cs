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
using WpfControlLibrary.ViewModel;
using WpfControlLibrary.View;
using WpfControlLibrary.Model.Voucher;


namespace WpfControlLibrary.View
{
    public partial class ServiceEditorCruise : UserControl
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------

        System.Windows.Forms.Form ParentForm;

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region ServiceEditorCruise()
        //----------------------------------------------------------------------------------------------------
        public ServiceEditorCruise(System.Windows.Forms.Form iParentForm)
        {
            InitializeComponent();

            ParentForm = iParentForm;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ServiceEditorCruise
        //----------------------------------------------------------------------------------------------------
        #region _voucher
        //----------------------------------------------------------------------------------------------------
        private Voucher _voucher
        {
            get
            {
                if (DataContext == null) return null;
                if (DataContext.GetType() != typeof(VoucherViewModel)) return null;

                VoucherViewModel vm = (VoucherViewModel)DataContext;
                return vm.Voucher;
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
        private void EditImage_MouseDown(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region EditImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void EditImage_MouseUp(object sender, RoutedEventArgs e)
        {
            On_EditClick(sender, e);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_EditClick
        //----------------------------------------------------------------------------------------------------
        private void On_EditClick(object sender, RoutedEventArgs e)
        {
            //if (ServiceList.SelectedItem == null) return;
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

            foreach (Service svc in _voucher.ServiceList)
            {
                if (svc.DlKey == dlkey)
                {
                    if (svc.EditFlag)
                        svc.EditFlag = false;
                    else
                        svc.EditFlag = true;
                }
                else
                {
                    svc.EditFlag = false;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_EditClick
        //----------------------------------------------------------------------------------------------------
        #region AnnulateImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        private void AnnulateImage_MouseDown(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AnnulateImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region AnnulateImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void AnnulateImage_MouseUp(object sender, RoutedEventArgs e)
        {
            On_AnnulateClick(sender, e);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AnnulateImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_AnnulateClick
        //----------------------------------------------------------------------------------------------------
        private void On_AnnulateClick(object sender, RoutedEventArgs e)
        {
            //if (ServiceList.SelectedItem == null) return;
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

            foreach (Service svc in _voucher.ServiceList)
            {
                if (svc.DlKey == dlkey)
                {
                    // annulate svc
                    MessageBox.Show("Аннуляция услуги " + svc.FullName);

                    break;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_AnnulateClick
        //----------------------------------------------------------------------------------------------------
        #region SaveButton_Click
        //----------------------------------------------------------------------------------------------------
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // save changes



            if (ParentForm != null)  ParentForm.Close();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SaveButton_Click
        //----------------------------------------------------------------------------------------------------
        #endregion // Controls events
        //====================================================================================================

    }
}
