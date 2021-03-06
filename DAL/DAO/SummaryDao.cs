﻿using PokerTracker.DAL.Attributes;
using System;

namespace PokerTracker.DAL.DAO
{
    [TableName("vw_Summaries")]
    public class SummaryDao : IDao
    {
        [IdField] public Guid SessionId { get; set; }
        public string Cardroom { get; set; } 
        public string Game { get; set; }
        public string Limit { get; set; }
        public decimal HoursPlayed { get; set; }
        public int DayOfMonth { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int WinLoss { get; set; }
        public decimal WinLossBB { get; set; }
        public int HourlyRate { get; set; }
        public decimal HourlyRateBB { get; set; }
    }
}
