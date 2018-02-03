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
            // Find your Account Sid and Auth Token at twilio.com/console
            const string accountSid = "AC50b93c726f0e0e8a034a687c33ba5243";
            const string authToken = "020c1d6b8296445779167f5dce1b11b1";
            TwilioClient.Init(accountSid, authToken);
            var toNumber = new PhoneNumber(to);
            var message = MessageResource.Create(
              toNumber,
              from: new PhoneNumber("+15612204349"),
              body: "Tomorrow's forecast in Financial District, San Francisco is Clear");            
        }
    }
}
