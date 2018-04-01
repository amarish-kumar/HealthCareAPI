using System;

namespace OMC.Models
{
    public class ConsultationReports : BaseEntity
    {
        public int ConsultationId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string Description { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPhoneNumber { get; set; }
        public int CountryId { get; set; }
        public DateTime ReportDate { get; set; }
        public string LabName { get; set; }
    }

    public class ConsultationReportResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
