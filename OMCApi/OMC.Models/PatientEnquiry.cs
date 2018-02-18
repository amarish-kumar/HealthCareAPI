using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OMC.Models
{
    public class PatientEnquiry
    {
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public string SpecialityId { get; set; }

        public string SpecialityDesc { get; set; }

        public string DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string SearchKeyWords { get; set; }
    }

    public class PatientEnquiryResponse
    {
        public List<PatientEnquiry> Enquiries { get; set; }
    }
}
