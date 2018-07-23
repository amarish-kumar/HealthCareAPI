using OMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMCWebApp.Models
{
    public class ConsultationSOAPModel
    {
        public int UserId { get; set; }
        public int ConsultationId { get; set; }

        /// <summary>
        /// this is used to create the consultation subjective record
        /// </summary>
        public ConsultationSubjectiveModel ConsultationSubjectiveModelObject { get; set; }
        public ConsultationSubjectiveResponse ConsultationSubjectiveResponseObject { get; set; }

        /// <summary>
        /// this is used to create the consultation Objective record
        /// </summary>
        public ConsultationObjectiveModel ConsultationObjectiveModelObject { get; set; }
        public ConsultationObjectiveResponse ConsultationObjectiveResponseObject { get; set; }

        /// <summary>
        /// this is used to create the consultation Assesment record
        /// </summary>
        public ConsultationAssesmentModel ConsultationAssesmentModelObject { get; set; }
        public ConsultationAssesmentResponse ConsultationAssesmentResponseObject { get; set; }

        /// <summary>
        /// this is used to create the consultation plan record
        /// </summary>
        public ConsultationPlanModel ConsultationPlanModelObject { get; set; }
        public ConsultationPlanResponse ConsultationPlanResponseObject { get; set; }

    }
}