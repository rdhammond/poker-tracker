using PokerTracker.DAL.Attributes;

namespace PokerTracker.DAL.DAO
{
    [TableName("vw_Statistics")]
    public class StatisticsDao : IDao
    {
        public decimal AvgHourlyRatePerSession { get; set; }
        public decimal HourlyRateVariance { get; set; }
        public decimal HourlyRateStdDev { get; set; }
        public decimal TotalHourlyRate { get; set; }
        public decimal TotalHoursPlayed { get; set; }
    }
}
