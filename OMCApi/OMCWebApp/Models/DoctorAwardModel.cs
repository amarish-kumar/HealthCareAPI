using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorAwardModel
    {
        public DoctorAwards DoctorAwardObject { get; set; }
        public int UserId { get; set; }

        public DoctorAwardModel()
        {
            DoctorAwardObject = new DoctorAwards();
        }
    }
}