using LeagueApp.Models;

namespace LeagueApp.Services;

public interface ITeamStatsService
{
    Task<TeamStats> GetTeamStatsByTeamIdAsync(int teamId);
    Task UpdateStatsAfterMatch(int homeTeamId, int awayTeamId, int homeScore, int awayScore);
    Task RevertStatsAfterMatchDeletion(int homeTeamId, int awayTeamId, int scoreHome, int scoreAway);

}
