using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class ConsultationSDDHabits : BaseEntity
    {
        [Required(ErrorMessage = "Consultation Id is required.")]
        public int ConsultationId { get; set; }
        public bool DoSmoke { get; set; }
        public bool EverSmoked { get; set; }
        public int? YearOfQuittingSmoking { get; set; }
        public int? SmokingFreq { get; set; }
        //public bool ConsumeAlcohol { get; set; }
        //public string AlcoholConsumptionFreq { get; set; }
        //public int? DrinksPerDay { get; set; }
        //public int? DrinksPerWeek { get; set; }
        //public bool ConsumeDrugs { get; set; }
        //public string DrugsConsumptionFreq { get; set; }
        //public int? DrugsPerDay { get; set; }
        //public int? DrugsPerWeek { get; set; }
    }

    public class ConsultationSDDHabitsDisplay : BaseEntity
    {
        public int ConsultationId { get; set; }
        public bool DoSmoke { get; set; }
        public bool EverSmoked { get; set; }
        public int? YearOfQuittingSmoking { get; set; }
        public int? SmokingFreq { get; set; }
    }

    public class ConsultationSDDHabitsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<ConsultationSDDHabitsDisplay> ConsultationSDDHabitsDisplayList { get; set; }
    }
}
