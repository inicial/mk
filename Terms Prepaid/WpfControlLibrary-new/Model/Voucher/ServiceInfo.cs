using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Model.Voucher
{
    public class ServiceInfo
    {
        public int dlkey;

        public int? BronId;
        public string dl_name;
        public ServiceType SType;

        public static ServiceInfo Parse(object obj)
        {
            return obj as ServiceInfo;
        }

        public ServiceInfo(int key, string name, ServiceType sType = ServiceType.Unknow, int? bronId = null)
        {
            dlkey = key;
            dl_name = name;
            SType = sType;
            BronId = bronId;
        }

        public override string ToString()
        {
            return dl_name;
        }

        public static ServiceInfo GetServiceInfo(Service service)
        {
            ServiceInfo serviceInfo = new ServiceInfo(service.DlKey, service.Name);

            if (service is AviaService)
            {
                var aviaService = (AviaService)service;
                serviceInfo.BronId = aviaService.BronId;
                serviceInfo.SType = ServiceType.Avia;
            }
            else if (service is VisaService)
                serviceInfo.SType = ServiceType.Visa;
            else if (service is InshurService)
                serviceInfo.SType = ServiceType.Inshur;
            else if (service is CruiseService)
                serviceInfo.SType = ServiceType.Cruise;
            else if (service is HotelService)
                serviceInfo.SType = ServiceType.Hotel;
            else if (service is TransferService)
                serviceInfo.SType = ServiceType.Transfer;

            serviceInfo.dl_name = service is AviaService ? ((AviaService)service).BookingAvia.Route : service.ServiceName;

            return serviceInfo;
        }
    }

    public class ServiceInfoComparer : IEqualityComparer<ServiceInfo>
    {

        public bool Equals(ServiceInfo x, ServiceInfo y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.dlkey.Equals(y.dlkey) &&
                x.dl_name != null && y.dl_name != null && x.dl_name.Equals(y.dl_name);
        }

        public int GetHashCode(ServiceInfo obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashProductName = obj.dl_name == null ? 0 : obj.dl_name.GetHashCode();

            //Get hash code for the Code field. 
            int hashProductCode = obj.dlkey.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }

    public class ServiceInfoComparerByDlKey : IEqualityComparer<ServiceInfo>
    {

        public bool Equals(ServiceInfo x, ServiceInfo y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.dlkey.Equals(y.dlkey);
        }

        public int GetHashCode(ServiceInfo obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashProductName = obj.dl_name == null ? 0 : obj.dl_name.GetHashCode();

            //Get hash code for the Code field. 
            return obj.dlkey.GetHashCode();
        }
    }
}
