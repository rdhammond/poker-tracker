using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.Tests.Comparers.Objects
{
    public class StatisticsComparer : DaoObjectComparer<StatisticsDao, Statistics>
    {
        public override int GetHashCode(Statistics obj)
        {
            return (int)(
                obj.AvgHourlyRatePerSession
                + obj.HourlyRateStdDev
                + obj.HourlyRateVariance
                + obj.TotalHourlyRate
                + obj.TotalHoursPlayed
            );
        }

        public override bool Equals(Statistics x, Statistics y)
        {
            return x.AvgHourlyRatePerSession == y.AvgHourlyRatePerSession
                && x.HourlyRateStdDev == y.HourlyRateStdDev
                && x.HourlyRateVariance == y.HourlyRateVariance
                && x.TotalHourlyRate == y.TotalHourlyRate
                && x.TotalHoursPlayed == y.TotalHoursPlayed;
        }

        public override bool Equals(StatisticsDao x, Statistics y)
        {
            return x.AvgHourlyRatePerSession == y.AvgHourlyRatePerSession
                && x.HourlyRateStdDev == y.HourlyRateStdDev
                && x.HourlyRateVariance == y.HourlyRateVariance
                && x.TotalHourlyRate == y.TotalHourlyRate
                && x.TotalHoursPlayed == y.TotalHoursPlayed;
        }

        public override bool Equals(Statistics x, StatisticsDao y)
        {
            return x.AvgHourlyRatePerSession == y.AvgHourlyRatePerSession
                && x.HourlyRateStdDev == y.HourlyRateStdDev
                && x.HourlyRateVariance == y.HourlyRateVariance
                && x.TotalHourlyRate == y.TotalHourlyRate
                && x.TotalHoursPlayed == y.TotalHoursPlayed;
        }
    }
}
