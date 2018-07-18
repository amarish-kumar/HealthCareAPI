using OMC.Models;

namespace OMCWebApp.Models
{
    public class ConsultationObjectiveModel
    {
        public ConsultationObjectives ConsultationObjectivesObject { get; set; }
        public ConsultationObjectiveModel()
        {
            ConsultationObjectivesObject = new ConsultationObjectives();
        }
    }
}