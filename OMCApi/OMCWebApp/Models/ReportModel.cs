using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ReportModel
    {
        public ConsultationReports ConsultationReportObject { get; set; }
        public List<Country> Countries { get; set; }
        public ReportModel()
        {
            ConsultationReportObject = new ConsultationReports();
        }
    }
}