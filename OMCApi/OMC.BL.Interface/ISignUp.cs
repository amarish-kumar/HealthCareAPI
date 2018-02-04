using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMC.Models;
using System.Data;

namespace OMC.BL.Interface
{
    public interface ISignUp : IDisposable
    {

        ErrorLog ValidateSignUpDetails(UserSignUp signupdetails);

        bool InitiateSignUpProcess(UserSignUp signupdetails);


    }
}
