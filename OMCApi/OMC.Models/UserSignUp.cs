using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OMC.Models
{
    //[Serializable]
    public class UserSignUp
    {
        #region Properties
        [Required(ErrorMessage ="FirstName required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName required")]
        public string LastName { get; set; }
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Required]
        [EmailAddress(ErrorMessage = "Email address not of valid format")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Address required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "PhoneNumber required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "PhoneNumber is of wrong format")]
        public string PhoneNumber { get; set; }
        [RequiredIf("UserType","1", ErrorMessage = "Gender required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "DOB required")]
        public string DOB { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "AlternateNo required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "AlternateNo is of wrong format")]
        public string AlternateNo { get; set; }
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "EmergencyContactNo is of wrong format")]
        public string EmergencyContactNo { get; set; }
        public string EmergencyContactPerson { get; set; }
        public string DLNumber { get; set; }
        public string SSN { get; set; }
        public int UserType { get; set; }
        public int Active { get; set; }
        public string LoggedInUserID { get; set; }
        public string UserAction { get; set; }
        #endregion

        #region Serialization
        public string Serialize()
        {
            return Helper.Serialize<UserSignUp>(this);
        }

        public bool ShouldSerializeEmailAddress()
        {
            return !string.IsNullOrEmpty(EmailAddress);
        }
        #endregion
    }

    public static class Helper
    {
        public static string Serialize<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
        }
    }
}
