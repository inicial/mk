using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using terms_prepaid.Forms;
using terms_prepaid.Helper_Classes;
using terms_prepaid.Helpers;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Voucher;
using System.Threading.Tasks;
using System.Web.UI;
using DataService;
using GalaSoft.MvvmLight.Messaging;
using WpfControlLibrary.Messages;
using WpfControlLibrary.Util;
using WpfControlLibrary.View;
using WpfControlLibrary.ViewModel;
using TextFormat = WpfControlLibrary.Util.TextFormat;
using UserControl = System.Windows.Forms.UserControl;


namespace terms_prepaid.UserControls
{

    public partial class ucDogovorSetting : UserControl, IDogovorSetting
    {
        public delegate void UpdateVaucher(bool aviaErrorCallback = false, ServiceType serviceType = ServiceType.Unknow);

        private UpdateVaucher _updateVaucher;

        public enum ModeType { vaucher, service };

        private readonly Color _colorEnabled = Color.White;
        private readonly Color _colorDisabled= Color.Silver;

        private readonly string _gbPaydPreaydName = @"Оплата\предоплата";
        private readonly string _gbDiscountName = "Скидки";
        private readonly string _gbStatusesName = "Статусы";

        private readonly string RoundFormat = "F0";

        private readonly int _mtbPaydTimeTopOffset = 4;

        private decimal Cost { get; set; }

        private string _dgcode = string.Empty;
        private int? _dlKey;

        private DataRow _rowDogovor = null;

        private IContractService _serv;

        private VoucherStatusView _voucherStatusView;

        private bool _prepaydVisible = true;
        int _oldTop1;
        int _oldTop1_2;
        int _oldTop2;
        int _dY = 50; 

        private string _rate;

        //public ServiceInfo ServiceInfo { get; private set; }
        public Service Service { get; private set; }

        private AccessClass _access;

        private Label _serviceTitle;

        private bool _discountEnabled = true;
        private bool _prePaymentEnabled = true;

        private VoucherViewModel _voucherViewModel;

        private DateTime _minDate = new DateTime(1900, 1, 1);

        private bool _dateInit;

        private ModeType _mode;
        private ModeType Mode 
        {
            get { return _mode; }
            set
            {
                if (_mode == value)
                    return;

                WpfServicesHost.Visible = false;

                
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Height = 440;

                switch (value)
                {
                    case ModeType.vaucher:
                        //flowLayoutPanel1.Controls.Add(gbPaydPreayd);
                        //flowLayoutPanel1.Controls.Add(gbDiscount);
                        flowLayoutPanel1.Controls.Add(pButtons);
                        flowLayoutPanel1.Controls.Add(gbStatuses);
                        flowLayoutPanel1.Controls.Add(gbServices);
                        break;

                    case ModeType.service:
                        flowLayoutPanel1.Controls.Add(_serviceTitle);
                        flowLayoutPanel1.Controls.Add(pButtons);
                        flowLayoutPanel1.Controls.Add(gbStatuses);
                        flowLayoutPanel1.Controls.Add(gbPaydPreayd);
                        flowLayoutPanel1.Controls.Add(gbDiscount);
                        break;
                }

                WpfServicesHost.Visible = false;
                WpfServicesHost.Visible = true;

                _mode = value;
            }
        }

        public ucDogovorSetting(UpdateVaucher updateVaucher, VoucherViewModel voucherViewModel)
        {
            _serv = Repository.GetInstance<IContractService>();
            InitializeComponent();

            dtpPrePaymentDate.MinDate = _minDate;
            dtpPaymentDate.MinDate = _minDate;

            _voucherViewModel = voucherViewModel;
            InitWpf();

            _serviceTitle = new Label();
            flowLayoutPanel1.Controls.Add(_serviceTitle);
            _serviceTitle.Visible = false;
            _serviceTitle.AutoSize = true;
            _serviceTitle.BackColor = Color.BurlyWood;
            _serviceTitle.Margin = new Padding(5, 5, 5, 5);
            _serviceTitle.Height = 20;
            _serviceTitle.Width = 500;
            _serviceTitle.Dock = DockStyle.Top;

            if (voucherViewModel.Voucher != null) _dgcode = voucherViewModel.Voucher.DgCode;

            pButtons.Dock = DockStyle.Top;
            gbStatuses.Dock = DockStyle.Top;
            gbPaydPreayd.Dock = DockStyle.Top;
            gbDiscount.Dock = DockStyle.Top;
            _updateVaucher = updateVaucher;
            //SetDogovorSetting();
        }

        public new void Dispose()
        {
            pButtons.Dispose();
            gbStatuses.Dispose();
            gbServices.Dispose();
            _serviceTitle.Dispose();
            gbPaydPreayd.Dispose();
            gbDiscount.Dispose();
            //gbStatuses.Dispose();
            
            DeregisterEvents();
        }

        private void InitWpf()
        {
            _voucherStatusView = new VoucherStatusView { DataContext = _voucherViewModel };
            _voucherStatusView.InitializeComponent();
            WpfServicesHost.Child = _voucherStatusView;
        }

