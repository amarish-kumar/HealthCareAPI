using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationReports : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string Description { get; set; }
        public string DoctorName { get; set; }
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "PhoneNumber is of wrong format")]
        public string DoctorPhoneNumber { get; set; }
        [Required(ErrorMessage = "Country Id is required.")]
        public int? CountryId { get; set; }
        [Required(ErrorMessage = "Report Date is required.")]
        public DateTime? ReportDate { get; set; }
        public string LabName { get; set; }
    }

    public class ConsultationReportResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationReportDisplay> ConsultationReports { get; set; }
    }

    public class ConsultationReportDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public string FileName { get; set; }
        public Byte[] FileData { get; set; }
        public string Description { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPhoneNumber { get; set; }
        public DateTime? ReportDate { get; set; }
        public string LabName { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
    }
}
