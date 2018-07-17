using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationSubjectives : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public string Onset { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public string Character { get; set; }
        public string AlleviatingFactors { get; set; }
        public string AggravatingFactors { get; set; }
        public string Radiation { get; set; }
        public string TemporalPattern { get; set; }
        public string Severity { get; set; }
        public string Chronology { get; set; }
        public string AdditionalSymptoms { get; set; }
        public string Allergies { get; set; }
    }

    public class ConsultationSubjectiveResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
