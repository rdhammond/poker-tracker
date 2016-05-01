using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.DAL.DAO
{
    [TableName("TimeEntries")]
    [PrimaryKey("Id")]
    public class TimeEntryDao : IdDao
    {
        public Guid SessionId { get; set; }
        public DateTime RecordedAt { get; set; }
        public int StackSize { get; set; }
        public int? StackDifferential { get; set; }
        public int? DealerTokes { get; set; }
        public int? ServerTips { get; set; }
    }
}
