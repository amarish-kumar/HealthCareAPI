using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class DoctorAwards : BaseEntity
    {
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Year Received is required.")]
        [Range(1901, 9999, ErrorMessage = "Year Received is invalid.")]
        public int YearReceived { get; set; }
        public string InstitutionName { get; set; }
        public string AwardName { get; set; }

        #region Serialization

        public bool ShouldSerializeInstitutionName()
        {
            return !string.IsNullOrEmpty(InstitutionName);
        }

        public bool ShouldSerializeAwardName()
        {
            return !string.IsNullOrEmpty(AwardName);
        }
        #endregion
    }

    public class DoctorAwardsResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<DoctorAwardsDisplay> DoctorAwardsList { get; set; }
    }

    public class DoctorAwardsDisplay : DoctorAwards
    {
        public string DoctorName { get; set; }
    }
}
