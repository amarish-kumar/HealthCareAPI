using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationIllegalDrugDetails :BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public bool ConsumeDrugs { get; set; }
        public int? IllegalDrugsID { get; set; }
        public string IllegalDrugDesc { get; set; }
        public string Frequency { get; set; }
        public int? PerFrequency { get; set; }
    }

    public class ConsultationIllegalDrugDetailsDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public bool ConsumeDrugs { get; set; }
        public int? IllegalDrugsID { get; set; }
        public string IllegalDrugDesc { get; set; }
        public string Frequency { get; set; }
        public int? PerFrequency { get; set; }
    }

    public class ConsultationIllegalDrugDetailsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationIllegalDrugDetailsDisplay> ConsultationIllegalDrugDetailsDisplayList { get; set; }
    }
}
