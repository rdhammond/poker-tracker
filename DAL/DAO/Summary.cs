﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.DAL.DAO
{
    public class Summary
    {
        public string Cardroom { get; set; }
        public string Game { get; set; }
        public string Limit { get; set; }
        public int HoursPlayed { get; set; }
        public int DayOfMonth { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int WinLoss { get; set; }
        public Decimal WinLossBB { get; set; }
        public int HourlyRate { get; set; }
        public Decimal HourlyRateBB { get; set; }
    }
}