        private void SetDiscountControlsState(bool enabled)
        {
            _discountEnabled = enabled;
            tbDiscount.ReadOnly = !enabled;
            tbDiscountSum.ReadOnly = !enabled;
            btnSaveDiscoount.Enabled = enabled;

            tbDiscount.BorderStyle = enabled ?  BorderStyle.Fixed3D : BorderStyle.None;
            tbDiscountSum.BorderStyle = enabled ? BorderStyle.Fixed3D : BorderStyle.None;

            tbDiscount.BackColor = enabled ? Color.White : Color.SandyBrown;
            tbDiscountSum.BackColor = enabled ? Color.White : Color.SandyBrown;

            tbDiscount.Top = enabled ? 10 : 16;
            tbDiscountSum.Top = tbDiscount.Top;
        }

        private void SetPrePaymentControlsState(bool enabled)
        {
            if (_prePaymentEnabled == enabled)
                return;

            _prePaymentEnabled = enabled;

            dtpPaymentDate.Enabled = enabled;
            mtbPaydTime.ReadOnly = !enabled;

            mtbPaydTime.BorderStyle = enabled ? BorderStyle.Fixed3D : BorderStyle.None;
            mtbPaydTime.BackColor = enabled ? Color.White : Color.SandyBrown;
        }

        private string GetPanelName(string baseName, int svKey, int idType)
        {
            string additionName;

            switch ((Voucher.SvType)svKey)
            {
                case Voucher.SvType.Avia:
                    additionName = "авиаперелету";
                    break;

                case Voucher.SvType.Cruise:
                    additionName = "круизу";
                    break;

                case Voucher.SvType.Insur:
                    additionName = "страховке";
                    break;

                case Voucher.SvType.Visa:
                    additionName = "визе";
                    break;

                default:
                    additionName = "услуге";
                    break;
            }

            if (idType == (int)Voucher.IdTypes.Cruise)
                additionName = "круизу";

            return String.Format("{0} по {1}", baseName, additionName);
        }

        public void DeregisterEvents()
        {
            btnSave.Click -= btnSaveService_Click;
            btnSave.Click -= btnSave_Click;

            btnSaveDiscoount.Click -= btnSaveDiscount_Click;
            btnSaveDiscoount.Click -= btnSaveDiscountService_Click;

            btnSaveStatus.Click -= btnSaveStatus_Click;
            btnSaveStatus.Click -= btnSaveStatusService_Click;

            cbStatuses.SelectedValueChanged -= cbStatuses_SelectedValueChanged;
            cbStatuses.SelectedValueChanged -= cbStatuses_ServiceSelectedValueChanged;
        }

        public void SetService(Service service)
        {
            //SetDiscountControlsState(_access.isSuperViser || _accessisBronir || (_access.isRealize && service.SType == ServiceType.Cruise));
            //SetPrePaymentControlsState((_access.isSuperViser || _access.isBron || _access.isBronir) && service.SType != ServiceType.Avia);
            SetPrePaymentControlsState(true);
            //btnSave.Enabled = service.SType != ServiceType.Avia;

            Service = service;

            ServiceSetting ss = null;

            try
            {
                if (Service.Group != null)
                  ss = Service.Group.Setting;
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
                ss = Service.ServiceSetting;
            }

            //ServiceSetting ss = _serv.GetServiceSetting(_dgcode, service.dlkey);
            
            //ServiceInfo = serviceInfo;

            if (ss == null)
            {
                Mode = ModeType.service;
                _dlKey = null;
            }
            else
            {
                DeregisterEvents();

                btnSave.Click += btnSaveService_Click;
                btnSaveDiscoount.Click += btnSaveDiscountService_Click;
                btnSaveStatus.Click += btnSaveStatusService_Click;
                cbStatuses.SelectedValueChanged += cbStatuses_ServiceSelectedValueChanged;

                Mode = ModeType.service;

                SetPaymentSetting(Service, ss.Payment);
                SetDiscountSetting(ss.Discount);
                SetStatusSetting(Service, ss.Status);

                _dlKey = ss.DlKey;

                //service is AviaService ? ((AviaService)service).BookingAvia.Route : service.ServiceName

                _serviceTitle.Text = Service.FullName ??
                                     (Service.ServType == ServiceType.Avia
                                         ? "Авиаперелет: " + ((AviaService) service).BookingAvia.Route
                                         : service.ServiceName);
                //_serviceTitle.Text = Service.FullName ?? (ServiceInfo.SType == ServiceType.Avia ? "Авиаперелет: " + ServiceInfo.dl_name : ServiceInfo.dl_name);

                _serviceTitle.Visible = true;
                
                cbStatuses.DataSource = _serv.GetStatusesForService(Service.SvKey);
                cbStatuses.DisplayMember = "name";
                cbStatuses.ValueMember = "key";
                cbStatuses.SelectedValue = ss.Status.StatusValue;

                SetPrepaydVisibility(Service.ServType != ServiceType.Avia);

                gbStatuses.Text = GetPanelName(_gbStatusesName, Service.SvKey, Service.TypeId);
                gbPaydPreayd.Text = GetPanelName(_gbPaydPreaydName, Service.SvKey, Service.TypeId);
                gbDiscount.Text = GetPanelName(_gbDiscountName, Service.SvKey, Service.TypeId);
            }
            this.AutoScrollPosition = new Point(0, 0);
        }

