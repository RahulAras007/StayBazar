using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Drawing;
using System.Threading.Tasks;


namespace StayBazar.Areas.Admin.Controllers
{
    public class StaticPageController : Controller
    {
        //
        // GET: /Admin/SEO/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            return View(LoadAllItems());
        }
        [Common.AdminRolePermission]
        public ActionResult NewPage()
        {
            Models.StaticPageModel data = new Models.StaticPageModel();
            data.PageId = 0;
            return View("_Edit", data);
        }
        [Common.AdminRolePermission]
        public ActionResult EditPage(long Id)
        {
            Models.StaticPageModel data = new Models.StaticPageModel();
            data.PageId = Id;
            CLayer.StaticPage dt = BLayer.StaticPage.GetPage(Id);
            if (dt == null)
            {
                return RedirectToAction("NewPage");
            }

            data.PageTitle = dt.PageTitle;
            data.Description = dt.Description;
            data.FileName = dt.FileName;
            data.ShowInWidget = dt.ShowInWidget;
            data.Location = dt.Location;
            data.City = dt.City;
            data.Image = dt.Image;
            data.RootFolder = dt.RootFolder;
            return View("_Edit", data);
        }
         [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Save(Models.StaticPageModel data)
        {
            try
            {
                CLayer.StaticPage sp = new CLayer.StaticPage();
                string fileName = "";


                if (data.ImageFile != null && data.ImageFile.ContentLength > 0)
                {
                    int Sizevalue = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileUploadSizeInMB"));
                    int MaxContentLength = 1024 * 1024 * Sizevalue; //3 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".jpeg" };
                    if (!AllowedFileExtensions.Contains(data.ImageFile.FileName.Substring(data.ImageFile.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError(string.Empty, "Please use files of type: " + string.Join(", ", AllowedFileExtensions));
                        return View("_Edit", data);
                    }
                    else if (data.ImageFile.ContentLength > MaxContentLength)
                    {
                        ModelState.AddModelError(string.Empty, "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        return View("_Edit", data);
                    }
                    else
                    {
                        fileName = System.IO.Path.GetFileName(data.ImageFile.FileName); //uploaded file from UI

                        if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Temp/")))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Temp/"));

                        }
                        //System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Temp/"+ data.RootFolder +"/"));


                        var path = System.IO.Path.Combine(Server.MapPath("~/Files/Temp/"), fileName);
                        data.ImageFile.SaveAs(path);


                        int maxHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaxImgHeight"));
                        Image tempimage = Image.FromFile(path);

                        Image resized = Common.Utils.ScaleImage(tempimage, maxHeight);

                        if (!System.IO.Directory.Exists(Server.MapPath("~/Files/SEO/")))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/SEO/"));
                        }
                        var path2 = System.IO.Path.Combine(Server.MapPath("~/Files/SEO/"), fileName);

                        //ImageCodecInfo 
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
                        catch { }

                    }//else part
                }//image file check
                if (data.PageId > 0)
                {

                    if (fileName == "")
                    {
                        CLayer.StaticPage ss = BLayer.StaticPage.GetPage(data.PageId);
                        fileName = ss.Image;
                    }
                }//update call

                sp.Image = fileName;
                sp.PageId = data.PageId;
                sp.PageTitle = data.PageTitle;
                sp.RootFolder = FixPath(data.RootFolder);
                sp.ShowInWidget = data.ShowInWidget;
                sp.Location = data.Location;
                sp.Description = data.Description;
                sp.City = data.City;
                sp.FileName = FixFile(data.FileName);
                BLayer.StaticPage.Save(sp);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }

        #region Custom Methods

        private string FixPath(string path)
        {
            string t = path.Trim();
            if (t.Length == 0) return t;
            t = t.Replace("//", "/").Replace("  ", " ").Replace("&","").Replace("?","");
            t = t.Replace(" ", "_");
            if (t.Substring(t.Length - 1) != "/")
                t = t + "/";
            if (t.Substring(0, 1) == "/")
                t = t.Substring(1);
            return t;
        }
        private string FixFile(string path)
        {
            string t = path.Trim();
            if (t.Length == 0) return t;
            t = t.Replace("/", "").Replace("  ", " ").Replace("&", "").Replace("?", "").Replace("\\","");
            t = t.Replace(" ", "_");
            return t;
        }
        private List<CLayer.StaticPage> LoadAllItems()
        {
            return BLayer.StaticPage.GetAll();
        }
        #endregion
       [Common.AdminRolePermission]
        public ActionResult Delete(long? PageId)
        {
            try
            {
                string fileName = "";
                CLayer.StaticPage dt = BLayer.StaticPage.GetPage(PageId.Value);
                Models.StaticPageModel data = new Models.StaticPageModel();
                data.Image = dt.Image;
                data.RootFolder = dt.RootFolder;
                data.FileName = dt.FileName;
                fileName = System.IO.Path.GetFileName(dt.Image);
                if (System.IO.Directory.Exists(Server.MapPath("~/Files/SEO/")))
                {
                    var path1 = System.IO.Path.Combine(Server.MapPath("~/Files/SEO/"), fileName);
                    if (System.IO.File.Exists(path1)) System.IO.File.Delete(path1);

                }              
                var path3 = System.IO.Path.Combine(Server.MapPath("~/"+ dt.RootFolder),dt.FileName);
                if (System.IO.File.Exists(path3))
                {
                    System.IO.File.Delete(path3);
                }
                BLayer.StaticPage.Delete(PageId.Value);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> GenerateFile(string GenPIds)
        {
            List<Models.StaticPageStatus> stat = new List<Models.StaticPageStatus>();
            try {
                List<long> ids = new List<long>();
            string[] s = GenPIds.Split(new string[] {","},StringSplitOptions.RemoveEmptyEntries);
            string t = "";
            long l;
            foreach (string PageId in s)
            {
                t = PageId.Trim();
                if(long.TryParse(t,out l))
                {
                    if (l > 0) ids.Add(l);
                }
            }

            string path = System.Configuration.ConfigurationManager.AppSettings.Get("StaticPageLink");
            string content = "";
            
            try{
                System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient();
                CLayer.StaticPage sp;
                string folderpath;
                string rootpath = Server.MapPath("~");
                System.IO.StreamWriter sw;
                Models.StaticPageStatus spst;
                if(path != null && path != "")
                {
                    foreach(long pid in ids)
                    {
                        spst = new Models.StaticPageStatus() { IsGenerated = true };
                        sp = BLayer.StaticPage.GetPage(pid);
                        if(sp != null){
                            
                            spst.Page = sp.RootFolder + sp.FileName;
                            try
                            {
                                content = await hc.GetStringAsync(path + pid.ToString());
                            }
                            catch { spst.IsGenerated = false; spst.ErrorMessage = "Could not download page"; }
                           try
                           {
                               if (spst.IsGenerated)
                               {
                                   folderpath = System.IO.Path.Combine(new string[] {rootpath, sp.RootFolder });
                                   if(!System.IO.Directory.Exists(folderpath)) System.IO.Directory.CreateDirectory(folderpath);
                                   folderpath = System.IO.Path.Combine(new string[] {folderpath,sp.FileName });
                                   sw = new System.IO.StreamWriter(folderpath, false);
                                   sw.Write(content);
                                   sw.Close();
                               }
                           }
                           catch { spst.IsGenerated = false; spst.ErrorMessage = "Could not save static page at server"; }
                           if (spst.IsGenerated) BLayer.StaticPage.SetUpdateDate(sp.PageId);
                           stat.Add(spst);
                        }

                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Could not retrieve URL from config";
                }

            }catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                ViewBag.ErrorMessage = "An error occured!. Details: " + ex.Message;
            }
           
        }catch(Exception ex)
        {
            Common.LogHandler.HandleError(ex);
        }
            return View("GenerateStatus",stat);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult ClearFile(string ClrPIds)
        {
            string[] s = ClrPIds.Split(',');
            foreach (string PageId in s)
            {
                long id = Convert.ToInt32(PageId);
                CLayer.StaticPage dt = BLayer.StaticPage.GetPage(id);
                var path4 = System.IO.Path.Combine(Server.MapPath("~/" + dt.RootFolder), dt.FileName);
                if (System.IO.File.Exists(path4))
                {
                    System.IO.File.Delete(path4);
                }

            }
            return RedirectToAction("Index");
        }
    }
}