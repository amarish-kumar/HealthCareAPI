using Ninject;
using OMC.BL.Interface;
using OMC.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace OMCApi.Areas.Doctor.Controllers
{
    /// <summary>
    /// This controller hosts all APIs related to Consultation functionality
    /// </summary>
    [RoutePrefix("api/DoctorAPI")]
    public class DoctorAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for DoctorAPIController
        /// </summary>
        public DoctorAPIController()
        {
            //_Kernel = new StandardKernel(new OMC.Modules.SignInModule());
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.DoctorModule());
        }

        #endregion

        #region Actions
        /// <summary>
        /// Gets the list of Timezones available in the system
        /// ~Sprint-3~
        /// OMC‌-130
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetTimezones        
        [HttpGet]
        [Route("GetTimezones")]
        public List<TimezoneMaster> GetTimezones(bool? isActive, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetTimezones(isActive, searchTerm);
        }

        /// <summary>
        /// API to handle create/edit for the doctor profile record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorProfile"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        // POST: api/DoctorAPI/InsertUpdateDoctorProfile
        [HttpPost]
        [Route("InsertUpdateDoctorProfile")]
        public IHttpActionResult InsertUpdateDoctorProfile([FromBody]DoctorProfile doctorProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctor>();
            doctorProfile.Active = true;
            var doctorProfileResult = DoctorBLObj.InsertUpdateDoctorProfile(doctorProfile, null);
            return Ok(doctorProfileResult.Message);
        }
        #endregion
    }
}