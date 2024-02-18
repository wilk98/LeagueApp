using LeagueApp.Models;

namespace LeagueApp.Services;

public interface ILeagueService
{
    Task<IEnumerable<League>> GetAllLeaguesAsync();
    Task<League> GetLeagueByIdAsync(int id);
    Task<League> CreateLeagueAsync(League league);
    Task UpdateLeagueAsync(League league);
    Task DeleteLeagueAsync(int id);
}
