using PokerTracker.DAL.DAO;
using System.Collections.Generic;

namespace PokerTracker.Tests.Comparers.Dao
{
    public class TimeEntryComparer : EqualityComparer<TimeEntryDao>
    {
        public override int GetHashCode(TimeEntryDao obj)
        {
            return obj.Id.GetHashCode();
        }

        public override bool Equals(TimeEntryDao x, TimeEntryDao y)
        {
            return x.DealerTokes == y.DealerTokes
                && x.Id == y.Id
                && x.RecordedAt == y.RecordedAt
                && x.ServerTips == y.ServerTips
                && x.SessionId == y.SessionId
                && x.StackDifferential == y.StackDifferential
                && x.StackSize == y.StackSize;
        }
    }
}
