﻿using Ninject;
using OMC.BL.Interface;
using OMC.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace OMCApi.Areas.Consultation.Controllers
{
    /// <summary>
    /// This controller hosts all APIs related to Consultation functionality
    /// </summary>
    [RoutePrefix("api/ConsultationAPI")]
    public class ConsultationAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor
        /// <summary>
        /// Controller for the Consultation related APIs
        /// </summary>
        public ConsultationAPIController()
        {
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.ConsultationModule());
        }

        #endregion

        #region Actions
        /// <summary>
        /// Gets the list of Consultation statuses available in the system
        /// ~Sprint-1~
        /// OMC‌-145
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationStatuses
        [HttpGet]
        [Route("GetConsultationStatuses")]
        public List<ConsultationStatus> GetConsultationStatuses(bool? isActive, string description)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetConsultationStatuses(isActive, description);
        }

        /// <summary>
        /// Gets the list of Cancer stages available in the system
        /// ~Sprint-2~
        /// OMC-116
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="cancerStageName"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetCancerStages
        [HttpGet]
        [Route("GetCancerStages")]
        public List<CancerStageMaster> GetCancerStages(bool? isActive, string cancerStageName)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetCancerStages(isActive, cancerStageName);
        }

        /// <summary>
        /// Gets the list of Surgeries available in the system
        /// ~Sprint-2~
        /// OMC-116
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="surgeryName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetSurgeryList
        [HttpGet]
        [Route("GetSurgeryList")]
        public List<SurgeryMaster> GetSurgeryList(bool? isActive, string surgeryName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetSurgeryList(isActive, surgeryName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Allergies available in the system
        /// ~Sprint-2~
        /// OMC-114
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="allergyName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetAllergyList
        [HttpGet]
        [Route("GetAllergyList")]
        public List<AllergyMaster> GetAllergyList(bool? isActive, string allergyName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetAllergyList(isActive, allergyName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Health conditions available in the system
        /// ~Sprint-2~
        /// OMC-118, OMC-115
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="healthConditionName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetHealthConditionList
        [HttpGet]
        [Route("GetHealthConditionList")]
        public List<HealthConditionMaster> GetHealthConditionList(bool? isActive, string healthConditionName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetHealthConditionList(isActive, healthConditionName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Doctors available in the system
        /// ~Sprint-1~
        /// OMC‌-145
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetDoctors
        [HttpGet]
        [Route("GetDoctors")]
        public List<UserDetail> GetDoctors(bool? isActive, string userRole)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetUserList(isActive, userRole);
        }

        /// <summary>
        /// Gets the list of Illegal drugs available in the system
        /// ~Sprint-2~
        /// OMC-162
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="IllegalDrug"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetIllegalDrugs
        [HttpGet]
        [Route("GetIllegalDrugs")]
        public List<IllegalDrugMaster> GetIllegalDrugs(bool? isActive, string IllegalDrug)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetIllegalDrugs(isActive, IllegalDrug);
        }

        /// <summary>
        /// Gets the list of Menstrual Symptoms available in the system
        /// ~Sprint-2~
        /// OMC-121
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="MenstrualSymptoms"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetMenstrualSymptoms
        [HttpGet]
        [Route("GetMenstrualSymptoms")]
        public List<MenstrualSymptomsMaster> GetMenstrualSymptoms(bool? isActive, string MenstrualSymptoms)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetMenstrualSymptoms(isActive, MenstrualSymptoms);
        }

        /// <summary>
        /// Gets the list of Drug types available in the system
        /// ~Sprint-2~
        /// OMC-113
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="drugType"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetDrugTypeList
        [HttpGet]
        [Route("GetDrugTypeList")]
        public List<DrugTypeMaster> GetDrugTypeList(bool? isActive, string drugType, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugTypeList(isActive, drugType, searchTerm);
        }

        /// <summary>
        /// Gets the list of Drug sub types available for the selected drug type in the system
        /// ~Sprint-2~
        /// OMC-113
        /// </summary>
        /// <param name="drugTypeId"></param>
        /// <param name="isActive"></param>
        /// <param name="drugSubTypeName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetDrugSubTypeList
        [HttpGet]
        [Route("GetDrugSubTypeList")]
        public List<DrugSubTypeMaster> GetDrugSubTypeList(int drugTypeId, bool? isActive, string drugSubTypeName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugSubTypeList(drugTypeId, isActive, drugSubTypeName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Drug brands available in the system
        /// ~Sprint-2~
        /// OMC-113
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="drugBrandName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetDrugBrandList
        [HttpGet]
        [Route("GetDrugBrandList")]
        public List<DrugBrandMaster> GetDrugBrandList(bool? isActive, string drugBrandName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugBrandList(isActive, drugBrandName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Drug chemicals available in the system
        /// ~Sprint-2~
        /// OMC-113
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="drugChemicalName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetDrugChemicalList
        [HttpGet]
        [Route("GetDrugChemicalList")]
        public List<DrugChemicalMaster> GetDrugChemicalList(bool? isActive, string drugChemicalName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugChemicalList(isActive, drugChemicalName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Drug frequencies available in the system
        /// ~Sprint-2~
        /// OMC-113
        /// OMC-162
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="drugFrequencyName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetDrugFrequencyList
        [HttpGet]
        [Route("GetDrugFrequencyList")]
        public List<DrugFrequencyMaster> GetDrugFrequencyList(bool? isActive, string drugFrequencyName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugFrequencyList(isActive, drugFrequencyName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Drug units available in the system
        /// ~Sprint-2~
        /// OMC-113
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="drugUnitName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetDrugUnitList
        [HttpGet]
        [Route("GetDrugUnitList")]
        public List<UnitMaster> GetDrugUnitList(bool? isActive, string drugUnitName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugUnitList(isActive, drugUnitName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Occupations available in the system
        /// ~Sprint-2~
        /// OMC-161
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="occupationName"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetOccupationList
        [HttpGet]
        [Route("GetOccupationList")]
        public List<OccupationMaster> GetOccupationList(bool? isActive, string occupationName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetOccupationList(isActive, occupationName, searchTerm);
        }

        /// <summary>
        /// Gets the list of Packages available in the system 
        /// ~Sprint-2~
        /// OMC-168
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetPackageList
        [HttpGet]
        [Route("GetPackageList")]
        public List<PackageMaster> GetPackageList(bool? isActive, int? packageId)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetPackageList(isActive, packageId);
        }

        /// <summary>
        /// API to handle the unregistered patient enquiry for doctors
        /// ~Sprint-1~
        /// OMC-4
        /// </summary>
        /// <param name="enquiry"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetUnregisteredPatientEnquiry
        [HttpPost]
        [Route("GetUnregisteredPatientEnquiry")]
        public PatientEnquiryResponse GetUnregisteredPatientEnquiry([FromBody]OMC.Models.PatientEnquiry enquiry)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.UnregisteredPatientEnquiry(enquiry);
        }

        /// <summary>
        /// Gets the list of consultation for the user as per the role of the logged in user (patient/doctor)
        /// ~Sprint-1~
        /// OMC‌-52
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationList
        [HttpGet]
        [Route("GetConsultationList")]
        public List<ConsultationDisplay> GetConsultationList(int userId, string userRole)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationList(userId, userRole);
        }

        /// <summary>
        /// Gets the list of conversations for a particularr consultation as per the role of the logged in user (patient/doctor)
        /// ~Sprint-1~
        /// OMC‌-95
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="userId"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationList
        [HttpGet]
        [Route("GetConversationList")]
        public ConversationResponse GetConversationList(int consultationId, int userId, string userRole)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConversationList(consultationId, userId, userRole);
        }

        /// <summary>
        /// API to create the new consultation record
        /// ~Sprint-1~
        /// OMC‌-145
        /// </summary>
        /// <param name="consultation"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/CreateConsultation
        [HttpPost]
        [Route("CreateConsultation")]
        public IHttpActionResult CreateConsultation([FromBody]OMC.Models.Consultation consultation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultation.Active = true;
            var ConsultationResult = ConsultationBLObj.InitiateConsultation(consultation);
            return Ok(ConsultationResult.Message); 
        }

        /// <summary>
        /// API to create the new conversation record 
        /// ~Sprint-1~
        /// OMC-54
        /// </summary>
        /// <param name="conversation"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/CreateConsultation
        [HttpPost]
        [Route("CreateConversation")]
        public IHttpActionResult CreateConversation([FromBody]Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            conversation.Active = true;
            var ConversationResult = ConsultationBLObj.RecordConversation(conversation);
            return Ok(ConversationResult.Message);
        }

        /// <summary>
        /// API to handle create/edit for the consultation report record
        /// ~Sprint-2~
        /// OMC‌-146
        /// </summary>
        /// <param name="consultationReport"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationReport
        [HttpPost]
        [Route("InsertUpdateConsultationReport")]
        public IHttpActionResult InsertUpdateConsultationReport([FromBody]ConsultationReports consultationReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationReport.Active = true;
            var ConversationResult = ConsultationBLObj.InsertUpdateConsultationReport(consultationReport);
            return Ok(ConversationResult.Message);
        }

        /// <summary>
        /// Gets the list of reports/specific report for the consultation record
        /// ~Sprint-2~
        /// OMC-146
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationReportId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationReportList
        [HttpGet]
        [Route("GetConsultationReportList")]
        public ConsultationReportResponse GetConsultationReportList(int consultationId, int? consultationReportId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationReportList(consultationId, consultationReportId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation surgery record
        /// ~Sprint-2~
        /// OMC-116
        /// </summary>
        /// <param name="consultationSurgery"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationSurgery
        [HttpPost]
        [Route("InsertUpdateConsultationSurgery")]
        public IHttpActionResult InsertUpdateConsultationSurgery([FromBody]ConsultationSurgeries consultationSurgery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationSurgery.Active = true;
            var ConversationResult = ConsultationBLObj.InsertUpdateConsultationSurgery(consultationSurgery);
            return Ok(ConversationResult.Message);
        }

        /// <summary>
        /// Gets the list of surgery records/specific surgery record for the consultation record
        /// ~Sprint-2~
        /// OMC-116
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationSurgeryId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationSurgeryList
        [HttpGet]
        [Route("GetConsultationSurgeryList")]
        public ConsultationSurgeryResponse GetConsultationSurgeryList(int consultationId, int? consultationSurgeryId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationSurgeryList(consultationId, consultationSurgeryId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation cancer treatment record
        /// ~Sprint-2~
        /// OMC-116
        /// </summary>
        /// <param name="consultationCancerTreatment"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationCancerTreatment
        [HttpPost]
        [Route("InsertUpdateConsultationCancerTreatment")]
        public IHttpActionResult InsertUpdateConsultationCancerTreatment([FromBody]ConsultationCancerTreatments consultationCancerTreatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationCancerTreatment.Active = true;
            var consultationCancerTreatmentResult = ConsultationBLObj.InsertUpdateConsultationCancerTreatment(consultationCancerTreatment);
            return Ok(consultationCancerTreatmentResult.Message);
        }

        /// <summary>
        /// Gets the list of cancer treatment records/specific cancer treatment record for the consultation record
        /// ~Sprint-2~
        /// OMC-116
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationCancerTreatmentId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationCancerTreatmentList
        [HttpGet]
        [Route("GetConsultationCancerTreatmentList")]
        public ConsultationCancerTreatmentResponse GetConsultationCancerTreatmentList(int consultationId, int? consultationCancerTreatmentId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationCancerTreatmentList(consultationId, consultationCancerTreatmentId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation illegal drug record
        /// ~Sprint-2~
        /// OMC-162
        /// </summary>
        /// <param name="consultationIllegalDrugDetails"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationIllegalDrugDetail
        [HttpPost]
        [Route("InsertUpdateConsultationIllegalDrugDetail")]
        public IHttpActionResult InsertUpdateConsultationIllegalDrugDetail([FromBody]ConsultationIllegalDrugDetails consultationIllegalDrugDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationIllegalDrugDetails.Active = true;
            var ConversationResult = ConsultationBLObj.InsertUpdateConsultationIllegalDrugDetail(consultationIllegalDrugDetails);
            return Ok(ConversationResult.Message);
        }

        /// <summary>
        /// Gets the list of illegal drug records/specific illegal drug record for the consultation record
        /// ~Sprint-2~
        /// OMC-162
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationIllegalDrugDetailsId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationIllegalDrugDetailList
        [HttpGet]
        [Route("GetConsultationIllegalDrugDetailList")]
        public ConsultationIllegalDrugDetailsResponse GetConsultationIllegalDrugDetailList(int consultationId, int? consultationIllegalDrugDetailsId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationIllegalDrugDetailList(consultationId, consultationIllegalDrugDetailsId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation pregnancy detail record
        /// ~Sprint-2~
        /// OMC-121
        /// </summary>
        /// <param name="consultationPregnancyDetails"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationPregnancyDetail
        [HttpPost]
        [Route("InsertUpdateConsultationPregnancyDetail")]
        public IHttpActionResult InsertUpdateConsultationPregnancyDetail([FromBody]ConsultationPregnancyDetails consultationPregnancyDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationPregnancyDetails.Active = true;
            var ConversationResult = ConsultationBLObj.InsertUpdateConsultationPregnancyDetail(consultationPregnancyDetails);
            return Ok(ConversationResult.Message);
        }

        /// <summary>
        /// Gets the list of pregnancy detail records/specific pregnancy detail record for the consultation record
        /// ~Sprint-2~
        /// OMC-121
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationPregnancyDetailsId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationPregnancyDetailsList
        [HttpGet]
        [Route("GetConsultationPregnancyDetailsList")]
        public ConsultationPregnancyDetailsResponse GetConsultationPregnancyDetailsList(int consultationId, int? consultationPregnancyDetailsId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationPregnancyDetailsList(consultationId, consultationPregnancyDetailsId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation previous pregnancy detail record
        /// ~Sprint-2~
        /// OMC-121
        /// </summary>
        /// <param name="consultationPreviousPregnancyDetails"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationPreviousPregnancyDetail
        [HttpPost]
        [Route("InsertUpdateConsultationPreviousPregnancyDetail")]
        public IHttpActionResult InsertUpdateConsultationPreviousPregnancyDetail([FromBody]ConsultationPreviousPregnancyDetails consultationPreviousPregnancyDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationPreviousPregnancyDetails.Active = true;
            var ConversationResult = ConsultationBLObj.InsertUpdateConsultationPreviousPregnancyDetail(consultationPreviousPregnancyDetails);
            return Ok(ConversationResult.Message);
        }

        /// <summary>
        /// Gets the list of previous pregnancy detail records/specific previous pregnancy detail record for the consultation record
        /// ~Sprint-2~
        /// OMC-121
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationPreviousPregnancyDetailsId"></param>
        /// <param name="CurrentPregnancyID"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationPreviousPregnancyDetailsList
        [HttpGet]
        [Route("GetConsultationPreviousPregnancyDetailsList")]
        public ConsultationPreviousPregnancyDetailsResponse GetConsultationPreviousPregnancyDetailsList(int consultationId, int? consultationPreviousPregnancyDetailsId, int? CurrentPregnancyID)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationPreviousPregnancyDetailsList(consultationId, consultationPreviousPregnancyDetailsId, CurrentPregnancyID);
        }

        /// <summary>
        /// API to handle create/edit for the consultation smoking, drinking and drug habit record
        /// ~Sprint-2~
        /// OMC-120
        /// </summary>
        /// <param name="consultationSDDHabits"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationSDDHabits
        [HttpPost]
        [Route("InsertUpdateConsultationSDDHabits")]
        public IHttpActionResult InsertUpdateConsultationSDDHabits([FromBody]ConsultationSDDHabits consultationSDDHabits)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationSDDHabits.Active = true;
            var ConversationResult = ConsultationBLObj.InsertUpdateConsultationSDDHabits(consultationSDDHabits);
            return Ok(ConversationResult.Message);
        }

        /// <summary>
        /// Gets the list of smoking, drinking and drug habit records/specific smoking, drinking and drug habit record for the consultation record
        /// ~Sprint-2~
        /// OMC-120
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationSDDHabitsId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationSDDHabitsList
        [HttpGet]
        [Route("GetConsultationSDDHabitsList")]
        public ConsultationSDDHabitsResponse GetConsultationSDDHabitsList(int consultationId, int? consultationSDDHabitsId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationSDDHabitsList(consultationId, consultationSDDHabitsId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation allergy record
        /// ~Sprint-2~
        /// OMC-114
        /// </summary>
        /// <param name="consultationAllergy"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationAllergy
        [HttpPost]
        [Route("InsertUpdateConsultationAllergy")]
        public IHttpActionResult InsertUpdateConsultationAllergy([FromBody]ConsultationAllergies consultationAllergy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationAllergy.Active = true;
            var consultationAllergyResult = ConsultationBLObj.InsertUpdateConsultationAllergy(consultationAllergy);
            return Ok(consultationAllergyResult.Message);
        }

        /// <summary>
        /// Gets the list of allergy records/specific allergy record for the consultation record
        /// ~Sprint-2~
        /// OMC-114
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationAllergyId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationAllergyList
        [HttpGet]
        [Route("GetConsultationAllergyList")]
        public ConsultationAllergyResponse GetConsultationAllergyList(int consultationId, int? consultationAllergyId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationAllergyList(consultationId, consultationAllergyId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation family history record
        /// ~Sprint-2~
        /// OMC-118, OMC-115
        /// </summary>
        /// <param name="consultationFamilyHistory"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationFamilyHistory
        [HttpPost]
        [Route("InsertUpdateConsultationFamilyHistory")]
        public IHttpActionResult InsertUpdateConsultationFamilyHistory([FromBody]ConsultationFamilyHistory consultationFamilyHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationFamilyHistory.Active = true;
            var consultationFamilyHistoryResult = ConsultationBLObj.InsertUpdateConsultationFamilyHistory(consultationFamilyHistory);
            return Ok(consultationFamilyHistoryResult.Message);
        }

        /// <summary>
        /// Gets the list of family history records/specific family history record for the consultation record
        /// ~Sprint-2~
        /// OMC-118, OMC-115
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationFamilyHistoryId"></param>
        /// <param name="relationshipId"></param>
        /// <param name="excludeSelf"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationFamilyHistoryList
        [HttpGet]
        [Route("GetConsultationFamilyHistoryList")]
        public ConsultationFamilyHistoryResponse GetConsultationFamilyHistoryList(int consultationId, int? consultationFamilyHistoryId
            , int? relationshipId, bool? excludeSelf)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationFamilyHistoryList(consultationId, consultationFamilyHistoryId, relationshipId, excludeSelf);
        }

        /// <summary>
        /// API to handle create/edit for the consultation occupation record
        /// ~Sprint-2~
        /// OMC-161
        /// </summary>
        /// <param name="consultationOccupation"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationOccupation
        [HttpPost]
        [Route("InsertUpdateConsultationOccupation")]
        public IHttpActionResult InsertUpdateConsultationOccupation(ConsultationOccupation consultationOccupation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationOccupation.Active = true;
            var consultationOccupationResult = ConsultationBLObj.InsertUpdateConsultationOccupation(consultationOccupation);
            return Ok(consultationOccupationResult.Message);
        }

        /// <summary>
        /// Gets the list of occupations/specific occupation record for the consultation record
        /// ~Sprint-2~
        /// OMC-161
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationOccupationId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationOccupationList
        [HttpGet]
        [Route("GetConsultationOccupationList")]
        public ConsultationOccupationResponse GetConsultationOccupationList(int consultationId, int? consultationOccupationId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationOccupationList(consultationId, consultationOccupationId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation blood pressure reading record
        /// ~Sprint-2~
        /// OMC-158
        /// </summary>
        /// <param name="consultationBloodPressureReading"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationBloodPressureReading
        [HttpPost]
        [Route("InsertUpdateConsultationBloodPressureReading")]
        public IHttpActionResult InsertUpdateConsultationBloodPressureReading(ConsultationBloodPressureReading consultationBloodPressureReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationBloodPressureReading.Active = true;
            var consultationBloodPressureReadingResult = ConsultationBLObj.InsertUpdateConsultationBloodPressureReading(consultationBloodPressureReading);
            return Ok(consultationBloodPressureReadingResult.Message);
        }

        /// <summary>
        /// Gets the list of blood pressure readings/specific blood pressure reading record for the consultation record
        /// ~Sprint-2~
        /// OMC-158
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationBloodPressureReadingId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationBloodPressureReadingList
        [HttpGet]
        [Route("GetConsultationBloodPressureReadingList")]
        public ConsultationBloodPressureReadingResponse GetConsultationBloodPressureReadingList(int consultationId, int? consultationBloodPressureReadingId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationBloodPressureReadingList(consultationId, consultationBloodPressureReadingId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation medication record
        /// ~Sprint-2~
        /// OMC-113
        /// </summary>
        /// <param name="consultationMedication"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationMedication
        [HttpPost]
        [Route("InsertUpdateConsultationMedication")]
        public IHttpActionResult InsertUpdateConsultationMedication(ConsultationMedications consultationMedication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationMedication.Active = true;
            var consultationMedicationResult = ConsultationBLObj.InsertUpdateConsultationMedication(consultationMedication);
            return Ok(consultationMedicationResult.Message);
        }

        /// <summary>
        /// Gets the list of medications/specific medication record for the consultation record
        /// ~Sprint-2~
        /// OMC-113
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationMedicationId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationMedicationList
        [HttpGet]
        [Route("GetConsultationMedicationList")]
        public ConsultationMedicationResponse GetConsultationMedicationList(int consultationId, int? consultationMedicationId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationMedicationList(consultationId, consultationMedicationId);
        }

        /// <summary>
        /// Gets the list of Consultation Subjective records/specific Consultation Subjective record for the consultation record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationSubjectiveId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationSubjectiveList
        [HttpGet]
        [Route("GetConsultationSubjectiveList")]
        public ConsultationSubjectiveResponse GetConsultationSubjectiveList(int consultationId
        , int? consultationSubjectiveId)     
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationSubjectiveList(consultationId, consultationSubjectiveId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation Subjective record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationSubjectives"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationSubjectives
        [HttpPost]
        [Route("InsertUpdateConsultationSubjectives")]
        public IHttpActionResult InsertUpdateConsultationSubjectives(ConsultationSubjectives consultationSubjectives)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationSubjectives.Active = true;
            var consultationSubjectiveResult = ConsultationBLObj.InsertUpdateConsultationSubjectives(consultationSubjectives);
            return Ok(consultationSubjectiveResult.Message);
        }

        /// <summary>
        /// Gets the list of Consultation Objective records/specific Consultation Objective record for the consultation record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationObjectiveId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationObjectiveList
        [HttpGet]
        [Route("GetConsultationObjectiveList")]
        public ConsultationObjectiveResponse GetConsultationObjectiveList(int consultationId
        , int? consultationObjectiveId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationObjectiveList(consultationId, consultationObjectiveId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation Objective record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationObjectives"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationObjectives
        [HttpPost]
        [Route("InsertUpdateConsultationObjectives")]
        public IHttpActionResult InsertUpdateConsultationObjectives(ConsultationObjectives consultationObjectives)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationObjectives.Active = true;
            var consultationObjectiveResult = ConsultationBLObj.InsertUpdateConsultationObjectives(consultationObjectives);
            return Ok(consultationObjectiveResult.Message);
        }

        /// <summary>
        /// Gets the list of Consultation Assesment records/specific Consultation Assesment record for the consultation record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationAssesmentId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationAssesmentList
        [HttpGet]
        [Route("GetConsultationAssesmentList")]
        public ConsultationAssesmentResponse GetConsultationAssesmentList(int consultationId
        , int? consultationAssesmentId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationAssesmentList(consultationId, consultationAssesmentId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation Assesment record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationAssesments"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationAssesments
        [HttpPost]
        [Route("InsertUpdateConsultationAssesments")]
        public IHttpActionResult InsertUpdateConsultationAssesments(ConsultationAssesments consultationAssesments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationAssesments.Active = true;
            var consultationAssesmentResult = ConsultationBLObj.InsertUpdateConsultationAssesments(consultationAssesments);
            return Ok(consultationAssesmentResult.Message);
        }

        /// <summary>
        /// Gets the list of Consultation Plan records/specific Consultation Plan record for the consultation record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationId"></param>
        /// <param name="consultationPlanId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationPlanList
        [HttpGet]
        [Route("GetConsultationPlanList")]
        public ConsultationPlanResponse GetConsultationPlanList(int consultationId
        , int? consultationPlanId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationPlanList(consultationId, consultationPlanId);
        }

        /// <summary>
        /// API to handle create/edit for the consultation Plan record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationPlans"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationPlans
        [HttpPost]
        [Route("InsertUpdateConsultationPlans")]
        public IHttpActionResult InsertUpdateConsultationPlans(ConsultationPlans consultationPlans)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationPlans.Active = true;
            var consultationPlanResult = ConsultationBLObj.InsertUpdateConsultationPlans(consultationPlans);
            return Ok(consultationPlanResult.Message);
        }

        /// <summary>
        /// Gets the list of Consultation Subjective Note records/specific Consultation Subjective Note record for the consultation record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationSubjectiveId"></param>
        /// <param name="consultationSubjectiveNoteId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationSubjectiveNoteList
        [HttpGet]
        [Route("GetConsultationSubjectiveNoteList")]
        public ConsultationSubjectiveNoteResponse GetConsultationSubjectiveNoteList(int consultationSubjectiveId
            , int? consultationSubjectiveNoteId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationSubjectiveNoteList(consultationSubjectiveId, consultationSubjectiveNoteId);
        }

        /// <summary>
        /// Gets the list of Consultation Subjective Note records/specific Consultation Subjective Note record for the consultation record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationSubjectiveNotes"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationSubjectiveNotes
        [HttpPost]
        [Route("InsertUpdateConsultationSubjectiveNotes")]
        public ConsultationSubjectiveNoteResponse InsertUpdateConsultationSubjectiveNotes
            (ConsultationSubjectiveNotes consultationSubjectiveNotes)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationSubjectiveNotes.Active = true;
            return ConsultationBLObj.InsertUpdateConsultationSubjectiveNotes(consultationSubjectiveNotes);
        }

        /// <summary>
        /// Gets the list of Consultation Objective Note records/specific Consultation Objective Note record for the consultation record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationObjectiveId"></param>
        /// <param name="consultationObjectiveNoteId"></param>
        /// <returns></returns>
        // Get: api/ConsultationAPI/GetConsultationObjectiveNoteList
        [HttpGet]
        [Route("GetConsultationObjectiveNoteList")]
        public ConsultationObjectiveNoteResponse GetConsultationObjectiveNoteList(int consultationObjectiveId
            , int? consultationObjectiveNoteId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationObjectiveNoteList(consultationObjectiveId, consultationObjectiveNoteId);
        }

        /// <summary>
        /// Gets the list of Consultation Objective Note records/specific Consultation Objective Note record for the consultation record
        /// ~Sprint-3~
        /// OMC-144
        /// </summary>
        /// <param name="consultationObjectiveNotes"></param>
        /// <returns></returns>
        // POST: api/ConsultationAPI/InsertUpdateConsultationObjectiveNotes
        [HttpPost]
        [Route("InsertUpdateConsultationObjectiveNotes")]
        public ConsultationObjectiveNoteResponse InsertUpdateConsultationObjectiveNotes
            (ConsultationObjectiveNotes consultationObjectiveNotes)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            consultationObjectiveNotes.Active = true;
            return ConsultationBLObj.InsertUpdateConsultationObjectiveNotes(consultationObjectiveNotes);
        }
        #endregion
    }
}