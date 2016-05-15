using PokerTracker.DAL.Attributes;
using System;

namespace PokerTracker.DAL.DAO
{
    [TableName("TimeEntries")]
    public class TimeEntryDao : IdDao
    {
        public Guid SessionId { get; set; }
        public DateTime RecordedAt { get; set; }
        public int StackSize { get; set; }
        public int? StackDifferential { get; set; }
        public int? DealerTokes { get; set; }
        public int? ServerTips { get; set; }
    }
}
