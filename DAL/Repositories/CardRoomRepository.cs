﻿using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;

namespace PokerTracker.DAL.Repositories
{
    public interface ICardRoomRepository : IReadOnlyRepository<CardRoomDao>
    {
    }

    public class CardRoomRepository : ReadOnlyRepository<CardRoomDao>, ICardRoomRepository
    {
        public CardRoomRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
