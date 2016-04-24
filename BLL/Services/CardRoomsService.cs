using PokerTracker.BLL.Objects;
using PokerTracker.DAL.Repositories;
using AutoMapper;
using PokerTracker.DAL.DAO;

namespace PokerTracker.BLL.Services
{
    public interface ICardRoomsService : IIdNameService<CardRoomDao, CardRoom, ICardRoomRepository>
    {
    }

    public class CardRoomsService : IdNameService<CardRoomDao, CardRoom, ICardRoomRepository>, ICardRoomsService
    {
        public CardRoomsService(IMapper mapper, ICardRoomRepository repository)
            : base(mapper, repository)
        { }
    }
}
