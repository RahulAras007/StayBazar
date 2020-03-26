using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace ExcelReport
{
    class TBOFunctions
    {
        public string GetToken()
        {
            TBOAUTH authenticate = new TBOAUTH();

            authenticate.ClientId = "ApiIntegrationNew";
            authenticate.UserName = "STAYBAZAR";
            authenticate.Password = "STAYBAZAR@123";
            authenticate.EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(); //"122.166.15.26";

            string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(authenticate);
            string authurl = "http://api.tektravels.com/SharedServices/SharedData.svc/rest/Authenticate";
            string responsedata;
            responsedata = TBOPostJson.GetResponse(requestdata, authurl);

            DataSet lds_auth = new DataSet();

            //lds_auth.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "Authenticate.xml");
            lds_auth.ReadXml(TBOPostJson.GenerateStreamFromString(responsedata));

            string tokenid;
            tokenid = "";
            if (int.Parse(lds_auth.Tables["Error"].Rows[0]["ErrorCode"].ToString()) == 0)
            {
                // no error has occured
                if (int.Parse(lds_auth.Tables["Main"].Rows[0]["Status"].ToString()) == 1)
                {
                    // status is ok
                    tokenid = lds_auth.Tables["Main"].Rows[0]["TokenId"].ToString();
                }

            }

            return tokenid;
        }
        public void GetCountry(string tokenid)
        {
            Country authenticate = new Country()
            {
                ClientId = "ApiIntegrationNew",
                EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(), //"122.166.15.26";
                TokenId = tokenid,
            };

            string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(authenticate);
            string authurl = "http://api.tektravels.com/SharedServices/SharedData.svc/rest/CountryList";

            string responsedata = TBOPostJson.GetResponse(requestdata, authurl);

            DataSet lds_auth = new DataSet();
            lds_auth.ReadXml(TBOPostJson.GenerateStreamFromString(responsedata));

            string lsz_countrylist = "";

            if (int.Parse(lds_auth.Tables["Error"].Rows[0]["ErrorCode"].ToString()) == 0)
            {
                if (int.Parse(lds_auth.Tables["Main"].Rows[0]["Status"].ToString()) == 1)
                {
                    // status is ok
                    lsz_countrylist = lds_auth.Tables["Main"].Rows[0]["CountryList"].ToString();
                }
            }

            if (lsz_countrylist != "")
            {
                DataSet lds_country = new DataSet();
                lds_country.ReadXml(TBOPostJson.GenerateStreamFromString(lsz_countrylist));
                for (int i = 0; i < lds_country.Tables["Country"].Rows.Count; i++)
                {
                    BLayer.TBO.UpdateCountry((lds_country.Tables["Country"].Rows[i][0]).ToString(),
                            (lds_country.Tables["Country"].Rows[i][1]).ToString());
                }
            }
        }
       
        //public void GetDestination(string tokenid)
        //{
        //    destination authenticate = new destination()
        //    {
        //        ClientId = "ApiIntegrationNew",
        //        EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(), //"122.166.15.26";
        //        TokenId = tokenid,
        //        CountryCode = "IN"
        //    };

        //    string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(authenticate);
        //    string authurl = "http://api.tektravels.com/SharedServices/SharedData.svc/rest/DestinationCityList";

        //    string responsedata = TBOPostJson.GetResponse(requestdata, authurl);

        //    DataSet lds_auth = new DataSet();
        //    lds_auth.ReadXml(TBOPostJson.GenerateStreamFromString(responsedata));
        //}
        public void GetDestinationCityID()
        {
            FileStream stream = File.Open(AppDomain.CurrentDomain.BaseDirectory + "website_data\\tbo\\NewHotelCityCode.xlsx", FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            int result1;
            //DataSet result = excelReader.AsDataSet();
            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
            {
                try
                {
                    result1 = BLayer.TBO.UpdateDestinationCityId(Convert.ToInt32(result.Tables[0].Rows[i][0]),
                    (result.Tables[0].Rows[i][1]).ToString(),
                    (result.Tables[0].Rows[i][2]).ToString(),
                    (result.Tables[0].Rows[i][3]).ToString(),
                    (result.Tables[0].Rows[i][4]).ToString(),
                    (result.Tables[0].Rows[i][5]).ToString()
                    );
                    Console.WriteLine("{0},{1},{2}",i,result.Tables[0].Rows[i][1], result1);
                }
                catch (Exception ex)
                {
                    LogHandler.LogError(ex);
                }
                
            }
        }

        //public void GetHotelSearch(string tokenid)
        //{
        //    HotelSearch authenticate = new HotelSearch
        //    {
        //        EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
        //        TokenId = tokenid,
        //        CheckInDate = DateTime.Now.ToString("dd/MM/yyyy"),
        //        NoOfNights = 1,
        //        CountryCode = "IN",
        //        CityId = 44740,
        //        PreferredCurrency = "INR",
        //        GuestNationality = "IN",
        //        NoOfRooms = "1",
        //        MaxRating = 5,
        //        MinRating = 0,
        //        PreferredHotel = "",
        //        IsNearBySearchAllowed = false,
        //    };
        //    authenticate.RoomGuests.Add(new RoomGuests()
        //    {
        //        NoOfAdults = 1,
        //        NoOfChild = 0,
        //    });

        //    string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(authenticate);
        //    string authurl = "http://api.tektravels.com/BookingEngineService_Hotel/hotelservice.svc/rest/GetHotelResult/";

        //    string responsedata = TBOPostJson.GetResponse(requestdata, authurl);

        //}
    }
}
