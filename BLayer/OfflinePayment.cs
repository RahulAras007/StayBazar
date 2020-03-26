using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class OfflinePayment
    {
        //for booking
        private const int MAX_LENGTH_BOOKING_REFNO = 20;
        private const int MAX_LENGTH_REFNO = 5;
        private const int BOOKING_ID_SEED = 1000;

        //postfix
        private const string PAY_REFNO_POSTFIX_CORP = "CP";
        private const string PAY_REFNO_POSTFIX_TRA = "TP";
        private const string PAY_REFNO_POSTFIX_REG = "RP";

        public static string GetRefNo(long id, CLayer.Role.Roles userRole)
        {
            string ids = (id + BOOKING_ID_SEED).ToString("X");
            ids = ids.PadLeft(MAX_LENGTH_REFNO, '0');
            switch (userRole)
            {
                case CLayer.Role.Roles.Corporate:
                case CLayer.Role.Roles.CorporateUser:
                    ids = PAY_REFNO_POSTFIX_CORP + ids;
                    break;
                case CLayer.Role.Roles.Agent:
                    ids = PAY_REFNO_POSTFIX_TRA + ids;
                    break;
                default:
                    ids = PAY_REFNO_POSTFIX_REG + ids;
                    break;
            }
            return ids;
        }
        public static bool IsExist(string paymentId)
        {
            DataLayer.OfflinePayment t = new DataLayer.OfflinePayment();
            return t.IsExist(paymentId);
        }
        public static void SetStatus(int status, long OfflinePayId)
        {
            DataLayer.OfflinePayment OffPay = new DataLayer.OfflinePayment();
            OffPay.SetStatus(status, OfflinePayId);
        }
        public static long SaveInitialPaymentData(CLayer.OfflinePayment data)
        {

            DataLayer.OfflinePayment bk = new DataLayer.OfflinePayment();
            //if (data.PaymentRefNo == "") data.PaymentRefNo = GetRefNo(data.ReferenceNumber,UserRole);
            return bk.SaveInitialPaymentData(data);
        }

        public static void SaveOfflineTras(CLayer.OfflineTransaction data)
        {
            DataLayer.OfflinePayment t = new DataLayer.OfflinePayment();
            t.SaveOfflineTras(data);
        }

        public static List<CLayer.OfflinePayment> ReportOfflinePayment_GetAll(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.OfflinePayment user = new DataLayer.OfflinePayment();
            return user.ReportOfflinePayment_GetAll(status, searchString, searchItem, start, limit);
        }



        public static long SetPaymentRefNo(long OfflinePaymentId, CLayer.Role.Roles UserRole, string PaymentRefNo)
        {

            DataLayer.OfflinePayment bk = new DataLayer.OfflinePayment();
            if (PaymentRefNo == " ") { PaymentRefNo = GetRefNo(OfflinePaymentId, UserRole); }
            return bk.SetPaymentRefNo(OfflinePaymentId, PaymentRefNo);
        }
        public static long SetCustomerPaymentRefNo(long OfflinePaymentId, CLayer.Role.Roles UserRole, string PaymentRefNo, Guid PaymentGuid)
        {

            DataLayer.OfflinePayment bk = new DataLayer.OfflinePayment();
            if (PaymentRefNo == " ") { PaymentRefNo = GetRefNo(OfflinePaymentId, UserRole); }
            return bk.SetCustomerPaymentRefNo(OfflinePaymentId, PaymentRefNo, PaymentGuid);
        }
        public static CLayer.OfflinePayment GetOfflinePaymentDetails(long OfflinePaymentId)
        {
            DataLayer.OfflinePayment book = new DataLayer.OfflinePayment();
            return book.GetOfflinePaymentDetails(OfflinePaymentId);
        }
        public static List<CLayer.OfflinePayment> GetOfflinePaymentDetails2(long OfflinePaymentId)
        {
            DataLayer.OfflinePayment book = new DataLayer.OfflinePayment();
            return book.GetOfflinePaymentDetails2(OfflinePaymentId);
        }
        public static string GetOfflinePaymentGuid(long OfflinePaymentId)
        {
            DataLayer.OfflinePayment book = new DataLayer.OfflinePayment();
            return book.GetOfflinePaymentGuid(OfflinePaymentId);
        }
        public static List<CLayer.OfflinePayment> GetOfflinePaymentDetails1(long OfflinePaymentId)
        {
            DataLayer.OfflinePayment user = new DataLayer.OfflinePayment();
            return user.GetOfflinePaymentDetails1(OfflinePaymentId);
        }

        public static long GetOfflinePayByRefNo(string OfflinePayRefNo)
        {
            DataLayer.OfflinePayment book = new DataLayer.OfflinePayment();
            return book.GetOfflinePayByRefNo(OfflinePayRefNo);
        }
        public static long SaveInitialCustomerPaymentData(CLayer.OfflinePayment data)
        {

            DataLayer.OfflinePayment bk = new DataLayer.OfflinePayment();
            //if (data.PaymentRefNo == "") data.PaymentRefNo = GetRefNo(data.ReferenceNumber,UserRole);
            return bk.SaveInitialCustomerPaymentData(data);
        }
        public static long GetOfflinePaymentUserID(Guid PaymentGuid)
        {

            DataLayer.OfflinePayment bk = new DataLayer.OfflinePayment();
            //if (data.PaymentRefNo == "") data.PaymentRefNo = GetRefNo(data.ReferenceNumber,UserRole);
            return bk.GetOfflinePaymentUserID(PaymentGuid);
        }
    }
}
