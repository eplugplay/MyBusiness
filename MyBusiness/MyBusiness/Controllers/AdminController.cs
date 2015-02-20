﻿using System;
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
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "User");
            }
            return View();
        }

       
        [HttpPost]
        public PartialViewResult ReloadImgWomen(string Folder)
        {
            ViewBag.isAdmin = true;
            return PartialView("_AllImgWomen", new ImageModel(Folder, false, "true"));
        }

        [HttpPost]
        public PartialViewResult ReloadImgMen(string Folder)
        {
            ViewBag.isAdmin = true;
            return PartialView("_AllImgMen", new ImageModel(Folder, false, "true"));
        }

        [HttpPost]
        [ValidateInput(false)]
        public string LoadCategories()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
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
            List<ImageCategory> listCategories = new List<ImageCategory>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ImageCategory objst = new ImageCategory();
                    objst.ID = dt.Rows[i]["id"].ToString();
                    objst.Name = dt.Rows[i]["category"].ToString();
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
                string[] split = path.Split('/');
                using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM mybusiness_images WHERE filename=@filename";
                        cmd.Parameters.AddWithValue("filename", split[2]);
                        cmd.ExecuteNonQuery();
                    }
                }

                // delete image 
                //FTP.DeleteImage(path, split[2]);
                FileInfo fileInfo = new FileInfo(Server.MapPath(path));
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

            }
            catch (Exception e)
            {
                return Json(e.Source);
            }

            return Json("Successful");
        }

        [HttpPost]
        public JsonResult UploadImage(string description, string gender, string folder, bool hidden)
        {
            string fileName = "";
            int length = 0;
            string path = "";
            int Hidden = hidden ? 1 : 0;
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        fileName = fileContent.FileName;
                        string[] split = fileName.Split('\\');
                        fileName = split[split.Length - 1];
                        length = fileContent.ContentLength;
                        var stream = fileContent.InputStream;
                        path = Server.MapPath("/" + folder + "//" + fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
                
                using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO mybusiness_images (filename, description, gender, folder, length, hidden) VALUES (@filename, @description, @gender, @folder, @length, @hidden)";
                        cmd.Parameters.AddWithValue("filename", fileName);
                        cmd.Parameters.AddWithValue("description", description);
                        cmd.Parameters.AddWithValue("gender", gender);
                        cmd.Parameters.AddWithValue("folder", folder);
                        cmd.Parameters.AddWithValue("length", length);
                        cmd.Parameters.AddWithValue("hidden", Hidden);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("Successful");
        }

        [HttpPost]
        public JsonResult ValidateImage(string path)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Server.MapPath(path));
                if (fileInfo.Exists)
                {
                    return Json("Exist");
                }
            }
            catch (Exception)
            {

            }

            return Json("None");
        }

        [HttpPost]
        public JsonResult ValidateImageDb(string fileName)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT count(*) as count FROM mybusiness_images WHERE filename=@filename";
                        cmd.Parameters.AddWithValue("filename", fileName);
                        int cnt = Convert.ToInt32(cmd.ExecuteScalar());
                        if (cnt != 0)
                        {
                            return Json("Exist");
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return Json("None");
        }

        [HttpPost]
        public JsonResult UpdateImgInfo(string path, string description, string gender, string folder, bool hidden, bool imgExist)
        {
            string[] split = path.Split('/');
            int Hidden = hidden ? 1 : 0;
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE mybusiness_images SET description=@description, gender=@gender, folder=@folder, hidden=@hidden where filename=@filename";
                        cmd.Parameters.AddWithValue("filename", split[2]);
                        cmd.Parameters.AddWithValue("description", description);
                        cmd.Parameters.AddWithValue("gender", gender);
                        cmd.Parameters.AddWithValue("folder", folder);
                        cmd.Parameters.AddWithValue("hidden", Hidden);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (imgExist == false)
                {
                    // delete image from old folder
                    FileInfo fileInfo = new FileInfo(Server.MapPath(path));
                    if (fileInfo.Exists)
                    {
                        // copy image to new folder
                        System.IO.File.Copy(Server.MapPath(path), Server.MapPath("\\" + folder + "\\" + split[2]));
                        fileInfo.Delete();
                    }
                }
            }
            catch
            {
                return Json("Failed to update");
            }
            return Json("Successful");
        }

        [HttpPost]
        [ValidateInput(false)]
        public string GetImgInfo(string path)
        {
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<ImageDetails> listInfo = new List<ImageDetails>();
            string[] split = path.Split('/');
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT filename, description, gender, folder, hidden FROM mybusiness_images where filename=@filename";
                        cmd.Parameters.AddWithValue("filename", split[2]);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ImageDetails objst = new ImageDetails();
                        objst.ImageFileName = dt.Rows[i]["filename"].ToString();
                        objst.ImageDescription = dt.Rows[i]["description"].ToString();
                        objst.ImageGender = dt.Rows[i]["gender"].ToString();
                        objst.ImageFolder = dt.Rows[i]["folder"].ToString();
                        objst.ImageHidden = Convert.ToInt32(dt.Rows[i]["hidden"]);
                        listInfo.Insert(i, objst);
                    }
                }
            }
            catch
            {
                return serializer.Serialize("Failed.");
            }
            return serializer.Serialize(listInfo);
        }

        /*Pages*/
        #region Pages
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdatePage(string ckeditor, string pagename)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE mybusiness_page SET pagedata=@pagedata WHERE pagename=@pagename";
                        cmd.Parameters.AddWithValue("pagename", pagename);
                        cmd.Parameters.AddWithValue("pagedata", ckeditor);
                        cmd.ExecuteNonQuery();
                    }
                };

            }
            catch
            {
                return Json("Failed");
            }
            return Json("Successful");
        }

        [HttpPost]
        [ValidateInput(false)]
        public string LoadPages(string pagename)
        {
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Page> lstPages = new List<Page>();
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"Select pagedata FROM mybusiness_page WHERE pagename=@pagename";
                        cmd.Parameters.AddWithValue("pagename", pagename);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i ++)
                    {
                        Page obj = new Page();
                        obj.PageData = dt.Rows[i]["pagedata"].ToString();
                        lstPages.Insert(i, obj);
                    }
                }
            }
            catch
            {
              
            }
            return serializer.Serialize(lstPages);
        }
        #endregion

        #region Tabs
        [HttpPost]
        [ValidateInput(false)]
        public string LoadTabs()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM mybusiness_tabs ORDER BY id asc";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Tabs> listCategories = new List<Tabs>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Tabs objst = new Tabs();
                    objst.id = dt.Rows[i]["id"].ToString();
                    listCategories.Insert(i, objst);
                }
            }
            return serializer.Serialize(listCategories);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateTabs(string id, int active)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection(ConnectionStrings.MySqlConnectionString()))
                {
                    cnn.Open();
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE mybusiness_tabs SET active=@active WHERE id=@id";
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("active", active);
                        cmd.ExecuteNonQuery();
                    }
                };

            }
            catch
            {
                return Json("Failed");
            }
            return Json("Successful");
        }
        #endregion
    }
}
