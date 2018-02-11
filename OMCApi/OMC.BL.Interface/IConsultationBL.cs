using OMC.Models;
using System;

namespace OMC.BL.Interface
{
    public interface IConsultationBL : IDisposable
    {
        ConsultationResponse InitiateConsultation(Consultation consultationDetails);
    }
}
