using System;

namespace OMC.Models
{
    public class ConsultationIllegalDrugs : BaseEntity
    {
        public int ConsultationId { get; set; }
        public bool ConsumeDrug { get; set; }
        public int IllegalDrugID { get; set; }
        public string Frequency { get; set; }
        public int NoPerDay { get; set; }
        public int NoPerWeek { get; set; }

    }

    public class IllegalDrug
    {
        public int ID { get; set; }
        public string IllegalDrugName { get; set;}

    }

    public class ConsultationIllegalDrugsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
