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
            _reportService.SetMatchup(matchup);
            await Clients.Others.SendAsync("ReceiveMatchup", matchup);
        }
        public async Task SendEventLog(ReportEventLog eventLog)
        {
            _reportService.SetEventLog(eventLog);
            await Clients.Others.SendAsync("ReceiveEventLog", eventLog);
        }

        public async Task BroadcastMinute(int currentMinute)
        {
            await Clients.Others.SendAsync("UpdateMinute", currentMinute);
        }

        public async Task SendScoreUpdate(string teamName, int teamAScore, int teamBScore)
        {
            await Clients.Others.SendAsync("ReceiveScoreUpdate", teamName, teamAScore, teamBScore);
        }
    }
}
