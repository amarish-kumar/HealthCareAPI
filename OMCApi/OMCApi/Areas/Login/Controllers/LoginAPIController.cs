﻿using Ninject;
using OMC.BL.Interface;
using OMC.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace OMCApi.Areas.Login.Controllers
{
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
        [HttpGet]
        [Route("GetRoles")]
        public List<Role> GetRoles(bool? isActive, string roleDescription)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetRoles(isActive, roleDescription);
        }


        // POST: api/LoginAPI
        [HttpPost]
        [Route("PostUserLogin")]
        public SignInResponse PostUserLogin([FromBody]UserLogin user)
        {
            var SignInObj = _Kernel.Get<ISignIn>();
            var SignInResult = SignInObj.InitiateSignInProcess(user);
            return SignInResult;
        }

        // POST: api/LoginAPI
        [HttpPost]
        [Route("GetAccessCode")]
        public UserAccessCodeResponse GetAccessCode([FromBody]UserLogin user)
        {
            var SignInObj = _Kernel.Get<ISignIn>();
            var getUserAccessCodeResult = SignInObj.GetAccessCode(user);

            return getUserAccessCodeResult;
        }

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
