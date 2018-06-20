using Ninject;
using OMC.BL.Interface;
using OMC.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace OMCApi.Areas.Login.Controllers
{
    /// <summary>
    /// This controller hosts all APIs related to login functionality
    /// </summary>
    [RoutePrefix("api/LoginAPI")]
    public class LoginAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public LoginAPIController()
        {
            //_Kernel = new StandardKernel(new OMC.Modules.SignInModule());
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.SignInModule());
        }

        #endregion

        // Get: api/LoginAPI/GetRoles
        /// <summary>
        /// Gets the list of Roles available in the system
        /// </summary>
        /// <param name="isActive">pass the value of isActive flag</param>
        /// <param name="roleDescription">pass the value of roleDescription to filter by role. Pass null/empty to bypass the filter</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoles")]
        public List<Role> GetRoles(bool? isActive, string roleDescription)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetRoles(isActive, roleDescription);
        }

        /// <summary>
        /// Posts the User Login data and checks for the validity
        /// ~Sprint-1~
        /// OMC‌-2
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: api/LoginAPI
        [HttpPost]
        [Route("PostUserLogin")]
        public SignInResponse PostUserLogin([FromBody]UserLogin user)
        {
            var SignInObj = _Kernel.Get<ISignIn>();
            var SignInResult = SignInObj.InitiateSignInProcess(user);
            return SignInResult;
        }

        /// <summary>
        /// Gets the 6 digit access code for validating the new device by email/sms
        /// ~Sprint-1~
        /// OMC‌-2
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: api/GetAccessCode
        [HttpPost]
        [Route("GetAccessCode")]
        public UserAccessCodeResponse GetAccessCode([FromBody]UserLogin user)
        {
            var SignInObj = _Kernel.Get<ISignIn>();
            var getUserAccessCodeResult = SignInObj.GetAccessCode(user);

            return getUserAccessCodeResult;
        }

        /// <summary>
        /// API to validate the 6 digit access code against the new device
        /// ~Sprint-1~
        /// OMC‌-2
        /// </summary>
        /// <param name="userAccessCode"></param>
        /// <returns></returns>
        // POST: api/LoginAPI
        [HttpPost]
        [Route("ValidateAccessCode")]
        public UserAccessCodeResponse ValidateAccessCode([FromBody]UserAccessCodeResponse userAccessCode)
        {
            var SignInObj = _Kernel.Get<ISignIn>();
            SignInObj.ValidateAccessCode(userAccessCode);
            return userAccessCode;
        }
    }
}
