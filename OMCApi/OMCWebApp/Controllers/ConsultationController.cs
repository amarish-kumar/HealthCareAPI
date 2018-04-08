using Newtonsoft.Json;
using Ninject;
using OMC.Models;
using OMCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            //set data for the surgery record insert
            result.SurgeryModelObject = new SurgeryModel
            {
                ConsultationSurgeryObject = new ConsultationSurgeries
                {
                    ConsultationId = consultationId,
                    AddedBy = userId
                }
            };

            //set data for the cancer treatment record insert
            result.CancerTreatmentModelObject = new CancerTreatmentModel
            {
                ConsultationCancerTreatmentObject = new ConsultationCancerTreatments
                {
                    ConsultationId = consultationId,
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
                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationSurgeryList?consultationId=" + consultationId.ToString() + "&consultationSurgeryId=");
                result.ConsultationSurgeryResponseObject = JsonConvert.DeserializeObject<ConsultationSurgeryResponse>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationCancerTreatmentList?consultationId=" + consultationId.ToString() + "&consultationCancerTreatmentId=");
                result.ConsultationCancerTreatmentResponseObject = JsonConvert.DeserializeObject<ConsultationCancerTreatmentResponse>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/SignUpAPI/GetCountries?isActive=true");
                result.ReportModelObject.Countries = JsonConvert.DeserializeObject<List<Country>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetCancerStages?isActive=true&cancerStageName=");
                result.CancerTreatmentModelObject.CancerStages = JsonConvert.DeserializeObject<List<CancerStageMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetSurgeryList?isActive=true&surgeryName=&searchTerm=");
                result.SurgeryModelObject.SurgeryList = JsonConvert.DeserializeObject<List<SurgeryMaster>>(Res.Content.ReadAsStringAsync().Result);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationSurgery(SurgeryModel surgery)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var json = JsonConvert.SerializeObject(surgery.ConsultationSurgeryObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationSurgery", content);
                ConsultationSurgeryResponse result = new ConsultationSurgeryResponse();
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
                return View("SurgeryResponse", result);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> EditSurgery(int userId, int consultationId, int consultationSurgeryId)
        {
            //set data for the surgery record edit
            var SurgeryModelObject = new SurgeryModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationSurgeryList?consultationId=" + consultationId.ToString() + "&consultationSurgeryId=" + consultationSurgeryId.ToString());
                var ConsultationSurgeryResponseObject = JsonConvert.DeserializeObject<ConsultationSurgeryResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationSurgeryResponseObject.ConsultationSurgeriesList != null
                    && ConsultationSurgeryResponseObject.ConsultationSurgeriesList.Count > 0)
                {
                    var consSurgeryDisplay = ConsultationSurgeryResponseObject.ConsultationSurgeriesList.First();
                    SurgeryModelObject.ConsultationSurgeryObject = new ConsultationSurgeries
                    {
                        Id = consSurgeryDisplay.Id,
                        ConsultationId = consSurgeryDisplay.ConsultationId,
                        SurgeryId = consSurgeryDisplay.SurgeryId,
                        SurgeryDate = consSurgeryDisplay.SurgeryDate,
                        ModifiedBy = userId,
                        Active = consSurgeryDisplay.Active
                    };
                }
                Res = await client.GetAsync("api/ConsultationAPI/GetSurgeryList?isActive=true&surgeryName=&searchTerm=");
                SurgeryModelObject.SurgeryList = JsonConvert.DeserializeObject<List<SurgeryMaster>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(SurgeryModelObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationCancerTreatment(CancerTreatmentModel cancerTreatment)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(cancerTreatment.ConsultationCancerTreatmentObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationCancerTreatment", content);
                ConsultationCancerTreatmentResponse result = new ConsultationCancerTreatmentResponse();
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
                return View("CancerTreatmentResponse", result);

            }
        }

        [HttpGet]
        public async Task<ActionResult> EditCancerTreatment(int userId, int consultationId, int consultationCancerTreatmentId)
        {
            //set data for the surgery record edit
            var CancerTreatmentModelObject = new CancerTreatmentModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationCancerTreatmentList?consultationId=" + consultationId.ToString() + "&consultationCancerTreatmentId=" + consultationCancerTreatmentId.ToString());
                var ConsultationCancerTreatmentResponseObject = JsonConvert.DeserializeObject<ConsultationCancerTreatmentResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationCancerTreatmentResponseObject.ConsultationCancerTreatmentList != null
                    && ConsultationCancerTreatmentResponseObject.ConsultationCancerTreatmentList.Count > 0)
                {
                    var consCancerTreatmentDisplay = ConsultationCancerTreatmentResponseObject.ConsultationCancerTreatmentList.First();
                    CancerTreatmentModelObject.ConsultationCancerTreatmentObject = new ConsultationCancerTreatments
                    {
                        Id = consCancerTreatmentDisplay.Id,
                        ConsultationId = consCancerTreatmentDisplay.ConsultationId,
                        CancerStageId = consCancerTreatmentDisplay.CancerStageId,
                        CancerType = consCancerTreatmentDisplay.CancerType,
                        DignosisDate = consCancerTreatmentDisplay.DignosisDate,
                        TreatmentCompletionDate = consCancerTreatmentDisplay.TreatmentCompletionDate,
                        IsTreatmentOn = consCancerTreatmentDisplay.IsTreatmentOn,
                        TreatmentType = consCancerTreatmentDisplay.TreatmentType,
                        ModifiedBy = userId,
                        Active = consCancerTreatmentDisplay.Active
                    };
                }
                Res = await client.GetAsync("api/ConsultationAPI/GetCancerStages?isActive=true&cancerStageName=");
                CancerTreatmentModelObject.CancerStages = JsonConvert.DeserializeObject<List<CancerStageMaster>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(CancerTreatmentModelObject);
        }
        #endregion
    }
}