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

                Res = await client.GetAsync("api/ConsultationAPI/GetPackageList?isActive=true&packageId=");
                model.Packages = JsonConvert.DeserializeObject<List<PackageMaster>>(Res.Content.ReadAsStringAsync().Result);
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

            //set data for the Illegal Drug detail record insert
            result.IllegalDrugDetailsModelObject = new IllegalDrugDetailsModel
            {
                ConsultationIllegalDrugDetailsObject = new ConsultationIllegalDrugDetails
                {
                    ConsultationId = consultationId,
                    AddedBy = userId
                }
            };

            //set data for the Smoking Habits record insert
            result.SDDHabitsModelObject = new SDDHabitsModel
            {
                ConsultationSDDHabitsObject = new ConsultationSDDHabits
                {
                    ConsultationId = consultationId,
                    AddedBy = userId
                }
            };

            //set data for the allergy record insert
            result.AllergyModelObject = new AllergyModel 
            {
                ConsultationAllergyObject = new ConsultationAllergies
                {
                    ConsultationId = consultationId,
                    AddedBy = userId
                }
            };

            //set data for the family history record insert
            result.FamilyHistoryModelObject = new FamilyHistoryModel
            {
                ConsultationFamilyHistoryObject = new ConsultationFamilyHistory
                {
                    ConsultationId = consultationId,
                    IsAlive = true,
                    AddedBy = userId
                }
            };

            //set data for the existing condition record insert
            result.ExistingConditionModelObject = new FamilyHistoryModel
            {
                ConsultationFamilyHistoryObject = new ConsultationFamilyHistory
                {
                    ConsultationId = consultationId,
                    RelationshipId = 1, //hard coded for self
                    IsAlive = true,
                    AddedBy = userId
                }
            };

            //set data for the occupation record insert
            result.OccupationModelObject = new OccupationModel
            {
                ConsultationOccupationObject = new ConsultationOccupation
                {
                    ConsultationId = consultationId,
                    AddedBy = userId
                }
            };

            //set data for the blood pressure record insert
            result.BloodPressureReadingModelObject = new BloodPressureReadingModel
            {
                ConsultationBloodPressureReadingObject = new ConsultationBloodPressureReading
                {
                    ConsultationId = consultationId,
                    AddedBy = userId
                }
            };

            //set data for the medication record insert
            result.MedicationModelObject = new MedicationModel
            {
                ConsultationMedicationObject = new ConsultationMedications
                {
                    ConsultationId = consultationId,
                    AddedBy = userId,
                },
                DrugSubTypeList = new List<DrugSubTypeMaster>()
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

                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationIllegalDrugDetailList?consultationId=" + consultationId.ToString() + "&consultationIllegalDrugDetailsId=");
                result.ConsultationIllegalDrugDetailsResponseObject = JsonConvert.DeserializeObject<ConsultationIllegalDrugDetailsResponse>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetIllegalDrugs?isActive=true&IllegalDrug=");
                result.IllegalDrugDetailsModelObject.IllegalDrugDetails = JsonConvert.DeserializeObject<List<IllegalDrugMaster>>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationSDDHabitsList?consultationId=" + consultationId.ToString() + "&consultationSDDHabitsId=");
                result.ConsultationSDDHabitsResponseObject = JsonConvert.DeserializeObject<ConsultationSDDHabitsResponse>(Res.Content.ReadAsStringAsync().Result);
                

                Res = await client.GetAsync("api/ConsultationAPI/GetAllergyList?isActive=true&allergyName=&searchTerm=");
                result.AllergyModelObject.AllergyList = JsonConvert.DeserializeObject<List<AllergyMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetHealthConditionList?isActive=true&healthConditionName=&searchTerm=");
                result.ExistingConditionModelObject.HealthConditionList = result.FamilyHistoryModelObject.HealthConditionList = JsonConvert.DeserializeObject<List<HealthConditionMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/SignUpAPI/GetRelationships?isActive=true&relationship=&excludeSelf=true");
                result.FamilyHistoryModelObject.RelationshipList = JsonConvert.DeserializeObject<List<RelationshipMaster>>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationAllergyList?consultationId=" + consultationId.ToString() + "&consultationAllergyId=");
                result.ConsultationAllergyResponseObject = JsonConvert.DeserializeObject<ConsultationAllergyResponse>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationFamilyHistoryList?consultationId=" + consultationId.ToString() + "&consultationFamilyHistoryId=&relationshipId=&excludeSelf=true");
                result.ConsultationFamilyHistoryResponseObject = JsonConvert.DeserializeObject<ConsultationFamilyHistoryResponse>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationFamilyHistoryList?consultationId=" + consultationId.ToString() + "&consultationFamilyHistoryId=&relationshipId=1&excludeSelf=false");
                result.ConsultationExistingConditionResponseObject = JsonConvert.DeserializeObject<ConsultationFamilyHistoryResponse>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/SignUpAPI/GetRelationships?isActive=true&relationship=self&excludeSelf=false");
                result.ExistingConditionModelObject.RelationshipList = JsonConvert.DeserializeObject<List<RelationshipMaster>>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/ConsultationAPI/GetOccupationList?isActive=true&occupationName=&searchTerm=");
                result.OccupationModelObject.OccupationList = JsonConvert.DeserializeObject<List<OccupationMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationOccupationList?consultationId=" + consultationId.ToString() + "&consultationOccupationId=");
                result.ConsultationOccupationResponseObject = JsonConvert.DeserializeObject<ConsultationOccupationResponse>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationBloodPressureReadingList?consultationId=" + consultationId.ToString() + "&consultationBloodPressureReadingId=");
                result.ConsultationBloodPressureReadingResponseObject = JsonConvert.DeserializeObject<ConsultationBloodPressureReadingResponse>(Res.Content.ReadAsStringAsync().Result);

                Res = await client.GetAsync("api/ConsultationAPI/GetDrugTypeList?isActive=true&drugType=&searchTerm=");
                result.MedicationModelObject.DrugTypeList = JsonConvert.DeserializeObject<List<DrugTypeMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetDrugBrandList?isActive=true&drugBrandName=&searchTerm=");
                result.MedicationModelObject.DrugBrandList = JsonConvert.DeserializeObject<List<DrugBrandMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetDrugChemicalList?isActive=true&drugChemicalName=&searchTerm=");
                result.MedicationModelObject.DrugChemicalList = JsonConvert.DeserializeObject<List<DrugChemicalMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetDrugFrequencyList?isActive=true&drugFrequencyName=&searchTerm=");
                result.MedicationModelObject.DrugFrequencyList = JsonConvert.DeserializeObject<List<DrugFrequencyMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/ConsultationAPI/GetConsultationMedicationList?consultationId=" + consultationId.ToString() + "&consultationMedicationId=");
                result.ConsultationMedicationResponseObject = JsonConvert.DeserializeObject<ConsultationMedicationResponse>(Res.Content.ReadAsStringAsync().Result);
            }

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Daily",
                Value = "Daily"
            });
            items.Add(new SelectListItem
            {
                Text = "Weekly",
                Value = "Weekly"
            });
            items.Add(new SelectListItem
            {
                Text = "Socially",
                Value = "Socially"
            });
            ViewBag.DDLItems = items;

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
                        OtherDescription = consSurgeryDisplay.OtherDescription,
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationIllegalDrugDetail(IllegalDrugDetailsModel illegaldrugDetails)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(illegaldrugDetails.ConsultationIllegalDrugDetailsObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationIllegalDrugDetail", content);
                ConsultationIllegalDrugDetailsResponse result = new ConsultationIllegalDrugDetailsResponse();
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
                return View("IllegalDrugDetailsResponse", result);

            }
        }

        [HttpGet]
        public async Task<ActionResult> EditIllegalDrugDetails(int userId, int consultationId, int consultationIllegalDrugDetailsId)
        {
            //set data for the surgery record edit
            var IllegalDrugDetailsModelObject = new IllegalDrugDetailsModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationIllegalDrugDetailList?consultationId=" + consultationId.ToString() + "&consultationIllegalDrugDetailsId=" + consultationIllegalDrugDetailsId.ToString());
                var ConsultationIllegalDrugDetailsResponseObject = JsonConvert.DeserializeObject<ConsultationIllegalDrugDetailsResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationIllegalDrugDetailsResponseObject.ConsultationIllegalDrugDetailsDisplayList != null
                    && ConsultationIllegalDrugDetailsResponseObject.ConsultationIllegalDrugDetailsDisplayList.Count > 0)
                {
                    var consIllegalDrugDetailsDisplay = ConsultationIllegalDrugDetailsResponseObject.ConsultationIllegalDrugDetailsDisplayList.First();
                    IllegalDrugDetailsModelObject.ConsultationIllegalDrugDetailsObject = new ConsultationIllegalDrugDetails
                    {
                        Id = consIllegalDrugDetailsDisplay.Id,
                        ConsultationId = consIllegalDrugDetailsDisplay.ConsultationId,
                        ConsumeDrugs = consIllegalDrugDetailsDisplay.ConsumeDrugs,
                        IllegalDrugsID = consIllegalDrugDetailsDisplay.IllegalDrugsID,
                        IllegalDrugDesc = consIllegalDrugDetailsDisplay.IllegalDrugDesc,
                        OtherDescription = consIllegalDrugDetailsDisplay.OtherDescription,
                        //Frequency = consIllegalDrugDetailsDisplay.Frequency,
                        PerFrequency = consIllegalDrugDetailsDisplay.PerFrequency,
                        ModifiedBy = userId,
                        Active = consIllegalDrugDetailsDisplay.Active
                    };
                }
                Res = await client.GetAsync("api/ConsultationAPI/GetIllegalDrugs?isActive=true&IllegalDrug=");
                IllegalDrugDetailsModelObject.IllegalDrugDetails = JsonConvert.DeserializeObject<List<IllegalDrugMaster>>(Res.Content.ReadAsStringAsync().Result);
            }

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Daily",
                Value = "Daily"
            });
            items.Add(new SelectListItem
            {
                Text = "Weekly",
                Value = "Weekly"
            });
            items.Add(new SelectListItem
            {
                Text = "Socially",
                Value = "Socially"
            });
            ViewBag.DDLItems = items;

            return View(IllegalDrugDetailsModelObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationSDDHabits(SDDHabitsModel sddHabits)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(sddHabits.ConsultationSDDHabitsObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationSDDHabits", content);
                ConsultationSDDHabitsResponse result = new ConsultationSDDHabitsResponse();
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
                return View("SDDHabitsResponse", result);

            }
        }

        [HttpGet]
        public async Task<ActionResult> EditSDDHabits(int userId, int consultationId, int consultationSDDHabitsId)
        {
            //set data for the surgery record edit
            var SDDHabitsModelObject = new SDDHabitsModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationSDDHabitsList?consultationId=" + consultationId.ToString() + "&consultationSDDHabitsId=" + consultationSDDHabitsId.ToString());
                var ConsultationSDDHabitsResponseObject = JsonConvert.DeserializeObject<ConsultationSDDHabitsResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationSDDHabitsResponseObject.ConsultationSDDHabitsDisplayList != null
                    && ConsultationSDDHabitsResponseObject.ConsultationSDDHabitsDisplayList.Count > 0)
                {
                    var consSDDHabitsDisplay = ConsultationSDDHabitsResponseObject.ConsultationSDDHabitsDisplayList.First();
                    SDDHabitsModelObject.ConsultationSDDHabitsObject = new ConsultationSDDHabits
                    {
                        Id = consSDDHabitsDisplay.Id,
                        ConsultationId = consSDDHabitsDisplay.ConsultationId,
                        DoSmoke = consSDDHabitsDisplay.DoSmoke,
                        EverSmoked = consSDDHabitsDisplay.EverSmoked,
                        YearOfQuittingSmoking = consSDDHabitsDisplay.YearOfQuittingSmoking,
                        SmokingFreq = consSDDHabitsDisplay.SmokingFreq,
                        ModifiedBy = userId,
                        Active = consSDDHabitsDisplay.Active
                    };
                }
                Res = await client.GetAsync("api/ConsultationAPI/GetIllegalDrugs?isActive=true&IllegalDrug=");
            }

            return View(SDDHabitsModelObject);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationAllergy(AllergyModel allergy)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(allergy.ConsultationAllergyObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationAllergy", content);
                ConsultationAllergyResponse result = new ConsultationAllergyResponse();
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
                return View("AllergyResponse", result);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditAllergy(int userId, int consultationId, int consultationAllergyId)
        {
            //set data for the surgery record edit
            var AllergyModelObject = new AllergyModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationAllergyList?consultationId=" + consultationId.ToString() + "&consultationAllergyId=" + consultationAllergyId.ToString());
                var ConsultationAllergyResponseObject = JsonConvert.DeserializeObject<ConsultationAllergyResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationAllergyResponseObject.ConsultationAllergyList != null
                    && ConsultationAllergyResponseObject.ConsultationAllergyList.Count > 0)
                {
                    var consAllergyDisplay = ConsultationAllergyResponseObject.ConsultationAllergyList.First();
                    AllergyModelObject.ConsultationAllergyObject = new ConsultationAllergies
                    {
                        Id = consAllergyDisplay.Id,
                        ConsultationId = consAllergyDisplay.ConsultationId,
                        AllergyId = consAllergyDisplay.AllergyId,
                        OtherDescription = consAllergyDisplay.OtherDescription,
                        AllergyStartDate = consAllergyDisplay.AllergyStartDate,
                        Treatment = consAllergyDisplay.Treatment,
                        ModifiedBy = userId,
                        Active = consAllergyDisplay.Active
                    };
                }
                Res = await client.GetAsync("api/ConsultationAPI/GetAllergyList?isActive=true&allergyName=&searchTerm=");
                AllergyModelObject.AllergyList = JsonConvert.DeserializeObject<List<AllergyMaster>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(AllergyModelObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationFamilyHistory(FamilyHistoryModel familyHistory)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(familyHistory.ConsultationFamilyHistoryObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationFamilyHistory", content);
                ConsultationFamilyHistoryResponse result = new ConsultationFamilyHistoryResponse();
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
                return View("FamilyHistoryResponse", result);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditFamilyHistory(int userId, int consultationId, int consultationFamilyHistoryId, int? relationshipId, bool? excludeSelf)
        {
            //set data for the surgery record edit
            var FamilyHistoryModelObject = new FamilyHistoryModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationFamilyHistoryList?consultationId=" + consultationId.ToString() + "&relationshipId=&excludeSelf=&consultationFamilyHistoryId=" + consultationFamilyHistoryId.ToString());
                var ConsultationFamilyHistoryResponseObject = JsonConvert.DeserializeObject<ConsultationFamilyHistoryResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationFamilyHistoryResponseObject.ConsultationFamilyHistories != null
                    && ConsultationFamilyHistoryResponseObject.ConsultationFamilyHistories.Count > 0)
                {
                    var consFamilyHistoryDisplay = ConsultationFamilyHistoryResponseObject.ConsultationFamilyHistories.First();
                    FamilyHistoryModelObject.ConsultationFamilyHistoryObject = new ConsultationFamilyHistory
                    {
                        Id = consFamilyHistoryDisplay.Id,
                        ConsultationId = consFamilyHistoryDisplay.ConsultationId,
                        RelationshipId = consFamilyHistoryDisplay.RelationshipId,
                        HealthConditionId = consFamilyHistoryDisplay.HealthConditionId,
                        OtherHealthConditionDescription = consFamilyHistoryDisplay.OtherHealthConditionDescription,
                        CurrentAge = consFamilyHistoryDisplay.CurrentAge,
                        AgeOnConditionStart = consFamilyHistoryDisplay.AgeOnConditionStart,
                        IsAlive = consFamilyHistoryDisplay.IsAlive,
                        AgeOnDeath = consFamilyHistoryDisplay.AgeOnDeath,
                        CauseOfDeath = consFamilyHistoryDisplay.CauseOfDeath,
                        ModifiedBy = userId,
                        Active = consFamilyHistoryDisplay.Active
                    };
                }
                Res = await client.GetAsync("api/ConsultationAPI/GetHealthConditionList?isActive=true&healthConditionName=&searchTerm=");
                FamilyHistoryModelObject.HealthConditionList = JsonConvert.DeserializeObject<List<HealthConditionMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/SignUpAPI/GetRelationships?isActive=true&relationship=&excludeSelf=true");
                FamilyHistoryModelObject.RelationshipList = JsonConvert.DeserializeObject<List<RelationshipMaster>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(FamilyHistoryModelObject);
        }

        [HttpGet]
        public async Task<ActionResult> EditExistingCondition(int userId, int consultationId, int consultationFamilyHistoryId, 
            int? relationshipId, bool? excludeSelf)
        {
            //set data for the surgery record edit
            var ExistingConditionModelObject = new FamilyHistoryModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationFamilyHistoryList?consultationId=" + consultationId.ToString() + "&relationshipId=&excludeSelf="+excludeSelf.ToString()+"&consultationFamilyHistoryId=" + consultationFamilyHistoryId.ToString());
                var ConsultationExistingConditionResponseObject = JsonConvert.DeserializeObject<ConsultationFamilyHistoryResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationExistingConditionResponseObject.ConsultationFamilyHistories != null
                    && ConsultationExistingConditionResponseObject.ConsultationFamilyHistories.Count > 0)
                {
                    var consExistingConditionDisplay = ConsultationExistingConditionResponseObject.ConsultationFamilyHistories.First();
                    ExistingConditionModelObject.ConsultationFamilyHistoryObject = new ConsultationFamilyHistory
                    {
                        Id = consExistingConditionDisplay.Id,
                        ConsultationId = consExistingConditionDisplay.ConsultationId,
                        RelationshipId = consExistingConditionDisplay.RelationshipId,
                        HealthConditionId = consExistingConditionDisplay.HealthConditionId,
                        OtherHealthConditionDescription = consExistingConditionDisplay.OtherHealthConditionDescription,
                        ConditionStartDate = consExistingConditionDisplay.ConditionStartDate,
                        IsAlive = consExistingConditionDisplay.IsAlive,
                        ModifiedBy = userId,
                        Active = consExistingConditionDisplay.Active
                    };
                }
                Res = await client.GetAsync("api/ConsultationAPI/GetHealthConditionList?isActive=true&healthConditionName=&searchTerm=");
                ExistingConditionModelObject.HealthConditionList = JsonConvert.DeserializeObject<List<HealthConditionMaster>>(Res.Content.ReadAsStringAsync().Result);
                Res = await client.GetAsync("api/SignUpAPI/GetRelationships?isActive=true&relationship=self&excludeSelf=false");
                ExistingConditionModelObject.RelationshipList = JsonConvert.DeserializeObject<List<RelationshipMaster>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(ExistingConditionModelObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationOccupation(OccupationModel occupation)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(occupation.ConsultationOccupationObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationOccupation", content);
                ConsultationOccupationResponse result = new ConsultationOccupationResponse();
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
                return View("OccupationResponse", result);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditOccupation(int userId, int consultationId, int? consultationOccupationId)
        {
            //set data for the Occupation record edit
            var OccupationModelObject = new OccupationModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationOccupationList?consultationId=" + consultationId.ToString() + "&consultationOccupationId=" + consultationOccupationId.ToString());
                var ConsultationOccupationResponseObject = JsonConvert.DeserializeObject<ConsultationOccupationResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationOccupationResponseObject.ConsultationOccupationList != null
                    && ConsultationOccupationResponseObject.ConsultationOccupationList.Count > 0)
                {
                    var consOccupationDisplay = ConsultationOccupationResponseObject.ConsultationOccupationList.First();
                    OccupationModelObject.ConsultationOccupationObject = new ConsultationOccupation
                    {
                        Id = consOccupationDisplay.Id,
                        ConsultationId = consOccupationDisplay.ConsultationId,
                        OccupationId = consOccupationDisplay.OccupationId,
                        OtherDescription = consOccupationDisplay.OtherDescription,
                        ModifiedBy = userId,
                        Active = consOccupationDisplay.Active
                    };
                }
                Res = await client.GetAsync("api/ConsultationAPI/GetOccupationList?isActive=true&occupationName=&searchTerm=");
                OccupationModelObject.OccupationList = JsonConvert.DeserializeObject<List<OccupationMaster>>(Res.Content.ReadAsStringAsync().Result);
            }
            return View(OccupationModelObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationBloodPressureReading(BloodPressureReadingModel bloodPressureReading)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(bloodPressureReading.ConsultationBloodPressureReadingObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationBloodPressureReading", content);
                ConsultationBloodPressureReadingResponse result = new ConsultationBloodPressureReadingResponse();
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
                return View("BloodPressureReadingResponse", result);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditBloodPressureReading(int userId, int consultationId, int? consultationBloodPressureReadingId)
        {
            //set data for the BloodPressureReading record edit
            var BloodPressureReadingModelObject = new BloodPressureReadingModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationBloodPressureReadingList?consultationId=" + consultationId.ToString() + "&consultationBloodPressureReadingId=" + consultationBloodPressureReadingId.ToString());
                var ConsultationBloodPressureReadingResponseObject = JsonConvert.DeserializeObject<ConsultationBloodPressureReadingResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationBloodPressureReadingResponseObject.ConsultationBloodPressureReadingList != null
                    && ConsultationBloodPressureReadingResponseObject.ConsultationBloodPressureReadingList.Count > 0)
                {
                    var consBloodPressureReading = ConsultationBloodPressureReadingResponseObject.ConsultationBloodPressureReadingList.First();
                    BloodPressureReadingModelObject.ConsultationBloodPressureReadingObject = new ConsultationBloodPressureReading
                    {
                        Id = consBloodPressureReading.Id,
                        ConsultationId = consBloodPressureReading.ConsultationId,
                        Systolic = consBloodPressureReading.Systolic,
                        Diastolic = consBloodPressureReading.Diastolic,
                        Timestamp = consBloodPressureReading.Timestamp,
                        ModifiedBy = userId,
                        Active = consBloodPressureReading.Active
                    };
                }
            }
            return View(BloodPressureReadingModelObject);
        }

        [HttpGet]
        public async Task<ActionResult> GetDrugSubTypeList(int drugTypeId)
        {
            List<DrugSubTypeMaster> result = new List<DrugSubTypeMaster>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetDrugSubTypeList?drugTypeId=" + drugTypeId.ToString() + "&isActive=true&drugSubTypeName=&searchTerm=");
                result = JsonConvert.DeserializeObject<List<DrugSubTypeMaster>>(Res.Content.ReadAsStringAsync().Result);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertUpdateConsultationMedication(MedicationModel medication)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(medication.ConsultationMedicationObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/ConsultationAPI/InsertUpdateConsultationMedication", content);
                ConsultationMedicationResponse result = new ConsultationMedicationResponse();
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
                return View("MedicationResponse", result);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditMedication(int userId, int consultationId, int? consultationMedicationId)
        {
            //set data for the Medicatio record edit
            var MedicationModelObject = new MedicationModel
            {
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ConsultationAPI/GetConsultationMedicationList?consultationId=" + consultationId.ToString() + "&consultationMedicationId=" + consultationMedicationId.ToString());
                var ConsultationMedicationResponseObject = JsonConvert.DeserializeObject<ConsultationMedicationResponse>(Res.Content.ReadAsStringAsync().Result);

                if (ConsultationMedicationResponseObject.ConsultationMedicationList != null
                    && ConsultationMedicationResponseObject.ConsultationMedicationList.Count > 0)
                {
                    var consMedication = ConsultationMedicationResponseObject.ConsultationMedicationList.First();
                    MedicationModelObject.ConsultationMedicationObject = new ConsultationMedications
                    {
                        Id = consMedication.Id,
                        ConsultationId = consMedication.ConsultationId,
                        DrugTypeId = consMedication.DrugTypeId,
                        DrugSubTypeId = consMedication.DrugSubTypeId,
                        DrugBrandId = consMedication.DrugBrandId,
                        DrugBrandOtherDescription = consMedication.DrugBrandOtherDescription,
                        DrugChemicalId = consMedication.DrugChemicalId,
                        DrugChemicalOtherDescription = consMedication.DrugChemicalOtherDescription,
                        DrugFrequencyId = consMedication.DrugFrequencyId,
                        DrugStartDate = consMedication.DrugStartDate,
                        DrugEndDate = consMedication.DrugEndDate,
                        DrugDosage = consMedication.DrugDosage,
                        ModifiedBy = userId,                        
                        Active = consMedication.Active
                    };
                    Res = await client.GetAsync("api/ConsultationAPI/GetDrugTypeList?isActive=true&drugType=&searchTerm=");
                    MedicationModelObject.DrugTypeList = JsonConvert.DeserializeObject<List<DrugTypeMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/ConsultationAPI/GetSubDrugTypeList?drugTypeId=" + MedicationModelObject.ConsultationMedicationObject.DrugTypeId + "isActive=true&drugSubTypeName=&searchTerm=");
                    MedicationModelObject.DrugSubTypeList = JsonConvert.DeserializeObject<List<DrugSubTypeMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/ConsultationAPI/GetDrugBrandList?isActive=true&drugBrandName=&searchTerm=");
                    MedicationModelObject.DrugBrandList = JsonConvert.DeserializeObject<List<DrugBrandMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/ConsultationAPI/GetDrugChemicalList?isActive=true&drugChemicalName=&searchTerm=");
                    MedicationModelObject.DrugChemicalList = JsonConvert.DeserializeObject<List<DrugChemicalMaster>>(Res.Content.ReadAsStringAsync().Result);
                    Res = await client.GetAsync("api/ConsultationAPI/GetDrugFrequencyList?isActive=true&drugFrequencyName=&searchTerm=");
                    MedicationModelObject.DrugFrequencyList = JsonConvert.DeserializeObject<List<DrugFrequencyMaster>>(Res.Content.ReadAsStringAsync().Result);
                }

            }
            return View(MedicationModelObject);
        }

        #endregion
    }
}