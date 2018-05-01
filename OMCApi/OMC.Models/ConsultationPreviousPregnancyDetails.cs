using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationPreviousPregnancyDetails:BaseEntity
    {
        public int ConsultationId { get; set; }
        public int CurrentPregnancyID { get; set; }
        public int? NoofPregnancy { get; set; }
	    public int? ChildNo { get; set; }
	    public string DeliveryYear { get; set; }
	    public string DeliveryType { get; set; }
    }

    public class ConsultationPreviousPregnancyDetailsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationPreviousPregnancyDetailsDisplay> ConsultationPreviousPregnancyDetailsList { get; set; }
    }

    public class ConsultationPreviousPregnancyDetailsDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int CurrentPregnancyID { get; set; }
        public int? NoofPregnancy { get; set; }
        public int? ChildNo { get; set; }
        public string DeliveryYear { get; set; }
        public string DeliveryType { get; set; }
    }
}
