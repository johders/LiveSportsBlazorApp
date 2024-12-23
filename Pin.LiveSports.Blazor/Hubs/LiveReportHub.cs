using Microsoft.AspNetCore.SignalR;
using Pin.LiveSports.Core.Models;
using Pin.LiveSports.Core.Services.Interfaces;

namespace Pin.LiveSports.Blazor.Hubs
{
    public class LiveReportHub : Hub
    {
        private readonly IReportService _reportService;

        public LiveReportHub(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task SendMatchup(Matchup matchup)
        {
            _reportService.GetMatchup(matchup);
            await Clients.Others.SendAsync("ReceiveMatchup", matchup);
        }

        public async Task SendMessage(string name, string message)
        {
            _reportService.AddAnnouncement($"{name}: {message}");
            await Clients.Others.SendAsync("ReceiveMessage", name, message);
        }
    }
}
