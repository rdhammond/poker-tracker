using System;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;

namespace PokerTracker.Tests.Comparers.Objects
{
    public class IdNameComparer<TDao,TObject> : DaoObjectComparer<TDao,TObject>
        where TDao : IdNameDao
        where TObject : IdNameObject
    {
        public override int GetHashCode(TObject obj)
        {
            return obj.Id.GetHashCode();
        }

        public override bool Equals(TObject x, TObject y)
        {
            return x.Id == y.Id
                && x.Name == y.Name;
        }

        public override bool Equals(TDao x, TObject y)
        {
            return x.Id == y.Id
                && x.Name == y.Name;
        }

        public override bool Equals(TObject x, TDao y)
        {
            return x.Id == y.Id
                && x.Name == y.Name;
        }
    }
}
