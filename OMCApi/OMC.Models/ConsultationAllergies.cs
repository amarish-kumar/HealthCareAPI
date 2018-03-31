using System;

namespace OMC.Models
{
    public class ConsultationAllergies : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int AllergyId { get; set; }
        public DateTime? AllergyStartDate { get; set; }
        public string Treatment { get; set; }
    }
}
