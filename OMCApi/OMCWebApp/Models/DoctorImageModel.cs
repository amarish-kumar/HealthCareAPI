using OMC.Models;
using System.Collections.Generic;
using System.Web;

namespace OMCWebApp.Models
{
    public class DoctorImageModel
    {
        public DoctorImages DoctorImageObject { get; set; }
        public int UserId { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public DoctorImageModel()
        {
            DoctorImageObject = new DoctorImages();
        }
    }
}