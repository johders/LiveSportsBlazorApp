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

        public void LogSubstitution(string teamName, string playerIn, string playerOut, int minute)
        {
            var team = GetTeamByName(teamName);
            if (team != null)
            {
                team.PerformSubstitution(playerIn, playerOut);
                EventLogs.Add(new ReportEventLog
                {
                    Minute = minute,
                    Type = "Substitution",
                    Team = teamName,
                    Player = $"{playerIn} in for {playerOut}",
                    Description = $"Substitution: {playerIn} replaced {playerOut}.",
                    Details = $"{playerIn} subbed in for {playerOut} at minute {minute}.",
                });
            }
        }

        public void LogGoal(string teamName, string playerName, int minute)
        {
            var team = GetTeamByName(teamName);
            if (team != null)
            {
                if (teamName == TeamA.Name)
                    TeamAScore++;
                else if (teamName == TeamB.Name)
                    TeamBScore++;

                EventLogs.Add(new ReportEventLog
                {
                    Minute = minute,
                    Type = "Goal",
                    Team = teamName,
                    Player = playerName,
                    Description = $"{playerName} scored a goal!",
                    Details = $"Goal by {playerName} at minute {minute}.",
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
