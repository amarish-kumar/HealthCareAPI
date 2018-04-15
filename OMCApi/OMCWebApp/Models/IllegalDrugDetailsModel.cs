using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class IllegalDrugDetailsModel
    {
        public ConsultationIllegalDrugDetails ConsultationIllegalDrugDetailsObject { get; set; }
        public List<IllegalDrugMaster> IllegalDrugDetails { get; set; }
        public IllegalDrugDetailsModel()
        {
            ConsultationIllegalDrugDetailsObject = new ConsultationIllegalDrugDetails();
        }
    }
}