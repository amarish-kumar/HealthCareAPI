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
        DoctorAwardsResponse InsertUpdateDoctorAward(DoctorAwards doctorAward, string operation);
        DoctorAwardsResponse GetDoctorAwardList(int doctorId, int? doctorAwardId);
        DoctorBoardResponse InsertUpdateDoctorBoard(DoctorBoard doctorBoard, string operation);
        DoctorBoardResponse GetDoctorBoardList(int doctorId, int? doctorBoardId);
        DoctorEducationResponse InsertUpdateDoctorEducation(DoctorEducation doctorEducation, string operation);
        DoctorEducationResponse GetDoctorEducationList(int doctorId, int? doctorEducationId);
        DoctorFellowshipResponse InsertUpdateDoctorFellowship(DoctorFellowship doctorFellowship, string operation);
        DoctorFellowshipResponse GetDoctorFellowshipList(int doctorId, int? doctorFellowshipId);
        DoctorImagesResponse InsertUpdateDoctorImage(DoctorImages doctorImage, string operation);
        DoctorImagesResponse GetDoctorImageList(int doctorId, int? doctorImageId);
        DoctorResidencyResponse InsertUpdateDoctorResidency(DoctorResidency doctorResidency, string operation);
        DoctorResidencyResponse GetDoctorResidencyList(int doctorId, int? doctorResidencyId);
    }
}
