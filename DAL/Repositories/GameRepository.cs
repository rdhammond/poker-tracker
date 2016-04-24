using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;

namespace PokerTracker.DAL.Repositories
{
    public interface IGameRepository : IReadOnlyRepository<GameDao>
    {
    }

    public class GameRepository : ReadOnlyRepository<GameDao>, IGameRepository
    {
        public GameRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
