using AsyncPoco;

namespace PokerTracker.DAL.DAO
{
    [TableName("CardRooms")]
    [PrimaryKey("Id")]
    public class CardRoomDao : IdNameDao
    { }
}
