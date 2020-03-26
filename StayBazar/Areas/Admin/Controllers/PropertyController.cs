using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;


namespace StayBazar.Areas.Admin.Controllers
{
    public class PropertyController : Controller
    {

        #region GetValues

        private Models.PropertyModel GetDetails(long propertyid)
        {
            Models.PropertyModel prprty = new Models.PropertyModel();
            CLayer.Property data = BLayer.Property.Get(propertyid);
            string HotelId = BLayer.Property.GetHotelId(propertyid);
            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(propertyid);
            //if (InventoryAPIType == null) { InventoryAPIType = 0; }
            if (HotelId == null) { HotelId = ""; }
            if (data != null)
            {
                if (data.CheckInTime == "" || data.CheckOutTime == "")
                {
                    data.CheckInTime = "02:00 AM";
                    data.CheckOutTime = "12:00 PM";
                }

                string a = data.CheckInTime;
                string[] Intime = a.Split(' ');
                string Inhrmin = Intime[0];
                string[] Inhrmin1 = Inhrmin.Split(':');


                string CheckInclock = Intime[1];
                string CheckInHr = Inhrmin1[0];
                string CheckInMin = Inhrmin1[1];

                string b = data.CheckOutTime;
                string[] Outtime = b.Split(' ');
                string Outhrmin = Outtime[0];
                string[] Outhrmin1 = Outhrmin.Split(':');

                string CheckOutclock = Outtime[1];
                string CheckOutHr = Outhrmin1[0];
                string CheckOutMin = Outhrmin1[1];
                prprty = new Models.PropertyModel()
                           {
                               PropertyId = data.PropertyId,
                               Title = data.Title,
                               Description = data.Description,
                               Location = data.Location,
                               Status = (int)data.Status,
                               OwnerId = data.OwnerId,
                               Address = data.Address,
                               //Country ,State, city beleow
                               ZipCode = data.ZipCode,
                               Email = data.Email,
                               Rating = data.Rating,
                               IsManualReview = data.IsManualReview,
                               Landmarks = GetLandmarks(propertyid),
                               Pictures = GetPictures(propertyid),
                               Features = GetFeatures(propertyid),
                               Accommodations = GetAccommodations(propertyid),
                               DistanceFromCity = data.DistanceFromCity,
                               Phone = data.Phone,
                               Mobile = data.Mobile,
                               PageTitle = data.PageTitle,
                               MetaDescription = data.MetaDescription,
                               AgeLimit = data.AgeLimit,
                               CheckInhr = CheckInHr,
                               CheckInmin = CheckInMin,
                               CheckInclock = CheckInclock,
                               CheckOuthr = CheckOutHr,
                               CheckOutmin = CheckOutMin,
                               CheckOutClock = CheckOutclock,
                               PropertyFor = data.PropertyFor,
                               HotelId = HotelId,
                               InventoryAPITypeId = InventoryAPIType,
                               PropertyInventoryType = data.PropertyInventoryType,
                               GSTRegistrationNo=data.GSTRegistrationNo



                           };
                prprty.Country = data.Country;
                prprty.State = data.State;
                if (data.City != null && data.City != "")
                    prprty.City = data.City;
                if (data.CityId > 0)
                    prprty.City = BLayer.City.Get(data.CityId).Name;
                prprty.CityId = data.CityId;
                prprty.LoadPlaces();//Model function calling

            }
            else
                prprty.PropertyId = -1;

            return prprty;

        }

        private Models.PropertyLandmarkModel GetLandmarks(long propertyId)
        {
            Models.PropertyLandmarkModel landmarks = new Models.PropertyLandmarkModel()
            {
                PropertyId = propertyId,
                Landmark = new Models.LandmarkModel() { LandmarkId = 0, Landmark = "", LandmarkTitleId = 0, PropertyId = propertyId, Range = 0 },
                Landmarks = BLayer.Landmarks.GetOnProperty(propertyId)
            };
            return landmarks;
        }

        private Models.PropertyPicturesModel GetPictures(long propertyId)
        {
            Models.PropertyPicturesModel pictures = new Models.PropertyPicturesModel();
            CLayer.Property data = BLayer.Property.Get(propertyId);
            if (data != null)
            {
                pictures.PropertyId = propertyId;
                pictures.Pictures = BLayer.PropertyFiles.GetAll(propertyId);//, CLayer.PropertyFiles.FileTypes.images
            }
            else
            {
                pictures.PropertyId = -1;
                pictures.Pictures = new List<CLayer.PropertyFiles>();
            }
            return pictures;
        }

        private Models.PropertyFeaturesModel GetFeatures(long propertyid)
        {
            Models.PropertyFeaturesModel features = new Models.PropertyFeaturesModel();
            features.PropertyId = propertyid;
            features.Features = BLayer.PropertyFeature.GetAllWithSelectedForProperty(propertyid);
            return features;
        }

