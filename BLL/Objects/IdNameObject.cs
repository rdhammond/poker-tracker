using System;
using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    public abstract class IdNameObject
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
