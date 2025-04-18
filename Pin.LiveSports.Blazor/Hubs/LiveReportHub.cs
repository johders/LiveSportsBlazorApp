﻿using Microsoft.AspNetCore.SignalR;
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
        public async Task BroadcastMinute()
        {
            _reportService.AddMinute();
            await Clients.Others.SendAsync("UpdateMinute", _reportService.GetMatchup().CurrentMinute);
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

        public async Task SendSubstitutionLog(ReportEventLog eventLog)
        {
            _reportService.LogSubstitution(eventLog.Team, eventLog.PlayerIn, eventLog.PlayerOut, eventLog.Minute);
            _reportService.InsertEventLog(eventLog);
            await Clients.Others.SendAsync("ReceiveEventLog", eventLog);
        }

        public async Task SendGoalLog(ReportEventLog eventLog)
        {
            _reportService.InsertEventLog(eventLog);
            _reportService.LogGoal(eventLog.Team, eventLog.Player, eventLog.Minute);
            await Clients.Others.SendAsync("ReceiveEventLog", eventLog);
        }

        public async Task StartGame(ReportEventLog eventLog)
        {
            _reportService.StartGame();
            _reportService.InsertEventLog(eventLog);
            await Clients.Others.SendAsync("ReceiveStartGame", eventLog);
        }

        public async Task SendGameMarker(ReportEventLog eventLog)
        {
            _reportService.InsertEventLog(eventLog);
            if (eventLog.Type == "Fulltime")
            {
                _reportService.AddToHistory(_reportService.GetMatchup());
            }
            await Clients.Others.SendAsync("ReceiveGameMarker");
        }

        public async Task StopGame(Matchup matchup, ReportEventLog eventLog)
        {
            _reportService.InsertEventLog(eventLog);
            _reportService.AddToHistory(_reportService.GetMatchup());
            await Clients.Others.SendAsync("ReceiveEventLog", eventLog);
        }

        public async Task<Matchup?> CheckLiveGame()
        {
            var liveMatchup = _reportService.GetMatchup();
            return liveMatchup;
        }
    }
}
