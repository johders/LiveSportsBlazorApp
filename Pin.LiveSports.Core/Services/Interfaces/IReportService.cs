using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Core.Services.Interfaces
{
    public interface IReportService
    {
        Matchup GetMatchup();
        List<ReportEventLog> GetEventLogs();
        void SetMatchup(Matchup matchup);
        void SetEventLog(ReportEventLog eventLog);
    }
}