        private Models.PropertyAccommmodationModel GetAccommodations(long propertyid)
        {
            Models.PropertyAccommmodationModel accommodations = new Models.PropertyAccommmodationModel();
            accommodations.PropertyId = propertyid;
            accommodations.Accommodation.PropertyId = propertyid;
            accommodations.Accommodations = BLayer.Accommodation.GetAllForOwnerProperty(propertyid, (int)CLayer.ObjectStatus.RateType.All);
            //accommodations.Accommodation.ActiveTab = "acc";

            return accommodations;
        }

        #region not using now
        private Models.PropertyRatesModel GetRates(long propertyid)
        {
            Models.PropertyRatesModel rates = new Models.PropertyRatesModel();
            return rates;
        }

        private Models.PropertyAccommmodationModel GetRooms(long propertyid)
        {
            Models.PropertyAccommmodationModel rooms = new Models.PropertyAccommmodationModel();
            rooms.Accommodations = BLayer.Accommodation.GetAllForOwnerProperty(propertyid, (int)CLayer.ObjectStatus.RateType.All);

            return rooms;
        }
        #endregion

        #endregion
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Index(long? id, string tab, string disable)
        {
            try
            {
                Models.PropertyModel prprty = new Models.PropertyModel();
                prprty.CheckInhr = "02";
                prprty.CheckOuthr = "12";
                ViewBag.manageproperties = disable;
                if (id.HasValue && id.Value > 0)
                {
                    long propertyId = id.Value;
                    prprty = GetDetails(id.Value);
                }
                else
                {
                    if (id.HasValue && id.Value < 0)
                    {
                        long ownerId = id.Value * -1;
                        prprty.OwnerId = ownerId;
                        prprty.PropertyId = 0;
                        List<CLayer.Address> adr = BLayer.Address.GetOnUser(ownerId);
                        if (adr.Count > 0)
                        {
                            prprty.State = adr[0].State;
                            prprty.CityId = adr[0].CityId;
                            prprty.City = adr[0].City;
                            prprty.Country = adr[0].CountryId;
                            prprty.LoadPlaces();
                        }
                    }
                    else
                        throw new Exception("Admin: Property - wrong parameter provided - Index - missing Id value");
                }
                if (tab == null || tab == "")
                {
                    prprty.ActiveTab = "home";
                }
                else
                {
                    prprty.ActiveTab = tab;
                }


                return View(prprty);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> SaveDetails(Models.PropertyModel data)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                data.CheckInTime = data.CheckInhr + ":" + data.CheckInmin + " " + data.CheckInclock;
                data.CheckOutTime = data.CheckOuthr + ":" + data.CheckOutmin + " " + data.CheckOutClock;
                //if (data.Description.Length > 8000) data.Description = data.Description.Substring(0, 8000);
                CLayer.Property prprty = new CLayer.Property()
                {

                    PropertyId = data.PropertyId,
                    Title = data.Title,
                    Description = data.Description,
                    Location = data.Location,
                    Status = (CLayer.ObjectStatus.StatusType)data.Status,
                    OwnerId = data.OwnerId,
                    Address = data.Address,
                    Country = data.Country,
                    State = data.State,
                    City = data.City,
                    CityId = data.CityId,
                    ZipCode = data.ZipCode,
                    Email = data.Email,
                    Phone = data.Phone,
                    Mobile = data.Mobile,
                    AgeLimit = data.AgeLimit,
                    PageTitle = data.PageTitle,
                    MetaDescription = data.MetaDescription,
                    CheckInTime = data.CheckInTime,
                    CheckOutTime = data.CheckOutTime,
                    CheckInhr = data.CheckInhr,
                    CheckInmin = data.CheckInmin,
                    CheckInclock = data.CheckInclock,
                    CheckOuthr = data.CheckOuthr,
                    CheckOutmin = data.CheckOutmin,
                    CheckOutClock = data.CheckOutClock,
                    PropertyFor = data.PropertyFor,
                    PropertyInventoryType = data.PropertyInventoryType,
                    GSTRegistrationNo=data.GSTRegistrationNo

                };
                string loc = "";
                try
                {
                    prprty.IsManualReview = data.IsManualReview;
                    if (data.IsManualReview == true)
                    {
                        prprty.Rating = data.Rating;
                    }
                    else
                    {
                        CLayer.Property cal = BLayer.Property.PropertyAvgRate(data.PropertyId);
                        int count = cal.CountRating;
                        decimal sum = cal.SumRating;
                        decimal avg = 0;
                        if (count != 0) avg = Convert.ToDecimal(sum / count);
                        prprty.Rating = Convert.ToInt32(avg);
                    }

                    string statename = BLayer.State.Get(data.State).Name;
                    string cityname;
                    //if (data.Country == 0)
                    //{

                    //}
                    if (data.CityId < 1)
                    {
                        cityname = data.City;
                    }
                    else
                    {
                        cityname = BLayer.City.Get(data.CityId).Name;
                        data.City = cityname;
                        prprty.City = cityname;
                    }
                    string Countryname = BLayer.Country.Get(data.Country).Name;
                    loc = cityname + "," + statename + "," + Countryname;
                    string qAdddress = data.Title + "," + data.Address + "," + loc;

                    Common.Utils.Location pos;

                    pos = await Common.Utils.GetLocation(qAdddress);
                    prprty.PositionLat = pos.Lattitude.ToString();
                    prprty.PositionLng = pos.Longitude.ToString();
                }
                catch (Exception ex)
                {
                    Common.LogHandler.LogError(ex);
                }
                if (data.DistanceFromCity <= 0)
                {
                    try
                    {
                        Common.Utils.Location pos;
                        pos = await Common.Utils.GetLocation(loc);
                        double lat, lng;
                        lat = lng = 0;
                        double.TryParse(prprty.PositionLat, out lat);
                        double.TryParse(prprty.PositionLng, out lng);
                        //  double val = Common.Utils.GetDistance(lat,lng, (double) pos.Lattitude,(double) pos.Longitude);
                        double val = Common.Utils.DistanceCalc(lat, lng, (double)pos.Lattitude, (double)pos.Longitude);
                        //val = val * 1000;
                        prprty.DistanceFromCity = (int)Math.Round(val, 1);
                        if (prprty.DistanceFromCity > 100) prprty.DistanceFromCity = 0;
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.LogError(ex);
                    }
                }
                else
                    prprty.DistanceFromCity = data.DistanceFromCity;

                data.PropertyId = BLayer.Property.Save(prprty);

                if (data.HotelId == null) { data.HotelId = ""; }


                BLayer.Property.SetHotelIdAPI(data.PropertyId, data.HotelId.Trim(), data.InventoryAPITypeId);


                BLayer.Property.SetPosition(data.PropertyId, prprty.PositionLat, prprty.PositionLng);
                ViewBag.Message = "Saved property.";
                return RedirectToAction("Index", new { id = data.PropertyId });
                //}
                //else
                //{
                //    ViewBag.Message = "Error. Please try again";
                //    return View("Index", data);
                //}
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult LandmarkSave(Models.PropertyLandmarkModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.Landmarks landmark = new CLayer.Landmarks()
                    {
                        LandmarkId = data.Landmark.LandmarkId,
                        LandmarkTitleId = data.Landmark.LandmarkTitleId,
                        Range = data.Landmark.Range,
                        PropertyId = data.PropertyId
                    };
                    if (data.Landmark.Landmark != null && data.Landmark.Landmark != "")
                        landmark.Landmark = data.Landmark.Landmark;
                    else
                        landmark.Landmark = "";
                    BLayer.Landmarks.Save(landmark);
                }
                return RedirectToAction("Index", new { id = data.PropertyId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public string SaveMarkup(Models.RateCommissionModel data)
        {
            try
            {
                CLayer.RateCommission rc = new CLayer.RateCommission();
                rc.B2BLongTerm = data.B2BLongTerm;
                rc.B2BShortTerm = data.B2BShortTerm;
                rc.B2CLongTerm = data.B2CLongTerm;
                rc.B2CShortTerm = data.B2CShortTerm;
                rc.PropertyId = data.ComPropertyId;
                BLayer.Property.SetCommission(rc);
                BLayer.Property.SaveCancellationDetails(data.ComPropertyId, data.CancellationCharges, data.CancellationPeriod, (CLayer.ObjectStatus.CancellationType)data.CancellationType, data.AppliesToRefund);
                BLayer.Rate.RateRefresh(data.ComPropertyId);
                return "true";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return "false";
        }

        public string SaveTax(long PropertyId, Models.TaxModel data)
        {
            try
            {
                CLayer.Tax rc = new CLayer.Tax();
                //BLayer.Tax.(PropertyId);

                return "true";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return "false";
        }
        public ActionResult LandmarkEdit(long id)
        {
            Models.PropertyLandmarkModel plm = new Models.PropertyLandmarkModel();
            CLayer.Landmarks data = BLayer.Landmarks.Get(id);
            if (data != null)
            {
                plm = new Models.PropertyLandmarkModel()
                {
                    Landmark = new Models.LandmarkModel()
                    {
                        LandmarkId = data.LandmarkId,
                        PropertyId = data.PropertyId,
                        LandmarkTitleId = data.LandmarkTitleId,
                        Landmark = data.Landmark,
                        Range = data.Range
                    },
                    Landmarks = BLayer.Landmarks.GetOnProperty(data.PropertyId),
                    PropertyId = data.PropertyId
                };
            }
            return PartialView("_Landmarks", plm);
        }

        public ActionResult LandmarkDelete(long id)
        {
            // Storing temporary to reload the model
            Models.PropertyLandmarkModel plm = new Models.PropertyLandmarkModel();
            CLayer.Landmarks data = BLayer.Landmarks.Get(id);
            //

            // Delete
            BLayer.Landmarks.Delete(id);
            //

            #region LoadPropertyLandmarkModel
            if (data != null)
            {
                plm = new Models.PropertyLandmarkModel()
                {
                    Landmark = new Models.LandmarkModel()
                    {
                        LandmarkId = 0,
                        PropertyId = data.PropertyId,
                        LandmarkTitleId = 0,
                        Landmark = "",
                        Range = 0
                    },
                    Landmarks = BLayer.Landmarks.GetOnProperty(data.PropertyId),
                    PropertyId = data.PropertyId
                };
            }
            #endregion

            return PartialView("_Landmarks", plm);
        }

        public ActionResult PictureSave(Models.PropertyPicturesModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    #region Save
                    string FileNamePart = data.PropertyId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss");
                    CLayer.PropertyFiles prprty = new CLayer.PropertyFiles()
                    {
                        PropertyId = data.PropertyId,
                        FileId = data.FileId,
                        FileName = FileNamePart + System.IO.Path.GetFileNameWithoutExtension(data.photo.FileName) + ".jpg"
                    };
                    if (data.photo != null && data.photo.ContentLength > 0)
                    {
                        int Sizevalue = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileUploadSizeInMB"));
                        int MaxContentLength = 1024 * 1024 * Sizevalue; //3 MB
                        string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".jpeg" };
                        if (!AllowedFileExtensions.Contains(data.photo.FileName.Substring(data.photo.FileName.LastIndexOf('.'))))
                        {
                            ModelState.AddModelError(string.Empty, "Please use files of type: " + string.Join(", ", AllowedFileExtensions));
                        }
                        else if (data.photo.ContentLength > MaxContentLength)
                        {
                            ModelState.AddModelError(string.Empty, "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        }
                        else
                        {
                            var fileName = FileNamePart + System.IO.Path.GetFileName(data.photo.FileName); //uploaded file from UI

                            if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Temp/")))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Temp/"));
                            }
                            var path = System.IO.Path.Combine(Server.MapPath("~/Files/Temp/"), fileName);
                            data.photo.SaveAs(path);

                            int maxHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaxImgHeight"));
                            Image tempimage = Image.FromFile(path);

                            Image resized = Common.Utils.ScaleImage(tempimage, maxHeight);

                            if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Property/" + data.PropertyId.ToString() + "/")))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Property/" + data.PropertyId.ToString() + "/"));
                            }
                            var path2 = System.IO.Path.Combine(Server.MapPath("~/Files/Property/" + data.PropertyId.ToString() + "/"), prprty.FileName);
                            //  ImageCodecInfo 
                            //System.Drawing.Imaging.ImageCodecInfo jgpEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
                            //System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            //System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters();
                            //System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder,50L);
                            //myEncoderParameters.Param[0] = myEncoderParameter;

                            resized.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg);
                            try
                            {
                                resized.Dispose();
                                tempimage.Dispose();
                                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                            }
                            catch
                            {

                            }
                            BLayer.PropertyFiles.Save(prprty);
                            ModelState.Clear();
                        }
                    }
                    #endregion
                    Models.PropertyModel property = GetDetails(data.PropertyId);
                    property.ActiveTab = "gallery";
                    return View("Index", property);
                }
                return Redirect("~/ErrorPage");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }


