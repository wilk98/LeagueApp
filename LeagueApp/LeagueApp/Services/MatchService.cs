using LeagueApp.Data;
using LeagueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Services;

public class MatchService : IMatchService
{
    private readonly ApplicationDbContext _context;

    public MatchService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Match>> GetAllMatchesAsync()
    {
        return await _context.Matches.Include(m => m.HomeTeam).Include(m => m.AwayTeam).ToListAsync();
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
        return match;
    }

    public async Task UpdateMatchAsync(Match match)
    {
        _context.Entry(match).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMatchAsync(int id)
    {
        var match = await _context.Matches.FindAsync(id);
        if (match != null)
        {
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
        }
    }
}
