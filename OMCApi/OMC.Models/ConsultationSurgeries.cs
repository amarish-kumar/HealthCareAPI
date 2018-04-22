using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationSurgeries : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        [Required(ErrorMessage = "Surgery Id is required.")]
        public int SurgeryId { get; set; }
        [Required(ErrorMessage = "Surgery Date Id is required.")]
        public DateTime? SurgeryDate { get; set; }
        public string OtherDescription { get; set; }

        #region Serialization
        public bool ShouldSerializeOtherDescription()
        {
            return !string.IsNullOrEmpty(OtherDescription);
        }
        #endregion
    }

    public class ConsultationSurgeryResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationSurgeryDisplay> ConsultationSurgeriesList { get; set; }
    }

    public class ConsultationSurgeryDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int SurgeryId { get; set; }
        public string SurgeryName { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public string OtherDescription { get; set; }
    }
}