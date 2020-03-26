using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.SystemLogger logger = new Common.SystemLogger();
            try
            {
                int i = 0;
//#if !DEBUG 
                if (args.Count() == 0 || args.Count() > 1)
                {
                    HelpOut();
                    return;
                }
                
                int.TryParse(args[0], out i);
//#endif
#if DEBUG 
                //i = 3;
              //  throw new Exception("testing");
#endif
                switch (i)
                {
                    case 1:
                        Common.SupplierMail sm = new Common.SupplierMail();
                        sm.Start();
                        break;
                    case 2:
                        Common.FailedTransactionMail failed = new Common.FailedTransactionMail();
                        failed.Start();
                        break;
                    case 3:
                        Common.BookingCartMail Bookingcart = new Common.BookingCartMail();
                        Bookingcart.Start();
                        break;
                    case 4:
                        Common.PartialPaymentListForMail PartialPayment = new Common.PartialPaymentListForMail();
                        PartialPayment.Start();
                        break;
                    case 5:
                        Common.PartialPaymentBCancellation PartialPaymentBCancel = new Common.PartialPaymentBCancellation();
                        PartialPaymentBCancel.Start();
                        break;
                    case 6:
                        Common.Feedback Feedback = new Common.Feedback();
                        Feedback.Start();
                        break;
                    default:
                        HelpOut();
                        break;
                }
                //Common.SupplierMail sm = new Common.SupplierMail();
                //sm.Start();
            }catch(Exception ex)
            {
                logger.LogError(ex.Message );
            }
            Console.WriteLine("Finished..");
#if DEBUG
           // Console.ReadLine();
#endif
        }


        public static void HelpOut()
        {
            Console.WriteLine("Mailer for Staybazar");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Mailer [1|2|3]");
            Console.WriteLine("1 - Supplier Daily Mail");
            Console.WriteLine("2 - Failed Transactions");
            Console.WriteLine("3 - Booking Cart Mail");
            Console.WriteLine("4 - Partial Payment Mail");
            Console.WriteLine("5 - Partial Payment Booking Cancellation Mail");
            Console.WriteLine("6 - Feedback");
        }


    }
}
