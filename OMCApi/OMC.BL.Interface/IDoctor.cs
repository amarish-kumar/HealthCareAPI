using OMC.Models;

namespace OMC.BL.Interface
{
    public interface IDoctor
    {
        DoctorProfileResponse InsertUpdateDoctorProfile(DoctorProfile doctorProfile, string operation);
    }
}
