using OMC.Models;

namespace OMCWebApp.Models
{
    public class ConsultationViewModel
    {
        public ConversationResponse ConversationResponseObject { get; set; }
        public int UserId { get; set; }
        public int ConsultationId { get; set; }
        public string UserRole { get; set; }
        /// <summary>
        /// this is used to create the record
        /// </summary>
        public ConversationModel ConversationModelObject { get; set; }

        /// <summary>
        /// this is used to create the report record
        /// </summary>
        public ReportModel ReportModelObject { get; set; }
        public ConsultationReportResponse ConsultationReportResponseObject { get; set; }

        /// <summary>
        /// this is used to create the surgery record
        /// </summary>
        public SurgeryModel SurgeryModelObject { get; set; }
        public ConsultationSurgeryResponse ConsultationSurgeryResponseObject { get; set; }

        /// <summary>
        /// this is used to create the cancer treatment record
        /// </summary>
        public CancerTreatmentModel CancerTreatmentModelObject { get; set; }
        public ConsultationCancerTreatmentResponse ConsultationCancerTreatmentResponseObject { get; set; }

        /// <summary>
        /// this is used to create the Illegal Drugs details record
        /// </summary>
        public IllegalDrugDetailsModel IllegalDrugDetailsModelObject { get; set; }
        public ConsultationIllegalDrugDetailsResponse ConsultationIllegalDrugDetailsResponseObject { get; set; }


        /// <summary>
        /// this is used to create the Illegal Drugs details record
        /// </summary>
        public SDDHabitsModel SDDHabitsModelObject { get; set; }
        public ConsultationSDDHabitsResponse ConsultationSDDHabitsResponseObject { get; set; }

        /// <summary>
        /// this is used to create the allergy record
        /// </summary>
        public AllergyModel AllergyModelObject { get; set; }
        public ConsultationAllergyResponse ConsultationAllergyResponseObject { get; set; }

        /// <summary>
        /// this is used to create the family history record
        /// </summary>
        public FamilyHistoryModel FamilyHistoryModelObject { get; set; }
        public ConsultationFamilyHistoryResponse ConsultationFamilyHistoryResponseObject { get; set; }

        /// <summary>
        /// this is used to create the existing conditions record
        /// </summary>
        public FamilyHistoryModel ExistingConditionModelObject { get; set; }
        public ConsultationFamilyHistoryResponse ConsultationExistingConditionResponseObject { get; set; }

        /// <summary>
        /// this is used to create the occupation record
        /// </summary>
        public OccupationModel OccupationModelObject { get; set; }
        public ConsultationOccupationResponse ConsultationOccupationResponseObject { get; set; }

        /// <summary>
        /// this is used to create the blood pressure record
        /// </summary>
        public BloodPressureReadingModel BloodPressureReadingModelObject { get; set; }
        public ConsultationBloodPressureReadingResponse ConsultationBloodPressureReadingResponseObject { get; set; }

        /// <summary>
        /// this is used to create the medication record
        /// </summary>
        public MedicationModel MedicationModelObject { get; set; }
        public ConsultationMedicationResponse ConsultationMedicationResponseObject { get; set; }
    }
}