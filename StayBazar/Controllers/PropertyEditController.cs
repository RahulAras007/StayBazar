using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;
using StayBazar.Models;
namespace StayBazar.Controllers
{
    public class PropertyEditController : Controller
    {

        #region GetValues

        private Models.PropertyModel GetDetails(long propertyid)
        {
            Models.PropertyModel prprty = new Models.PropertyModel();
            CLayer.Property data = BLayer.Property.Get(propertyid);
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
                    ZipCode = data.ZipCode,
                    Landmarks = GetLandmarks(propertyid),
                    Pictures = GetPictures(propertyid),
                    Features = GetFeatures(propertyid),
                    Email = data.Email,
                    Accommodations = GetAccommodations(propertyid),
                    DistanceFromCity = data.DistanceFromCity,
                    Phone = data.Phone,
                    Mobile = data.Mobile,
                    AgeLimit = data.AgeLimit,
                    CheckInhr = CheckInHr,
                    CheckInmin = CheckInMin,
                    CheckInclock = CheckInclock,
                    CheckOuthr = CheckOutHr,
                    CheckOutmin = CheckOutMin,
                    CheckOutClock = CheckOutclock,
                    PageTitle = data.PageTitle,
                    MetaDescription = data.MetaDescription,
                    PropertyFor=data.PropertyFor

                };
                prprty.Country = data.Country;
                prprty.State = data.State;
                if (data.City != null && data.City != "")
                    prprty.City = data.City;
                if (data.CityId > 0)
                    prprty.City = BLayer.City.Get(data.CityId).Name;
                prprty.CityId = data.CityId;
                prprty.LoadPlaces();
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
            //CLayer.Property data = BLayer.Property.Get(propertyid);
            //if (data != null)
            //{
            //    rates = new Models.PropertyRates()
            //    {
            //        PropertyId = data.PropertyId,
            //        DailyRate = data.DailyRate,
            //        RateForIndividual = data.RateForIndividual,
            //        MonthlyRate = data.MonthlyRate,
            //        WeeklyRate = data.WeeklyRate
            //    };
            //}
            //else
            //    rates.PropertyId = -1;
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

