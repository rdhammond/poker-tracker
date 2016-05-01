using AutoMapper;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;

namespace PokerTracker.BLL.Services
{
    public interface IGamesService : ILookupService<Game>
    { }

    public class GamesService
        : LookupService<GameDao, Game, IGameRepository>, IGamesService
    {
        public GamesService(IMapper mapper, IGameRepository repository)
            : base(mapper, repository)
        { }
    }
}
