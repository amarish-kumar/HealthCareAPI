using OMC.Models;
using System.Collections.Generic;
using System.Web;

namespace OMCWebApp.Models
{
    public class ReportModel
    {
        public ConsultationReports ConsultationReportObject { get; set; }
        public List<Country> Countries { get; set; }
        public HttpPostedFileBase ReportFile { get; set; }
        public ReportModel()
        {
            ConsultationReportObject = new ConsultationReports();
        }
    }
}