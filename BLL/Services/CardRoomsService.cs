using PokerTracker.BLL.Objects;
using PokerTracker.DAL.Repositories;
using AutoMapper;
using PokerTracker.DAL.DAO;

namespace PokerTracker.BLL.Services
{
    public interface ICardRoomsService : ILookupService<CardRoom>
    { }

    public class CardRoomsService
        : LookupService<CardRoomDao, CardRoom, ICardRoomRepository>, ICardRoomsService
    {
        public CardRoomsService(IMapper mapper, ICardRoomRepository repository)
            : base(mapper, repository)
        { }
    }
}
