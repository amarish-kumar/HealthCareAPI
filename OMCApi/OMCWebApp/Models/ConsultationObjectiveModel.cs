using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ConsultationObjectiveModel
    {
        public ConsultationObjectives ConsultationObjectivesObject { get; set; }
        public List<ConsultationObjectiveNotesDisplay> ConsultationObjectiveNotes { get; set; }

        public ConsultationObjectiveModel()
        {
            ConsultationObjectivesObject = new ConsultationObjectives();
        }
    }
}