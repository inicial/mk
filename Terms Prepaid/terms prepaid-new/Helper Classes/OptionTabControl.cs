using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.Model.Voucher;
using WpfControlLibrary.Util;
using WpfControlLibrary.ViewModel;

namespace terms_prepaid.Helper_Classes
{
    public class OptionTabControl
    {
        private OptionTabPage.GenerateServiceBlock _generateServiceBlock;
        public OptionTabPage CurrentPage { get; set; }
        public TabControl Control { get; set; }
        private Dictionary<frmNewOptions.TabType, OptionTabPage> _tabPageDictionary;
        private IEnumerable<TabPage> _allTabPages;

        public OptionTabControl(TabControl control, OptionTabPage.GenerateServiceBlock generateServiceBlock, IEnumerable<TabPage> allTabPages)
        {
            Control = control;
            _tabPageDictionary = new Dictionary<frmNewOptions.TabType, OptionTabPage>();
            _generateServiceBlock = generateServiceBlock;
            _allTabPages = allTabPages;
        }

        public void Add(OptionTabPage tabPage, frmNewOptions.TabType tabType, EventHandler cbSelectedIndexChanged)
        {
            tabPage.CbSelectedIndexChanged = cbSelectedIndexChanged;
            tabPage.generateServiceBlock = _generateServiceBlock;
            _tabPageDictionary.Add(tabType, tabPage);
        }

        public void SetPagesState()
        {
            Control.TabPages.Clear();
            Control.TabPages.AddRange(_allTabPages.ToArray());

            foreach (var page in _tabPageDictionary)
            {
                if (!page.Value.FillComboBox())
                {
                    Control.TabPages.Remove(page.Value.TabPage);
                    //page.Value.TabPage.Dispose();
                }
            }
        }

        public void Dispose()
        {
            foreach (var page in _allTabPages)
                if(!page.IsDisposed)
                    page.Dispose();
        }

        public OptionTabPage GetTabPage(frmNewOptions.TabType tabType)
        {
            return _tabPageDictionary[tabType];
        }

        public void GenerateAllFirstBlocks()
        {
            foreach (var page in _tabPageDictionary)
            {
                page.Value.GenerateFirstService();
            }

        }

        public void SelectPage(frmNewOptions.TabType tabType)
        {
            /*var tab = GetTabPage(tabType);
            if (tab != null && tab.TabPage != null)
            {
                foreach (var page in Control.TabPages)
                {
                    if(page.Equals(tab))
                        Control.SelectedTab = tab.TabPage;
                }
            }*/
        }
    }

    public class OptionTabPage
    {
        public delegate void GenerateServiceBlock(OptionTabPage pageControl, ServiceInfo service);

        public string url;
        public ServiceType ServiceType { get; set; }
        public TabPage TabPage { get; set; }
        public ComboBox Cb { get; set; }
        public List<ServiceInfo> ServiceList { get; set; }
        public ServiceInfo CurrentService { get; set; }

        public EventHandler CbSelectedIndexChanged { get; set; }

        public GenerateServiceBlock generateServiceBlock { get; set; }

        public ServiceTabViewModel ServiceTabViewModel { get; set; }

        public OptionTabPage(ServiceType serviceType, TabPage tabPage, ComboBox cb)
        {
            ServiceType = serviceType;
            TabPage = tabPage;
            Cb = cb;
            TabPage.Tag = this;
        }

        public bool FillComboBox()
        {
            if (ServiceList != null && ServiceList.Count > 0 )
            {
                if (Cb == null)
                    return true;

                Cb.DataSource = ServiceList;
                Cb.SelectedIndexChanged -= CbSelectedIndexChanged;
                Cb.SelectedIndexChanged += CbSelectedIndexChanged;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void GenerateFirstService()
        {
            if (ServiceList != null)
            {
                var service = ServiceList.FirstOrDefault();

                if (generateServiceBlock != null)
                    generateServiceBlock.Invoke(this, service);
            }
        }

    }
}
