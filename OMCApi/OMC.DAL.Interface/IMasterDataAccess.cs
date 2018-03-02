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
        List<Address> GetAddressTypes(bool? isActive);
    }
}
