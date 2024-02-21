using LeagueApp.Data;
using LeagueApp.Models;
using LeagueApp.Services;
using LeagueApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionString"));
});

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<ITeamStatsService, TeamStatsService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Leagues}/{action=Index}/{id?}");

app.Run();
