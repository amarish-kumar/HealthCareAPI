using OMC.Models;

namespace OMCWebApp.Models
{
    public class ConsultationViewModel
    {
        public ConversationResponse ConversationResponseObject { get; set; }
        public int UserId { get; set; }
        public int ConsultationId { get; set; }
        public string UserRole { get; set; }
    }
}