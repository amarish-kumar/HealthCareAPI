using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationObjectiveNotes : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Objective Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Consultation Objective Id is required.")]
        public int ConsultationObjectiveId { get; set; }
        public string Notes { get; set; }
        public DateTime? Timestamp { get; set; }
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public string DoctorId { get; set; }
    }

    public class ConsultationObjectiveNoteResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationObjectiveNotes> ConsultationObjectiveNoteList { get; set; }
    }

    public class ConsultationObjectiveNotesDisplay : ConsultationObjectiveNotes
    {
        public string DoctorName { get; set; }
    }
}
