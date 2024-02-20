using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    MatchTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreHome = table.Column<int>(type: "int", nullable: false),
                    ScoreAway = table.Column<int>(type: "int", nullable: false),
                    IsBest = table.Column<bool>(type: "bit", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Matches_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamStats",
                columns: table => new
                {
                    TeamStatsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    MatchesPlayed = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStats", x => x.TeamStatsId);
                    table.ForeignKey(
                        name: "FK_TeamStats_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamUser",
                columns: table => new
                {
                    FansId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FavoriteTeamsTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUser", x => new { x.FansId, x.FavoriteTeamsTeamId });
                    table.ForeignKey(
                        name: "FK_TeamUser_AspNetUsers_FansId",
                        column: x => x.FansId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamUser_Teams_FavoriteTeamsTeamId",
                        column: x => x.FavoriteTeamsTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "LeagueId", "Name" },
                values: new object[] { 1, "Liga Belgijska" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "LeagueId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Westerlo" },
                    { 2, 1, "Cercle Brugge" },
                    { 3, 1, "Anderlecht" },
                    { 4, 1, "Oostende" },
                    { 5, 1, "KV Mechelen" },
                    { 6, 1, "Antwerp" },
                    { 7, 1, "Club Brugge" },
                    { 8, 1, "Genk" },
                    { 9, 1, "St. Truiden" },
                    { 10, 1, "Royale Union SG" },
                    { 11, 1, "Kortrijk" },
                    { 12, 1, "Leuven" },
                    { 13, 1, "Waregem" },
                    { 14, 1, "Seraing" },
                    { 15, 1, "Charleroi" },
                    { 16, 1, "Eupen" },
                    { 17, 1, "St. Liege" },
                    { 18, 1, "Gent" }
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "AwayTeamId", "HomeTeamId", "IsBest", "LeagueId", "MatchTime", "RoundNumber", "ScoreAway", "ScoreHome" },
                values: new object[,]
                {
                    { 1, 2, 1, false, 1, new DateTime(2022, 7, 24, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, 2 },
                    { 2, 4, 3, false, 1, new DateTime(2022, 7, 24, 18, 30, 0, 0, DateTimeKind.Unspecified), 1, 0, 2 },
                    { 3, 6, 5, false, 1, new DateTime(2022, 7, 24, 16, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 0 },
                    { 4, 8, 7, true, 1, new DateTime(2022, 7, 24, 13, 30, 0, 0, DateTimeKind.Unspecified), 1, 2, 3 },
                    { 5, 10, 9, false, 1, new DateTime(2022, 7, 23, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 1, 1 },
                    { 6, 12, 11, false, 1, new DateTime(2022, 7, 23, 18, 15, 0, 0, DateTimeKind.Unspecified), 1, 2, 0 },
                    { 7, 14, 13, false, 1, new DateTime(2022, 7, 23, 18, 15, 0, 0, DateTimeKind.Unspecified), 1, 0, 2 },
                    { 8, 16, 15, false, 1, new DateTime(2022, 7, 23, 16, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 3 },
                    { 9, 18, 17, false, 1, new DateTime(2022, 7, 22, 20, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, 2 },
                    { 10, 13, 6, false, 1, new DateTime(2022, 7, 31, 21, 0, 0, 0, DateTimeKind.Unspecified), 2, 0, 1 },
                    { 11, 11, 14, false, 1, new DateTime(2022, 7, 31, 18, 30, 0, 0, DateTimeKind.Unspecified), 2, 1, 0 },
                    { 12, 7, 16, true, 1, new DateTime(2022, 7, 31, 16, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 2 },
                    { 13, 17, 8, false, 1, new DateTime(2022, 7, 31, 13, 30, 0, 0, DateTimeKind.Unspecified), 2, 1, 3 },
                    { 14, 9, 18, false, 1, new DateTime(2022, 7, 30, 20, 45, 0, 0, DateTimeKind.Unspecified), 2, 1, 1 },
                    { 15, 1, 12, false, 1, new DateTime(2022, 7, 30, 18, 15, 0, 0, DateTimeKind.Unspecified), 2, 0, 2 },
                    { 16, 5, 4, false, 1, new DateTime(2022, 7, 30, 18, 15, 0, 0, DateTimeKind.Unspecified), 2, 1, 2 },
                    { 17, 3, 2, false, 1, new DateTime(2022, 7, 30, 16, 0, 0, 0, DateTimeKind.Unspecified), 2, 0, 1 },
                    { 18, 15, 10, false, 1, new DateTime(2022, 7, 29, 20, 45, 0, 0, DateTimeKind.Unspecified), 2, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "TeamStats",
                columns: new[] { "TeamStatsId", "Draws", "GoalsAgainst", "GoalsFor", "Losses", "MatchesPlayed", "Points", "TeamId", "Wins" },
                values: new object[,]
                {
                    { 1, 0, 3, 4, 1, 2, 3, 1, 1 },
                    { 2, 0, 2, 1, 1, 2, 3, 2, 1 },
                    { 3, 0, 1, 2, 1, 2, 3, 3, 1 },
                    { 4, 0, 3, 2, 1, 2, 3, 4, 1 },
                    { 5, 0, 4, 1, 2, 2, 0, 5, 0 },
                    { 6, 0, 0, 3, 0, 2, 6, 6, 2 },
                    { 7, 0, 4, 4, 1, 2, 3, 7, 1 },
                    { 8, 0, 4, 5, 1, 2, 3, 8, 1 },
                    { 9, 2, 2, 2, 0, 2, 2, 9, 0 },
                    { 10, 1, 1, 2, 0, 2, 4, 10, 1 },
                    { 11, 0, 2, 1, 1, 2, 3, 11, 1 },
                    { 12, 0, 0, 4, 0, 2, 6, 12, 2 },
                    { 13, 0, 1, 2, 1, 2, 3, 13, 1 },
                    { 14, 0, 3, 0, 2, 2, 0, 14, 0 },
                    { 15, 0, 2, 3, 1, 2, 3, 15, 1 },
                    { 16, 0, 4, 3, 1, 2, 3, 16, 1 },
                    { 17, 1, 5, 3, 1, 2, 1, 17, 0 },
                    { 18, 2, 3, 3, 0, 2, 2, 18, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_TeamId",
                table: "TeamStats",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamUser_FavoriteTeamsTeamId",
                table: "TeamUser",
                column: "FavoriteTeamsTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "TeamStats");

            migrationBuilder.DropTable(
                name: "TeamUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
