using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class AllergyModel
    {
        public ConsultationAllergies ConsultationAllergyObject { get; set; }
        public List<AllergyMaster> AllergyList { get; set; }
        public AllergyModel()
        {
            ConsultationAllergyObject = new ConsultationAllergies();
        }
    }
}