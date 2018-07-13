using OMC.Models;
using System.Collections.Generic;

namespace OMC.DAL.Interface
{
    public interface IMasterDataAccess
    {
        List<Role> GetRoles(bool? isActive, string roleName);
        List<ConsultationStatus> GetConsultationStatuses(bool? isActive, string description);
        List<UserDetail> GetUserList(bool? isActive, string userRole);
        List<Country> GetCountries(bool? isActive);
        List<AddressType> GetAddressTypes(bool? isActive);
        List<RelationshipMaster> GetRelationships(bool? isActive, string relationship, bool? excludeSelf);
        List<Gender> GetGenders(bool? isActive, string genderName);
        List<CancerStageMaster> GetCancerStages(bool? isActive, string cancerStageName);
        List<SurgeryMaster> GetSurgeryList(bool? isActive, string surgeryName, string searchTerm);
        List<IllegalDrugMaster> GetIllegalDrugs(bool? isActive, string IllegalDrug);
        List<AllergyMaster> GetAllergyList(bool? isActive, string allergyName, string searchTerm);
        List<HealthConditionMaster> GetHealthConditionList(bool? isActive, string healthConditionName, string searchTerm);
        List<DrugTypeMaster> GetDrugTypeList(bool? isActive, string drugType, string searchTerm);
        List<OccupationMaster> GetOccupationList(bool? isActive, string occupationName, string searchTerm);
        List<PackageMaster> GetPackageList(bool? isActive, int? packageId);
        List<DrugSubTypeMaster> GetDrugSubTypeList(int drugTypeId, bool? isActive, string drugSubTypeName, string searchTerm);
        List<DrugBrandMaster> GetDrugBrandList(bool? isActive, string drugBrandName, string searchTerm);
        List<DrugChemicalMaster> GetDrugChemicalList(bool? isActive, string drugChemicalName, string searchTerm);
        List<DrugFrequencyMaster> GetDrugFrequencyList(bool? isActive, string drugFrequencyName, string searchTerm);
        List<UnitMaster> GetDrugUnitList(bool? isActive, string drugUnitName, string searchTerm);
        List<MenstrualSymptomsMaster> GetMenstrualSymptoms(bool? isActive, string MenstrualSymptoms);
        List<TimezoneMaster> GetTimezones(bool? isActive, string searchTerm);
        List<StateMaster> GetStates(bool? isActive, int? countryId, int? stateId);
        List<BoardMaster> GetBoards(bool? isActive, int? boardId, string board);
    }
}
