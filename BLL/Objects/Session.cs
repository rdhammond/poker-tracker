using System;
using System.Collections.Generic;

namespace PokerTracker.BLL.Objects
{
    public class Session
    {
        private readonly List<TimeEntry> _timeEntries = new List<TimeEntry>();

        public List<TimeEntry> TimeEntries
        {
            get { return _timeEntries; }
        }

        public Guid Id { get; set; }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public DateTime StartTime { get; set; }

        public Game Game { get; set; }
        public CardRoom CardRoom { get; set; }
    }
}
