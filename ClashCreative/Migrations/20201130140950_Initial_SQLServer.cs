using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashCreative.Migrations
{
    public partial class Initial_SQLServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    RequiredTrophies = table.Column<int>(nullable: false),
                    DonationsPerWeek = table.Column<int>(nullable: false),
                    Members = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    DeckId = table.Column<int>(nullable: false),
                    Card1Id = table.Column<int>(nullable: false),
                    Card2Id = table.Column<int>(nullable: false),
                    Card3Id = table.Column<int>(nullable: false),
                    Card4Id = table.Column<int>(nullable: false),
                    Card5Id = table.Column<int>(nullable: false),
                    Card6Id = table.Column<int>(nullable: false),
                    Card7Id = table.Column<int>(nullable: false),
                    Card8Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.DeckId);
                });

            migrationBuilder.CreateTable(
                name: "GameModes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ClanTag = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    LastSeen = table.Column<string>(nullable: true),
                    CurrentDeckId = table.Column<int>(nullable: false),
                    ExpLevel = table.Column<int>(nullable: false),
                    Trophies = table.Column<int>(nullable: false),
                    BestTrophies = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false),
                    Donations = table.Column<int>(nullable: false),
                    DonationsReceived = table.Column<int>(nullable: false),
                    TotalDonations = table.Column<int>(nullable: false),
                    ClanCardsCollected = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TwoVTwo = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    Name2 = table.Column<string>(nullable: true),
                    Tag2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    BattleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team1Id = table.Column<int>(nullable: false),
                    Team2Id = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    BattleTime = table.Column<string>(nullable: true),
                    DeckSelection = table.Column<string>(nullable: true),
                    IsLadderTournament = table.Column<bool>(nullable: false),
                    GameModeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.BattleId);
                    table.ForeignKey(
                        name: "FK_Battles_GameModes_GameModeId",
                        column: x => x.GameModeId,
                        principalTable: "GameModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_GameModeId",
                table: "Battles",
                column: "GameModeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Clans");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "GameModes");
        }
    }
}
