using System;
using System.Collections.Generic;

namespace OMC.Models
{
    public class Conversation
    {
        public string Description { get; set; }        
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int ConsultationId { get; set; }
        public bool IsLocked { get; set; }
    }

    public class ConversationResponse
    {
        public ConsultationDisplay ConsultationObject { get; set; }
        public List<ConversationDisplay> Conversations { get; set; }
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
