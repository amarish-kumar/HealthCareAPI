using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ConsultationObjectiveNoteModel
    {
        public ConsultationObjectiveNotes ConsultationObjectiveNotesObject { get; set; }
        public List<UserDetail> Doctors { get; set; }
        public ConsultationObjectiveNoteModel()
        {
            ConsultationObjectiveNotesObject = new ConsultationObjectiveNotes();
        }
    }
}