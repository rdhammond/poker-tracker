using AsyncPoco;

namespace PokerTracker.DAL.DAO
{
    [TableName("Games")]
    [PrimaryKey("Id")]
    public class GameDao : IdNameDao
    { }
}
