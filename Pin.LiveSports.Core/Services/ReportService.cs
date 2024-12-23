using Pin.LiveSports.Core.Models;
using Pin.LiveSports.Core.Services.Interfaces;

namespace Pin.LiveSports.Core.Services
{
    public class ReportService : IReportService
    {
        private Matchup _matchup = new();

        public List<ReportEventLog> GetEventLogs()
        {
            return _matchup.EventLogs;
        }
        public Matchup GetMatchup()
        {
            return _matchup;
        }
        public void SetMatchup(Matchup matchup)
        {
            _matchup = matchup;
        }

        public void SetEventLog(ReportEventLog eventLog)
        {
            _matchup.EventLogs.Insert(0, eventLog);
        }
    }
}
