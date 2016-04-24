using System;
using System.Collections.Generic;

namespace PokerTracker.BLL.Objects
{
    public class Session
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IList<TimeEntry> _timeEntries = new List<TimeEntry>();

        public Guid Id
        {
             get { return _id; }
        }

        public IList<TimeEntry> TimeEntries
        {
            get { return _timeEntries; }
        }

        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public DateTime StartTime { get; set; }
        public string Notes { get; set; }

        public Game Game { get; set; }
        public CardRoom CardRoom { get; set; }
    }
}
