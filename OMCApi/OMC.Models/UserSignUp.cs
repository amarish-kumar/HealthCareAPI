using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace OMC.Models
{
    public class Country
    {
        public int ID;
        public string CountryDesc;
    }

    public class Address
    {
        public int ID;
        public string AddressDesc;
    }

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
        [RequiredIf("UserType", "2", ErrorMessage = "Address1 required")]
        public string Address1 { get; set; }
        [RequiredIf("UserType", "2", ErrorMessage = "Address2 required")]
        public string Address2 { get; set; }
        [RequiredIf("UserType", "2", ErrorMessage = "City required")]
        public string City { get; set; }
        [RequiredIf("UserType", "2", ErrorMessage = "State required")]
        public string State { get; set; }
        [RequiredIf("UserType", "2", ErrorMessage = "ZipCode required")]
        public string ZipCode { get; set; }
        [RequiredIf("UserType", "2", ErrorMessage = "AddressType required")]
        public string AddressType { get; set; }
        public List<Address> AddressTypes { get; set; }
        [RequiredIf("UserType", "2", ErrorMessage = "Country required")]
        public string Country { get; set; }
        public List<Country> Countries { get; set; }
        [Required(ErrorMessage = "PhoneNumber required")]
        [RegularExpression(@"^\+(?:[0-9] ?){6,14}[0-9]$", ErrorMessage = "PhoneNumber is of wrong format")]
        public string PhoneNumber { get; set; }
        [RequiredIf("UserType","4", ErrorMessage = "Gender required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "DOB required")]
        public string DOB { get; set; }
        [RequiredIf("UserType", "4", ErrorMessage = "Password required")]
        public string Password { get; set; }
        [RequiredIf("UserType", "2", ErrorMessage = "AlternateNo required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "AlternateNo is of wrong format")]
        public string AlternateNo { get; set; }
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "EmergencyContactNo is of wrong format")]
        public string EmergencyContactNo { get; set; }
        public string EmergencyContactPerson { get; set; }
        public string DLNumber { get; set; }
        public string DLCopy { get; set; }
        public string SSN { get; set; }
        public int UserType { get; set; }
        public int Active { get; set; }
        public string LoggedInUserID { get; set; }
        public string UserAction { get; set; }
        public string isEmailVerified { get; set; }
        public string isPhoneVerified { get; set; }
        [RequiredIf("UserType", "4", ErrorMessage = "Terms and Condition not accepted")]
        public bool isTnCAccepted { get; set; }
        public string TnC { get; set; }
        public string TnCID { get; set; }
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
