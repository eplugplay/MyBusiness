using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data;


namespace MyBusiness.Models
{
    public class ImageModel:List<Image>
    {
        public ImageModel(string folder, bool hidden)
        {
            //string directoryOfImage = HttpContext.Current.Server.MapPath("~/" + folder + "/");
            //XDocument imageData = XDocument.Load(directoryOfImage + @"/ImageMetaData.xml");
            //var images = from image in imageData.Descendants("image") select new Image(image.Element("filename").Value, image.Element("description").Value, image.Element("gender").Value);
            DataTable dt = new DataTable();
            if (folder == "HomeScrollImages")
            {
                dt = uti.GetRandomInfo(folder, hidden);
                var images = from image in dt.AsEnumerable() select new Image(image.Field<string>("folder"), image.Field<string>("filename"), image.Field<string>("description"), image.Field<string>("gender"));
                this.AddRange(images.ToList<Image>());
            }
            else
            {
                dt = uti.GetInfo(folder, hidden);
                var images = from image in dt.AsEnumerable() orderby image.Field<string>("filename") select new Image(image.Field<string>("folder"), image.Field<string>("filename"), image.Field<string>("description"), image.Field<string>("gender"));
                this.AddRange(images.ToList<Image>());
            }
        }
    }
}