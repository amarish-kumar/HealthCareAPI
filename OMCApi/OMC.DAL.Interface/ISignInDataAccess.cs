using OMC.Models;

namespace OMC.DAL.Interface
{
    public interface ISignInDataAccess
    {
        SignInResponse InitiateSignInProcess(UserLogin user);
        UserAccessCodeResponse GetAccessCode(UserLogin user);
        Email GetEmailData(string emailType);
        void ValidateAccessCode(UserAccessCodeResponse userAccessCode);
    }
}
