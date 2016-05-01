using System;
using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    [DataContract]
    public class TimeEntry : IdObject
    {
        [DataMember]
        public DateTime RecordedAt { get; set; }

        [DataMember]
        public int StackSize { get; set; }

        [DataMember]
        public int DealerTokes { get; set; }

        [DataMember]
        public int ServerTips { get; set; }
    }
}
