using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorBoardModel
    {
        public DoctorBoard DoctorBoardObject { get; set; }
        public List<BoardMaster> BoardList { get; set; }
        public int UserId { get; set; }

        public DoctorBoardModel()
        {
            DoctorBoardObject = new DoctorBoard();
        }
    }
}