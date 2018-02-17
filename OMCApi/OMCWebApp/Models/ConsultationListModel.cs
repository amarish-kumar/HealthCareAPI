using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ConsultationListModel
    {
        public List<ConsultationDisplay> ConsultationList { get; set; }
        public int UserId { get; set; }
        public string UserRole { get; set; }
    }
}