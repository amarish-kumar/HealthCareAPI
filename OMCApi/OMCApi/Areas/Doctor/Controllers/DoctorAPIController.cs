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
        /// Gets the list of Boards available in the system
        /// ~Sprint-3~
        /// OMC‌-130
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="boardId"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetBoards        
        [HttpGet]
        [Route("GetBoards")]
        public List<BoardMaster> GetBoards(bool? isActive, int? boardId, string board)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetBoards(isActive, boardId, board);
        }

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

        /// <summary>
        /// API to handle create/edit for the doctor profile award record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorAward"></param>
        /// <returns></returns>
        // POST: api/DoctorAPI/InsertUpdateDoctorAward
        [HttpPost]
        [Route("InsertUpdateDoctorAward")]
        public IHttpActionResult InsertUpdateDoctorAward([FromBody]DoctorAwards doctorAward)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            doctorAward.Active = true;
            var doctorAwardResult = DoctorBLObj.InsertUpdateDoctorAward(doctorAward, null);
            return Ok(doctorAwardResult.Message);
        }

        /// <summary>
        /// Gets the list of doctor award records/specific user award record for the doctor profile record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="doctorAwardId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetDoctorAwardList
        [HttpGet]
        [Route("GetDoctorAwardList")]
        public DoctorAwardsResponse GetDoctorAwardList(int doctorId, int? doctorAwardId)
        {
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            return DoctorBLObj.GetDoctorAwardList(doctorId, doctorAwardId);
        }

        /// <summary>
        /// API to handle create/edit for the doctor profile board record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorBoard"></param>
        /// <returns></returns>
        // POST: api/DoctorAPI/InsertUpdateDoctorBoard
        [HttpPost]
        [Route("InsertUpdateDoctorBoard")]
        public IHttpActionResult InsertUpdateDoctorBoard([FromBody]DoctorBoard doctorBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            doctorBoard.Active = true;
            var doctorBoardResult = DoctorBLObj.InsertUpdateDoctorBoard(doctorBoard, null);
            return Ok(doctorBoardResult.Message);
        }

        /// <summary>
        /// Gets the list of doctor board records/specific board record for the doctor profile record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="doctorBoardId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetDoctorBoardList
        [HttpGet]
        [Route("GetDoctorBoardList")]
        public DoctorBoardResponse GetDoctorBoardList(int doctorId, int? doctorBoardId)
        {
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            return DoctorBLObj.GetDoctorBoardList(doctorId, doctorBoardId);
        }

        /// <summary>
        /// API to handle create/edit for the doctor profile education record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorEducation"></param>
        /// <returns></returns>
        // POST: api/DoctorAPI/InsertUpdateDoctorEducation
        [HttpPost]
        [Route("InsertUpdateDoctorEducation")]
        public IHttpActionResult InsertUpdateDoctorEducation([FromBody]DoctorEducation doctorEducation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            doctorEducation.Active = true;
            var doctorEducationResult = DoctorBLObj.InsertUpdateDoctorEducation(doctorEducation, null);
            return Ok(doctorEducationResult.Message);
        }

        /// <summary>
        /// Gets the list of doctor education records/specific education record for the doctor profile record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="doctorEducationId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetDoctorEducationList
        [HttpGet]
        [Route("GetDoctorEducationList")]
        public DoctorEducationResponse GetDoctorEducationList(int doctorId, int? doctorEducationId)
        {
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            return DoctorBLObj.GetDoctorEducationList(doctorId, doctorEducationId);
        }

        /// <summary>
        /// API to handle create/edit for the doctor profile fellowship record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorFellowship"></param>
        /// <returns></returns>
        // POST: api/DoctorAPI/InsertUpdateDoctorFellowship
        [HttpPost]
        [Route("InsertUpdateDoctorEducation")]
        public IHttpActionResult InsertUpdateDoctorFellowship([FromBody]DoctorFellowship doctorFellowship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            doctorFellowship.Active = true;
            var doctorFellowshipResult = DoctorBLObj.InsertUpdateDoctorFellowship(doctorFellowship, null);
            return Ok(doctorFellowshipResult.Message);
        }

        /// <summary>
        /// Gets the list of doctor fellowship records/specific fellowship record for the doctor profile record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="doctorFellowshipId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetDoctorFellowshipList
        [HttpGet]
        [Route("GetDoctorFellowshipList")]
        public DoctorFellowshipResponse GetDoctorFellowshipList(int doctorId, int? doctorFellowshipId)
        {
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            return DoctorBLObj.GetDoctorFellowshipList(doctorId, doctorFellowshipId);
        }

        /// <summary>
        /// API to handle create/edit for the doctor profile image record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorImage"></param>
        /// <returns></returns>
        // POST: api/DoctorAPI/InsertUpdateDoctorImage
        [HttpPost]
        [Route("InsertUpdateDoctorImage")]
        public IHttpActionResult InsertUpdateDoctorImage([FromBody]DoctorImages doctorImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            doctorImage.Active = true;
            var doctorImageResult = DoctorBLObj.InsertUpdateDoctorImage(doctorImage, null);
            return Ok(doctorImageResult.Message);
        }

        /// <summary>
        /// Gets the list of doctor image records/specific image record for the doctor profile record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="doctorImageId"></param>
        /// <returns></returns>
        // Get: api/DoctorAPI/GetDoctorImageList
        [HttpGet]
        [Route("GetDoctorImageList")]
        public DoctorImagesResponse GetDoctorImageList(int doctorId, int? doctorImageId)
        {
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            return DoctorBLObj.GetDoctorImageList(doctorId, doctorImageId);
        }

        /// <summary>
        /// API to handle create/edit for the doctor profile residency record
        /// ~Sprint-3~
        /// OMC-130
        /// </summary>
        /// <param name="doctorResidency"></param>
        /// <returns></returns>
        // POST: api/DoctorAPI/InsertUpdateDoctorResidency
        [HttpPost]
        [Route("InsertUpdateDoctorResidency")]
        public IHttpActionResult InsertUpdateDoctorResidency([FromBody]DoctorResidency doctorResidency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DoctorBLObj = _Kernel.Get<IDoctorBL>();
            doctorResidency.Active = true;
            var doctorResidencyResult = DoctorBLObj.InsertUpdateDoctorResidency(doctorResidency, null);
            return Ok(doctorResidencyResult.Message);
        }
        #endregion
    }
}