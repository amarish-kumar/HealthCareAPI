using OMC.Models;

namespace OMCWebApp.Models
{
    public class GetAccessCodeModel
    {
        public SignInResponse ObjSignInResponse { get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string Method { get; set; }
    }
}