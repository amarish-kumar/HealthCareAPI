using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationAssesments :BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public string Notes { get; set; }
        public DateTime? DiagnosisTimestamp { get; set; }
        public string DiagnosisNotes { get; set; }
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Diagnosis doctor Id is required.")]
        public int DiagnosisDoctorId { get; set; }
        public DateTime? DiffDiagnosisTimestamp { get; set; }
        public string DiffDiagnosisNotes { get; set; }
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Diff diagnosis doctor Id is required.")]
        public int DiffDiagnosisDoctorId { get; set; }
    }

    public class ConsultationAssesmentResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationAssesmentsDisplay> ConsultationAssesmentsList { get; set; }
    }

    public class ConsultationAssesmentsDisplay : ConsultationAssesments
    {
        public string DiagnosisDoctorName { get; set; }
        public string DiffDiagnosisDoctorName { get; set; }
    }
}
