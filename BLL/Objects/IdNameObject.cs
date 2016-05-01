using System;
using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    public abstract class IdNameObject : IdObject
    {
        [DataMember]
        public string Name { get; set; }
    }
}
