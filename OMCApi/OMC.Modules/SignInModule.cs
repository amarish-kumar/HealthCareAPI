using Ninject.Modules;
using OMC.BL.Interface;
using OMC.BL.Library;
using OMC.DAL.Interface;
using OMC.DAL.Library;

namespace OMC.Modules
{
    public class SignInModule : NinjectModule
    {
        public override void Load()
        {
            try
            {
                Bind<ISignIn>().To<SignIn>();
                Bind<ISignInDataAccess>().To<SignInDataAccess>();
                Bind<IMaster>().To<Master>();
                Bind<IMasterDataAccess>().To<MasterDataAccess>();
            }
            catch
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
