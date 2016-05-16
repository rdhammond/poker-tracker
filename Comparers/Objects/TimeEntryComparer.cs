using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.Tests.Comparers.Objects
{
    public class TimeEntryComparer : DaoObjectComparer<TimeEntryDao, TimeEntry>
    {
        public override int GetHashCode(TimeEntry obj)
        {
            return obj.Id.GetHashCode();
        }

        public override bool Equals(TimeEntry x, TimeEntry y)
        {
            return x.DealerTokes == y.DealerTokes
                && x.Id == y.Id
                && x.RecordedAt == y.RecordedAt
                && x.ServerTips == y.ServerTips
                && x.StackSize == y.StackSize;
        }

        public override bool Equals(TimeEntryDao x, TimeEntry y)
        {
            return x.DealerTokes == y.DealerTokes
                && x.Id == y.Id
                && x.RecordedAt == y.RecordedAt
                && x.ServerTips == y.ServerTips
                && x.StackSize == y.StackSize;
        }

        public override bool Equals(TimeEntry x, TimeEntryDao y)
        {
            return x.DealerTokes == y.DealerTokes
                && x.Id == y.Id
                && x.RecordedAt == y.RecordedAt
                && x.ServerTips == y.ServerTips
                && x.StackSize == y.StackSize;
        }
    }
}
