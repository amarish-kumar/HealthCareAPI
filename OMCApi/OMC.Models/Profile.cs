namespace OMC.Models
{
    public class Profile : BaseEntity
    {
        public int UserId { get; set; }
        public int RelationshipId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string DOB { get; set; }
        public bool IsDefault { get; set; }

        #region Display Properties
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string GenderName { get; set; }
        public string Relationship { get; set; }
        #endregion
    }
}
