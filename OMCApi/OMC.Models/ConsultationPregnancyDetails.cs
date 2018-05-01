using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationPregnancyDetails : BaseEntity
    {
        public int ConsultationId { get; set; }
        public bool CurrentlyPregnant { get; set; }
	    public int? CurrentPregnancyMonths { get; set; }
        public DateTime? CurrentPregnancyEDD { get; set; }
        public bool PregnantBefore { get; set; }
        public bool MenstrualCycles { get; set; }
        public string NoMCReason { get; set; }
        public DateTime? LastMCCycle { get; set; }
        public bool MCRegInterval { get; set; }
        public int? LenMCCycle { get; set; }
        public int? MCStartAge { get; set; }
        public string MCFlow { get; set; }
        public string MCProductType { get; set; }
        public int? MCProductPerDay { get; set; }
        public bool MCPain { get; set; }
        public int? MCPainSeverity { get; set; }
        public string MCSymptomID { get; set; }
        public string MCSymptomDesc { get; set; }
        public int[] MCSymptomIDArray { get; set; }

        #region Serialization
        public bool ShouldSerializeLastMCCycle()
        {
            return LastMCCycle.HasValue;
        }

        #endregion
    }

    public class ConsultationPregnancyDetailsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationPregnancyDetailsDisplay> ConsultationPregnancyDetailsList { get; set; }
    }

    public class ConsultationPregnancyDetailsDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public bool CurrentlyPregnant { get; set; }
        public int? CurrentPregnancyMonths { get; set; }
        public DateTime? CurrentPregnancyEDD { get; set; }
        public bool PregnantBefore { get; set; }
        public bool MenstrualCycles { get; set; }
        public string NoMCReason { get; set; }
        public DateTime? LastMCCycle { get; set; }
        public bool MCRegInterval { get; set; }
        public int? LenMCCycle { get; set; }
        public int? MCStartAge { get; set; }
        public string MCFlow { get; set; }
        public string MCProductType { get; set; }
        public int? MCProductPerDay { get; set; }
        public bool MCPain { get; set; }
        public int? MCPainSeverity { get; set; }
        public string MCSymptomID { get; set; }
        public string MCSymptomDesc { get; set; }
        public int[] MCSymptomIDArray { get; set; }
    }
}
