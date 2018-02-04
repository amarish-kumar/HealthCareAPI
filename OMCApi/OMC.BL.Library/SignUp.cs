using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMC.BL.Interface;
using OMC.DAL.Interface;
using OMC.Models;


namespace OMC.BL.Library
{
    public class SignUp:ISignUp
    {
        #region Declarations
        ISignUpDataAccess _signUpDA;
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
                return isSignin;
            }
            catch (Exception ex)
            {
                //Log
                return false;
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
