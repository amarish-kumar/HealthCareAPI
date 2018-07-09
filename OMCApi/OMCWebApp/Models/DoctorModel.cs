using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorModel
    {
        public DoctorProfile DoctorProfileObject { get; set; }
        public List<TimezoneMaster> Timezones { get; set; }
        /// <summary>
        /// Use this for logged in user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// this is used to create the user address record
        /// </summary>
        public UserAddressModel UserAddressModelObject { get; set; }
        public UserAddressResponse UserAddressResponseObject { get; set; }

        public DoctorModel()
        {
            DoctorProfileObject = new DoctorProfile();
        }
    }
}