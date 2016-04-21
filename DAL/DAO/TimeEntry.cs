using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.DAL.DAO
{
    public class TimeEntry
    {
        public Guid? Id { get; set; }
        public Guid SessionId { get; set; }
        public DateTime RecordedAt { get; set; }
        public int StackSize { get; set; }
        public int? StackDifferential { get; set; }
        public int? DealerTokes { get; set; }
        public int? ServerTips { get; set; }
    }
}
