using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LeagueApp.Models;
using LeagueApp.Services;
using LeagueApp.Services.Interfaces;

namespace LeagueApp.Controllers;

public class TeamsController : Controller
{
    private readonly ITeamService _teamService;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<TeamsController> _logger;
    private readonly IUserService _userService;

    public TeamsController(ITeamService teamService, UserManager<User> userManager, ILogger<TeamsController> logger, IUserService userService)
    {
        _teamService = teamService;
        _userManager = userManager;
        _logger = logger;
        _userService = userService;
    }

    public async Task<IActionResult> Index(int leagueId)
    {
        _logger.LogInformation("Wywołanie metody Index dla ligi o ID: {LeagueId}", leagueId);
        var teams = await _teamService.GetAllTeamsAsync(leagueId);
        _logger.LogInformation("Pobrano {Count} drużyn dla ligi o ID: {LeagueId}", teams.Count(), leagueId);

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            _logger.LogInformation("Zalogowany użytkownik: {UserName}", user.UserName);
            var favoriteTeamIds = await _userService.GetUserFavoriteTeamIdsAsync(user.Id);
            _logger.LogInformation("Znaleziono {Count} ulubionych drużyn dla użytkownika {UserName}", favoriteTeamIds.Count, user.UserName);
            ViewBag.FavoriteTeamIds = favoriteTeamIds;
        }
        else
        {
            _logger.LogWarning("Nie znaleziono zalogowanego użytkownika");
            ViewBag.FavoriteTeamIds = new List<int>();
        }
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