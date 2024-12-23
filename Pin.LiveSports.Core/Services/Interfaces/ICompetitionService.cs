using Pin.LiveSports.Core.Dtos;
using Pin.LiveSports.Core.Models;

namespace Pin.LiveSports.Core.Services.Interfaces
{
    public interface ICompetitionService
    {
        Task<ResultModel<IEnumerable<Team>>> GetAllTeamsAsync();
        Task<ResultModel<Matchup>> GenerateMatchupAsync();
    }
}
