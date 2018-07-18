using OMC.Models;

namespace OMCWebApp.Models
{
    public class ConsultationPlanModel
    {
        public ConsultationPlans ConsultationPlansObject { get; set; }
        public ConsultationPlanModel()
        {
            ConsultationPlansObject = new ConsultationPlans();
        }
    }
}