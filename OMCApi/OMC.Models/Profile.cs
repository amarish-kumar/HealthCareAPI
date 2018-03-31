using System.Text;

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
        public string FullName
        {
            get
            {
                var result = new StringBuilder();
                if (!string.IsNullOrEmpty(LastName))
                {
                    result.Append(LastName);
                    result.Append(", ");
                }
                if (!string.IsNullOrEmpty(FirstName))
                {
                    result.Append(FirstName);
                }
                return result.ToString();
            }
        }
        public string GenderName { get; set; }
        public string Relationship { get; set; }
        #endregion
    }
}
