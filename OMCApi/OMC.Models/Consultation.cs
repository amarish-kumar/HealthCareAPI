using System;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class Consultation : BaseEntity
    {
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Patient Id is required.")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Profile Id is required.")]
        public int ProfileId { get; set; }

        [Required(ErrorMessage = "Package Id is required.")]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "Doctor Id is required.")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Consultation Status Id is required.")]
        public int ConsultationStatusId { get; set; }
    }

    public class ConsultationResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ConsultationDisplay
    {
        public int ConsultationId { get; set; }
        public string ConsultationDescription { get; set; }
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int ConsultationStatusId { get; set; }
        public string ConsultationStatus { get; set; }
        public DateTime? ConsultationCreateDate { get; set; }
    }
}
