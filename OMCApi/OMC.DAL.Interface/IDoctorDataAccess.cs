using OMC.Models;
namespace OMC.DAL.Interface
{
    public interface IDoctorDataAccess
    {
        DoctorProfileResponse InsertUpdateDoctorProfile(DoctorProfile doctorProfile, string operation);
    }
}
