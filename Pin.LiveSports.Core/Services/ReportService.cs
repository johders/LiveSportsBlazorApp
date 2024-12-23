using Pin.LiveSports.Core.Models;
using Pin.LiveSports.Core.Services.Interfaces;

namespace Pin.LiveSports.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly List<string> _messages = new();
        private Matchup _matchup = new();

        public List<string> Messages()
        {
            return _messages;
        }

        public void AddAnnouncement(string message)
        {
            _messages.Add(message);
        }

        public void GetMatchup(Matchup matchup)
        {
            _matchup = matchup;
        }

    }
}
