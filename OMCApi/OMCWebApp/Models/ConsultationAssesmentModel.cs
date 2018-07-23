using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ConsultationAssesmentModel
    {
        public ConsultationAssesments ConsultationAssesmentsObject { get; set; }
        public List<UserDetail> Doctors { get; set; }
        public ConsultationAssesmentModel()
        {
            ConsultationAssesmentsObject = new ConsultationAssesments();
        }
    }
}