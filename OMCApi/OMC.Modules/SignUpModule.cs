using Ninject.Modules;
using OMC.BL.Interface;
using OMC.BL.Library;
using OMC.DAL.Interface;
using OMC.DAL.Library;

namespace OMC.Modules
{
    public class SignUpModule : NinjectModule
    {
        public override void Load()
        {
            try
            {
                Bind<ISignUp>().To<SignUp>();
                Bind<ISignUpDataAccess>().To<SignUpDataAccess>();
            }
            catch
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
