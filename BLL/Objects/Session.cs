using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    [DataContract]
    public class Session : IdObject
    {
        private List<TimeEntry> _timeEntries;

        [DataMember]
        public List<TimeEntry> TimeEntries
        {
            get
            {
                _timeEntries = _timeEntries ?? new List<TimeEntry>();
                return _timeEntries;
            }
        }

        [DataMember]
        public int SmallBlind { get; set; }

        [DataMember]
        public int BigBlind { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime? EndTime { get; set; }

        [DataMember]
        public decimal? HoursActive { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public Guid CardRoomId { get; set; }
    }
}