        public void SetVoucher(int serviceCount)
        {
            //SetDiscountControlsState(true);
            SetPrePaymentControlsState(true);
            btnSave.Enabled = true;
            //SetDiscountControlsState(_access.isSuperViser);

            DeregisterEvents();

            btnSave.Click += btnSave_Click;
            btnSaveDiscoount.Click += btnSaveDiscount_Click;
            btnSaveStatus.Click += btnSaveStatus_Click;
            cbStatuses.SelectedValueChanged += cbStatuses_SelectedValueChanged;

            Mode = ModeType.vaucher;

            SetPrepaydVisibility(true);

            GetVaucher();

            gbStatuses.Text = _gbStatusesName;
            gbPaydPreayd.Text = _gbPaydPreaydName;
            gbDiscount.Text = _gbDiscountName;

            gbServices.Text = string.Format("Услуги ({0})", serviceCount);
            this.AutoScrollPosition = new Point(0, 0);
        }

        private void btnSaveService_Click(object sender, EventArgs e)
        {
            if(_dlKey == null)
                return;

            MyTime ppaymenttime = MyTime.ParseTime(mtbPrePaydTime.Text, "Предоплата до");
            MyTime paymenttime = MyTime.ParseTime(mtbPaydTime.Text, "Оплата до");
            if (ppaymenttime == null || paymenttime == null) { return; }

            _serv.UpdateDogovorLinePaymentSetting(_dgcode, (int)_dlKey, cbIsprocentPrePayd.Checked, cbIsprocentPrePayd.Checked ? decimal.Parse(tbPrePaid.Text) : decimal.Parse(tbPrePaydSum.Text),
                                              dtpPrePaymentDate.Checked
                                                  ? (object)dtpPrePaymentDate.Value.Date.AddHours(ppaymenttime.hour)
                                                                  .AddMinutes(ppaymenttime.minute)
                                                  : DBNull.Value,
                                              dtpPaymentDate.Checked
                                                  ? (object)dtpPaymentDate.Value.Date.AddHours(paymenttime.hour)
                                                                     .AddMinutes(paymenttime.minute)
                                                  : DBNull.Value, "test");

            /*if (_updateVaucher != null)
                _updateVaucher.Invoke(serviceType: ServiceInfo.SType);*/

            if (_updateVaucher != null)
                _updateVaucher.Invoke(serviceType: Service.ServType);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MyTime ppaymenttime = MyTime.ParseTime(mtbPrePaydTime.Text, "Предоплата до");
            MyTime paymenttime = MyTime.ParseTime(mtbPaydTime.Text, "Оплата до");
            if(ppaymenttime==null||paymenttime==null){return;}

            
            _serv.UpdateDogovorSetting(_dgcode, cbIsprocentPrePayd.Checked, cbIsprocentPrePayd.Checked ? decimal.Parse(tbPrePaid.Text) : decimal.Parse(tbPrePaydSum.Text),
                                              dtpPrePaymentDate.Checked
                                                  ? (object)dtpPrePaymentDate.Value.Date.AddHours(ppaymenttime.hour)
                                                                  .AddMinutes(ppaymenttime.minute)
                                                  : DBNull.Value,
                                              dtpPaymentDate.Checked
                                                  ? (object)dtpPaymentDate.Value.Date.AddHours(paymenttime.hour)
                                                                     .AddMinutes(paymenttime.minute)
                                                  : DBNull.Value);
            SetDogovorSetting();
        }

        private void btnSaveDiscountService_Click(object sender, EventArgs e)
        {
            if (_dlKey == null)
                return;

            decimal discountFirst = cbIsProcentCommision.Checked
                ? decimal.Parse(tbDiscount.Text)
                : decimal.Parse(tbDiscountSum.Text);

            _serv.UpdateDogovorLineDiscountSetting(_dgcode, (int)_dlKey, cbIsProcentCommision.Checked,
                discountFirst);

            if (Service != null && Service.RelatedServices != null)
            {
                decimal discount = cbIsProcentCommision.Checked ? decimal.Parse(tbDiscount.Text) : 0;
                foreach (var relatedService in Service.RelatedServices)
                {
                    _serv.UpdateDogovorLineDiscountSetting(_dgcode, relatedService.DlKey, 
                        cbIsProcentCommision.Checked, discount);
                }
            }

            _serv.DogovorRecalc(_dgcode);

            /*if (_updateVaucher != null)
                _updateVaucher.Invoke(serviceType: ServiceInfo.SType);*/

            if (_updateVaucher != null)
                _updateVaucher.Invoke(serviceType: Service.ServType);
        }

        private void btnSaveDiscount_Click(object sender, EventArgs e)
        {
            _serv.UpdateDiscountDogovor(_dgcode, decimal.Parse(tbDiscount.Text));
            SetDogovorSetting();
        }

