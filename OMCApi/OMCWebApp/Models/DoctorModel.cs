using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorModel
    {
        public DoctorProfile DoctorProfileObject { get; set; }
        public List<TimezoneMaster> Timezones { get; set; }

        public DoctorModel()
        {
            DoctorProfileObject = new DoctorProfile();
        }
    }
}