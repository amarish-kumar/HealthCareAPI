using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationBloodPressureReading : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        [Required(ErrorMessage = "Systolic value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Systolic value (positive number) is required.")]
        public int Systolic { get; set; }
        [Required(ErrorMessage = "Diastolic value is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Diastolic value (positive number) is required.")]
        public int Diastolic { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    public class ConsultationBloodPressureReadingResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationBloodPressureReading> ConsultationBloodPressureReadingList { get; set; }
    }
}
