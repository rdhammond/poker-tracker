using AutoMapper;
using PokerTracker.BLL;

namespace PokerTracker.Tests.BLL
{
    public static class MapperFactory
    {
        public static IMapper Create()
        {
            return new MapperConfiguration(c => c.AddProfile<BLLProfile>())
                .CreateMapper();
        }
    }
}
