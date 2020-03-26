using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class NumberGenerator
    {

        public static CLayer.InvoiceNumberData GetInvoiceNumber()
        {
            DataLayer.NumberGenerator ng = new DataLayer.NumberGenerator();
            return ng.GetNumber(Guid.NewGuid().ToString(), DataLayer.NumberGenerator.NumberType.Invoice);
        }

        public static string GetProformaNumber()
        {
            DataLayer.NumberGenerator ng = new DataLayer.NumberGenerator();
            CLayer.InvoiceNumberData dt = ng.GetNumber(Guid.NewGuid().ToString(), DataLayer.NumberGenerator.NumberType.Proforma);
            return dt.InvoiceNumber;
        }

        public static CLayer.InvoiceNumberData GetGSTInvoiceNumber(long offlineBookId)
        {
            int stateId = BLayer.OfflineBooking.GetBillingEntityState(offlineBookId);
            if (stateId == 0)
            { throw new Exception("SB Entity state id not found.. BLayer::NumberGenerator::GetGSTInvoiceNumber"); }
            DataLayer.State state = new DataLayer.State();
            CLayer.InvoiceNumberData data = BLayer.Invoice.GetOldInvoiceNumber(stateId);
            if(data == null || data.InvoiceNumber == "")
            {
                string statecode = state.GetGSTCode(stateId);
                long numb = state.GetInvoiceNumber(stateId);

                data = new CLayer.InvoiceNumberData();
                data.InvoiceNumber = statecode + numb.ToString("00000#");
                data.InvoiceDate = DateTime.Today;
                return data; //return new invoice number
            }

            return data; //return deleted invoice's data and invoice number
        }
        public static CLayer.InvoiceNumberData GetGDSGSTInvoiceNumber(long BookId,long PropertyID=0)
        {
            int BillingEntityStateID = BLayer.State.GetBillingEntityStateID(PropertyID);
            BillingEntityStateID = (BillingEntityStateID == 0) ? Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARDEFAULTBILLINGENTITY)) : BillingEntityStateID;

            string GDSStateID =Convert.ToString(BillingEntityStateID);
            int stateId = Convert.ToInt32(GDSStateID);
            if (stateId == 0)
            { throw new Exception("SB Entity state id not found.. BLayer::NumberGenerator::GetGSTInvoiceNumber"); }
            DataLayer.State state = new DataLayer.State();
            CLayer.InvoiceNumberData data = BLayer.Invoice.GetOldGDSInvoiceNumber(stateId);
            if (data == null || data.InvoiceNumber == "")
            {
                string statecode = state.GetGSTCode(stateId);
                long numb = state.GetInvoiceNumber(stateId);

                data = new CLayer.InvoiceNumberData();
                data.InvoiceNumber = statecode + numb.ToString("00000#");
                data.InvoiceDate = DateTime.Today;
                return data; //return new invoice number
            }

            return data; //return deleted invoice's data and invoice number
        }
    }
}
