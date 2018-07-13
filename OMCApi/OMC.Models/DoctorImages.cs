using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class DoctorImages : BaseEntity
    {
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public int DoctorId { get; set; }
        public bool IsPrimary { get; set; }
        [Required(ErrorMessage = "File Name is required.")]
        public string FileName { get; set; }
        [Required(ErrorMessage = "File Data is required.")]
        public byte[] FileData { get; set; }

        #region Serialization
        
        #endregion
    }

    public class DoctorImagesResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<DoctorImageDisplay> DoctorImagesList { get; set; }
    }

    public class DoctorImageDisplay : DoctorImages
    {
        public string DoctorName { get; set; }
    }
}
