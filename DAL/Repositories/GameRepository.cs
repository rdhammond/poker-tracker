using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface IGameRepository : IReadOnlyRepository<GameDao>
    { }

    public class GameRepository : ReadOnlyRepository<GameDao>, IGameRepository
    {
        public GameRepository(IDatabase database)
            : base(database)
        { }
    }
}