        private void btnSaveStatusService_Click(object sender, EventArgs e)
        {
            if (_dlKey == null)
                return;

            _serv.UpdateDogovorLineStatusSetting(_dgcode, (int) _dlKey, (int) cbStatuses.SelectedValue, "");

            if (Service != null && Service.RelatedServices != null)
                foreach (var relatedService in Service.RelatedServices)
                {
                    _serv.UpdateDogovorLineStatusSetting(_dgcode, (int)relatedService.DlKey,
                        (int) cbStatuses.SelectedValue, "");
                }

            /*if (_updateVaucher != null)
                _updateVaucher.Invoke(serviceType: ServiceInfo.SType);*/

            if (_updateVaucher != null)
                _updateVaucher.Invoke(serviceType: Service.ServType);

            //Messenger.Default.Send(new DlKeyInfo(_Service.DlKey));
        }

        private void btnSaveStatus_Click(object sender, EventArgs e)
        {
            if (cbStatuses.SelectedValue != null)
            {
                _serv.UpdateStatusDogovor(_dgcode, (int) cbStatuses.SelectedValue);
                SetDogovorSetting();
            }
        }

        public void SetDogovorSetting()
        {
            _dgcode = _voucherViewModel.Voucher.DgCode;
            _rowDogovor = _serv.GetDogovorSettings(_dgcode);

            if (_rowDogovor.Field<int>("DG_SOR_CODE") == 2)
            {
                dtpPaymentDate.Enabled = false;
                dtpPrePaymentDate.Enabled = false;
                dtpVisaDate.Enabled = false;
                cbStatuses.SelectedValue = 2;
                cbStatuses.Enabled = false;
                tbDiscount.Enabled = false;
                tbPrePaid.Enabled = false;
                cbIsProcentCommision.Enabled = false;
                btnSave.Enabled = false;
                mtbPaydTime.Enabled = false;
                mtbPrePaydTime.Enabled = false;
                btnSaveDiscoount.Enabled = false;
                btnSaveStatus.Enabled = false;
                tbPrePaydSum.Enabled = false;
            }

            GetVaucher();
        }

        private void GetVaucher()
        {
            _rate = " " + _rowDogovor.Field<string>("DG_RATE");

            Cost = _rowDogovor.Field<decimal>("DG_PRICE");

            GetDiscount();
            GetStatus();
            GetPrePayd();
        }

