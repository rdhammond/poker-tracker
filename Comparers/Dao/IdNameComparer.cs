using PokerTracker.DAL.DAO;
using System.Collections.Generic;

namespace PokerTracker.Tests.Comparers.Dao
{
    public class IdNameComparer<T> : EqualityComparer<T>
        where T : IdNameDao
    {
        public override int GetHashCode(T obj)
        {
            return obj.Id.GetHashCode();
        }

        public override bool Equals(T x, T y)
        {
            return x.Id == y.Id
                && x.Name == y.Name;
        }
    }
}
