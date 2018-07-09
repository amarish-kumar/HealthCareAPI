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
        /// Gets the list of states available in the system for the country
        /// ~Sprint-3~
        /// OMC‌-130
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="countryId"></param>
        /// <param name="stateId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetStates        
        [HttpGet]
        [Route("GetStates")]
        public List<StateMaster> GetStates(bool? isActive, int? countryId, int? stateId)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetStates(isActive, countryId, stateId);
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
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            doctorProfile.Active = true;
            var doctorProfileResult = DoctorBLObj.InsertUpdateDoctorProfile(doctorProfile, null);
            return Ok(doctorProfileResult.Message);
        }

        /// <summary>
        /// API to soft delete the doctor profile
        /// ~Sprint-3~
        /// OMC‌-130
        /// </summary>
        /// <param name="doctorProfileId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/DeleteDoctorProfile
        [HttpPost]
        [Route("DeleteDoctorProfile")]
        public IHttpActionResult DeleteDoctorProfile(int doctorProfileId, int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            var doctorProfileResult = DoctorBLObj.DeleteDoctorProfile(doctorProfileId, userId);
            return Ok(doctorProfileResult.Message);
        }

        /// <summary>
        /// API to get the list of doctor profiles
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetDoctorProfileList
        [HttpGet]
        [Route("GetDoctorProfileList")]
        public List<DoctorProfileDisplay> GetDoctorProfileList(int userId, int? doctorId)
        {
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            return DoctorBLObj.GetDoctorProfileList(userId, doctorId);
        }

        /// <summary>
        /// API to handle create/edit for the doctor profile address record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="userAddress"></param>
        /// <returns></returns>
        // POST: api/DoctorAPI/InsertUpdateUserAddress
        [HttpPost]
        [Route("InsertUpdateUserAddress")]
        public IHttpActionResult InsertUpdateUserAddress([FromBody]UserAddress userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            userAddress.Active = true;
            var userAddressResult = DoctorBLObj.InsertUpdateUserAddress(userAddress, null);
            return Ok(userAddressResult.Message);
        }

        /// <summary>
        /// Gets the list of user address records/specific user address record for the doctor profile record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetUserAddressList
        [HttpGet]
        [Route("GetUserAddressList")]
        public UserAddressResponse GetUserAddressList(int userId, int? addressId)
        {
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            return DoctorBLObj.GetUserAddressList(userId, addressId);
        }
        #endregion
    }
}