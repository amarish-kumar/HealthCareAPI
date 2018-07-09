namespace OMC.Models
{
    public class StateMaster : BaseEntity
    {
        public string State { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
