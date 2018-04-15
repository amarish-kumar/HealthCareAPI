using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class SDDHabitsModel
    {
        public ConsultationSDDHabits ConsultationSDDHabitsObject { get; set; }
        public SDDHabitsModel()
        {
            ConsultationSDDHabitsObject = new ConsultationSDDHabits();
        }
    }
}