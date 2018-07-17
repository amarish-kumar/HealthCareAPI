using System;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationPlans : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public string Notes { get; set; }
        public DateTime? Timestamp { get; set; }
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public string DoctorId { get; set; }
    }

    public class ConsultationPlanResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ConsultationPlansDisplay : ConsultationPlans
    {
        public string DoctorName { get; set; }
    }
}
