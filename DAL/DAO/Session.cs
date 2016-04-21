using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.DAL.DAO
{
    public class Session
    {
        public Guid? Id { get; set; }
        public Guid CardRoomId { get; set; }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Notes { get; set; }
        public int? MinutesActive { get; set; }
        public Guid GameId { get; set; }
    }
}
