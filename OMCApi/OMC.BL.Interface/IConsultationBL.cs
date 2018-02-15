using OMC.Models;
using System;
using System.Collections.Generic;

namespace OMC.BL.Interface
{
    public interface IConsultationBL : IDisposable
    {
        ConsultationResponse InitiateConsultation(Consultation consultationDetails);
        List<ConsultationDisplay> GetConsultationList(int userId, string userRole);
    }
}
