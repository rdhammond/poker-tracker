using System;

namespace PokerTracker.BLL.Objects
{
    public class TimeEntry
    {
        public Guid Id { get; set; }
        public DateTime RecordedAt { get; set; }
        public int StackSize { get; set; }
        public int DealerTokes { get; set; }
        public int ServerTips { get; set; }
    }
}
