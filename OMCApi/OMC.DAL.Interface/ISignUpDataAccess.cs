using OMC.Models;
using System.Collections.Generic;

namespace OMC.DAL.Interface
{
    public interface ISignUpDataAccess
    {
        bool InitiateSignUpProcess(UserSignUp signupdetails);
        bool InsertUpdateProfile(Profile profile);
        List<Profile> GetProfileList(int userId, int? profileId);
    }
}
