using OMC.Models;
using System;
using System.Collections.Generic;

namespace OMC.BL.Interface
{
    public interface IConsultationBL : IDisposable
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
        ConsultationAllergyResponse InsertUpdateConsultationAllergy(ConsultationAllergies consultationAllergy);
        ConsultationAllergyResponse GetConsultationAllergyList(int consultationId, int? consultationAllergyId);
        ConsultationFamilyHistoryResponse InsertUpdateConsultationFamilyHistory(ConsultationFamilyHistory consultationFamilyHistory);
        ConsultationFamilyHistoryResponse GetConsultationFamilyHistoryList(int consultationId, int? consultationFamilyHistoryId
            , int? relationshipId, bool? excludeSelf);
        ConsultationOccupationResponse InsertUpdateConsultationOccupation(ConsultationOccupation consultationOccupation);
        ConsultationOccupationResponse GetConsultationOccupationList(int consultationId, int? consultationOccupationId);
        ConsultationBloodPressureReadingResponse InsertUpdateConsultationBloodPressureReading(ConsultationBloodPressureReading consultationBloodPressureReading);
        ConsultationBloodPressureReadingResponse GetConsultationBloodPressureReadingList(int consultationId, int? consultationBloodPressureReadingId);
        ConsultationMedicationResponse InsertUpdateConsultationMedication(ConsultationMedications consultationMedication);
        ConsultationMedicationResponse GetConsultationMedicationList(int consultationId, int? consultationMedicationId);
    }
}
