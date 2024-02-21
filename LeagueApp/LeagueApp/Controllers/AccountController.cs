using Microsoft.AspNetCore.Mvc;
using LeagueApp.Models;
using LeagueApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LeagueApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userService.LoginUserAsync(model);
            if (result.Succeeded)
            {
                var defaultReturnUrl = Url.Action(nameof(LeaguesController.Index), "Leagues");
                return LocalRedirect(defaultReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                var loginResult = await _userService.LoginUserAsync(new LoginViewModel { Email = model.Email, Password = model.Password });
                if (loginResult.Succeeded)
                {
                    return RedirectToAction(nameof(LeaguesController.Index), "Leagues");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();
            return RedirectToAction(nameof(LeaguesController.Index), "Leagues");
        }

        [Route("Account/ToggleFavoriteTeam/{teamId}")]
        [HttpPost]
        public async Task<IActionResult> ToggleFavoriteTeam(int teamId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("ToggleFavoriteTeam: User not found.");
                return Json(new { success = false, message = "Musisz być zalogowany, aby zmienić ulubione drużyny." });
            }

            var favoriteTeamIds = await _userService.GetUserFavoriteTeamIdsAsync(user.Id);
            var isFavorite = favoriteTeamIds.Contains(teamId);

            IdentityResult result;
            if (isFavorite)
            {
                _logger.LogInformation("Removing team {TeamId} from favorites for user {UserName}.", teamId, user.UserName);
                result = await _userService.RemoveTeamFromFavoritesAsync(user.Id, teamId);
            }
            else
            {
                _logger.LogInformation("Adding team {TeamId} to favorites for user {UserName}.", teamId, user.UserName);
                result = await _userService.AddTeamToFavoritesAsync(user.Id, teamId);
            }

            if (result.Succeeded)
            {
                _logger.LogInformation("ToggleFavoriteTeam: Team {TeamId} is now {State} for user {UserName}.", teamId, isFavorite ? "unfavorited" : "favorited", user.UserName);
                return Json(new { success = true, isFavorite = !isFavorite });
            }
            else
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Nie udało się zaktualizować ulubionych.";
                _logger.LogError("ToggleFavoriteTeam: Failed to update favorites for user {UserName}. Error: {ErrorMessage}", user.UserName, errorMessage);
                return Json(new { success = false, message = errorMessage });
            }
        }
    }
}
