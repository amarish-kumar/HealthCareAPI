using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationFamilyHistory : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        [Required(ErrorMessage = "Relationship Id is required.")]
        public int RelationshipId { get; set; }
        [Required(ErrorMessage = "Health Condition Id is required.")]
        public int HealthConditionId { get; set; }
        public int? CurrentAge { get; set; }
        public int? AgeOnConditionStart { get; set; }
        public bool? IsAlive { get; set; }
        public string CauseOfDeath { get; set; }
        public int? AgeOnDeath { get; set; }
    }

    public class ConsultationFamilyHistoryResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationFamilyHistoryDisplay> ConsultationFamilyHistories { get; set; }
    }

    public class ConsultationFamilyHistoryDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int RelationshipId { get; set; }
        public string Relationship { get; set; }
        public int HealthConditionId { get; set; }
        public string HealthCondition { get; set; }
        public int? CurrentAge { get; set; }
        public int? AgeOnConditionStart { get; set; }
        public bool? IsAlive { get; set; }
        public string CauseOfDeath { get; set; }
        public int? AgeOnDeath { get; set; }
    }
}
