using System;

namespace OMC.Models
{
    public class ConsultationFamilyHistory : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int RelationshipId { get; set; }
        public int HealthConditionId { get; set; }
        public int? CurrentAge { get; set; }
        public int? AgeOnConditionStart { get; set; }
        public bool IsAlive { get; set; }
        public string CauseOfDeath { get; set; }
        public int? AgeOnDeath { get; set; }
    }
}
