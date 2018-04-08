using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class SurgeryModel
    {
        public ConsultationSurgeries ConsultationSurgeryObject { get; set; }
        public List<SurgeryMaster> SurgeryList { get; set; }
        public SurgeryModel()
        {
            ConsultationSurgeryObject = new ConsultationSurgeries();
        }
    }
}