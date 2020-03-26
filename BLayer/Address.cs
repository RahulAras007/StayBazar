using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Address
    {
        public static void Delete(long AddressId)
        {
            DataLayer.Address address = new DataLayer.Address();
            address.Delete(AddressId);
        }

        public static void DeleteOnUser(long UserId)
        {
            DataLayer.Address address = new DataLayer.Address();
            address.DeleteOnUser(UserId);
        }

        public static List<CLayer.Address> GetOnUser(long UserId)
        {
            DataLayer.Address address = new DataLayer.Address();
            return address.GetOnUser(UserId);
        }

        public static CLayer.Address GetOnUserId(long UserId,int Addresstype)
        {
            DataLayer.Address address = new DataLayer.Address();
            return address.GetOnUserId(UserId, Addresstype);
        }

        public static CLayer.Address GetPrimaryOnUser(long UserId)
        {
            DataLayer.Address address = new DataLayer.Address();
            return address.GetPrimaryOnUser(UserId);
        }
        public static CLayer.Address GetBillingAddress(long UserId)
        {
            DataLayer.Address address = new DataLayer.Address();
            return address.GetBillingAddress(UserId);
        }
        public static  CLayer.Address GetAddressOnUserId(long UserId)
        {
            DataLayer.Address address = new DataLayer.Address();
            return address.GetAddressOnUserId(UserId);
        }


        public static CLayer.Address Get(long AddressId)
        {
            DataLayer.Address address = new DataLayer.Address();
            return address.Get(AddressId);
        }

        public static long Save(CLayer.Address data)
        {
            DataLayer.Address address = new DataLayer.Address();
            data.Validate();
            return address.Save(data);
        }
        public static long BillingAddress(CLayer.Address data)
        {
            DataLayer.Address address = new DataLayer.Address();
            data.Validate();
            return address.BillingAddress(data);
        }
        public static int GetStatidOnUserId(long UserId)
        {
            DataLayer.Address API = new DataLayer.Address();
            //     t.Validate();
            return API.GetStatidOnUserId(UserId);
        }
    }
}
