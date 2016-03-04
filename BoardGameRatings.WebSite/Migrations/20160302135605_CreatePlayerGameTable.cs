using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreatePlayerGameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("PlayerGame", table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                GameId = table.Column<int>(nullable: false),
                PlayerId = table.Column<int>(nullable: false)
            },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGame", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey("FK_PlayerGame_Game_GameId", x => x.GameId, "Game", "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_PlayerGame_Player_PlayerId", x => x.PlayerId, "Player", "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex("IX_PlayerGame_PlayerId_GameId", "PlayerGame", new[] {"PlayerId", "GameId"},
                unique: true)
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("PlayerGame");
        }
    }
}