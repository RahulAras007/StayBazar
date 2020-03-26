using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;


namespace GDSProcess
{
    public static class GDSFormatDescription
    {
        #region VARIABLES
        public static int StarRatings;
        public static  string HotelDetails = string.Empty;
        public static string CancellationPolicy = string.Empty;
        public static  string AreaInformation = string.Empty;
        public  static string RoomSafetyAndSecurity = string.Empty;
        public static  string Features = string.Empty;
        public static string Reception = string.Empty;
        #endregion

        #region FUNCTIONS

        public static string GetFormattedDescription(string xmlResponse)
        {
            string result = string.Empty;
            List<CLayer.ContentFormat> objHotelDetails = GetHotelDetails(xmlResponse);
            List<CLayer.DetailContents> Output = FormattedDescription(xmlResponse);
            Output = Output.Where(x => x.Details != null).ToList();

           
            foreach (var item in Output.OrderBy(X => X.Order))
            {
                if (item.Section.ToLower() == "about")
                {
                     result +=  FirstLetterToUpperCase(item.Details) + "#256#";
                }
                else
                {
                    result += "<br clear='All'/><b>" + item.Section + "</b><br/>" + "<div>" + FirstLetterToUpperCase(item.Details) + "</div>";
                }

            }
            return result;
        }
        public static string GetRatings(string xmlResponse)
        {
            string result = string.Empty;
            List<CLayer.ContentFormat> objHotelDetails = GetHotelDetails(xmlResponse);
            List<CLayer.DetailContents> Output = FormattedDescription(xmlResponse);
            Output = Output.Where(x => x.Details != null).ToList();


            foreach (var item in Output.OrderBy(X => X.Order))
            {
                if (item.Section.ToLower() == "about")
                {
                    result += FirstLetterToUpperCase(item.Details) + "#256#";
                }
                else
                {
                    result += "<br clear='All'/><b>" + item.Section + "</b><br/>" + "<div>" + FirstLetterToUpperCase(item.Details) + "</div>";
                }

            }
            return result;
        }

        public static string GetGeneralDescription(List<CLayer.ContentLines> pContentLines, string InfoCode = "", string AdditionalDetailCode = "")
        {
            string result = string.Empty;

            string TblHeader = string.Empty;
            string HotelDescription = string.Empty;

            int i = 0;


            foreach (var item in pContentLines)
            {
                string subTitle = GetShortTitle(item.Description);
                if (subTitle == "<b>" + item.Description + "</b>")
                    subTitle = item.Description;
                if (item.Description.Contains("."))
                {
                    if (HasAlphaNumbericChars(item.Description))
                    {
                        if (item.Description.EndsWith("."))
                        {
                            HotelDescription = HotelDescription + subTitle + "<br/>";
                        }
                        else
                        {
                            HotelDescription = HotelDescription + subTitle;
                        }

                    }

                }
                else
                {

                    if (subTitle.Contains("- stars"))
                    {
                        string Ratings = string.Empty;
                        try
                        {
                            Ratings = subTitle.Replace("-", "").Replace("stars", "");
                            if (HasLettercChars(Ratings.ToString()))
                            {
                                StarRatings = 0;
                            }
                            else
                            {
                                StarRatings = HasNumericChars(Ratings.Trim()) ? Convert.ToInt32(Ratings.Trim()) : 0;
                            }
                        }
                        catch(Exception ex)
                        {
                            StarRatings = 0; ;
                            throw ex;
                        }
                     
                        // Convert.ToInt32(Ratings.Trim());
                        subTitle = "";
                    }
                    else if (subTitle.ToLower().Contains("star rating"))
                    {
                        string Ratings = string.Empty;
                        try
                        {
                            Ratings = subTitle.Substring(subTitle.Length - 1, 1);
                            if (!HasNumericChars(Ratings))
                            {
                                Ratings = subTitle.Replace("star rating", "").Replace("-", "").Replace("stars", "").Trim();
                            }

                            if (HasLettercChars(Ratings.ToString()))
                            {
                                if (subTitle.ToLower().Contains("star rating-"))
                                {
                                    subTitle = subTitle.Replace("stars", "").Trim();
                                    Ratings = subTitle.ToLower().Replace("star rating-", "").Replace("-", "").Trim();
                                    if (HasLettercChars(Ratings.ToString()))
                                    {
                                        StarRatings = 0;
                                    }
                                    else
                                    {
                                        StarRatings = HasNumericChars(Ratings.Trim()) ? Convert.ToInt32(Ratings.Trim()) : 0;
                                    }
                                    // Convert.ToInt32(Ratings.Trim());
                                    subTitle = "";
                                }
                                else
                                {
                                    StarRatings = 0;
                                }

                            }
                            else
                            {
                                if (HasLettercChars(Ratings.ToString()))
                                {
                                    StarRatings = 0;
                                }
                                else
                                {
                                    StarRatings = HasNumericChars(Ratings.Trim()) ? Convert.ToInt32(Ratings.Trim()) : 0;
                                }

                            }
                        }
                        catch(Exception ex)
                        {
                            StarRatings = 0;
                            throw ex;
                        }
                         
                        // Convert.ToInt32(Ratings.Trim());
                        subTitle = "";
                    }
                     if (subTitle.ToLower().Contains("star rating-"))
                    {
                        try
                        {
                            subTitle = subTitle.Replace("stars", "").Trim();

                            string Ratings = subTitle.ToLower().Replace("star rating-", ""); //subTitle.Substring(subTitle.Length - 1, 1);
                            if (HasLettercChars(Ratings.ToString()))
                            {
                                StarRatings = 0;
                            }
                            else
                            {
                                StarRatings = Convert.ToInt32(Ratings.Trim());
                            }
                        }
                        catch(Exception ex)
                        {
                            StarRatings = 0; ;
                            throw ex;
                        }

                        // Convert.ToInt32(Ratings.Trim());
                        subTitle = "";
                    }
                    if (subTitle.ToLower().Contains("ratings"))
                    {
                        subTitle = "";
                    }
                    if (subTitle.ToLower().Contains("other description"))
                    {
                        subTitle = ReplaceStringTOEmpty("other description", "").Replace("-", "").Trim();
                    }
                    if (subTitle.ToLower().Contains("additional hotel description"))
                    {
                        subTitle = "";
                    }
                    if (subTitle.ToLower().Contains("cancellation policy:"))
                    {
                        subTitle = "<b>Cancellation policy</b><br/>";
                    }
                    if (subTitle.ToLower().Contains("property description"))
                    {
                        subTitle = "";
                    }

                    if ((InfoCode == "2") && (AdditionalDetailCode == ""))
                    {
                        subTitle = subTitle + "<br/>";
                    }

                    HotelDescription = HotelDescription + subTitle;
                }
                i = i + 1;

            }

            if ((InfoCode == "1") && (AdditionalDetailCode == "3"))
            {
                HotelDescription = "<b>Location and area information</b><br/>" + HotelDescription; //GetLocation(HotelDescription);
            }
            else if ((InfoCode == "1") && (AdditionalDetailCode == "2"))
            {
                HotelDescription = "<b>Property type</b><br/>" + HotelDescription; //GetLocation(HotelDescription);
            }
            else if ((InfoCode == "") && (AdditionalDetailCode == "17"))
            {
                HotelDescription = "<b>Features</b><br/>" + HotelDescription;
            }
            else if ((InfoCode == "5") && (string.IsNullOrEmpty(AdditionalDetailCode)))
            {
                HotelDescription = "<b>Property type</b><br/>" + HotelDescription; //GetLocation(HotelDescription);
            }
            else
            {
                HotelDescription = HotelDescription + "<br/>";
            }

            result = HotelDescription;
            return result;
        }

