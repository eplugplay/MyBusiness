using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace MyBusiness.Models
{
    public class Image
    {
        public Image(string path, string desc, string gender)
        {
            Path = path;
            Description = desc;
            Gender = gender;
        }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
    }
}