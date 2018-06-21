using System.Text;

namespace OMC.Models
{
    public class TimezoneMaster : BaseEntity
    {
        public string ShortForm { get; set; }
        public string Timezone   { get; set; }
        public string Time { get; set; }

        public string TimezoneDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ShortForm);
                sb.Append(" ");
                sb.Append(Time);
                return sb.ToString();
            }
        }
    }
}
