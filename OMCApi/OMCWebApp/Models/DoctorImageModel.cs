using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorImageModel
    {
        public DoctorImages DoctorImageObject { get; set; }
        public List<Country> CountryList { get; set; }
        public int UserId { get; set; }

        public DoctorImageModel()
        {
            DoctorImageObject = new DoctorImages();
        }
    }
}