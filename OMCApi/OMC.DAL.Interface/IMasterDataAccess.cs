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
        List<RelationshipMaster> GetRelationships(bool? isActive, string relationship);
        List<Gender> GetGenders(bool? isActive, string genderName);
        List<CancerStageMaster> GetCancerStages(bool? isActive, string cancerStageName);
        List<SurgeryMaster> GetSurgeryList(bool? isActive, string surgeryName, string searchTerm);
    }
}
