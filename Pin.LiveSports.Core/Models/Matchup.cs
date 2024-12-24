using Pin.LiveSports.Core.Dtos;

namespace Pin.LiveSports.Core.Models
{
    public class Matchup
    {
        public Team TeamA { get; set; }
        public int TeamAScore { get; set; }
        public Team TeamB { get; set; }
        public int TeamBScore { get; set; }
        public int CurrentMinute { get; set; }
        public List<ReportEventLog> EventLogs { get; set; } = new List<ReportEventLog>();

        public void LogYellowCard(string teamName, string playerName, int minute)
        {
            var team = GetTeamByName(teamName);
            if (team != null)
            {
                team.IssueYellowCard(playerName);
                EventLogs.Add(new ReportEventLog
                {
                    Minute = minute,
                    Type = "Yellow Card",
                    Team = teamName,
                    Player = playerName,
                    Description = $"{playerName} received a yellow card.",
                    Details = $"Yellow card issued to {playerName} at minute {minute}.",
                });
            }
        }

        public void LogRedCard(string teamName, string playerName, int minute)
        {
            var team = GetTeamByName(teamName);
            if (team != null)
            {
                team.IssueRedCard(playerName);
                EventLogs.Add(new ReportEventLog
                {
                    Minute = minute,
                    Type = "Red Card",
                    Team = teamName,
                    Player = playerName,
                    Description = $"{playerName} received a red card and is disqualified.",
                    Details = $"Red card issued to {playerName} at minute {minute}.",
                });
            }
        }

        private Team? GetTeamByName(string teamName)
        {
            if (TeamA.Name == teamName) return TeamA;
            if (TeamB.Name == teamName) return TeamB;
            return null;
        }
    }
}
