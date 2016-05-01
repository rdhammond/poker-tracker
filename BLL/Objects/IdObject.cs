using System;
using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    public abstract class IdObject
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
