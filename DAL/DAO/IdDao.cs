using PokerTracker.DAL.Attributes;
using System;

namespace PokerTracker.DAL.DAO
{
    public class IdDao : IDao
    {
        [IdField] public Guid Id { get; set; }
    }
}
