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
using OMCWebApp.Models;

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

        #region Actions

        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CSRSignUp()
        {
            var model = new UserSignUp();
            
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/SignUpAPI/GetAddressTypes?isActive=true");
                model.AddressTypes = JsonConvert.DeserializeObject<List<AddressType>>(Res.Content.ReadAsStringAsync().Result);

                //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Res = await client.GetAsync("api/SignUpAPI/GetCountries?isActive=true");
                model.Countries = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);

            }
            return View(model);
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
                        return View("SignUpSuccessfull");
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
        
        [HttpGet]
        public async Task<ActionResult> AddProfile(int userId)
        {
            var model = new ProfileModel();
            model.ProfileObject.UserId = userId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/SignUpAPI/GetRelationships?isActive=true&relationship=&excludeSelf=true");
                model.Relationships = JsonConvert.DeserializeObject<List<RelationshipMaster>>(Res.Content.ReadAsStringAsync().Result);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Res = await client.GetAsync("api/SignUpAPI/GetGenders?isActive=true&genderName=");
                model.Genders = JsonConvert.DeserializeObject<List<Gender>>(Res.Content.ReadAsStringAsync().Result);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateProfile(int userId, int profileId)
        {
            var model = new ProfileModel();
            model.ProfileObject.UserId = userId;
            model.ProfileObject.Id = profileId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/SignUpAPI/GetRelationships?isActive=true&relationship=&excludeSelf=true");
                model.Relationships = JsonConvert.DeserializeObject<List<RelationshipMaster>>(Res.Content.ReadAsStringAsync().Result);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Res = await client.GetAsync("api/SignUpAPI/GetGenders?isActive=true&genderName=");
                model.Genders = JsonConvert.DeserializeObject<List<Gender>>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/SignUpAPI/GetProfiles?userId=" + userId.ToString() + "&profileId=" + profileId.ToString());
                var profiles = JsonConvert.DeserializeObject<List<Profile>>(Res.Content.ReadAsStringAsync().Result);
                if(profiles!=null && profiles.Count() != 0)
                {
                    model.ProfileObject = profiles.First();
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> InsertUpdateProfile(ProfileModel profile)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(profile.ProfileObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/SignUpAPI/InsertUpdateProfile", content);

                if (Res.IsSuccessStatusCode)
                {
                    var isSuccess = JsonConvert.DeserializeObject<bool>(Res.Content.ReadAsStringAsync().Result);
                    if (isSuccess)
                    {
                        return View("AddUpdateProfileSuccess", profile);
                    }
                }
                return View("LoginFailure");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ProfileList(int userId)
        {
            var model = new ProfileListModel();
            model.UserId = userId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/SignUpAPI/GetRelationships?isActive=true&relationship=&excludeSelf=false");
                model.Relationships = JsonConvert.DeserializeObject<List<RelationshipMaster>>(Res.Content.ReadAsStringAsync().Result);
                
                Res = await client.GetAsync("api/SignUpAPI/GetGenders?isActive=true&genderName=");
                model.Genders = JsonConvert.DeserializeObject<List<Gender>>(Res.Content.ReadAsStringAsync().Result);
                
                Res = await client.GetAsync("api/SignUpAPI/GetProfiles?userId=" + userId.ToString() + "&profileId=");
                model.Profiles = JsonConvert.DeserializeObject<List<Profile>>(Res.Content.ReadAsStringAsync().Result);
            }

            return View(model);
        }
        #endregion
    }
}