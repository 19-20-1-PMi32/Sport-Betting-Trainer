using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SBT.Database.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    PasswordHash = table.Column<string>(type: "char(32)", nullable: false),
                    Ballance = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SportData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    Group = table.Column<string>(type: "varchar(20)", nullable: false),
                    Details = table.Column<string>(type: "varchar(50)", nullable: false),
                    Title = table.Column<string>(type: "varchar(20)", nullable: false),
                    SportId = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportData_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Team1 = table.Column<string>(type: "varchar(20)", nullable: false),
                    Team2 = table.Column<string>(type: "varchar(20)", nullable: false),
                    SportDataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_SportData_SportDataId",
                        column: x => x.SportDataId,
                        principalTable: "SportData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Coefficient = table.Column<float>(nullable: false),
                    Money = table.Column<float>(nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", nullable: false),
                    Result = table.Column<string>(type: "varchar(10)", nullable: true),
                    GameId = table.Column<int>(nullable: false),
                    AccountEmail = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Accounts_AccountEmail",
                        column: x => x.AccountEmail,
                        principalTable: "Accounts",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bets_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    LastUpdate = table.Column<int>(nullable: false),
                    FirstWin = table.Column<float>(nullable: false),
                    SecondWin = table.Column<float>(nullable: false),
                    Draw = table.Column<float>(nullable: true),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sites_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_AccountEmail",
                table: "Bets",
                column: "AccountEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_GameId",
                table: "Bets",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_SportDataId",
                table: "Games",
                column: "SportDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_GameId",
                table: "Sites",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SportData_SportId",
                table: "SportData",
                column: "SportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "SportData");

            migrationBuilder.DropTable(
                name: "Sports");
        }
    }
}
