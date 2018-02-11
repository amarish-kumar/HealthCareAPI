using Ninject;
using OMC.BL.Interface;
using OMC.Models;
using System.Collections.Generic;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OMC.Models;
using Ninject;
using OMC.BL.Interface;
using System.Data;

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
        #endregion
    }
}