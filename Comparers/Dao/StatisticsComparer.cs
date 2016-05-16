using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using System.Collections.Generic;

namespace PokerTracker.Tests.Comparers.Dao
{
    public class StatisticsComparer : EqualityComparer<StatisticsDao>
    {
        public override int GetHashCode(StatisticsDao s)
        {
            return (int)(
                s.AvgHourlyRatePerSession
                + s.HourlyRateStdDev
                + s.HourlyRateVariance
                + s.TotalHourlyRate
                + s.TotalHoursPlayed
            );
        }

        public override bool Equals(StatisticsDao x, StatisticsDao y)
        {
            return x.AvgHourlyRatePerSession == y.AvgHourlyRatePerSession
                && x.HourlyRateStdDev == y.HourlyRateStdDev
                && x.HourlyRateVariance == y.HourlyRateVariance
                && x.TotalHourlyRate == y.TotalHourlyRate
                && x.TotalHoursPlayed == y.TotalHoursPlayed;
        }
    }
}
