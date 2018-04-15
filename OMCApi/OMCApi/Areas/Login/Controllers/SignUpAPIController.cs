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

namespace OMCApi.Areas.Login.Controllers
{
    [RoutePrefix("api/SignUpAPI")]
    public class SignUpAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public SignUpAPIController()
        {
            //_Kernel = new StandardKernel(new OMC.Modules.SignInModule());
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.SignUpModule());
        }

        #endregion

        // GET: api/SignUpAPI
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Get: api/SignUpAPI/GetCountries
        [HttpGet]
        [Route("GetCountries")]
        public List<Country> GetCountries(bool? isActive)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetCountries(isActive);
        }

        // Get: api/SignUpAPI/GetAddressTypes
        [HttpGet]
        [Route("GetAddressTypes")]
        public List<AddressType> GetAddressTypes(bool? isActive)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetAddressTypes(isActive);
        }

        [HttpPost]
        [Route("PostUserSignUp")]
        //[Authorize]
        public IHttpActionResult PostUserSignUp([FromBody]UserSignUp userdetails)
        {

            try
            {
                var SignUpResult = false;
                var SignUpObj = _Kernel.Get<ISignUp>();

                if (!ModelState.IsValid)
                {
                    //return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return BadRequest(ModelState);
                }
                else
                {
                    if (userdetails.UserType == 4 && !userdetails.isTnCAccepted)
                        return BadRequest("Patient Terms and Conditions not accepted");
                    else
                        SignUpResult = SignUpObj.InitiateSignUpProcess(userdetails);
                }

                if (SignUpResult)
                {
                    if (userdetails.UserType == 4)
                        return Ok("Patient details saved");
                    else if (userdetails.UserType == 3)
                        return Ok("CSRAdmin details saved");
                    else if (userdetails.UserType == 2)
                        return Ok("CSR details saved");
                    else if (userdetails.UserType == 5)
                        return Ok("Doctor details saved");
                    else
                        return Ok("SuperAdmin details saved");
                }
                else
                    return BadRequest("SignUp Error!");
            }
            catch(Exception ex)
            {
                if (ex.ToString().Contains("Violation of UNIQUE KEY constraint 'UC_UserDetail_PhoneNumber'") || ex.ToString().Contains("Violation of UNIQUE KEY constraint 'UC_UserDetail_EmailAddress'"))
                    return BadRequest("User is already registered!");
                else
                    return BadRequest(ex.ToString());
            }
        }

        // POST: api/SignUpAPI/InsertUpdateProfile
        [HttpPost]
        [Route("InsertUpdateProfile")]
        public IHttpActionResult InsertUpdateProfile([FromBody]Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var SignUpObj = _Kernel.Get<ISignUp>();
            profile.Active = true;
            var InsertUpdateProfileResult = SignUpObj.InsertUpdateProfile(profile);
            return Ok(InsertUpdateProfileResult);
        }

        // Get: api/SignUpAPI/GetProfiles
        [HttpGet]
        [Route("GetProfiles")]
        public List<Profile> GetProfiles(int userId, int? profileId)
        {
            var signupObj = _Kernel.Get<ISignUp>();
            return signupObj.GetProfileList(userId, profileId);
        }

        // Get: api/SignUpAPI/GetRelationships
        [HttpGet]
        [Route("GetRelationships")]
        public List<RelationshipMaster> GetRelationships(bool? isActive, string relationship, bool? excludeSelf)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetRelationships(isActive, relationship, excludeSelf);
        }

        // Get: api/SignUpAPI/GetGenders
        [HttpGet]
        [Route("GetGenders")]
        public List<Gender> GetGenders(bool? isActive, string genderName)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetGenders(isActive, genderName);
        }
    }
}
