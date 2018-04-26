using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationMedications : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }

        [Required(ErrorMessage = "Drug Chemical Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Drug Chemical Id is required.")]
        public int DrugChemicalId { get; set; }

        public string DrugChemicalOtherDescription { get; set; }

        [Required(ErrorMessage = "Drug Brand Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Drug Brand Id is required.")]
        public int DrugBrandId { get; set; }

        public string DrugBrandOtherDescription { get; set; }
        public DateTime? DrugStartDate { get; set; }
        public DateTime? DrugEndDate { get; set; }
        public decimal? DrugDosage { get; set; }

        [Required(ErrorMessage = "Drug Frequency Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Drug Frequency Id is required.")]
        public int DrugFrequencyId { get; set; }

        [Required(ErrorMessage = "Drug Type Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Drug Type Id is required.")]
        public int DrugTypeId { get; set; }

        [Required(ErrorMessage = "Drug Sub Type Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Drug Sub Type Id is required.")]
        public int DrugSubTypeId { get; set; }

        #region Serialization

        public bool ShouldSerializeDrugDosage()
        {
            return DrugDosage.HasValue;
        }

        public bool ShouldSerializeDrugChemicalOtherDescription()
        {
            return !string.IsNullOrEmpty(DrugChemicalOtherDescription);
        }

        public bool ShouldSerializeDrugBrandOtherDescription()
        {
            return !string.IsNullOrEmpty(DrugBrandOtherDescription);
        }

        public bool ShouldSerializeDrugStartDate()
        {
            return DrugStartDate.HasValue;
        }

        public bool ShouldSerializeDrugEndDate()
        {
            return DrugEndDate.HasValue;
        }
        #endregion
    }

    public class ConsultationMedicationResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationMedicationDisplay> ConsultationMedicationList { get; set; }
    }

    public class ConsultationMedicationDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public int DrugChemicalId { get; set; }
        public string DrugChemicalName { get; set; }
        public string DrugChemicalOtherDescription { get; set; }
        public int DrugBrandId { get; set; }
        public string DrugBrandName { get; set; }
        public string DrugBrandOtherDescription { get; set; }
        public DateTime? DrugStartDate { get; set; }
        public DateTime? DrugEndDate { get; set; }
        public decimal? DrugDosage { get; set; }
        public int DrugFrequencyId { get; set; }
        public string DrugFrequencyName { get; set; }
        public int DrugTypeId { get; set; }
        public string DrugTypeName { get; set; }
        public int DrugSubTypeId { get; set; }
        public string DrugSubTypeName { get; set; }
    }
}
