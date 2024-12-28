using Pin.LiveSports.Core.Dtos;
using Pin.LiveSports.Core.Models;
using Pin.LiveSports.Core.Services.Interfaces;

namespace Pin.LiveSports.Core.Services
{
    public class ReportService : IReportService
    {
        private Matchup _matchup = new();

        private List<Matchup> _history = new();

        public event Action? OnMatchupHistoryUpdated;

        public List<ReportEventLog> GetEventLogs()
        {
            return _matchup.EventLogs;
        }
        public Matchup GetMatchup()
        {
            return _matchup;
        }

        public void StartGame()
        {
            _matchup.HasStarted = true;
        }

        public void AddToHistory(Matchup matchup)
        {
            matchup.Id = _history.Count + 1;
            matchup.EndTime = DateTime.Now.ToString();
            matchup.HasStarted = false;
            matchup.HasFinished = true;
            matchup.IsLive = false;
            _history.Add(matchup);
            OnMatchupHistoryUpdated?.Invoke();
        }

        public List<Matchup> GetTodaysGames()
        {
            return _history;
        }
        public void SetMatchup(Matchup matchup)
        {
            matchup.IsLive = true;
            _matchup = matchup;
        }

        public void InsertEventLog(ReportEventLog eventLog)
        {
            eventLog.Id = Guid.NewGuid();
            _matchup.EventLogs.Insert(0, eventLog);
        }

        public void LogYellowCard(string teamName, string playerName, int minute)
        {
            var team = GetTeamByName(teamName);
            if (team == null) return;

            team.IssueYellowCard(playerName);

            var log = new ReportEventLog
            {
                Minute = minute,
                Type = "Yellow Card",
                Team = teamName,
                Player = playerName,
                Description = $"{playerName} received a yellow card.",
                Details = $"Yellow card issued to {playerName} at minute {minute}.",
            };
        }

        public void LogRedCard(string teamName, string playerName, int minute)
        {
            var team = GetTeamByName(teamName);
            if (team == null) return;

            team.IssueRedCard(playerName);

            var log = new ReportEventLog
            {
                Minute = minute,
                Type = "Red Card",
                Team = teamName,
                Player = playerName,
                Description = $"{playerName} received a red card and is disqualified.",
                Details = $"Red card issued to {playerName} at minute {minute}.",
            };
        }

        public void LogSubstitution(string teamName, string playerIn, string playerOut, int minute)
        {
            var team = GetTeamByName(teamName);
            if (team == null) return;

            team.PerformSubstitution(playerIn, playerOut);

            var log = new ReportEventLog
            {
                Minute = minute,
                Type = "Substitution",
                Team = teamName,
                PlayerIn = playerIn,
                PlayerOut = playerOut,
                Description = $"Substitution: {playerIn} replaced {playerOut}.",
                Details = $"{playerIn} subbed in for {playerOut} at minute {minute}.",
            };
        }

        public void LogGoal(string teamName, string playerName, int minute)
        {
            var team = GetTeamByName(teamName);
            if (team == null) return;

            if (teamName == _matchup.TeamA.Name)
                _matchup.TeamAScore++;
            else if (teamName == _matchup.TeamB.Name)
                _matchup.TeamBScore++;

            team.IncrementScoredGoals(playerName);
            _matchup.SnapshotScores();

            var log = new ReportEventLog
            {
                Minute = minute,
                Type = "Goal",
                Team = teamName,
                Player = playerName,
                Description = $"{playerName} scored a goal!",
                Details = $"Goal by {playerName} at minute {minute}.",
            };
        }

        private Team? GetTeamByName(string teamName)
        {
            if (_matchup.TeamA.Name == teamName) return _matchup.TeamA;
            if (_matchup.TeamB.Name == teamName) return _matchup.TeamB;
            return null;
        }
    }
}
