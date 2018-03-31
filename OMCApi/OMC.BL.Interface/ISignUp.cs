using OMC.Models;
using System;
using System.Collections.Generic;

namespace OMC.BL.Interface
{
    public interface ISignUp : IDisposable
    {
        ErrorLog ValidateSignUpDetails(UserSignUp signupdetails);
        bool InitiateSignUpProcess(UserSignUp signupdetails);
        bool InsertUpdateProfile(Profile profile);
        List<Profile> GetProfileList(int userId, int? profileId);
    }
}