        private void SetPaymentSetting(Service service, PaymentSetting p)
        {
            _dateInit = false;
            Cost = p.PaymentValue.Cost;

            cbIsprocentPrePayd.Checked = p.PrePaymentValue.ValueType == 1;
            SetPrePaydTextBoxState();

            dtpPaymentDate.MinDate = DateTimePicker.MinDateTime;
            dtpPrePaymentDate.MinDate = DateTimePicker.MinDateTime;
            dtpPaymentDate.MaxDate = DateTimePicker.MaxDateTime;
            dtpPrePaymentDate.MaxDate = DateTimePicker.MaxDateTime;

            // Оплата - дата
            lbPaymentDate.Text = TextFormat.GetDate(p.PaymentDate.DateValue);
            lbPaymentDateD.Text = p.PaymentDate.DateChange;
            lbPaymentDateM.Text = p.PrePaymentDate.Manager;

            dtpPaymentDate.Checked = p.PaymentDate.DateValue != null;
            if (p.PaymentDate.DateValue != null)
                dtpPaymentDate.Value = (DateTime)p.PaymentDate.DateValue;
            
            mtbPaydTime.Text = p.PaymentDate.DateValue != null ? ((DateTime)p.PaymentDate.DateValue).ToString("HH:mm") : "";
            lbPaymentDateM.Text = p.PrePaymentDate.Manager;

            // Предоплата - дата
            lbPrePaydeDate.Text = TextFormat.GetDate(p.PrePaymentDate.DateValue);
            lbPPaymentDateD.Text = p.PrePaymentDate.DateChange;
            lbPPaymentDateM.Text = p.PrePaymentDate.Manager;

            dtpPrePaymentDate.Checked = p.PrePaymentDate.DateValue != null;

            try
            {
                if (p.PrePaymentDate.DateValue != null)
                    dtpPrePaymentDate.Value = (DateTime) p.PrePaymentDate.DateValue;
            }
            catch (ArgumentException e)
            {
                dtpPrePaymentDate.Value = dtpPrePaymentDate.MaxDate;
                MessageBox.Show(String.Format("Ошибка данных услуги \"{0}\": дата предоплаты не может быть больше даты полной оплаты! \n\nДата предоплаты принудительно установлена равной дате оплаты. \n\nНажмите кнопку \"Сохранить\" для внесения изменений!", service.ServiceName), "Неверная дата предоплаты услуги", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            //dtpPrePaymentDate.Value = p.PrePaymentDate.DateValue ?? _minDate;
            mtbPrePaydTime.Text = p.PrePaymentDate.DateValue != null ? ((DateTime)p.PrePaymentDate.DateValue).ToString("HH:mm") : "";
            lbPPaymentDateM.Text = p.PrePaymentDate.Manager;

            // Предоплата - сумма
            lbPrePayd.Text = p.PrePaymentValue.NumericValue.ToString(RoundFormat) + _rate;
            lbPrePaydD.Text = p.PrePaymentValue.DateChange;
            lbPrePaydM.Text = p.PrePaymentValue.Manager;

            tbPrePaid.Text = p.PrePaymentValue.ProcentValue.ToString(RoundFormat);
            tbPrePaydSum.Text = p.PrePaymentValue.NumericValue.ToString(RoundFormat);

            lbPrePaydEu.Text = _rate;

            //var paymentValue = p.PaymentValue.NumericValue;
            var paymentValue = Cost;

            if (service.Voucher.VoucherSettingInfo.Course != 1 && service is AviaService)
                lbPrice.Text = String.Format("{0}{1} ({2} руб)", paymentValue.ToString(RoundFormat), _rate, Math.Round(service.Voucher.VoucherSettingInfo.Course * paymentValue, 2));
            else
            {
                lbPrice.Text = paymentValue.ToString(RoundFormat) + _rate;
            }

            // Оплата - сумма
            //lbPrice.Text = p.PaymentValue.NumericValue.ToString(RoundFormat) + _rate;
            lbPriceD.Text = p.PaymentValue.DateChange;
            lbPriceM.Text = p.PaymentValue.Manager;
            _dateInit = true;
        }

        private void SetDiscountSetting(DiscountSetting d)
        {
            //gbDiscount.Text = "Скидки по услуге";

            //Cost = d.DiscountValue.Cost;
            cbIsProcentCommision.Checked = d.DiscountValue.ValueType == 1;
            SetCommisionTextBoxState();

            // Скидка - сумма
            lbDiscount.Text = d.TrueNumericValue.ToString(RoundFormat) + _rate;
            lbDiscountD.Text = d.DiscountValue.DateChange;
            lbDiscountM.Text = d.DiscountValue.Manager;

            tbDiscount.Text = d.DiscountValue.ProcentValue.ToString(RoundFormat);
            tbDiscountSum.Text = d.TrueNumericValue.ToString(RoundFormat);
            lbDiscountM.Text = d.DiscountValue.Manager;

            lbDiscountEu.Text = _rate;
        }

        private void SetStatusSetting(Service serv, StatusSetting s)
        {
            //gbStatuses.Text = "Статус по услуге";

            // Статус
            if (serv.SvKey == 3184)
            {
                var avia = serv as AviaService;
                lbStatus.Text = avia.GetLastAviaStatus();
                lbStatusD.Text = TextFormat.GetDate(avia.GetLastAviaDateOfStatusChange());
                lbStatusM.Text = "";
            }
            else
            {
                lbStatus.Text = s.StatusName;
                if (serv.SvKey == 6)
                {
                    InshurService ins = serv as InshurService;
                    lbStatus.Text = ins.GetInshurStatus();
                }
                lbStatusD.Text = s.DateChange;
                lbStatusM.Text = s.Manager;
            }
           
            
            cbStatuses.DisplayMember = "name";
            cbStatuses.ValueMember = "key";
            cbStatuses.SelectedValue = s.StatusValue;
        }

        private void GetPrePayd()
        {
            _dateInit = false;
            dtpPaymentDate.MinDate = DateTimePicker.MinDateTime;
            dtpPrePaymentDate.MinDate = DateTimePicker.MinDateTime;
            dtpPaymentDate.MaxDate = DateTimePicker.MaxDateTime;
            dtpPrePaymentDate.MaxDate = DateTimePicker.MaxDateTime;

            if (_rowDogovor["DG_VISADATE"].Equals(DBNull.Value))
            {
                dtpVisaDate.Checked = false;
            }
            else
            {
                dtpVisaDate.Checked = true;
                dtpVisaDate.Value = _rowDogovor.Field<DateTime>("DG_VISADATE");
            }


            lbPrePaydEu.Text = _rowDogovor.Field<string>("DG_RATE");

            lbPPaymentDateD.Text = TextFormat.GetDate(_rowDogovor.Field<DateTime?>("PPAYMENTDATEWHY"));
            lbPaymentDateD.Text = TextFormat.GetDate(_rowDogovor.Field<DateTime?>("PAYMENTDATEWHY"));
            lbPrePaydD.Text = TextFormat.GetDate(_rowDogovor.Field<DateTime?>("Razmer_PWHY"));
            lbPriceD.Text = TextFormat.GetDate(_rowDogovor.Field<DateTime?>("PriceWHY"));
 
            lbPrePayd.Text =
                (_rowDogovor.Field<int>("DG_PROCENT") == 1
                     ? (_rowDogovor.Field<decimal?>("DG_PRICE") ?? 0) * (_rowDogovor.Field<decimal?>("DG_RazmerP") ?? 0) / 100
                     : (_rowDogovor.Field<decimal?>("DG_RazmerP") ?? 0)).ToString(RoundFormat) + " " +
                _rowDogovor.Field<string>("DG_RATE");
            
            lbPrice.Text = (_rowDogovor.Field<decimal?>("DG_Price") ?? 0).ToString(RoundFormat) + " " +
                           _rowDogovor.Field<string>("DG_RATE");
            
            lbPriceM.Text = _rowDogovor.Field<string>("PRICEWHO");

            lbPPaymentDateM.Text = _rowDogovor.Field<string>("PPAYMENTDATEWHO");
            lbPaymentDateM.Text = _rowDogovor.Field<string>("PAYMENTDATEWHO");
            lbPrePaydM.Text = _rowDogovor.Field<string>("Razmer_PWHO");

            if (_rowDogovor["DG_PAYMENTDATE"].Equals(DBNull.Value))
            {
                dtpPaymentDate.Checked = false;
                lbPaymentDate.Text = "";
            }
            else
            {
                dtpPaymentDate.Checked = true;
                lbPaymentDate.Text = TextFormat.GetDate(_rowDogovor.Field<DateTime?>("DG_PAYMENTDATE"));
                dtpPaymentDate.Value = _rowDogovor.Field<DateTime>("DG_PAYMENTDATE").Date;
                mtbPaydTime.Text = _rowDogovor.Field<DateTime>("DG_PAYMENTDATE").ToString("HH:mm");
            }
            if (_rowDogovor["DG_PPAYMENTDATE"].Equals(DBNull.Value))
            {
                dtpPrePaymentDate.Checked = false;
                lbPrePaydeDate.Text = "";
            }
            else
            {
                dtpPrePaymentDate.Checked = true;
                lbPrePaydeDate.Text = TextFormat.GetDate(_rowDogovor.Field<DateTime>("DG_PPAYMENTDATE"));

                try
                {
                    dtpPrePaymentDate.Value = _rowDogovor.Field<DateTime>("DG_PPAYMENTDATE").Date;
                }
                catch (ArgumentException e)
                {
                    dtpPrePaymentDate.Value = dtpPrePaymentDate.MaxDate;
                    MessageBox.Show(String.Format("Ошибка данных путевки: дата предоплаты не может быть больше даты полной оплаты! \n\nДата предоплаты принудительно установлена равной дате оплаты. \n\nНажмите кнопку \"Сохранить\" для внесения изменений!"), "Неверная дата предоплаты путевки", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                mtbPrePaydTime.Text = _rowDogovor.Field<DateTime>("DG_PPAYMENTDATE").ToString("HH:mm");
            }

            var dogovorPrice = _rowDogovor.Field<decimal?>("DG_PRICE") ?? 0;
            var dogovorRazmerP = _rowDogovor.Field<decimal?>("DG_RazmerP") ?? 0;

            if (_rowDogovor.Field<int>("DG_PROCENT") == 1)
            {
                cbIsprocentPrePayd.Checked = true;
                SetPrePaydTextBoxState();
                tbPrePaid.Text = dogovorRazmerP.ToString(RoundFormat);
                tbPrePaydSum.Text = (dogovorRazmerP * dogovorPrice / 100).ToString(RoundFormat);
            }
            else
            {
                cbIsprocentPrePayd.Checked = false;
                SetPrePaydTextBoxState();

                tbPrePaydSum.Text = dogovorRazmerP.ToString(RoundFormat); ;
                
                try
                {
                    tbPrePaid.Text = (dogovorRazmerP / dogovorPrice * 100).ToString(RoundFormat);
                }
                catch (DivideByZeroException)
                {
                    tbPrePaid.Text = "0";
                }
            }
            _dateInit = true;
        }

        private void GetStatus()
        {
            var status = _rowDogovor.Field<string>("NS_Name");
            DateTime date = _serv.GetDateForStatus(_dgcode, _rowDogovor.Field<string>("NS_QUERYFORDATE"));
            lbStatus.Text = _rowDogovor.Field<string>("NS_Name") + " " + TextFormat.GetDate(date);

            lbStatusD.Text = TextFormat.GetDate(_rowDogovor.Field<DateTime?>("StatusWHY"));
            
            lbStatusM.Text = _rowDogovor.Field<string>("StatusWHO");

            cbStatuses.SelectedValueChanged -= cbStatuses_SelectedValueChanged;
            DataTable dt = _serv.GetStatuses();

            cbStatuses.DataSource = dt;
            cbStatuses.DisplayMember = "name";
            cbStatuses.ValueMember = "key";

            var statuses = dt.Select().Select(r => r.Field<string>("name")).ToList();
            int index = statuses.IndexOf(status);
            cbStatuses.SelectedIndex = index;

            if (index == -1) cbStatuses.ResetText();
            
            btnSaveStatus.Enabled = false;
            cbStatuses.SelectedValueChanged += cbStatuses_SelectedValueChanged;

            ArrayList rows = new ArrayList();

            foreach (DataRow dataRow in dt.Rows)
                rows.Add(string.Join(";", dataRow.ItemArray.Select(item => item.ToString())));

            ArrayList arrayList = new ArrayList();
            //create arraylsit from DataTable
            foreach (DataRow dr in dt.Rows)
            {
                arrayList.Add(dr["Name"]);
            }

            //cbStatuses.SelectedIndex = -1;

            //cbStatuses.SelectedValue = _rowDogovor.Field<int>("DA_STATUS");
        }

        private void GetDiscount()
        {
            //gbDiscount.Text = "Скидки";

            lbDiscount.Text = (_rowDogovor.Field<decimal?>("DG_DISCOUNTSUM") ?? 0).ToString(RoundFormat) + " " +
                              _rowDogovor.Field<string>("DG_RATE");

            lbDiscountD.Text = TextFormat.GetDate(_rowDogovor.Field<DateTime?>("DISCOUNTWHY"));

            lbDiscountM.Text = _rowDogovor.Field<string>("DISCOUNTWHO");

            var discountSum = _rowDogovor.Field<decimal>("DG_DISCOUNTSUM");
            var discount = _rowDogovor.Field<decimal?>("DG_DISCOUNT") ?? 0;

            if (_rowDogovor.Field<Int16>("DG_TYPECOUNT") == 1)
            {
                cbIsProcentCommision.Checked = true;
                SetCommisionTextBoxState();
                tbDiscount.Text = discount.ToString(RoundFormat);
                tbDiscountSum.Text = (discount * Cost / 100).ToString(RoundFormat);
            }
            else
            {
                cbIsProcentCommision.Checked = false;
                SetCommisionTextBoxState();
                tbDiscountSum.Text = discountSum.ToString(RoundFormat);

                try
                {
                    tbDiscount.Text = (discountSum / Cost * 100).ToString(RoundFormat);
                }
                catch (DivideByZeroException)
                {
                    tbDiscountSum.Text = "0";
                }
            }
        }

        void SetAnnulation()
        {
            if (_rowDogovor.Field<decimal>("DG_PAYED") > 0)
            {
                MessageBox.Show("Аннуляция невозможна так как путевка оплачена!","", MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
            else
            {
                int? idReason = frmAnnulationReasons.GetReason();
                if (idReason != null)
                {
                   if (
                       MessageBox.Show("Вы уверены что хотите аннулировать путевку?", "", MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question) == DialogResult.Yes)
                   {
                       _serv.SetAnnulateDogovor(_dgcode, idReason.Value);
                   }
                }
            }
        }

        private void cbStatuses_ServiceSelectedValueChanged(object sender, EventArgs e)
        {
            btnSaveStatus.Enabled = true;
        }

        private void cbStatuses_SelectedValueChanged(object sender, EventArgs e)
        {
            btnSaveStatus.Enabled = true;
            try
            {
                if (cbStatuses.SelectedValue.Equals(2) && _rowDogovor.Field<int>("DG_SOR_CODE") != 2)
                {
                    SetAnnulation();
                    SetDogovorSetting();
                }
                else
                {
                    lbStatusesDateNew.Text = _serv.GetDateForStatus(_dgcode, (int) cbStatuses.SelectedValue).ToString("dd.MM.yy  HH:mm");
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }

        private void tbPrePaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!ValidationHelper.Validate(((TextBox)sender).Text, e.KeyChar))
                e.Handled = true;
        }

        private void tbPrePaid_TextChanged(object sender, EventArgs e)
        {
            if (cbIsprocentPrePayd.Checked)
            {
                try
                {
                    tbPrePaydSum.Text = (decimal.Parse(tbPrePaid.Text) * Cost / 100).ToString(RoundFormat);
                }
                catch (Exception)
                {
           
                }

            }
        }

        private void tbDiscountSum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!ValidationHelper.Validate(((TextBox)sender).Text, e.KeyChar))
                e.Handled = true;
        }

        private void tbDiscount_TextChanged(object sender, EventArgs e)
        {
            if (_dlKey == null) return;
            
            if (cbIsProcentCommision.Checked)
            {
                /*try
                {*/
                decimal discountProcent = decimal.Parse(tbDiscount.Text);
                var discountNumeric = _serv.CalculateDogovorLineDiscountSetting(_dgcode, (int)_dlKey, discountProcent);
                tbDiscountSum.Text = discountNumeric.ToString(RoundFormat);
                    //tbDiscountSum.Text = (decimal.Parse(tbDiscount.Text)*Cost/100).ToString(RoundFormat);
                /*}
                catch (Exception)
                {

                }*/
            }
        }

        private void tbDiscountSum_TextChanged(object sender, EventArgs e)
        {
            if (!cbIsProcentCommision.Checked)
            {
                try
                {
                    tbDiscount.Text =
                        (decimal.Parse(tbDiscountSum.Text) / Cost * 100).ToString(RoundFormat);

                }
                catch (DivideByZeroException)
                {
                    tbDiscount.Text = "0";
                }

            }
        }

        private void SetPrePaydTextBoxState()
        {
            if (tbPrePaid.IsDisposed)
                return;

            if (cbIsprocentPrePayd.Checked)
            {
                tbPrePaid.ReadOnly = false;
                tbPrePaydSum.ReadOnly = true;
                tbPrePaid.BackColor = _colorEnabled;
                tbPrePaydSum.BackColor = _colorDisabled;
            }
            else
            {
                tbPrePaid.ReadOnly = true;
                tbPrePaydSum.ReadOnly = false;
                tbPrePaid.BackColor = _colorDisabled;
                tbPrePaydSum.BackColor = _colorEnabled;
            }
        }


        private void cbIsprocentPrePayd_CheckedChanged(object sender, EventArgs e)
        {
            SetPrePaydTextBoxState();
        }

        private void SetCommisionTextBoxState()
        {
            if (_discountEnabled && !tbDiscount.IsDisposed)
            {
                if (cbIsProcentCommision.Checked)
                {
                    tbDiscount.ReadOnly = false;
                    tbDiscountSum.ReadOnly = true;
                    tbDiscount.BackColor = _colorEnabled;
                    tbDiscountSum.BackColor = _colorDisabled;
                }
                else
                {
                    tbDiscount.ReadOnly = true;
                    tbDiscountSum.ReadOnly = false;
                    tbDiscount.BackColor = _colorDisabled;
                    tbDiscountSum.BackColor = _colorEnabled;
                }
            }
        }

        private void cbIsProcentCommision_CheckedChanged(object sender, EventArgs e)
        {
            SetCommisionTextBoxState();
        }

        private void tbPrePaid_Click(object sender, EventArgs e)
        {
            cbIsprocentPrePayd.Checked = true;
        }

        private void tbPrePaydSum_TextChanged(object sender, EventArgs e)
        {
            if (!cbIsprocentPrePayd.Checked)
            {
                try
                {
                    tbPrePaid.Text =
                        (decimal.Parse(tbPrePaydSum.Text) / Cost * 100).ToString(RoundFormat);

                }
                catch (DivideByZeroException)
                {
                    tbPrePaid.Text = "0";
                }

            }
        }

        private void tbDiscount_Click(object sender, EventArgs e)
        {
            cbIsProcentCommision.Checked = true;
        }

        private void tbDiscountSum_Click(object sender, EventArgs e)
        {
            cbIsProcentCommision.Checked = false;
        }

        private void tbPrePaydSum_Click(object sender, EventArgs e)
        {
            cbIsprocentPrePayd.Checked = false;
        }

        private void SetPrepaydVisibility(bool state)
        {
            lbPrePaydDateCaption.Visible = state;
            lbPrePaydeDate.Visible = state;
            lbPPaymentDateD.Visible = state;
            lbPPaymentDateM.Visible = state;
            dtpPrePaymentDate.Visible = state;
            mtbPrePaydTime.Visible = state;

            lbPrePaydCaption.Visible = state;
            lbPrePayd.Visible = state;
            lbPrePaydD.Visible = state;
            lbPrePaydM.Visible = state;
            tbPrePaid.Visible = state;
            lbPrePaydProcent.Visible = state;
            tbPrePaydSum.Visible = state;
            lbPrePaydEu.Visible = state;

            if (state != _prepaydVisible)
            {
                if (state)
                    BringBack();
                else
                    LiftUp();
            }

            _prepaydVisible = state;
            
        }

        private void LiftUp()
        {
            _oldTop1 = lbPaydDateCaption.Top;
            _oldTop1_2 = dtpPaymentDate.Top;
            _oldTop2 = lbPaydCaption.Top;

            int newTop1 = lbPrePaydDateCaption.Top;
            int newTop1_2 = dtpPrePaymentDate.Top;
            int newTop2 = lbPaydDateCaption.Top;

            lbPaydDateCaption.Top = newTop1;
            lbPaymentDate.Top = newTop1; 
            lbPaymentDateD.Top = newTop1;
            lbPaymentDateM.Top = newTop1;

            dtpPaymentDate.Top = newTop1_2;
            mtbPaydTime.Top = _prePaymentEnabled ? newTop1_2 : newTop1_2 + _mtbPaydTimeTopOffset;

            lbPaydCaption.Top = newTop2;
            lbPrice.Top = newTop2;
            lbPriceD.Top = newTop2;
            lbPriceM.Top = newTop2;

            pPrePaydView.Height -= _dY;
            pPrePaydChange.Height -= _dY;
            gbPaydPreayd.Height -= _dY;
            btnSave.Top -= _dY;
        }

        private void BringBack()
        {
            lbPaydDateCaption.Top = _oldTop1;
            lbPaymentDate.Top = _oldTop1;
            lbPaymentDateD.Top = _oldTop1;
            lbPaymentDateM.Top = _oldTop1;
            
            dtpPaymentDate.Top = _oldTop1_2;
            mtbPaydTime.Top = _prePaymentEnabled ? _oldTop1_2 : _oldTop1_2 + _mtbPaydTimeTopOffset;

            lbPaydCaption.Top = _oldTop2;
            lbPrice.Top = _oldTop2;
            lbPriceD.Top = _oldTop2;
            lbPriceM.Top = _oldTop2;

            pPrePaydView.Height += _dY;
            pPrePaydChange.Height += _dY;
            gbPaydPreayd.Height += _dY;
            btnSave.Top += _dY;
        }

        private void flowLayoutPanel1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            
        }

        private void UpdateValues()
        {

            bool lastChecked = dtpPaymentDate.Checked;
            dtpPaymentDate.MinDate = dtpPrePaymentDate.Checked ? dtpPrePaymentDate.Value : _minDate;
            dtpPaymentDate.Checked = lastChecked;

            lastChecked = dtpPrePaymentDate.Checked;
            dtpPrePaymentDate.MaxDate = dtpPaymentDate.Checked ? dtpPaymentDate.Value : DateTimePicker.MaxDateTime;
            dtpPrePaymentDate.Checked = lastChecked;

            dtpPaymentDate.MinDate = dtpPrePaymentDate.Checked ? dtpPrePaymentDate.Value : _minDate;
        }

        private void dtpPrePaymentDate_ValueChanged(object sender, EventArgs e)
        {
            if (_dateInit)
                UpdateValues();
        }

        private void dtpPaymentDate_ValueChanged(object sender, EventArgs e)
        {
            if (_dateInit)
                UpdateValues();
        }

        private void btnAnnul_Click(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {

        }
    }
}
