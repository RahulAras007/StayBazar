using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;

namespace BLayer
{
    public class Transaction
    {
        public static void RefundAmount(long bookingId, decimal refundAmountAfterApplyingCancCharge, decimal cancellationCharge)
        {
            decimal RefundAmount = refundAmountAfterApplyingCancCharge;
            CLayer.Booking detail = BLayer.Bookings.GetDetails(bookingId);
            List<CLayer.Transaction> refundable = BLayer.Transaction.GetAllRefundable(bookingId);
            double serviceCharge = 0;
            double totalServiceCharge = 0;
            CLayer.Property prp = BLayer.Property.GetCancellationCharges(BLayer.Bookings.GetPropertyId(bookingId));

            //found refund amount by reducing cancellation charge, service charge will be deducted once the transaction is done

            bool CanCancel = (RefundAmount > CLayer.Settings.TRANSACTION_MIN_AMOUNT);
            decimal totalRefund = RefundAmount;

            if (CanCancel)
            {
                //mark as trying
                double amt = 0;
                decimal tryRefund = 0;
                foreach (CLayer.Transaction tran in refundable)
                {
                    //(pPaymentId VARCHAR(12),pTryAmount DECIMAL(14,2), pTryTime DATETIME,pTotalAmount DECIMAL(14,2),pStatus INT)
                    serviceCharge = GetServiceCharge((CLayer.ObjectStatus.PaymentMethod)tran.PaymentType);
                    if (serviceCharge > 0)
                    {
                        serviceCharge = Math.Round((double)RefundAmount * serviceCharge / 100 * 2);
                        RefundAmount = RefundAmount - (decimal)serviceCharge;
                    }
                    if (RefundAmount <= 0) break;
                    amt = tran.TotalAmount; // -serviceCharge;
                    if (amt >= (double)RefundAmount)
                    {
                        amt = amt - (double)RefundAmount;
                        tryRefund = RefundAmount;
                        RefundAmount = 0;
                    }
                    else
                    {
                        RefundAmount = RefundAmount - (decimal)amt;
                        tryRefund = (decimal)amt;
                        amt = 0;
                    }
                    totalServiceCharge = totalServiceCharge + serviceCharge;
                    tran.TryAmount += tryRefund;
                    tran.ServiceCharge = tran.ServiceCharge + serviceCharge;
                    tran.TryTime = DateTime.Today;
                    tran.TotalAmount = amt;
                    tran.Status = CLayer.ObjectStatus.TransactionStatus.TryingForRefund;
                    BLayer.Transaction.UpdateAmountAndStatus(tran);

                }

                foreach (CLayer.Transaction tran in refundable)
                {
                    try
                    {
                        if (tran.Status != CLayer.ObjectStatus.TransactionStatus.TryingForRefund) continue;
                        if (SendRefund(tran))
                        {
                            if (tran.TotalAmount > 0)
                                tran.Status = CLayer.ObjectStatus.TransactionStatus.PartialRefund;
                            else
                                tran.Status = CLayer.ObjectStatus.TransactionStatus.Refunded;
                            tran.TryAmount = 0;
                            BLayer.Transaction.UpdateAmountAndStatus(tran);
                        }

                    }
                    catch
                    { }
                }

                //set totalservice charge and cancellation charge 
                BLayer.Bookings.UpdateCharges(bookingId, cancellationCharge, (decimal)totalServiceCharge);
                BLayer.Bookings.SetRefund(bookingId, totalRefund - (decimal)totalServiceCharge);

                //save to booking refund               

                //BLayer.Bookings.SaveBookingRefundAmt(bookingId, totalRefund - (decimal)totalServiceCharge, (int)CLayer.ObjectStatus.BookingChangeType.Modify);
            }
        }
        //public static void CancelAccommodation(long bookingItemId)
        //{
        //    CLayer.BookingItem detail = BLayer.BookingItem.GetDetails(bookingItemId);
        //    List<CLayer.Transaction> refundable = BLayer.Transaction.GetAllRefundable(detail.BookingId);
        //    double serviceCharge = 0; // BLayer.Transaction.GetTotalCancellationServiceCharge(detail.BookingId, ref refundable, detail.Amount);
        //    decimal cancellationCharge = 0;
        //    decimal firstDayCharge = BLayer.BookingItem.GetFirstDayCharge(bookingItemId);
        //    CLayer.Property prp = BLayer.Property.GetCancellationCharges(BLayer.Bookings.GetPropertyId(detail.BookingId));
        //    if (detail.CheckIn.AddHours(-1 * (double)prp.CancellationPeriod) <= DateTime.Now)
        //    {
        //        cancellationCharge = BLayer.Bookings.GetTotalCancellationCharge(prp, detail.Amount, firstDayCharge, detail.CheckIn); ;
        //    }
        //    //found refund amount by reducing cancellation charge, service charge will be deducted once the transaction is done
        //    decimal RefundAmount = detail.TotalAmount - cancellationCharge;
        //    bool CanCancel = (RefundAmount > CLayer.Settings.TRANSACTION_MIN_AMOUNT);

