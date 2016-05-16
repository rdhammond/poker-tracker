using PokerTracker.BLL.Objects;
using AutoMapper;
using PokerTracker.DAL.DAO;

namespace PokerTracker.BLL
{
    public class BLLProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<CardRoomDao, CardRoom>();
            CreateMap<GameDao, Game>();
            CreateMap<StatisticsDao, Statistics>();

            CreateMap<SummaryDao, Summary>()
                .AfterMap((dao, obj) => obj.Id = dao.SessionId);

            CreateMap<TimeEntry, TimeEntryDao>();
            CreateMap<Session, SessionDao>();
        }
    }
}
