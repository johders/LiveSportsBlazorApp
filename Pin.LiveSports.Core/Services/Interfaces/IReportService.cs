namespace Pin.LiveSports.Core.Services.Interfaces
{
    public interface IReportService
    {
        List<string> Messages();
        void AddAnnouncement(string message);
    }
}
