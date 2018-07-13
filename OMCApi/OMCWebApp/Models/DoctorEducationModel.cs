using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorEducationModel
    {
        public DoctorEducation DoctorEducationObject { get; set; }
        public List<Country> CountryList { get; set; }
        public List<StateMaster> StateList { get; set; }
        public int UserId { get; set; }

        public DoctorEducationModel()
        {
            DoctorEducationObject = new DoctorEducation();
        }
    }
}