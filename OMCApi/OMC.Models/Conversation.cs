using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class Conversation : BaseEntity
    {
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }        

        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }

        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }

        public bool IsLocked { get; set; }

        #region Serialization

        public bool ShouldSerializePatientId()
        {
            return PatientId.HasValue;
        }

        public bool ShouldSerializeDoctorId()
        {
            return DoctorId.HasValue;
        }
        #endregion  
    }

    public class ConversationResponse
    {
        public ConsultationDisplay ConsultationObject { get; set; }
        public List<ConversationDisplay> Conversations { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ConversationDisplay
    {
        public int ConversationId { get; set; }
        public int ConsultationId { get; set; }
        public string ConversationDescription { get; set; }
        public int? PatientId { get; set; }
        public string PatientName { get; set; }
        public int? DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime? ConversationCreateDate { get; set; }
        public bool IsLocked { get; set; }
    }
}
