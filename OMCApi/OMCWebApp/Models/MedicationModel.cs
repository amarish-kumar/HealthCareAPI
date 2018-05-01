using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class MedicationModel
    {
        public ConsultationMedications ConsultationMedicationObject { get; set; }
        public List<DrugTypeMaster> DrugTypeList { get; set; }
        public List<DrugSubTypeMaster> DrugSubTypeList { get; set; }
        public List<DrugFrequencyMaster> DrugFrequencyList { get; set; }
        public List<DrugBrandMaster> DrugBrandList { get; set; }
        public List<DrugChemicalMaster> DrugChemicalList { get; set; }
        public List<UnitMaster> DrugUnitList { get; set; }
        public MedicationModel()
        {
            ConsultationMedicationObject = new ConsultationMedications();
        }
    }
}