        //private System.Drawing.Imaging.ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
        //{
        //    System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders();
        // //   System.Drawing.Imaging.ImageCodecInfo codecw = default(System.Drawing.Imaging.ImageCodecInfo);
        //    foreach (System.Drawing.Imaging.ImageCodecInfo codecw in codecs)
        //    {
        //        if (codecw.FormatID == format.Guid) {
        //            return codecw;
        //        }
        //    }
        //    return null;
        //}

        public ActionResult PictureDelete(long id)
        {
            try
            {
                CLayer.PropertyFiles pf = BLayer.PropertyFiles.Get(id);
                BLayer.PropertyFiles.Delete(id);
                if (System.IO.File.Exists(Server.MapPath("~/Files/Property/" + pf.PropertyId.ToString() + "/" + pf.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Files/Property/" + pf.PropertyId.ToString() + "/" + pf.FileName));
                }
                Models.PropertyModel property = GetDetails(pf.PropertyId);
                property.ActiveTab = "gallery";
                return View("Index", property);
                //return RedirectToAction("Index", new { id = pf.PropertyId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [HttpPost]
        public String FeatureSave(long PropertyId, string features)
        {
            BLayer.PropertyFeature.DeleteFeatureOnProperty(PropertyId);
            string[] ids = features.Split(',');
            for (int i = 0; i < ids.Length - 1; i++)// ids.Length-1 because there will be an empty string in the last row of array
            {
                CLayer.PropertyFeature pf = new CLayer.PropertyFeature()
                {
                    PropertyFeatureId = Convert.ToInt32(ids[i].ToString()),
                    PropertyId = PropertyId
                };
                BLayer.PropertyFeature.SavePropertyFeature(pf);
            }
            return "true";
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public ActionResult AccommodationSave(Models.AccommodationModel data)
        {
            try
            {

                string userid = User.Identity.GetUserId();
                long id = 0;
                long.TryParse(userid, out id);
                //if (ModelState.IsValid)
                //{
                CLayer.Accommodation accmdtn = new CLayer.Accommodation()
                {
                    AccommodationId = data.AccommodationId,
                    PropertyId = data.PropertyId,
                    AccommodationTypeId = data.AccommodationTypeId,
                    StayCategoryId = data.StayCategoryId,
                    AccommodationCount = data.AccommodationCount,
                    Description = data.Description,
                    //Location = data.Location,
                    MaxNoOfPeople = data.MaxNoOfPeople,
                    MaxNoOfChildren = data.MaxNoOfChildren,
                    MinNoOfPeople = data.MinNoOfPeople,
                    BedRooms = data.BedRooms,
                    Area = data.Area,
                    Status = data.Status,
                    TotalAccommodations = data.TotalAccommodations,
                    UpdatedById = id
                };
                if (accmdtn.MaxNoOfPeople < accmdtn.MinNoOfPeople) accmdtn.MaxNoOfPeople = accmdtn.MinNoOfPeople;
                if (accmdtn.MaxNoOfPeople < accmdtn.MaxNoOfChildren) accmdtn.MaxNoOfPeople = accmdtn.MaxNoOfChildren;
                long accId = BLayer.Accommodation.Save(accmdtn);
                if (data.RoomId == null) { data.RoomId = ""; }

                BLayer.Accommodation.SetRoomId(data.AccommodationId, data.RoomId.Trim());


                Models.PropertyModel property = GetDetails(data.PropertyId);
                property.ActiveTab = "accommodation";
                property.Accommodations.Accommodation = AccommodationGet(accId);
                //property.Accommodations.Accommodation.ActiveTab = "acc";
                return View("Index", property);
                //return RedirectToAction("Index", new { id = data.PropertyId });
                //}
                //return Redirect("~/ErrorPage");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        private Models.AccommodationModel AccommodationGet(long accommodationId)
        {
            Models.AccommodationModel data = new Models.AccommodationModel() { AccommodationId = 0 };
            CLayer.Accommodation sub = BLayer.Accommodation.Get(accommodationId);

            string RoomId = BLayer.Accommodation.GetRoomId(accommodationId);
            if (RoomId == null) { RoomId = ""; }

            if (sub != null)
            {
                data = new Models.AccommodationModel()
                {
                    AccommodationId = sub.AccommodationId,
                    AccommodationTypeId = sub.AccommodationTypeId,
                    StayCategoryId = sub.StayCategoryId,
                    AccommodationCount = sub.AccommodationCount,
                    PropertyId = sub.PropertyId,
                    Description = sub.Description,
                    //Location = sub.Location,
                    MaxNoOfChildren = sub.MaxNoOfChildren,
                    MaxNoOfPeople = sub.MaxNoOfPeople,
                    MinNoOfPeople = sub.MinNoOfPeople,
                    BedRooms = sub.BedRooms,
                    Area = sub.Area,
                    TotalAccommodations = sub.TotalAccommodations,
                    Status = sub.Status,
                    ActiveTab = "acc",
                    RoomId = RoomId
                };

                data.AccommodationPictures = new Models.AccommodationPicturesModel()
                {
                    FileId = 0,
                    AccommodationId = data.AccommodationId,
                    PropertyId = data.PropertyId,
                    FileName = "",
                    AccommodationPhoto = null,
                    AccommodationPictures = BLayer.AccommodationFiles.GetOnAccommodation(data.AccommodationId)
                };

                data.AccommodationFeatures = new Models.AccommodationFeaturesModel()
                {
                    AccommodationId = data.AccommodationId,
                    FeatureSet = "",
                    Features = BLayer.AccommodationFeature.GetAllWithSelectedForAccommodation(data.AccommodationId)
                };
            }
            return data;
        }

        public ActionResult AccommodationEdit(int id)
        {
            Models.AccommodationModel data = AccommodationGet(id);
            data.ActiveTab = "acc";
            return PartialView("_Accommodation", data);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult AccommodationNew(int propertyid)
        {
            Models.AccommodationModel data = AccommodationGet(0);// new Models.AccommodationModel() { AccommodationId = 0 };
            data.PropertyId = propertyid;
            data.ActiveTab = "acc";
            return PartialView("_Accommodation", data);
        }
        public ActionResult AccommodationPictureSave(Models.AccommodationPicturesModel data)
        {
            try
            {
                if (data.AccommodationId == 0)
                {
                    Models.PropertyModel property = GetDetails(data.PropertyId);
                    property.ActiveTab = "accommodation";
                    property.Accommodations.Accommodation = AccommodationGet(data.AccommodationId);
                    property.Accommodations.Accommodation.ActiveTab = "pictures";
                    return View("Index", property);
                }
                if (ModelState.IsValid)
                {


                    #region Save
                    string FileNamePart = data.AccommodationId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss");
                    CLayer.AccommodationFiles acmdtn = new CLayer.AccommodationFiles()
                    {
                        AccommodationId = data.AccommodationId,
                        FileId = data.FileId,
                        FileName = FileNamePart + System.IO.Path.GetFileNameWithoutExtension(data.AccommodationPhoto.FileName) + ".jpg"

                    };
                    if (data.AccommodationPhoto != null && data.AccommodationPhoto.ContentLength > 0)
                    {
                        int Sizevalue = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileUploadSizeInMB"));
                        int MaxContentLength = 1024 * 1024 * Sizevalue; //3 MB
                        string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".jpeg" };

                        if (!AllowedFileExtensions.Contains(data.AccommodationPhoto.FileName.Substring(data.AccommodationPhoto.FileName.LastIndexOf('.'))))
                        {
                            ModelState.AddModelError(string.Empty, "Please use files of type: " + string.Join(", ", AllowedFileExtensions));
                        }
                        else if (data.AccommodationPhoto.ContentLength > MaxContentLength)
                        {
                            ModelState.AddModelError(string.Empty, "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        }
                        else
                        {
                            var fileName = FileNamePart + System.IO.Path.GetFileName(data.AccommodationPhoto.FileName); //uploaded file from UI

                            if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Temp/")))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Temp/"));
                            }
                            var path = System.IO.Path.Combine(Server.MapPath("~/Files/Temp/"), fileName);
                            data.AccommodationPhoto.SaveAs(path);


                            int maxHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaxImgHeight"));
                            Image tempimage = Image.FromFile(path);
                            Image resized = Common.Utils.ScaleImage(tempimage, maxHeight);
                            //var fileName = acmdtn.FileName;
                            if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Property/Accommodation/" + data.AccommodationId.ToString() + "/")))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Property/Accommodation/" + data.AccommodationId.ToString() + "/"));
                            }
                            var path2 = System.IO.Path.Combine(Server.MapPath("~/Files/Property/Accommodation/" + data.AccommodationId.ToString() + "/"), acmdtn.FileName);
                            resized.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg);
                            try
                            {
                                resized.Dispose();
                                tempimage.Dispose();
                                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                            }
                            catch
                            {

                            }
                            BLayer.AccommodationFiles.Save(acmdtn);
                            ModelState.Clear();

                        }
                    }
                    #endregion
                    Models.PropertyModel property = GetDetails(data.PropertyId);
                    property.ActiveTab = "accommodation";
                    property.Accommodations.Accommodation = AccommodationGet(data.AccommodationId);
                    property.Accommodations.Accommodation.ActiveTab = "pictures";
                    return View("Index", property);
                    //return RedirectToAction("Index", new { id = data.PropertyId });
                }
                return Redirect("~/ErrorPage");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult AccommodationPictureDelete(long id)
        {
            try
            {
                CLayer.AccommodationFiles pf = BLayer.AccommodationFiles.Get(id);
                BLayer.AccommodationFiles.Delete(id);
                if (System.IO.File.Exists(Server.MapPath("~/Files/Property/Accommodation/" + pf.AccommodationId.ToString() + "/" + pf.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Files/Property/Accommodation/" + pf.AccommodationId.ToString() + "/" + pf.FileName));
                }
                CLayer.Accommodation ac = BLayer.Accommodation.Get(pf.AccommodationId);
                Models.PropertyModel property = GetDetails(ac.PropertyId);
                property.ActiveTab = "accommodation";
                property.Accommodations.Accommodation = AccommodationGet(pf.AccommodationId);
                property.Accommodations.Accommodation.ActiveTab = "pictures";
                return View("Index", property);
                //return RedirectToAction("Index", new { id = ac.PropertyId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [HttpPost]
        public string AccommodationFeatureSave(long AccommodationId, string features)
        {
            try
            {
                BLayer.AccommodationFeature.DeleteFeatureOnAccommodation(AccommodationId);
                string[] ids = features.Split(',');
                for (int i = 0; i < ids.Length - 1; i++)// ids.Length-1 because there will be an empty string in the last row of array
                {
                    CLayer.AccommodationFeature af = new CLayer.AccommodationFeature()
                    {
                        AccommodationFeatureId = Convert.ToInt32(ids[i].ToString()),
                        AccommodationId = AccommodationId
                    };
                    BLayer.AccommodationFeature.SaveAccommodationFeature(af);
                }
                return "true";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
        }

        [HttpPost]
        public ActionResult SaveDiscount(Models.B2BDiscountModel data)
        {
            Models.B2BDiscountModel fresh = new Models.B2BDiscountModel();
            fresh.B2BId = 0;
            fresh.B2BDLongTerm = 0;
            fresh.B2BDShortTerm = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    fresh.B2BPropertyId = data.B2BPropertyId;
                    if (data.B2BId < 1 || data.B2BPropertyId < 1)
                    {
                        return PartialView("_CorporateEdit", fresh);
                    }
                    CLayer.Discount dis = new CLayer.Discount();
                    dis.B2BId = data.B2BId;
                    dis.PropertyId = data.B2BPropertyId;
                    dis.ShortTermRate = data.B2BDShortTerm;
                    dis.LongTermRate = data.B2BDLongTerm;
                    BLayer.Discount.Save(dis);
                    return PartialView("_CorporateEdit", fresh);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("_CorporateEdit", fresh);
        }
        public ActionResult GetDiscounts(long propertyId)
        {
            List<CLayer.Discount> lst = new List<CLayer.Discount>();
            try
            {
                lst = BLayer.Discount.GetAll(propertyId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("_CorporateDiscountList", lst);
        }
        [HttpPost]
        public void RemoveDiscount(long b2bId, long propertyId)
        {
            try
            {
                BLayer.Discount.Delete(b2bId, propertyId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
        }

        [HttpPost]
        public ActionResult GetDiscount(long b2bId, long propertyId)
        {
            Models.B2BDiscountModel fresh = new Models.B2BDiscountModel();
            try
            {
                fresh.B2BId = b2bId;
                fresh.B2BPropertyId = propertyId;
                if (ModelState.IsValid)
                {
                    if (b2bId < 1 || propertyId < 1)
                    {
                        return PartialView("_CorporateEdit", fresh);
                    }
                    CLayer.Discount dis = BLayer.Discount.Get(b2bId, propertyId);
                    if (dis != null)
                    {
                        fresh.B2BId = b2bId;
                        fresh.B2BPropertyId = propertyId;
                        fresh.B2BDLongTerm = dis.LongTermRate;
                        fresh.B2BDShortTerm = dis.ShortTermRate;
                        fresh.B2BName = dis.B2BName;
                    }
                    return PartialView("_CorporateEdit", fresh);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("_CorporateEdit", fresh);

        }

        [HttpPost]
        public string SaveStdDiscount(long propertyId, double shortTerm, double longTerm)
        {
            try
            {
                BLayer.Discount.SaveStdDiscount(new CLayer.Discount() { PropertyId = propertyId, LongTermRate = longTerm, ShortTermRate = shortTerm });
                return "true";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return "false";
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Delete(long id, long sid)
        {
            try
            {
                if (id < 1)
                {
                    return RedirectToAction("Index");
                }
                BLayer.Property.Delete(id);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
            return RedirectToAction("Details", "Supplier", new { id = sid });
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult AccommodationDelete(long id)
        {
            long PId = BLayer.Accommodation.GetPropertyId(id);
            try
            {
                if (id < 1)
                {
                    return RedirectToAction("Index");
                }
                BLayer.Accommodation.Delete(id);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
            return RedirectToAction("Index", new { id = PId, tab = "accommodation" });
        }
        [HttpPost]
        public ActionResult SaveTaxProperty(Models.PropertyModel data)
        {
            try
            {
                string userid = User.Identity.GetUserId();
                long UId = 0;
                long.TryParse(userid, out UId);

                CLayer.Tax taxdt = new CLayer.Tax()
                {
                    TaxTitleId = data.TaxTitleId,
                    PropertyId = data.PropertyId,
                    Rate = data.Rate,
                    UpdatedBy = UId
                };
                taxdt.Status = (int)CLayer.ObjectStatus.StatusType.Active;

                long id = BLayer.Tax.SaveTax(taxdt);

                // update rates after add property tax

                List<CLayer.Accommodation> acc = BLayer.Accommodation.GetAllAccByPropertyid(data.PropertyId);
                foreach (CLayer.Accommodation accid in acc)
                {
                    List<CLayer.Rates> allrts = BLayer.Rate.GetAllRatesByAccId(accid.AccommodationId);
                    if (allrts != null)
                    {
                        foreach (CLayer.Rates ratedt in allrts)
                        {
                            CLayer.Rates dt = new CLayer.Rates();
                            dt.AccommodationId = accid.AccommodationId;
                            dt.RateId = ratedt.RateId;
                            dt.RateFor = ratedt.RateFor;
                            dt.DailyRate = ratedt.DailyRate;
                            dt.WeeklyRate = ratedt.WeeklyRate;
                            dt.MonthlyRate = ratedt.MonthlyRate;
                            dt.LongTermRate = ratedt.LongTermRate;
                            dt.GuestRate = ratedt.GuestRate;
                            dt.StartDate = ratedt.StartDate;
                            dt.EndDate = ratedt.EndDate;
                            dt.UpdatedBy = ratedt.UpdatedBy;

                            BLayer.Rate.Save(dt);
                        }

                    }
                }
                return View("_TaxList", data.PropertyId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage"); //                
            }
        }
        [HttpPost]
        public ActionResult SavePartialPay(Models.PropertyModel data)
        {
            try
            {
                CLayer.Property payment = new CLayer.Property()
                {
                    B2BPartialPaymentsPcte = data.B2BPartialPaymentsPcte,
                    B2CPartialPaymentsPcte = data.B2CPartialPaymentsPcte,
                    PropertyId = data.PropertyId

                };
                long id = BLayer.Property.SavePartialPay(payment);
                return null;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage"); //                
            }
        }

        public ActionResult DeletePropertyTax(long? PropertyId, long Tid)
        {
            try
            {
                long Id = PropertyId.Value;
                BLayer.Tax.DeletePropertyTax(Id, Tid);

                // update rates after add property tax

                List<CLayer.Accommodation> acc = BLayer.Accommodation.GetAllAccByPropertyid(PropertyId.Value);
                foreach (CLayer.Accommodation accid in acc)
                {
                    List<CLayer.Rates> allrts = BLayer.Rate.GetAllRatesByAccId(accid.AccommodationId);
                    if (allrts != null)
                    {
                        foreach (CLayer.Rates ratedt in allrts)
                        {
                            CLayer.Rates dt = new CLayer.Rates();
                            dt.AccommodationId = accid.AccommodationId;
                            dt.RateId = ratedt.RateId;
                            dt.RateFor = ratedt.RateFor;
                            dt.DailyRate = ratedt.DailyRate;
                            dt.WeeklyRate = ratedt.WeeklyRate;
                            dt.MonthlyRate = ratedt.MonthlyRate;
                            dt.LongTermRate = ratedt.LongTermRate;
                            dt.GuestRate = ratedt.GuestRate;
                            dt.StartDate = ratedt.StartDate;
                            dt.EndDate = ratedt.EndDate;
                            dt.UpdatedBy = ratedt.UpdatedBy;

                            BLayer.Rate.Save(dt);
                        }

                    }
                }

                return View("_TaxList", Id);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [HttpPost]
        public ActionResult Searchforofflinebook(string name)
        {
            List<CLayer.Property> result = new List<CLayer.Property>();
            try
            {
                result = BLayer.B2B.Searchpropertylist(name);
                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/OfflineBookingGST/_PropertyList.cshtml", result);
        }

        

        [HttpPost]
        public ActionResult SearchforofflineInventorybook(string name)
        {
            List<CLayer.Property> result = new List<CLayer.Property>();
            try
            {
                result = BLayer.B2B.Searchpropertylist(name);
                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/BookingofflineRequestTransactions/_PropertyList.cshtml", result);
        }

        [HttpPost]
        public ActionResult Searchforofflinebookfromcustom(string name)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            try
            {
                result = BLayer.B2B.Searchcustompropertylist(name);

                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/OfflineBookingGST/_CustomPropertyList.cshtml", result);
        }

        public string ValidAccountCustomProp(long id)
        {
            string result = "1";
            try
            {
                if(!BLayer.Property.HasValidAccountForCustomProperty(id))
                { result = "0";  }
               
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return result;
        }
        public string ValidAccountProperty(long id)
        {
            string result = "1";
            try
            {
                if (!BLayer.Property.HasValidAccountForProperty(id))
                { result = "0"; }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return result;
        }

        public string GetOwner(long id)
        {
            return BLayer.Property.GetSupplierId(id).ToString();
        }

        [HttpPost]
        public ActionResult SearchforSupplierInvoiceBook(string name)
       {
            List<CLayer.Property> result = new List<CLayer.Property>();
            try
            {
                result = BLayer.B2B.Searchpropertylist(name);
                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
           catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/SupplierInvoiceDetails/_PropertyList.cshtml", result);
        }
        [HttpPost]
        public ActionResult SearchforSupplierInvoicefromcustom(string name)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            try
            {
                result = BLayer.B2B.Searchcustompropertylist(name);

                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/SupplierInvoiceDetails/_CustomPropertyList.cshtml", result);
        }



        [HttpPost]
        public ActionResult SearchforofflinebookInventoryfromcustom(string name)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            try
            {
                result = BLayer.B2B.Searchcustompropertylist(name);

                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/BookingofflineRequestTransactions/_CustomPropertyList.cshtml", result);
        }
        [HttpPost]
        public ActionResult Searchforofflinebookaftersup(string name)
        {
            List<CLayer.Property> result = new List<CLayer.Property>();
            try
            {
                result = BLayer.B2B.Searchpropertylistaftersup(name);

                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/OfflineBookingGST/_PropertyList.cshtml", result);
        }
    }
}