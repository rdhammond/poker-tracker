using System;
using System.Collections.Generic;

namespace PokerTracker.BLL.Objects
{
    public class Session
    {
        private readonly IList<TimeEntry> _timeEntries;

        public IList<TimeEntry> TimeEntries
        {
            get { return _timeEntries; }
        }

        public Guid Id { get; set; }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Notes { get; set; }
        public int? MinutesActive { get; set; }

        public Game Game { get; set; }
        public CardRoom CardRoom { get; set; }
    }
}
