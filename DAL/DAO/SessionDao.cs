using AsyncPoco;
using System;

namespace PokerTracker.DAL.DAO
{
    [TableName("Sessions")]
    [PrimaryKey("Id")]
    public class SessionDao : IdDao
    {
        public Guid CardRoomId { get; set; }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Notes { get; set; }
        public decimal? HoursActive { get; set; }
        public Guid GameId { get; set; }
    }
}
