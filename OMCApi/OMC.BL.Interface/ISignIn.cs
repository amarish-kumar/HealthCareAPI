using OMC.Models;
using System;

namespace OMC.BL.Interface
{
    public interface ISignIn : IDisposable
    {
        SignInResponse InitiateSignInProcess(UserLogin user);
        UserAccessCodeResponse GetAccessCode(UserLogin user);
        void ValidateAccessCode(UserAccessCodeResponse userAccessCode);
    }
}
