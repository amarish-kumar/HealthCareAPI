namespace OMC.Models
{
    public class PackageMaster : BaseEntity
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Packagename { get; set; }
    }
}
