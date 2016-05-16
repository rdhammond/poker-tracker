using PokerTracker.DAL.DAO;
using System.Collections.Generic;

namespace PokerTracker.Tests.Comparers.Dao
{
    public class SessionComparer : EqualityComparer<SessionDao>
    {
        public override int GetHashCode(SessionDao obj)
        {
            return obj.Id.GetHashCode();
        }

        public override bool Equals(SessionDao x, SessionDao y)
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
