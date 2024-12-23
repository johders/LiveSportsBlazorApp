﻿namespace Pin.LiveSports.Core.Models
{
    public class ReportEventLog
    {
        public int Minute { get; set; }
        public string Type { get; set; }
        public string Team { get; set; }
        public string Player { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
    }
}
