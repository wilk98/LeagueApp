using LeagueApp.Models;

namespace LeagueApp.Services;

public interface IMatchService
{
    Task<IEnumerable<Match>> GetAllMatchesAsync();
    Task<Match> GetMatchByIdAsync(int id);
    Task<Match> CreateMatchAsync(Match match);
    Task UpdateMatchAsync(Match match);
    Task DeleteMatchAsync(int id);
}
