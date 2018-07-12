using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorFellowshipModel
    {
        public DoctorFellowship DoctorFellowshipObject { get; set; }
        public List<Country> CountryList { get; set; }
        public List<StateMaster> StateList { get; set; }
        public int UserId { get; set; }

        public DoctorFellowshipModel()
        {
            DoctorFellowshipObject = new DoctorFellowship();
        }
    }
}