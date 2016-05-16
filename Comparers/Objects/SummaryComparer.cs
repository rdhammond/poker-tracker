using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using System.Collections.Generic;
using System;

namespace PokerTracker.Tests.Comparers.Objects
{
    public class SummaryComparer : DaoObjectComparer<SummaryDao,Summary>
    {
        public override int GetHashCode(Summary obj)
        {
            return obj.Id.GetHashCode();
        }

        public override bool Equals(Summary x, Summary y)
        {
            return x.Cardroom == y.Cardroom
                && x.DayOfMonth == y.DayOfMonth
                && x.DayOfWeek == y.DayOfWeek
                && x.EndTime == y.EndTime
                && x.Game == y.Game
                && x.HourlyRate == y.HourlyRate
                && x.HourlyRateBB == y.HourlyRateBB
                && x.HoursPlayed == y.HoursPlayed
                && x.Id == y.Id
                && x.Limit == y.Limit
                && x.StartTime == y.StartTime
                && x.WinLoss == y.WinLoss
                && x.WinLossBB == y.WinLossBB;
        }

        public override bool Equals(SummaryDao x, Summary y)
        {
            return x.Cardroom == y.Cardroom
                && x.DayOfMonth == y.DayOfMonth
                && x.DayOfWeek == y.DayOfWeek
                && x.EndTime == y.EndTime
                && x.Game == y.Game
                && x.HourlyRate == y.HourlyRate
                && x.HourlyRateBB == y.HourlyRateBB
                && x.HoursPlayed == y.HoursPlayed
                && x.SessionId == y.Id
                && x.Limit == y.Limit
                && x.StartTime == y.StartTime
                && x.WinLoss == y.WinLoss
                && x.WinLossBB == y.WinLossBB;
        }

        public override bool Equals(Summary x, SummaryDao y)
        {
            return x.Cardroom == y.Cardroom
                && x.DayOfMonth == y.DayOfMonth
                && x.DayOfWeek == y.DayOfWeek
                && x.EndTime == y.EndTime
                && x.Game == y.Game
                && x.HourlyRate == y.HourlyRate
                && x.HourlyRateBB == y.HourlyRateBB
                && x.HoursPlayed == y.HoursPlayed
                && x.Id == y.SessionId
                && x.Limit == y.Limit
                && x.StartTime == y.StartTime
                && x.WinLoss == y.WinLoss
                && x.WinLossBB == y.WinLossBB;
        }
    }
}
