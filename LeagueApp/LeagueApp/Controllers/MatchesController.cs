using Microsoft.AspNetCore.Mvc;
using LeagueApp.Models;
using LeagueApp.Services;

namespace LeagueApp.Controllers;

public class MatchesController : Controller
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    public async Task<IActionResult> Index(int leagueId)
    {
        var matches = await _matchService.GetAllMatchesAsync(leagueId);
        return View(matches);
    }

    [Route("Matches/GetByTeamId/{teamId}")]
    public async Task<IActionResult> GetByTeamId(int teamId)
    {
        var matches = await _matchService.GetMatchesByTeamIdAsync(teamId);
        if (matches == null)
        {
            return NotFound();
        }
        var formattedMatches = matches.Select(m => new
        {
            homeTeamName = m.HomeTeam.Name,
            awayTeamName = m.AwayTeam.Name,
            score = $"{m.ScoreHome} - {m.ScoreAway}",
            matchTime = m.MatchTime.ToString("g"),
            roundNumber = m.RoundNumber
        }).OrderByDescending(m => m.matchTime).ToList();

        return Json(formattedMatches);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("MatchId,HomeTeamId,AwayTeamId,ScoreHome,ScoreAway,MatchDate")] Match match)
    {
        if (ModelState.IsValid)
        {
            await _matchService.CreateMatchAsync(match);
            return RedirectToAction(nameof(Index));
        }
        return View(match);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var match = await _matchService.GetMatchByIdAsync(id);
        if (match == null)
        {
            return NotFound();
        }
        return View(match);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("MatchId,HomeTeamId,AwayTeamId,ScoreHome,ScoreAway,MatchDate")] Match match)
    {
        if (id != match.MatchId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _matchService.UpdateMatchAsync(match);
            return RedirectToAction(nameof(Index));
        }
        return View(match);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var match = await _matchService.GetMatchByIdAsync(id);
        if (match == null)
        {
            return NotFound();
        }
        return View(match);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _matchService.DeleteMatchAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
