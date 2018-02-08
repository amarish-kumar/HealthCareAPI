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
        //[Authorize]
        public IHttpActionResult PostUserSignUp([FromBody]UserSignUp userdetails)
        {

            var SignUpResult = false;
            var SignUpObj = _Kernel.Get<ISignUp>();

            if (!ModelState.IsValid)
            {
                //return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return BadRequest(ModelState);
            }
            else
                SignUpResult = SignUpObj.InitiateSignUpProcess(userdetails);

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

        // PUT: api/SignUpAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

       
    }
}
