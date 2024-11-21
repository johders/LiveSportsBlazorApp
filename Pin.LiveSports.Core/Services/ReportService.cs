using Pin.LiveSports.Core.Services.Interfaces;

namespace Pin.LiveSports.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly List<string> _messages = new();

        public List<string> Messages()
        {
            return _messages;
        }

        public void AddAnnouncement(string message)
        {
            _messages.Add(message);
        }
       
    }
}
