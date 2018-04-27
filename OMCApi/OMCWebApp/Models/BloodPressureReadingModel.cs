using OMC.Models;

namespace OMCWebApp.Models
{
    public class BloodPressureReadingModel
    {
        public ConsultationBloodPressureReading ConsultationBloodPressureReadingObject { get; set; }
        public BloodPressureReadingModel()
        {
            ConsultationBloodPressureReadingObject = new ConsultationBloodPressureReading();
        }
    }
}