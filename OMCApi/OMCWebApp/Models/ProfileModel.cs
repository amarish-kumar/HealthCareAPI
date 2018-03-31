using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ProfileModel
    {
        public Profile ProfileObject { get; set; }
        public List<RelationshipMaster> Relationships { get; set; }
        public List<Gender> Genders { get; set; }

        public ProfileModel()
        {
            ProfileObject = new Profile();
        }
    }
}