using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationOccupation : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        [Required(ErrorMessage = "Occupation Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Occupation Id is required.")]
        public int OccupationId { get; set; }
        public string OtherDescription { get; set; }

        #region Serialization
        public bool ShouldSerializeOtherDescription()
        {
            return !string.IsNullOrEmpty(OtherDescription);
        }
        #endregion
    }

    public class ConsultationOccupationResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationOccupationDisplay> ConsultationOccupationList { get; set; }
    }

    public class ConsultationOccupationDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int OccupationId { get; set; }
        public string OtherDescription { get; set; }
        public string OccupationName { get; set; }
    }
}
