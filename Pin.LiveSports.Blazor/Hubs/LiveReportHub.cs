using Microsoft.AspNetCore.SignalR;
using Pin.LiveSports.Core.Models;
using Pin.LiveSports.Core.Services.Interfaces;
using System.Linq.Expressions;

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
        public async Task BroadcastMinute(int currentMinute)
        {
            await Clients.Others.SendAsync("UpdateMinute", currentMinute);
        }
        public async Task SendEventLog(ReportEventLog eventLog)
        {
            _reportService.InsertEventLog(eventLog);
            await Clients.Others.SendAsync("ReceiveEventLog", eventLog);
        }

        public async Task SendYellowCardLog(ReportEventLog eventLog)
        {
            _reportService.LogYellowCard(eventLog.Team, eventLog.Player, eventLog.Minute);
            _reportService.InsertEventLog(eventLog);
            await Clients.Others.SendAsync("ReceiveEventLog", eventLog);
        }

        public async Task SendRedCardLog(ReportEventLog eventLog)
        {
            _reportService.LogRedCard(eventLog.Team, eventLog.Player, eventLog.Minute);
            _reportService.InsertEventLog(eventLog);
            await Clients.Others.SendAsync("ReceiveEventLog", eventLog);
        }

        public async Task SendScoreUpdate(string teamName, int teamAScore, int teamBScore)
        {
            await Clients.Others.SendAsync("ReceiveScoreUpdate", teamName, teamAScore, teamBScore);
        }


    }
}
