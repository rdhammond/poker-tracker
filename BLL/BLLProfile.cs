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
            CreateMap<CardRoom, CardRoomDao>();
            CreateMap<Game, GameDao>();
            CreateMap<TimeEntry, TimeEntryDao>();

            CreateMap<Session, SessionDao>()
                .ForMember(dest => dest.CardRoomId, m => m.MapFrom(src => src.CardRoom.Id))
                .ForMember(dest => dest.GameId, m => m.MapFrom(src => src.Game.Id));
        }
    }
}
