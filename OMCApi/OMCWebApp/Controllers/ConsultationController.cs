using Newtonsoft.Json;
using Ninject;
using OMC.Models;
using OMCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
namespace OMCWebApp.Controllers
{
    public class ConsultationController : Controller
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public ConsultationController()
        {
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.ConsultationModule());
        }

        #endregion

        #region Methods

        // GET: Consultation/Index
        public async Task<ActionResult> Index(int patientId)
        {
            var model = new ConsultationModel();
            model.ConsultationObject.PatientId = patientId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationStatuses?isActive=true&description=");
                model.ConsultationStatuses = JsonConvert.DeserializeObject<List<ConsultationStatus>>(Res.Content.ReadAsStringAsync().Result);
                
                Res = await client.GetAsync("api/ConsultationAPI/GetDoctors?isActive=true&userRole=Doctor");
                model.Doctors = JsonConvert.DeserializeObject<List<UserDetail>>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/SignUpAPI/GetProfiles?userId=" + patientId.ToString() + "&profileId=");
                model.Profiles = JsonConvert.DeserializeObject<List<Profile>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ConsultationModel consultation)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(consultation.ConsultationObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/CreateConsultation", content);
                ConsultationResponse result = new ConsultationResponse();
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
                return View("ConsResponse", result);

            }
        }

        [HttpGet]
        public async Task<ActionResult> List(int userId, string userRole)
        {
            ConsultationListModel result = new ConsultationListModel();
            result.UserId = userId;
            result.UserRole = userRole;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationList?userId=" + userId.ToString()+ "&userRole=" + userRole);
                result.ConsultationList = JsonConvert.DeserializeObject<List<ConsultationDisplay>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> View(int userId, int consultationId, string userRole)
        {
            ConsultationViewModel result = new ConsultationViewModel();
            result.UserId = userId;
            result.ConsultationId = consultationId;
            result.UserRole = userRole;

            //set data for the records insert
            result.ConversationModelObject = new ConversationModel
                                            {
                                                ConversationObject = new Conversation
                                                {
                                                    ConsultationId = consultationId,
                                                    DoctorId = userRole.ToLower() == "doctor" ? userId : (int?) null,
                                                    PatientId = userRole.ToLower() == "patient" ? userId : (int?)null,
                                                }
                                            };
            //set data for the report record insert
            result.ReportModelObject = new ReportModel
            {
                ConsultationReportObject = new ConsultationReports
                {
                    ConsultationId = consultationId,
                    ReportDate = DateTime.Now.Date,
                    AddedBy = userId
                }
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConversationList?userId=" + userId.ToString() + "&consultationId=" + consultationId + "&userRole=" + userRole);
                result.ConversationResponseObject = JsonConvert.DeserializeObject<ConversationResponse>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationReportList?consultationId=" + consultationId.ToString() + "&consultationReportId=");
                result.ConsultationReportResponseObject = JsonConvert.DeserializeObject<ConsultationReportResponse>(Res.Content.ReadAsStringAsync().Result);
                Session["ConsultationReportResponseObject"] = result.ConsultationReportResponseObject.ConsultationReports;
                Res = await client.GetAsync("api/SignUpAPI/GetCountries?isActive=true");
                result.ReportModelObject.Countries = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateConversation(ConversationModel conversation)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(conversation.ConversationObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/CreateConversation", content);
                ConversationResponse result = new ConversationResponse();
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
                return View("ConvResponse", result);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationReport(ReportModel report)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (report.ReportFile != null)
                {
                    MemoryStream target = new MemoryStream();
                    report.ReportFile.InputStream.CopyTo(target);
                    report.ConsultationReportObject.FileData = target.ToArray();
                    report.ConsultationReportObject.FileName = report.ReportFile.FileName;
                }
                var json = JsonConvert.SerializeObject(report.ConsultationReportObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationReport", content);
                ConsultationReportResponse result = new ConsultationReportResponse();
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
                return View("ReportResponse", result);

            }
        }

        [HttpGet]
        public FileResult DownLoadFile(int consultationReportId)
        {
            var reports = Session["ConsultationReportResponseObject"] as List<ConsultationReportDisplay>;
            var currentReport = reports.Where(cr => cr.Id == consultationReportId).FirstOrDefault();
            if (currentReport != null && currentReport.FileData != null)
            {
                return File(currentReport.FileData, "application/pdf", currentReport.FileName);
            }
            return null;
        }

        [HttpGet]
        public async Task<ActionResult> EditReport(int userId, int consultationId, int consultationReportId)
        {
            //set data for the report record edit
            var ReportModelObject = new ReportModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationReportList?consultationId=" + consultationId.ToString() + "&consultationReportId="+ consultationReportId.ToString());
                var ConsultationReportResponseObject = JsonConvert.DeserializeObject<ConsultationReportResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationReportResponseObject.ConsultationReports != null
                    && ConsultationReportResponseObject.ConsultationReports.Count > 0)
                {
                    var consReportDisplay = ConsultationReportResponseObject.ConsultationReports.First();
                    ReportModelObject.ConsultationReportObject = new ConsultationReports
                    {
                        Id = consReportDisplay.Id,
                        ConsultationId = consReportDisplay.ConsultationId,
                        Description = consReportDisplay.Description,
                        DoctorName = consReportDisplay.DoctorName,
                        DoctorPhoneNumber = consReportDisplay.DoctorPhoneNumber,
                        FileName = consReportDisplay.FileName,
                        LabName = consReportDisplay.LabName,
                        ModifiedBy = userId,
                        CountryId = consReportDisplay.CountryId,
                        ReportDate = consReportDisplay.ReportDate,
                        FileData = consReportDisplay.FileData
                    };   
                }
                Res = await client.GetAsync("api/SignUpAPI/GetCountries?isActive=true");
                ReportModelObject.Countries = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);
            }            
            return View(ReportModelObject);
        }
        #endregion
    }
}