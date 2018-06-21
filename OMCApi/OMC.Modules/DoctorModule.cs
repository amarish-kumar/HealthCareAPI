using Ninject.Modules;
using OMC.BL.Interface;
using OMC.BL.Library;
using OMC.DAL.Interface;
using OMC.DAL.Library;

namespace OMC.Modules
{
    public class DoctorModule : NinjectModule
    {
        public override void Load()
        {
            try
            {
                Bind<IDoctor>().To<Doctor>();
                Bind<IDoctorDataAccess>().To<DoctorDataAccess>();
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
