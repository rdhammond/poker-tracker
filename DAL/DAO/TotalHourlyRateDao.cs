using PokerTracker.DAL.Attributes;

namespace PokerTracker.DAL.DAO
{
    [TableName("vw_TotalHourlyRate")]
    public class TotalHourlyRateDao : IDao
    {
        public decimal TotalHourlyRate { get; set; }
    }
}
