using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationObjectives: BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public string Vitals { get; set; }
        public string LabResults { get; set; }
        public string RadioGraphicResults { get; set; }
    }

    public class ConsultationObjectiveResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationObjectives> ConsultationObjectiveList { get; set; }
    }
}
