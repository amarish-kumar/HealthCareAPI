using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class CancerTreatmentModel
    {
        public ConsultationCancerTreatments ConsultationCancerTreatmentObject { get; set; }
        public List<CancerStageMaster> CancerStages { get; set; }
        public CancerTreatmentModel()
        {
            ConsultationCancerTreatmentObject = new ConsultationCancerTreatments();
        }
    }
}