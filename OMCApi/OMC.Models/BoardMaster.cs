namespace OMC.Models
{
    public class BoardMaster : BaseEntity
    {
        public string Board { get; set; }
        public string Description { get; set; }
        public string DisplayName
        {
            get
            {
                return Board + " - " + Description;
            }
        }
    }
}
