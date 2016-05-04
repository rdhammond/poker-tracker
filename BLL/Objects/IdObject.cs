using System;
using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    [DataContract]
    public abstract class IdObject
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
