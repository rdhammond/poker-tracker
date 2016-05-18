using System;

namespace PokerTracker.DAL.DAO
{
    public class SwingDao : IDao
    {
        public int BiggestSwing { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
