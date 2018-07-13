using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorResidencyModel
    {
        public DoctorResidency DoctorResidencyObject { get; set; }
        public List<Country> CountryList { get; set; }
        public List<StateMaster> StateList { get; set; }
        public int UserId { get; set; }

        public DoctorResidencyModel()
        {
            DoctorResidencyObject = new DoctorResidency();
        }
    }
}