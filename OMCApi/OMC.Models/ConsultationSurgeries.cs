using System;

namespace OMC.Models
{
    public class ConsultationSurgeries : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int SurgeryId { get; set; }
        public DateTime SurgeryDate { get; set; }
    }
}