using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    [DataContract]
    public class Statistics
    {
        [DataMember]
        public decimal AvgHourlyRatePerSession { get; set; }

        [DataMember]
        public decimal HourlyRateVariance { get; set; }

        [DataMember]
        public decimal HourlyRateStdDev { get; set; }

        [DataMember]
        public decimal TotalHourlyRate { get; set; }

        [DataMember]
        public decimal TotalHoursPlayed { get; set; }
    }
}
