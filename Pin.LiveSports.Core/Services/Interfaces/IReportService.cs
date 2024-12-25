using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Core.Services.Interfaces
{
    public interface IReportService
    {
        Matchup GetMatchup();
        List<ReportEventLog> GetEventLogs();
        void SetMatchup(Matchup matchup);
        void InsertEventLog(ReportEventLog eventLog);
        void LogYellowCard(string teamName, string playerName, int minute);
        void LogRedCard(string teamName, string playerName, int minute);
    }
}