        //    BLayer.BookingItem.SetStatus(bookingItemId, CLayer.ObjectStatus.StatusType.Disabled);

        //    if (CanCancel)
        //    {
        //        //mark as trying
        //        double amt = 0;
        //        decimal tryRefund = 0;
        //        foreach (CLayer.Transaction tran in refundable)
        //        {
        //            //(pPaymentId VARCHAR(12),pTryAmount DECIMAL(14,2), pTryTime DATETIME,pTotalAmount DECIMAL(14,2),pStatus INT)
        //            serviceCharge = GetServiceCharge((CLayer.ObjectStatus.PaymentMethod)tran.PaymentType);
        //            if (serviceCharge > 0)
        //            {
        //                serviceCharge = Math.Round((double)RefundAmount * serviceCharge / 100 * 2);
        //                RefundAmount = RefundAmount - (decimal)serviceCharge;
        //            }
        //            if (RefundAmount <= 0) break;
        //            amt = tran.TotalAmount;
        //            if (amt >= (double)RefundAmount)
        //            {
        //                amt = amt - (double)RefundAmount;
        //                tryRefund = RefundAmount;
        //                RefundAmount = 0;
        //            }
        //            else
        //            {
        //                RefundAmount = RefundAmount - (decimal)amt;
        //                tryRefund = (decimal)amt;
        //                amt = 0;
        //            }

        //            tran.TryAmount = tryRefund;
        //            tran.ServiceCharge = tran.ServiceCharge + serviceCharge;
        //            tran.TryTime = DateTime.Today;
        //            tran.TotalAmount = amt;
        //            tran.Status = CLayer.ObjectStatus.TransactionStatus.TryingForRefund;
        //            BLayer.Transaction.UpdateAmountAndStatus(tran);
        //            // if (RefundAmount <= 0) break;
        //        }

        //        foreach (CLayer.Transaction tran in refundable)
        //        {
        //            try
        //            {
        //                if (tran.Status != CLayer.ObjectStatus.TransactionStatus.TryingForRefund) continue;
        //                if (SendRefund(tran))
        //                {
        //                    if (tran.TotalAmount > 0)
        //                        tran.Status = CLayer.ObjectStatus.TransactionStatus.PartialRefund;
        //                    else
        //                        tran.Status = CLayer.ObjectStatus.TransactionStatus.Refunded;
        //                    tran.TryAmount = 0;
        //                    BLayer.Transaction.UpdateAmountAndStatus(tran);
        //                }

        //            }
        //            catch
        //            { }
        //        }
        //        //BLayer.Bookings.UpdateCharges(bookingId, cancellationCharge, (decimal)totalServiceCharge);
        //        //BLayer.Bookings.SetRefund(bookingId, totalRefund - (decimal)totalServiceCharge);
        //    }
        //}

