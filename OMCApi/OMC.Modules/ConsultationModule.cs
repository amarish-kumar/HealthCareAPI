using Ninject.Modules;
using OMC.BL.Interface;
using OMC.BL.Library;
using OMC.DAL.Interface;
using OMC.DAL.Library;

namespace OMC.Modules
{
    public class ConsultationModule : NinjectModule
    {
        public override void Load()
        {
            try
            {
                Bind<IConsultationBL>().To<ConsultationBL>();
                Bind<IConsultationDataAccess>().To<ConsultationDataAccess>();
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
