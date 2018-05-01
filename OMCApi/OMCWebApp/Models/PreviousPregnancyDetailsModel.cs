using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class PreviousPregnancyDetailsModel
    {
        public ConsultationPreviousPregnancyDetails ConsultationPreviousPregnancyDetailsObject { get; set; }
        public PreviousPregnancyDetailsModel()
        {
            ConsultationPreviousPregnancyDetailsObject = new ConsultationPreviousPregnancyDetails();
        }
    }
}