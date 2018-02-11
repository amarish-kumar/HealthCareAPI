using System.Web.Mvc;

namespace OMCApi.Areas.Consultation
{
    public class ConsultationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Consultation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Consultation_default",
                "Consultation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OMCApi.Areas.Consultation.Controllers" }
            );
        }
    }
}