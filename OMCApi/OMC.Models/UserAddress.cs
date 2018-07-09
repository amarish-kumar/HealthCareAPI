using System.Collections.Generic;

namespace OMC.Models
{
    public class UserAddress : BaseEntity
    {
        public int UserId { get; set; }
        public int AddressTypeId { get; set; }
        public int CountryId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string ZipCode { get; set; }
    }

    public class UserAddressResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<UserAddressDisplay> UserAddressList { get; set; }
    }

    public class UserAddressDisplay : UserAddress
    {
        public string AddressType { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
    }
}

