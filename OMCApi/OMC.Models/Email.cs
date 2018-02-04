namespace OMC.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string EmailType { get; set; }
        public string SenderEmailAddress { get; set; }
        public string SenderEmailAddressPassword { get; set; }
    }
}
