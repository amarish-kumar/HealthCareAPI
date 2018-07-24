using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class ConsultationSubjectiveNoteModel
    {
        public ConsultationSubjectiveNotes ConsultationSubjectiveNotesObject { get; set; }
        public List<UserDetail> Doctors { get; set; }
        public ConsultationSubjectiveNoteModel()
        {
            ConsultationSubjectiveNotesObject = new ConsultationSubjectiveNotes();
        }
    }
}