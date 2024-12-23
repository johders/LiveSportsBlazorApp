using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Core.Services.Interfaces
{
    public interface IReportService
    {
        List<string> GetMessages();
        Matchup GetMatchup();
        List<ReportEventLog> GetEventLogs();
        void SetAnnouncement(string message);
        void SetMatchup(Matchup matchup);
        void SetEventLog(ReportEventLog eventLog);
    }
}
