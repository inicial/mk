using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.ViewModel;


namespace WpfControlLibrary.View
{
    public delegate int InshurAddCallback();
    public delegate int InshurCopyCallback(InsControlServiceItem item); //, ref int params_id);
    public delegate bool InshurSaveCallback(InsControlServiceItem item, ref bool undo_flag);
    public delegate bool InshurDeleteCallback(InsControlServiceItem item, bool confirm_flag = true);
    public delegate bool InshurConfirmCallback(InsControlServiceItem item);
    public delegate bool InshurEditDescCallback(InsControlServiceItem item);
    public delegate bool InshurSortCallback(int sort_field, int sort_dir, bool request_data = true);


    public partial class InshurControlView : System.Windows.Controls.UserControl
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------

        private System.Windows.Forms.Form ParentForm;

        private InshurControlViewModel _insViewModel;

        public InshurAddCallback AddCallback;
        public InshurCopyCallback CopyCallback;
        public InshurSaveCallback SaveCallback;
        public InshurConfirmCallback CancelCallback;
        public InshurDeleteCallback DeleteCallback;
        public InshurEditDescCallback EditDescCallback;
        public InshurEditDescCallback ViewDescCallback;
        public InshurSortCallback SortCallback;

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region InshurControlView()
        //----------------------------------------------------------------------------------------------------
        public InshurControlView(System.Windows.Forms.Form iParentForm)
        {
            InitializeComponent();

            ParentForm = iParentForm;

            EdittingFlag = false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InshurControlView
        //----------------------------------------------------------------------------------------------------
        #endregion // Methods
        //====================================================================================================
        #region Controls access
        //----------------------------------------------------------------------------------------------------
        #region InsViewModel
        //----------------------------------------------------------------------------------------------------
        public InshurControlViewModel InsViewModel
        {
            get
            {
                if (_insViewModel == null) _insViewModel = GetViewModel();
                return _insViewModel;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsViewModel
        //----------------------------------------------------------------------------------------------------
        #region ServiceSelected
        //----------------------------------------------------------------------------------------------------
        public InsControlServiceItem ServiceSelected
        {
            get
            {
                if (ServiceListView == null) return null;
                if (ServiceListView.SelectedIndex < 0) return null;

                return (InsControlServiceItem)ServiceListView.SelectedItem;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ServiceSelected
        //----------------------------------------------------------------------------------------------------
        #endregion // Controls access
        //====================================================================================================
        #region Data access
        //----------------------------------------------------------------------------------------------------
        #region GetViewModel
        //----------------------------------------------------------------------------------------------------
        private InshurControlViewModel GetViewModel()
        {
            InshurControlViewModel view_model = null;
            if (DataContext != null)
            {
                if (DataContext.GetType() == typeof(InshurControlViewModel))
                    view_model = (InshurControlViewModel)DataContext;
            }
            return view_model; 
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetViewModel
        //----------------------------------------------------------------------------------------------------
        #region EditService
        //----------------------------------------------------------------------------------------------------
        public InsControlServiceItem EditService
        {
            set
            {
                if (InsViewModel != null) InsViewModel.EditService = value;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditService
        //----------------------------------------------------------------------------------------------------
        #region InsServiceList
        //----------------------------------------------------------------------------------------------------
        public InsControlServiceList InsServiceList
        {
            get { if (InsViewModel != null) return InsViewModel.InsControlServiceList; else return null; }
            set { if (InsViewModel != null) InsViewModel.InsControlServiceList = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsServiceList
        //----------------------------------------------------------------------------------------------------
        #region SortNameDir
        //----------------------------------------------------------------------------------------------------
        public int SortNameDir
        {
            get { if (InsViewModel != null) return InsViewModel.SortNameDir; else return 0; }
            set { if (InsViewModel != null) InsViewModel.SortNameDir = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortNameDir
        //----------------------------------------------------------------------------------------------------
        #region SortDateFromDir
        //----------------------------------------------------------------------------------------------------
        public int SortDateFromDir
        {
            get { if (InsViewModel != null) return InsViewModel.SortDateFromDir; else return 0; }
            set { if (InsViewModel != null) InsViewModel.SortDateFromDir = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortDateFromDir
        //----------------------------------------------------------------------------------------------------
        #region SortDateTillDir
        //----------------------------------------------------------------------------------------------------
        public int SortDateTillDir
        {
            get { if (InsViewModel != null) return InsViewModel.SortDateTillDir; else return 0; }
            set { if (InsViewModel != null) InsViewModel.SortDateTillDir = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortDateTillDir
        //----------------------------------------------------------------------------------------------------
        #region SortCurrencyDir
        //----------------------------------------------------------------------------------------------------
        public int SortCurrencyDir
        {
            get { if (InsViewModel != null) return InsViewModel.SortCurrencyDir; else return 0; }
            set { if (InsViewModel != null) InsViewModel.SortCurrencyDir = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortCurrencyDir
        //----------------------------------------------------------------------------------------------------
        #region EdittingItem
        //----------------------------------------------------------------------------------------------------
        public InsControlServiceItem EdittingItem
        {
            get { if (InsViewModel != null) return InsViewModel.EdittingItem; else return null; }
            set { if (InsViewModel != null) InsViewModel.EdittingItem = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EdittingItem
        //----------------------------------------------------------------------------------------------------
        #region EdittingItemBack
        //----------------------------------------------------------------------------------------------------
        public InsControlServiceItem EdittingItemBack
        {
            get { if (InsViewModel != null) return InsViewModel.EdittingItemBack; else return null; }
            set { if (InsViewModel != null) InsViewModel.EdittingItemBack = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EdittingItem
        //----------------------------------------------------------------------------------------------------
        #region EdittingNumber
        //----------------------------------------------------------------------------------------------------
        public int EdittingNumber
        {
            get { if (InsViewModel != null) return InsViewModel.EdittingNumber; else return 0; }
            set { if (InsViewModel != null) InsViewModel.EdittingNumber = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EdittingNumber
        //----------------------------------------------------------------------------------------------------
        #region EdittingFlag
        //----------------------------------------------------------------------------------------------------
        public bool EdittingFlag
        {
            get { if (InsViewModel != null) return InsViewModel.EdittingFlag; else return false; }
            set 
            { 
                bool flag = value;
                if (InsViewModel != null) InsViewModel.EdittingFlag = value;
                
                MoreButton.IsEnabled = !flag;
                EditSaveButton.IsEnabled = flag;
                EditCancelButton.IsEnabled = flag;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsServiceList
        //----------------------------------------------------------------------------------------------------
        #region SortServiceList
        //----------------------------------------------------------------------------------------------------
        private void SortServiceList(int field, int dir)
        {
            //if (InsServiceList == null) return;

            if (SortCallback != null) SortCallback(field, dir);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SortServiceList
        //----------------------------------------------------------------------------------------------------
        #endregion // Data access
        //====================================================================================================
        #region Controls events
        //----------------------------------------------------------------------------------------------------
        #region tblHeaderName_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void tblHeaderName_MouseUp(object sender, RoutedEventArgs e)
        {
            int dir = 1;
            if (SortNameDir > 0) dir = -1;

            SortServiceList(1, dir);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // tblHeaderName_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region tblHeaderDateFrom_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void tblHeaderDateFrom_MouseUp(object sender, RoutedEventArgs e)
        {
            int dir = 1;
            if (SortDateFromDir > 0) dir = -1;

            SortServiceList(2, dir);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // tblHeaderDateFrom_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region tblHeaderDateTill_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void tblHeaderDateTill_MouseUp(object sender, RoutedEventArgs e)
        {
            int dir = 1;
            if (SortDateTillDir > 0) dir = -1;

            SortServiceList(3, dir);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // tblHeaderDateTill_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region tblHeaderCurrency_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void tblHeaderCurrency_MouseUp(object sender, RoutedEventArgs e)
        {
            int dir = 1;
            if (SortCurrencyDir > 0) dir = -1;

            SortServiceList(4, dir);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // tblHeaderCurrency_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region ServiceList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        private void ServiceList_SelectionChanged(object sender, EventArgs e)
        {
            InsControlServiceItem service = ServiceSelected;

            EditService = service;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ServiceList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        #region IsEditting
        //----------------------------------------------------------------------------------------------------
        private bool IsEditting
        {
            get
            {
                if (InsServiceList == null) return false;

                foreach (InsControlServiceItem ins_item in InsServiceList)
                {
                    if (ins_item.EditFlag) return true;
                }

                return false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // IsEditting
        //----------------------------------------------------------------------------------------------------
        #region IsChanged
        //----------------------------------------------------------------------------------------------------
        private bool IsChanged
        {
            get
            {
                if (!EdittingFlag) return false;
                if (EdittingItem == null) return false;
                if (EdittingItemBack == null) return true;
                if (EdittingItem.CheckDiff(EdittingItemBack)) return true;

                return false;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // IsChanged
        //----------------------------------------------------------------------------------------------------
        #region BackButton_Click
        //----------------------------------------------------------------------------------------------------
        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ParentForm != null) ParentForm.Close();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // BackButton_Click
        //----------------------------------------------------------------------------------------------------
        #region CreateButton_Click
        //----------------------------------------------------------------------------------------------------
        public void CreateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CreateButton_Click
        //----------------------------------------------------------------------------------------------------
        #region AddButton_Click
        //----------------------------------------------------------------------------------------------------
        public void AddButton_Click(object sender, RoutedEventArgs e)
        {
            On_AddClick();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddButton_Click
        //----------------------------------------------------------------------------------------------------
        #region On_AddClick()
        //----------------------------------------------------------------------------------------------------
        public void On_AddClick()
        {
            if (InsServiceList == null) return;
            if (IsEditting) return;

            //....................................................................................................
            if (AddCallback == null) return;

            int added_id = AddCallback();
            if (added_id <= 0) return;

            InsControlServiceItem item = new InsControlServiceItem(InsServiceList.Count + 1, "Новая услуга/...");
            item.RowID = added_id;

            item.Set_CurrencyList(InsViewModel.currency_list);
            item.Set_PeriodList(InsViewModel.period_list);
            item.Set_PartnerList(InsViewModel.partenr_list);

            item.СostRateString = "$";
            item.CurrencyIndex = 0;
            item.CurrencySelectedValue = "$";
            item.PartnerKey = 55166;
            item.PartnerIndex = 0;
            item.PartnerSelectedValue = item.PartnerList[0];

            item.NewFlag = true;
            item.EditFlag = true;

            InsServiceList.Add(item);

            ServiceListView.SelectedIndex = InsServiceList.Count - 1;
            //ServiceListView.ScrollIntoView(ServiceListView.SelectedItem);
            if (InsServiceList.Count > 0)
                ServiceListView.ScrollIntoView(InsServiceList[InsServiceList.Count - 1]);

            BeginEditting(item);

            //EdittingFlag = true;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // On_AddClick()
        //----------------------------------------------------------------------------------------------------
        #region EditSaveButton_Click
        //----------------------------------------------------------------------------------------------------
        public void EditSaveButton_Click(object sender, RoutedEventArgs e)
        {
            On_SaveClick();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditSaveButton_Click
        //----------------------------------------------------------------------------------------------------
        #region On_SaveClick()
        //----------------------------------------------------------------------------------------------------
        public void On_SaveClick()
        {
            if (InsServiceList == null) return;
            if (!IsEditting) return;
            if (!EdittingFlag) return;
            if (!(EdittingNumber > 0)) return;

            //....................................................................................................
            On_EditClick( EdittingNumber );

            //....................................................................................................
            //EdittingFlag = false;
            //EdittingNumber = 0;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // On_SaveClick()
        //----------------------------------------------------------------------------------------------------
        #region EditCancelButton_Click
        //----------------------------------------------------------------------------------------------------
        public void EditCancelButton_Click(object sender, RoutedEventArgs e)
        {
            On_CancelClick();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditCancelButton_Click
        //----------------------------------------------------------------------------------------------------
        #region On_CancelClick()
        //----------------------------------------------------------------------------------------------------
        public void On_CancelClick()
        {
            if (InsServiceList == null) return;
            if (!IsEditting) return;
            if (!EdittingFlag) return;
            if (!(EdittingNumber > 0)) return;

            //....................................................................................................
            //if (IsChanged && CancelCallback != null)
            //    if (!CancelCallback(null)) return;

            On_EditClick(EdittingNumber, true);

            //....................................................................................................
            EdittingFlag = false;
            EdittingNumber = 0;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // On_CancelClick()
        //----------------------------------------------------------------------------------------------------
        #region GearButton_Click --
        //----------------------------------------------------------------------------------------------------
        public void GearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GearButton_Click
        //----------------------------------------------------------------------------------------------------
        #region GearButton_Closed --
        //----------------------------------------------------------------------------------------------------
        private void GearButton_Closed(object sender, EventArgs e)
        {
        //    GearButton.IsChecked = false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GearButton_Closed
        //----------------------------------------------------------------------------------------------------
        #region GearButton_Checked --
        //----------------------------------------------------------------------------------------------------
        private void GearButton_Checked(object sender, RoutedEventArgs e)
        {
        //    GearPopup.IsOpen = true;
        //    GearPopup.Closed -= GearButton_Closed;
        //    GearPopup.Closed += GearButton_Closed;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GearButton_Checked
        //----------------------------------------------------------------------------------------------------
        #region GearButton_Unchecked --
        //----------------------------------------------------------------------------------------------------
        private void GearButton_Unchecked(object sender, RoutedEventArgs e)
        {
        //    GearPopup.IsOpen = false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GearButton_Unchecked
        //----------------------------------------------------------------------------------------------------
        #region EditButton1_Click --
        //----------------------------------------------------------------------------------------------------
        private void EditButton1_Click(object sender, RoutedEventArgs e)
        {
        //    GearButton.IsChecked = false;
        //    OnEditButtonClick(1);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditButton1_Click
        //----------------------------------------------------------------------------------------------------
        #region EditButton2_Click --
        //----------------------------------------------------------------------------------------------------
        private void EditButton2_Click(object sender, RoutedEventArgs e)
        {
        //    GearButton.IsChecked = false;
        //    OnEditButtonClick(2);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditButton2_Click
        //----------------------------------------------------------------------------------------------------
        #region EditButton2_Click
        //----------------------------------------------------------------------------------------------------
        private void OnEditButtonClick(int selected)
        {
            //if (OnButtonClick != null)
            //    OnButtonClick(this);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditButton2_Click
        //----------------------------------------------------------------------------------------------------
        #region UpdateButton_Click
        //----------------------------------------------------------------------------------------------------
        public void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(System.Windows.Controls.Button)) return;

            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
            int number = 0;
            string tag = "";
            tag = btn.Tag.ToString();
            if (btn.Tag != null) tag = btn.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) number = int.Parse(tag);
            if (number == 0) return;

            On_EditClick(number);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // UpdateButton_Click
        //----------------------------------------------------------------------------------------------------
        #region EditButton_Click
        //----------------------------------------------------------------------------------------------------
        public void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditButton_Click
        //----------------------------------------------------------------------------------------------------
        #region SaveButton_Click
        //----------------------------------------------------------------------------------------------------
        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SaveButton_Click
        //----------------------------------------------------------------------------------------------------
        #region CancelButton_Click
        //----------------------------------------------------------------------------------------------------
        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CancelButton_Click
        //----------------------------------------------------------------------------------------------------
        #region DeleteButton_Click
        //----------------------------------------------------------------------------------------------------
        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DeleteButton_Click
        //----------------------------------------------------------------------------------------------------
        #region DescImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        private void DescImage_MouseDown(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DescImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region DescImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void DescImage_MouseUp(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) number = int.Parse(tag);
            if (number == 0) return;

            On_DescClick(number);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DescImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_DescClick
        //----------------------------------------------------------------------------------------------------
        public void On_DescClick(int number)
        {
            if (number == 0) return;

            if (InsServiceList == null) return;
            if (InsServiceList.Count == 0) return;

            bool bEditting = false;
            foreach (InsControlServiceItem item in InsServiceList)
            {
                if (item.EditFlag && item.RowNumber != number)
                {
                    bEditting = true;
                    break;
                }
            }

            if (bEditting) return;

            InsControlServiceItem desc_item = null;
            foreach (InsControlServiceItem item in InsServiceList)
            {
                if (item.RowNumber == number)
                {
                    desc_item = item;
                    break;
                }
            }
            if (desc_item == null) return;

            //if (desc_item.EditFlag)
                On_DescEdit(desc_item);
            //else
            //    On_DescView(desc_item);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_DescClick
        //----------------------------------------------------------------------------------------------------
        #region On_DescEdit
        //----------------------------------------------------------------------------------------------------
        public void On_DescEdit(InsControlServiceItem desc_item)
        {
            if (desc_item == null) return;

            //System.Windows.MessageBox.Show("Редактирование описания...");

            if (EditDescCallback != null) EditDescCallback(desc_item);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_DescClick
        //----------------------------------------------------------------------------------------------------
        #region On_DescView
        //----------------------------------------------------------------------------------------------------
        public void On_DescView(InsControlServiceItem desc_item)
        {
            if (desc_item == null) return;

            if (ViewDescCallback != null) ViewDescCallback(desc_item);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_DescView
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
            //if (IsEditting) return;
            //if (EdittingFlag) return;

            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) number = int.Parse(tag);
            if (number == 0) return;

            On_EditClick(number, false, true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // EditImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_EditClick
        //----------------------------------------------------------------------------------------------------
        public void On_EditClick(int number, bool cancel_flag = false, bool icon_flag = false)
        {
            if (number == 0) return;

            if (InsServiceList == null) return;
            if (InsServiceList.Count == 0) return;

            bool bEditting = false;
            foreach (InsControlServiceItem item in InsServiceList)
            {
                if (item.EditFlag && item.RowNumber != number) 
                {
                    bEditting = true;
                    break;
                }
            }

            if (bEditting) return;

            //foreach (InsControlServiceItem item in InsServiceList)
            for (int i = 0; i < InsServiceList.Count; i++)
            {
                InsControlServiceItem item = InsServiceList[i];
                if (item.RowNumber == number)
                {
                    if (item.EditFlag)
                    {
                        if (IsChanged || item.NewFlag)
                        {
                            if (icon_flag) return;
                            if (cancel_flag) UndoEditting(item);
                            if (!cancel_flag) 
                                if (!SaveRowData(item)) return;
                        }
                        EndEditting(item);
                        //item.EditFlag = false;
                        //EdittingNumber = 0;
                    }
                    else
                    {
                        BeginEditting(item);
                        //item.EditFlag = true;
                        //EdittingNumber = item.RowNumber;
                        bEditting = true;
                    }
                }
                //else
                //{
                //    item.EditFlag = false;
                //    item.CopyFlag = false;
                //}
            }

            foreach (InsControlServiceItem item in InsServiceList)
            {
                if (item.RowNumber != number)
                {
                    item.EditFlag = false;
                    item.CopyFlag = false;
                    //item.LockFlag = bEditting;
                }
            }

            EdittingFlag = bEditting;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_EditClick
        //----------------------------------------------------------------------------------------------------
        #region CopyImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        private void CopyImage_MouseDown(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CopyImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region CopyImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void CopyImage_MouseUp(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) number = int.Parse(tag);
            if (number == 0) return;

            On_CopyClick(number);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CopyImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_CopyClick
        //----------------------------------------------------------------------------------------------------
        private void On_CopyClick(int number)
        {
            if (number == 0) return;

            if (InsServiceList == null) return;
            if (InsServiceList.Count == 0) return;
            if (IsEditting) return;
            if (EdittingFlag) return;

            InsControlServiceItem copy_item = null;

            foreach (InsControlServiceItem ins_item in InsServiceList)
            {
                if (ins_item.RowNumber == number)
                {
                    copy_item = ins_item;
                    break;
                }
            }
            if (copy_item == null) return;

            //....................................................................................................
            if (CopyCallback == null) return;

            //int params_id = 0;
            int added_id = CopyCallback(copy_item); //, ref params_id);
            if (added_id <= 0) return;

            InsControlServiceItem item = new InsControlServiceItem(InsServiceList.Count + 1, "Новая запись...");
            item.RowID = added_id;
            item.ParamsKey = copy_item.ParamsKey; // params_id;
            item.Set_CurrencyList(InsViewModel.currency_list);
            item.Set_PeriodList(InsViewModel.period_list);
            item.Set_PartnerList(InsViewModel.partenr_list);

            item.ServiceName = copy_item.ServiceName;
            item.DateFrom = copy_item.DateFrom;
            item.DateTill = copy_item.DateTill;
            item.СostRateString = copy_item.СostRateString;
            item.CurrencyIndex = copy_item.CurrencyIndex;
            item.Netto = copy_item.Netto;
            item.Brutto = copy_item.Brutto;
            item.PartnerKey = copy_item.PartnerKey;
            item.PartnerString = copy_item.PartnerString;
            item.PartnerIndex = copy_item.PartnerIndex;
            item.PriceByDay = copy_item.PriceByDay;
            item.PeriodIndex = copy_item.PeriodIndex;

            item.NewFlag = true;
            item.EditFlag = true;
            copy_item.CopyFlag = true;

            //InsServiceList.Add(item);
            int ins_index = InsServiceList.IndexOf(copy_item) + 1;
            InsServiceList.Insert(ins_index, item);

            ServiceListView.SelectedIndex = ins_index;
            ServiceListView.ScrollIntoView(ServiceListView.SelectedItem);
            //if (InsServiceList.Count > 0)
            //    ServiceListView.ScrollIntoView(InsServiceList[InsServiceList.Count - 1]);

            BeginEditting(item);

            //EdittingItemBack.ServiceName = "";
            //EdittingItemBack.DescriptionString = "New copy data...";

            //EdittingFlag = true;
            //EdittingNumber = item.RowNumber;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_CopyClick
        //----------------------------------------------------------------------------------------------------
        #region DeleteImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        private void DeleteImage_MouseDown(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DeleteImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region DeleteImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void DeleteImage_MouseUp(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) number = int.Parse(tag);
            if (number == 0) return;

            On_DeleteClick(number);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DeleteImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_DeleteClick
        //----------------------------------------------------------------------------------------------------
        private void On_DeleteClick(int number)
        {
            if (number == 0) return;

            if (InsServiceList == null) return;
            if (InsServiceList.Count == 0) return;
            //if (IsEditting) return;
            //if (EdittingFlag) return;

            InsControlServiceItem delete_item = null;
            foreach (InsControlServiceItem item in InsServiceList)
            {
                if (item.RowNumber == number)
                {
                    delete_item = item;
                    break;
                }
            }
            if (delete_item == null) return;
            if (EdittingFlag && EdittingNumber != delete_item.RowNumber) return;

            //....................................................................................................
            if (DeleteCallback == null) return;

            if (!EdittingFlag) delete_item.EditFlag = true;
            bool bDeleted = DeleteCallback(delete_item);
            if (!EdittingFlag) delete_item.EditFlag = false;

            if (!bDeleted) return;

            if (IsEditting || EdittingFlag) EndEditting(delete_item);

            InsServiceList.Remove(delete_item);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_DeleteClick
        //----------------------------------------------------------------------------------------------------
        #region PreviewTextInput
        //----------------------------------------------------------------------------------------------------
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender.GetType() != typeof(System.Windows.Controls.TextBox))
            {
                //e.Handled = false;
                return;
            }

            System.Windows.Controls.TextBox tbx = (System.Windows.Controls.TextBox)sender;

            //e.Handled = !IsTextAllowed(tbx.Text, e.Text); 
        }

        private bool IsTextAllowed(string text, string symbol)
        {
            if (string.IsNullOrEmpty(text)) return false;
            int pos = text.IndexOf((char)47); // /
            if (pos >= 0) return true;
            return false;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // PreviewTextInput
        //----------------------------------------------------------------------------------------------------
        #endregion // Controls events
        //====================================================================================================
        #region Data work
        //----------------------------------------------------------------------------------------------------
        #region BeginEditting()
        //----------------------------------------------------------------------------------------------------
        public void BeginEditting(InsControlServiceItem edit_item)
        {
            if (edit_item == null) return;

            EdittingItem = edit_item;
            EdittingNumber = edit_item.RowNumber;

            EdittingItemBack = new InsControlServiceItem(edit_item.RowNumber, edit_item.ServiceName);
            EdittingItemBack.CopyBack(EdittingItem);

            EdittingItem.EditFlag = true;

            foreach (InsControlServiceItem item in InsServiceList)
                if (item.RowNumber != EdittingNumber)
                    item.LockFlag = true;

            EdittingFlag = true;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // BeginEditting()
        //----------------------------------------------------------------------------------------------------
        #region EndEditting()
        //----------------------------------------------------------------------------------------------------
        public void EndEditting(InsControlServiceItem edit_item = null)
        {
            if (EdittingItem == null && edit_item != null) EdittingItem = edit_item;

            if (EdittingItem != null)
            {
                EdittingItem.EditFlag = false;
                EdittingItem.NewFlag = false;
                EdittingItem = null;
                EdittingNumber = 0;
            }

            foreach (InsControlServiceItem item in InsServiceList)
            {
                if (item.RowNumber != EdittingNumber)
                {
                    item.EditFlag = false;
                    item.CopyFlag = false;
                    item.LockFlag = false;
                }
            }

            EdittingFlag = false;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // EndEditting()
        //----------------------------------------------------------------------------------------------------
        #region UndoEditting()
        //----------------------------------------------------------------------------------------------------
        public void UndoEditting(InsControlServiceItem edit_item = null)
        {
            if (InsServiceList == null) return;

            InsControlServiceItem undo_item = EdittingItem;
            if (undo_item == null && edit_item != null) undo_item = edit_item;

            //bool bCopy = false;
            //foreach (InsControlServiceItem item in InsServiceList)
            //    if (item.RowNumber != EdittingNumber)
            //        if (item.CopyFlag) bCopy = true;

            //if (bCopy && DeleteCallback != null)
            if (undo_item.NewFlag && DeleteCallback != null)
            {
                bool bDeleted = DeleteCallback(undo_item, false);
                if (bDeleted)
                {
                    InsServiceList.Remove(undo_item);
                    return;
                }
            }

            if (undo_item != null && EdittingItemBack != null)
                undo_item.CopyBack(EdittingItemBack);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // UndoEditting()
        //----------------------------------------------------------------------------------------------------
        #region SaveRowData()
        //----------------------------------------------------------------------------------------------------
        public bool SaveRowData(InsControlServiceItem item)
        {
            if (item == null) return false;

            item.Read_SelectedCurrency();
            item.Read_SelectedPeriod();
            item.Read_SelectedPartner();

            if (SaveCallback != null)
            {
                bool bUndo = false;
                bool bSaved = SaveCallback(item, ref bUndo);
                if (!bSaved)
                {
                    if (bUndo)
                    {
                        UndoEditting(item);
                        return true;
                    }
                    return false;
                }
            }
            return true;
        }
        //----------------------------------------------------------------------------------------------------
        #endregion // SaveRowData()
        //----------------------------------------------------------------------------------------------------
        #endregion // Data work
        //====================================================================================================

    }
}
