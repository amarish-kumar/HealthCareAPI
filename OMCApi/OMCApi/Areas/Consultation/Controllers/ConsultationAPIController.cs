﻿using Ninject;
using OMC.BL.Interface;
using OMC.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace OMCApi.Areas.Consultation.Controllers
{
    [RoutePrefix("api/ConsultationAPI")]
    public class ConsultationAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public ConsultationAPIController()
        {
            //_Kernel = new StandardKernel(new OMC.Modules.SignInModule());
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.ConsultationModule());
        }

        #endregion

        #region Actions
        // Get: api/ConsultationAPI/GetConsultationStatuses
        [HttpGet]
        [Route("GetConsultationStatuses")]
        public List<ConsultationStatus> GetConsultationStatuses(bool? isActive, string description)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetConsultationStatuses(isActive, description);
        }

        // Get: api/ConsultationAPI/GetDoctors
        [HttpGet]
        [Route("GetDoctors")]
        public List<UserDetail> GetDoctors(bool? isActive, string userRole)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetUserList(isActive, userRole);
        }

        // Get: api/ConsultationAPI/GetUnregisteredPatientEnquiry
        [HttpPost]
        [Route("GetUnregisteredPatientEnquiry")]
        public PatientEnquiryResponse GetUnregisteredPatientEnquiry([FromBody]OMC.Models.PatientEnquiry enquiry)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.UnregisteredPatientEnquiry(enquiry);

        }

        // Get: api/ConsultationAPI/GetConsultationList
        [HttpGet]
        [Route("GetConsultationList")]
        public List<ConsultationDisplay> GetConsultationList(int userId, string userRole)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationList(userId, userRole);
        }

        // Get: api/ConsultationAPI/GetConsultationList
        [HttpGet]
        [Route("GetConversationList")]
        public ConversationResponse GetConversationList(int consultationId, int userId, string userRole)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConversationList(consultationId, userId, userRole);
        }

        // POST: api/ConsultationAPI/CreateConsultation
        [HttpPost]
        [Route("CreateConsultation")]
        public IHttpActionResult CreateConsultation([FromBody]OMC.Models.Consultation consultation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultation.Active = true;
            var ConsultationResult = ConsultationBLObj.InitiateConsultation(consultation);
            return Ok(ConsultationResult.Message); 
        }

        // POST: api/ConsultationAPI/CreateConsultation
        [HttpPost]
        [Route("CreateConversation")]
        public IHttpActionResult CreateConversation([FromBody]Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            conversation.Active = true;
            var ConversationResult = ConsultationBLObj.RecordConversation(conversation);
            return Ok(ConversationResult.Message);
        }

        // POST: api/ConsultationAPI/InsertUpdateConsultationReport
        [HttpPost]
        [Route("InsertUpdateConsultationReport")]
        public IHttpActionResult InsertUpdateConsultationReport([FromBody]ConsultationReports consultationReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationReport.Active = true;
            var ConversationResult = ConsultationBLObj.InsertUpdateConsultationReport(consultationReport);
            return Ok(ConversationResult.Message);
        }

        // Get: api/ConsultationAPI/GetConsultationReportList
        [HttpGet]
        [Route("GetConsultationReportList")]
        public ConsultationReportResponse GetConsultationReportList(int consultationId, int? consultationReportId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationReportList(consultationId, consultationReportId);
        }
        #endregion
    }
}