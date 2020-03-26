using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class B2B
    {
        //for booking
       // private const int MAX_LENGTH_BOOKING_REFNO = 20;
        private const int MAX_LENGTH_REFNO = 5;
        private const int ID_SEED = 1000;
        //postfix
        private const string REFNO_POSTFIX_CORP = "CA";
        private const string REFNO_POSTFIX_TRA = "TA";
        private const string REFNO_POSTFIX_REG = "SA";

        public static string GetCode(long id, CLayer.Role.Roles userRole)
        {
            string ids = (id + ID_SEED).ToString("X");
            ids = ids.PadLeft(MAX_LENGTH_REFNO, '0');
            switch (userRole)
            {
                case CLayer.Role.Roles.Corporate:
                case CLayer.Role.Roles.CorporateUser:
                    ids = REFNO_POSTFIX_CORP + ids;
                    break;
                case CLayer.Role.Roles.Agent:
                    ids = REFNO_POSTFIX_TRA + ids;
                    break;
                default:
                    ids = REFNO_POSTFIX_REG + ids;
                    break;
            }
            return ids;
        }

        public static void FixB2BCodes()
        {
            DataLayer.B2B b2 = new DataLayer.B2B();
            List<CLayer.B2B> b2blst = b2.GetEmptyCodeRows();
            while (b2blst.Count > 0)
            {
                foreach (CLayer.B2B b in b2blst)
                {
                    b2.SetCode(b.B2BId, GetCode(b.B2BId, (CLayer.Role.Roles)b.UserType));
                }
                b2blst = b2.GetEmptyCodeRows();
            }
        }
        public static void SetCode(long b2bid, string code)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.SetCode(b2bid,code);
        }
        public static string GetBusinessName(long userId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.GetBusinessName(userId);
        }
        public static void SetApprovalDate(long b2bId,DateTime approvalDate)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.SetApprovalDate(b2bId,approvalDate);
        }
        public static long GetCorporateIdOfUser(long corpUserId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.GetCorporateIdOfUser(corpUserId);
        }

        public static void Delete(long b2bId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.Delete(b2bId);
        }
        public static void DeleteSupplierAfffiliate(long userId, long B2BId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.DeleteSupplierAfffiliate(userId,B2BId,(int)CLayer.Role.Roles.Supplier);
        }
        public static void SaveAffiliate(CLayer.B2B data)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.SaveAffiliate(data);
        }
        public static void SaveSupplierForAffiliate(CLayer.B2B data)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.SaveSupplierForAffiliate(data);
        }
        public static int SaveCBookCredit(CLayer.B2B data)
        {
            DataLayer.B2B task = new DataLayer.B2B();
            return task.SaveCBookCredit(data);
        }

        public static int AllowCBookSamedaybook(int IsCorpBookingtoday,long UserId)
        {
            DataLayer.B2B task = new DataLayer.B2B();
            return task.AllowCBookSamedaybook(IsCorpBookingtoday, UserId);
        }
        public static List<CLayer.B2B> SearchCorporate(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.SearchCorporate(name);
        }

        public static List<CLayer.Property> GetProperties(long supplierId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.GetProperties(supplierId);
        }
        public static List<CLayer.B2B> SearchSupplier(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.SearchSupplier(name);
        }

        public static List<CLayer.B2B> Searchsupplierforofflinebook(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.Searchsupplierforofflinebook(name);
        }
        public static List<CLayer.OfflineBooking> Searchsupplierforofflinebookfromcustom(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.Searchsupplierforofflinebookfromcustom(name);
        }
        public static List<CLayer.Property> Searchpropertylist(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.Searchpropertylist(name);
        }

        public static List<CLayer.OfflineBooking> Searchcustompropertylist(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.Searchcustompropertylist(name);
        }
        public static List<CLayer.OfflineBooking> SearchcustomerListlist(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.SearchcustomerListlist(name);
        }

        public static List<CLayer.OfflineBooking> SearchPaymentCustomerList(string name, int start, int limit)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.SearchPaymentCustomerList(name,start,limit);
        }


        public static List<CLayer.Property> Searchpropertylistaftersup(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.Searchpropertylistaftersup(name);
        }
        public static List<CLayer.B2B> SearchAffiliate(string name)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.SearchAffiliate(name);
        }
        public static List<CLayer.B2B> Getall(int usertype, int status = 0)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.Getall(usertype, status);
        }
        public static List<CLayer.B2B> GetAllSupplier(long userId)
        {
            DataLayer.B2B Supplier = new DataLayer.B2B();
            return Supplier.GetAllSupplier(userId,(int)CLayer.Role.B2BRoles.Supplier);
        }
        public static List<CLayer.B2B> GetSearchAllSupplier(string name,int start,int limit,out int totalRows)
        {
            DataLayer.B2B Supplier = new DataLayer.B2B();
            return Supplier.GetSearchAllSupplier(name,start,limit,(int)CLayer.Role.Roles.Supplier,out totalRows);
        }
        public static CLayer.B2B Get(long b2bId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.Get(b2bId);
        }

        public static CLayer.B2B GetbookingcreditforCorte(long b2bId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.GetbookingcreditforCorte(b2bId);
        }

        public static DateTime GetbookingsmedayforCorp(long b2bId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.GetbookingsmedayforCorp(b2bId);
        }
        public static long Update(CLayer.B2B data)
        {
            DataLayer.B2B user = new DataLayer.B2B();
            data.Validate();
            return user.Update(data);
        }
        public static long Save(CLayer.B2B data)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            CLayer.Role.Roles rle = BLayer.User.GetRole(data.UserId);
            data.UserCode = GetCode(data.UserId, rle);
            data.Validate();
            return b2b.Save(data);
        }
         public static long Savedoc(CLayer.B2B data)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            CLayer.Role.Roles rle = BLayer.User.GetRole(data.UserId);
            data.UserCode = GetCode(data.UserId, rle);
            data.Validate();
            return b2b.Savedoc(data);
        }

        public static long SaveBusinessname(CLayer.B2B data)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            data.Validate();
            return b2b.SaveBusinessname(data);
        }
        public static void SetStatus(long B2BId, int Status)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.SetStatus(B2BId, Status);
        }

        public static void SetMaxStaff(long B2BId, int MaxStaff)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.SetMaxStaff(B2BId, MaxStaff);
        }
        public static void SetCreditPeriod(long B2BId, int CreditPeriod)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            b2b.SetCreditPeriod(B2BId, CreditPeriod);
        }
        public static List<CLayer.User> GetAllCorporateUsers(long corporateId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.GetAllCorporateUsers(corporateId);
        }
        public static long GetSupplierID(string SupplierName)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.GetSupplierID(SupplierName);
        }
        public static bool  IsCreditBookingNeeded(long userId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.IsCreditBookingNeeded(userId);
        }
        public static long  LastApproverID(long userId,long bookingid)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.LastApproverID(userId,bookingid );
        }
        public static CLayer.B2B GetbookingcreditforCorporateUser(long b2bId)
        {
            DataLayer.B2B b2b = new DataLayer.B2B();
            return b2b.GetbookingcreditforCorporateUser(b2bId);
        }
    }
}
