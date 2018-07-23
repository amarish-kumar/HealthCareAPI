using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ConsultationPlanModel
    {
        public ConsultationPlans ConsultationPlansObject { get; set; }
        public List<UserDetail> Doctors { get; set; }
        public ConsultationPlanModel()
        {
            ConsultationPlansObject = new ConsultationPlans();
        }
    }
}