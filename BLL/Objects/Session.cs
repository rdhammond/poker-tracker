using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    [DataContract]
    public class Session : IdObject
    {
        private readonly List<TimeEntry> _timeEntries = new List<TimeEntry>();

        [DataMember]
        public List<TimeEntry> TimeEntries
        {
            get { return _timeEntries; }
        }

        [DataMember]
        public int SmallBlind { get; set; }

        [DataMember]
        public int BigBlind { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public Game Game { get; set; }

        [DataMember]
        public CardRoom CardRoom { get; set; }
    }
}
