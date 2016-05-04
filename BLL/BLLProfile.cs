using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            CreateMap<SummaryDao, Summary>();

            CreateMap<TimeEntry, TimeEntryDao>();
            CreateMap<Session, SessionDao>();
        }
    }
}
