using System;
using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    [DataContract]
    public class Swing
    {
        [DataMember]
        public int BiggestSwing { get; set; }

        [DataMember]
        public DateTime? StartTime { get; set; }

        [DataMember]
        public DateTime? EndTime { get; set; }
    }
}
