using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace MyBusiness.Models
{
    public class Image
    {
        public Image(string folder, string path, string desc, string gender)
        {
            Folder = folder;
            Path = path;
            Description = desc;
            Gender = gender;
        }
        public string Folder { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
    }

    public class ImageDetails
    {
        public string ImageName { get; set; }
        public string ImageID { get; set; }
    }
}