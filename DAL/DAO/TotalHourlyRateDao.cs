using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.DAL.DAO
{
    [TableName("vw_TotalHourlyRate")]
    public class TotalHourlyRateDao
    {
        [ResultColumn]
        public decimal TotalHourlyRate { get; set; }
    }
}
