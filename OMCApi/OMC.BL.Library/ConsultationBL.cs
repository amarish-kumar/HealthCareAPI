using OMC.BL.Interface;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;

namespace OMC.BL.Library
{
    public class ConsultationBL : IConsultationBL
    {
        #region Declarations
        IConsultationDataAccess _consultationDA;
        #endregion

        #region Constructors

        public ConsultationBL(IConsultationDataAccess ConsultationDA)
        {
            this._consultationDA = ConsultationDA;
        }

        #endregion

        #region Methods
        public List<ConsultationDisplay> GetConsultationList(int userId, string userRole)
        {
            try
            {
                return this._consultationDA.GetConsultationList(userId, userRole);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConversationResponse GetConversationList(int consultationId, int userId, string userRole)
        {
            try
            {
                return this._consultationDA.GetConversationList(consultationId, userId, userRole);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationResponse InitiateConsultation(Consultation consultationDetails)
        {
            try
            {
                return this._consultationDA.InitiateConsultation(consultationDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }


        public PatientEnquiryResponse UnregisteredPatientEnquiry(PatientEnquiry enquiry)
        {
            try
            {
                return this._consultationDA.UnregisteredPatientEnquiry(enquiry);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConversationResponse RecordConversation(Conversation conversationDetails)
        {
            try
            {
                return this._consultationDA.RecordConversation(conversationDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationReportResponse InsertUpdateConsultationReport(ConsultationReports consultationReport)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationReport(consultationReport);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationReportResponse GetConsultationReportList(int consultationId, int? consultationReportId)
        {
            try
            {
                return this._consultationDA.GetConsultationReportList(consultationId, consultationReportId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationSurgeryResponse InsertUpdateConsultationSurgery(ConsultationSurgeries consultationSurgery)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationSurgery(consultationSurgery);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationSurgeryResponse GetConsultationSurgeryList(int consultationId, int? consultationSurgeryId)
        {
            try
            {
                return this._consultationDA.GetConsultationSurgeryList(consultationId, consultationSurgeryId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationCancerTreatmentResponse InsertUpdateConsultationCancerTreatment(ConsultationCancerTreatments consultationCancerTreatment)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationCancerTreatment(consultationCancerTreatment);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationCancerTreatmentResponse GetConsultationCancerTreatmentList(int consultationId, int? consultationCancerTreatmentId)
        {
            try
            {
                return this._consultationDA.GetConsultationCancerTreatmentList(consultationId, consultationCancerTreatmentId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationIllegalDrugDetailsResponse InsertUpdateConsultationIllegalDrugDetail(ConsultationIllegalDrugDetails consultationIllegalDrugDetails)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationIllegalDrugDetail(consultationIllegalDrugDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationIllegalDrugDetailsResponse GetConsultationIllegalDrugDetailList(int consultationId, int? consultationIllegalDrugDetailsId)
        {
            try
            {
                return this._consultationDA.GetConsultationIllegalDrugDetailList(consultationId, consultationIllegalDrugDetailsId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationPregnancyDetailsResponse InsertUpdateConsultationPregnancyDetail(ConsultationPregnancyDetails consultationPregnancyDetails)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationPregnancyDetail(consultationPregnancyDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }


        public ConsultationPregnancyDetailsResponse GetConsultationPregnancyDetailsList(int consultationId, int? consultationPregnancyDetailsId)
        {
            try
            {
                return this._consultationDA.GetConsultationPregnancyDetailsList(consultationId, consultationPregnancyDetailsId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationPreviousPregnancyDetailsResponse InsertUpdateConsultationPreviousPregnancyDetail(ConsultationPreviousPregnancyDetails consultationPreviousPregnancyDetails)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationPreviousPregnancyDetail(consultationPreviousPregnancyDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationPreviousPregnancyDetailsResponse GetConsultationPreviousPregnancyDetailsList(int consultationId, int? consultationPreviousPregnancyDetailsId, int? CurrentPregnancyID)
        {
            try
            {
                return this._consultationDA.GetConsultationPreviousPregnancyDetailsList(consultationId, consultationPreviousPregnancyDetailsId, CurrentPregnancyID);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationSDDHabitsResponse InsertUpdateConsultationSDDHabits(ConsultationSDDHabits consultationSDDHabits)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationSDDHabits(consultationSDDHabits);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationSDDHabitsResponse GetConsultationSDDHabitsList(int consultationId, int? consultationSDDHabitsId)
        {
            try
            {
                return this._consultationDA.GetConsultationSDDHabitsList(consultationId, consultationSDDHabitsId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationAllergyResponse InsertUpdateConsultationAllergy(ConsultationAllergies consultationAllergy)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationAllergy(consultationAllergy);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationAllergyResponse GetConsultationAllergyList(int consultationId, int? consultationAllergyId)
        {
            try
            {
                return this._consultationDA.GetConsultationAllergyList(consultationId, consultationAllergyId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationFamilyHistoryResponse InsertUpdateConsultationFamilyHistory(ConsultationFamilyHistory consultationFamilyHistory)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationFamilyHistory(consultationFamilyHistory);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationFamilyHistoryResponse GetConsultationFamilyHistoryList(int consultationId, int? consultationFamilyHistoryId
            , int? relationshipId, bool? excludeSelf)
        {
            try
            {
                return this._consultationDA.GetConsultationFamilyHistoryList(consultationId, consultationFamilyHistoryId
                    , relationshipId, excludeSelf);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationOccupationResponse InsertUpdateConsultationOccupation(ConsultationOccupation consultationOccupation)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationOccupation(consultationOccupation);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationOccupationResponse GetConsultationOccupationList(int consultationId, int? consultationOccupationId)
        {
            try
            {
                return this._consultationDA.GetConsultationOccupationList(consultationId, consultationOccupationId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationBloodPressureReadingResponse InsertUpdateConsultationBloodPressureReading(ConsultationBloodPressureReading consultationBloodPressureReading)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationBloodPressureReading(consultationBloodPressureReading);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationBloodPressureReadingResponse GetConsultationBloodPressureReadingList(int consultationId, int? consultationBloodPressureReadingId)
        {
            try
            {
                return this._consultationDA.GetConsultationBloodPressureReadingList(consultationId, consultationBloodPressureReadingId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationMedicationResponse InsertUpdateConsultationMedication(ConsultationMedications consultationMedication)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationMedication(consultationMedication);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationMedicationResponse GetConsultationMedicationList(int consultationId, int? consultationMedicationId)
        {
            try
            {
                return this._consultationDA.GetConsultationMedicationList(consultationId, consultationMedicationId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        #region Consultation SOAP   
        
        public ConsultationSubjectiveResponse InsertUpdateConsultationSubjectives(ConsultationSubjectives consultationSubjectives)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationSubjectives(consultationSubjectives);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationSubjectiveResponse GetConsultationSubjectiveList(int consultationId, int? consultationSubjectiveId)
        {
            try
            {
                return this._consultationDA.GetConsultationSubjectiveList(consultationId, consultationSubjectiveId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationObjectiveResponse InsertUpdateConsultationObjectives(ConsultationObjectives consultationObjectives)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationObjectives(consultationObjectives);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationObjectiveResponse GetConsultationObjectiveList(int consultationId, int? consultationObjectiveId)
        {
            try
            {
                return this._consultationDA.GetConsultationObjectiveList(consultationId, consultationObjectiveId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationAssesmentResponse InsertUpdateConsultationAssesments(ConsultationAssesments consultationAssesments)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationAssesments(consultationAssesments);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationAssesmentResponse GetConsultationAssesmentList(int consultationId, int? consultationAssesmentId)
        {
            try
            {
                return this._consultationDA.GetConsultationAssesmentList(consultationId, consultationAssesmentId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationPlanResponse InsertUpdateConsultationPlans(ConsultationPlans consultationPlans)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationPlans(consultationPlans);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationPlanResponse GetConsultationPlanList(int consultationId, int? consultationPlanId)
        {
            try
            {
                return this._consultationDA.GetConsultationPlanList(consultationId, consultationPlanId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationSubjectiveNoteResponse InsertUpdateConsultationSubjectiveNotes(ConsultationSubjectiveNotes consultationSubjectiveNotes)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationSubjectiveNotes(consultationSubjectiveNotes);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationSubjectiveNoteResponse GetConsultationSubjectiveNoteList(int consultationSubjectiveId, int? consultationSubjectiveNoteId)
        {
            try
            {
                return this._consultationDA.GetConsultationSubjectiveNoteList(consultationSubjectiveId, consultationSubjectiveNoteId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationObjectiveNoteResponse InsertUpdateConsultationObjectiveNotes(ConsultationObjectiveNotes consultationObjectiveNotes)
        {
            try
            {
                return this._consultationDA.InsertUpdateConsultationObjectiveNotes(consultationObjectiveNotes);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public ConsultationObjectiveNoteResponse GetConsultationObjectiveNoteList(int consultationSubjectiveId, int? consultationObjectiveNoteId)
        {
            try
            {
                return this._consultationDA.GetConsultationObjectiveNoteList(consultationSubjectiveId, consultationObjectiveNoteId);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }
        #endregion
        #endregion

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            { }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
