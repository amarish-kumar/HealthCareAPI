using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ProfileListModel
    {
        public int UserId { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<RelationshipMaster> Relationships { get; set; }
        public List<Gender> Genders { get; set; }

        public ProfileListModel()
        {
            Profiles = new List<Profile>();
        }
    }
}