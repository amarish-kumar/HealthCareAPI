using OMC.Models;

namespace OMCWebApp.Models
{
    public class ConsultationSubjectiveModel
    {
        public ConsultationSubjectives ConsultationSubjectivesObject { get; set; }
        public ConsultationSubjectiveModel()
        {
            ConsultationSubjectivesObject = new ConsultationSubjectives();
        }
    }
}