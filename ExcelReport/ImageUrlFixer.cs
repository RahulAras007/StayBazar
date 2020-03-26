using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using MySql.Data.MySqlClient;

namespace ExcelReport
{
    public class ImageUrlFixer
    {
        public void FixInvalidPicturesAll()
        {
            string sql = "SELECT id,imageurl FROM gdspropertyimageurls WHERE  (IsValid IS NULL) OR (IsValid AND  TIMESTAMPDIFF(DAY,IFNULL(LastChecked,DATE_ADD(CURDATE(),INTERVAL -5 DAY)) , NOW()) > 3)  LIMIT 100";
           
            MySqlConnection _connection = new MySqlConnection();
            const int IMAGE_FOUND = 1;
            const int IMAGE_NOTFOUND = 2;
            const int IMAGE_ERROR = 3;
            const int IMAGE_ERROR_MAXCOUNT = 3;

            try
            {
                StringBuilder validIds, deleteIds;
                validIds = new StringBuilder();
                deleteIds = new StringBuilder();
                DataTable dt = null;

                MySqlCommand _cmd = new MySqlCommand();

                _connection = new MySqlConnection();
                _connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                _cmd.CommandText = sql;
                _cmd.CommandType = CommandType.Text;
                _cmd.Connection = _connection;

                MySqlCommand _execmd = new MySqlCommand();

                MySqlDataAdapter _dada = new MySqlDataAdapter(_cmd);
                HttpWebRequest request;
                string imageUrl;
                long imageId = 0;
                int imageExist;
                bool vadded, dadded;
                int imageDownloadError = 0;
                int maxRepetition = 0;
                while (maxRepetition < 200)
                {
                    maxRepetition++;

                    validIds.Clear();
                    deleteIds.Clear();
                    
                    try
                    {
                        dt = new DataTable();
                        

                        _connection.Open();
                        _dada.Fill(dt);
                    }
                    finally
                    {
                        if(_connection.State == ConnectionState.Open) { _connection.Close(); }
                    }
                    
                    if(dt == null | dt.Rows.Count ==0 )
                    {
                        break;
                    }
                    vadded = false;
                    dadded = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        imageExist = IMAGE_NOTFOUND;
                        imageId = DataConv.ToLong(dr["id"]);
                        imageUrl = DataConv.ToString(dr["imageurl"]);

#if DEBUG
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif
                        request = (HttpWebRequest)HttpWebRequest.Create(imageUrl);
#if !DEBUG
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif
                        request.Timeout = 10000; //10 seconds
                        request.Method = "HEAD";
                        try {
                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    imageExist = IMAGE_FOUND;
                                    Console.WriteLine("Image Found - " + imageUrl);
                                }
                            }
                          
                        }
                        catch(Exception ex)
                        {
                            if(ex.Message.Contains("404"))
                            {
                                imageExist = IMAGE_NOTFOUND;
                                Console.WriteLine("Image NOT Found - " + imageUrl);
                            }
                            else
                            {
                                imageExist = IMAGE_ERROR;
                                imageDownloadError++;
                                //  LogHandler.HandleError(ex);
                                Console.WriteLine("ERROR - " + ex.Message + " -- " + imageUrl);
                                if (imageDownloadError >= IMAGE_ERROR_MAXCOUNT)
                                {
                                    return;//do not run this function.... it may be due to broken internet connection
                                }
                            }
                        }
                        if(imageExist==IMAGE_FOUND)
                        {
                            if (vadded) { validIds.Append(","); } else { vadded = true; }
                            validIds.Append(imageId);
                        }
                        else if(imageExist == IMAGE_NOTFOUND)
                        {
                            if(dadded) { deleteIds.Append(","); } else { dadded = true; }
                            deleteIds.Append(imageId);
                        }

                    }
                    try
                    {
                        _execmd.Connection = _connection;
                        _connection.Open();
                        
                        if (vadded)
                        {
                            _execmd.CommandText = "Update gdspropertyimageurls set IsValid=1,LastChecked=NOW() Where id in(" + validIds.ToString() + ")";
                            _execmd.ExecuteNonQuery();
                        }
                        if (dadded)
                        {
                            _execmd.CommandText = "Delete From gdspropertyimageurls Where id in(" + deleteIds.ToString() + ")";
                            _execmd.ExecuteNonQuery();
                        }
                    }
                    finally
                    {
                        if(_connection.State == ConnectionState.Open) { _connection.Close(); }
                    }
                } 

            }catch(Exception ex)
            {
                LogHandler.HandleError(ex);
            }


        }
    }
}
