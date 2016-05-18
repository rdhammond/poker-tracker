using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using System.Collections.Generic;

namespace PokerTracker.Tests.Comparers.Dao
{
    public class SwingComparer : EqualityComparer<SwingDao>
    {
        public override int GetHashCode(SwingDao obj)
        {
            return obj.BiggestSwing
                + obj.StartTime.GetHashCode()
                + obj.EndTime.GetHashCode();
        }

        public override bool Equals(SwingDao x, SwingDao y)
        {
            return x.BiggestSwing == y.BiggestSwing
                && x.StartTime == y.StartTime
                && x.EndTime == y.EndTime;
        }
    }
}
