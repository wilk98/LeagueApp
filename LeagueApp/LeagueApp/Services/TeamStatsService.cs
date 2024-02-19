using LeagueApp.Data;
using LeagueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Services;

public class TeamStatsService : ITeamStatsService
{
    private readonly ApplicationDbContext _context;

    public TeamStatsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TeamStats> GetTeamStatsByTeamIdAsync(int teamId)
    {
        return await _context.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == teamId);
    }

    public async Task UpdateStatsAfterMatch(int homeTeamId, int awayTeamId, int homeScore, int awayScore)
    {
        var homeTeamStats = await _context.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == homeTeamId);
        var awayTeamStats = await _context.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == awayTeamId);

        if (homeTeamStats != null && awayTeamStats != null)
        {
            // Aktualizacja meczów rozegranych
            homeTeamStats.MatchesPlayed++;
            awayTeamStats.MatchesPlayed++;

            // Aktualizacja bramek zdobytych i straconych
            homeTeamStats.GoalsFor += homeScore;
            homeTeamStats.GoalsAgainst += awayScore;

            awayTeamStats.GoalsFor += awayScore;
            awayTeamStats.GoalsAgainst += homeScore;

            // Obliczanie zwycięstw, remisów i porażek
            if (homeScore > awayScore) // Wygrana drużyny gospodarzy
            {
                homeTeamStats.Wins++;
                awayTeamStats.Losses++;
                homeTeamStats.Points += 3;
            }
            else if (homeScore < awayScore) // Wygrana drużyny gości
            {
                awayTeamStats.Wins++;
                homeTeamStats.Losses++;
                awayTeamStats.Points += 3;
            }
            else // Remis
            {
                homeTeamStats.Draws++;
                awayTeamStats.Draws++;
                homeTeamStats.Points += 1;
                awayTeamStats.Points += 1;
            }

            _context.Update(homeTeamStats);
            _context.Update(awayTeamStats);
            await _context.SaveChangesAsync();
        }
    }
    public async Task RevertStatsAfterMatchDeletion(int homeTeamId, int awayTeamId, int scoreHome, int scoreAway)
    {
        var homeTeamStats = await _context.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == homeTeamId);
        var awayTeamStats = await _context.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == awayTeamId);

        if (homeTeamStats != null && awayTeamStats != null)
        {
            homeTeamStats.MatchesPlayed--;
            awayTeamStats.MatchesPlayed--;

            homeTeamStats.GoalsFor -= scoreHome;
            homeTeamStats.GoalsAgainst -= scoreAway;

            awayTeamStats.GoalsFor -= scoreAway;
            awayTeamStats.GoalsAgainst -= scoreHome;

            if (scoreHome > scoreAway)
            {
                homeTeamStats.Wins--;
                homeTeamStats.Points -= 3;
                awayTeamStats.Losses--;
            }
            else if (scoreHome < scoreAway)
            {
                awayTeamStats.Wins--;
                awayTeamStats.Points -= 3;
                homeTeamStats.Losses--;
            }
            else
            {
                homeTeamStats.Draws--;
                awayTeamStats.Draws--;
                homeTeamStats.Points -= 1;
                awayTeamStats.Points -= 1;
            }

            _context.Update(homeTeamStats);
            _context.Update(awayTeamStats);
            await _context.SaveChangesAsync();
        }
    }

}
