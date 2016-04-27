using AutoMapper;
using PokerTracker.BLL;

namespace PokerTracker.Tests.BLL
{
    public static class GlobalMapper
    {
        public static readonly IMapper Mapper = new MapperConfiguration(
            c => c.AddProfile<BLLProfile>()
        )
            .CreateMapper();
    }
}
