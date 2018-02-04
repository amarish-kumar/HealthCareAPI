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



        [HttpPost]
        [Route("PostUserSignUp")]
        public IHttpActionResult PostUserSignUp([FromBody]UserSignUp userdetails)
        {

            //CSR level validation
            if (userdetails.UserType == 3)
            {
                ModelState["Gender"].Errors.Clear();
                //ModelState.Remove("Gender");
            }
            if (userdetails.UserType == 1)
            {
                ModelState.Remove("Address");
                ModelState.Remove("AlternateNo");
            }

            if (!ModelState.IsValid)
            {
                //return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return BadRequest(ModelState);
            }

            var SignUpObj = _Kernel.Get<ISignUp>();
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            var SignUpValidation = SignUpObj.ValidateSignUpDetails(userdetails);

            var SignUpResult = false;

            if (SignUpValidation.ExceptionType == "Validation Success")
            {
                SignUpResult = SignUpObj.InitiateSignUpProcess(userdetails);
            }
            else
                return BadRequest(SignUpValidation.Message);

            return Ok("Patient details saved");
            //return SignUpResult;
        }

        // PUT: api/SignUpAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

       
    }
}
