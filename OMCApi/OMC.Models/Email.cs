namespace OMC.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string EmailType { get; set; }
        public string SenderAccountId { get; set; }
        public string SenderAddress { get; set; }
        public string SenderPassword { get; set; }
    }
}
