using System;

namespace PokerTracker.BLL.Objects
{
    public abstract class IdNameObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
