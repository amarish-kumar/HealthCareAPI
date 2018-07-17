using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationSubjectiveNotes : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Subjective Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Consultation Subjective Id is required.")]
        public int ConsultationSubjectiveId { get; set; }
        public string Notes { get; set; }
        public DateTime? Timestamp { get; set; }
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public int DoctorId { get; set; }
    }

    public class ConsultationSubjectiveNoteResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationSubjectiveNotesDisplay> ConsultationSubjectiveNoteList { get; set; }
    }

    public class ConsultationSubjectiveNotesDisplay : ConsultationSubjectiveNotes
    {
        public string DoctorName { get; set; }
    }
}