        public static void CancelAllTransactions(long bookingId)
        {
            CLayer.ObjectStatus.BookingStatus BStatus = BLayer.Bookings.GetStatus(bookingId);
            long PayOption = BLayer.Bookings.GetPaymentoption(bookingId);
            CLayer.Booking detail = BLayer.Bookings.GetDetails(bookingId);
            List<CLayer.Transaction> refundable = BLayer.Transaction.GetAllRefundable(bookingId);
            double serviceCharge = 0;// BLayer.Transaction.GetTotalCancellationServiceCharge(bookingId, ref refundable);
            decimal cancellationCharge = 0;


            double totalServiceCharge = 0;

            decimal firstDayCharge = BLayer.Bookings.GetFirstDayCharge(bookingId);
            CLayer.Property prp = BLayer.Property.GetCancellationCharges(BLayer.Bookings.GetPropertyId(bookingId));
            int noOfDays = (detail.CheckOut - detail.CheckIn).Days;
            cancellationCharge = BLayer.Bookings.GetTotalCancellationCharge(prp, detail.TotalAmount, noOfDays * firstDayCharge, detail.CheckIn);

            if (BStatus == CLayer.ObjectStatus.BookingStatus.BookingRequest)
            {
                cancellationCharge = 0;
            }

            decimal RefundAmountFCancel = Math.Round(detail.TotalAmount - cancellationCharge);

            if (PayOption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment)
            {
                RefundAmountFCancel = Math.Round(detail.TotalAmount - cancellationCharge);
            }
            else if (PayOption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            {
                CLayer.ObjectStatus.PartialPaymentStatus PartialPayStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                CLayer.Booking PartBookDt = BLayer.Bookings.GetPartialBookDetailsbyBookId(bookingId);

                if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.Secondpaycheckout)
                {
                    RefundAmountFCancel = Math.Round(PartBookDt.PaymentFirstinstallment - cancellationCharge);
                }
                else if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentSuccess)
                {
                    RefundAmountFCancel = Math.Round(detail.TotalAmount - cancellationCharge);
                }
            }


            bool CanCancel = (RefundAmountFCancel > CLayer.Settings.TRANSACTION_MIN_AMOUNT);


