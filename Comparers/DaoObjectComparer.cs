using PokerTracker.DAL.DAO;
using System.Collections.Generic;

namespace PokerTracker.Tests.Comparers
{
    public abstract class DaoObjectComparer<TDao,TObject> : EqualityComparer<TObject>
        where TDao : IDao
    {
        public abstract bool Equals(TDao x, TObject y);
        public abstract bool Equals(TObject x, TDao y);
    }
}
