using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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
    public delegate void FormCollapsedNotice(bool collapsed_flag);
    public delegate void AddServicesCallback();
    public delegate void NewPolicyCallback(int row_number);
    public delegate void NewCabinetPolicyCallback(int row_number, bool cabinet_flag);
    //public delegate int FormListCallback(int x, int y);
    public delegate void FormListCallback(int row_number, int selected_index);


    public partial class ServiceEditorInsurance : UserControl
    {
        //====================================================================================================
        #region Properties
        //----------------------------------------------------------------------------------------------------

        System.Windows.Forms.Form ParentForm;

        public FormCollapsedNotice CollapsedCallback;
        public AddServicesCallback AddCallback;
        public AddServicesCallback SaveCallback;
        public FormListCallback ListCallback;
        public NewPolicyCallback DeleteCallback;
        public NewCabinetPolicyCallback NewPolicyCallback;
        //public NewPolicyCallback RealisPolicyCallback;
        //public NewPolicyCallback CabinetPolicyCallback;
        public NewPolicyCallback AnnulatePolicyCallback;

        public bool IsAddingMode;
        public bool IsAddingTouristMode;

        public bool IsListOpened;
        public bool WasListOpened;
        public DateTime ListClosedDT;

        //System.Windows.Forms.Form frmList; // форма для выбора услуги
        //System.Windows.Window wndList;

        //----------------------------------------------------------------------------------------------------
        #endregion // Properties
        //====================================================================================================
        #region Methods
        //----------------------------------------------------------------------------------------------------
        #region ServiceEditorInsurance()
        //----------------------------------------------------------------------------------------------------
        public ServiceEditorInsurance(System.Windows.Forms.Form iParentForm)
        {
            InitializeComponent();

            ParentForm = iParentForm;

            ListClosedDT = DateTime.Now;
        }

        public void SetListHeight(int min_height)
        {
            ServiceListView.MinHeight = min_height;
        }

        public void AccordControls()
        {
            //if (WithPolicyFlag)
            //    AnnulatePolicyButton.Visibility = System.Windows.Visibility.Visible;
            //else
            //    AnnulatePolicyButton.Visibility = System.Windows.Visibility.Hidden;

            if (WithoutPolicyFlag)
            {
                NewPolicyButton.Visibility = System.Windows.Visibility.Visible;
                CabinetPolicyButton.Visibility = System.Windows.Visibility.Visible;
                AddButton.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                NewPolicyButton.Visibility = System.Windows.Visibility.Hidden;
                CabinetPolicyButton.Visibility = System.Windows.Visibility.Hidden;
                //CabinetPolicyButton.Visibility = System.Windows.Visibility.Visible;
                //CabinetButtonText = "Выложить  в  ЛК";
                AddButton.Visibility = System.Windows.Visibility.Hidden;
            }

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ServiceEditorInsurance
        //----------------------------------------------------------------------------------------------------
        #endregion // Methods
        //====================================================================================================
        #region Data access
        //----------------------------------------------------------------------------------------------------
        #region InsViewModel
        //----------------------------------------------------------------------------------------------------
        private InsuranceViewModel InsViewModel
        {
            get 
            {
                if (DataContext == null) return null;
                if (DataContext.GetType() != typeof(InsuranceViewModel)) return null;

                return (InsuranceViewModel)DataContext;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsViewModel
        //----------------------------------------------------------------------------------------------------
        #region ServiceList
        //----------------------------------------------------------------------------------------------------
        private ObservableCollection<Service> ServiceList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.ServiceList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ServiceList
        //----------------------------------------------------------------------------------------------------
        #region InsTouristList
        //----------------------------------------------------------------------------------------------------
        private List<InsTouristItem> InsTouristList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.InsTouristList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsTouristList
        //----------------------------------------------------------------------------------------------------
        #region InsServiceList
        //----------------------------------------------------------------------------------------------------
        private List<InsServiceItem> InsServiceList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.InsServiceList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsTouristList
        //----------------------------------------------------------------------------------------------------
        #region InsGroupServiceList
        //----------------------------------------------------------------------------------------------------
        private List<InsServiceItem> InsGroupServiceList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.InsGroupServiceList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsGroupServiceList
        //----------------------------------------------------------------------------------------------------
        #region InsGroupList
        //----------------------------------------------------------------------------------------------------
        private List<InsGroupItem> InsGroupList
        {
            get
            {
                if (InsViewModel == null) return null;

                return InsViewModel.InsGroupList;
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsGroupList
        //----------------------------------------------------------------------------------------------------
        #region AddButtonText
        //----------------------------------------------------------------------------------------------------
        private string AddButtonText
        {
            get { if (InsViewModel != null) return InsViewModel.AddButtonText; else return ""; }
            set { if (InsViewModel != null) InsViewModel.AddButtonText = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddButtonText
        //----------------------------------------------------------------------------------------------------
        #region CabinetButtonText
        //----------------------------------------------------------------------------------------------------
        private string CabinetButtonText
        {
            get { if (InsViewModel != null) return InsViewModel.CabinetButtonText; else return ""; }
            set { if (InsViewModel != null) InsViewModel.CabinetButtonText = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CabinetButtonText
        //----------------------------------------------------------------------------------------------------
        #region WithPolicyFlag
        //----------------------------------------------------------------------------------------------------
        private bool WithPolicyFlag
        {
            get { if (InsViewModel != null) return InsViewModel.WithPolicyFlag; else return false; }
            set { if (InsViewModel != null) InsViewModel.WithPolicyFlag = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WithPolicyFlag
        //----------------------------------------------------------------------------------------------------
        #region WithoutPolicyFlag
        //----------------------------------------------------------------------------------------------------
        private bool WithoutPolicyFlag
        {
            get { if (InsViewModel != null) return InsViewModel.WithoutPolicyFlag; else return false; }
            set { if (InsViewModel != null) InsViewModel.WithoutPolicyFlag = value; }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // WithoutPolicyFlag
        //----------------------------------------------------------------------------------------------------
        #endregion // Data access
        //====================================================================================================
        #region Data work
        //----------------------------------------------------------------------------------------------------
        #region GetData()
        //----------------------------------------------------------------------------------------------------
        private void GetData()
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GetData()
        //----------------------------------------------------------------------------------------------------
        #endregion // Data work
        //====================================================================================================
        #region Controls events
        //----------------------------------------------------------------------------------------------------
        #region tbxCode_Changed
        //----------------------------------------------------------------------------------------------------
        public void tbxCode_Changed(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // tbxCode_Changed
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
        #region NumberImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        private void NumberImage_MouseDown(object sender, RoutedEventArgs e)
        {
            On_ShowNumber(sender, e, true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // NumberImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region NumberImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void NumberImage_MouseUp(object sender, RoutedEventArgs e)
        {
            On_ShowNumber(sender, e, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // NumberImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region NumberImage_MouseEnter
        //----------------------------------------------------------------------------------------------------
        private void NumberImage_MouseEnter(object sender, RoutedEventArgs e)
        {
            //On_ShowNumber(sender, e, true);
            On_ShowNumberTooltip(sender, e, true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // NumberImage_MouseEnter
        //----------------------------------------------------------------------------------------------------
        #region NumberImage_MouseLeave
        //----------------------------------------------------------------------------------------------------
        private void NumberImage_MouseLeave(object sender, RoutedEventArgs e)
        {
            On_ShowNumberTooltip(sender, e, false);
            On_ShowNumber(sender, e, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // NumberImage_MouseLeave
        //----------------------------------------------------------------------------------------------------
        #region On_ShowNumber
        //----------------------------------------------------------------------------------------------------
        private void On_ShowNumber(object sender, RoutedEventArgs e, bool show_flag)
        {
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) number = int.Parse(tag);
            if (number == 0) return;

            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.RowNumber == number)
                {
                    item.NumberFlag = show_flag;
                    if (show_flag)
                        Clipboard.SetText(item.PolicyNumberString);
                    break;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_ShowNumber
        //----------------------------------------------------------------------------------------------------
        #region On_ShowNumberTooltip
        //----------------------------------------------------------------------------------------------------
        private void On_ShowNumberTooltip(object sender, RoutedEventArgs e, bool show_flag)
        {
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) number = int.Parse(tag);
            if (number == 0) return;

            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.RowNumber == number)
                {
                    //item.NumberFlag = show_flag;
                    if (show_flag)
                        Clipboard.SetText(item.PolicyNumberString);
                    break;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_ShowNumberTooltip
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
            if (IsAddingMode) return;

            //if (ServiceList.SelectedItem == null) return;
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int row_number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) row_number = int.Parse(tag);
            if (row_number == 0) return;

            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            bool bEdit = false;
            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.EditFlag) bEdit = true;
            }

            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.RowNumber == row_number)
                {
                    if (item.EditFlag)
                    {
                        //item.EditFlag = false;
                        //Edit_End();
                    }
                    else
                    {
                        if (!bEdit)
                        {
                            item.EditFlag = true;
                            Edit_Begin();
                        }
                    }
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_EditClick
        //----------------------------------------------------------------------------------------------------
        #region Edit_Begin
        //----------------------------------------------------------------------------------------------------
        private void Edit_Begin()
        {
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            int dlkey = 0;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                tourist.CanEditFlag = false;
                tourist.EditIconsFlag = false;
                tourist.PolicyIconsFlagCopy = tourist.PolicyIconsFlag;
                tourist.PolicyIconsFlag = false;
                tourist.AddControlFlag = false;
                tourist.AddingFlag = false;
                tourist.AddFlag = false;
                tourist.SelectedFlag = false;
                tourist.AddVisibility = "Collapsed";
                tourist.AccordEmpty();

                if (tourist.EditFlag)
                {
                    tourist.CopyEdit();
                    dlkey = tourist.DlKey;
                }
            }

            foreach (InsTouristItem tourist in InsTouristList)
            {
                //tourist.PricingString = tourist.DlKey.ToString();
                if (tourist.DlKey == dlkey)
                {
                    foreach (InsTouristItem item in InsTouristList)
                    {
                        if (item.Tukey == tourist.Tukey)
                        {
                            if (item.DlKey == dlkey)
                            {
                                item.AddControlFlag = true;
                                item.AddingFlag = true;
                                item.AddFlag = true;
                                item.AddVisibility = "Visible";
                                //if (tourist.EditFlag) tourist.AddFlag = true;
                                item.NameEditFlag = true;
                            }
                            if (!string.IsNullOrEmpty(item.TouristName)) item.NameEditFlag = true;
                        }
                    }
                }
            }

            if (CollapsedCallback != null) CollapsedCallback(true);

            AddStackPanel.Visibility = System.Windows.Visibility.Visible;
            InsListGrid.Visibility = System.Windows.Visibility.Collapsed;

            AddButton.Visibility = System.Windows.Visibility.Hidden;
            SaveEditButton.Visibility = System.Windows.Visibility.Visible;
            CancelEditButton.Visibility = System.Windows.Visibility.Visible;

            NewPolicyButton.Visibility = System.Windows.Visibility.Hidden;
            CabinetPolicyButton.Visibility = System.Windows.Visibility.Hidden;

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Edit_Begin
        //----------------------------------------------------------------------------------------------------
        #region Edit_End
        //----------------------------------------------------------------------------------------------------
        private void Edit_End( bool cancel_flag = false )
        {
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.EditFlag && cancel_flag) tourist.CancelEdit();

                tourist.CanEditFlag = true;
                tourist.EditIconsFlag = true;
                tourist.PolicyIconsFlag = tourist.PolicyIconsFlagCopy;
                tourist.AddControlFlag = tourist.CheckControlFlag;
                tourist.AddFlag = false;
                tourist.SelectedFlag = false;
                tourist.AddingFlag = false;
                tourist.AddVisibility = "Hidden";
                tourist.EditFlag = false;
                tourist.NameEditFlag = false;
                tourist.AccordEmpty();
            }

            SaveEditButton.Visibility =  System.Windows.Visibility.Hidden;
            CancelEditButton.Visibility = System.Windows.Visibility.Hidden;
            AddButton.Visibility = System.Windows.Visibility.Visible;

            NewPolicyButton.Visibility = System.Windows.Visibility.Visible; ///Policy
            CabinetPolicyButton.Visibility = System.Windows.Visibility.Visible; ///Policy

            AccordControls();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Edit_End
        //----------------------------------------------------------------------------------------------------
        #region Reset_Controls
        //----------------------------------------------------------------------------------------------------
        public void Reset_Controls()
        {
            foreach (InsTouristItem tourist in InsTouristList)
            {
                tourist.CanEditFlag = true;
                tourist.EditIconsFlag = true;
                tourist.PolicyIconsFlag = tourist.PolicyIconsFlagCopy;
                tourist.AddControlFlag = tourist.CheckControlFlag;
                tourist.AddFlag = false;
                tourist.SelectedFlag = false;
                tourist.AddingFlag = false;
                tourist.AddVisibility = "Hidden";
                tourist.EditFlag = false;
                tourist.NameEditFlag = false;
                tourist.AccordEmpty();
            }

            AddButton.Visibility = System.Windows.Visibility.Visible;
            AddButtonText = "Выбрать  туриста  и  добавить  услугу";
            CabinetButtonText = "Выписать  и  выложить  в  ЛК";

            PreCancelButton.Visibility = System.Windows.Visibility.Hidden;

            SaveEditButton.Visibility = System.Windows.Visibility.Hidden;
            CancelEditButton.Visibility = System.Windows.Visibility.Hidden;

            NewPolicyButton.Visibility = System.Windows.Visibility.Visible;   ///Policy
            CabinetPolicyButton.Visibility = System.Windows.Visibility.Visible; ///Policy

            AddStackPanel.Visibility = System.Windows.Visibility.Visible;
            AddButton.Visibility = System.Windows.Visibility.Visible;
            InsListGrid.Visibility = System.Windows.Visibility.Collapsed;

            AccordControls();

            foreach (InsServiceItem item in InsServiceList)
            {
                item.AddFlag = false;
            }

            IsAddingMode = false;
            IsAddingTouristMode = false;

            if (CollapsedCallback != null) CollapsedCallback(true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Reset_Controls
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
            On_DeleteClick(sender, e);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // DeleteImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_DeleteClick
        //----------------------------------------------------------------------------------------------------
        private void On_DeleteClick(object sender, RoutedEventArgs e)
        {
            //if (ServiceList.SelectedItem == null) return;
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int row_number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) row_number = int.Parse(tag);
            if (row_number == 0) return;

            if (DeleteCallback != null) DeleteCallback(row_number);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_DeleteClick
        //----------------------------------------------------------------------------------------------------
        #region PolicyImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        private void PolicyImage_MouseDown(object sender, RoutedEventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // PolicyImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region PolicyImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void PolicyImage_MouseUp(object sender, RoutedEventArgs e)
        {
            On_PolicyClick(sender, e);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // PolicyImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_PolicyClick
        //----------------------------------------------------------------------------------------------------
        private void On_PolicyClick(object sender, RoutedEventArgs e)
        {
            //if (ServiceList.SelectedItem == null) return;
            if (sender.GetType() != typeof(Image)) return;

            Image img = (Image)sender;
            int row_number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) row_number = int.Parse(tag);
            if (row_number == 0) return;

            if (ServiceList == null) return;
            if (ServiceList.Count == 0) return;

            if (NewPolicyCallback != null) NewPolicyCallback(row_number, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_PolicyClick
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
            int row_number = 0;
            string tag = "";
            tag = img.Tag.ToString();
            if (img.Tag != null) tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) row_number = int.Parse(tag);
            if (row_number == 0) return;

            if (ServiceList == null) return;
            if (ServiceList.Count == 0) return;

            if (AnnulatePolicyCallback != null) AnnulatePolicyCallback(row_number);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_AnnulateClick
        //----------------------------------------------------------------------------------------------------
        #region ListImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        private void ListImage_MouseDown(object sender, RoutedEventArgs e)
        {
            WasListOpened = IsListOpened;
            //if ((DateTime.Now - ListClosedDT).TotalMilliseconds < 1000) WasListOpened = true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ListImage_MouseDown
        //----------------------------------------------------------------------------------------------------
        #region ListImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        private void ListImage_MouseUp(object sender, RoutedEventArgs e)
        {
            if (WasListOpened) return;
            if ((DateTime.Now - ListClosedDT).TotalMilliseconds < 1000)
            {
                ListClosedDT = DateTime.Now.AddSeconds(-10);
                return;
            }

            On_ListClick(sender, e);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // ListImage_MouseUp
        //----------------------------------------------------------------------------------------------------
        #region On_ListClick
        //----------------------------------------------------------------------------------------------------
        private void On_ListClick(object sender, RoutedEventArgs e)
        {
            //if (sender.GetType() != typeof(Image)) return;
            if (sender.GetType() != typeof(TextBlock)) return;

            //Image img = (Image)sender;
            TextBlock img = (TextBlock)sender;
            int row_number = 0;
            string tag = img.Tag.ToString();
            if (!string.IsNullOrEmpty(tag)) row_number = int.Parse(tag);
            if (row_number == 0) return;

            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            bool bEdit = false;
            int dl_code = 0;
            int subcode1 = 0;
            string service_name = "";
            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.RowNumber == row_number)
                {
                    if (item.EditFlag) bEdit = true;

                    dl_code = item.DlCode;
                    subcode1 = item.DlSubcode;
                    service_name = item.ServiceName;
                    break;
                }
            }

            if (!bEdit) return;

            int selected_index = -1;
            if (InsServiceList != null && InsServiceList.Count > 0)
            {
                int index = -1;
                foreach (InsServiceItem item in InsServiceList)
                {
                    index++;
                    if (item.CODE == dl_code && item.Subcode1 == subcode1)
                    {
                        selected_index = index;
                        break;
                    }
                }
            }

            if (ListCallback != null)
            {
                if (!WasListOpened) ListCallback(row_number, selected_index);
                IsListOpened = true;
            }

            //....................................................................................................
            /*
            if (frmList != null)
            {
                if (frmList.WindowState == System.Windows.Forms.FormWindowState.Normal)
                {
                    frmList.Close();
                }
                if (!frmList.IsDisposed) frmList.Dispose();
                frmList = null;
            }
            if (frmList == null)
            {
                int ix = 20;
                int iy = row_number * 20 + 142;
                int x = ParentForm.Left + ix;
                int y = ParentForm.Top + iy;

                frmList = new frmSerchDogovorsShips(x, y, _cruiseArray, _shipArray, _settingsArray, SetShipFiltr, flgSee, flgRiver, SelectedCruise, SelectedShip);
                frmList.Show();
            }
            */
            //....................................................................................................
            /*
            if (wndList != null)
            {
                if (wndList.WindowState == System.Windows.WindowState.Normal)
                {
                    wndList.Close();
                }
                //if (!wndList.IsDisposed) wndList.Dispose();
                wndList = null;
            }
            if (wndList == null)
            {
                int ix = 20;
                int iy = row_number * 20 + 142;
                int x = ParentForm.Left + ix;
                int y = ParentForm.Top + iy;

                wndList = new ServiceEditorInsuranceList(x, y);
                wndList.Show();
            }
            */
            //....................................................................................................
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // On_ListClick
        //----------------------------------------------------------------------------------------------------
        #region InsuranceList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        private void InsuranceList_SelectionChanged(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // InsuranceList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        #region GroupList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        private void GroupList_SelectionChanged(object sender, EventArgs e)
        {
            int index = GroupListView.SelectedIndex;
            if (index < 0 || index >= InsGroupList.Count) return;
            InsGroupItem group = InsGroupList[index];
            if (group == null) return;

            InsGroupServiceList.Clear();

            foreach (InsServiceItem service in InsServiceList)
            {
                if (service.GroupNumber == group.GroupNumber)
                {
                    InsGroupServiceList.Add(service);
                    //service.FilterFlag = false;
                    CheckServiceFilter(service);

                }
            }

            if (InsuranceListView == null) return;

            InsuranceListView.ItemsSource = null;
            InsuranceListView.ItemsSource = InsGroupServiceList;
        }

        private bool CheckServiceFilter(InsServiceItem service)
        {
            service.FilterFlag = false;

            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.AddFlag)
                {
                    foreach (InsTouristItem item in InsTouristList)
                    {
                        if (item.Tukey == tourist.Tukey)
                        {
                            bool bFilter = service.CODE == item.DlCode;
                            if (!bFilter && !string.IsNullOrEmpty(item.ServiceName))
                            {
                                int pos = service.ServiceName.IndexOf(" ");
                                pos = service.ServiceName.IndexOf(" ", pos + 1);
                                string s1 = service.ServiceName.Substring(0, pos);
                                pos = item.ServiceName.IndexOf(" ");
                                pos = item.ServiceName.IndexOf(" ", pos + 1);
                                string s2 = item.ServiceName.Substring(0, pos);
                                bFilter = string.Compare(s1, s2) == 0;
                            }
                            if (bFilter)
                            {
                                service.FilterFlag = true;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // GroupList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        #region PoliciesList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        private void PoliciesList_SelectionChanged(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------------------------------------------
        #endregion // PoliciesList_SelectionChanged
        //----------------------------------------------------------------------------------------------------
        #region AddFlag_Checked
        //----------------------------------------------------------------------------------------------------
        private void AddFlag_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(CheckBox)) return;
            
            CheckBox box = (CheckBox)sender;
            int row_number = 0;
            string tag = box.Tag.ToString();
            //int key = 0;
            //if (!string.IsNullOrEmpty(tag)) key = int.Parse(tag);
            if (!string.IsNullOrEmpty(tag)) row_number = int.Parse(tag);
            if (row_number == 0) return;
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.RowNumber == row_number)
                {
                    Set_SelectedTourist(item.Tukey, row_number, (bool)box.IsChecked);
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddFlag_Checked
        //----------------------------------------------------------------------------------------------------
        #region AddFlag_Unchecked
        //----------------------------------------------------------------------------------------------------
        private void AddFlag_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(CheckBox)) return;

            CheckBox box = (CheckBox)sender;
            int row_number = 0;
            string tag = box.Tag.ToString();
            //int key = 0;
            //if (!string.IsNullOrEmpty(tag)) key = int.Parse(tag);
            if (!string.IsNullOrEmpty(tag)) row_number = int.Parse(tag);
            if (row_number == 0) return;
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            foreach (InsTouristItem item in InsTouristList)
            {
                if (item.RowNumber == row_number)
                {
                    Set_SelectedTourist(item.Tukey, row_number, (bool)box.IsChecked);
                }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddFlag_Unchecked
        //----------------------------------------------------------------------------------------------------
        #region Set_SelectedTourist
        //----------------------------------------------------------------------------------------------------
        private void Set_SelectedTourist(int tourist_key, int row_number, bool selected_flag)
        {
            if (InsTouristList == null) return;
            if (InsTouristList.Count == 0) return;

            bool bEdit = false;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.EditFlag) { bEdit = true; break; }
            }

            bool bAdd = false;
            bool bEnd = false;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.Tukey == tourist_key)
                {
                    if (bEdit)
                    {
                        if (tourist.RowNumber == row_number)
                            tourist.SelectedFlag = selected_flag;

                        if (tourist.EditFlag && !selected_flag) bEnd = true;
                    }
                    else
                    {
                        tourist.SelectedFlag = selected_flag;
                    }
                }
                if (tourist.AddFlag) bAdd = true;
            }

            if (IsAddingTouristMode)
            {
                if (bAdd)
                    AddButtonText = "Добавить  услугу";
                else
                    AddButtonText = "Выбрать  туриста  и  добавить  услугу";
            }

            if (!bAdd && !IsAddingMode && !IsAddingTouristMode) bEnd = true;
            if (bEnd) Edit_End();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // Set_SelectedTourist
        //----------------------------------------------------------------------------------------------------
        #region AddCheckBox_Checked
        //----------------------------------------------------------------------------------------------------
        private void AddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AddCheckBox_Accord();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddCheckBox_Checked
        //----------------------------------------------------------------------------------------------------
        #region AddCheckBox_Unchecked
        //----------------------------------------------------------------------------------------------------
        private void AddCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AddCheckBox_Accord();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddCheckBox_Unchecked
        //----------------------------------------------------------------------------------------------------
        #region AddCheckBox_Accord
        //----------------------------------------------------------------------------------------------------
        private void AddCheckBox_Accord()
        {
            bool bAdd = false;
            foreach (InsServiceItem item in InsServiceList)
            {
                if (item.AddFlag) 
                {
                    bAdd = true;
                    break;
                }
            }

            if (bAdd) 
                SaveButton.Visibility = System.Windows.Visibility.Visible;
            else
                SaveButton.Visibility = System.Windows.Visibility.Hidden;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddCheckBox_Accord
        //----------------------------------------------------------------------------------------------------
        #region AddButton_Click
        //----------------------------------------------------------------------------------------------------
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //if (AddStackPanel.Visibility == System.Windows.Visibility.Collapsed) return;
            //if (InsListGrid.Visibility == System.Windows.Visibility.Visible) return;

            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.EditFlag) { Edit_End(); break; }
            }

            if (!IsAddingTouristMode)
            {
                IsAddingTouristMode = true;

                //AddButton.Visibility = System.Windows.Visibility.Hidden;
                PreCancelButton.Visibility = System.Windows.Visibility.Visible;
                //AddButtonText = "Выбрать  услугу";

                NewPolicyButton.Visibility = System.Windows.Visibility.Hidden;
                CabinetPolicyButton.Visibility = System.Windows.Visibility.Hidden;

                if (InsTouristList != null)
                {
                    foreach (InsTouristItem tourist in InsTouristList)
                    {
                        tourist.CanEditFlag = false;
                        tourist.EditIconsFlag = false;
                        tourist.PolicyIconsFlagCopy = tourist.PolicyIconsFlag;
                        tourist.PolicyIconsFlag = false;
                        if (!tourist.PolicyExistsFlag)
                        {
                            tourist.AddControlFlag = tourist.CheckControlFlag;
                            tourist.AddingFlag = true;
                            tourist.AddVisibility = "Visible";
                        }
                        tourist.AccordEmpty();
                    }
                }

                return;
            }
            else
            {
                bool bAdd = false;

                if (InsTouristList != null)
                {
                    foreach (InsTouristItem tourist in InsTouristList)
                    {
                        if (tourist.AddFlag) bAdd = true;
                    }
                }
                if (!bAdd) return;
            }

            if (CollapsedCallback != null) CollapsedCallback(false);

            AddStackPanel.Visibility = System.Windows.Visibility.Collapsed;
            InsListGrid.Visibility = System.Windows.Visibility.Visible;

            if (InsGroupList.Count > 0) GroupListView.SelectedIndex = 0;

            IsAddingMode = true;
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // AddButton_Click
        //----------------------------------------------------------------------------------------------------
        #region SaveButton_Click
        //----------------------------------------------------------------------------------------------------
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // save changes

            GroupListView.SelectedIndex = -1;

            if (AddCallback != null) AddCallback();

//            if (ParentForm != null)  ParentForm.Close();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SaveButton_Click
        //----------------------------------------------------------------------------------------------------
        #region CancelButton_Click
        //----------------------------------------------------------------------------------------------------
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // cancel changes

            //if (CollapsedCallback != null) CollapsedCallback(true);

            Reset_Controls();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CancelButton_Click
        //----------------------------------------------------------------------------------------------------
        #region SaveEditButton_Click
        //----------------------------------------------------------------------------------------------------
        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            bool bEdit = false;
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.EditFlag) 
                {
                    if (!tourist.IsEditChanged())
                    {
                        //MessageBox.Show("Вы не внесли никаких изменений.", "Сообщение", MessageBoxButton.OK);
                        Edit_End();
                        return;
                    }
                    bEdit = true;
                    break; 
                }
            }
            if (!bEdit) return;

            //bool bSave = false;
            //if (MessageBox.Show("Вы хотите сохранить изменения ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes) bSave = true;

            //if (!bSave) return;
            
            //save changes
            if (SaveCallback != null) SaveCallback();

            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.EditFlag) { Edit_End(); break; }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // SaveEditButton_Click
        //----------------------------------------------------------------------------------------------------
        #region CancelEditButton_Click
        //----------------------------------------------------------------------------------------------------
        private void CancelEditButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (InsTouristItem tourist in InsTouristList)
            {
                if (tourist.EditFlag) { Edit_End( true ); break; }
            }
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CancelEditButton_Click
        //----------------------------------------------------------------------------------------------------
        #region NewPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        private void NewPolicyButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewPolicyCallback != null) NewPolicyCallback(0, false);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // NewPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        #region RealisPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        //private void RealisPolicyButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (RealisPolicyCallback != null) RealisPolicyCallback(0);
        //}

        //----------------------------------------------------------------------------------------------------
        #endregion // RealisPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        #region CabinetPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        private void CabinetPolicyButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewPolicyCallback != null) NewPolicyCallback(0, true);
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // CabinetPolicyButton_Click
        //----------------------------------------------------------------------------------------------------
        #region btnCreate_Click
        //----------------------------------------------------------------------------------------------------
        public void btnCreate_Click(object sender, EventArgs e)
        {
            //Create_Policy();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // btnCreate_Click
        //----------------------------------------------------------------------------------------------------
        #region btnAnnulate_Click
        //----------------------------------------------------------------------------------------------------
        public void btnAnnulate_Click(object sender, EventArgs e)
        {
            //Annulate_Policy();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // btnAnnulate_Click
        //----------------------------------------------------------------------------------------------------
        #region btnPrint_Click
        //----------------------------------------------------------------------------------------------------
        public void btnPrint_Click(object sender, EventArgs e)
        {
            //Print_Policy();
        }

        //----------------------------------------------------------------------------------------------------
        #endregion // btnPrint_Click
        //----------------------------------------------------------------------------------------------------
        #endregion // Controls events
        //====================================================================================================

    }
}