            if (BStatus == CLayer.ObjectStatus.BookingStatus.BookingRequest)
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Decline, bookingId);

            }
            else
            {

                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, bookingId);
            }


            BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.BookingCancel, bookingId);
            BLayer.Bookings.SetUpdatedDate(bookingId);

            decimal totalRefund = RefundAmountFCancel;
            long Gatewaytype = BLayer.Bookings.Getgatewaytype(bookingId);



            if (CanCancel)
            {
                //mark as trying
                double amt = 0;
                decimal tryRefund = 0;
                int countlist = 0;
                foreach (CLayer.Transaction tran in refundable)
                {
                    decimal RefundAmount = RefundAmountFCancel;
                  
                    if (PayOption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment)
                    {
                        RefundAmount = RefundAmountFCancel;
                    }
                    else if (PayOption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                    {


                        CLayer.Booking PartBookDt = BLayer.Bookings.GetPartialBookDetailsbyBookId(bookingId);
                        if (countlist == 0)
                        {
                            RefundAmount = Math.Round(PartBookDt.PaymentFirstinstallment - cancellationCharge);
                        }
                        else if (countlist == 1)
                        {
                            RefundAmount = Math.Round(PartBookDt.PaymentSecondinstallment - cancellationCharge);
                        }
                    }
                    else
                    {
                        RefundAmount = RefundAmountFCancel;
                    }



                    serviceCharge = GetServiceCharge((CLayer.ObjectStatus.PaymentMethod)tran.PaymentType);
                    if (serviceCharge > 0)
                    {
                        serviceCharge = Math.Round((double)RefundAmount * serviceCharge / 100 * 2);
                        if (BStatus == CLayer.ObjectStatus.BookingStatus.Decline)
                        {
                            serviceCharge = 0;
                        }
                        RefundAmount = RefundAmount - (decimal)serviceCharge;
                    }
                    if (RefundAmount <= 0) break;
                    amt = tran.TotalAmount;



                    if (Gatewaytype == (int)CLayer.ObjectStatus.Gateway.EBS)
                    {
                        if (amt >= (double)RefundAmount)
                        {
                            amt = amt - (double)RefundAmount;
                            tryRefund = RefundAmount;
                            RefundAmount = 0;
                        }
                        else
                        {
                            RefundAmount = RefundAmount - (decimal)amt;
                            tryRefund = (decimal)amt;
                            amt = 0;
                        }
                    }
                    else if (Gatewaytype == (int)CLayer.ObjectStatus.Gateway.PayPal)
                    {


                        CLayer.Currency cur = BLayer.Currency.Get("USD");
                        RefundAmount = Math.Round(RefundAmount * cur.ConversionRate, 2, MidpointRounding.AwayFromZero);

                        if (amt >= (double)RefundAmount)
                        {
                            amt = amt - (double)RefundAmount;
                            tryRefund = RefundAmount;
                            RefundAmount = 0;
                        }
                        else
                        {
                            RefundAmount = RefundAmount - (decimal)amt;
                            tryRefund = (decimal)amt;
                            amt = 0;
                        }
                    }




                    totalServiceCharge = totalServiceCharge + serviceCharge;
                    tran.TryAmount += tryRefund;
                    tran.ServiceCharge = tran.ServiceCharge + serviceCharge;

                    //tran.TryAmount = tryRefund;
                    //tran.ServiceCharge = tran.ServiceCharge + serviceCharge;
                    tran.TryTime = DateTime.Today;
                    tran.TotalAmount = amt;
                    tran.Status = CLayer.ObjectStatus.TransactionStatus.TryingForCanc;
                    BLayer.Transaction.UpdateAmountAndStatus(tran);
                    // if (RefundAmount <= 0) break;

                    countlist = countlist + 1;
                }
                //set totalservice charge and cancellation charge 
                BLayer.Bookings.UpdateCharges(bookingId, cancellationCharge, (decimal)totalServiceCharge);
                BLayer.Bookings.SetRefund(bookingId, totalRefund - (decimal)totalServiceCharge);

                //save to booking refund
                //BLayer.Bookings.SaveBookingRefundAmt(bookingId, totalRefund - (decimal)totalServiceCharge, (int)CLayer.ObjectStatus.BookingChangeType.Cancel);

                foreach (CLayer.Transaction tran in refundable)
                {
                    try
                    {
                        //if (tran.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
                        //{
                        //    continue;
                        //}

                        if (tran.Status != CLayer.ObjectStatus.TransactionStatus.TryingForCanc) continue;

                        if (Gatewaytype == (int)CLayer.ObjectStatus.Gateway.EBS)
                        {
                            if (SendRefund(tran))
                            {
                                //      if (tran.TotalAmount > 0)
                                //          tran.Status = CLayer.ObjectStatus.TransactionStatus.PartialRefund;
                                //       else
                                tran.Status = CLayer.ObjectStatus.TransactionStatus.Refunded;
                                tran.TryAmount = 0;
                                BLayer.Transaction.UpdateAmountAndStatus(tran);
                            }
                        }
                        else if (Gatewaytype == (int)CLayer.ObjectStatus.Gateway.PayPal)
                        {
                            if (SendPayPalRefund(tran))
                            {
                                tran.Status = CLayer.ObjectStatus.TransactionStatus.Refunded;
                                tran.TryAmount = 0;
                                BLayer.Transaction.UpdateAmountAndStatus(tran);
                            }
                        }


                    }
                    catch
                    { }
                }
            }
            else
            {
                //very less amount to refund which can be less than Rs.10
                BLayer.Transaction.RefundAllTransactions(bookingId);
            }
        }

        public static bool SendRefund(CLayer.Transaction tranToRefund)
        {
            try
            {
                string url = BLayer.Settings.GetValue(CLayer.Settings.PAYMENT_REFUND_LINK);

                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("AccountID", BLayer.Settings.GetValue(CLayer.Settings.PAY_ACCOUNT_ID));
                reqparm.Add("SecretKey", BLayer.Settings.GetValue(CLayer.Settings.PAY_SECRET_KEY));
                reqparm.Add("Amount", tranToRefund.TryAmount.ToString());
                reqparm.Add("PaymentID", tranToRefund.PaymentId);
                reqparm.Add("Action", "refund");
                WebClient client = new WebClient();
                byte[] responsebytes = client.UploadValues(url, "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);

                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(responsebody);
                if (xdoc.DocumentElement.Attributes["errorCode"] != null && xdoc.DocumentElement.Attributes["errorCode"].Value != "")
                {
                    string n = responsebody + tranToRefund.Notes;
                    BLayer.Transaction.SetNote(tranToRefund.TransactionId, tranToRefund.PaymentId, n);
                    return false;
                }
                else
                {
                    if (xdoc.DocumentElement.Attributes["response"] != null && xdoc.DocumentElement.Attributes["response"].Value.ToUpper() == "SUCCESS")
                    {
                        CLayer.Transaction retran = new CLayer.Transaction();
                        retran.TransactionType = CLayer.ObjectStatus.TransactionType.Refund;
                        retran.TransactionId = xdoc.DocumentElement.Attributes["transactionId"].Value;
                        retran.PaymentId = xdoc.DocumentElement.Attributes["paymentId"].Value;
                        double amount = 0;
                        double.TryParse(xdoc.DocumentElement.Attributes["amount"].Value, out amount);
                        retran.Amount = amount;
                        DateTime dat = DateTime.Today;
                        DateTime.TryParse(xdoc.DocumentElement.Attributes["dateTime"].Value, out dat);
                        retran.DateCreated = dat;
                        retran.Status = CLayer.ObjectStatus.TransactionStatus.Refunded;
                        retran.Notes = "Reference Number= " + xdoc.DocumentElement.Attributes["referenceNo"].Value + ", mode=" + xdoc.DocumentElement.Attributes["mode"].Value + ", transactionType=" + xdoc.DocumentElement.Attributes["transactionType"].Value
                            + ", status=" + xdoc.DocumentElement.Attributes["status"].Value;
                        retran.BookingId = tranToRefund.BookingId;
                        try
                        {
                            BLayer.Transaction.SaveCancelTrans(retran);
                        }
                        catch
                        {
                            string n = "Could not create transaction record. Received Response from Gateway: " + responsebody + " " + tranToRefund.Notes;
                            BLayer.Transaction.SetNote(tranToRefund.TransactionId, tranToRefund.PaymentId, n);
                        }

                    }
                    else
                    {
                        if (xdoc.DocumentElement.Attributes["transactionType"] != null && xdoc.DocumentElement.Attributes["transactionType"].Value.ToLower() == "refunded")
                        {
                            CLayer.Transaction retran = new CLayer.Transaction();
                            retran.TransactionType = CLayer.ObjectStatus.TransactionType.Refund;
                            retran.TransactionId = xdoc.DocumentElement.Attributes["transactionId"].Value;
                            retran.PaymentId = xdoc.DocumentElement.Attributes["paymentId"].Value;
                            double amount = 0;
                            double.TryParse(xdoc.DocumentElement.Attributes["amount"].Value, out amount);
                            retran.Amount = amount;
                            DateTime dat = DateTime.Today;
                            DateTime.TryParse(xdoc.DocumentElement.Attributes["dateTime"].Value, out dat);
                            retran.DateCreated = dat;
                            retran.Status = CLayer.ObjectStatus.TransactionStatus.Refunded;
                            retran.Notes = "Reference Number= " + xdoc.DocumentElement.Attributes["referenceNo"].Value + ", mode=" + xdoc.DocumentElement.Attributes["mode"].Value + ", transactionType=" + xdoc.DocumentElement.Attributes["transactionType"].Value
                                + ", status=" + xdoc.DocumentElement.Attributes["status"].Value;
                            retran.BookingId = tranToRefund.BookingId;
                            try
                            {
                                BLayer.Transaction.SaveCancelTrans(retran);
                            }
                            catch
                            {
                                string n = "Could not create transaction record. Received Response from Gateway: " + responsebody + " " + tranToRefund.Notes;
                                BLayer.Transaction.SetNote(tranToRefund.TransactionId, tranToRefund.PaymentId, n);
                            }
                        }
                        else
                        {
                            string n = responsebody + tranToRefund.Notes;
                            BLayer.Transaction.SetNote(tranToRefund.TransactionId, tranToRefund.PaymentId, n);
                            return false;
                        }
                    }

                }
            }
            catch
            {
                return false;
            }
            return true;

        }

        public static string GetValueFromResultString(string key, string source)
        {
            string rkey = key + "=";
            string[] items = source.Split('&');
            foreach (string s in items)
            {
                if (s.Contains(rkey))
                {
                    string[] val = s.Split('=');
                    if (val.Length < 2) return "";
                    return val[1].Trim();
                }
            }
            return "";
        }


        public static bool SendPayPalRefund(CLayer.Transaction tranToRefund)
        {
            try
            {

                string url = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_TOKEN_URL);
                WebRequest rqst = HttpWebRequest.Create(url);
                rqst.Method = "POST";

                string user, pwd, signature, returnurl, cancelurl;                                   //authorization details for transaction
                user = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_USER);
                pwd = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_PWD);
                signature = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_SIGNAUTRE);
                returnurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_RETURNURL);
                cancelurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_CANCELURL);



                string[] PyId = tranToRefund.PaymentId.Split('&');

                string postdata = "METHOD=RefundTransaction&VERSION=109.0";
                postdata = postdata + "&USER=" + user + "&PWD=" + pwd + "&SIGNATURE=" + signature;
                postdata = postdata + "&RefundType=Full" + "&TransactionID=" + tranToRefund.TransactionId;
                postdata = postdata + "&PAYERID=" + PyId[0];
                postdata = postdata + "&AMT=" + tranToRefund.TryAmount.ToString("F2") + "&CURRENCYCODE=USD";




                if (!String.IsNullOrEmpty(postdata))
                {

                    rqst.ContentType = "application/x-www-form-urlencoded";
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(postdata);
                    rqst.ContentLength = byteData.Length;
                    using (Stream postStream = rqst.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                        postStream.Close();
                    }
                }
                ((HttpWebRequest)rqst).KeepAlive = false;
                System.IO.StreamReader rsps = new System.IO.StreamReader(rqst.GetResponse().GetResponseStream());
                string strRsps = rsps.ReadToEnd();

                //transaction save with response parameters

                string ack = GetValueFromResultString("ACK", strRsps);
                string Transactionid = GetValueFromResultString("REFUNDTRANSACTIONID", strRsps);

                decimal Grossrefundamount = 0;
                CLayer.Transaction refundtrandt = new CLayer.Transaction();

                if (ack == "Success")
                {
                    refundtrandt.ResponseCode = CLayer.Transaction.TRAN_SUCCESS;
                    refundtrandt.Status = CLayer.ObjectStatus.TransactionStatus.Refunded;
                    Grossrefundamount = Convert.ToDecimal(tranToRefund.TryAmount.ToString());
                }
                else if (ack == "Failure")
                {
                    refundtrandt.ResponseCode = CLayer.Transaction.TRAN_FAILED;
                    refundtrandt.Status = CLayer.ObjectStatus.TransactionStatus.TryingForRefund;
                    Grossrefundamount = 0;
                }


                refundtrandt.Notes = strRsps;                                                       //all transaction response details notes
                DateTime reftransdate = DateTime.Today;
                refundtrandt.DateCreated = reftransdate;
                refundtrandt.TransactionType = CLayer.ObjectStatus.TransactionType.Refund;
                refundtrandt.TransactionId = Transactionid;
                refundtrandt.PaymentId = PyId[0];
                refundtrandt.Amount = Convert.ToDouble(tranToRefund.TryAmount.ToString());
                refundtrandt.BookingId = tranToRefund.BookingId;
                refundtrandt.TryTime = DateTime.Today;


                try
                {
                    BLayer.Transaction.SaveCancelTrans(refundtrandt);
                    BLayer.Transaction.SaveRefundAmount(refundtrandt.TransactionId, refundtrandt.PaymentId, Grossrefundamount);                //save total refund amount 
                }
                catch
                {
                    string n = "Could not create transaction record. Received Response from Gateway: " + strRsps + " " + tranToRefund.Notes;
                    BLayer.Transaction.SetNote(tranToRefund.TransactionId, tranToRefund.PaymentId, n);
                }
            }
            catch
            {
                return false;
            }
            return true;

        }

        public static double GetTotalCancellationServiceCharge(long bookingId, ref List<CLayer.Transaction> refundableTrans, decimal amountToRefund)
        {
            double charge = 0;
            double credit, debit, netbanking, cashcard, amex;
            credit = debit = netbanking = cashcard = amex = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_CREDITCARD), out credit);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_DEBITCARD), out debit);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_NETBANKING), out netbanking);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_CASHCARD), out cashcard);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_AMEX), out amex);
            double amount = 0;
            foreach (CLayer.Transaction trans in refundableTrans)
            {
                amount = trans.TotalAmount;
                switch ((CLayer.ObjectStatus.PaymentMethod)trans.PaymentType)
                {
                    case CLayer.ObjectStatus.PaymentMethod.CreditCard:
                        trans.CurrentServiceCharge = trans.TotalAmount * credit / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                    case CLayer.ObjectStatus.PaymentMethod.DebitCard:
                        trans.CurrentServiceCharge = trans.TotalAmount * debit / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                    case CLayer.ObjectStatus.PaymentMethod.NetBanking:
                        trans.CurrentServiceCharge = trans.TotalAmount * netbanking / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                    case CLayer.ObjectStatus.PaymentMethod.CashCard:
                        trans.CurrentServiceCharge = trans.TotalAmount * cashcard / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                    case CLayer.ObjectStatus.PaymentMethod.AMEX_JCB:
                        trans.CurrentServiceCharge = trans.TotalAmount * amex / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                }
                if (amount >= (double)amountToRefund)
                {
                    break;
                }
                else
                {
                    amountToRefund = amountToRefund - (decimal)amount;
                }
            }

            return charge;
        }

        public static double GetServiceCharge(CLayer.ObjectStatus.PaymentMethod payMethod)
        {
            double charge = 0;

            switch (payMethod)
            {
                case CLayer.ObjectStatus.PaymentMethod.CreditCard:
                    double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_CREDITCARD), out charge);
                    break;
                case CLayer.ObjectStatus.PaymentMethod.DebitCard:
                    double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_DEBITCARD), out charge);
                    break;
                case CLayer.ObjectStatus.PaymentMethod.NetBanking:
                    double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_NETBANKING), out charge);
                    break;
                case CLayer.ObjectStatus.PaymentMethod.CashCard:
                    double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_CASHCARD), out charge);
                    break;
                case CLayer.ObjectStatus.PaymentMethod.AMEX_JCB:
                    double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_AMEX), out charge);
                    break;
            }

            return charge;
        }
        public static double GetTotalCancellationServiceCharge(long bookingId, ref List<CLayer.Transaction> refundableTrans)
        {
            double charge = 0;
            double credit, debit, netbanking, cashcard, amex;
            credit = debit = netbanking = cashcard = amex = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_CREDITCARD), out credit);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_DEBITCARD), out debit);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_NETBANKING), out netbanking);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_CASHCARD), out cashcard);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_AMEX), out amex);
            //double refund = refundAmount;

            foreach (CLayer.Transaction trans in refundableTrans)
            {
                switch ((CLayer.ObjectStatus.PaymentMethod)trans.PaymentType)
                {
                    case CLayer.ObjectStatus.PaymentMethod.CreditCard:
                        trans.CurrentServiceCharge = trans.TotalAmount * credit / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                    case CLayer.ObjectStatus.PaymentMethod.DebitCard:
                        trans.CurrentServiceCharge = trans.TotalAmount * debit / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                    case CLayer.ObjectStatus.PaymentMethod.NetBanking:
                        trans.CurrentServiceCharge = trans.TotalAmount * netbanking / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                    case CLayer.ObjectStatus.PaymentMethod.CashCard:
                        trans.CurrentServiceCharge = trans.TotalAmount * cashcard / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                    case CLayer.ObjectStatus.PaymentMethod.AMEX_JCB:
                        trans.CurrentServiceCharge = trans.TotalAmount * amex / 100;
                        charge = charge + trans.CurrentServiceCharge;
                        break;
                }
                /*  if (refundAmount > 0)
                  {
                      if(trans.TotalAmount>= refund)
                      {
                          break;
                      }
                      else
                      {
                          refund = refund - trans.TotalAmount;
                      }
                  }*/
            }

            return Math.Round(charge * 2);
        }
        public static void UpdateAmountAndStatus(CLayer.Transaction data)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            t.UpdateAmountAndStatus(data);
        }
        public static void SetNote(string transactionId, string paymentId, string note)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            t.SetNote(transactionId, paymentId, note);
        }

        public static List<CLayer.Booking> GetAllPartialPaymentForSearch(int status, string searchString, int searchItem, int start, int limit,long userid = 0)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllPartialPaymentForSearch(status, searchString, searchItem, start, limit, userid);
        }
        public static List<CLayer.Booking> GetAllCorporateCreditBookingsForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllCorporateCreditBookingsForSearch(status, searchString, searchItem, start, limit);
        }

        public static List<CLayer.Booking> GetAllCorporateCreditBookingsRequestForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllCorporateCreditBookingsRequestForSearch(status, searchString, searchItem, start, limit);
        }
        public static List<CLayer.Booking> GetAllPartialPaymentCancelledForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllPartialPaymentCancelledForSearch(status, searchString, searchItem, start, limit);
        }
        public static void RefundAllTransactions(long bookingId)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            t.RefundAllTransactions(bookingId);
        }
        public static List<CLayer.Transaction> GetAllRefundable(long bookingId)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            return t.GetAllRefundable(bookingId);
        }
        /// <summary>
        /// For checking the NEW payment 
        /// </summary>
        /// <param name="paymentId">Payment Id</param>
        /// <returns>exist or not as boolean</returns>
        public static bool IsExist(string paymentId)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            return t.IsExist(paymentId);
        }

        public static bool IsExistForPaypal(string transactionid)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            return t.IsExistForPaypal(transactionid);
        }

        public static int getGatewaytypebybookid(long bookingid)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            return t.getGatewaytypebybookid(bookingid);
        }
        public static void Save(CLayer.Transaction data)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            t.SavePayment(data);
        }

        public static void SaveCancelTrans(CLayer.Transaction data)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            t.SaveCancelTrans(data);
        }
        public static void SaveRefundAmount(string TransactionId, string Paymentid, decimal Refundamount)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            t.SaveRefundAmount(TransactionId, Paymentid, Refundamount);
        }
        public static void updategatewaytype(string TransactionId)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            t.updategatewaytype(TransactionId);
        }
        public static void SavePartialPayment(CLayer.Transaction data)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            t.SavePartialPayment(data);
        }
        public static List<CLayer.Booking> GetNotVerified(int Status, int days, int Start, int Limit)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            return t.NotVerified(Status, days, Start, Limit);
        }

        public static List<CLayer.Booking> GetUserId(long UserId, int Start, int Limit)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            return t.VerifiedByUserId(UserId, Start, Limit);
        }

        public static List<CLayer.Booking> GetVerifiedById(int Status, long VerifiedBy, int days, int Start, int Limit)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            return t.VerifiedById(Status, VerifiedBy, days, Start, Limit);
        }
        public static int BookingStatusChange(CLayer.Booking t)
        {
            DataLayer.Transaction save = new DataLayer.Transaction();
            return save.StatusChange(t);
        }
        public static List<CLayer.Booking> VerifiedByDate(int Status, long VerifiedBy, int Start, int Limit, DateTime StartDate, DateTime EndDate)
        {
            DataLayer.Transaction t = new DataLayer.Transaction();
            return t.VerifiedByDate(Status, VerifiedBy, Start, Limit, StartDate, EndDate);
        }
        //Transaction for search with selller address
        public static List<CLayer.Booking> GetAllForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllForSearch(status, searchString, searchItem, start, limit);
        }
        public static List<CLayer.Booking> GetAllForSearchWithType(int status,int Type, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllForSearchWithType(status,Type, searchString, searchItem, start, limit);
        }
        public static List<CLayer.Booking> GetAllForSearchBookingofflineRequest(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllForSearchBookingofflineRequest(status, searchString, searchItem, start, limit);
        }
        public static List<CLayer.Booking> GetAllForSearchBookingRequest(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllForSearchBookingRequest(status, searchString, searchItem, start, limit);
        }
        public static List<CLayer.Booking> GetAllForSearchBookingRequestWithType(int status,int Type, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllForSearchBookingRequestWithType(status,Type, searchString, searchItem, start, limit);
        }
        //Transaction for fillter search with selller address
        public static List<CLayer.Booking> GetFillForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GetAllForSearch(status, searchString, searchItem, start, limit);
        }
        //Booking Buyer address
        public static List<CLayer.Address> BookedByAddressSearch(long BookingId)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.SearchBookedByAddress(BookingId);
        }

        public static List<CLayer.Booking> BookingPendingApprovalsForCorporate_GetOnUserSearch(long userid,int status,string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.BookingPendingApprovalsForCorporate_GetOnUserSearch(userid,searchString, searchItem, start, limit);
        }
        public static List<CLayer.Booking> BookingPendingApprovals_GetOnUserSearch(long userid, int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.BookingPendingApprovals_GetOnUserSearch(userid, searchString, searchItem, start, limit);
        }

        public static List<CLayer.Booking> GetBookingApprovalHistoryDetailsForCorporateAdmin(long userid,int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Transaction user = new DataLayer.Transaction();
            return user.GtBookingApprovalHistoryDetailsForCoroporateAdmin(userid,searchString, searchItem, start, limit);
        }
        //Booking For Address
        //public static List<CLayer.Address>Booking_GetBookedFor(long BookingId )
        // {
        //     DataLayer.Transaction user = new DataLayer.Transaction();
        //     return user.Booking_GetBookedFor(BookingId);
        // }
    }
}
