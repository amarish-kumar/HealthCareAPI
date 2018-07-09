using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class UserAddressModel
    {
        public UserAddress UserAddressObject { get; set; }
        public List<Country> CountryList { get; set; }
        public List<StateMaster> StateList { get; set; }
        public List<AddressType> AddressTypes { get; set; }
        public int UserId { get; set; }

        public UserAddressModel()
        {
            UserAddressObject = new UserAddress();
            CountryList = new List<Country>();
            StateList = new List<StateMaster>();
        }
    }
}