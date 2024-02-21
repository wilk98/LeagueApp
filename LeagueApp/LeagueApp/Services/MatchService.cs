using LeagueApp.Data;
using LeagueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Services;

public class MatchService : IMatchService
{
    private readonly ApplicationDbContext _context;
    private readonly ITeamStatsService _teamStatsService;

    public MatchService(ApplicationDbContext context, ITeamStatsService teamStatsService)
    {
        _context = context;
        _teamStatsService = teamStatsService;
    }

    public async Task<IEnumerable<Match>> GetAllMatchesAsync(int leagueId)
    {
        return await _context.Matches
            .Where(team => team.LeagueId == leagueId)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetMatchesByTeamIdAsync(int teamId)
    {
        return await _context.Matches
            .Where(team => team.AwayTeamId == teamId || team.HomeTeamId == teamId)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .ToListAsync();
    }

    public async Task<Match> GetMatchByIdAsync(int id)
    {
        return await _context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .FirstOrDefaultAsync(m => m.MatchId == id);
    }

    public async Task<Match> CreateMatchAsync(Match match)
    {
        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        await _teamStatsService.UpdateStatsAfterMatch(match.HomeTeamId, match.AwayTeamId, match.ScoreHome, match.ScoreAway);

        return match;
    }

    public async Task UpdateMatchAsync(Match match)
    {
        var existingMatch = await _context.Matches.AsNoTracking().FirstOrDefaultAsync(m => m.MatchId == match.MatchId);
        if (existingMatch != null)
        {
            _context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            if (existingMatch.ScoreHome != match.ScoreHome || existingMatch.ScoreAway != match.ScoreAway)
            {
                await _teamStatsService.UpdateStatsAfterMatch(match.HomeTeamId, match.AwayTeamId, match.ScoreHome, match.ScoreAway);
            }
        }
    }
    public async Task DeleteMatchAsync(int id)
    {
        var match = await _context.Matches.FindAsync(id);
        if (match != null)
        {
            int homeScore = match.ScoreHome;
            int awayScore = match.ScoreAway;
            int homeTeamId = match.HomeTeamId;
            int awayTeamId = match.AwayTeamId;

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            await _teamStatsService.RevertStatsAfterMatchDeletion(homeTeamId, awayTeamId, homeScore, awayScore);
        }
    }

}
