using OMC.Models;

namespace OMCWebApp.Models
{
    public class ConsultationAssesmentModel
    {
        public ConsultationAssesments ConsultationAssesmentsObject { get; set; }
        public ConsultationAssesmentModel()
        {
            ConsultationAssesmentsObject = new ConsultationAssesments();
        }
    }
}