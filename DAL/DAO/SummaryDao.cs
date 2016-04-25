using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.DAL.DAO
{
    [TableName("vw_Summaries")]
    public class SummaryDao
    {
        [ResultColumn] public Guid SessionId { get; set; }
        [ResultColumn] public string Cardroom { get; set; }
        [ResultColumn] public string Game { get; set; }
        [ResultColumn] public string Limit { get; set; }
        [ResultColumn] public int HoursPlayed { get; set; }
        [ResultColumn] public int DayOfMonth { get; set; }
        [ResultColumn] public DateTime StartTime { get; set; }
        [ResultColumn] public DateTime EndTime { get; set; }
        [ResultColumn] public int WinLoss { get; set; }
        [ResultColumn] public decimal WinLossBB { get; set; }
        [ResultColumn] public int HourlyRate { get; set; }
        [ResultColumn] public decimal HourlyRateBB { get; set; }
    }
}
