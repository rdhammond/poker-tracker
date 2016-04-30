using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.BLL.Objects
{
    [DataContract]
    public class Summary
    {
        [DataMember]
        public Guid SessionId { get; set; }

        [DataMember]
        public string Cardroom { get; set; }

        [DataMember]
        public string Game { get; set; }

        [DataMember]
        public string Limit { get; set; }

        [DataMember]
        public int HoursPlayed { get; set; }

        [DataMember]
        public int DayOfMonth { get; set; }

        [DataMember]
        public string DayOfWeek { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public int WinLoss { get; set; }

        [DataMember]
        public decimal WinLossBB { get; set; }

        [DataMember]
        public int HourlyRate { get; set; }

        [DataMember]
        public decimal HourlyRateBB { get; set; }
    }
}
