using OMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMCWebApp.Models
{
    public class ConsultationModel
    {
        public Consultation ConsultationObject { get; set; }
        public List<ConsultationStatus> ConsultationStatuses { get; set; }
        public List<UserDetail> Doctors { get; set; }

        public ConsultationModel()
        {
            ConsultationObject = new Consultation();
        }
    }
}