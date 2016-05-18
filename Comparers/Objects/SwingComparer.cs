using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;

namespace PokerTracker.Tests.Comparers.Objects
{
    public class SwingComparer : DaoObjectComparer<SwingDao,Swing>
    {
        public override int GetHashCode(Swing obj)
        {
            return obj.BiggestSwing
                + obj.StartTime.GetHashCode()
                + obj.EndTime.GetHashCode();
        }

        public override bool Equals(Swing x, Swing y)
        {
            return x.BiggestSwing == y.BiggestSwing
                && x.StartTime == y.StartTime
                && x.EndTime == y.EndTime;
        }

        public override bool Equals(SwingDao x, Swing y)
        {
            return x.BiggestSwing == y.BiggestSwing
                && x.StartTime == y.StartTime
                && x.EndTime == y.EndTime;
        }

        public override bool Equals(Swing x, SwingDao y)
        {
            return x.BiggestSwing == y.BiggestSwing
                && x.StartTime == y.StartTime
                && x.EndTime == y.EndTime;
        }
    }
}
