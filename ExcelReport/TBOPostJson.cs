using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReport
{
    class TBOPostJson
    {
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string GetResponse(string requestData, string url)
        {
            string responseXML = string.Empty;
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(requestData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
                WebResponse webResponse = request.GetResponse();
                Stream rsp = webResponse.GetResponseStream();
                if (rsp == null)
                {
                    //throw exception
                }
                //using (StreamReader readStream = new StreamReader(new GZipStream(rsp, CompressionMode.Decompress)))
                using (StreamReader readStream = new StreamReader(rsp))
                {
                    responseXML = JsonConvert.DeserializeXmlNode(readStream.ReadToEnd(), "Main").InnerXml;
                }
                return responseXML;
            }
            catch (WebException webEx)
            {
                //get the response stream
                WebResponse response = webEx.Response;
                Stream stream = response.GetResponseStream();
                String responseMessage = new StreamReader(stream).ReadToEnd();
                return responseMessage;
            }
        }
    }
}
