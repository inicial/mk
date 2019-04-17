using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DocumentServices;

namespace HelperClasses
{

    class mk_docoment_by_dogovorRusultExtendet :mk_documents_by_dogovorResult
     {

         public Image button1 { get; set; }
         public Image button2 { get; set; }  
        public Image button3 { get; set; }
        public string subStatus { get; set; }
        public mk_docoment_by_dogovorRusultExtendet(mk_documents_by_dogovorResult result)
        {
            
            this.Document = result.Document;
            this.CodeStatusDocument = result.CodeStatusDocument;
            this.NameDocument = result.NameDocument;
            this.Servises = result.Servises;
            this.ServisesKeys = result.ServisesKeys;
            this.StatusDocument = result.StatusDocument;
            this.Turists = result.Turists;
            this.TuristsKeys = result.TuristsKeys;
            this.TypeOfDocument = result.TypeOfDocument;
            this.id = result.id;
            this.DateCreated = result.DateCreated;
            this.WhoCreated = result.WhoCreated;
            if (this.CodeStatusDocument == 1)
            {
                button3 = DocumentServices.Properties.Resources.Delete;
                button2 = DocumentServices.Properties.Resources.edit;
                button1 = DocumentServices.Properties.Resources.view;
            }
            else
            {
                button1 = button2 = button3= DocumentServices.Properties.Resources.empty;
            }
        }
        public void ChekStatus(DateTime dateAccept)
        {
            if (this.CodeStatusDocument == 1)
            {
                if (this.DateCreated < dateAccept)
                {
                    this.subStatus = "Обработано и выложено в ЛК";
                }
                else
                {
                    this.subStatus = "Обработано";
                }
            }
            else
            {
                this.subStatus = "Не обработано";
            }
        }
    }
    class ToursExtended : Tours
    {
        public string documents { get; set; }

        public ToursExtended(string Name,string Document)
        {
            this.DL_NAME = Name;
            this.documents = Document;
        }
        public ToursExtended(Tours tour)
        {
            this.documents = "";
            this.DL_BRUTTO = tour.DL_BRUTTO;
            this.dl_KEY = tour.dl_KEY;
            this.dl_svkey = tour.dl_svkey;
            this.id = tour.id;
            this.name_servise = tour.name_servise;
            this.TL_TIP = tour.TL_TIP;
            this.DL_NAME = tour.DL_NAME;
            this.order = tour.order;
            this.tipe = tour.tipe;
            this.DL_CODE = tour.DL_CODE;
            this.DL_CONTROL = tour.DL_CONTROL;
            this.DL_DISCOUNT = tour.DL_DISCOUNT;
            this.DL_NDAYS = tour.DL_NDAYS;
            this.DL_NMEN = tour.DL_NMEN;
            this.DL_NNIGT = tour.DL_NNIGT;
            this.DL_PAKETKEY = tour.DL_PAKETKEY;
            this.DL_PARTNERKEY = tour.DL_PARTNERKEY;
            this.DL_SUBCODE1 = tour.DL_SUBCODE1;
            this.DL_SUBCODE2 = tour.DL_SUBCODE2;
            this.DL_TURDATE = tour.DL_TURDATE;
           // this.

        }
    }
}
