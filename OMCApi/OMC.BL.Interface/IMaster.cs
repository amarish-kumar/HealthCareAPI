using OMC.Models;
using System;
using System.Collections.Generic;

namespace OMC.BL.Interface
{
    public interface IMaster : IDisposable
    {
        List<Role> GetRoles(bool? isActive, string roleDescription);
        List<ConsultationStatus> GetConsultationStatuses(bool? isActive, string description);
        List<UserDetail> GetUserList(bool? isActive, string userRole);
        List<Country> GetCountries(bool? isActive);
        List<AddressType> GetAddressTypes(bool? isActive);
        List<RelationshipMaster> GetRelationships(bool? isActive, string relationship);
        List<Gender> GetGenders(bool? isActive, string genderName);
    }
}