        //
        // GET: /PropertyEdit/
        public ActionResult Index(long? id, string tab)
        {
            try
            {
                Models.PropertyModel prprty = new Models.PropertyModel();
                prprty.CheckInhr = "02";
                prprty.CheckOuthr = "12";
                if (id.HasValue && id.Value > 0)
                {
                    long propertyId = id.Value;
                    long ownerId = BLayer.User.GetUserId(User.Identity.Name);
                    if (!BLayer.Property.IsOwnerProperty(id.Value, ownerId))
                    {
                        return Redirect("PropertyList"); //current user does not own this property
                    }
                    prprty = GetDetails(id.Value);

                }
                else
                {
                    long ownerId = 0; // = BLayer.User.GetUserId(User.Identity.GetUserId);
                    long.TryParse(User.Identity.GetUserId(), out ownerId);
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

        public async Task<ActionResult> SaveDetails(Models.PropertyModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (data.PropertyFor == 0)
                    {
                        data.PropertyFor = 3;                    
                    }
                    data.CheckInTime = data.CheckInhr + ":" + data.CheckInmin + " " + data.CheckInclock;
                    data.CheckOutTime = data.CheckOuthr + ":" + data.CheckOutmin + " " + data.CheckOutClock;
                    //data.Description != "" && 
                    //if (data.Description.Length > 8000) data.Description = data.Description.Substring(0, 8000);
                    CLayer.Property prprty = new CLayer.Property()
                    {
                        PropertyId = data.PropertyId,
                        Title = data.Title,
                        Description = data.Description,
                        Location = data.Location,
                        Status = (CLayer.ObjectStatus.StatusType)data.Status,
                        OwnerId = BLayer.User.GetUserId(User.Identity.Name),
                        Address = data.Address,
                        Country = data.Country,
                        // CountryId=data.CountryId,
                        State = data.State,
                        City = data.City,
                        CityId = data.CityId,
                        ZipCode = data.ZipCode,
                        Email = data.Email,
                        DistanceFromCity = data.DistanceFromCity,
                        Phone = data.Phone,
                        Mobile = data.Mobile,
                        CheckInTime = data.CheckInTime,
                        CheckOutTime = data.CheckOutTime,
                        CheckInhr = data.CheckInhr,
                        CheckInmin = data.CheckInmin,
                        CheckInclock = data.CheckInclock,
                        CheckOuthr = data.CheckOuthr,
                        CheckOutmin = data.CheckOutmin,
                        CheckOutClock = data.CheckOutClock,
                        AgeLimit = data.AgeLimit,
                        PageTitle = data.PageTitle,
                        MetaDescription = data.MetaDescription,
                        PropertyFor=data.PropertyFor

                    };
                    string loc = "";
                    try
                    {

                        string statename = BLayer.State.Get(data.State).Name;
                        string cityname;

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

                        //string apiQuery = BLayer.Settings.GetValue(CLayer.Settings.GOOGLE_API_QUERY);
                        //apiQuery = apiQuery.Replace("[ADDR]", HttpUtility.UrlEncode(qAdddress));
                        //apiQuery = apiQuery.Replace("[API]", BLayer.Settings.GetValue(CLayer.Settings.GOOGLE_API_KEY));

                        //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                        //string result = await client.GetStringAsync(apiQuery);
                        //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        //doc.LoadXml(result);
                        //string status = doc.SelectSingleNode("/GeocodeResponse/status").InnerText;
                        //if (status.ToUpper().Trim() == "OK")
                        //{
                        //    string lat = doc.SelectSingleNode("/GeocodeResponse/result/geometry/location/lat").InnerText;
                        //    string longitude = doc.SelectSingleNode("/GeocodeResponse/result/geometry/location/lng").InnerText;
                        //    prprty.PositionLat = lat;
                        //    prprty.PositionLng = longitude;
                        //}

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
                    BLayer.Property.SetPosition(data.PropertyId, prprty.PositionLat, prprty.PositionLng);
                    ViewBag.Message = "Saved property.";
                    return RedirectToAction("Index", new { id = data.PropertyId });
                }
                else
                {
                    ViewBag.Message = "Error. Please try again";
                    return View("Index", data);
                }
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
                {//.....ImageFile resize save new 
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

                            // var  = prprty.FileName;// DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.photo.FileName);
                            if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Property/" + data.PropertyId.ToString() + "/")))
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Property/" + data.PropertyId.ToString() + "/"));
                            }
                            var path2 = System.IO.Path.Combine(Server.MapPath("~/Files/Property/" + data.PropertyId.ToString() + "/"), prprty.FileName);
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

        [HttpPost]
        public ActionResult AccommodationSave(Models.AccommodationModel data)
        {
            try
            {
                string userid = User.Identity.GetUserId();
                long id = 0;
                long.TryParse(userid, out id);
                if (ModelState.IsValid)
                {
                    CLayer.Accommodation accmdtn = new CLayer.Accommodation()
                    {
                        AccommodationId = data.AccommodationId,
                        PropertyId = data.PropertyId,
                        AccommodationTypeId = data.AccommodationTypeId,
                        StayCategoryId = data.StayCategoryId,
                        AccommodationCount = data.AccommodationCount,
                        Description = data.Description,

                        MaxNoOfPeople = data.MaxNoOfPeople,
                        MaxNoOfChildren = data.MaxNoOfChildren,
                        MinNoOfPeople = data.MinNoOfPeople,
                        BedRooms = data.BedRooms,
                        Area = data.Area,
                        Status = data.Status,
                        TotalAccommodations = data.TotalAccommodations,
                        UpdatedById = id
                    };
                    long accId = BLayer.Accommodation.Save(accmdtn);
                    Models.PropertyModel property = GetDetails(data.PropertyId);
                    property.ActiveTab = "accommodation";
                    property.Accommodations.Accommodation = AccommodationGet(accId);
                    //property.Accommodations.Accommodation.ActiveTab = "acc";
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

        private Models.AccommodationModel AccommodationGet(long accommodationId)
        {
            Models.AccommodationModel data = new Models.AccommodationModel() { AccommodationId = 0 };
            CLayer.Accommodation sub = BLayer.Accommodation.Get(accommodationId);
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

                    MaxNoOfChildren = sub.MaxNoOfChildren,
                    MaxNoOfPeople = sub.MaxNoOfPeople,
                    MinNoOfPeople = sub.MinNoOfPeople,
                    BedRooms = sub.BedRooms,
                    Area = sub.Area,
                    Status = sub.Status,
                    TotalAccommodations = sub.TotalAccommodations,
                    ActiveTab = "acc"
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
        [Common.RoleRequired(Supplier = true)]
        public ActionResult AccommodationDelete(long id)
        {

            long PropertyId = BLayer.Accommodation.GetPropertyId(id);
            try
            {
                if (id < 1)
                {
                    return RedirectToAction("Index");
                }
                long uid = 0;
                long.TryParse(User.Identity.GetUserId(), out uid);
                long PId = BLayer.Accommodation.GetPropertyId(id);
                if (BLayer.Property.IsOwnerProperty(PId, uid))
                {
                    BLayer.Accommodation.Delete(id);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }

            return RedirectToAction("Index", new { id = PropertyId, tab = "accommodation" });
        }
    }
}