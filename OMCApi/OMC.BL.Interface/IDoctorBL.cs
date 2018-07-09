using OMC.Models;
using System.Collections.Generic;

namespace OMC.BL.Interface
{
    public interface IDoctorBL
    {
        DoctorProfileResponse InsertUpdateDoctorProfile(DoctorProfile doctorProfile, string operation);
        List<DoctorProfileDisplay> GetDoctorProfileList(int userId, int? doctorId);
        DoctorProfileResponse DeleteDoctorProfile(int doctorProfileId, int userId);
        UserAddressResponse InsertUpdateUserAddress(UserAddress userAddress, string operation);
        UserAddressResponse GetUserAddressList(int userId, int? addressId);
    }
}
