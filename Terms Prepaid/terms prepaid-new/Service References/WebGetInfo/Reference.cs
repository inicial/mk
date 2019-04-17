﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace terms_prepaid.WebGetInfo {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WebGetInfo.Service1Soap")]
    public interface Service1Soap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ArtWebService", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ArtWebService();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebCreateDogovor", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        terms_prepaid.WebGetInfo.MessageInfos WebCreateDogovor(string sDateBeg, int nTrKey, string sCnCode, int nCtKey, int nCtDepartureKey, string sRate, int nMen, int nPartnerKey, int nDays, float nPrice, string sDgCodePartner, int nBTKey, int nCnKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebCreateTurist", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        terms_prepaid.WebGetInfo.MessageInfos WebCreateTurist(int nSex, string sFName, string sName, string sDR, string sNom, string sPasport, string sDatePasportEnd, string sDgCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebCreateDogList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        terms_prepaid.WebGetInfo.MessageInfos WebCreateDogList(string sDgCode, int nDay, int nSvKey, int nCode, int nSubCode1, int nSubCode2, int nPkKey, int nPrKey, int nMen, int nDays, int nCtKey, float nNetto, float nBrutto, string sNameService, string sTuristKeys);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebRecalculateDogovor", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        terms_prepaid.WebGetInfo.MessageInfos WebRecalculateDogovor(string sDgCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebCreateMFPayment", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        terms_prepaid.WebGetInfo.MessageInfos WebCreateMFPayment(string sDgCode, float fSumma);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebInsertDictionary", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        terms_prepaid.WebGetInfo.MessageInfos WebInsertDictionary(int nSvKey, string sNameRus, string sNameLat, int nCnKey, int nCtKey, int nDaysCountMin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebCreateCost", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        terms_prepaid.WebGetInfo.MessageInfos WebCreateCost(int nSvKey, int nCode, int nSubCode1, int nSubCode2, int nPkKey, int nPrKey, int nNetto, int nBrutto, string sDateFrom, string sDateTo, string sPPDateFrom, string sPPDateTo, int nComiss, int nGroup, string sRate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        terms_prepaid.WebGetInfo.TourInfo GetInfo(string sDgCode);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1067.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class MessageInfos : object, System.ComponentModel.INotifyPropertyChanged {
        
        private MessageInfo[] allMessageInfoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        public MessageInfo[] AllMessageInfo {
            get {
                return this.allMessageInfoField;
            }
            set {
                this.allMessageInfoField = value;
                this.RaisePropertyChanged("AllMessageInfo");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1067.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class MessageInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int keyField;
        
        private int infoNumberField;
        
        private string infoField;
        
        private string dopInfoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
                this.RaisePropertyChanged("Key");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int InfoNumber {
            get {
                return this.infoNumberField;
            }
            set {
                this.infoNumberField = value;
                this.RaisePropertyChanged("InfoNumber");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Info {
            get {
                return this.infoField;
            }
            set {
                this.infoField = value;
                this.RaisePropertyChanged("Info");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DopInfo {
            get {
                return this.dopInfoField;
            }
            set {
                this.dopInfoField = value;
                this.RaisePropertyChanged("DopInfo");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1067.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class AllTourInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string statusField;
        
        private string priceField;
        
        private string payedField;
        
        private string rateDogovorField;
        
        private string rate1Field;
        
        private string rate2Field;
        
        private string realCourseField;
        
        private string inCourseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("Status");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Price {
            get {
                return this.priceField;
            }
            set {
                this.priceField = value;
                this.RaisePropertyChanged("Price");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Payed {
            get {
                return this.payedField;
            }
            set {
                this.payedField = value;
                this.RaisePropertyChanged("Payed");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RateDogovor {
            get {
                return this.rateDogovorField;
            }
            set {
                this.rateDogovorField = value;
                this.RaisePropertyChanged("RateDogovor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Rate1 {
            get {
                return this.rate1Field;
            }
            set {
                this.rate1Field = value;
                this.RaisePropertyChanged("Rate1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Rate2 {
            get {
                return this.rate2Field;
            }
            set {
                this.rate2Field = value;
                this.RaisePropertyChanged("Rate2");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RealCourse {
            get {
                return this.realCourseField;
            }
            set {
                this.realCourseField = value;
                this.RaisePropertyChanged("RealCourse");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InCourse {
            get {
                return this.inCourseField;
            }
            set {
                this.inCourseField = value;
                this.RaisePropertyChanged("InCourse");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1067.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class TourInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private AllTourInfo[] allInfoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        public AllTourInfo[] AllInfo {
            get {
                return this.allInfoField;
            }
            set {
                this.allInfoField = value;
                this.RaisePropertyChanged("AllInfo");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Service1SoapChannel : terms_prepaid.WebGetInfo.Service1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1SoapClient : System.ServiceModel.ClientBase<terms_prepaid.WebGetInfo.Service1Soap>, terms_prepaid.WebGetInfo.Service1Soap {
        
        public Service1SoapClient() {
        }
        
        public Service1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ArtWebService() {
            return base.Channel.ArtWebService();
        }
        
        public terms_prepaid.WebGetInfo.MessageInfos WebCreateDogovor(string sDateBeg, int nTrKey, string sCnCode, int nCtKey, int nCtDepartureKey, string sRate, int nMen, int nPartnerKey, int nDays, float nPrice, string sDgCodePartner, int nBTKey, int nCnKey) {
            return base.Channel.WebCreateDogovor(sDateBeg, nTrKey, sCnCode, nCtKey, nCtDepartureKey, sRate, nMen, nPartnerKey, nDays, nPrice, sDgCodePartner, nBTKey, nCnKey);
        }
        
        public terms_prepaid.WebGetInfo.MessageInfos WebCreateTurist(int nSex, string sFName, string sName, string sDR, string sNom, string sPasport, string sDatePasportEnd, string sDgCode) {
            return base.Channel.WebCreateTurist(nSex, sFName, sName, sDR, sNom, sPasport, sDatePasportEnd, sDgCode);
        }
        
        public terms_prepaid.WebGetInfo.MessageInfos WebCreateDogList(string sDgCode, int nDay, int nSvKey, int nCode, int nSubCode1, int nSubCode2, int nPkKey, int nPrKey, int nMen, int nDays, int nCtKey, float nNetto, float nBrutto, string sNameService, string sTuristKeys) {
            return base.Channel.WebCreateDogList(sDgCode, nDay, nSvKey, nCode, nSubCode1, nSubCode2, nPkKey, nPrKey, nMen, nDays, nCtKey, nNetto, nBrutto, sNameService, sTuristKeys);
        }
        
        public terms_prepaid.WebGetInfo.MessageInfos WebRecalculateDogovor(string sDgCode) {
            return base.Channel.WebRecalculateDogovor(sDgCode);
        }
        
        public terms_prepaid.WebGetInfo.MessageInfos WebCreateMFPayment(string sDgCode, float fSumma) {
            return base.Channel.WebCreateMFPayment(sDgCode, fSumma);
        }
        
        public terms_prepaid.WebGetInfo.MessageInfos WebInsertDictionary(int nSvKey, string sNameRus, string sNameLat, int nCnKey, int nCtKey, int nDaysCountMin) {
            return base.Channel.WebInsertDictionary(nSvKey, sNameRus, sNameLat, nCnKey, nCtKey, nDaysCountMin);
        }
        
        public terms_prepaid.WebGetInfo.MessageInfos WebCreateCost(int nSvKey, int nCode, int nSubCode1, int nSubCode2, int nPkKey, int nPrKey, int nNetto, int nBrutto, string sDateFrom, string sDateTo, string sPPDateFrom, string sPPDateTo, int nComiss, int nGroup, string sRate) {
            return base.Channel.WebCreateCost(nSvKey, nCode, nSubCode1, nSubCode2, nPkKey, nPrKey, nNetto, nBrutto, sDateFrom, sDateTo, sPPDateFrom, sPPDateTo, nComiss, nGroup, sRate);
        }
        
        public terms_prepaid.WebGetInfo.TourInfo GetInfo(string sDgCode) {
            return base.Channel.GetInfo(sDgCode);
        }
    }
}
