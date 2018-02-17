using OMC.Models;
using System.Collections.Generic;

namespace OMC.DAL.Interface
{
    public interface IConsultationDataAccess
    {
        ConsultationResponse InitiateConsultation(Consultation consultationDetails);
        List<ConsultationDisplay> GetConsultationList(int userId, string userRole);
        ConversationResponse GetConversationList(int consultationId, int userId, string userRole);
    }
}
