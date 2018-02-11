using System.Collections.Generic;

namespace OMC.Models
{
    public class Consultation : BaseEntity
    {
        public string Description { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ConsultationStatusId { get; set; }
    }

    public class ConsultationResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
