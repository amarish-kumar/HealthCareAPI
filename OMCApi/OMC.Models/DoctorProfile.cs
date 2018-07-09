using System;
using System.ComponentModel.DataAnnotations;

namespace OMC.Models
{
    public class DoctorProfile : BaseEntity
    {
        [Required(ErrorMessage = "Doctor Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Doctor Id is required.")]
        public int DoctorId { get; set; }
        public bool IsPublished { get; set; }
        [Required(ErrorMessage = "Email Address 1 is required.")]
        [EmailAddress(ErrorMessage = "Email address not of valid format")]
        public string EmailAddress1 { get; set; }
        public bool IsEmailAddress1Default { get; set; }
        [EmailAddress(ErrorMessage = "Email address not of valid format")]
        public string EmailAddress2 { get; set; }
        public bool IsEmailAddress2Default { get; set; }
        [EmailAddress(ErrorMessage = "Email address not of valid format")]
        public string EmailAddress3 { get; set; }
        public bool IsEmailAddress3Default { get; set; }
        [Required(ErrorMessage = "Phone Number 1 is required.")]
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "PhoneNumber is of wrong format")]
        public string PhoneNumber1 { get; set; }
        public bool IsPhoneNumber1Default { get; set; }
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "PhoneNumber is of wrong format")]
        public string PhoneNumber2 { get; set; }
        public bool IsPhoneNumber2Default { get; set; }
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "PhoneNumber is of wrong format")]
        public string PhoneNumber3 { get; set; }
        public bool IsPhoneNumber3Default { get; set; }
        public int? DefaultAddressId { get; set; }
        [RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+[\w-]+[\.]+[\.com]+([./?%&=]*)?", ErrorMessage = "Website Address is of wrong format")]
        public string WebsiteAddress { get; set; }
        [Required(ErrorMessage = "Timezone is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Timezone is required.")]
        public int? TimezoneId { get; set; }

        #region Serialization
        public bool ShouldSerializeTimezoneId()
        {
            return TimezoneId.HasValue;
        }

        public bool ShouldSerializeDefaultAddressId()
        {
            return DefaultAddressId.HasValue;
        }

        public bool ShouldSerializeEmailAddress2()
        {
            return !string.IsNullOrEmpty(EmailAddress2);
        }

        public bool ShouldSerializeEmailAddress3()
        {
            return !string.IsNullOrEmpty(EmailAddress3);
        }

        public bool ShouldSerializePhoneNumber2()
        {
            return !string.IsNullOrEmpty(PhoneNumber2);
        }

        public bool ShouldSerializePhoneNumber3()
        {
            return !string.IsNullOrEmpty(PhoneNumber3);
        }
        #endregion
    }

    public class DoctorProfileResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class DoctorProfileDisplay : DoctorProfile
    {
        public string DoctorName { get; set; }
        public string TimezoneDescription { get; set; }
    }
}
