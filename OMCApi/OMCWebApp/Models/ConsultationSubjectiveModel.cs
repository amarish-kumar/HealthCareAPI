using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ConsultationSubjectiveModel
    {
        public ConsultationSubjectives ConsultationSubjectivesObject { get; set; }
        public List<ConsultationSubjectiveNotesDisplay> ConsultationSubjectiveNotes { get; set; }
        public ConsultationSubjectiveModel()
        {
            ConsultationSubjectivesObject = new ConsultationSubjectives();
        }
    }
}