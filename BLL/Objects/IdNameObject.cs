using System.Runtime.Serialization;

namespace PokerTracker.BLL.Objects
{
    [DataContract]
    public abstract class IdNameObject : IdObject
    {
        [DataMember]
        public string Name { get; set; }
    }
}
