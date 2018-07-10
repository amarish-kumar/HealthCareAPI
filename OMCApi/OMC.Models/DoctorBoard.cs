using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class DoctorBoard : BaseEntity
    {
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "BoardI Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Board Id is required.")]
        public int BoardId { get; set; }
        public string OtherDescription { get; set; }

        #region Serialization

        public bool ShouldSerializeOtherDescription()
        {
            return !string.IsNullOrEmpty(OtherDescription);
        }

        #endregion
    }

    public class DoctorBoardResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<DoctorBoardDisplay> DoctorBoardList { get; set; }
    }

    public class DoctorBoardDisplay : DoctorBoard
    {
        public string DoctorName { get; set; }
        public string BoardName { get; set; }
    }
}
