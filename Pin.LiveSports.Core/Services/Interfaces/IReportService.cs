using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Core.Services.Interfaces
{
    public interface IReportService
    {
        List<string> Messages();
        void AddAnnouncement(string message);
        void GetMatchup(Matchup matchup);
    }
}
