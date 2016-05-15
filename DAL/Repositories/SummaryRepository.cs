﻿using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;

namespace PokerTracker.DAL.Repositories
{
    public interface ISummaryRepository : IRepository<SummaryDao>
    { }

    public class SummaryRepository : Repository<SummaryDao>, ISummaryRepository
    {
        public SummaryRepository(IDatabase database)
            : base(database)
        { }
    }
}
