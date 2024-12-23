using Pin.LiveSports.Core.Dtos;

namespace Pin.LiveSports.Core.Models
{
    public class Matchup
    {
        public Team TeamA { get; set; }
        public int TeamAScore { get; set; }
        public Team TeamB { get; set; }
        public int TeamBScore { get; set; }
        public List<ReportEventLog> EventLogs { get; set; } = new List<ReportEventLog>();
    }
}
