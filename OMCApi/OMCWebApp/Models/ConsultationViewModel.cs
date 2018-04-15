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
    }
}