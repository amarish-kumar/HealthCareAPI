using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class DoctorFellowship : BaseEntity
    {
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Begining Year is required.")]
        [Range(1901, 9999, ErrorMessage = "Begining Year is invalid.")]
        public int BeginingYear { get; set; }
        [Required(ErrorMessage = "Ending Year is required.")]
        [Range(1901, 9999, ErrorMessage = "Ending Year is invalid.")]
        public int EndingYear { get; set; }
        public string HospitalName { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "State Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "State Id is required.")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Country Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Country Id is required.")]
        public int CountryId { get; set; }

        #region Serialization

        public bool ShouldSerializeHospitalName()
        {
            return !string.IsNullOrEmpty(HospitalName);
        }

        public bool ShouldSerializeCity()
        {
            return !string.IsNullOrEmpty(City);
        }
        #endregion
    }

    public class DoctorFellowshipResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<DoctorFellowshipDisplay> DoctorFellowshipList { get; set; }
    }

    public class DoctorFellowshipDisplay : DoctorFellowship
    {
        public string DoctorName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
    }
}
