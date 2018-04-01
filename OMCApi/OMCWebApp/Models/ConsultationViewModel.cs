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
    }
}