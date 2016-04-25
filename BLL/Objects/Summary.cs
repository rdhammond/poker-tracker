using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.BLL.Objects
{
    public class Summary
    {
        public Guid SessionId { get; set; }
        public string Cardroom { get; set; }
        public string Game { get; set; }
        public string Limit { get; set; }
        public int HoursPlayed { get; set; }
        public int DayOfMonth { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int WinLoss { get; set; }
        public decimal WinLossBB { get; set; }
        public int HourlyRate { get; set; }
        public decimal HourlyRateBB { get; set; }
    }
}
