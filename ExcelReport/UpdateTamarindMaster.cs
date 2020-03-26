using System;
using System.Data;
using System.Configuration ;
using System.Xml;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ExcelReport
{
    class UpdateTamarindMaster
    {
        public void UpdateMaster()
        {
            TamarindBasic.BaseDetailsClient client = new TamarindBasic.BaseDetailsClient();
            Tamarind.TamarindDataServiceClient client1 = new Tamarind.TamarindDataServiceClient();

            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings.Get("TamarindUserName"); 
            client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings.Get("TamarindPassword");

            client1.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings.Get("TamarindUserName");
            client1.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings.Get("TamarindPassword");

            // Use the 'client' variable to call operations on the service.
            string lsz_cityxml = client.GetCities();
            string lsz_destxml = client.GetDestinations();
            string lsz_countriesxml = client.GetCountries();
            string lsz_curencyxml = client.GetMasterCurrency();

            //XmlDocument xdoc = new XmlDocument();
            //xdoc.LoadXml(lsz_cityxml);
            //xdoc.Save(AppDomain.CurrentDomain.BaseDirectory + "website_data\\Tamarind\\City.xml");

            //xdoc.LoadXml(lsz_destxml);
            //xdoc.Save(AppDomain.CurrentDomain.BaseDirectory + "website_data\\Tamarind\\Destination.xml");

            //xdoc.LoadXml(lsz_curencyxml);
            //xdoc.Save(AppDomain.CurrentDomain.BaseDirectory + "website_data\\Tamarind\\Currency.xml");

            //xdoc.LoadXml(lsz_countriesxml);
            //xdoc.Save(AppDomain.CurrentDomain.BaseDirectory + "website_data\\Tamarind\\Country.xml");


                DataSet lds_city = new DataSet();
                DataSet lds_dest = new DataSet();
                DataSet lds_currency = new DataSet();
                DataSet lds_country = new DataSet();

            lds_city.ReadXml(GenerateStreamFromString(lsz_cityxml));
            lds_dest.ReadXml(GenerateStreamFromString(lsz_destxml));
            lds_country.ReadXml(GenerateStreamFromString(lsz_countriesxml));
            lds_currency.ReadXml(GenerateStreamFromString(lsz_curencyxml));

            //lds_city.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "website_data\\Tamarind\\City.xml");
            //lds_dest.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "website_data\\Tamarind\\Destination.xml");
            //lds_currency.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "website_data\\Tamarind\\Currency.xml");
            //    lds_country.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "website_data\\Tamarind\\Country.xml");

                DataTable ldt_city = lds_city.Tables["City"];
                DataTable ldt_des = lds_dest.Tables["City"];
                DataTable ldt_currency = lds_currency.Tables["Currency"];
                DataTable ldt_country = lds_country.Tables["country"];

                //city
                for (int i = 0; i < lds_city.Tables["City"].Rows.Count; i++)
                {
                    BLayer.Tamarind.UpdateCity((lds_city.Tables["City"].Rows[i][0]).ToString(), (lds_city.Tables["city"].Rows[i][1]).ToString());
                }
                //destination
                for (int i = 0; i < lds_dest.Tables["City"].Rows.Count; i++)
                {
                    BLayer.Tamarind.UpdateCity((lds_dest.Tables["City"].Rows[i][1]).ToString(), (lds_dest.Tables["city"].Rows[i][0]).ToString());
                }
                //Currency
                for (int i = 0; i < ldt_currency.Rows.Count; i++)
                {
                    BLayer.Tamarind.UpdateCurrency((ldt_currency.Rows[i][0]).ToString());
                }
                //Country
                for (int i = 0; i < ldt_country.Rows.Count; i++)
                {
                    BLayer.Tamarind.UpdateCountry((ldt_country.Rows[i][1]).ToString());
                }
 
        }
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
    
}
