using Pin.LiveSports.Core.Dtos;

namespace Pin.LiveSports.Core.Models
{
    public class Matchup
    {
        public int Id { get; set; }
        public Team TeamA { get; set; }
        public int TeamAScore { get; set; }
        public Team TeamB { get; set; }
        public int TeamBScore { get; set; }
        public int CurrentMinute { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool HasStarted { get; set; }
        public bool HasFinished { get; set; }
        public List<ReportEventLog> EventLogs { get; set; } = new List<ReportEventLog>();
        public Dictionary<string, (int TeamAScore, int TeamBScore)> ScoreSnapshots { get; set; }
    = new Dictionary<string, (int TeamAScore, int TeamBScore)>();

        public void SnapshotScores()
        {
            foreach (var log in EventLogs.Where(l => l.Type == "Goal"))
            {
                string logKey = log.Id.ToString();
                if (!ScoreSnapshots.ContainsKey(logKey))
                {
                    ScoreSnapshots[logKey] = (TeamAScore, TeamBScore);
                }
            }
        }
    }
}
