using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMC.Models;
using Ninject;
using OMC.BL.Interface;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace OMCWebApp.Controllers
{
    public class SignUpController : Controller
    {


        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public SignUpController()
        {
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.SignUpModule());
        }

        #endregion

        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CSRSignUp()
        {
            return View();
        }


        public ActionResult DoctorSignUp()
        {
            return View();
        }

        public ActionResult PatientEnquiry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveSignUpDetails(UserSignUp userdetails)
        {
            //var SignUpObj = _Kernel.Get<ISignUp>();

            //Patient
            //userdetails.UserType = 1;
            //CSR
            //userdetails.UserType = 3;

            //userdetails.Active = 1;

          

            using (var client = new HttpClient())
            {

                //Passing service base url  
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(userdetails);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/SignUpAPI/PostUserSignUp", content);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var SignInResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //UserInfo = JsonConvert.DeserializeObject<List<User>>(SignInResponse);
                    //if (Convert.ToBoolean(SignInResponse))
                        return View("Index");
                    //else
                      //  return View("LoginFailure");
                }
                else
                {
                    var SignInResponse = Res.Content.ReadAsStringAsync().Result;
                    ViewData["ErrorMessage"] = SignInResponse.ToString();
                    return View("SignUpFailure");
                    
                }
                    //return View("Index");
                //return View("LoginFailure");

            }
        }
    }
}