        public static string GetAreaDescription1(List<CLayer.ContentLines> pContentLines)
        {

            string result = string.Empty;
            try
            {
                string Col1 = string.Empty;
                string Col2 = string.Empty;

                //table start
                result = result + "<table cellspacing='0', cellpadding='10'>";
                string HotelDescription = string.Empty;
                string DescriptionHotel = "";


                //     string SubTitle = GetSubTitle(pContentLines[0].InfoCode , pContentLines[0].AdditionalDetailCode);
                string TblHeader = string.Empty;


                int i = 0;
                string itemDescription = string.Empty;
                foreach (var item in pContentLines)
                {
                    string a = item.Description;
                    itemDescription = a;

                    if ((a.Contains(" MI")) && (a.Contains(" KM")))
                    {
                        if (a.Contains("distance from"))
                        {
                        }
                        else if (a.Contains("distance to"))
                        {
                        }
                        else
                        {
                            string[] a1 = a.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

                            int nCol1 = a.IndexOfAny("0123456789".ToCharArray());

                            a = a.Replace("-", "").Trim();

                            Col1 = StringHelper.ToTitleCase(a, TitleCase.First).Substring(0, nCol1 - 2).Trim();
                            Col2 = a.Substring(nCol1 - 2);
                            int nColLength = Col2.Length;
                            Col2 = (Col2.Length < 15) ? Col2.Substring(0, nColLength) : Col2.Substring(0, 15);
                            Col2 = (!Col2.Contains(" KM")) ? Col2 + " KM" : Col2;


                            result = result + "<tr>";
                            Col1 = "<td style='text-align:left;border: 1px solid #ddd;padding:10px;'>" + StringHelper.ToTitleCase(Col1.Trim(), TitleCase.First) + "</td>";
                            Col2 = "<td style='text-align:left;border: 1px solid #ddd;padding:10px;'>" + Col2 + "<td>";
                            result = result + Col1.Replace(" ."," ").Replace(".","") + Col2;
                            result = result + "</tr>";
                        }

                    }
                }
                DescriptionHotel = "<b>Location and area information</b><br/>" + HotelDescription;

                if (Col1 == "")
                {
                    result = "<b>Area information</b><br/><table><tr><td>" + itemDescription + "</td<</tr></table>";
                }
                else
                {
                    result = "<b>Area information</b><br/>" + result + "</table>";
                }


                //table end
                


            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;

        }
        public static string GetAreaDescription(List<CLayer.ContentLines> pContentLines)
        {

            string result = string.Empty;
            try
            {
                string Col1 = string.Empty;
                string Col2 = string.Empty;

                //table start
                result = result + "<ul class='AreaContainter'>";
                string HotelDescription = string.Empty;
                string DescriptionHotel = "";


                //     string SubTitle = GetSubTitle(pContentLines[0].InfoCode , pContentLines[0].AdditionalDetailCode);
                string TblHeader = string.Empty;


                int i = 0;
                string itemDescription = string.Empty;
                foreach (var item in pContentLines)
                {
                    string a = item.Description;
                    itemDescription = a;

                    if ((a.Contains(" MI")) && (a.Contains(" KM")))
                    {
                        if (a.Contains("distance from"))
                        {
                        }
                        else if (a.Contains("distance to"))
                        {
                        }
                        else
                        {
                            string[] a1 = a.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

                            int nCol1 = a.IndexOfAny("0123456789".ToCharArray());

                            a = a.Replace("-", "").Trim();

                            Col1 = StringHelper.ToTitleCase(a, TitleCase.First).Substring(0, nCol1 - 2).Trim();
                            Col2 = a.Substring(nCol1 - 2);
                            int nColLength = Col2.Length;
                            Col2 = (Col2.Length < 15) ? Col2.Substring(0, nColLength) : Col2.Substring(0, 15);
                            Col2 = (!Col2.Contains(" KM")) ? Col2 + " KM" : Col2;


                            result = result + "<li>";
                            Col1 = "<div class='place'>" + StringHelper.ToTitleCase(Col1.Trim(), TitleCase.First) + "</div>";
                            Col2 = "<div class='distance'>" + Col2 + "<div>";
                            result = result + Col1.Replace(" .", " ").Replace(".", "") + Col2;
                            result = result + "</li>";
                        }

                    }
                }
                DescriptionHotel = "<b>Location and area information</b><br/>" + HotelDescription;

                if (Col1 == "")
                {
                    result = "<b>Area information</b><br/><ul><li><div>" + itemDescription + "</div></li></ul>";
                }
                else
                {
                    result = "<b>Area information</b><br/>" + result + "</ul>";
                }


                //table end



            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;

        }

        public static string GetShortTitle(string pValue)
        {
            string result = string.Empty;
            try
            {
                if (pValue.ToLower().Contains("ratings"))
                {
                    result = "<b>Ratings</b><br/>";
                }
                else if (pValue.ToLower().Contains("free services"))
                {
                    result = "<b>Free services</b><br/>";
                }
                else if (pValue.ToLower().Contains("property description"))
                {
                    result = "<b>Property description</b><br/>";
                }
                else if (pValue.ToLower().Contains("kichen"))
                {
                    result = "<b>The Kitchen</b><br/>";
                }
                else if (pValue.ToLower().Contains("free services"))
                {
                    result = "<b>Free services</b><br/>";
                }
                else if (pValue.ToLower().Contains("additional hotel description"))
                {
                    result = "<b>Additional hotel description</b><br/>";
                }
                else if (pValue.ToLower().Contains("location and area information"))
                {
                    result = "<b>Location and area information</b><br/>";
                }
                else if (pValue.ToLower().Contains("contact"))
                {
                    result = "<b>Contact</b><br/>";
                }
                else if (pValue.ToLower().Contains("room safety and security"))
                {
                    result = "<b>Room safety and security</b><br/>";
                }
                else if (pValue.ToLower() == "cancellation policy")
                {
                    result = "<b>Cancellation policy</b><br/>";
                }
                else if (pValue.ToLower().Contains("property type"))
                {
                    result = "<b>Property type</b><br/>";
                }
                else if (pValue.ToLower() == "reception")
                {
                    result = "<b>Reception</b><br/>";
                }
                else if (pValue.ToLower().Contains("additional property location"))
                {
                    result = "<b>Additional property location</b><br/>";
                }
                else if (pValue.ToLower().Contains("general data"))
                {
                    result = "<b>General data</b><br/>";
                }
                else
                {
                    result = " " + pValue;
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }
        private static CLayer.GDSBookingDetailsRatings.Envelope GetStarRatings(string pValue, Serializer ser)
        {
            CLayer.GDSBookingDetailsRatings.Envelope result = null;
            try
            {

                result  = ser.Deserialize<CLayer.GDSBookingDetailsRatings.Envelope>(pValue);
            }
            catch (Exception ex2)
            {
                result = null;
            }
            finally
            {

            }
            return result;
        }
        private static CLayer.GDSBookingDetailsRatingsAdv.Envelope GetStarRatingsMain(string pValue, Serializer ser)
        {
            CLayer.GDSBookingDetailsRatingsAdv.Envelope result = null;
            try
            {

                result = ser.Deserialize<CLayer.GDSBookingDetailsRatingsAdv.Envelope>(pValue);
            }
            catch (Exception ex2)
            {
                result = null;
            }
            finally
            {

            }
            return result;
        }


        public static  List<CLayer.ContentFormat> GetHotelDetails(string DetailXML)
        {
            List<CLayer.ContentFormat> objContentList = new List<CLayer.ContentFormat>();
            List<CLayer.ContentFormat> objAreaContentList = new List<CLayer.ContentFormat>();
            List<CLayer.ContentFormat> objAffiliationContentList = new List<CLayer.ContentFormat>();
            List<CLayer.ContentFormat> objPolicyContentList = new List<CLayer.ContentFormat>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.LoadXml(DetailXML);

                Serializer ser = new Serializer();

                try
                {
                    #region GDS Descriptions
                    CLayer.GDSBookingDetails.Envelope ss = new CLayer.GDSBookingDetails.Envelope();
                    ss = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(xmlDoc.InnerXml.ToString());


                   CLayer.GDSBookingDetailsRatingsAdv.Envelope ssMainRating = new CLayer.GDSBookingDetailsRatingsAdv.Envelope();
                    ssMainRating =  GetStarRatingsMain(xmlDoc.InnerXml.ToString(), ser);


                    CLayer.GDSBookingDetailsRatings.Envelope ssRating = new CLayer.GDSBookingDetailsRatings.Envelope();
                     ssRating = GetStarRatings(xmlDoc.InnerXml.ToString(), ser);



                    int u = 1;
                    #region HotelInfo
                    try
                    {
                        if (ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo != null)
                        {
                            var description = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;

                            if (description != null)
                            {
                                var descriptionItem = description.MultimediaDescriptions.MultimediaDescription;

                                var Descriptions = from order in descriptionItem
                                                   where order.TextItems != null
                                                   select order;
                                string HotelDescription = string.Empty;
                                string HotelLocationDescription = string.Empty;
                                string OutPut = "";
                                string Ratings = "0";
                                int BlockType = 0;


                                //IEnumerable<GDSBookingDetails.MultimediaDescription> HotelLocationDescriptionList = Descriptions.Where(x => (x.InfoCode == "1" && x.AdditionalDetailCode == "3"));
                                //if(HotelLocationDescriptionList!=null)

                                //HotelLocationDescription = GetHotelLocation(HotelLocationDescriptionList);

                                // int u = 1;
                                foreach (var desc in Descriptions)
                                {


                                    CLayer.ContentFormat objContentFormat = new CLayer.ContentFormat();
                                    objContentFormat.BlockType = u;
                                    objContentFormat.InfoCode = desc.InfoCode;
                                    objContentFormat.AdditionalDetailCode = desc.AdditionalDetailCode;
                                    List<CLayer.ContentLines> objLinesList = new List<CLayer.ContentLines>();

                                    foreach (var datas in desc.TextItems.TextItem.Description)
                                    {
                                        if (datas.__Text != null)
                                        {
                                            if (datas.__Text.ToLower().Contains("contact"))
                                            {
                                                break;
                                            }
                                           
                                            else
                                            {
                                                CLayer.ContentLines objLines = new CLayer.ContentLines();
                                                objLines.Description = datas.__Text;
                                                objLinesList.Add(objLines);
                                            }
                                        }


                                    }
                                    if (objLinesList.Count > 0)
                                    {
                                        objContentFormat.lines = objLinesList;
                                        objContentList.Add(objContentFormat);
                                        u = u + 1;
                                    }

                                }


                                //   return objContentList;





                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        objContentList = null;
                        //if (ssAdv.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo != null)
                        //{
                        //    var description = ssAdv.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;

                        //    if (description != null)
                        //    {
                        //       // var descriptionItem = description.MultimediaDescriptions.MultimediaDescription;
                        //    foreach(var descriptionItem in description.MultimediaDescriptions)
                        //        {
                        //            var Descriptions = from order in descriptionItem.TextItems
                        //                               where order.TextItems != null
                        //                               select order;
                        //            string HotelDescription = string.Empty;
                        //            string HotelLocationDescription = string.Empty;
                        //            string OutPut = "";
                        //            string Ratings = "0";
                        //            int BlockType = 0;


                        //            //IEnumerable<GDSBookingDetails.MultimediaDescription> HotelLocationDescriptionList = Descriptions.Where(x => (x.InfoCode == "1" && x.AdditionalDetailCode == "3"));
                        //            //if(HotelLocationDescriptionList!=null)

                        //            //HotelLocationDescription = GetHotelLocation(HotelLocationDescriptionList);

                        //            // int u = 1;
                        //            foreach (var desc in Descriptions.)
                        //            {


                        //                CLayer.ContentFormat objContentFormat = new CLayer.ContentFormat();
                        //                objContentFormat.BlockType = u;
                        //                objContentFormat.InfoCode = desc.InfoCode;
                        //                objContentFormat.AdditionalDetailCode = desc.AdditionalDetailCode;
                        //                List<CLayer.ContentLines> objLinesList = new List<CLayer.ContentLines>();

                        //                foreach (var datas in desc.TextItems.TextItem.Description)
                        //                {
                        //                    if (datas.__Text != null)
                        //                    {
                        //                        if (datas.__Text.ToLower().Contains("contact"))
                        //                        {
                        //                            break;
                        //                        }
                        //                        else
                        //                        {
                        //                            CLayer.ContentLines objLines = new CLayer.ContentLines();
                        //                            objLines.Description = datas.__Text;
                        //                            objLinesList.Add(objLines);
                        //                        }
                        //                    }


                        //                }
                        //                if (objLinesList.Count > 0)
                        //                {
                        //                    objContentFormat.lines = objLinesList;
                        //                    objContentList.Add(objContentFormat);
                        //                    u = u + 1;
                        //                }

                        //            }


                        //            //   return objContentList;




                        //        }


                        //    }
                        //}
                    }
                
                    #endregion

                    #region AreaInfo
                    if (ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.AreaInfo != null)
                    {
                        var description = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.AreaInfo.RefPoints;

                        if (description != null)
                        {
                            if(description.RefPoint.Transportations!=null)
                            {
                                if (description.RefPoint.Transportations.Transportation.MultimediaDescriptions!=null)
                                {
                                    var descriptionItemArea = description.RefPoint.Transportations.Transportation.MultimediaDescriptions.MultimediaDescription;

                                    var Descriptions = from order in descriptionItemArea
                                                       where order.TextItems != null
                                                       select order;
                                    string AreaDescription = string.Empty;
                                    string HotelLocationDescription = string.Empty;
                                    string OutPut = "";
                                    string Ratings = "0";
                                    int BlockType = 0;


                                    //IEnumerable<GDSBookingDetails.MultimediaDescription> HotelLocationDescriptionList = Descriptions.Where(x => (x.InfoCode == "1" && x.AdditionalDetailCode == "3"));
                                    //if(HotelLocationDescriptionList!=null)

                                    //HotelLocationDescription = GetHotelLocation(HotelLocationDescriptionList);


                                    foreach (var desc in Descriptions)
                                    {


                                        CLayer.ContentFormat objContentFormat = new CLayer.ContentFormat();
                                        objContentFormat.BlockType = u;
                                        objContentFormat.InfoCode = desc.InfoCode;
                                        objContentFormat.AdditionalDetailCode = desc.AdditionalDetailCode;
                                        List<CLayer.ContentLines> objAreaLinesList = new List<CLayer.ContentLines>();

                                        foreach (var datas in desc.TextItems.TextItem.Description)
                                        {
                                            if (datas.__Text != null)
                                            {
                                                if (datas.__Text.ToLower().Contains("contact"))
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    CLayer.ContentLines objLines = new CLayer.ContentLines();
                                                    objLines.Description = datas.__Text;
                                                    objAreaLinesList.Add(objLines);
                                                }
                                            }



                                        }
                                        if (objAreaLinesList.Count > 0)
                                        {
                                            objContentFormat.lines = objAreaLinesList;
                                            objAreaContentList.Add(objContentFormat);
                                            u = u + 1;
                                        }

                                    }
                                }





                            }
                        

                        }
                    }
                    #endregion

                    #region AffiliationInfo
                    if (ssMainRating != null)
                    {
                        if (ssMainRating.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.AffiliationInfo != null)
                        {
                            try
                            {
                                var description = ssMainRating.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.AffiliationInfo.Awards;
                                CLayer.ContentFormat objContentFormat = new CLayer.ContentFormat();
                                objContentFormat.BlockType = u;
                                objContentFormat.InfoCode = "";
                                objContentFormat.AdditionalDetailCode = "";
                                List<CLayer.ContentLines> objAffliationLinesList = new List<CLayer.ContentLines>();
                                if(description!=null)
                                {
                                    foreach (var item in description)
                                    {
                                        CLayer.ContentLines objLines = new CLayer.ContentLines();
                                        objLines.Description = item.Provider + " " + Convert.ToString(item.Rating);
                                        objAffliationLinesList.Add(objLines);
                                    }
                                }
                               

                                if (objAffliationLinesList.Count > 0)
                                {
                                    objContentFormat.lines = objAffliationLinesList;
                                    objAffiliationContentList.Add(objContentFormat);
                                    u = u + 1;
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }
                    }

                    if(objAffiliationContentList.Count==0)
                    {
                        if (ssRating != null)
                        {
                            if (ssRating.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.AffiliationInfo != null)
                            {
                                try
                                {
                                    var description = ssRating.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.AffiliationInfo.Awards;
                                    CLayer.ContentFormat objContentFormat = new CLayer.ContentFormat();
                                    objContentFormat.BlockType = u;
                                    objContentFormat.InfoCode = "";
                                    objContentFormat.AdditionalDetailCode = "";
                                    List<CLayer.ContentLines> objAffliationLinesList = new List<CLayer.ContentLines>();
                                    if (description != null)
                                    {
                                        //item.Rating
                                        CLayer.ContentLines objLines = new CLayer.ContentLines();
                                        objLines.Description = description.Award.Provider + " " + Convert.ToString(description.Award.Rating);
                                        objAffliationLinesList.Add(objLines);
                                    }

                                    if (objAffliationLinesList.Count > 0)
                                    {
                                        objContentFormat.lines = objAffliationLinesList;
                                        objAffiliationContentList.Add(objContentFormat);
                                        u = u + 1;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }

                            }
                        }
                    }
                 
                  
                    #endregion

               

                    if (objAreaContentList.Count > 0)
                    {
                        objContentList.AddRange(objAreaContentList);
                    }
                    if (objAffiliationContentList.Count>0)
                    {
                        objContentList.AddRange(objAffiliationContentList);
                    }


                    return objContentList;

                }
                catch (Exception ex)
                {
                    objContentList = null;
                  CLayer.GDSBookingDetailsAdv.Envelope ssAdv = new CLayer.GDSBookingDetailsAdv.Envelope();
                    ssAdv = ser.Deserialize<CLayer.GDSBookingDetailsAdv.Envelope>(xmlDoc.InnerXml.ToString());
                }

                #endregion




            }
            catch (Exception ex)
            {
                objContentList = null;
            }
            return objContentList;
        }

        private static  string GetCancellationPolicy(string pValue)
        {
            string Result = "-Sunbathing       -theater          -exercising";// string.Empty;
            string Feature1 = string.Empty;
            string Feature2 = string.Empty;
            string Feature3 = string.Empty;
            try
            {
                int nCol1 = Result.IndexOf(" -");
                if (nCol1 != -1)
                {
                    string Text1 = Result.Substring(0, nCol1).Trim();
                    Feature1 = Text1;
                    string Text2 = Result.Substring(nCol1).Trim();
                    int nCol2 = Text2.IndexOf(" -");
                    if (nCol2 != -1)
                    {
                        Feature2 = Text2.Substring(0, nCol2).Trim();
                        Feature3 = Text2.Substring(nCol2);
                    }

                }

                Feature1 = Feature1.Replace("-", "").Trim();
                Feature2 = Feature2.Replace("-", "").Trim();
                Feature3 = Feature3.Replace("-", "").Trim();
            }
            catch (Exception ex)
            {
                Result = "";
            }
            return Result;

        }

        public static string GetFeatures(List<CLayer.ContentLines> pContentLines, string InfoCode = "", string AdditionalDetailCode = "")
        {
            List<string> objResultList = new List<string>();

            List<string> keywords = new List<string>();
            string Lines = string.Empty;

            int AmenitiesIndex = pContentLines.FindIndex(A => A.Description.ToLower().Contains("amenities"));
            if(AmenitiesIndex >= 0)
            {
                pContentLines.RemoveRange(0, AmenitiesIndex);
            }
            
            int FacilitiesIndex = pContentLines.FindIndex(A => A.Description.ToLower().Contains("facilities like"));
            if (FacilitiesIndex >= 0)
            {
                pContentLines.RemoveAt(FacilitiesIndex);
            }


             foreach (var line in pContentLines)
            {


                Lines = " " + line.Description;
                string[] items = Lines.Split(new string[] { " -" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string it in items)
                {
                    string it1 = it.Trim().ToLower().Replace("-", "").Trim();
                    if (!HasLettercChars(it1))
                    {
                        if (!HasLettercChars(it1))
                        {
                            it1 = "";
                        }
                    }
                    if(it1!="")
                    {
                        if (HasAlphaNumbericChars(it.ToString().Trim()))
                        {
                            //  keywords.Add(FirstLetterToUpperCase(it.ToLower().ToString()));
                            keywords.Add(StringHelper.ToTitleCase(it.Trim().ToLower().ToString(), TitleCase.First));
                        }
                    }


                }
            }
            if(keywords.Count>40)
            {
                objResultList = keywords.Take(40).Distinct().ToList();
            }
            else
            {
                objResultList = keywords.Distinct().ToList();
            }

            string Result = GetFormattedFeaturesAdvanced(objResultList);  //GetFormattedFeatures(objResultList);

            //string OutPut = "";
            //foreach (var item in objResultList)
            //{
            //    OutPut = OutPut + "<li>" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.ToString().Trim().ToLower()) + "</li>";
            //}
            //string Result = "<div><ul>" + OutPut + "</ul></div>";
            return Result;
        }

        public static List<CLayer.DetailContents> FormattedDescription(string xmlResponse)
        {
            List<CLayer.DetailContents> DetailContents = new List<CLayer.DetailContents>();

            string result = string.Empty;
            try
            {
                List<CLayer.ContentFormat> objHotelDetails = GetHotelDetails(xmlResponse);
                foreach (var item in objHotelDetails)
                {
                    CLayer.DetailContents objDetailContents = new CLayer.DetailContents();
                    CLayer.DetailContents objDetailContentsCancel = new CLayer.DetailContents();
                    item.InfoCode = (item.InfoCode == null) ? "" : item.InfoCode;
                    switch (item.InfoCode)
                    {
                        case "":
                            if (item.AdditionalDetailCode == "17")
                            {
                                // result = GetGeneralDescription(item.lines,item.InfoCode,item.AdditionalDetailCode);
                                result = GetFeatures(item.lines, item.InfoCode, item.AdditionalDetailCode);
                                Features = result;
                                objDetailContents.Section = "Amenities";
                                objDetailContents.Details = ReplaceStringTOEmpty(Features, "features");
                                objDetailContents.Order = 3;
                            }
                            else if (string.IsNullOrEmpty(item.AdditionalDetailCode))
                            {
                                result = GetGeneralDescription(item.lines, item.InfoCode, item.AdditionalDetailCode);
                                AreaInformation = result;
                                if(result !="<br/>")
                                {
                                    objDetailContents.Section = "Area information";
                                    objDetailContents.Details = ReplaceStringTOEmpty(AreaInformation, "area information");
                                    objDetailContents.Order = 2;
                                }
                            }
                            else
                            {
                                result = GetGeneralDescription(item.lines, item.InfoCode, item.AdditionalDetailCode);
                            }
                            break;
                        case "1":

                            if (item.AdditionalDetailCode == "2")
                            {
                                //  result = result + GetGeneralDescription(item.lines, item.InfoCode, item.AdditionalDetailCode);
                                result = GetGeneralDescription(item.lines, item.InfoCode, item.AdditionalDetailCode);
                                objDetailContents.Section = "About";
                                objDetailContents.Details = ReplaceStringTOEmpty(result, "property type");
                                objDetailContents.Order = 1;
                                int nCol1 = result.ToLower().IndexOf("cancellation policy");
                                if (nCol1 != -1)
                                {
                                    HotelDetails = StringHelper.ToTitleCase(result.Substring(0, nCol1).ToLower(), TitleCase.First);
                                    objDetailContents.Section = "About";
                                    objDetailContents.Details = ReplaceStringTOEmpty(HotelDetails, "property type"); ;
                                    objDetailContents.Order = 1;
                                    CancellationPolicy = result.Substring(result.ToLower().IndexOf("cancellation policy"));
                                    objDetailContentsCancel = new CLayer.DetailContents();
                                    objDetailContentsCancel.Section = "Cancellation Policy";
                                    objDetailContentsCancel.Details = ReplaceStringTOEmpty(CancellationPolicy, "cancellation policy");
                                    objDetailContentsCancel.Order = 5;

                                }
                            }
                            else if (item.AdditionalDetailCode == "3")
                            {
                                // result = result + GetAreaDescription(item.lines);
                                result = GetAreaDescription(item.lines);
                                AreaInformation = result;
                                if (result != "")
                                {
                                    objDetailContents.Section = "Area information";
                                    objDetailContents.Details = ReplaceStringTOEmpty(AreaInformation, "area information");
                                    objDetailContents.Order = 2;
                                }
                            }
                            //else if (item.AdditionalDetailCode == "34")
                            //{
                            //    //result = result + GetGeneralDescription(item.lines);
                            //    result = GetGeneralDescription(item.lines,item.InfoCode,item.AdditionalDetailCode);
                            //    RoomSafetyAndSecurity = result;
                            //    objDetailContents.Section = "Room safety and security";
                            //    objDetailContents.Details = RoomSafetyAndSecurity.ToLower().Replace("room safety and security","");
                            //    objDetailContents.Order = 4;

                            //}
                            break;
                        case "2":
                            if (string.IsNullOrEmpty(item.AdditionalDetailCode))
                            {
                                result = GetGeneralDescription(item.lines);
                                int nCol = result.ToLower().IndexOf("reception");
                                if (nCol != -1)
                                {
                                    // HotelDetails = StringHelper.ToTitleCase(result.Substring(0, nCol).ToLower(), TitleCase.First);
                                    Reception = result.Substring(nCol);
                                    objDetailContents.Section = "Reception";
                                    objDetailContents.Details = ReplaceStringTOEmpty(Reception, "reception").Replace("</b><br/>", ""); ;
                                    objDetailContents.Order = 6;
                                }
                            }
                            break;
                        case "5":
                            if (item.AdditionalDetailCode == "2")
                            {
                                //  result = result + GetGeneralDescription(item.lines, item.InfoCode, item.AdditionalDetailCode);
                                result = GetGeneralDescription(item.lines, item.InfoCode, item.AdditionalDetailCode);
                                objDetailContents.Section = "About";
                                objDetailContents.Details = ReplaceStringTOEmpty(result, "property type");
                                objDetailContents.Order = 1;
                            }
                            else if (item.AdditionalDetailCode == "3")
                            {
                                // result = result + GetAreaDescription(item.lines);
                                result = GetAreaDescription(item.lines);
                                AreaInformation = result;
                                if(result!="")
                                {
                                    objDetailContents.Section = "Area information";
                                    objDetailContents.Details = ReplaceStringTOEmpty(AreaInformation, "area information");
                                    objDetailContents.Order = 2;
                                }

                            }
                            else
                            {
                                result = GetGeneralDescription(item.lines, item.InfoCode, item.AdditionalDetailCode);
                                objDetailContents.Section = "About";
                                objDetailContents.Details = ReplaceStringTOEmpty(result, "property type");
                                objDetailContents.Order = 1;
                            }
                            break;
                        case "8":
                            if (item.AdditionalDetailCode == "2")
                            {
                                //   result = result + GetGeneralDescription(item.lines,item.InfoCode,item.AdditionalDetailCode);
                                result = GetGeneralDescription(item.lines, item.InfoCode, item.AdditionalDetailCode);
                                result = result.Replace("<b>", "").Trim();
                                int nCol2 = result.IndexOf("Cancellation policy");
                                if (nCol2 != -1)
                                {
                                    HotelDetails = StringHelper.ToTitleCase(result.Substring(0, nCol2).ToLower(), TitleCase.First);
                                    objDetailContents.Section = "About";
                                    objDetailContents.Details = ReplaceStringTOEmpty(HotelDetails, "property type"); ;
                                    objDetailContents.Order = 1;
                                    CancellationPolicy = result.Substring(result.IndexOf("Cancellation policy"));
                                    objDetailContentsCancel = new CLayer.DetailContents();
                                    objDetailContentsCancel.Section = "Cancellation Policy";
                                    objDetailContentsCancel.Details = ReplaceStringTOEmpty(CancellationPolicy, "cancellation policy");
                                    objDetailContentsCancel.Order = 5;

                                }

                            }
                            else if (item.AdditionalDetailCode == "3")
                            {
                                // result = result + GetAreaDescription(item.lines);
                                result = GetAreaDescription(item.lines);
                                AreaInformation = result;
                            }
                            else
                            {
                                //  result = result + GetGeneralDescription(item.lines);
                                result = GetGeneralDescription(item.lines);
                                result = result.Replace("<b>", "").Trim();
                                int nCol3 = result.IndexOf("Cancellation policy");
                                if (nCol3 != -1)
                                {
                                    HotelDetails = result.Substring(0, nCol3);
                                    objDetailContents.Section = "About";
                                    objDetailContents.Details = HotelDetails;
                                    objDetailContents.Order = 1;
                                    CancellationPolicy = result.Substring(result.IndexOf("Cancellation policy"));
                                    objDetailContentsCancel = new CLayer.DetailContents();
                                    objDetailContentsCancel.Section = "Cancellation Policy";
                                    objDetailContentsCancel.Details = ReplaceStringTOEmpty(CancellationPolicy, "cancellation policy");
                                    objDetailContentsCancel.Order = 5;
                                }

                                // result = "";
                            }
                            break;
                            //default:
                            //    result = result + GetGeneralDescription(item.lines);
                            //    break;

                    }
                    var noofAboutContents = DetailContents.Where(x => x.Section == "About").ToList();
                    var noofCancelContents = DetailContents.Where(x => x.Section == "Cancellation Policy").ToList();
                    var noofAreaContents = DetailContents.Where(x => x.Section == "Area information").ToList();

                    if (objDetailContents.Section == "About")
                    {
                        if (noofAboutContents.Count == 0)
                        {
                            if (objDetailContents.Details != null)
                            {
                                DetailContents.Add(objDetailContents);
                            }

                        }

                    }
                    else if (objDetailContents.Section == "Area information")
                    {
                        if (noofAreaContents.Count == 0)
                        {
                            if (objDetailContents.Details != null)
                            {
                                DetailContents.Add(objDetailContents);
                            }

                        }

                    }
                    else
                    {
                        if (objDetailContents.Details != null)
                        {
                            DetailContents.Add(objDetailContents);
                        }

                    }



                    if (objDetailContentsCancel != null)
                    {
                        if ((objDetailContentsCancel.Section == "Cancellation Policy"))
                        {
                            if (noofCancelContents.Count == 0)
                            {
                                if (objDetailContentsCancel.Details != null)
                                {
                                    DetailContents.Add(objDetailContentsCancel);
                                }
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //    result = HotelDetails + AreaInformation + Features + CancellationPolicy+RoomSafetyAndSecurity ;

            return DetailContents;
        }
        public static string GetFormattedFeatures(List<string> pValue)
        {
            string result = string.Empty;
            try
            {
                var totalrecords = pValue.Count;// 50;
                var noofrecords = 20;
                var AdvancedPages = (totalrecords % noofrecords) > 0 ? 1 : 0;
                int pagenumber = 1;

                var noofpages = Convert.ToInt32((totalrecords / noofrecords)) + AdvancedPages;

                result = "<table><tr>";
                string subresult = "";
                string subinnerresult = "";

                for (int j = 1; j <= noofpages; j++)
                {
                    var start = ((j - 1) * noofrecords);
                    var end = (start + noofrecords);
                    if (end > totalrecords)
                        end = (start + (totalrecords % noofrecords));
                    subresult = subresult + "<td style=\"vertical-align:top\"><ul>";
                    subinnerresult = "";
                    for (var i = start; i < end; i++)
                    {
                        subinnerresult = subinnerresult + "<li>" + pValue[i] + "</li>";
                    }
                    subresult = subresult + subinnerresult + "</ul></td>";
                }
                result = result + subresult + "</tr></table>";
            }
            catch (Exception ex)
            {
                result = string.Empty;
            }

            return result;
        }
        public static string GetFormattedFeaturesAdvanced(List<string> pValue)
        {
            string result = string.Empty;
            try
            {
                var totalrecords = pValue.Count;// 50;
                var noofrecords = 20;
                var AdvancedPages = (totalrecords % noofrecords) > 0 ? 1 : 0;
                int pagenumber = 1;

                var noofpages = Convert.ToInt32((totalrecords / noofrecords)) + AdvancedPages;

                result = "<table><tr>";
                string subresult = "";
                string subinnerresult = "";

            
                   
                    subresult = subresult + "<td style=\"vertical-align:top\"><ul class=\"Amenities-ul\" > ";
                    subinnerresult = "";
                    foreach  (var item in pValue)
                    {
                        subinnerresult = subinnerresult + "<li>" +item.ToString() + "</li>";
                    }
                    subresult = subresult + subinnerresult + "</ul></td>";
                
                result = result + subresult + "</tr></table>";
            }
            catch (Exception ex)
            {
                result = string.Empty;
            }

            return result;
        }


        #endregion

        #region UTILITY FUNCTIONS

        public static  string ReplaceStringTOEmpty(string pValue, string pToReplace)
        {
            string Result;
            try
            {
                Result = Regex.Replace(pValue, pToReplace, "", RegexOptions.IgnoreCase);
                Result = Result.Replace("<b></b><br/>", "");
                Result = Result.Replace("</b><br/>", "");
            }
            catch (Exception ex)
            {
                Result = "";
            }
            Result = FirstLetterToUpperCase(Result.Trim());
            return Result;
        }

        public static  bool HasAlphaNumbericChars(string yourString)
        {
            return yourString.Any(ch => Char.IsLetterOrDigit(ch));
        }
        public static bool HasLettercChars(string yourString)
        {
            return yourString.Any(ch => Char.IsLetter(ch));
        }
        public static bool HasNumericChars(string yourString)
        {
            return yourString.Any(ch => Char.IsNumber(ch));
        }
        public static string FirstLetterToUpperCase(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;

            return char.ToUpper(s[0]) + s.Substring(1);
        }
        #endregion
    }
}
