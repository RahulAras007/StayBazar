using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReport
{
    class Program
    {
        private const string PROPERTYREPORT = "ALLPROPERTIES";
        private const string PROPERTYDESCRIPTIONANDIMAGE = "ALLPROPERTYDESCANDIMAGE";
        private const string CANCELINCOMPLETEGDSBOOKINGS = "CANCELGDSBOOKINGS";
        private const string REMOVEDEADIMAGES = "REMOVEDEADIMAGES";
        private const string CLEARGDSLOG = "CLEARGDSLOG";
        private const string UPDATEGDSRATES = "UPDATEGDSRATES";
        private const string UPDATEGDSSTARRATINGS = "UPDATEGDSSTARRATINGS";
        private const string UPDATETAMARINDMASTER = "UPDATETAMARINDMASTER";
        private const string UPDATETBOMASTER = "UPDATETBOMASTER";
        private const string UPDATETBOHOTELCITYEXCEL = "UPDATETBOHOTELCITYEXCEL";

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid Parameters");
                Console.WriteLine("Parameters (only one at a time)");
                Console.WriteLine("------------------------------");
                Console.WriteLine("ALLPROPERTIES - Generate excel report with all properties");
                Console.WriteLine("ALLPROPERTYDESCANDIMAGE - Download Amadeus Hotel's images");
                Console.WriteLine("CANCELGDSBOOKINGS - Cancel incomplete booking");
                Console.WriteLine("REMOVEDEADIMAGES - Remove dead image urls");
                Console.WriteLine("CLEARGDSLOG - clear gds transaction log");
                Console.WriteLine("UPDATEGDSRATES - update gds rates transaction log");
                Console.WriteLine("UPDATEGDSSTARRATINGS - update gds star ratings");
                Console.WriteLine("UPDATETAMARINDMASTER - update tamarind master");
                Console.WriteLine("UPDATETBOMASTER - update TBO master");
                Console.WriteLine("UPDATETBOHOTELCITYEXCEL - update TBO Hotel Cities from Excel");


                LogHandler.AddLog("Using Default ConnectionString " + System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                LogHandler.AddLog("Using ReadOnly ConnectionString " + System.Configuration.ConfigurationManager.ConnectionStrings["ReadOnlyConnection"].ConnectionString);


                //   Console.ReadLine();
                
                return;
            }
            try
            {
                if (args[0].ToUpper() == PROPERTYREPORT)
                {
                    PropertyReport pr = new PropertyReport();
                    pr.GenerateReport();
                }
                else if (args[0].ToUpper() == PROPERTYDESCRIPTIONANDIMAGE)
                {
                    //update image and description
                    PropertyDetails pd = new PropertyDetails();

                    if (pd.UpdatePropertyDescriptionandImages())
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Property images and descriptions successfully updated");
                        Console.WriteLine("------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Error in updating images and descriptions");
                        Console.WriteLine("------------------------------");
                    }

                }
                else if (args[0].ToUpper() == CANCELINCOMPLETEGDSBOOKINGS)
                {
                    //update image and description
                    CancelGDSBookings pc = new CancelGDSBookings();
                    if (pc.CancelInCompleteGDSBookings())
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Invalid GDS Bookings successfully cancelled");
                        Console.WriteLine("------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Invalid GDS Bookings cancellation failed");
                        Console.WriteLine("------------------------------");
                    }

                }
                else if (args[0].ToUpper() == REMOVEDEADIMAGES)
                {
                    ImageUrlFixer fx = new ImageUrlFixer();
                    fx.FixInvalidPicturesAll();
                }
                else if (args[0].ToUpper() == CLEARGDSLOG)
                {
                    CleadGDSTransactionLog clr = new CleadGDSTransactionLog();
                    clr.ClearGDSLog();

                }
                else if (args[0].ToUpper() == UPDATEGDSRATES)
                {
                    UpdateGDSRates clr = new UpdateGDSRates();
                    clr.UpdatePropertyRates();
                }
                else if (args[0].ToUpper() == UPDATEGDSSTARRATINGS)
                {
                    UpdateStarRating clr = new UpdateStarRating();
                    clr.UpdatePropertyStarRatings();
                    
                }
                else if (args[0].ToUpper() == UPDATETAMARINDMASTER)
                {
                    UpdateTamarindMaster  clr = new UpdateTamarindMaster();
                    clr.UpdateMaster();
                }
                else if (args[0].ToUpper() == UPDATETBOMASTER)
                {
                    UpdateTBO clr = new UpdateTBO();
                    clr.UpdateMaster();
                }
                else if (args[0].ToUpper() == UPDATETBOHOTELCITYEXCEL)
                {
                    UpdateTBO clr = new UpdateTBO();
                    clr.UpdateHotelCityExcel();
                }
                else
                {
                    Console.WriteLine("Invalid Argument");
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex);
            }
        }
    }
}
