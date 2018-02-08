using OMC.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace OMC.BL.Library.Helpers
{
    public class SMSHelper
    {
        public static void SendSMS(Email objEmail, string to)
        {
            TwilioClient.Init(objEmail.SenderAccountId, objEmail.SenderPassword);
            var toNumber = new PhoneNumber(to);
            var message = MessageResource.Create(
              toNumber,
              from: new PhoneNumber(objEmail.SenderAddress),
              body: objEmail.Body);            
        }
    }
}
