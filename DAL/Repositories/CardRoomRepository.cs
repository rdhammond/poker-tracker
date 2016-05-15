using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface ICardRoomRepository : IReadOnlyRepository<CardRoomDao>
    { }

    public class CardRoomRepository : ReadOnlyRepository<CardRoomDao>, ICardRoomRepository
    {
        public CardRoomRepository(IDatabase database)
            : base(database)
        { }
    }
}
