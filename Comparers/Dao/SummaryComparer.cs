using PokerTracker.DAL.DAO;
using System.Collections.Generic;

namespace PokerTracker.Tests.Comparers.Dao
{
    public class SummaryComparer : EqualityComparer<SummaryDao>
    {
        public override int GetHashCode(SummaryDao obj)
        {
            return obj.SessionId.GetHashCode();
        }

        public override bool Equals(SummaryDao x, SummaryDao y)
        {
            return x.Cardroom == y.Cardroom
                && x.DayOfMonth == y.DayOfMonth
                && x.DayOfWeek == y.DayOfWeek
                && x.EndTime == y.EndTime
                && x.Game == y.Game
                && x.HourlyRate == y.HourlyRate
                && x.HourlyRateBB == y.HourlyRateBB
                && x.HoursPlayed == y.HoursPlayed
                && x.Limit == y.Limit
                && x.SessionId == y.SessionId
                && x.StartTime == y.StartTime
                && x.WinLoss == y.WinLoss
                && x.WinLossBB == y.WinLossBB;
        }
    }
}
