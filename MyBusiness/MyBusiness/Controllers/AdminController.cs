using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using MyBusiness.Models;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Net;

namespace MyBusiness.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            //if (!Request.IsAuthenticated)
            //{
            //    return RedirectToAction("LogIn", "User");
            //}
            return View();
        }

       
        [HttpPost]
        public PartialViewResult ReloadImgWomen(string Folder)
        {
            return PartialView("_AllImgWomen", new ImageModel(Folder));
        }

        [HttpPost]
        public PartialViewResult ReloadImgMen(string Folder)
        {
            return PartialView("_AllImgMen", new ImageModel(Folder));
        }

        [HttpPost]
        [ValidateInput(false)]
        public string LoadCategories()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyBusinessCnn"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM mybusiness_categories ORDER BY category asc";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<ImageDetails> listCategories = new List<ImageDetails>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ImageDetails objst = new ImageDetails();
                    objst.ImageID = dt.Rows[i]["id"].ToString();
                    objst.ImageName = dt.Rows[i]["category"].ToString();
                    listCategories.Insert(i, objst);
                }
            }
            return serializer.Serialize(listCategories);
        }

        [HttpPost]
        public JsonResult DeleteImage(string path)
        {
            try
            {
                string uri = "ftp://208.118.63.29/site2" + path;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return Json("failed");
                }
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://208.118.63.29/site2" + path));
                reqFTP.Credentials = new NetworkCredential("eplugplay-001", ConfigurationManager.ConnectionStrings["ftp"].ToString());
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();

                // and optionally write the file to disk
                //FileInfo fileInfo = new FileInfo("ftp://208.118.63.29/site2" + path);
                //if (fileInfo.Exists)
                //{
                //    fileInfo.Delete();
                //}

            }
            catch (Exception)
            {
                return Json("Delete failed");
            }

            return Json("File deleted successfully");
        }

        public string GetPath()
        {
            string toReturn = "";
            toReturn = Server.MapPath("~");
            string[] splitPath = toReturn.Split('\\');
            toReturn = splitPath[0] + "//MBR0282-CoastalFlow//newsflash//PDF/";
            return toReturn;
        }
    }
}
