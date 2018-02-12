using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMC.BL.Interface;
using OMC.BL.Library.Helpers;
using OMC.DAL.Interface;
using OMC.DAL.Library;
using OMC.Models;


namespace OMC.BL.Library
{
    public class SignUp:ISignUp
    {
        #region Declarations
        ISignUpDataAccess _signUpDA;
        ISignInDataAccess _signInDA;
        #endregion

        public SignUp(ISignUpDataAccess SignUpDA)
        {
            this._signUpDA = SignUpDA;
            
        }

        public bool InitiateSignUpProcess(UserSignUp signupdetails)
        {
            try
            {
                bool isSignin = this._signUpDA.InitiateSignUpProcess(signupdetails);

                if (isSignin && signupdetails.UserType == 2)
                {
                    this._signInDA = new SignInDataAccess();
                    var objEmail = this._signInDA.GetEmailData("GET_PWD_CSR");
                    objEmail.Body = string.Format(objEmail.Body, signupdetails.FirstName);
                    EmailHelper.SendEmail(objEmail, signupdetails.EmailAddress);

                    objEmail = this._signInDA.GetEmailData("GET_PWD_CSR_SMS");
                    objEmail.Body = string.Format(objEmail.Body, signupdetails.FirstName);
                    SMSHelper.SendSMS(objEmail, signupdetails.PhoneNumber);
                }
                return isSignin;
            }
            catch (Exception ex)
            {
                //Log
                throw ex;
            }
            finally
            {
                //Log
            }
        }

        public ErrorLog ValidateSignUpDetails(UserSignUp signupdetails)
        {
            ErrorLog validationmsg = new ErrorLog();
            try
            {
                //bool isDetailsValid = true;
               

                if (!string.IsNullOrEmpty(signupdetails.EmailAddress) && !string.IsNullOrEmpty(signupdetails.FirstName) && !string.IsNullOrEmpty(signupdetails.LastName) && !string.IsNullOrEmpty(signupdetails.DOB) && !string.IsNullOrEmpty(signupdetails.Gender) && !string.IsNullOrEmpty(signupdetails.Password) && !string.IsNullOrEmpty(signupdetails.PhoneNumber))
                {
                    validationmsg.ExceptionType = "Validation Success";
                    validationmsg.Source = "ValidateSignUpDetails";
                }
                else
                {
                    validationmsg.ExceptionType = "Validation Failed";
                    validationmsg.Message = "Mandatory fields missing";
                    validationmsg.Source = "ValidateSignUpDetails";
                }

                return validationmsg;
            }
            catch (Exception ex)
            {
                //Log

                validationmsg.ExceptionType = ex.GetType().ToString();
                validationmsg.Message = ex.ToString();
                validationmsg.Source = "ValidateSignUpDetails";
                return validationmsg;
            }
            finally
            {
                //Log
            }
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
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
