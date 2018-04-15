using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class FamilyHistoryModel
    {
        public ConsultationFamilyHistory ConsultationFamilyHistoryObject { get; set; }
        public List<HealthConditionMaster> HealthConditionList { get; set; }
        public List<RelationshipMaster> RelationshipList { get; set; }
        public FamilyHistoryModel()
        {
            ConsultationFamilyHistoryObject = new ConsultationFamilyHistory();
        }
    }
}