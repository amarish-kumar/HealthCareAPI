using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationPlans : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public string Notes { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Timestamp { get; set; }
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public int DoctorId { get; set; }
    }

    public class ConsultationPlanResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationPlansDisplay> ConsultationPlanList { get; set; }
    }

    public class ConsultationPlansDisplay : ConsultationPlans
    {
        public string DoctorName { get; set; }
    }
}
