using System;

namespace OMC.Models
{
    public class ConsultationCancerTreatments : BaseEntity
    {
        public int ConsultationId { get; set; }
        public string CancerType { get; set; }
        public int CancerStageId { get; set; }
        public DateTime? DignosisDate { get; set; }
        public string TreatmentType { get; set; }
        public bool IsTreatmentOn { get; set; }
        public DateTime? TreatmentCompletionDate { get; set; }
    }
}
