using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataProvider
{
    public class DataPlugFactory
    {

        private static DataPlug _instance = null;
        private static string ReadOnlyConnectionString { get; set; }
        private static string ConnectionString { get; set; }
        private static string DatabaseType { get; set; }

        public static DataPlug GetInstance()
        {
            try
            {
                if (_instance == null)
                {
                    DatabaseType = "MYSQL";
                    ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    ReadOnlyConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ReadOnlyConnection"].ConnectionString;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-IN");

                    switch (DatabaseType.ToUpper())
                    {
                        case "MYSQL":
                            _instance = new MySQLDataPlug();
                            break;
                        case "SQLSERVER":
                            _instance = new SqlDataPlug();
                            break;
                        //add other providers here
                    }
                    if (_instance != null) _instance.MakeConnection(ConnectionString, ReadOnlyConnectionString);
                }
            }
            catch (Exception ex)
            { throw ex; }
            return _instance;
        }
        //clear _instance variable for a fresh object
        public static void CleanUp()
        {
            _instance = null;
        }


    }
}
