using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Core.Services.Interfaces
{
    public interface IReportService
    {
        event Action? OnMatchupHistoryUpdated;

        Matchup GetMatchup();
        List<ReportEventLog> GetEventLogs();
        void SetMatchup(Matchup matchup);
        void InsertEventLog(ReportEventLog eventLog);
        void LogYellowCard(string teamName, string playerName, int minute);
        void LogRedCard(string teamName, string playerName, int minute);
        void LogSubstitution(string teamName, string playerIn, string playerOut, int minute);
        void LogGoal(string teamName, string playerName, int minute);
        void StartGame();
        List<Matchup> GetTodaysGames();
        void AddToHistory(Matchup matchup);
        void AddMinute();
    }
}
