using Microsoft.AspNetCore.Mvc;
using LeagueApp.Services;
using LeagueApp.Models;

namespace LeagueApp.Controllers;

public class TeamsController : Controller
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    public async Task<IActionResult> Index(int leagueId)
    {
        var teams = await _teamService.GetAllTeamsAsync(leagueId);
        return View(teams);
    }

    public async Task<IActionResult> Details(int id)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return View(team);
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("TeamId,Name")] Team team)
    {
        if (ModelState.IsValid)
        {
            await _teamService.CreateTeamAsync(team);
            return RedirectToAction(nameof(Index));
        }
        return View(team);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return View(team);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name")] Team team)
    {
        if (id != team.TeamId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _teamService.UpdateTeamAsync(team);
            return RedirectToAction(nameof(Index));
        }
        return View(team);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var team = await _teamService.GetTeamByIdAsync(id);
        if (team == null)
        {
            return NotFound();
        }
        return View(team);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _teamService.DeleteTeamAsync(id);
        return RedirectToAction(nameof(Index));
    }


}
