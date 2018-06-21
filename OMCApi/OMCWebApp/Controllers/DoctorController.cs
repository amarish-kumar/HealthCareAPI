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

        public async Task<ActionResult> Index(int doctorId)
        {
            var model = new DoctorModel();
            model.DoctorProfileObject.DoctorId = doctorId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/DoctorAPI/GetTimezones?isActive=true&searchTerm=");
                model.Timezones = JsonConvert.DeserializeObject<List<TimezoneMaster>>(Res.Content.ReadAsStringAsync().Result);                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DoctorModel doctor)
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
        #endregion
    }
}
