using OMC.Models;
using System.Net.Mail;

namespace OMC.BL.Library.Helpers
{
    public class EmailHelper
    {
        public static void SendEmail(Email objEmail, string to)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(objEmail.SenderAddress);
            mail.Subject = objEmail.Subject;
            mail.Body = objEmail.Body;
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(objEmail.SenderAddress, objEmail.SenderPassword);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
