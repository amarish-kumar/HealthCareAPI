using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class PregnancyDetailsModel
    {
        public ConsultationPregnancyDetails ConsultationPregnancyDetailsObject { get; set; }
        public List<MenstrualSymptomsMaster> MenstrualSymptoms { get; set; }
        public PregnancyDetailsModel()
        {
            ConsultationPregnancyDetailsObject = new ConsultationPregnancyDetails();
        }
    }
}