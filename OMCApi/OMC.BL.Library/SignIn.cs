using OMC.BL.Interface;
using OMC.BL.Library.Helpers;
using OMC.DAL.Interface;
using OMC.Models;
using System;

namespace OMC.BL.Library
{
    public class SignIn : ISignIn
    {
        #region Declarations
        ISignInDataAccess _signInDA;
        #endregion

        public SignIn(ISignInDataAccess SignInDA)
        {
            this._signInDA = SignInDA;
        }

        public SignInResponse InitiateSignInProcess(UserLogin user)
        {
            try
            {
                SignInResponse isSignin = this._signInDA.InitiateSignInProcess(user);
                return isSignin;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //Log
            }
        }

        public UserAccessCodeResponse GetAccessCode(UserLogin user)
        {
            try
            {
                var userAccessCodeResponse =  this._signInDA.GetAccessCode(user);
                if (user.GetCodeMethod.ToLower() == "email")
                {
                    var objEmail = this._signInDA.GetEmailData("GET_ACCESS_CODE");
                    objEmail.Body = string.Format(objEmail.Body, userAccessCodeResponse.AccessCode);
                    EmailHelper.SendEmail(objEmail, userAccessCodeResponse.EmailAddress);
                }
                else
                {
                    var objEmail = this._signInDA.GetEmailData("GET_ACCESS_CODE_SMS");
                    objEmail.Body = string.Format(objEmail.Body, userAccessCodeResponse.AccessCode);
                    SMSHelper.SendSMS(objEmail, userAccessCodeResponse.PhoneNumber);
                }
                return userAccessCodeResponse;
            }
            catch (Exception ex)
            {
                return new UserAccessCodeResponse
                {
                    ErrorMessage= ex.Message
                };
            }
            finally
            {
            }
        }

        public void ValidateAccessCode(UserAccessCodeResponse userAccessCode)
        {
            try
            {
                this._signInDA.ValidateAccessCode(userAccessCode);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }


        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            { }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
