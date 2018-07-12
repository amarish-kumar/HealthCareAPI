using Newtonsoft.Json;
using Ninject;
using OMC.Models;
using OMCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace OMCApi.Areas.Login.Controllers
{
    public class DoctorController : Controller
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public DoctorController()
        {
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.DoctorModule());
        }

        #endregion

        #region Methods

        public async Task<ActionResult> Index(int doctorId, int userId)
        {
            var model = new DoctorModel();
            model.DoctorProfileObject.DoctorId = doctorId;
            model.UserId = userId;

            //set data for the user address record insert
            model.UserAddressModelObject = new UserAddressModel
            {
                UserAddressObject = new UserAddress
                {
                    UserId = doctorId,
                    AddedBy = userId
                }
            };

            //set data for the doctor award record insert
            model.DoctorAwardModelObject = new DoctorAwardModel
            {
                UserId = userId,
                DoctorAwardObject = new DoctorAwards
                {
                    AddedBy = userId
                }
            };

            //set data for the doctor Board record insert
            model.DoctorBoardModelObject = new DoctorBoardModel
            {
                UserId = userId,
                DoctorBoardObject = new DoctorBoard
                {
                    AddedBy = userId
                }
            };

            //set data for the doctor award record insert
            model.DoctorEducationModelObject = new DoctorEducationModel
            {
                UserId = userId,
                DoctorEducationObject = new DoctorEducation
                {
                    AddedBy = userId
                }
            };

            //set data for the doctor fellwoship record insert
            model.DoctorFellowshipModelObject = new DoctorFellowshipModel
            {
                UserId = userId,
                DoctorFellowshipObject = new DoctorFellowship
                {
                    AddedBy = userId
                }
            };

            //set data for the doctor image record insert
            model.DoctorImageModelObject = new DoctorImageModel
            {
                UserId = userId,
                DoctorImageObject = new DoctorImages
                {
                    AddedBy = userId
                }
            };

            //set data for the doctor award record insert
            model.DoctorResidencyModelObject = new DoctorResidencyModel
            {
                UserId = userId,
                DoctorResidencyObject = new DoctorResidency
                {
                    AddedBy = userId
                }
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/DoctorAPI/GetTimezones?isActive=true&searchTerm=");
                model.Timezones = JsonConvert.DeserializeObject<List<TimezoneMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/SignUpAPI/GetCountries?isActive=true");
                model.UserAddressModelObject.CountryList = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);
                model.DoctorEducationModelObject.CountryList = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);
                model.DoctorFellowshipModelObject.CountryList = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);
                model.DoctorResidencyModelObject.CountryList = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/DoctorAPI/GetBoards?isActive=true&boardId=&board=");
                model.DoctorBoardModelObject.BoardList = JsonConvert.DeserializeObject<List<BoardMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/DoctorAPI/GetDoctorProfileList?userId=" + userId.ToString() + "&doctorId=" + doctorId.ToString());
                var doctorProfileList = JsonConvert.DeserializeObject<List<DoctorProfileDisplay>>(Res.Content.ReadAsStringAsync().Result);
                model.DoctorProfileObject = doctorProfileList.Count > 0 ? doctorProfileList.First() : model.DoctorProfileObject;
                Res = await client.GetAsync("api/DoctorAPI/GetUserAddressList?userId=" + doctorId.ToString()
                       + "&addressId=");
                model.UserAddressResponseObject = JsonConvert.DeserializeObject<UserAddressResponse>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdate(DoctorModel doctor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(doctor.DoctorProfileObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/DoctorAPI/InsertUpdateDoctorProfile", content);
                DoctorProfileResponse result = new DoctorProfileResponse();
                if (Res.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;

                }
                return View("DoctorProfileResponse", result);

            }
        }

        [HttpGet]
        public async Task<ActionResult> GetStateList(int countryId)
        {
            List<StateMaster> result = new List<StateMaster>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/DoctorAPI/GetStates?isActive=true&countryId=" + countryId.ToString() + "&stateId=");
                result = JsonConvert.DeserializeObject<List<StateMaster>>(Res.Content.ReadAsStringAsync().Result);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int doctorProfileId, int userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new { doctorProfileId = doctorProfileId, userId = userId });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/DoctorAPI/DeleteDoctorProfile?doctorProfileId="+ doctorProfileId
                    + "&userId="+userId, content);
                DoctorProfileResponse result = new DoctorProfileResponse();
                if (Res.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;

                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DoctorProfileResponse(string message, bool isSuccess)
        {
            return View("DoctorProfileResponse", new DoctorProfileResponse { Message = message, IsSuccess = isSuccess });
        }

        [HttpGet]
        public async Task<ActionResult> UserAddress(int doctorId, int? userAddressId, int userId)
        {
            var model = new UserAddressModel
            {
                UserId = userId
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/SignUpAPI/GetCountries?isActive=true");
                model.CountryList = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);
                if (model.CountryList != null && model.CountryList.Count() != 0)
                {
                    Res = await client.GetAsync("api/DoctorAPI/GetStates?isActive=true&countryId=" + model.CountryList.First().Id + "&stateId=");
                    model.StateList = JsonConvert.DeserializeObject<List<StateMaster>>(Res.Content.ReadAsStringAsync().Result);
                }
                Res = await client.GetAsync("api/SignUpAPI/GetAddressTypes?isActive=true");
                model.AddressTypes = JsonConvert.DeserializeObject<List<AddressType>>(Res.Content.ReadAsStringAsync().Result);
                if (userAddressId.HasValue)
                {
                    Res = await client.GetAsync("api/DoctorAPI/GetUserAddressList?userId=" + doctorId.ToString()
                       + "&addressId=" + (userAddressId.HasValue ? userAddressId.Value.ToString() : string.Empty));
                    var userAddressResponse = JsonConvert.DeserializeObject<UserAddressResponse>(Res.Content.ReadAsStringAsync().Result);
                    if (userAddressResponse.UserAddressList != null
                        && userAddressResponse.UserAddressList.Count() != 0)
                    {
                        model.UserAddressObject = userAddressResponse.UserAddressList.First();
                    }
                }
                else
                {
                    model.UserAddressObject = new UserAddress
                    {
                        UserId = doctorId
                    };
                }
            }
            return View("UserAddress", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateUserAddress(UserAddressModel userAddress)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(userAddress.UserAddressObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/DoctorAPI/InsertUpdateUserAddress", content);
                UserAddressResponse result = new UserAddressResponse();
                if (Res.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;

                }
                return View("UserAddressResponse", result);

            }
        }

        [HttpGet]
        public async Task<ActionResult> DoctorAward(int doctorId, int? doctorAwardId, int userId)
        {
            var model = new DoctorAwardModel
            {
                UserId = userId
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/SignUpAPI/GetCountries?isActive=true");
                if (doctorAwardId.HasValue)
                {
                    //Res = await client.GetAsync("api/DoctorAPI/GetUserAddressList?userId=" + doctorId.ToString()
                    //   + "&addressId=" + (userAddressId.HasValue ? userAddressId.Value.ToString() : string.Empty));
                    //var userAddressResponse = JsonConvert.DeserializeObject<UserAddressResponse>(Res.Content.ReadAsStringAsync().Result);
                    //if (userAddressResponse.UserAddressList != null
                    //    && userAddressResponse.UserAddressList.Count() != 0)
                    //{
                    //    model.UserAddressObject = userAddressResponse.UserAddressList.First();
                    //}
                }
                else
                {
                    model.DoctorAwardObject = new DoctorAwards
                    {
                        DoctorId = doctorId
                    };
                }
            }
            return View("DoctorAward", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateDoctorAward(DoctorAwardModel doctorAward)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(doctorAward.DoctorAwardObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/DoctorAPI/InsertUpdateDoctorAward", content);
                UserAddressResponse result = new UserAddressResponse();
                if (Res.IsSuccessStatusCode)
                {
                    result.IsSuccess = true;
                    result.Message = Res.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = Res.Content.ReadAsStringAsync().Result;

                }
                return View("UserAddressResponse", result);

            }
        }
        #endregion
    }
}
