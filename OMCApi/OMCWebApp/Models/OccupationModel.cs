using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class OccupationModel
    {
        public ConsultationOccupation ConsultationOccupationObject { get; set; }
        public List<OccupationMaster> OccupationList { get; set; }
        public OccupationModel()
        {
            ConsultationOccupationObject = new ConsultationOccupation();
        }
    }
}