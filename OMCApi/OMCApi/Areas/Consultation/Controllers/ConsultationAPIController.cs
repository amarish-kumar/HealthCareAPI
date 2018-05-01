using Ninject;
using OMC.BL.Interface;
using OMC.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace OMCApi.Areas.Consultation.Controllers
{
    [RoutePrefix("api/ConsultationAPI")]
    public class ConsultationAPIController : ApiController
    {
        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public ConsultationAPIController()
        {
            //_Kernel = new StandardKernel(new OMC.Modules.SignInModule());
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.ConsultationModule());
        }

        #endregion

        #region Actions
        // Get: api/ConsultationAPI/GetConsultationStatuses
        [HttpGet]
        [Route("GetConsultationStatuses")]
        public List<ConsultationStatus> GetConsultationStatuses(bool? isActive, string description)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetConsultationStatuses(isActive, description);
        }

        // Get: api/ConsultationAPI/GetCancerStages
        [HttpGet]
        [Route("GetCancerStages")]
        public List<CancerStageMaster> GetCancerStages(bool? isActive, string cancerStageName)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetCancerStages(isActive, cancerStageName);
        }

        // Get: api/ConsultationAPI/GetSurgeryList
        [HttpGet]
        [Route("GetSurgeryList")]
        public List<SurgeryMaster> GetSurgeryList(bool? isActive, string surgeryName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetSurgeryList(isActive, surgeryName, searchTerm);
        }

        // Get: api/ConsultationAPI/GetAllergyList
        [HttpGet]
        [Route("GetAllergyList")]
        public List<AllergyMaster> GetAllergyList(bool? isActive, string allergyName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetAllergyList(isActive, allergyName, searchTerm);
        }

        // Get: api/ConsultationAPI/GetHealthConditionList
        [HttpGet]
        [Route("GetHealthConditionList")]
        public List<HealthConditionMaster> GetHealthConditionList(bool? isActive, string healthConditionName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetHealthConditionList(isActive, healthConditionName, searchTerm);
        }

        // Get: api/ConsultationAPI/GetDoctors
        [HttpGet]
        [Route("GetDoctors")]
        public List<UserDetail> GetDoctors(bool? isActive, string userRole)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetUserList(isActive, userRole);
        }

        // Get: api/ConsultationAPI/GetIllegalDrugs
        [HttpGet]
        [Route("GetIllegalDrugs")]
        public List<IllegalDrugMaster> GetIllegalDrugs(bool? isActive, string IllegalDrug)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetIllegalDrugs(isActive, IllegalDrug);
        }
        
        // Get: api/ConsultationAPI/GetMenstrualSymptoms
        [HttpGet]
        [Route("GetMenstrualSymptoms")]
        public List<MenstrualSymptomsMaster> GetMenstrualSymptoms(bool? isActive, string MenstrualSymptoms)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetMenstrualSymptoms(isActive, MenstrualSymptoms);
        }


        // Get: api/ConsultationAPI/GetDrugTypeList
        [HttpGet]
        [Route("GetDrugTypeList")]
        public List<DrugTypeMaster> GetDrugTypeList(bool? isActive, string drugType, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugTypeList(isActive, drugType, searchTerm);
        }

        // Get: api/ConsultationAPI/GetDrugSubTypeList
        [HttpGet]
        [Route("GetDrugSubTypeList")]
        public List<DrugSubTypeMaster> GetDrugSubTypeList(int drugTypeId, bool? isActive, string drugSubTypeName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugSubTypeList(drugTypeId, isActive, drugSubTypeName, searchTerm);
        }

        // Get: api/ConsultationAPI/GetDrugBrandList
        [HttpGet]
        [Route("GetDrugBrandList")]
        public List<DrugBrandMaster> GetDrugBrandList(bool? isActive, string drugBrandName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugBrandList(isActive, drugBrandName, searchTerm);
        }

        // Get: api/ConsultationAPI/GetDrugChemicalList
        [HttpGet]
        [Route("GetDrugChemicalList")]
        public List<DrugChemicalMaster> GetDrugChemicalList(bool? isActive, string drugChemicalName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugChemicalList(isActive, drugChemicalName, searchTerm);
        }

        // Get: api/ConsultationAPI/GetDrugFrequencyList
        [HttpGet]
        [Route("GetDrugFrequencyList")]
        public List<DrugFrequencyMaster> GetDrugFrequencyList(bool? isActive, string drugFrequencyName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetDrugFrequencyList(isActive, drugFrequencyName, searchTerm);
        }
        // Get: api/ConsultationAPI/GetOccupationList
        [HttpGet]
        [Route("GetOccupationList")]
        public List<OccupationMaster> GetOccupationList(bool? isActive, string occupationName, string searchTerm)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetOccupationList(isActive, occupationName, searchTerm);
        }

        // Get: api/ConsultationAPI/GetPackageList
        [HttpGet]
        [Route("GetPackageList")]
        public List<PackageMaster> GetPackageList(bool? isActive, int? packageId)
        {
            var masterObj = _Kernel.Get<IMaster>();
            return masterObj.GetPackageList(isActive, packageId);
        }

        // Get: api/ConsultationAPI/GetUnregisteredPatientEnquiry
        [HttpPost]
        [Route("GetUnregisteredPatientEnquiry")]
        public PatientEnquiryResponse GetUnregisteredPatientEnquiry([FromBody]OMC.Models.PatientEnquiry enquiry)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.UnregisteredPatientEnquiry(enquiry);

        }

        // Get: api/ConsultationAPI/GetConsultationList
        [HttpGet]
        [Route("GetConsultationList")]
        public List<ConsultationDisplay> GetConsultationList(int userId, string userRole)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationList(userId, userRole);
        }

        // Get: api/ConsultationAPI/GetConsultationList
        [HttpGet]
        [Route("GetConversationList")]
        public ConversationResponse GetConversationList(int consultationId, int userId, string userRole)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConversationList(consultationId, userId, userRole);
        }

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

        // Get: api/ConsultationAPI/GetConsultationReportList
        [HttpGet]
        [Route("GetConsultationReportList")]
        public ConsultationReportResponse GetConsultationReportList(int consultationId, int? consultationReportId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationReportList(consultationId, consultationReportId);
        }

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

        // Get: api/ConsultationAPI/GetConsultationSurgeryList
        [HttpGet]
        [Route("GetConsultationSurgeryList")]
        public ConsultationSurgeryResponse GetConsultationSurgeryList(int consultationId, int? consultationSurgeryId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationSurgeryList(consultationId, consultationSurgeryId);
        }

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

        // Get: api/ConsultationAPI/GetConsultationCancerTreatmentList
        [HttpGet]
        [Route("GetConsultationCancerTreatmentList")]
        public ConsultationCancerTreatmentResponse GetConsultationCancerTreatmentList(int consultationId, int? consultationCancerTreatmentId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationCancerTreatmentList(consultationId, consultationCancerTreatmentId);
        }

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

        // Get: api/ConsultationAPI/GetConsultationIllegalDrugDetailList
        [HttpGet]
        [Route("GetConsultationIllegalDrugDetailList")]
        public ConsultationIllegalDrugDetailsResponse GetConsultationIllegalDrugDetailList(int consultationId, int? consultationIllegalDrugDetailsId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationIllegalDrugDetailList(consultationId, consultationIllegalDrugDetailsId);
        }


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

        // Get: api/ConsultationAPI/GetConsultationPregnancyDetailsList
        [HttpGet]
        [Route("GetConsultationPregnancyDetailsList")]
        public ConsultationPregnancyDetailsResponse GetConsultationPregnancyDetailsList(int consultationId, int? consultationPregnancyDetailsId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationPregnancyDetailsList(consultationId, consultationPregnancyDetailsId);
        }


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

        // Get: api/ConsultationAPI/GetConsultationPreviousPregnancyDetailsList
        [HttpGet]
        [Route("GetConsultationPreviousPregnancyDetailsList")]
        public ConsultationPreviousPregnancyDetailsResponse GetConsultationPreviousPregnancyDetailsList(int consultationId, int? consultationPreviousPregnancyDetailsId, int? CurrentPregnancyID)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationPreviousPregnancyDetailsList(consultationId, consultationPreviousPregnancyDetailsId, CurrentPregnancyID);
        }

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

        // Get: api/ConsultationAPI/GetConsultationSDDHabitsList
        [HttpGet]
        [Route("GetConsultationSDDHabitsList")]
        public ConsultationSDDHabitsResponse GetConsultationSDDHabitsList(int consultationId, int? consultationSDDHabitsId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationSDDHabitsList(consultationId, consultationSDDHabitsId);
        }


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

        // Get: api/ConsultationAPI/GetConsultationAllergyList
        [HttpGet]
        [Route("GetConsultationAllergyList")]
        public ConsultationAllergyResponse GetConsultationAllergyList(int consultationId, int? consultationAllergyId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationAllergyList(consultationId, consultationAllergyId);
        }

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

        // Get: api/ConsultationAPI/GetConsultationFamilyHistoryList
        [HttpGet]
        [Route("GetConsultationFamilyHistoryList")]
        public ConsultationFamilyHistoryResponse GetConsultationFamilyHistoryList(int consultationId, int? consultationFamilyHistoryId
            , int? relationshipId, bool? excludeSelf)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationFamilyHistoryList(consultationId, consultationFamilyHistoryId, relationshipId, excludeSelf);
        }

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

        // Get: api/ConsultationAPI/GetConsultationOccupationList
        [HttpGet]
        [Route("GetConsultationOccupationList")]
        public ConsultationOccupationResponse GetConsultationOccupationList(int consultationId, int? consultationOccupationId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationOccupationList(consultationId, consultationOccupationId);
        }

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

        // Get: api/ConsultationAPI/GetConsultationBloodPressureReadingList
        [HttpGet]
        [Route("GetConsultationBloodPressureReadingList")]
        public ConsultationBloodPressureReadingResponse GetConsultationBloodPressureReadingList(int consultationId, int? consultationBloodPressureReadingId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationBloodPressureReadingList(consultationId, consultationBloodPressureReadingId);
        }

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

        // Get: api/ConsultationAPI/GetConsultationMedicationList
        [HttpGet]
        [Route("GetConsultationMedicationList")]
        public ConsultationMedicationResponse GetConsultationMedicationList(int consultationId, int? consultationMedicationId)
        {
            var ConsultationBLObj = _Kernel.Get<IConsultationBL>();
            return ConsultationBLObj.GetConsultationMedicationList(consultationId, consultationMedicationId);
        }
        #endregion
    }
}