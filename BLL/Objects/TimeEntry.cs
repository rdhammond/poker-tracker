using System;

namespace PokerTracker.BLL.Objects
{
    public class TimeEntry
    {
        private readonly Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get { return _id; }
        }

        public DateTime RecordedAt { get; set; }
        public int StackSize { get; set; }
        public int DealerTokes { get; set; }
        public int ServerTips { get; set; }
    }
}
