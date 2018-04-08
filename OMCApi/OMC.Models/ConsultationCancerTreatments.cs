using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationCancerTreatments : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public string CancerType { get; set; }
        public int? CancerStageId { get; set; }
        public DateTime? DignosisDate { get; set; }
        public string TreatmentType { get; set; }
        public bool IsTreatmentOn { get; set; }
        public DateTime? TreatmentCompletionDate { get; set; }
    }

    public class ConsultationCancerTreatmentResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationCancerTreatmentDisplay> ConsultationCancerTreatmentList { get; set; }
    }

    public class ConsultationCancerTreatmentDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public string CancerType { get; set; }
        public int? CancerStageId { get; set; }
        public string CancerStage { get; set; }
        public DateTime? DignosisDate { get; set; }
        public string TreatmentType { get; set; }
        public bool? IsTreatmentOn { get; set; }
        public DateTime? TreatmentCompletionDate { get; set; }
    }
}
