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
            .HasOne(m => m.HomeTeam) // Mecz ma jedną drużynę gospodarzy
            .WithMany(t => t.HomeMatches) // Drużyna może mieć wiele meczów jako gospodarz
            .HasForeignKey(m => m.HomeTeamId) // Klucz obcy w tabeli Match
            .OnDelete(DeleteBehavior.Restrict); // Określ zachowanie przy usuwaniu

        // Konfiguracja relacji jeden-do-wielu dla AwayTeam
        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam) // Mecz ma jedną drużynę gości
            .WithMany(t => t.AwayMatches) // Drużyna może mieć wiele meczów jako gość
            .HasForeignKey(m => m.AwayTeamId) // Klucz obcy w tabeli Match
            .OnDelete(DeleteBehavior.Restrict); // Określ zachowanie przy usuwaniu
    }
}
