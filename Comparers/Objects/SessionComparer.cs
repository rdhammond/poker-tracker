using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;

namespace PokerTracker.Tests.Comparers.Objects
{
    public class SessionComparer : DaoObjectComparer<SessionDao,Session>
    {
        public override int GetHashCode(Session obj)
        {
            return obj.Id.GetHashCode();
        }

        public override bool Equals(Session x, Session y)
        {
            return x.BigBlind == y.BigBlind
                && x.CardRoomId == y.CardRoomId
                && x.EndTime == y.EndTime
                && x.GameId == y.GameId
                && x.Id == y.Id
                && x.Notes == y.Notes
                && x.PercentOfTimePlayed == y.PercentOfTimePlayed
                && x.SmallBlind == y.SmallBlind
                && x.StartTime == y.StartTime;
        }

        public override bool Equals(SessionDao x, Session y)
        {
            return x.BigBlind == y.BigBlind
                && x.CardRoomId == y.CardRoomId
                && x.EndTime == y.EndTime
                && x.GameId == y.GameId
                && x.Id == y.Id
                && x.Notes == y.Notes
                && x.PercentOfTimePlayed == y.PercentOfTimePlayed
                && x.SmallBlind == y.SmallBlind
                && x.StartTime == y.StartTime;
        }

        public override bool Equals(Session x, SessionDao y)
        {
            return x.BigBlind == y.BigBlind
                && x.CardRoomId == y.CardRoomId
                && x.EndTime == y.EndTime
                && x.GameId == y.GameId
                && x.Id == y.Id
                && x.Notes == y.Notes
                && x.PercentOfTimePlayed == y.PercentOfTimePlayed
                && x.SmallBlind == y.SmallBlind
                && x.StartTime == y.StartTime;
        }
    }
}
