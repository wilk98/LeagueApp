using LeagueApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<TeamStats> TeamStats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfiguracja relacji jeden-do-wielu dla HomeTeam
        modelBuilder.Entity<Match>()
            .HasOne(m => m.HomeTeam)
            .WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        // Konfiguracja relacji jeden-do-wielu dla AwayTeam
        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<League>().HasData(new League { LeagueId = 1, Name = "Liga Belgijska" });

        modelBuilder.Entity<Team>().HasData(
            // Liga Belgijska - Drużyny
            new Team { TeamId = 1, LeagueId = 1, Name = "Westerlo" },
            new Team { TeamId = 2, LeagueId = 1, Name = "Cercle Brugge" },
            new Team { TeamId = 3, LeagueId = 1, Name = "Anderlecht" },
            new Team { TeamId = 4, LeagueId = 1, Name = "Oostende" },
            new Team { TeamId = 5, LeagueId = 1, Name = "KV Mechelen" },
            new Team { TeamId = 6, LeagueId = 1, Name = "Antwerp" },
            new Team { TeamId = 7, LeagueId = 1, Name = "Club Brugge" },
            new Team { TeamId = 8, LeagueId = 1, Name = "Genk" },
            new Team { TeamId = 9, LeagueId = 1, Name = "St. Truiden" },
            new Team { TeamId = 10, LeagueId = 1, Name = "Royale Union SG" },
            new Team { TeamId = 11, LeagueId = 1, Name = "Kortrijk" },
            new Team { TeamId = 12, LeagueId = 1, Name = "Leuven" },
            new Team { TeamId = 13, LeagueId = 1, Name = "Waregem" },
            new Team { TeamId = 14, LeagueId = 1, Name = "Seraing" },
            new Team { TeamId = 15, LeagueId = 1, Name = "Charleroi" },
            new Team { TeamId = 16, LeagueId = 1, Name = "Eupen" },
            new Team { TeamId = 17, LeagueId = 1, Name = "St. Liege" },
            new Team { TeamId = 18, LeagueId = 1, Name = "Gent" });

        modelBuilder.Entity<Match>().HasData(
            // Pierwsza kolejka
            new Match { MatchId = 1, HomeTeamId = 1, AwayTeamId = 2, ScoreHome = 2, ScoreAway = 0, MatchTime = new DateTime(2022, 7, 24, 21, 0, 0), LeagueId = 1, IsBest = false, RoundNumber = 1 },
            new Match { MatchId = 2, HomeTeamId = 3, AwayTeamId = 4, ScoreHome = 2, ScoreAway = 0, MatchTime = new DateTime(2022, 7, 24, 18, 30, 0), LeagueId = 1, IsBest = false, RoundNumber = 1 },
            new Match { MatchId = 3, HomeTeamId = 5, AwayTeamId = 6, ScoreHome = 0, ScoreAway = 2, MatchTime = new DateTime(2022, 7, 24, 16, 0, 0), LeagueId = 1, IsBest = false, RoundNumber = 1 },
            new Match { MatchId = 4, HomeTeamId = 7, AwayTeamId = 8, ScoreHome = 3, ScoreAway = 2, MatchTime = new DateTime(2022, 7, 24, 13, 30, 0), LeagueId = 1, IsBest = true, RoundNumber = 1 },
            new Match { MatchId = 5, HomeTeamId = 9, AwayTeamId = 10, ScoreHome = 1, ScoreAway = 1, MatchTime = new DateTime(2022, 7, 23, 20, 45, 0), LeagueId = 1, IsBest = false, RoundNumber = 1 },
            new Match { MatchId = 6, HomeTeamId = 11, AwayTeamId = 12, ScoreHome = 0, ScoreAway = 2, MatchTime = new DateTime(2022, 7, 23, 18, 15, 0), LeagueId = 1, IsBest = false, RoundNumber = 1 },
            new Match { MatchId = 7, HomeTeamId = 13, AwayTeamId = 14, ScoreHome = 2, ScoreAway = 0, MatchTime = new DateTime(2022, 7, 23, 18, 15, 0), LeagueId = 1, IsBest = false, RoundNumber = 1 },
            new Match { MatchId = 8, HomeTeamId = 15, AwayTeamId = 16, ScoreHome = 3, ScoreAway = 1, MatchTime = new DateTime(2022, 7, 23, 16, 0, 0), LeagueId = 1, IsBest = false, RoundNumber = 1 },
            new Match { MatchId = 9, HomeTeamId = 17, AwayTeamId = 18, ScoreHome = 2, ScoreAway = 2, MatchTime = new DateTime(2022, 7, 22, 20, 45, 0), LeagueId = 1, IsBest = false, RoundNumber = 1 },

            // Druga kolejka
            new Match { MatchId = 10, HomeTeamId = 6, AwayTeamId = 13, ScoreHome = 1, ScoreAway = 0, MatchTime = new DateTime(2022, 7, 31, 21, 0, 0), LeagueId = 1, IsBest = false, RoundNumber = 2 },
            new Match { MatchId = 11, HomeTeamId = 14, AwayTeamId = 11, ScoreHome = 0, ScoreAway = 1, MatchTime = new DateTime(2022, 7, 31, 18, 30, 0), LeagueId = 1, IsBest = false, RoundNumber = 2 },
            new Match { MatchId = 12, HomeTeamId = 16, AwayTeamId = 7, ScoreHome = 2, ScoreAway = 1, MatchTime = new DateTime(2022, 7, 31, 16, 0, 0), LeagueId = 1, IsBest = true, RoundNumber = 2 },
            new Match { MatchId = 13, HomeTeamId = 8, AwayTeamId = 17, ScoreHome = 3, ScoreAway = 1, MatchTime = new DateTime(2022, 7, 31, 13, 30, 0), LeagueId = 1, IsBest = false, RoundNumber = 2 },
            new Match { MatchId = 14, HomeTeamId = 18, AwayTeamId = 9, ScoreHome = 1, ScoreAway = 1, MatchTime = new DateTime(2022, 7, 30, 20, 45, 0), LeagueId = 1, IsBest = false, RoundNumber = 2 },
            new Match { MatchId = 15, HomeTeamId = 12, AwayTeamId = 1, ScoreHome = 2, ScoreAway = 0, MatchTime = new DateTime(2022, 7, 30, 18, 15, 0), LeagueId = 1, IsBest = false, RoundNumber = 2 },
            new Match { MatchId = 16, HomeTeamId = 4, AwayTeamId = 5, ScoreHome = 2, ScoreAway = 1, MatchTime = new DateTime(2022, 7, 30, 18, 15, 0), LeagueId = 1, IsBest = false, RoundNumber = 2 },
            new Match { MatchId = 17, HomeTeamId = 2, AwayTeamId = 3, ScoreHome = 1, ScoreAway = 0, MatchTime = new DateTime(2022, 7, 30, 16, 0, 0), LeagueId = 1, IsBest = false, RoundNumber = 2 },
            new Match { MatchId = 18, HomeTeamId = 10, AwayTeamId = 15, ScoreHome = 1, ScoreAway = 0, MatchTime = new DateTime(2022, 7, 29, 20, 45, 0), LeagueId = 1, IsBest = false, RoundNumber = 2 });

        modelBuilder.Entity<TeamStats>().HasData(
            new TeamStats { TeamStatsId = 1, TeamId = 1, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 4, GoalsAgainst = 3 },
            new TeamStats { TeamStatsId = 2, TeamId = 2, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 1, GoalsAgainst = 2 },
            new TeamStats { TeamStatsId = 3, TeamId = 3, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 2, GoalsAgainst = 1 },
            new TeamStats { TeamStatsId = 4, TeamId = 4, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 2, GoalsAgainst = 3 },
            new TeamStats { TeamStatsId = 5, TeamId = 5, Points = 0, MatchesPlayed = 2, Wins = 0, Draws = 0, Losses = 2, GoalsFor = 1, GoalsAgainst = 4 },
            new TeamStats { TeamStatsId = 6, TeamId = 6, Points = 6, MatchesPlayed = 2, Wins = 2, Draws = 0, Losses = 0, GoalsFor = 3, GoalsAgainst = 0 },
            new TeamStats { TeamStatsId = 7, TeamId = 7, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 4, GoalsAgainst = 4 },
            new TeamStats { TeamStatsId = 8, TeamId = 8, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 5, GoalsAgainst = 4 },
            new TeamStats { TeamStatsId = 9, TeamId = 9, Points = 2, MatchesPlayed = 2, Wins = 0, Draws = 2, Losses = 0, GoalsFor = 2, GoalsAgainst = 2 },
            new TeamStats { TeamStatsId = 10, TeamId = 10, Points = 4, MatchesPlayed = 2, Wins = 1, Draws = 1, Losses = 0, GoalsFor = 2, GoalsAgainst = 1 },
            new TeamStats { TeamStatsId = 11, TeamId = 11, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 1, GoalsAgainst = 2 },
            new TeamStats { TeamStatsId = 12, TeamId = 12, Points = 6, MatchesPlayed = 2, Wins = 2, Draws = 0, Losses = 0, GoalsFor = 4, GoalsAgainst = 0 },
            new TeamStats { TeamStatsId = 13, TeamId = 13, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 2, GoalsAgainst = 1 },
            new TeamStats { TeamStatsId = 14, TeamId = 14, Points = 0, MatchesPlayed = 2, Wins = 0, Draws = 0, Losses = 2, GoalsFor = 0, GoalsAgainst = 3 },
            new TeamStats { TeamStatsId = 15, TeamId = 15, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 3, GoalsAgainst = 2 },
            new TeamStats { TeamStatsId = 16, TeamId = 16, Points = 3, MatchesPlayed = 2, Wins = 1, Draws = 0, Losses = 1, GoalsFor = 3, GoalsAgainst = 4 },
            new TeamStats { TeamStatsId = 17, TeamId = 17, Points = 1, MatchesPlayed = 2, Wins = 0, Draws = 1, Losses = 1, GoalsFor = 3, GoalsAgainst = 5 },
            new TeamStats { TeamStatsId = 18, TeamId = 18, Points = 2, MatchesPlayed = 2, Wins = 0, Draws = 2, Losses = 0, GoalsFor = 3, GoalsAgainst = 3 });
    }
}
