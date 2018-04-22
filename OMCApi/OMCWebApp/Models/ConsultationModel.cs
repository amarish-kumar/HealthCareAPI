using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ConsultationModel
    {
        public Consultation ConsultationObject { get; set; }
        public List<ConsultationStatus> ConsultationStatuses { get; set; }
        public List<UserDetail> Doctors { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<PackageMaster> Packages { get; set; }

        public ConsultationModel()
        {
            ConsultationObject = new Consultation();
        }
    }
}