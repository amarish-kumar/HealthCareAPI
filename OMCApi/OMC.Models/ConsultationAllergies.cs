using System;
using System.Collections.Generic;

namespace OMC.Models
{
    public class ConsultationAllergies : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int AllergyId { get; set; }
        public DateTime? AllergyStartDate { get; set; }
        public string Treatment { get; set; }
    }

    public class ConsultationAllergyResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationAllergyDisplay> ConsultationAllergyList { get; set; }
    }

    public class ConsultationAllergyDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int AllergyId { get; set; }
        public string AllergyName { get; set; }
        public DateTime? AllergyStartDate { get; set; }
        public string Treatment { get; set; }
    }
}
