using OMC.Models;
using System.Collections.Generic;

namespace OMC.DAL.Interface
{
    public interface IConsultationDataAccess
    {
        ConsultationResponse InitiateConsultation(Consultation consultationDetails);
        PatientEnquiryResponse UnregisteredPatientEnquiry(PatientEnquiry enquiry);
        List<ConsultationDisplay> GetConsultationList(int userId, string userRole);
        ConversationResponse GetConversationList(int consultationId, int userId, string userRole);
        ConversationResponse RecordConversation(Conversation conversationDetails);
        ConsultationReportResponse InsertUpdateConsultationReport(ConsultationReports consultationReport);
        ConsultationReportResponse GetConsultationReportList(int consultationId, int? consultationReportId);
        ConsultationSurgeryResponse InsertUpdateConsultationSurgery(ConsultationSurgeries consultationSurgery);
        ConsultationSurgeryResponse GetConsultationSurgeryList(int consultationId, int? consultationSurgeryId);
        ConsultationCancerTreatmentResponse InsertUpdateConsultationCancerTreatment(ConsultationCancerTreatments consultationCancerTreatment);
        ConsultationCancerTreatmentResponse GetConsultationCancerTreatmentList(int consultationId, int? consultationCancerTreatmentId);
        ConsultationIllegalDrugDetailsResponse InsertUpdateConsultationIllegalDrugDetail(ConsultationIllegalDrugDetails consultationIllegalDrugDetails);
        ConsultationIllegalDrugDetailsResponse GetConsultationIllegalDrugDetailList(int consultationId, int? consultationIllegalDrugDetailsId);
        ConsultationSDDHabitsResponse InsertUpdateConsultationSDDHabits(ConsultationSDDHabits consultationSDDHabits);
        ConsultationSDDHabitsResponse GetConsultationSDDHabitsList(int consultationId, int? consultationSDDHabitsId);
    }
}
